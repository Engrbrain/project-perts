<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AddNewMaterial.ascx.vb" Inherits="MDS_ELM.AddNewMaterial" %>
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
<table class="auto-style1">
    <tr>
        <td>Material Mapping Manager</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMsg" runat="server" ForeColor="#0099CC" style="font-weight: 700" Font-Size="Large"></asp:Label>
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
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearch" WatermarkText="Search By Material Description,ELM Code, SAP Code"></cc1:TextBoxWatermarkExtender>
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
                                <asp:TemplateColumn HeaderText="ELM Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLEG_CODE" runat="server" CssClass="" Text='<%#DataBinder.Eval(Container, "DataItem.LEG_CODE")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ELM Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLEG_DESC" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "LEG_DESC")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                

                                <asp:TemplateColumn HeaderText="SAP Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMATERIAL_CODE" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "MATERIAL_CODE")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>

                                 <asp:TemplateColumn HeaderText="SAP Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMATERIAL_DESC" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "MATERIAL_DESC")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>

                                  <asp:TemplateColumn HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUNIT_OF_MEASURE" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "UNIT_OF_MEASURE")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
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
            <table id="tblAddNewAccount" runat="server" class="cardDetails" style="width: 100%">
                <tr>
                    <td class="tdtop2" style="width: 100%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table style="width: 100%">
                            <tr>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <table class="auto-style1">
                                                <tr>
                                                    <td width="50%">ELM Material Code:</td>
                                                    <td width="50%">ELM Description</td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <asp:DropDownList ID="ddlELMMaterialCode" runat="server" AutoPostBack="True" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="50%">
                                                        <asp:TextBox ID="txtELMDescription" runat="server" Width="98%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">&nbsp;</td>
                                <td style="width: 50%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table class="auto-style1">
                                                <tr>
                                                    <td width="50%">SAP Material Code:</td>
                                                    <td width="50%">SAP Description:</td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <asp:DropDownList ID="ddlSAPMaterialCode" runat="server" AutoPostBack="True" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="50%">
                                                        <asp:TextBox ID="txtSAPDescription" runat="server" ReadOnly="True" Width="98%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">&nbsp;</td>
                                <td style="width: 50%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 50%">Division</td>
                                <td style="width: 50%">Unit of Measurement:</td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <asp:DropDownList ID="ddlDivision" runat="server" Width="100%">
                                        <asp:ListItem Value="1">--- Please Select ---</asp:ListItem>
                                        <asp:ListItem Value="70">Warehousing Services</asp:ListItem>
                                        <asp:ListItem Value="71">Haulage Services</asp:ListItem>
                                        <asp:ListItem Value="72">Distrib. Serv Teleco</asp:ListItem>
                                        <asp:ListItem Value="73">Distrib. Serv FMCG</asp:ListItem>
                                        <asp:ListItem Value="74">Reverse Logistics</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 50%">
                                    <asp:DropDownList ID="ddlActive" runat="server" Width="100%">
                                        <asp:ListItem Value="1">--- Please Select ---</asp:ListItem>
                                        <asp:ListItem Value="AU">Service Material</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    &nbsp;</td>
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
                                                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Add Material" />
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
                                Are you sure you want to delete this Material?</td>
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
<link href="assets/mycss/form.css" rel="stylesheet" />
<link href="assets/mycss/style.css" rel="stylesheet" />


