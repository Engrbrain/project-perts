<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="RegisterUsers.ascx.vb" Inherits="MDS_ELM.RegisterUsers" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<link href="assets/mycss/form.css" rel="stylesheet" />
<link href="assets/mycss/style.css" rel="stylesheet" />
<table class="auto-style1">
    <tr>
        <td>Create Users</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMsg" runat="server" ForeColor="#0099CC" style="font-weight: 700"></asp:Label>
            <asp:Label ID="lblUsername" runat="server" Visible="False"></asp:Label>

        </td>
    </tr>
    <tr>
        <td>
            <table id="tblDataGrid" runat="server" style="width: 100%">
                <tr>
                    <td style="width: 100%">
                        <table width="100%">
                            <tr>
                                <td style="width: 70%; height: 51px;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 70%">
                                                <asp:TextBox ID="txtSearch" runat="server" Width="100%"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearch" WatermarkText="Search By Customer Name, ELM ID, SAP ID, SAP Name"></cc1:TextBoxWatermarkExtender>
                                            </td>
                                            <td align="right" style="width: 30%">
                                                <asp:Button ID="btnSearching" runat="server" AccessKey="r" CssClass="button" Text="Search" />


                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" style="width: 15%" valign="top">
                                    <asp:ImageButton ID="ImgBAddNew" runat="server" ImageUrl="~/Resources/LadDurainImages/New.jpg" />
                                </td>
                                <td align="right" style="width: 15%" valign="top">
                                    <asp:ImageButton ID="ImgBDelete" runat="server" ImageUrl="~/Resources/LadDurainImages/Delete.jpg" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:DataGrid ID="dgSalesPerson" runat="server" AllowPaging="False" AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="False" CssClass="gridBorder" DataKeyField="ID" GridLines="Horizontal" ShowFooter="False" Width="100%">
                            <PagerStyle Visible="False" />
                            <AlternatingItemStyle CssClass="gridAlt" />
                            <ItemStyle CssClass="gridItem" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="S/N" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" CssClass="" Text='<%#DataBinder.Eval(Container,"DataItem.ID")%>' Visible="false">
                                                    </asp:Label>
                                        <%#(dgSalesPerson.PageSize * dgSalesPerson.CurrentPageIndex) + Container.ItemIndex + 1%>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="First Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFIRST_NAME" runat="server" CssClass="" Text='<%#DataBinder.Eval(Container, "DataItem.FIRST_NAME")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Last Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLAST_NAME" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "LAST_NAME")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                

                                <asp:TemplateColumn HeaderText="Username">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEMAIL" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "EMAIL")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                  <asp:TemplateColumn HeaderText="Reset">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgButton1" runat="server" CommandArgument="Reset" ImageUrl="~/Resources/LadDurainImages/SmallUnlock.jpg" ToolTip="Reset Password" OnClientClick="return confirm('WARNING!!\r\nAre you sure you want to Reset this Password?')" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:TemplateColumn>
                               
                                <asp:TemplateColumn HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgButton" runat="server" CommandArgument="Edit" ImageUrl="~/Resources/LadDurainImages/SmallEdit.jpg" ToolTip="Edit Record" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkViewDetails" runat="server" CommandArgument="View" CssClass="gridButton">View Details</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                        </asp:DataGrid>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%"></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table id="tblAddNewAccount" runat="server" class="cardDetails" style="width: 100%" align ="Center">
    <tr>
        <td class="tdtop2" style="width: 100%">Add New User</td>
    </tr>
    <tr>
        <td style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td style="width: 50%">First Name:</td>
                    <td style="width: 50%">Last Name:</td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtFirstName" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtLastName" runat="server" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%"></td>
                    <td style="width: 50%"></td>
                </tr>
                <tr>
                    <td style="width: 50%">Personnel Number:</td>
                    <td style="width: 50%">Email Address:</td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtPersonnelNumber" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtEmailAdress" runat="server" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        &nbsp;</td>
                    <td style="width: 50%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        Password:</td>
                    <td style="width: 50%">
                        Confirm Password:</td>
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
                        &nbsp;</td>
                    <td style="width: 50%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        User Role:</td>
                    <td style="width: 50%">
                        Other Information</td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:DropDownList ID="ddlUserRoles" runat="server" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtOtherInformation" runat="server" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        &nbsp;</td>
                    <td style="width: 50%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 50%">
           <asp:Label ID="lblMsg1" runat="server" ForeColor="#FF3300" style="font-weight: 700"></asp:Label>
           <asp:Label ID="lblUserName1" runat="server" ForeColor="#FF3300" style="font-weight: 700" Visible="False"></asp:Label>
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
                                                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Create User" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><table id="tblDeleteCaution" runat="server" 
                class="cardDetails" style="width: 100%">
            <tr>
                <td class="tdtop2" style="width: 100%">
                    &nbsp; Please Confirm</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                Are you sure you want to delete this Customers Record?</td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Button ID="Button2" runat="server" CssClass="button" Text="No! Back" /></td>
                                        <td align="right" style="width: 50%">
                                            <asp:Button ID="btnIAmSure" runat="server" CssClass="button" Text="Sure ! Delete" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>


