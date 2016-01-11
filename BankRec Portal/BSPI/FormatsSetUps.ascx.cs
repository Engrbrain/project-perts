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


public partial class DesktopModules_BSPI_FormatsSetUps : PortalModuleBase
{
    OperationResultInfo retVal = new OperationResultInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {

            // FetchBanks();

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        int BankID = Convert.ToInt32(hdnBank.Value);
        string ColumnName = txtColmnNAme.Text;
        string ColumnText = txtSampleText.Text;
        int Status = 0;
        string BankName = ddlBankEarly.SelectedItem.Text;
        if (chkActive.Checked == true)
        {
            Status = 1;
        }

        if (btnSubmit.Text == "Submit")
        {
            CreateAccount(BankID, ColumnName, ColumnText, BankName);

        }
        else
        {
            UpdateCompany(BankID, ColumnName, ColumnText, Status, BankName);
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
            retVal = Format.FetchAllFormat(Convert.ToInt32(comValue), Convert.ToInt32(hdnBank.Value));
            DataSet ds = (DataSet)retVal.ReturnObject;
            DataTable dt = ds.Tables[0];

            DataRow dr = dt.Rows[0];
            ddlBankEarly.SelectedValue = dr["BAnkID"].ToString();
            txtSampleText.Text = dr["SampleText"].ToString();
            txtColmnNAme.Text = dr["ColumnName"].ToString();

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

    public void CreateAccount(int BakNAme, string ColumnName, string ColumnText, string BankName)
    {
        int id = 0;
        try
        {
            retVal = Format.InsertFormat(ref id, BakNAme, ColumnName, ColumnText, BankName);
            int response = Convert.ToInt32(retVal.ReturnObject);
            if (response == 2)
            {
                lblInfo.CssClass = "ErrorMessage";
                lblInfo.Text = "The Column already exists under the same Bank";
            }
            else if (response == 1)
            {

                lblMessage.CssClass = "ConfirmationMessage";
                lblMessage.Text = "Column successfully created";

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
        txtSampleText.Text = "";
        txtColmnNAme.Text = "";

        pnlExist.Visible = false;
        lblMessage.Text = "";

    }
    public void Reset()
    {
        FetchExistingFormat(0, Convert.ToInt32(hdnBank.Value));
        txtSampleText.Text = "";
        txtColmnNAme.Text = "";
        //ddlBankName.SelectedValue = "0";
        lblInfo.Text = "";
        pnlNew.Visible = false;

    }
    public void FetchExistingFormat(int ID, int BankID)
    {

        try
        {
            retVal = Format.FetchAllFormat(ID, BankID);
            DataSet ds = (DataSet)retVal.ReturnObject;
            if (ds != null)
            {

                DataTable dt = ds.Tables[0];
                Session["Table"] = dt;
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

    public void UpdateCompany(int BakNAme, string ColumnName, string ColumnText, int Statuss, string BankName)
    {
        try
        {
            retVal = Format.UpdateFormat(Convert.ToInt32(hdnCurrent.Value), BakNAme, ColumnName, ColumnText, Statuss, BankName);
            int response = Convert.ToInt32(retVal.ReturnObject);
            if (response == 2)
            {
                lblInfo.CssClass = "ErrorMessage";
                lblInfo.Text = "This Column-Name already exist under this Bank";
            }
            else if (response == 1)
            {

                lblMessage.CssClass = "ConfirmationMessage";
                lblMessage.Text = "Column  successfully Updated";

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

    protected void SortItem_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        string commandName = button.CommandName;
        string[] array = commandName.Split('_');
        int id = Int32.Parse(array[1]);
        int directon = Int32.Parse(array[2]);
        int currentSequence = 0;
        int prevId = 0;
        int prevSequence = 0;
        int nextId = 0;
        int nextSequence = 0;

        //get the id and sequence of the item above and below the selected item
        DataTable data = (DataTable)Session["Table"];
        for (int i = 0; i < data.Rows.Count; i++)
        {
            DataRow dr = data.Rows[i];
            if (Common.GetIntegerValue(dr["id"]) == id)
            {
                currentSequence = Common.GetIntegerValue(dr["Index"]);
                if (directon == 1)
                {
                    prevId = Common.GetIntegerValue(data.Rows[i - 1]["id"]);
                    prevSequence = Common.GetIntegerValue(data.Rows[i - 1]["Index"]);
                }
                else if (directon == 0)
                {
                    nextId = Common.GetIntegerValue(data.Rows[i + 1]["id"]);
                    nextSequence = Common.GetIntegerValue(data.Rows[i + 1]["Index"]);
                }
                break;
            }

        }
        if (directon == 1)
        {
            //move up
            Format.UpdateFormatIndex(id, prevSequence);
            Format.UpdateFormatIndex(prevId, currentSequence);
        }
        else
        {
            //move down
            Format.UpdateFormatIndex(id, nextSequence);
            Format.UpdateFormatIndex(nextId, currentSequence);
        }
        FetchExistingFormat(0, Convert.ToInt32(hdnBank.Value));


    }

    DataTable _FormatColumnTable = null;
    DataTable FormatColumnTable
    {

        get
        {
            if (_FormatColumnTable == null)
            {
                retVal = Format.FetchAllFormat(0, Convert.ToInt32(hdnBank.Value));
                _FormatColumnTable = ((DataSet)retVal.ReturnObject).Tables[0];
            }
            return _FormatColumnTable;
        }
        set
        {
            _FormatColumnTable = value;
        }
    }
    public bool GetVisibility(object id, int direction)
    {
        //check if the item is the first or last in the datatable
        bool retVal = true;
        DataTable data = FormatColumnTable;
        if (Common.GetIntegerValue(data.Rows[0]["id"]) == Convert.ToInt32(id) && direction == 1)
        {
            retVal = false;
        }
        if (Common.GetIntegerValue(data.Rows[data.Rows.Count - 1]["id"]) == Convert.ToInt32(id) && direction == 0)
        {
            retVal = false;
        }
        return retVal;
    }


    public string EditCommandName(object obj)
    {
        int id = (int)obj;
        return string.Format("edit_{0}", id.ToString());
    }

    public string SortCommandName(object id, int direction)
    {
        return string.Format("sort_{0}_{1}", id.ToString(), direction.ToString());
    }

    protected void btnRemove_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        string commandName = button.CommandName;
        string[] array = commandName.Split('_');
        int id = Int32.Parse(array[1]);
        Session["indeces"] = id;
        int index = id;
        retVal = Format.DeleteFormatByID(index);
        if (Convert.ToInt32(retVal.ReturnObject) == 1)
        {
            FetchExistingFormat(0, Convert.ToInt32(hdnBank.Value));
            lblMessage.Text = "";


        }
        else
        {

            lblMessage.CssClass = "ErrorMessage";
            lblMessage.Text = "An error occured while removing this Book Field";
        }
    }
    protected void grvAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pgindex = e.NewPageIndex;
        grvAccount.PageIndex = pgindex;
        FetchExistingFormat(0, Convert.ToInt32(hdnBank.Value));
    }
    protected void ddlBankEarly_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        hdnBank.Value = ddlBankEarly.SelectedValue;
        pnlNew.Visible = false;
        pnlExisting.Visible = true;
        FetchExistingFormat(0, Convert.ToInt32(hdnBank.Value));
    }
}