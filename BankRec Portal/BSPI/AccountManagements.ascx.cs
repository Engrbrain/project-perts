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

public partial class DesktopModules_BSPI_AccountManagements : PortalModuleBase
{
    OperationResultInfo retVal = new OperationResultInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {


            FetchCompanies();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int CompanyID = Convert.ToInt32(ddlCompanyName.SelectedValue);
        string BankID = ddlBankName.SelectedValue;
        string ACcountID = txtAccountNo.Text;
        string Currency = txtCurrency.Text;
        string Account = txtAccount.Text;
        int Status = 0;
        if (chkActive.Checked == true)
        {
            Status = 1;
        }

        if (btnSubmit.Text == "Submit")
        {
            CreateAccount(CompanyID, BankID, ACcountID, Currency, Account);

        }
        else
        {
            UpdateCompany(Convert.ToInt32(hdnCurrent.Value), CompanyID, BankID, ACcountID, Currency, Status, Account);
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
    protected void lnkAccount_Click(object sender, EventArgs e)
    {
        Shownew();
        try
        {
            LinkButton lnk = (LinkButton)sender;
            string comValue = lnk.CommandArgument;
            pnlExist.Visible = false;
            hdnCurrent.Value = comValue;
            //txtName.Text = lnk.Text;
            btnSubmit.Text = "Update";
            chkActive.Enabled = true;
            chkActive.Checked = true;
            retVal = Account.FetchAllAccount(Convert.ToInt32(comValue), Convert.ToInt32(hdnBank.Value));
            DataSet ds = (DataSet)retVal.ReturnObject;
            DataTable dt = ds.Tables[0];

            DataRow dr = dt.Rows[0];
            ddlBankName.SelectedValue = dr["HouseBankID"].ToString();
            ddlCompanyName.SelectedValue = dr["CompanyID"].ToString();
            txtAccountNo.Text = dr["AccountID"].ToString();
            txtCurrency.Text = dr["Currency"].ToString();
            txtAccount.Text = dr["Account"].ToString();
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

    public void CreateAccount(int CompanyID, string BankID, string AccountId, string Currency, string account)
    {
        try
        {

            retVal = Account.InsertAccount(CompanyID, BankID, AccountId, Currency, account);
            int response = Convert.ToInt32(retVal.ReturnObject);
            if (response == 2)
            {
                lblInfo.CssClass = "ErrorMessage";
                lblInfo.Text = "This Account already exist for this Company under the same Bank";
            }
            else if (response == 1)
            {

                lblMessage.CssClass = "ConfirmationMessage";
                lblMessage.Text = "Account successfully created";

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
        ddlBankName.SelectedValue = "0";
        ddlCompanyName.SelectedValue = "0";
        txtAccountNo.Text = "";
        txtCurrency.Text = "";
        txtAccount.Text = "";
        pnlExist.Visible = false;
        lblMessage.Text = "";

    }
    public void Reset()
    {
        FetchExistingAccount(0, Convert.ToInt32(hdnBank.Value));
        txtCurrency.Text = "";
        txtAccountNo.Text = "";
        ddlCompanyName.SelectedValue = "0";
        ddlBankName.SelectedValue = "0";
        lblInfo.Text = "";
        pnlNew.Visible = false;
        txtAccount.Text = "";
    }
    public void FetchExistingAccount(int ID, int BankID)
    {

        try
        {
            retVal = Account.FetchAllAccount(ID, BankID);
            DataSet ds = (DataSet)retVal.ReturnObject;
            if (ds != null)
            {

                DataTable dt = ds.Tables[0];

                grvAccount.DataSource = dt;
                grvAccount.DataBind();
                pnlExisting.Visible = true;
                pnlExist.Visible = true;
            }
            else
            {
                pnlNew.Visible = false;
                pnlExist.Visible = true;
                pnlExisting.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);
            lblInfo.CssClass = "ErrorMessage";
            lblInfo.Text = "Problem encountered processing your request";
        }
    }

    public void UpdateCompany(int ID, int CompanyID, string BankID, string AccountId, string Currency, int status, string account)
    {
        try
        {
            retVal = Account.UpdateAccount(ID, CompanyID, BankID, AccountId, Currency, status, account);
            int response = Convert.ToInt32(retVal.ReturnObject);
            if (response == 2)
            {
                lblInfo.CssClass = "ErrorMessage";
                lblInfo.Text = "This Account already exist for this Company under the same Bank";
            }
            else if (response == 1)
            {

                lblMessage.CssClass = "ConfirmationMessage";
                lblMessage.Text = "Account information successfully Updated";

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

    public void FetchCompanies()
    {
        try
        {
            retVal = Company.FetchAllActiveCompany();
            DataSet ds = (DataSet)retVal.ReturnObject;
            if (ds != null)
            {
                ddlCompanyName.DataValueField = "ID";
                ddlCompanyName.DataTextField = "CompanyName";
                ddlCompanyName.DataSource = ds;
                ddlCompanyName.DataBind();

            }
        }

        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);
            lblInfo.CssClass = "ErrorMessage";
            lblInfo.Text = "Problem encountered processing your request";
        }

    }
    public void FetchBanks(int BankID)
    {
        try
        {
            retVal = HouseBank.FetchAllActiveHouseBank(BankID);
            DataSet ds = (DataSet)retVal.ReturnObject;
            if (ds != null)
            {
                ddlBankName.Items.Clear();
                ddlBankName.Items.Add(new ListItem("[Select HouseBank]", "0"));
                ddlBankName.DataValueField = "HouseBankID";
                ddlBankName.DataTextField = "Bank";
                ddlBankName.DataSource = ds;
                ddlBankName.DataBind();

            }
        }

        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);
            lblInfo.CssClass = "ErrorMessage";
            lblInfo.Text = "Problem encountered processing your request";
        }

    }
    protected void grvAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pgindex = e.NewPageIndex;
        grvAccount.PageIndex = pgindex;
        FetchExistingAccount(0, Convert.ToInt32(hdnBank.Value));
    }
    protected void ddlBankEarly_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        hdnBank.Value = ddlBankEarly.SelectedValue;
        pnlNew.Visible = false;
        FetchBanks(Convert.ToInt32(hdnBank.Value));
        FetchExistingAccount(0, Convert.ToInt32(hdnBank.Value));
    }

}