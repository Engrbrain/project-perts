<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ChangePasswordModule.ascx.vb" Inherits="MDS_ELM.ChangePasswordModule" %>
<style type="text/css">
    .auto-style1
    {
        width: 100%;
    }
    .button
    {
        height: 26px;
    }
</style>

<table class="auto-style1">
    <tr>
        <td colspan="3">Change Password</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" ForeColor="#0099CC" style="font-weight: 700"></asp:Label>
            </td>
    </tr>
    <tr>
        <td width ="20%">Old Password:</td>
        <td width ="40%">
            <asp:TextBox ID="txtOldPassword" runat="server" Width="100%"></asp:TextBox>
        </td>
        <td width ="40%">&nbsp;</td>
    </tr>
    <tr>
        <td width ="20%">New Password:</td>
        <td width ="40%">
            <asp:TextBox ID="txtNewPassword" runat="server" Width="100%"></asp:TextBox>
        </td>
        <td width ="40%">&nbsp;</td>
    </tr>
    <tr>
        <td>Confirm New Password:</td>
        <td>
            <asp:TextBox ID="txtConfirmPassword" runat="server" Width="100%"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
                                                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Change Password" />
                                            </td>
        <td>&nbsp;</td>
    </tr>
</table>

