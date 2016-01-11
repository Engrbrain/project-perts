<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="RegisterUser.ascx.vb" Inherits="UnicoTesting.RegisterUser" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<link href="assets/mycss/form.css" rel="stylesheet" />
<link href="assets/mycss/style.css" rel="stylesheet" />
<table id="tblAddNewAccount" runat="server" class="cardDetails" style="width: 100%" align ="Center">
    <tr>
        <td class="tdtop2" colspan="3" style="width: 100%">Register New User</td>
    </tr>
    <tr>
        <td colspan="3" style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td style="width: 50%">Surname:</td>
                    <td style="width: 50%">First Name</td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtSurname" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtFirstName" runat="server" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%"></td>
                    <td style="width: 50%"></td>
                </tr>
                <tr>
                    <td style="width: 50%">Other Names:</td>
                    <td style="width: 50%">Email:</td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtOtherNames" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtEmail" runat="server" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">&nbsp;</td>
                    <td style="width: 50%"></td>
                </tr>
                <tr>
                    <td style="width: 50%">Phone Number:</td>
                    <td style="width: 50%">Username:</td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtPhoneNumber" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtUserName" runat="server" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">&nbsp;</td>
                    <td style="width: 50%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 50%">Password:</td>
                    <td style="width: 50%">Confirm Password</td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtPassword" runat="server" Width="98%" TextMode="Password"></asp:TextBox>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtConfirmPassword" runat="server" Width="98%" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">
           <asp:Label ID="lblMsg" runat="server" ForeColor="#FF3300" style="font-weight: 700"></asp:Label>
           <asp:Label ID="lblUserName" runat="server" ForeColor="#FF3300" style="font-weight: 700"></asp:Label>
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
                                    <asp:Button ID="btnRegister" runat="server" CssClass="button" Text="Register" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

