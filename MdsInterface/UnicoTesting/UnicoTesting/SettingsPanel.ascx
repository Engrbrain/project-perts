<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SettingsPanel.ascx.vb" Inherits="UnicoTesting.SettingsPanel" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<style type="text/css">

    .auto-style1 {
        width: 70%;
    }
    .auto-style2
    {
        height: 26px;
    }
    .auto-style3
    {
        width: 100%;
    }
    </style>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<link href="assets/mycss/form.css" rel="stylesheet" />
<link href="assets/mycss/style.css" rel="stylesheet" />
<table align="center" class="auto-style1" id ="tblSectionA" runat="server">
    <tr>
        <td width ="30%">&nbsp;</td>
        <td width ="40%">
            &nbsp;</td>
        <td width ="30%">&nbsp;</td>
    </tr>
    <tr>
        <td width ="30%">Present Client Used:</td>
        <td width ="40%">
            <asp:TextBox ID="txtPresentClient" runat="server" Width="100%" Enabled="False" ReadOnly="True"></asp:TextBox>
           


              
        </td>
        <td width ="30%">
           

              
            &nbsp;</td>
    </tr>
    <tr>
        <td width ="30%">&nbsp;</td>
        <td width ="40%">
            &nbsp;</td>
        <td width ="30%">
           

              
            &nbsp;</td>
    </tr>
    <tr>
        <td width ="30%" class="auto-style2">New Client:</td>
        <td width ="40%" class="auto-style2">
            <asp:TextBox ID="txtNewClient" runat="server" Width="100%"></asp:TextBox>
           


              
        </td>
        <td width ="30%" class="auto-style2">
           

              
            &nbsp;</td>
    </tr>
    <tr>
        <td width ="30%">&nbsp;</td>
        <td width ="40%">
            &nbsp;</td>
        <td width ="30%">
           

              
            &nbsp;</td>
    </tr>
    <tr>
       <td width ="30%">&nbsp;</td>
        <td width ="40%">&nbsp;</td>
        <td width ="30%">&nbsp;</td>
    </tr>
    <tr>
      <td width ="30%">&nbsp;</td>
        <td width ="40%">
            <asp:Button ID="btnExtract" runat="server" Text="Change Client" cssClass ="button"/>
        </td>
        <td width ="30%">&nbsp;</td>
    </tr>
    <tr>
       <td colspan="3" style="width: 60%">
           <asp:Label ID="lblMsg" runat="server" ForeColor="#FF3300" style="font-weight: 700"></asp:Label>
           <asp:Label ID="lblUserName" runat="server" ForeColor="#FF3300" style="font-weight: 700"></asp:Label>
        </td>
    </tr>
</table>


<table align="center" width ="100%" runat="server" id ="tblViewBalances">
    <tr>
        <td width ="100%">
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <table class="auto-style3">
                <tr>
                    <td width ="30%" align ="left">
                        &nbsp;</td>
                    <td width ="40%" align ="center">
                        &nbsp;</td>
                    <td width ="30%" align ="right">
                        &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>



