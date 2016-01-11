using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Web.Mail;
using System.Configuration;
using System.IO;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ICSharpCode.SharpZipLib.Zip;
using System.ComponentModel;
using System.Data;
using System.Reflection;


/// <summary>
/// Summary description for _Utilities
/// </summary>
namespace BankStatementProcessor.Utility
{

    #region OperationResult Region
    public class OperationResult
    {
        int _Value = 0;
        int _Status = 0;
        Exception _Error = null;
        object _RetVal = null;
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        public Exception Error
        {
            get
            {
                return _Error;
            }
            set
            {
                _Error = value;
            }
        }

        public object RetVal
        {
            get
            {
                return _RetVal;
            }
            set
            {
                _RetVal = value;
            }
        }
    }
    #endregion

    #region Common Region
    public class Common
    {

        public static void AlertBox(Page page, string message, string targetControlClientID)
        {
            string s = "<script language = javascript>";
            s += "jAlert('" + message + "','Error', function(){scrollToElement('" + targetControlClientID + "')});";
            s += "</script>";
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "script1", s);
        }



        public static void SortList<T>(List<T> dataSource, string fieldName, SortDirection sortDirection)
        {
            PropertyInfo propInfo = typeof(T).GetProperty(fieldName);
            Comparison<T> compare = delegate(T a, T b)
            {
                bool asc = sortDirection == SortDirection.Ascending;
                object valueA = asc ? propInfo.GetValue(a, null) : propInfo.GetValue(b, null);
                object valueB = asc ? propInfo.GetValue(b, null) : propInfo.GetValue(a, null);

                return valueA is IComparable ? ((IComparable)valueA).CompareTo(valueB) : 0;
            };
            dataSource.Sort(compare);
        }

