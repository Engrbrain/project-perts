<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Extract_From_ELM.ascx.vb" Inherits="MDS_ELM.Extract_From_ELM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .auto-style1
    {
        width: 100%;
    }
    .auto-style2
    {
        height: 26px;
    }
    </style>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<p>
            <asp:Label ID="lblMsg" runat="server" ForeColor="#FF3300" style="font-weight: 700"></asp:Label>
                    </p>

<table class="auto-style1">
    <tr>
        <td>
            <table id="tblSectionA" runat="server" align="center" class="auto-style1">
                <tr>
                    <td width="30%">&nbsp;</td>
                    <td width="40%">&nbsp;</td>
                    <td width="30%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="30%">Client</td>
                    <td width="40%">
                        <asp:DropDownList ID="ddlClient" runat="server" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td width="30%"> &nbsp;</td>
                </tr>
                <tr>
                    <td width="30%">&nbsp;</td>
                    <td width="40%">
                        &nbsp;</td>
                    <td width="30%"> &nbsp;</td>
                </tr>
                <tr>
                    <td width="30%">Start Date:</td>
                    <td width="40%">
                        <asp:TextBox ID="txtStartDate" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td width="30%"> <cc1:CalendarExtender ID="CalendarExtender1" Format="yyyy-MM-dd" runat="server" TargetControlID="txtStartDate"></cc1:CalendarExtender>  </td>
                </tr>
                <tr>
                    <td width="30%">&nbsp;</td>
                    <td width="40%">&nbsp;</td>
                    <td width="30%">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2" width="30%">End Date:</td>
                    <td class="auto-style2" width="40%">
                        <asp:TextBox ID="txtEndDate" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style2" width="30%">
                        <cc1:CalendarExtender ID="CalendarExtender2" Format="yyyy-MM-dd" runat="server" TargetControlID="txtEndDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td width="30%">&nbsp;</td>
                    <td width="40%">
                        &nbsp;</td>
                    <td width="30%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="30%">&nbsp;</td>
                    <td width="40%">
                        &nbsp;</td>
                    <td width="30%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="30%">&nbsp;</td>
                    <td width="40%">
                        <asp:Button ID="btnExtract" runat="server" cssClass="button" Text="Extract" />
                    </td>
                    <td width="30%">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="width: 60%">
                        <asp:Label ID="lblUserName" runat="server" ForeColor="#FF3300" style="font-weight: 700" Visible="False"></asp:Label>
                        <asp:Label ID="lblPeriod" runat="server" ForeColor="#FF3300" style="font-weight: 700" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table class="auto-style1" runat ="server"  id ="tblViewExtract">
                <tr>
                    <td>
                        <asp:Button ID="btnExtract0" runat="server" cssClass="button" Text="Remove Selected" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid ID="dgViewBillNotes" runat="server" AllowPaging="False" AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="False" CssClass="gridBorder" DataKeyField="ID" GridLines="Horizontal" ShowFooter="False" Width="100%">
                            <PagerStyle Visible="False" />
                            <AlternatingItemStyle CssClass="gridAlt" />
                            <ItemStyle CssClass="gridItem" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                        <asp:Label ID="lblID" runat="server" CssClass="" Text='<%#DataBinder.Eval(Container,"DataItem.ID")%>' Visible="false">
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Document No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocumentNo" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "DocumentNo")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CLientID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClientID" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "ClientID")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Transaction Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransactionDate" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "TransactionDate")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Prepared By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPreparedBy" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "PreparedBy")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Charges Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChargesTotal" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "ChargesTotal")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Discount Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscountTotal" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "DiscountTotal")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Payable Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPayableAmount" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "PayableAmount")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Discounted Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscountedAmount" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "DiscountedAmount")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Computed Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComputedAmount" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "ComputedAmount")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>

                                 <asp:TemplateColumn HeaderText="">


                        <ItemTemplate>
                            <asp:LinkButton ID="lnkViewDetails" runat="server" CommandArgument="View" CssClass="gridButton">View Line Items</asp:LinkButton>
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
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <table class="auto-style1">
                            <tr>
                                <td align ="left" Width ="50%" >
                        <asp:Button ID="btnExtract2" runat="server" cssClass="button" Text="Back"  />
                                </td>
                                <td align ="right" Width ="50%"  >
                        <asp:Button ID="btnLoad" runat="server" cssClass="button"  Text="Load" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table class="auto-style1" runat="server" id ="tblViewLineItems">
                <tr>
                    <td>
                        <asp:DataGrid ID="dgViewLineItems" runat="server" AllowPaging="False" AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="False" CssClass="gridBorder"  GridLines="Horizontal" ShowFooter="False" Width="100%">
                            <PagerStyle Visible="False" />
                            <AlternatingItemStyle CssClass="gridAlt" />
                            <ItemStyle CssClass="gridItem" VerticalAlign="Middle" />
                            <Columns>
                              
                                <asp:TemplateColumn HeaderText="Deport ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepotID" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "DepotID")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                              
                               

                                <asp:TemplateColumn HeaderText="Rated Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRatedAmount" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "RatedAmount")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>

                                  <asp:TemplateColumn HeaderText="Minimum Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMinimumAmount" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "MinimumAmount")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Amount Used">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmountUsed" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "AmountUsed")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                              
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                        </asp:DataGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnExtract1" runat="server" cssClass="button" Text="Back" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table class="auto-style1" runat ="server" id ="tblSuccess">
                <tr>
                    <td>Successful</td>
                </tr>
                <tr>
                    <td>Data has been loaded successfully from ELM to SAP to create the corresponding sales Order. Please note the final load into SAP takes place during off peak period of 8pm usin SAP data services.....</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnExtract3" runat="server" cssClass="button" Text="Finish" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
                        <table class="auto-style1" runat ="server" id ="tblViewUnMapped">
                            <tr>
                                <td><b>UnMapped Customers</b></td>
                            </tr>
                            <tr>
                                <td>The Following Clients are not Mapped to SAP Customers on this Application. Is Either they are Set as InActive or Have not been Mapped, All Transactions Bearing the Below Listed will not be Loaded into SAP.</td>
                            </tr>
                            <tr>
                                <td>
                        <asp:DataGrid ID="dgViewUnMappedCustomers" runat="server" AllowPaging="False" AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="False" CssClass="gridBorder"  GridLines="Horizontal" ShowFooter="False" Width="100%">
                            <PagerStyle Visible="False" />
                            <AlternatingItemStyle CssClass="gridAlt" />
                            <ItemStyle CssClass="gridItem" VerticalAlign="Middle" />
                            <Columns>
                                
                                <asp:TemplateColumn HeaderText="Client Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClientCode" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "ClientCode")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Client Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClientName" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "ClientName")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                
                                
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                        </asp:DataGrid>
                                    <asp:Label ID="lblUnMappedCustomers" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><b>UnMapped Depots</b></td>
                            </tr>
                            <tr>
                                <td>The Following Depots are not Mapped to SAP Storage Locations on this Application. Is Either they are Set as InActive or Have not been Mapped, All Transactions Bearing the Below Listed will not be Loaded into SAP. </td>
                            </tr>
                            <tr>
                                <td>
                        <asp:DataGrid ID="dgViewUnMappedDeports" runat="server" AllowPaging="False" AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="False" CssClass="gridBorder" GridLines="Horizontal" ShowFooter="False" Width="100%">
                            <PagerStyle Visible="False" />
                            <AlternatingItemStyle CssClass="gridAlt" />
                            <ItemStyle CssClass="gridItem" VerticalAlign="Middle" />
                            <Columns>
                                
                                <asp:TemplateColumn HeaderText="Depot Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepotCode" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "DepotCode")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Depot Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepotName" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "DepotName")%>'>
                                                    </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                
                                
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                        </asp:DataGrid>
                                    <asp:Label ID="lblUnmappedDepots" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="auto-style1">
                                        <tr>
                                            <td>
                        <asp:Button ID="btnExtract4" runat="server" cssClass="button" Text="Ignore and Continue" />
                                            </td>
                                            <td align ="right">
                        <asp:Button ID="btnExtract5" runat="server" cssClass="button" Text="Re-Extract" />
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
</table>

