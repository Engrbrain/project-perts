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

public partial class DesktopModules_BSPI_SystemSettingMain : PortalModuleBase
{
    OperationResultInfo retVal = new OperationResultInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {


        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        int BankID = Convert.ToInt32(ddlBankEarly.SelectedValue);
        string Transaction = txtTransaction.Text;
        string ExternalTransaction = txtExternalTransaction.Text;
        string TransactionCategory = txtTranscationCategory.Text;
        string OperationType = txtOperationType.Text;
        string BankName = ddlBankEarly.SelectedItem.Text;
        int Status = 0;
        if (chkActive.Checked == true)
        {
            Status = 1;
        }

        if (btnSubmit.Text == "Submit")
        {
            CreateSettings(BankID, Transaction, ExternalTransaction, TransactionCategory, OperationType, BankName);
        }
        else
        {
            UpdateSetting(Convert.ToInt32(hdnCurrent.Value), BankID, Transaction, ExternalTransaction, TransactionCategory, OperationType, Status, BankName);
        }
    }
    protected void lnkExisting_Click(object sender, EventArgs e)
    {
        if (hdnBank.Value == "0")
        {
            lblMessage.CssClass = "ConfirmationMessage";
            lblMessage.Text = "Select a Bank from the List Above";
        }
        else
        {
            lblMessage.Text = "";
            Reset();
        }
    }
    protected void lnkNew_Click(object sender, EventArgs e)
    {
        if (hdnBank.Value == "0")
        {
            lblMessage.CssClass = "ConfirmationMessage";
            lblMessage.Text = "Select a Bank from the List Above";
        }
        else
        {
            lblMessage.Text = "";
            chkActive.Enabled = false;
            chkActive.Checked = true;
            btnSubmit.Text = "Submit";
            Shownew();
        }
    }
    protected void lnkSystems_Click(object sender, EventArgs e)
    {
        Shownew();
        try
        {
            LinkButton lnk = (LinkButton)sender;
            string comValue = lnk.CommandArgument;
            pnlExist.Visible = false;
            hdnCurrent.Value = comValue;

            btnSubmit.Text = "Update";
            chkActive.Enabled = true;
            chkActive.Checked = true;
            retVal = SystemSetting.FetchAllSystemsettings(Convert.ToInt32(comValue), Convert.ToInt32(hdnBank.Value));
            DataSet ds = (DataSet)retVal.ReturnObject;
            DataTable dt = ds.Tables[0];

            DataRow dr = dt.Rows[0];
            txtExternalTransaction.Text = dr["ExternalCode"].ToString();
            txtOperationType.Text = dr["Operationtype"].ToString(); ;
            txtTransaction.Text = dr["Transaction"].ToString(); ;
            txtTranscationCategory.Text = dr["BankTransactionCategory"].ToString(); ;
            ddlBankEarly.SelectedValue = dr["BankID"].ToString(); ;

            int result = Convert.ToInt32(dr["Statuss"].ToString());
            if (result == 0)
            {
                chkActive.Checked = false;
            }

        }
        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);
            lblInfo.CssClass = "ErrorMessage";
            lblInfo.Text = "Problem encountered processing your request";
        }

    }


    public void CreateSettings(int BankId, string Transaction, string ExternalTransaction, string TransactionCategory, string operationType, string BankName)
    {
        try
        {
            retVal = SystemSetting.InsertSystemSetting(BankId, Transaction, ExternalTransaction, TransactionCategory, operationType, BankName);
            int response = Convert.ToInt32(retVal.ReturnObject);
            if (response == 2)
            {
                lblInfo.CssClass = "ErrorMessage";
                lblInfo.Text = "This system setting already exist for this Bank";
            }
            else if (response == 1)
            {

                lblMessage.CssClass = "ConfirmationMessage";
                lblMessage.Text = "Company successfully created";

                Reset();
            }

        }
        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);
            lblInfo.CssClass = "ErrorMessage";
            lblInfo.Text = "Problem encountered processing your request";
        }
    }
    public void Shownew()
    {
        pnlNew.Visible = true;
        //ddlBankName.SelectedValue = "0";
        txtExternalTransaction.Text = "";
        txtOperationType.Text = "";
        txtTransaction.Text = "";
        txtTranscationCategory.Text = "";

        pnlExist.Visible = false;
        lblMessage.Text = "";

    }
    public void Reset()
    {
        FetchExistingSettings(0, Convert.ToInt32(hdnBank.Value));
        pnlNew.Visible = false;
        lblMessage.Text = "";

    }
    public void FetchExistingSettings(int ID, int BankID)
    {

        try
        {
            retVal = SystemSetting.FetchAllSystemsettings(ID, BankID);
            DataSet ds = (DataSet)retVal.ReturnObject;
            if (ds != null)
            {


                DataTable dt = ds.Tables[0];
                grvSystems.DataSource = dt;
                grvSystems.DataBind();
                pnlExisting.Visible = true;
                pnlExist.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "";
            }
            else
            {
                pnlExisting.Visible = true;
                pnlNew.Visible = false;
                pnlExist.Visible = true;
                lblMessage.Text = " No data exist for this bank";
            }

        }
        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);
            lblInfo.CssClass = "ErrorMessage";
            lblInfo.Text = "Problem encountered processing your request";
        }
    }

    public void UpdateSetting(int ID, int BankId, string Transaction, string ExternalTransaction, string TransactionCategory, string operationType, int status, string BankName)
    {
        try
        {
            retVal = SystemSetting.UpdateSystemSettings(ID, BankId, Transaction, ExternalTransaction, TransactionCategory, operationType, status, BankName);
            int response = Convert.ToInt32(retVal.ReturnObject);
            if (response == 2)
            {
                lblInfo.CssClass = "ErrorMessage";
                lblInfo.Text = "This system setting already exist for this Bank";
            }
            else if (response == 1)
            {

                lblMessage.CssClass = "ConfirmationMessage";
                lblMessage.Text = "Settings information successfully Updated";

                Reset();
            }

        }
        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);
            lblInfo.CssClass = "ErrorMessage";
            lblInfo.Text = "Problem encountered processing your request";
        }
    }
    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grvSystems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pgindex = e.NewPageIndex;
        grvSystems.PageIndex = pgindex;
        FetchExistingSettings(0, Convert.ToInt32(hdnBank.Value));
    }
    protected void ddlBankEarly_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        hdnBank.Value = ddlBankEarly.SelectedValue;
        pnlNew.Visible = false;
        pnlExisting.Visible = true;
        FetchExistingSettings(0, Convert.ToInt32(hdnBank.Value));
    }
}