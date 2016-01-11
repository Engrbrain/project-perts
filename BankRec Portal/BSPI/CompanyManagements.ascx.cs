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
public partial class DesktopModules_BSPI_CompanyManagements : PortalModuleBase
{
    OperationResultInfo retVal = new OperationResultInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {
            FetchExistingCompany(0);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string CompanyName = txtName.Text;
        string CompanyCode = txtCode.Text;
        int Status = 0;
        if (chkActive.Checked == true)
        {
            Status = 1;
        }

        if (btnSubmit.Text == "Submit")
        {
            CreateCompany(CompanyName, CompanyCode);
        }
        else
        {
            UpdateCompany(Convert.ToInt32(hdnCurrent.Value), CompanyName, CompanyCode, Status);
        }
    }
    protected void lnkExisting_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        Reset();
    }
    protected void lnkNew_Click(object sender, EventArgs e)
    {
        chkActive.Enabled = false;
        chkActive.Checked = true;
        btnSubmit.Text = "Submit";
        Shownew();
    }
    protected void lnkCompany_Click(object sender, EventArgs e)
    {
        Shownew();
        try
        {
            LinkButton lnk = (LinkButton)sender;
            string comValue = lnk.CommandArgument;
            pnlExist.Visible = false;
            hdnCurrent.Value = comValue;
            txtName.Text = lnk.Text;
            btnSubmit.Text = "Update";
            chkActive.Enabled = true;
            chkActive.Checked = true;
            retVal = Company.FetchAllCompany(Convert.ToInt32(comValue));
            DataSet ds = (DataSet)retVal.ReturnObject;
            DataTable dt = ds.Tables[0];

            DataRow dr = dt.Rows[0];
            txtCode.Text = dr["CompanyCode"].ToString();

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

    public void CreateCompany(string CompanyNAme, string CompanyCode)
    {
        try
        {
            retVal = Company.InsertCompany(CompanyNAme, CompanyCode);
            int response = Convert.ToInt32(retVal.ReturnObject);
            if (response == 2)
            {
                lblInfo.CssClass = "ErrorMessage";
                lblInfo.Text = "Either the CompanyNAme or the CompanyCode already exist.";
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
        txtName.Text = "";
        txtCode.Text = "";
        pnlExist.Visible = false;
        pnlExisting.Visible = false;
        lblMessage.Text = "";

    }
    public void Reset()
    {
        FetchExistingCompany(0);
        txtCode.Text = "";
        txtName.Text = "";
        lblInfo.Text = "";
        pnlNew.Visible = false;

    }
    public void FetchExistingCompany(int ID)
    {

        try
        {
            retVal = Company.FetchAllCompany(ID);
            DataSet ds = (DataSet)retVal.ReturnObject;
            if (ds != null)
            {

                DataTable dt = ds.Tables[0];
                grvCompany.DataSource = dt;
                grvCompany.DataBind();
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

    public void UpdateCompany(int ID, string CompanyNAme, string CompanyCode, int status)
    {
        try
        {
            retVal = Company.UpdateCompany(ID, CompanyNAme, CompanyCode, status);
            int response = Convert.ToInt32(retVal.ReturnObject);
            if (response == 2)
            {
                lblInfo.CssClass = "ErrorMessage";
                lblInfo.Text = "Either the CompanyName or the CompanyCode already exist. for another Company";
            }
            else if (response == 1)
            {

                lblMessage.CssClass = "ConfirmationMessage";
                lblMessage.Text = "Conmpany information successfully Updated";

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
    protected void grvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pgindex = e.NewPageIndex;
        grvCompany.PageIndex = pgindex;
        FetchExistingCompany(0);
    }

}