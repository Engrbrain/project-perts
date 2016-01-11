using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankStatementProcessor.ObjectInfo;
using BankStatementProcessor.Utility;
using System.Data;
using BankStatementProcessor.BLL;
using DotNetNuke.Framework;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Common;
using DotNetNuke;
using DotNetNuke.Entities;
using iTextSharp;
using System.IO;
public partial class DesktopModules_BSPI_StatementUploadss : PortalModuleBase
{
    DataSet ds = new DataSet();
    bool DateRelations = false;
    OperationResultInfo retVal = new OperationResultInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {

            FetchAccountType();
          

            bool isAdmin = UserInfo.Roles.Contains(Empermos._Utilities.Constants.Accountant);
            int UserID = UserInfo.UserID;
            retVal = C2GREport.GetUserBU(UserID.ToString());
            hdnBU.Value = (string)retVal.ReturnObject;
            retVal = C2GREport.GetBUAccount(hdnBU.Value);
            ddlMemType.SelectedValue = (string)retVal.ReturnObject;
            ddlMemType.Enabled = false;
            GetConfig(ddlMemType.SelectedValue);

        }
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        hdnStatementDate.Value = txtDate.Text;
        hdnStatement.Value = txtStatement.Text;
        try
        {
            if (Page.IsValid)
            {
                if (txtFileName.HasFile)
                {
                    //Upload components
                    //hdnFileName.Value = txtFileName.PostedFile.ToString();
                    Object Try = txtFileName.PostedFile;
                    List<string> Columnss = new List<string>();
                    Columnss = (List<string>)Session["Format"];
                    OperationResultInfo retVal = StatementUploads.ProcesUpload((HttpPostedFile)Try, Columnss);


                    if (retVal.Status.Equals(false))
                    {
                        Common.ShowMessage(lblErrorLabel, retVal.ErrorMessage, false);

                    }
                    else
                    {

                        grdprogrammes.DataSource = retVal.ReturnObject;
                        grdprogrammes.DataBind();
                        CloseAllPanels();
                        //   ds = (DataSet)retVal.ReturnObject;
                        Session["data"] = (DataTable)retVal.ReturnObject;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);
            retVal.Status = false;
            retVal.ErrorMessage = "Error processing data!";
        }




    }

    private void CloseAllPanels()
    {
        pnlnewprogrm.Visible = false;
        pnlSample.Visible = false;
        pnlexistingprog.Visible = true;
        lblErrorLabel.Text = "";
        grdprogrammes.Visible = true;
    }

    private void OpenAllPanels()
    {

        pnlnewprogrm.Visible = true;
        pnlexistingprog.Visible = false;
        lblErrorLabel.Text = "";
        grdprogrammes.DataSource = null;
       // ddlMemType.SelectedValue = "0";
        txtStatement.Text = "";
        txtDate.Text = "";

        pnlSample.Visible = false;


        bool isAdmin = UserInfo.Roles.Contains(Empermos._Utilities.Constants.Accountant);
        int UserID = UserInfo.UserID;
        retVal = C2GREport.GetUserBU(UserID.ToString());
        hdnBU.Value = (string)retVal.ReturnObject;
        retVal = C2GREport.GetBUAccount(hdnBU.Value);
        ddlMemType.SelectedValue = (string)retVal.ReturnObject;
        ddlMemType.Enabled = false;
        GetConfig(ddlMemType.SelectedValue);

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        OpenAllPanels();
    }

    public DataTable ProcessEntryFBN(DataGrid gd)
    {
        int RowNumber = 1;
        double OpeningBalance = 0;
        string ClosingBalance = "";
        string PostingDate = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataRow dr;
        string Transaction = "";
        string ExternalTransaction = "";
        dt.Columns.Add("Client");
        dt.Columns.Add("CompanyCode");
        dt.Columns.Add("House Bank Key");
        dt.Columns.Add("Account ID");
        dt.Columns.Add("Statement No");
        dt.Columns.Add("Statement Date");
        dt.Columns.Add("Opening Balance");
        dt.Columns.Add("Closing Balance");
        dt.Columns.Add("Posting date");
        dt.Columns.Add("Line Item No.");
        dt.Columns.Add("Transaction");
        dt.Columns.Add("Value Date");
        dt.Columns.Add("Amount");
        dt.Columns.Add("Currency");
        dt.Columns.Add("External Transaction");
        dt.Columns.Add("Bank Reference");
        dt.Columns.Add("Posting Text");


        for (int i = 0; i < 1; i++)
        {
            foreach (DataGridItem GR in grdprogrammes.Items)
            {
                //if (GR.RowType == DataControlRowType.DataRow)
                //{
                    string Right = GR.Cells[7].Text.Trim().ToString();
                    string Left = GR.Cells[6].Text.Trim().ToString();
                    if (GR.Cells[5].Text.ToString() == "Cr")
                    {
                        OpeningBalance = Convert.ToDouble(Right) - Convert.ToDouble(Left);
                    }
                    else
                    {
                        OpeningBalance = Convert.ToDouble(Right) + Convert.ToDouble(Left);
                    }
                    if (i == 0)
                    {
                        break;
                    }
                //}
            }

        }
        // for (int i = 0; i < grdprogrammes.Rows.Count; i++ )
        //{
        int Counter = 0;
        foreach (DataGridItem GR in grdprogrammes.Items)
        {
           // if (GR.RowType == DataControlRowType.DataRow)
            //{


                if (Counter == grdprogrammes.Items.Count - 1)
                {
                    ClosingBalance = GR.Cells[7].Text.ToString().Trim();
                    PostingDate = TryToParse(GR.Cells[10].Text.ToString());
                    break;
                }
                Counter = Counter + 1;

           // }

            //}

        }



        foreach (DataGridItem GR in grdprogrammes.Items)
        {

           // if (GR.RowType == DataControlRowType.DataRow)
           // {

                dr = dt.NewRow();
                dr["Client"] = "";
                dr["CompanyCode"] = hdnCompanyCode.Value;
                dr["House Bank Key"] = hdnHouseBank.Value;
                dr["Account ID"] = hdnAccountId.Value;
                dr["Statement No"] = hdnStatement.Value;// Convert.ToDateTime(txtDate.Text).Month.ToString().Trim();
                dr["Statement Date"] = Convert.ToDateTime(hdnStatementDate.Value.TrimStart()).ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                dr["Opening Balance"] = OpeningBalance.ToString();
                dr["Closing Balance"] = ClosingBalance;
                dr["Posting date"] = PostingDate;

                dr["Line Item No."] = RowNumber++;

                if (Session["Config"] != null)
                {
                    List<Statement> Config = (List<Statement>)Session["Config"];

                    foreach (Statement s in Config)
                    {

                        
                  if (GR.Cells[5].Text.ToString().ToUpper().Trim() == s.CrDr.ToUpper().Trim() && GR.Cells[9].Text.ToString().ToUpper().Trim() == s.TxnCategory.ToUpper().Trim())
                        {
                            Transaction = s.Transactions;
                            ExternalTransaction = s.ExternalTransactions;



                        }


                    }


                    dr["Transaction"] = Transaction;
                    dr["Value Date"] = TryValueDate(GR.Cells[11].Text.ToString());
                   

  if (GR.Cells[5].Text.ToString().ToUpper().Trim() == "DR")
                    {
                        dr["Amount"] = GR.Cells[6].Text.ToString().Replace(",", "").Trim() + "-";


                    }
                    else
                    {
                        dr["Amount"] = GR.Cells[6].Text.ToString().Replace(",","").Trim();

                    }


                    dr["Currency"] = hdnCurrency.Value;
                    dr["External Transaction"] = ExternalTransaction;
                    dr["Bank Reference"] = "";
                    dr["Posting Text"] = GR.Cells[2].Text.ToString();


 Transaction = "N/A";
                            ExternalTransaction = "N/A";

                    dt.Rows.Add(dr);
                }
           // }
        }




        return dt;
    }
    public DataTable ProcessEntryZenith(DataGrid gd)
    {
        int RowNumber = 1;
        double OpeningBalance = 0;
        string ClosingBalance = "";
        string PostingDate = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataRow dr;
        string Transaction = "";
        string ExternalTransaction = "";
        dt.Columns.Add("Client");
        dt.Columns.Add("CompanyCode");
        dt.Columns.Add("House Bank Key");
        dt.Columns.Add("Account ID");
        dt.Columns.Add("Statement No");
        dt.Columns.Add("Statement Date");
        dt.Columns.Add("Opening Balance");
        dt.Columns.Add("Closing Balance");
        dt.Columns.Add("Posting date");
        dt.Columns.Add("Line Item No.");
        dt.Columns.Add("Transaction");
        dt.Columns.Add("Value Date");
        dt.Columns.Add("Amount");
        dt.Columns.Add("Currency");
        dt.Columns.Add("External Transaction");
        dt.Columns.Add("Bank Reference");



        for (int i = 0; i < 1; i++)
        {
            foreach (DataGridItem GR in grdprogrammes.Items)
            {
                //if (GR.RowType == DataControlRowType.DataRow)
                //{
                    string Right = GR.Cells[7].Text.ToString();
                    string Left = GR.Cells[6].Text.ToString();
                    if (GR.Cells[4].Text.ToString() == "credit")
                    {
                        OpeningBalance = Convert.ToDouble(Right) - Convert.ToDouble(Left);
                    }
                    else
                    {
                        OpeningBalance = Convert.ToDouble(Right) + Convert.ToDouble(Left);
                    }
                    if (i == 0)
                    {
                        break;
                    }
               // }
            }

        }
        // for (int i = 0; i < grdprogrammes.Rows.Count; i++ )
        //{
        int Counter = 0;
        foreach (DataGridItem GR in grdprogrammes.Items)
        {
           // if (GR.RowType == DataControlRowType.DataRow)
           // {


                if (Counter == grdprogrammes.Items.Count - 1)
                {
                    ClosingBalance = GR.Cells[7].Text.ToString().Trim();
                    PostingDate = TryToParse(GR.Cells[0].Text.ToString());
                    break;
                }
                Counter = Counter + 1;

           // }

            //}

        }



        foreach (DataGridItem GR in grdprogrammes.Items)
        {

            //if (GR.RowType == DataControlRowType.DataRow)
            //{

                dr = dt.NewRow();
                dr["Client"] = "";
                dr["CompanyCode"] = hdnCompanyCode.Value;
                dr["House Bank Key"] = hdnHouseBank.Value;
                dr["Account ID"] = hdnAccountId.Value;
                dr["Statement No"] = hdnStatement.Value;// Convert.ToDateTime(txtDate.Text).Month.ToString().Trim();
                dr["Statement Date"] = Convert.ToDateTime(hdnStatementDate.Value.TrimStart()).ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                dr["Opening Balance"] = OpeningBalance.ToString();
                dr["Closing Balance"] = ClosingBalance;
                dr["Posting date"] = PostingDate;

                dr["Line Item No."] = RowNumber++;

                if (Session["Config"] != null)
                {
                    List<Statement> Config = (List<Statement>)Session["Config"];

                    foreach (Statement s in Config)
                    {

                        if (GR.Cells[4].Text.ToString() == s.CrDr && GR.Cells[5].Text.ToString() == s.TxnCategory)
                        {
                            Transaction = s.Transactions;
                            ExternalTransaction = s.ExternalTransactions;



                        }
                    }


                    dr["Transaction"] = Transaction;
                    dr["Value Date"] = TryValueDate(GR.Cells[1].Text.ToString());
                    if (GR.Cells[4].Text.ToString() == "debit")
                    {
                        dr["Amount"] = GR.Cells[6].Text.ToString() + "-";


                    }
                    else
                    {
                        dr["Amount"] = GR.Cells[6].Text.ToString();

                    }

                    dr["Currency"] = hdnCurrency.Value;
                    dr["External Transaction"] = ExternalTransaction;
                    dr["Bank Reference"] = "";

                    dt.Rows.Add(dr);
                }
           // }
        }




        return dt;
    }
    public DataTable ProcessEntryGTMethod(DataGrid gd)
    {
        int RowNumber = 1;
        double OpeningBalance = 0;
        string ClosingBalance = "";
        string PostingDate = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataRow dr;
        string Transaction = "";
        string ExternalTransaction = "";
        dt.Columns.Add("Client");
        dt.Columns.Add("CompanyCode");
        dt.Columns.Add("House Bank Key");
        dt.Columns.Add("Account ID");
        dt.Columns.Add("Statement No");
        dt.Columns.Add("Statement Date");
        dt.Columns.Add("Opening Balance");
        dt.Columns.Add("Closing Balance");
        dt.Columns.Add("Posting date");
        dt.Columns.Add("Line Item No.");
        dt.Columns.Add("Transaction");
        dt.Columns.Add("Value Date");
        dt.Columns.Add("Amount");
        dt.Columns.Add("Currency");
        dt.Columns.Add("External Transaction");
        dt.Columns.Add("Bank Reference");



        for (int i = 0; i < 1; i++)
        {
            foreach (DataGridItem GR in grdprogrammes.Items)
            {
                //if (GR.RowType == DataControlRowType.DataRow)
                //{
                    string Right = GR.Cells[5].Text.ToString();
                    string Left = GR.Cells[3].Text.ToString();
                    if (GR.Cells[4].Text.ToString() == "&nbsp;")
                    {
                        OpeningBalance = Convert.ToDouble(Right) - Convert.ToDouble(Left);
                    }
                    else
                    {

                        OpeningBalance = Convert.ToDouble(Right) + Convert.ToDouble(GR.Cells[4].Text.ToString());
                    }
                    if (i == 0)
                    {
                        break;
                    }
               // }
            }

        }
        // for (int i = 0; i < grdprogrammes.Rows.Count; i++ )
        //{
        int Counter = 0;
        foreach (DataGridItem GR in grdprogrammes.Items)
        {
            //if (GR.RowType == DataControlRowType.DataRow)
           // {


                if (Counter == grdprogrammes.Items.Count - 1)
                {
                    ClosingBalance = GR.Cells[5].Text.ToString().Trim();
                    PostingDate = TryToParse(GR.Cells[0].Text.ToString());
                    break;
                }
                Counter = Counter + 1;

           // }

            //}

        }



        foreach (DataGridItem GR in grdprogrammes.Items)
        {

            //if (GR.RowType == DataControlRowType.DataRow)
            //{

                dr = dt.NewRow();
                dr["Client"] = "";
                dr["CompanyCode"] = hdnCompanyCode.Value;
                dr["House Bank Key"] = hdnHouseBank.Value;
                dr["Account ID"] = hdnAccountId.Value;
                dr["Statement No"] = hdnStatement.Value;// Convert.ToDateTime(txtDate.Text).Month.ToString().Trim();
                dr["Statement Date"] = Convert.ToDateTime(hdnStatementDate.Value.TrimStart()).ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                dr["Opening Balance"] = OpeningBalance.ToString();
                dr["Closing Balance"] = ClosingBalance;
                dr["Posting date"] = PostingDate;

                dr["Line Item No."] = RowNumber++;

                if (Session["Config"] != null)
                {
                    List<Statement> Config = (List<Statement>)Session["Config"];

                    foreach (Statement s in Config)
                    {

                        if (GR.Cells[4].Text.ToString() == "&nbsp;" && GR.Cells[6].Text.ToString() == s.TxnCategory)
                        {
                            Transaction = s.Transactions;
                            ExternalTransaction = s.ExternalTransactions;



                        }
                        else if (GR.Cells[4].Text.ToString() != "&nbsp;" && GR.Cells[6].Text.ToString() == s.TxnCategory)
                        {
                            Transaction = s.Transactions;
                            ExternalTransaction = s.ExternalTransactions;



                        }
                    }


                    dr["Transaction"] = Transaction;
                    dr["Value Date"] = TryValueDate(GR.Cells[2].Text.ToString());
                    if (GR.Cells[3].Text.ToString() != "")
                    {
                        dr["Amount"] = GR.Cells[3].Text.ToString() + "";


                    }
                    else if (GR.Cells[4].Text.ToString() != "")
                    {
                        dr["Amount"] = GR.Cells[4].Text.ToString() + "-";

                    }

                    dr["Currency"] = hdnCurrency.Value;
                    dr["External Transaction"] = ExternalTransaction;
                    dr["Bank Reference"] = "";

                    dt.Rows.Add(dr);
                }
           // }
        }




        return dt;
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        try
        {
            string PostingDate = "";

            int Counter = 0;
            if (Convert.ToInt32(hdnBank.Value) == Constants.Zenith_Bank || Convert.ToInt32(hdnBank.Value) == Constants.First_Bank || Convert.ToInt32(hdnBank.Value) == Constants.Fidelity_Bank)
            {
                foreach (DataGridItem GR in grdprogrammes.Items)
                {
                    //if (GR.RowType == DataControlRowType.DataRow)
                    //{

                    
                       // if (Counter == GR.Cells.Count - 10)
                       // {

                            PostingDate = TryToParse(GR.Cells[10].Text.ToString());
                            break;
                       // }
                       // Counter = Counter + 1;

                   // }



                }
            }
            else if (Convert.ToInt32(hdnBank.Value) == Constants.GTB_Bank)
            {
                foreach (DataGridItem GR in grdprogrammes.Items)
                {
                    //if (GR.RowType == DataControlRowType.DataRow)
                    //{


                    //if (Counter == GR.Cells.Count - 1)
                       // {

                            PostingDate = TryToParse(GR.Cells[0].Text.ToString());
                            break;
                       // }
                       // Counter = Counter + 1;

                   // }



                }
            }
            DateRelations = CompareDates(PostingDate, Convert.ToDateTime(hdnStatementDate.Value.TrimStart()).ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            if (DateRelations == true)
            {
                if (Convert.ToInt32(hdnBank.Value) == Constants.Zenith_Bank)
                {
                    DataTable dt = ProcessEntryZenith(grdprogrammes);



                    ExporttoExcel(dt);
                    OpenAllPanels();

                }
                else if (Convert.ToInt32(hdnBank.Value) == Constants.GTB_Bank)
                {
                    DataTable dt = ProcessEntryGTMethod(grdprogrammes);
                    ExporttoExcel(dt);
                    OpenAllPanels();
                }
                else if (Convert.ToInt32(hdnBank.Value) == Constants.First_Bank)
                {

                    DataTable dt = ProcessEntryFBN(grdprogrammes);

                    ExporttoExcel(dt);
                    OpenAllPanels();
                }
                else if (Convert.ToInt32(hdnBank.Value) == Constants.Fidelity_Bank)
                {

                    DataTable dt= ProcessEntryFBN(grdprogrammes);
                    
                    ExporttoExcel(dt);
                    OpenAllPanels();
                }



            }
            else
            {

                lblErrorLabel.CssClass = "ErrorMessage";
                lblErrorLabel.Text = "The selected Statement Date  does not match the Posting Date. Ensure this is done";

            }
        }
        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);

            lblErrorLabel.CssClass = "ErrorMessage";
            lblErrorLabel.Text = "Problem encountered processing your request";
        }
    }