        public static void DatatableToExcel(DataTable dt, string Filename)
        {
            try
            {
                string folder = HttpContext.Current.Server.MapPath("~/tempexcel");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string tempFilename = string.Format("{0}-{1}.xls", Guid.NewGuid().ToString(), DateTime.Now.Ticks);
                string fullpath = string.Format("{0}\\{1}", folder, tempFilename);

                string connStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES\"", fullpath);

                //create columns
                StringBuilder createTableScript = new StringBuilder();
                createTableScript.Append("CREATE TABLE [Sheet1]("); //create statement
                foreach (DataColumn column in dt.Columns)
                {
                    createTableScript.AppendFormat("[{0}] MEMO,", column.ColumnName, column.DataType.Name);
                }

                //remove trailing comma
                createTableScript.Remove((createTableScript.Length - 1), 1);
                createTableScript.Append(")"); //closing bracket

                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    using (OleDbCommand command = new OleDbCommand(createTableScript.ToString(), conn))
                    {
                        command.ExecuteNonQuery();
                        createTableScript = null;
                        //insert data
                        int looper = dt.Columns.Count;
                        StringBuilder insertScript = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            insertScript = new StringBuilder();
                            insertScript.Append("INSERT INTO [Sheet1] VALUES(");

                            //populate data
                            for (int i = 0; i < looper; i++)
                            {
                                insertScript.AppendFormat("'{0}',", row[i].ToString().Replace("'", "''"));
                            }

                            //remove trailing comma
                            insertScript.Remove((insertScript.Length - 1), 1);
                            insertScript.Append(")");

                            command.CommandText = insertScript.ToString();
                            command.CommandType = CommandType.Text;

                            command.ExecuteNonQuery();
                            insertScript = null;
                        }

                    }
                }

                //zip up file
                string zipFie = string.Format("{0}.zip", Filename);
                string zipFullpath = string.Format("{0}\\{1}", folder, zipFie);
                using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipFullpath)))
                {
                    zipStream.SetLevel(9);

                    byte[] buffer = new byte[4096];

                    ZipEntry entry = new ZipEntry(string.Format("{0}.xls", Filename));
                    entry.DateTime = DateTime.Now;

                    zipStream.PutNextEntry(entry);

                    using (FileStream fs = File.OpenRead(fullpath))
                    {
                        // manage memory usage.
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            zipStream.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }

                    zipStream.Finish();
                    zipStream.Close();
                }

                //clean up
                byte[] retFile = null;
                using (FileStream fs = File.OpenRead(zipFullpath))
                {
                    retFile = new byte[fs.Length];
                    fs.Read(retFile, 0, retFile.Length);
                }

                //delete temp file
                try
                {
                    File.Delete(fullpath);
                    File.Delete(zipFullpath);
                }
                catch (Exception ex)
                {
                    ErrorMgt.WriteLog(ex);
                }

                //sent file
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.AddHeader("content-disposition", string.Format("attachment;filename={0}.zip", Filename.Replace(" ", "_")));
                response.Charset = "";
                response.Cache.SetCacheability(HttpCacheability.NoCache);
                response.ContentType = "application/octet-stream";
                response.BinaryWrite(retFile);
                response.End();
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
            }
        }

        public static void DatatableToExcelNoZip(DataTable dt, string Filename, string VirtualFolderPath)
        {
            try
            {
                string folder = HttpContext.Current.Server.MapPath(VirtualFolderPath);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string fullpath = string.Format("{0}\\{1}", folder, Filename);

                string connStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES\"", fullpath);

                //create columns
                string sheetName = Guid.NewGuid().ToString().Replace("-", "");
                StringBuilder createTableScript = new StringBuilder();
                createTableScript.Append("CREATE TABLE [" + sheetName + "]("); //create statement
                foreach (DataColumn column in dt.Columns)
                {
                    createTableScript.AppendFormat("[{0}] MEMO,", column.ColumnName, column.DataType.Name);
                }

                //remove trailing comma
                createTableScript.Remove((createTableScript.Length - 1), 1);
                createTableScript.Append(")"); //closing bracket

                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    using (OleDbCommand command = new OleDbCommand(createTableScript.ToString(), conn))
                    {
                        command.ExecuteNonQuery();
                        createTableScript = null;
                        //insert data
                        int looper = dt.Columns.Count;
                        StringBuilder insertScript = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            insertScript = new StringBuilder();
                            insertScript.Append("INSERT INTO [" + sheetName + "] VALUES(");

                            //populate data
                            for (int i = 0; i < looper; i++)
                            {
                                insertScript.AppendFormat("'{0}',", row[i].ToString().Replace("'", "''"));
                            }

                            //remove trailing comma
                            insertScript.Remove((insertScript.Length - 1), 1);
                            insertScript.Append(")");

                            command.CommandText = insertScript.ToString();
                            command.CommandType = CommandType.Text;

                            command.ExecuteNonQuery();
                            insertScript = null;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
            }
        }

        public static void Export2Excel(string filename, DataTable dt)
        {
            int totrowcount = 0;

            try
            {
                string connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;\"";
                // Dim connstring As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=YES;"""
                string destinationfolder = string.Format("{0}", HttpContext.Current.Server.MapPath("~/Exceldumps\\"));
                if (!File.Exists(destinationfolder))
                    Directory.CreateDirectory(destinationfolder);
                string filepath = Path.Combine(destinationfolder, filename);
                using (OleDbConnection xlsConnection = new OleDbConnection(String.Format(connstring, filepath)))
                {
                    using (OleDbCommand xlsCommand = new OleDbCommand())
                    {
                        xlsCommand.Connection = xlsConnection;
                        xlsConnection.Open();

                        //generate all column
                        string tab = Guid.NewGuid().ToString().Replace("-", "_");
                        StringBuilder tbDefinition = new StringBuilder();
                        tbDefinition.Append("Create Table " + tab + "(");
                        foreach (DataColumn col in dt.Columns)
                        {
                            tbDefinition.AppendFormat("[{0}] {1},", col.ColumnName.ToUpper(), ChangeDataType(col.DataType.Name));
                            string col1 = col.DataType.Name;
                        }
                        string removeComma = "";
                        if (tbDefinition.ToString().EndsWith(","))
                        {
                            removeComma = tbDefinition.ToString().Substring(0, tbDefinition.Length - 1) + ")";
                        }
                        else
                        {
                            removeComma = tbDefinition.ToString();
                        }
                        string tbDef = removeComma;
                        xlsCommand.CommandText = tbDef;
                        //"Create Table newTest(a memo,b varchar(50))"
                        xlsCommand.ExecuteNonQuery();

                        //'generate row for insert                       
                        foreach (DataRow rw in dt.Rows)
                        {
                            StringBuilder rwDefinition = new StringBuilder();
                            rwDefinition.Append("Insert into " + tab + " values(");
                            for (int colindx = 0; colindx <= dt.Columns.Count - 1; colindx++)
                            {
                                rwDefinition.AppendFormat("'{0}',", rw[colindx].ToString().Replace("'", "''").ToUpper());
                            }
                            string removeRWComma = "";
                            if (tbDefinition.ToString().EndsWith(","))
                            {
                                removeRWComma = rwDefinition.ToString().Substring(0, rwDefinition.Length - 1) + ")";
                            }
                            else
                            {
                                removeRWComma = rwDefinition.ToString();
                            }
                            string rwDef = removeRWComma;
                            xlsCommand.CommandText = rwDef;
                            //"Insert into newTest values('OYEDEJI WALE','00002')"
                            //General_BLL.WriteLog(totrowcount)
                            totrowcount = totrowcount + 1;
                            xlsCommand.ExecuteNonQuery();
                            rwDefinition = null;
                        }
                    }
                    xlsConnection.Close();
                }

                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=Export_{0}.xls", DateTime.Now.Ticks));
                HttpContext.Current.Response.BufferOutput = true;
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.Charset = "UTF-8";
                int length = 0;
                byte[] data = null;

                try
                {
                    if (File.Exists(filepath))
                    {
                        using (FileStream fs = new FileStream(filepath, FileMode.Open))
                        {
                            length = Convert.ToInt32(fs.Length);
                            data = new byte[length];
                            if (length > 0)
                            {
                                fs.Read(data, 0, length);
                                fs.Close();
                            }
                        }
                        File.Delete(filepath);
                    }

                }
                catch (Exception ex)
                {
                }

                HttpContext.Current.Response.BinaryWrite(data);
                HttpContext.Current.Response.End();

            }
            catch (Exception ex)
            {
                Common.WriteLog(ex);
                //General_BLL.WriteLog(totrowcount)
            }
        }
        public static string ChangeDataType(string type)
        {
            if (type == "Int32" | type == "Int64" | type == "Int16")
            {
                return "INT";
            }
            else
            {
                return "MEMO";
            }

        }
        public static string DataTableToHTMLTableRowsCandSchool(DataTable dt, HiddenField HiddenItem, string Identifier, string ImageSource)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<tr align='center' id='thSchHeader'>");
            int rowID = 0;
            foreach (DataColumn c in dt.Columns)
            {
                html.AppendFormat("<th align='center' class='fltTableHeader'>{0}</th>", c.ColumnName);
            }
            html.AppendFormat("<th align='center' class='fltTableHeader'></th>");
            html.Append("</tr>");
            foreach (DataRow dataRow in dt.Rows)
            {
                int i;
                html.AppendFormat("<tr align='center' id='{0}{1}'>", Identifier, rowID.ToString());
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dataRow[i]))
                    {
                        html.AppendFormat("<td>{0}</td>", dataRow[i]);
                        HiddenItem.Value += dataRow[i] + ":";
                    }
                }
                string delImage = string.Format(@"<img src='{0}' id='{1}' onclick='javascript:DeleteSchool({2})' />", ImageSource, rowID.ToString(), rowID.ToString());
                html.AppendFormat("<td>{0}</td>", delImage);

                HiddenItem.Value += "~";
                rowID = rowID + 1;
                html.Append("</tr>");
            }

            //html.Append("</table>");
            return html.ToString();
        }

        public static string DataTableToHTMLTableRowsCandExam(DataTable dt, HiddenField HiddenItem, string Identifier, string ImageSource)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<tr align='center' id='thSchHeader'>");
            int rowID = 0;
            foreach (DataColumn c in dt.Columns)
            {
                html.AppendFormat("<th align='center' class='fltTableHeader'>{0}</th>", c.ColumnName);
            }
            html.AppendFormat("<th align='center' class='fltTableHeader'></th>");
            html.Append("</tr>");
            foreach (DataRow dataRow in dt.Rows)
            {
                int i;
                html.AppendFormat("<tr align='center' id='{0}{1}'>", Identifier, rowID.ToString());
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dataRow[i]))
                    {
                        html.AppendFormat("<td>{0}</td>", dataRow[i]);
                        HiddenItem.Value += dataRow[i] + ":";
                    }
                }
                string delImage = string.Format(@"<img src='{0}' id='{1}' onclick='javascript:DeleteExam({2})' />", ImageSource, rowID.ToString(), rowID.ToString());
                html.AppendFormat("<td>{0}</td>", delImage);

                HiddenItem.Value += "~";
                rowID = rowID + 1;
                html.Append("</tr>");
            }

            //html.Append("</table>");
            return html.ToString();
        }

        public static string PersistHtmlTableCandSchool(HiddenField hnd, string Header, string Identifier, string ImageSource)
        {
            string[] headerArray = Header.Split(':');

            StringBuilder html = new StringBuilder();
            html.Append("<tr align='center' id='thSchHeader'>");
            foreach (string item in headerArray)
            {
                html.AppendFormat("<th align='center' class='fltTableHeader'>{0}</th>", item);
            }

            html.Append("<th align='center' class='fltTableHeader'></th>");
            html.Append("</tr>");
            int rowID = 0;
            string[] schoolArray = new string[0];
            schoolArray = hnd.Value.Split('~');
            hnd.Value = string.Empty;
            foreach (string school in schoolArray)
            {
                if (school != string.Empty)
                {
                    html.AppendFormat("<tr align='center' id='{0}{1}'>", Identifier, rowID.ToString());
                    string[] schoolInfoArray = new string[0];
                    schoolInfoArray = school.Split(':');
                    foreach (string item in schoolInfoArray)
                    {
                        if (item != string.Empty)
                        {
                            html.AppendFormat("<td>{0}</td>", item);
                            hnd.Value += item + ":";
                        }
                    }
                    string delImage = string.Format(@"<img src='{0}' id='{1}' onclick='javascript:DeleteSchool({2})' />", ImageSource, rowID.ToString(), rowID.ToString());
                    html.AppendFormat("<td>{0}</td>", delImage);
                    html.Append("</tr>");
                    hnd.Value += "~";
                    rowID = rowID + 1;
                }
            }
            if (rowID == 0)
            {
                hnd.Value = "~";
            }
            return html.ToString();
        }

        public static string PersistHtmlTableCandExam(HiddenField hnd, string Header, string Identifier, string ImageSource)
        {
            string[] headerArray = Header.Split(':');

            StringBuilder html = new StringBuilder();
            html.Append("<tr align='center' id='thSchHeader'>");
            foreach (string item in headerArray)
            {
                html.AppendFormat("<th align='center' class='fltTableHeader'>{0}</th>", item);
            }

            html.Append("<th align='center' class='fltTableHeader'></th>");
            html.Append("</tr>");
            int rowID = 0;
            string[] schoolArray = new string[0];
            schoolArray = hnd.Value.Split('~');
            hnd.Value = string.Empty;
            foreach (string school in schoolArray)
            {
                if (school != string.Empty)
                {
                    html.AppendFormat("<tr align='center' id='{0}{1}'>", Identifier, rowID.ToString());
                    string[] schoolInfoArray = new string[0];
                    schoolInfoArray = school.Split(':');
                    foreach (string item in schoolInfoArray)
                    {
                        if (item != string.Empty)
                        {
                            html.AppendFormat("<td>{0}</td>", item);
                            hnd.Value += item + ":";
                        }
                    }
                    string delImage = string.Format(@"<img src='{0}' id='{1}' onclick='javascript:DeleteExam({2})' />", ImageSource, rowID.ToString(), rowID.ToString());
                    html.AppendFormat("<td>{0}</td>", delImage);
                    html.Append("</tr>");
                    hnd.Value += "~";
                    rowID = rowID + 1;
                }
            }
            if (rowID == 0)
            {
                hnd.Value = "~";
            }
            return html.ToString();
        }

        public static void ShowMessage(Label label, string Message, bool IsSuccess)
        {
            label.Visible = true;
            label.Text = Message;
            label.CssClass = "error";
            if (IsSuccess)
            {
                label.CssClass = "success";
            }
        }
        public static void GridviewToExcel(GridView Grid, string Filename, HttpContext context)
        {
            try
            {
                context.Response.Clear();
                context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", Filename));
                context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = "application/vnd.xls";
                context.Response.Flush();
                StringWriter stringWriter = new StringWriter();
                HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
                Grid.RenderControl(htmlWriter);
                context.Response.Write(stringWriter.ToString());
            }
            catch (Exception ex)
            {

                //WriteLog(ex);
            }
            finally
            {
                context.Response.End();
            }
        }

        public static void GridViewControlToExcel(GridView Grid, HttpContext context)
        {
            string attachment = "attachment; filename=list.xls";
            context.Response.ClearContent();
            context.Response.AddHeader("content-disposition", attachment);
            context.Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create a form to contain the grid
            HtmlForm frm = new HtmlForm();
            Grid.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(Grid);

            frm.RenderControl(htw);
            //GridView1.RenderControl(htw);
            context.Response.Write(sw.ToString());
            context.Response.End();
        }

        //public static bool UpdatePassword(UserInfo info, string OldPassword, string NewPassword)
        //{
        //    bool retVal = UserController.ChangePassword(info, OldPassword, NewPassword);

        //    return retVal;

        //}

        public static void DeleteImage(string fileName)
        {
            string file = HttpContext.Current.Server.MapPath("~/QuestionImages/" + fileName);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        public static List<Control> GetAllControls(ControlCollection ctrls)
        {
            List<Control> RetCtrls = new List<Control>();
            foreach (Control ctl in ctrls)
            {
                RetCtrls.Add(ctl);
                List<Control> SubCtrls = GetAllControls(ctl.Controls);
                RetCtrls.AddRange(SubCtrls);
            }
            return RetCtrls;
        }
        public static DataTable ConvertExcelToDatatable(string FileSource, string SheetName)
        {
            string connstr = string.Format("Provider=Microsoft.Jet.Oledb.4.0;Data Source={0};Extended Properties=Excel 8.0;", FileSource);
            OleDbConnection conn = new OleDbConnection(connstr);
            string strSQL = string.Format("SELECT * FROM [{0}$]", SheetName);
            OleDbCommand cmd = new OleDbCommand(strSQL, conn);
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];

        }

        public static void ResizeImage(string OriginalFile, string Destination, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(OriginalFile);

            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (OnlyResizeIfWider)
            {
                if (FullsizeImage.Width <= NewWidth)
                {
                    NewWidth = FullsizeImage.Width;
                }
            }

            int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
            if (NewHeight > MaxHeight)
            {
                // Resize with height instead
                NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                NewHeight = MaxHeight;
            }

            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            FullsizeImage.Dispose();

            // Save resized picture
            NewImage.Save(Destination, ImageFormat.Jpeg);
            File.Delete(OriginalFile);
        }

        public static void WriteLog(Exception ex)
        {
            if (!ex.Message.Contains("Thread was being aborted"))
            {
                string message = ex.Message + " : " + ex.StackTrace;
                HttpContext context = HttpContext.Current;
                string _path = "";
                string path = context.Server.MapPath("~/ErrorLog/ErrorLog.txt");
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine(message + " " + DateTime.Now.ToString());
                writer.Close();
            }
        }

        public static string GetMailStyle
        {
            get
            {
                return "'color: Navy;font-family: verdana, tahoma, arial;font-size: 10pt;font-weight: normal;'";
            }
        }
        public static int GetIntegerValue(Object obj)
        {
            int retVal = 0;
            try
            {
                retVal = (!(obj is DBNull) ? Convert.ToInt32(obj.ToString()) : 0);
            }
            catch
            {
            }
            return retVal;


        }

        public static string GetStringValue(object obj)
        {
            string s = "";
            try
            {
                s = (!(obj is DBNull) ? obj.ToString() : "");
            }
            catch
            {
            }
            return s;
        }

        public static decimal GetDecimalValue(Object obj)
        {
            decimal retVal = 0M;
            try
            {
                retVal = (!(obj is DBNull) ? Convert.ToDecimal(obj.ToString()) : 0M);
            }
            catch
            {
            }
            return retVal;
        }

        public static byte[] GetByteValue(Object obj)
        {
            return (!(obj is DBNull) ? (byte[])obj : null);
        }

        public static bool GetBoolValue(Object obj)
        {
            bool retVal = false;
            try
            {
                retVal = (!(obj is DBNull) ? (bool)obj : false);
            }
            catch { }
            return retVal;
        }

        public static DateTime? GetDateNullValue(Object obj)
        {
            DateTime? retVal = null;
            try
            {
                if (!(obj is DBNull))
                {
                    retVal = (DateTime)obj;
                }
                else
                {
                    retVal = null;
                }
            }
            catch { }
            return retVal;
        }

        public static DateTime GetDateValue(Object obj)
        {
            DateTime retVal = DateTime.Now;
            try
            {
                if (!(obj is DBNull))
                {
                    retVal = (DateTime)obj;
                }
            }
            catch { }
            return retVal;
        }

        public static string GetDateStringValue(Object obj)
        {
            string retVal = DateTime.Now.ToShortDateString();
            try
            {
                retVal = (!(obj is DBNull) ? ((DateTime)obj).ToShortDateString() : "");
            }
            catch { }
            return retVal;
        }


        public static byte[] ConvertStreamToByteArray(Stream stream)
        {
            // Create a buffer to hold the stream bytes
            byte[] buffer = new byte[stream.Length];

            // Read the bytes from this stream
            stream.Position = 0;
            stream.Read(buffer, 0, (int)stream.Length);
            stream.Close();
            return buffer;

        }


        public static byte[] ConvertStreamToByteArray(MemoryStream stream)
        {
            // Create a buffer to hold the stream bytes
            byte[] buffer = new byte[stream.Length];

            // Read the bytes from this stream

            stream.Position = 0;

            stream.Read(buffer, 0, (int)stream.Length);
            stream.Close();
            return buffer;

        }

        public static Bitmap ResizePicture(Stream Picture, int? PicWidth, int? PicHeight)
        {
            System.Drawing.Bitmap bmpOut = null;
            const int defaultWidth = 176;
            const int defaultHeight = 164;
            int lnWidth = PicWidth == null ? defaultWidth : (int)PicWidth;
            int lnHeight = PicHeight == null ? defaultHeight : (int)PicHeight;
            try
            {
                Bitmap loBMP = new Bitmap(Picture);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                //*** If the image is smaller than a thumbnail just return it
                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                {
                    bmpOut = loBMP;
                }
                else
                {
                    if (loBMP.Width > loBMP.Height)
                    {
                        lnRatio = (decimal)lnWidth / loBMP.Width;
                        lnNewWidth = lnWidth;
                        decimal lnTemp = loBMP.Height * lnRatio;
                        lnNewHeight = (int)lnTemp;
                    }
                    else
                    {
                        lnRatio = (decimal)lnHeight / loBMP.Height;
                        lnNewHeight = lnHeight;
                        decimal lnTemp = loBMP.Width * lnRatio;
                        lnNewWidth = (int)lnTemp;
                    }

                    bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                    Graphics g = Graphics.FromImage(bmpOut);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                    g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
                    loBMP.Dispose();
                }

                return bmpOut;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetApplicationPath
        {
            get
            {
                string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                string hostName = System.Web.HttpContext.Current.Request.Url.Host;
                //render menu item...

                return string.Format("http://{0}{1}", hostName, applicationPath);

            }
        }



        public static string FormatDate(DateTime? dt)
        {
            if (dt != null)
            {
                return String.Format("{0:dd-MMM-yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate(DateTime dt)
        {
            if (dt != null)
            {
                return String.Format("{0:dd-MMM-yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }


        }

        public static string FormatDate(string dt)
        {
            if (dt != null)
            {
                try
                {
                    return String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dt));
                }
                catch { return string.Empty; }
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate(object dt)
        {
            if (dt != null)
            {
                try
                {
                    return String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dt));
                }
                catch { return string.Empty; }
            }
            else
            {
                return string.Empty;
            }
        }


        public static string TransformEmailBody(NameValueCollection Collection, string EmailBodyTemplate)
        {
            string transformedBody = "";
            transformedBody = LoadEmailTemplate(EmailBodyTemplate);
            int count = Collection.Count;
            string key = "";
            string value = "";
            for (int i = 0; i < count; i++)
            {
                key = Collection.GetKey(i);
                value = Collection.GetValues(i)[0];
                transformedBody = transformedBody.Replace(key, value);
            }
            return transformedBody;

        }

        public static string LoadEmailTemplate(string virtualPath)
        {
            string filePath = HttpContext.Current.Server.MapPath(virtualPath);
            StreamReader streamReader = new StreamReader(filePath);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            return text;
        }



        public static void SendMail(string _body, string _to, string _subject, string _from)
        {
            SendMailHelper mailHelper = new SendMailHelper(_from, _to, _body, _subject);
            mailHelper.Run();
        }


        public static void LoadPopup(Page page, string url, int width, int height)
        {
            try
            {
                // window.open(href, windowname, 'width=400,height=200,scrollbars=yes');
                string s = "<script language = javascript>";
                s += string.Format(" window.open({0}, windowname, 'width={1},height={2},scrollbars=yes');", url, width, height);
                s += "</script>";
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "script1", s);
            }
            catch (Exception ex)
            {
                Common.WriteLog(ex);
            }
        }

        //convert List object to DataTable

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(
                    prop.Name,
                    (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        ? Nullable.GetUnderlyingType(prop.PropertyType)
                        : prop.PropertyType
                );
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static string TransformNameValue(NameValueCollection Collection, string Template)
        {
            string transformedBody = Template;
            int count = Collection.Count;
            string key = "";
            string value = "";
            for (int i = 0; i < count; i++)
            {
                key = Collection.GetKey(i);
                value = Collection.GetValues(i)[0];
                transformedBody = transformedBody.Replace(key, value);
            }
            return transformedBody;

        }
    }

    #endregion

    #region SendMailHelper Region
    public class SendMailHelper
    {
        string _from = string.Empty;
        string _to = string.Empty;
        string _body = string.Empty;
        string _subject = string.Empty;
        HttpContext context;

        public delegate bool SendMailDelegate();

        public SendMailHelper(string from, string to, string body, string subject)
        {
            _from = from;
            _to = to;
            _body = body;
            _subject = subject;
            context = HttpContext.Current;
        }

        private bool SendMail()
        {
            bool retVal = false;
            try
            {
                MailMessage MMsg = new MailMessage();
                MMsg.From = _from;
                MMsg.To = _to;
                MMsg.Subject = _subject;
                MMsg.Body = _body;
                MMsg.Priority = MailPriority.High;
                MMsg.BodyFormat = MailFormat.Html;

                string SmtpServerIP = Common.GetStringValue(ConfigurationSettings.AppSettings["SMTPServer"]);

                //MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "127.0.0.1");

                //SmtpMail.SmtpServer = "127.0.0.1";

                MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", SmtpServerIP);

                MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);

                MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2);

                MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);

                MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "appsend");

                MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "@pps3nd");

                SmtpMail.SmtpServer = SmtpServerIP;

                SmtpMail.Send(MMsg);
                retVal = true;

            }
            catch (Exception ex)
            {
                //WriteLog(ex.Message + "SendMail Module");
                //General_BLL.WriteLog(ex.Message + ex.StackTrace);
                retVal = false; ;
            }
            return retVal;

        }

        public void Run()
        {
            SendMailDelegate d = new SendMailDelegate(SendMail);

            IAsyncResult r = d.BeginInvoke(new AsyncCallback(CallbackMethod), d);
        }


        void CallbackMethod(IAsyncResult ar)
        {
            // Retrieve the delegate.
            SendMailDelegate caller = (SendMailDelegate)ar.AsyncState;

            // Call EndInvoke to retrieve the results.
            bool result = caller.EndInvoke(ar);
            // build a session string for storing mail status...
            string output = "";
            if (result == true)
            {
                output = string.Format("<span style='color:blue'>Your {0} notification was sent successfully!<br/>Message was sent on {1}</span>", _subject, DateTime.Now.ToShortDateString());
                //write log

            }
            else
            {
                output = string.Format("<span style='color:red'>An error was encountered while sending your {0} notification!<br/>Please, contact your portal administrator</span>", _subject);
                //write log
            }


        }

    }

    #endregion

    #region JSONAdapter Region
    public class JSONAdapter
    {
        public JSONAdapter()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string GetJSONString(DataSet Ds)
        {
            StringBuilder Sb = new StringBuilder();
            if (Ds.Tables.Count > 0)
            {
                string dataSetName = Ds.DataSetName;
                Sb.Append("{");
                for (int j = 0; j < Ds.Tables.Count; j++)
                {
                    if (j < Ds.Tables.Count - 1 && Ds.Tables[j + 1].Rows.Count > 0)
                    {
                        Sb.AppendFormat("{0},", GetJSON(Ds.Tables[j]));
                    }
                    else
                    {
                        Sb.AppendFormat("{0}", GetJSON(Ds.Tables[j]));
                    }
                }
                Sb.Append("}");
            }
            // string retVal = Sb.ToString().Replace("\n", "<br>");
            return Sb.ToString();
        }

        static string GetJSON(DataTable Dt)
        {
            if (Dt.Rows.Count > 0)
            {
                string[] StrDc = new string[Dt.Columns.Count];
                string HeadStr = string.Empty;

                for (int i = 0; i < Dt.Columns.Count; i++)
                {

                    StrDc[i] = Dt.Columns[i].Caption;

                    HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
                }

                HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

                StringBuilder Sb = new StringBuilder();
                Sb.Append("\"" + Dt.TableName + "\" : [");

                for (int i = 0; i < Dt.Rows.Count; i++)
                {

                    string TempStr = HeadStr;
                    Sb.Append("{");

                    for (int j = 0; j < Dt.Columns.Count; j++)
                    {

                        TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString());
                    }

                    Sb.Append(TempStr + "},");
                }

                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                Sb.Append("]");
                string retVal = Sb.ToString();
                retVal = retVal.Replace("\\", HttpContext.Current.Server.UrlEncode("\\"));
                retVal = retVal.Replace("\r\n", "<br>");
                retVal = retVal.Replace("\r", "<br>");
                retVal = retVal.Replace("\n", "<br>");


                return retVal;
            }
            else
            {
                return "";
            }
        }

        public static string GetJSONString(DataTable Dt)
        {
            if (Dt.Rows.Count > 0)
            {
                string[] StrDc = new string[Dt.Columns.Count];
                string HeadStr = string.Empty;

                for (int i = 0; i < Dt.Columns.Count; i++)
                {

                    StrDc[i] = Dt.Columns[i].Caption;

                    HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
                }

                HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

                StringBuilder Sb = new StringBuilder();
                Sb.Append("{\"" + Dt.TableName + "\" : [");

                for (int i = 0; i < Dt.Rows.Count; i++)
                {

                    string TempStr = HeadStr;
                    Sb.Append("{");

                    for (int j = 0; j < Dt.Columns.Count; j++)
                    {

                        TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString());
                    }

                    Sb.Append(TempStr + "},");
                }

                Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
                Sb.Append("]}");
                string retVal = Sb.ToString();
                retVal = retVal.Replace("\\", HttpContext.Current.Server.UrlEncode("\\"));
                retVal = retVal.Replace("\r\n", "<br>");
                retVal = retVal.Replace("\r", "<br>");
                retVal = retVal.Replace("\n", "<br>");

                return retVal;
            }
            else
            {
                return "";
            }
        }



    }
    #endregion

    #region Constants Region
    public class Constants
    {

        public const string PASSPORTFOLDER = "~/Passports/";
        public const string DOCUMENTTFOLDER = "~/Documents/";
        public const string Debit = "Dr";
        public const string PASSPORTFOLDERTEMP = "~/Passports/temp/";
        public const int DIRECT_APPLICANT = 1;
        public const int SPECIAL_APPLICANT = 2;

        public const int First_Bank = 1;
        public const int Diamond_Bank = 2;
        public const int Fidelity_Bank = 3;
        public const int City_Bank = 4;
        public const int StanbicIBTC_Bank = 5;
        public const int Zenith_Bank = 6;
        public const int Skye_Bank = 7;
        public const int Stanbic_ibtc = 8;
        public const int United_Bank = 9;
        public const int Union_Bank = 10;
        public const int Wema_Bank = 11;
        public const int GTB_Bank = 12;


        //ROLES
        //public const string ROLE_CollegeAdmin = "CollegeAdmin";

    }
    #endregion

    #region DataTransformationToCSV
    public class DataTransformation
    {
        public static void DatatableToCSVFile(DataTable dt, string Filename)
        {
            try
            {
                StringBuilder script = new StringBuilder();

                if (dt.Rows.Count > 0)
                {
                    //form the header
                    foreach (DataColumn column in dt.Columns)
                    {
                        script.AppendFormat("{0},", column.ColumnName);
                    }

                    //remove the last \t
                    script.Remove((script.Length - 1), 1);
                    script.AppendLine();

                    //form the data
                    foreach (DataRow row in dt.Rows)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (!Convert.IsDBNull(row[i]))
                            {
                                script.AppendFormat("{0},", EscapeText(row[i]));
                            }
                            else
                            {
                                script.AppendFormat(",");
                            }
                        }

                        //remove the last \t
                        script.Remove((script.Length - 1), 1);
                        script.AppendLine();
                    }

                }

                //sent file
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.AddHeader("content-disposition", string.Format("attachment;filename={0}.csv", Filename));
                response.Charset = "";
                response.Cache.SetCacheability(HttpCacheability.NoCache);
                response.ContentType = "application/vnd.ms-excel";
                response.Write(script.ToString());
                response.End();
            }
            catch (Exception ex)
            {
                Common.WriteLog(ex);
            }
        }

        public static void GridviewToExcel(GridView gv, string fileName)
        {
            try
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", fileName));
                response.Charset = "";
                response.Cache.SetCacheability(HttpCacheability.NoCache);
                response.ContentType = "application/vnd.xls";
                //response.ContentType = "application/vnds-excel";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                gv.RenderControl(htmlWrite);
                response.Write(stringWrite.ToString());
                response.End();
            }
            catch (Exception ex)
            {
                Common.WriteLog(ex);
            }

        }


        private static string EscapeText(Object Data)
        {
            string Text;
            if (Data.GetType().Name == "DateTime")
            {
                Text = DateTime.Parse(Data.ToString()).ToString("ddd d MMM, yyyy");
            }
            else
            {
                Text = Data.ToString();
            }
            if (Text.Contains(","))
            {
                Text = string.Format("\"{0}\"", Text);
            }

            return Text;
        }
        public static DataTable ExcelToDataSet(string SourceFilename)
        {
            DataTable ds = new DataTable();
            try
            {
                //string connStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1;\"", SourceFilename);

                //OleDbConnection conn = new OleDbConnection(connStr);

               string connstr = string.Format("Provider=Microsoft.Jet.Oledb.4.0;Data Source={0};Extended Properties=Excel 8.0;", SourceFilename);
                OleDbConnection conn = new OleDbConnection(connstr);
                conn.Open();
                DataTable schemaDT = conn.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
                conn.Close();

                string tableName = schemaDT.Rows[0]["TABLE_NAME"].ToString();

                OleDbCommand cmd = new OleDbCommand(string.Format("SELECT * FROM [{0}]", tableName), conn);

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(ds);

                foreach (DataRow dr in ds.Rows)
                {

                    if (dr.IsNull(0) == true)
                    {


                        dr.Delete();
                    }

                }


            }
            catch (Exception ex)
            {
                Common.WriteLog(ex);

            }


            return ds;
        }

        public static DataTable Parse(string fileName)
        {
            string connectionString = string.Format("provider=Microsoft.Jet.OLEDB.4.0; data source={0};Extended Properties=Excel 8.0;", fileName);


            DataSet data = new DataSet();

            foreach (var sheetName in GetExcelSheetNames(connectionString))
            {
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    var dataTable = new DataTable();
                    string query = string.Format("SELECT * FROM [{0}]", sheetName);
                    con.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                    adapter.Fill(dataTable);
                    data.Tables.Add(dataTable);
                }
            }
            DataTable ds = data.Tables[0];
             foreach (DataRow dr in ds.Rows)
                {

                    if (dr.IsNull(0) == true)
                    {


                        dr.Delete();
                    }

                }
            return ds;
        }

        static string[] GetExcelSheetNames(string connectionString)
        {
            OleDbConnection con = null;
            DataTable dt = null;
            con = new OleDbConnection(connectionString);
            con.Open();
            dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }

            String[] excelSheetNames = new String[dt.Rows.Count];
            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                excelSheetNames[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            return excelSheetNames;
        }
    }
    #endregion

    #region ErrorMgt
    public class ErrorMgt
    {
        public static void WriteLog(Exception ex)
        {
            if (ex.Message == "Thread was being aborted.")
            {
                return;
            }

            HttpContext context = HttpContext.Current;
            string file = string.Empty;
            if (context != null)
            {
                file = context.Server.MapPath(@"~/ErrorLog\ErrorLog.txt");
            }
            else
            {
                //handle threading
                string folder = AppDomain.CurrentDomain.BaseDirectory;
                file = string.Format("{0}ErrorLog\\ErrorLogThread.txt", folder);
            }
            StreamWriter writer;
            using (writer = new StreamWriter(file, true))
            {
                writer.WriteLine(string.Format("============================{0}=====================================", DateTime.Now.ToString()));
                writer.WriteLine(ex.Message);
                writer.WriteLine("------Stack Trace---");
                writer.WriteLine(ex.StackTrace);
            }
        }

        public static void WriteLog(string Message)
        {
            HttpContext context = HttpContext.Current;
            string file = string.Empty;
            if (context != null)
            {
                file = context.Server.MapPath(@"~/ErrorLog\ErrorLog.txt");
            }
            else
            {
                //handle threading
                string folder = AppDomain.CurrentDomain.BaseDirectory;
                file = string.Format("{0}ErrorLog\\ErrorLogThread.txt", folder);
            }
            StreamWriter writer;
            using (writer = new StreamWriter(file, true))
            {
                writer.WriteLine(string.Format("============================{0}=====================================", DateTime.Now.ToString()));
                writer.WriteLine(Message);
            }
        }
    }
    #endregion


}
