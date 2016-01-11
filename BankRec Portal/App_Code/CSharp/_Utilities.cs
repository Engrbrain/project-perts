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
using DotNetNuke.Entities.Users;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ICSharpCode.SharpZipLib.Zip;
using System.ComponentModel;
using System.Reflection;


/// <summary>
/// Summary description for _Utilities
/// </summary>
namespace Empermos._Utilities
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
        private static Random Rnd = new Random();
        public static string RandomizeNumber()
        {
            string number = "";
            for (int i = 0; i < 10; i++)
            {
                number = number + Convert.ToString(Rnd.Next(0, 9));
            }
            return number;
        }
        public static string Randomize(bool isGuid)
        {
            string guid = Guid.NewGuid().ToString();
            guid = guid.Replace("-", "").Substring(0, 10);
            return guid;
        }
        public static string Randomize()
        {
            string number = "";
            for (int i = 0; i < 16; i++)
            {
                number = number + Convert.ToString(Rnd.Next(0, 9));
            }
            return number;
        }

        public static void AlertBox(Page page, string message, string targetControlClientID)
        {
            string s = "<script language = javascript>";
            s += "jAlert('" + message + "','Error', function(){scrollToElement('" + targetControlClientID + "')});";
            s += "</script>";
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "script1", s);
        }
        public static string Tokenize(string amount)
        {

            return (amount.Split('.')[0].Replace(",", ""));

        }

        public static void CreateAuditLog(int userId, string description, int moduleAction)
        {
            //APP_ActivitiesLogInfo newActivity = new APP_ActivitiesLogInfo();
            //newActivity.ActionDate = DateTime.Now.ToString("yyyy/MM/dd");
            //newActivity.Modules_ActionsID = moduleAction;
            //newActivity.ActionTime = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            //newActivity.UserID = userId;
            //newActivity.Description = description;
            //ActivitiesLog.insertActivity(newActivity);
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
                    createTableScript.AppendFormat("[{0}] MEMO,", column.ColumnName.ToUpper(), column.DataType.Name);
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
            }
            catch (Exception ex)
            {
                ErrorMgt.WriteLog(ex);
            }
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

        public static bool UpdatePassword(UserInfo info, string OldPassword, string NewPassword)
        {
            bool retVal = UserController.ChangePassword(info, OldPassword, NewPassword);

            return retVal;

        }

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

        public static string FormatDate_YYYY(DateTime? dt)
        {
            if (dt != null)
            {
                return String.Format("{0:yyyy/MM/dd}", dt);
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
                s += string.Format(" window.open({0}, windowname, 'width={1},height={2},scrollbars=yes');",url,width,height);
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
            if (data.Count <= 0)
                return null;

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

                string SmtpServerIP = Common.GetStringValue(ConfigurationSettings.AppSettings["SmtpServerIP"]);

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
        public static int PARTICIPANT_USERID = 0;

        public static string Accountant = "Accountant";
        //public static string STAFF_ROLES = "Staff";
        public static int STAFF_ROLEID = 11;
        public static string ADMIN_ROLES = "BU Admin";
        public static int STUDENT_ROLEID = 3;
        public static int FIELD_TYPE_TEXT = 1;
        public static int FIELD_TYPE_NUMBER = 2;
        public static int FIELD_TYPE_MULTIPLE_TEXT = 3;
        public static int FIELD_TYPE_YES_NO = 4;
        public static int FIELD_TYPE_DATE = 5;
        public static int FIELD_TYPE_DROP_DOWN = 6;
        public static int FIELD_TYPE_EMAIL = 7;
        public static int FIELD_TYPE_CONSTANT = 8;

        public static int NOT_CUSTOMIZE = 0;
        public static int INTERSWITCH = 1;
        public static int ETRANZACT = 2;
        public static int BANK = 3;
        public static int VISA = 4;
        public static int PAYMENT_SUCCESSFUL = 1;
        public static int PAYMENT_FAILED = 2;

        public static int MODULE_CREATE_REPORT = 63;   // to change...
        public static int MODULE_DELETE_REPORT = 65;
        public static int MODULE_UPDATE_REPORT = 64;

        public static string VENDOR_DOCUMENT = "~/Documents";

        public static string REPORT_BUILDER_LIST_PAGE = "~/Reports/LibraryReportBuilder/tabid/64/Default.aspx";
        public static string REPORT_BUILDER_EDIT_PAGE = "~/Reports/LibraryReportBuilder/EditLibraryReport/tabid/68/Default.aspx";
        public static string REPORT_BUILDER_EXECUTE_PAGE = "~/Reports/LibraryReportBuilder/ExecuteLibraryReport/tabid/85/Default.aspx";
        public static string REPORT_BUILDER_ADD_PAGE = "~/Reports/LibraryReportBuilder/CreateLibraryReport/tabid/65/Default.aspx";

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

        public static void GridviewToExcel(GridView gv,string fileName)
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
                System.IO.StringWriter stringWrite =new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite  = new HtmlTextWriter(stringWrite);
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
        public static DataSet ExcelToDataSet(string SourceFilename)
        {
            DataSet ds = new DataSet();
            try
            {
                string connStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=YES;\"", SourceFilename);

                OleDbConnection conn = new OleDbConnection(connStr);
                conn.Open();
                DataTable schemaDT = conn.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
                conn.Close();

                string tableName = schemaDT.Rows[0]["TABLE_NAME"].ToString();

                OleDbCommand cmd = new OleDbCommand(string.Format("SELECT * FROM [{0}]", tableName), conn);

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                Common.WriteLog(ex);

            }

            return ds;
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
            string file = context.Server.MapPath(@"~/ErrorLog\ErrorLog.txt");
            StreamWriter writer;
            using (writer = new StreamWriter(file, true))
            {
                writer.WriteLine(string.Format("{0} {1} {2}", ex.Message, ex.StackTrace, DateTime.Now.ToString()));
            }
        }
    }
    #endregion

    // #region Activity Log
    //public class ActivityLog
    //{
       
    //    public static void CreateAuditLog(int userId, string description, int moduleAction)
    //    {


    //        APP_ActivitiesLog newActivity = new APP_ActivitiesLog();
    //        newActivity.ActionDate = DateTime.Now.ToString("yyyy/MM/dd");
    //        newActivity.Modules_ActionsID = moduleAction;
    //        newActivity.ActionTime = String.Format("{0} : {1} : {2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
    //        newActivity.UserID = userId;
    //        newActivity.Description = description;
    //        ActivityLogDAL.InsertLog(newActivity);
    //    }
    //    #endregion

    //}
}