    protected void grdprogrammes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int pgindex = e.NewPageIndex;
           // grdprogrammes.PageIndex = pgindex;
            grdprogrammes.DataSource = (DataSet)Session["data"];
            grdprogrammes.DataBind();
        }
        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);

            lblErrorLabel.CssClass = "ErrorMessage";
            lblErrorLabel.Text = "Problem encountered processing your request";
        }
    }
    private void ExporttoExcel(DataTable table)
    {



        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=FormattedBankStatement.xls");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");

        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;
        HttpContext.Current.Response.Write("<style> .text { mso-number-format: \\@; } </style> ");



        for (int j = 0; j < columnscount; j++)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(table.Columns[j].ColumnName.ToString());
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");

            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());

                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
    

    protected void ddlMemType_SelectedIndexChanged(object sender, EventArgs e)
    {

        
    }

    public void GetConfig(string AccountID)
    {

        retVal = Account.FetchBankByAccountID(Convert.ToInt32(AccountID));
        DataSet sp = (DataSet)retVal.ReturnObject;
        DataTable dtSp = sp.Tables[0];
        DataRow drSP = dtSp.Rows[0];

        if (drSP["BankID"].ToString() != null)
        {
            pnlSample.Visible = true;
        }

        else
        {
            pnlSample.Visible = false;
        }

        try
        {
            //retVal = Account.FetchAllActiveAccount(Convert.ToInt32(ddlMemType.SelectedValue);
            retVal = Account.FetchAllActiveAccount();
            DataSet ds = (DataSet)retVal.ReturnObject;
            DataTable dt = ds.Tables[0];

            DataRow dr = dt.Rows[0];
            hdnAccountId.Value = drSP["AccountID"].ToString();
            hdnCompanyCode.Value = drSP["CompanyCode"].ToString();
            hdnCurrency.Value = drSP["Currency"].ToString();
            hdnHouseBank.Value = drSP["HouseBankID"].ToString();
            hdnBank.Value = drSP["Bank"].ToString();


            FetchConfig(Convert.ToInt32(drSP["BankID"].ToString()));



            FetchSampleByBankID(Convert.ToInt32(drSP["BankID"].ToString()));
        }
        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);

            lblErrorLabel.CssClass = "ErrorMessage";
            lblErrorLabel.Text = "Problem encountered processing your request";
        }


    }

    public void FetchAccountType()
    {

        try
        {
            retVal = Account.FetchAllActiveAccount();
            DataSet ds = (DataSet)retVal.ReturnObject;
            if (ds != null)
            {
                ddlMemType.DataTextField = "Account";
                ddlMemType.DataValueField = "ID";
                ddlMemType.DataSource = ds;
                ddlMemType.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);

            lblErrorLabel.CssClass = "ErrorMessage";
            lblErrorLabel.Text = "Problem encountered processing your request";
        }
    }

    public void FetchConfig(int BankID)
    {
        Session["Config"] = null;
        List<Statement> st = new List<Statement>();
        Statement s = new Statement();
        try
        {
            retVal = SystemSetting.FetchAllSystemsettingsBYBank(BankID);
            DataSet ds = (DataSet)retVal.ReturnObject;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    s = new Statement();
                    s.Transactions = r["Transaction"].ToString();
                    s.ExternalTransactions = r["Externalcode"].ToString();
                    s.TxnCategory = r["BankTransactionCategory"].ToString();
                    s.CrDr = r["OperationType"].ToString();
                    st.Add(s);
                }
                Session["Config"] = st;
                btnUpload.Enabled = true;
                lblErrorLabel.Text = "";
            }
            else
            {
                lblErrorLabel.Text = "You cannot Process a Bank Statement at this time because the Configuartion for the Selected Bank has not been done by the Administrator";
                btnUpload.Enabled = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void FetchSampleByBankID(int BankId)
    {
        try
        {
            retVal = Format.FetchAllActiveFormatByBankID(BankId);
            List<string> CNames = new List<string>();
            DataSet ds = (DataSet)retVal.ReturnObject;
            DataTable dts = ds.Tables[0];

            if (dts.Rows.Count > 0)
            {

                btnUpload.Enabled = true;
                lblErrorLabel.Text = "";
                DataRow dr;

                int i = 0;
                DataTable dt = new DataTable();
                List<string> Test = new List<string>();
                int j = 1;

                if (ds != null)
                {

                    foreach (DataRow row in dts.Rows)
                    {

                        dt.Columns.Add(row["ColumnName"].ToString());
                        CNames.Add(row["ColumnName"].ToString());

                    }
                    dt.Rows.Add("");
                    foreach (DataRow row in dts.Rows)
                    {
                        dt.Rows[0].SetField(i, row["SampleText"].ToString());


                        i++;

                    }

                    DataTable dttt = dt;
                    grdSamples.DataSource = dt;
                    grdSamples.DataBind();
                    Session["Format"] = CNames;
                    pnlSample.Visible = true;


                    lblInformation.CssClass = "ErrorMessage";
                    lblInformation.Text = "*Please note that the Excel document you are expected to upload should take the format below. Also save your file in 97-2003 Template.xlt";
                }


                else
                {
                    lblErrorLabel.Text = "You cannot Process a Bank Statement at this time because the Sample for Upload has not been Configured by the Administrator";
                    btnUpload.Enabled = false;

                }
            }

        }
        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);

            lblErrorLabel.CssClass = "ErrorMessage";
            lblErrorLabel.Text = "Problem encountered processing your request";
        }

    }

    public string TryToParse(string value)
    {
        DateTime number;
        string Test = "";
        bool result = DateTime.TryParse(value, out number);
        if (result)
        {
            DateTime OriginalDate = Convert.ToDateTime(value);
            DateTime lastDate = new DateTime(OriginalDate.Year, OriginalDate.Month, 1).AddMonths(1).AddDays(-1);
            string MainDate = lastDate.ToString();
            Test = Convert.ToDateTime(MainDate).ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }
        else
        {
            retVal = C2GDateGetters.GetDate(value);
            string Pst = retVal.ReturnObject.ToString();
            char[] array = Pst.ToCharArray();
            Test = Pst.Replace("-", "");
        }
        return Test;
    }
    public string TryValueDate(string value)
    {
        DateTime number;
        string Test = "";

        bool result = DateTime.TryParse(value, out number);
        if (result)
        {
            DateTime OriginalDate = Convert.ToDateTime(value);
            DateTime lastDate = new DateTime(OriginalDate.Year, OriginalDate.Month, 1).AddMonths(1).AddDays(-1);
            string MainDate = lastDate.ToString();
            Test = Convert.ToDateTime(MainDate).ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }
        else
        {

            retVal = C2GDateGetters.GetValueDate(value);
            string Pst = retVal.ReturnObject.ToString();
            char[] array = Pst.ToCharArray();
            Test = Pst.Replace("-", "");
        }

        return Test;
    }
    public bool CompareDates(string PostingDate, string StatementDate)
    {
        DateTime number;
        bool Confirm = false;
        string Test1 = "";
        string Test2 = "";
        bool result = DateTime.TryParse(StatementDate, out number);
        if (result)
        {
            DateTime OriginalDate = Convert.ToDateTime(StatementDate);
            DateTime lastDate = new DateTime(OriginalDate.Year, OriginalDate.Month, 1).AddMonths(1).AddDays(-1);
            string MainDate = lastDate.ToString();
            Test1 = Convert.ToDateTime(MainDate).ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }
        else
        {
            retVal = C2GDateGetters.GetDate(StatementDate);
            string Pst = retVal.ReturnObject.ToString();
            char[] array = Pst.ToCharArray();
            Test1 = Pst.Replace("-", "");
        }

        bool result1 = DateTime.TryParse(PostingDate, out number);
        if (result1)
        {
            DateTime OriginalDate = Convert.ToDateTime(PostingDate);
            DateTime lastDate = new DateTime(OriginalDate.Year, OriginalDate.Month, 1).AddMonths(1).AddDays(-1);
            string MainDate = lastDate.ToString();
            Test2 = Convert.ToDateTime(MainDate).ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }
        else
        {
            retVal = C2GDateGetters.GetDate(PostingDate);
            string Pst = retVal.ReturnObject.ToString();
            char[] array = Pst.ToCharArray();
            Test2 = Pst.Replace("-", "");
        }

        if (Test1 == Test2)
        {
            Confirm = true;
        }
        return Confirm;

    }
}
