<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AddGL_Account.ascx.vb" Inherits="UnicoTesting.AddGL_Account" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<link href="assets/mycss/form.css" rel="stylesheet" />
<link href="assets/mycss/style.css" rel="stylesheet" />
<table id="tblAddNewAccount" runat="server" class="cardDetails" style="width: 100%" align ="Center">
    <tr>
        <td class="tdtop2" colspan="3" style="width: 100%">Add New GL Account</td>
    </tr>
    <tr>
        <td colspan="3" style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td style="width: 50%">Legacy Account Number:</td>
                    <td style="width: 50%">Legacy Account Description</td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtLegacyAccountNumber" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtLegacyAccountDescription" runat="server" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%"></td>
                    <td style="width: 50%"></td>
                </tr>
                <tr>
                    <td style="width: 50%">SAP Account Number:</td>
                    <td style="width: 50%">SAP Account Description:</td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtSAPAccountNumber" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtSAPAccountDescription" runat="server" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">
           <asp:Label ID="lblMsg" runat="server" ForeColor="#FF3300" style="font-weight: 700"></asp:Label>
           <asp:Label ID="lblUserName" runat="server" ForeColor="#FF3300" style="font-weight: 700" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 50%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 50%">&nbsp;</td>
                    <td style="width: 50%">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Button ID="Button4" runat="server" CssClass="button" Text="Back" />
                                </td>
                                <td align="right" style="width: 50%">
                                    <asp:Button ID="btnRegister" runat="server" CssClass="button" Text="Add" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

