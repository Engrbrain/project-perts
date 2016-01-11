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
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using Empermos._Utilities;
public partial class DesktopModules_BSPI_ReconCiliations : PortalModuleBase
{
    DataSet ds = new DataSet();


    OperationResultInfo retVal = new OperationResultInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
            HtmlLink   objLink = new HtmlLink();
            objLink.ID = ID;
            objLink.Attributes["rel"] = "stylesheet";
            objLink.Attributes["type"] = "text/css";
            objLink.Href ="css.css";
            Page.Header.Controls.Add(objLink);

            bool isAdmin = UserInfo.Roles.Contains(Empermos._Utilities.Constants.Accountant);
            int UserID = UserInfo.UserID;
            retVal = C2GREport.GetUserBU(UserID.ToString());
            hdnBU.Value = (string)retVal.ReturnObject;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        hdnStatementDate.Value = Convert.ToDateTime(txtDate.Text).ToString("yyyyMMdd");
        hdnAccountNo.Value = "0000" + txtAccountNo.Text;
        string GLACcountNumber = hdnAccountNo.Value;
        string ClientID = ddlClient.SelectedValue;
        if (hdnStatementDate.Value.ToString() == "1/1/0001 12:00:00 AM")
        {
            lblErrorLabel.Text = "Ensure to select a Statement Date from the Control";

        }
        else if (GetExistence(GLACcountNumber))
        {
            lblErrorLabel.Text = "";
            FetchStatemnetBreakDown(hdnStatementDate.Value, hdnAccountNo.Value,  hdnBU.Value, ddlClient.SelectedValue);
        }
        else
        {
            lblErrorLabel.Text = "An Account with such Number does not exist";
            pnlexistingprog.Visible = false;
        }


    }

    public bool GetExistence(string AccountNo)
    {

        bool Result = false;
        retVal = C2GREport.GetExistence(AccountNo);
        if (Convert.ToInt32(retVal.ReturnObject) == 1)
        {
            Result = true;
        }
        else
        {
            Result = false;
        }

        return Result;
    }


    public void FetchStatemnetBreakDown(string Date, string AccountNo, string Company, string Client)
    {


        try
        {
            retVal = C2GREport.GetMAinAccountReport(Date, AccountNo, Company, Client);
            DataSet ds = (DataSet)retVal.ReturnObject;
            if (ds != null)
            {


                DataTable dt = ds.Tables[1];
                if (dt.Rows.Count != 0)
                {
                    grdCleared.DataSource = dt;
                    ViewState["UnclearedPayment"] = dt;
                    grdCleared.DataBind();
                    pnlUnclearedPayments.Visible = true;
                    btnExport.Visible = true;
                    lblUnclearedP.Text = "";
                }
                else
                {
                    grdCleared.DataSource = null;
                    grdCleared.DataBind();
                    lblUnclearedP.Text = "No Item";
                    pnlUnclearedPayments.Visible = true;
                    btnExport.Visible = false;
                }


                DataTable dtC = ds.Tables[2];
                if (dtC.Rows.Count != 0)
                {
                    grdTest.DataSource = dtC;
                    ViewState["UnclearedDepo"] = dtC;
                    grdTest.DataBind();
                    pnlUnclearedDeposit.Visible = true;
                    btncd.Visible = true;
                    lblUncelaredDepo.Text = " ";
                }
                else
                {
                    grdTest.DataSource = null;
                    grdTest.DataBind();
                    btncd.Visible = false;
                    pnlUnclearedDeposit.Visible = true;
                    lblUncelaredDepo.Text = "No Item ";
                }


                DataTable dts = ds.Tables[0];
                if (dts.Rows.Count != 0)
                {
                    grdUncleared.DataSource = dts;
                    ViewState["Cleared"] = dts;
                    grdUncleared.DataBind();
                    pnlCleared.Visible = true;
                    btnExportToExcel.Visible = true;
                    lblCleared.Text = "";
                }
                else
                {
                    grdUncleared.DataSource = null;
                    grdUncleared.DataBind();
                    pnlCleared.Visible = true;
                    lblCleared.Text = " No Item";
                    btnExportToExcel.Visible = false;
                }
                DataTable dtsURec = ds.Tables[3];
                if (dtsURec.Rows.Count != 0)
                {

                    grdUnrec.DataSource = dtsURec;
                    ViewState["UnRec"] = dtsURec;
                    grdUnrec.DataBind();
                    pnlCleared.Visible = true;
                    btnExportUR.Visible = true;
                    lblUnrec.Text = "";
                }
                else
                {
                    grdUnrec.DataSource = null;
                    grdUnrec.DataBind();
                    pnlCleared.Visible = true;
                    btnExportUR.Visible = false;
                    lblUnrec.Text = " No Item";

                }
                pnlexistingprog.Visible = true;
                pnlSummary.Visible = false;


                DataSet dsSummarys = (DataSet)retVal.ReturnObject;
                DataTable dtSummarys = dsSummarys.Tables[5];

                DataRow drSummarys = dtSummarys.Rows[0];
                lblA.Text = drSummarys["NotOndSystemCount"].ToString() + " Items";
                lblB.Text = string.Format("{0:#,#}", drSummarys["NotOndSystemFigure"].ToString());


                DataSet dsSummary = (DataSet)retVal.ReturnObject;
                DataTable dtSummary = dsSummary.Tables[4];

                DataRow drSummary = dtSummary.Rows[0];

                lblOPeningBalance.Text = string.Format("{0:#,#}", drSummary["OpeningBalance"].ToString());

                lblUnclearedCheckCount.Text = drSummary["UnClearedIncomingChequesCount"].ToString() + " Items/";
                lblUncleraedDepositCount.Text = drSummary["UnClearedCheckCount"].ToString() + " Items/";
                lblUnclearedDepositFigure.Text = string.Format("{0:#,#}", drSummary["UnClearedCheckFigure"].ToString());
                UnclearedCheckFigure.Text = string.Format("{0:#,#}", drSummary["UnClearedIncomingChequesFigure"].ToString());

                Label3.Text = drSummary["UnclearedOtherPaymentINCount"].ToString() + " Items/";
                Label5.Text = drSummary["UnclearedOtherPaymentOutCount"].ToString() + " Items/";
                Label6.Text = string.Format("{0:#,#}", drSummary["UnclearedOtherPaymentOutFigure"].ToString());
                Label4.Text = string.Format("{0:#,#}", drSummary["UnclearedOtherPaymentINSum"].ToString());
                Label7.Text = string.Format("{0:#,#}", drSummary["ChequeClearedSums"].ToString());


                lblClearedCount.Text = drSummary["ClearedCheckCount"].ToString() + " Items/";
                lblClearedFigure.Text = string.Format("{0:#,#}", drSummary["ClearedCheckFigure"].ToString());
                lblDepositCount.Text = drSummary["ClearedDepositCount"].ToString() + " Items/";
                lblClearedDepositFigure.Text = string.Format("{0:#,#}", drSummary["ClearedDepositFigure"].ToString());


                lblAccountNAme.Text = drSummary["AccountName"].ToString();

                lblclearedSum.Text = string.Format("{0:#,#}", drSummary["ClearedSum"].ToString());
                lblUnclearedSum.Text = string.Format("{0:#,#}", drSummary["UnclearedSum"].ToString());
                lblRegisteredbalance.Text = string.Format("{0:#,#}", drSummary["RegisteredBalance"].ToString());
                lblAccounType.Text = drSummary["AccountClass"].ToString();



                DateTime ItemDate = Convert.ToDateTime(txtDate.Text);
                lblDate.Text = "Transaction Period:" + ItemDate.Year.ToString() + "/" + ItemDate.Month.ToString();
                lblBSCB.Text = string.Format("{0:#,#}", drSummary["BankStatementClosingBalance"].ToString());
                lblCR.Text = string.Format("{0:#,#}", drSummarys["NotOndSystemFigureCR"].ToString());
                lblDR.Text = string.Format("{0:#,#}", drSummarys["NotOndSystemFigureDR"].ToString());
                //lblExpected.Text = string.Format("{0:#,#}", drSummarys["ExpectedBankClosing"].ToString());

                lblAccountNo.Text = "AccountNo: " + hdnAccountNo.Value;
            }
            else
            {
                pnlexistingprog.Visible = false;
                pnlSummary.Visible = false;
            }

        }
        catch (Exception ex)
        {
           BankStatementProcessor.Utility. ErrorMgt.WriteLog(ex);
            lblInfo.CssClass = "ErrorMessage";
            lblInfo.Text = "Problem encountered processing your request";
        }
    }
    protected void lnkNew_Click(object sender, EventArgs e)
    {
        pnlSummary.Visible = true;
        pnlexistingprog.Visible = false;
    }

    public void FetchStatemnetSummary(DateTime Date, string AccountNo, string company, string Client)
    {

        try
        {
            retVal = C2GREport.GetMAinAccountReport(Date.ToString(), AccountNo, company, ddlClient.SelectedValue);
            DataSet ds = (DataSet)retVal.ReturnObject;
            if (ds != null)
            {

                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                // hdnAccountId.Value = dr["AccountID"].ToString();

            }
            else
            {
                pnlexistingprog.Visible = false;
                pnlSummary.Visible = false;
            }

        }
        catch (Exception ex)
        {
            BankStatementProcessor.Utility.ErrorMgt.WriteLog(ex);
            lblInfo.CssClass = "ErrorMessage";
            lblInfo.Text = "Problem encountered processing your request";
        }
    }


    private void ExporttoExcel(DataTable table, string Date)
    {



        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Date);

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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlSummary.Visible = false;
        pnlexistingprog.Visible = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

        string Date = hdnStatementDate.Value;
        string Year = Date.Substring(0, 4);
        string Month = Date.Substring(4, 2);
        string Months = "UnclearedIncoming" + Year + "/" + Month;
        if (ViewState["UnclearedPayment"] != null)
        {
            DataTable dts = (DataTable)ViewState["UnclearedPayment"];
            ExporttoExcel(dts, Months + ".xls");
        }

    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {

        string Date = hdnStatementDate.Value;
        string Year = Date.Substring(0, 4);
        string Month = Date.Substring(4, 2);
        string Months = "Cleared" + Year + "/" + Month;
        if (ViewState["Cleared"] != null)
        {
            DataTable dts = (DataTable)ViewState["Cleared"];
            ExporttoExcel(dts, Months + ".xls");
        }
        //DateTime ItemDate = Convert.ToDateTime(hdnStatementDate.Value);
        //string Month = "Cleared" + ItemDate.Year.ToString() + "/" + ItemDate.Month.ToString();
        //if (ViewState["Cleared"] != null)
        //{
        //    DataTable dts = (DataTable)ViewState["Cleared"];
        //    ExporttoExcel(dts, Month + ".xls");
        //}
    }
    protected void grdUncleared_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        int pgindex = e.NewPageIndex;
        grdUncleared.PageIndex = pgindex;
        FetchStatemnetBreakDown(hdnStatementDate.Value, hdnAccountNo.Value,  hdnBU.Value, ddlClient.SelectedValue);


    }
    protected void grdCleared_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pgindex = e.NewPageIndex;
        grdCleared.PageIndex = pgindex;
        FetchStatemnetBreakDown(hdnStatementDate.Value, hdnAccountNo.Value,  hdnBU.Value, ddlClient.SelectedValue);

    }

    protected void grdTest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pgindex = e.NewPageIndex;
        grdTest.PageIndex = pgindex;
        FetchStatemnetBreakDown(hdnStatementDate.Value, hdnAccountNo.Value,  hdnBU.Value, ddlClient.SelectedValue);

    }

    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();
    }



    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Printable;

        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
        Response.Redirect("ReconCiliation.aspx", true);
    }
    protected void btncd_Click(object sender, EventArgs e)
    {
        string Date = hdnStatementDate.Value;
        string Year = Date.Substring(0, 4);
        string Month = Date.Substring(4, 2);
        string Months = "UnclearedOutgoing" + Year + "/" + Month;
        if (ViewState["UnclearedDepo"] != null)
        {
            DataTable dts = (DataTable)ViewState["UnclearedDepo"];
            ExporttoExcel(dts, Months + ".xls");
        }

        //DateTime ItemDate = Convert.ToDateTime(hdnStatementDate.Value);
        //string Month = "UnclearedDepo" + ItemDate.Year.ToString() + "/" + ItemDate.Month.ToString();
        //if (ViewState["UnclearedDepo"] != null)
        //{
        //    DataTable dts = (DataTable)ViewState["UnclearedDepo"];
        //    ExporttoExcel(dts, Month + ".xls");
        //}
    }
    protected void btnExportUR_Click(object sender, EventArgs e)
    {
        string Dates = hdnStatementDate.Value;
        string Year = Dates.Substring(0, 4);
        string Month = Dates.Substring(4, 2);
        string Months = "UnRec" + Year + "/" + Month;
        if (ViewState["UnRec"] != null)
        {
            DataTable dts = (DataTable)ViewState["UnRec"];
            ExporttoExcel(dts, Months + ".xls");
        }
    }

    protected void grdUnrec_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pgindex = e.NewPageIndex;
        grdUnrec.PageIndex = pgindex;
        FetchStatemnetBreakDown(hdnStatementDate.Value, hdnAccountNo.Value,  hdnBU.Value, ddlClient.SelectedValue);

    }

}