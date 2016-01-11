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

public partial class DesktopModules_BSPI_HouseBankManagements : PortalModuleBase
{
    OperationResultInfo retVal = new OperationResultInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {

            //FetchBanks();

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int BankID = Convert.ToInt32(hdnBank.Value);
        string HouseBankID = txtHsbBID.Text;
        string BankName = ddlBankEarly.SelectedItem.Text;
        int Status = 0;
        if (chkActive.Checked == true)
        {
            Status = 1;
        }

        if (btnSubmit.Text == "Submit")
        {
            CreateBank(BankID, HouseBankID, BankName);
        }
        else
        {
            UpdateBank(Convert.ToInt32(hdnCurrent.Value), BankID, HouseBankID, Status, BankName);
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
            btnSubmit.Text = "Submit";
            chkActive.Checked = true;
            Shownew();
        }
    }
    protected void lnkBanks_Click(object sender, EventArgs e)
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
            retVal = HouseBank.FetchAllHouseBank(Convert.ToInt32(comValue), Convert.ToInt32(hdnBank.Value));
            DataSet ds = (DataSet)retVal.ReturnObject;
            DataTable dt = ds.Tables[0];

            DataRow dr = dt.Rows[0];
            txtHsbBID.Text = dr["HouseBankId"].ToString();
            ddlBankEarly.SelectedValue = dr["BankId"].ToString();
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

    public void CreateBank(int BankId, string HouseBankID, string BankName)
    {
        try
        {
            retVal = HouseBank.InsertHouseBank(BankId, HouseBankID, BankName);
            int response = Convert.ToInt32(retVal.ReturnObject);
            if (response == 2)
            {
                lblInfo.CssClass = "ErrorMessage";
                lblInfo.Text = " HouseBankId already exist under the selected Bank";
            }
            else if (response == 1)
            {

                lblMessage.CssClass = "ConfirmationMessage";
                lblMessage.Text = "HouseBank  successfully created";

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
        txtHsbBID.Text = "";
        pnlExist.Visible = false;
        lblMessage.Text = "";

    }
    public void Reset()
    {
        FetchExistingHouseBanks(0, Convert.ToInt32(hdnBank.Value));
        txtHsbBID.Text = "";
        // ddlBankName.SelectedValue = "0";
        lblInfo.Text = "";
        pnlNew.Visible = false;
        pnlExist.Visible = true;

    }
    public void FetchExistingHouseBanks(int ID, int BankId)
    {

        try
        {
            retVal = HouseBank.FetchAllHouseBank(ID, BankId);
            DataSet ds = (DataSet)retVal.ReturnObject;
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                grvBanks.DataSource = dt;
                grvBanks.DataBind();
                pnlExisting.Visible = true;
                pnlExist.Visible = true;
            }
            else
            {
                pnlNew.Visible = false;
                pnlExist.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ErrorMgt.WriteLog(ex);
            lblInfo.CssClass = "ErrorMessage";
            lblInfo.Text = "Problem encountered processing your request";
        }
    }
    //public void FetchBanks()
    //{
    //    try
    //    {
    //        retVal = Bank.FetchAllActiveBank();
    //        DataSet ds = (DataSet)retVal.ReturnObject;
    //        if (ds != null)
    //        {

    //            ddlBankName.DataValueField = "ID";
    //            ddlBankName.DataTextField = "Bank";
    //            ddlBankName.DataSource = ds;
    //            ddlBankName.DataBind();

    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        ErrorMgt.WriteLog(ex);
    //        lblInfo.CssClass = "ErrorMessage";
    //        lblInfo.Text = "Problem encountered processing your request";
    //    }

    //}
    public void UpdateBank(int ID, int BankId, string HouseBankID, int status, string BankName)
    {
        try
        {
            retVal = HouseBank.UpdateHouseBank(ID, BankId, HouseBankID, status, BankName);
            int response = Convert.ToInt32(retVal.ReturnObject);
            if (response == 2)
            {
                lblInfo.CssClass = "ErrorMessage";
                lblInfo.Text = "HouseBAnkId already exist under same Bank";
            }
            else if (response == 1)
            {

                lblMessage.CssClass = "ConfirmationMessage";
                lblMessage.Text = "HouseBank information successfully Updated";

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
    protected void grvBanks_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pgindex = e.NewPageIndex;
        grvBanks.PageIndex = pgindex;

        FetchExistingHouseBanks(0, Convert.ToInt32(hdnBank.Value));

    }
    protected void ddlBankEarly_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        hdnBank.Value = ddlBankEarly.SelectedValue;
        pnlNew.Visible = false;
        pnlExisting.Visible = true;
        FetchExistingHouseBanks(0, Convert.ToInt32(hdnBank.Value));
    }
}