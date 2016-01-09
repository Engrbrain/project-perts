<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Extract_From_Legacy.ascx.vb" Inherits="UnicoTesting.Extract_From_Legacy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
<link href="assets/mycss/form.css" rel="stylesheet" />
<link href="assets/mycss/style.css" rel="stylesheet" />
<asp:Label ID="lblMsg" runat="server" ForeColor="#FF3300" style="font-weight: 700"></asp:Label>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table align="center" class="auto-style1" id ="tblSectionA" runat="server">
    <tr>
        <td width ="30%">&nbsp;</td>
        <td width ="40%">
            &nbsp;</td>
        <td width ="30%">&nbsp;</td>
    </tr>
    <tr>
        <td width ="30%">Year:</td>
        <td width ="40%">
            <asp:DropDownList ID="ddlUnicoYear" runat="server" Width ="100%">
            </asp:DropDownList>
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
        <td width ="30%" class="auto-style2">Period:</td>
        <td width ="40%" class="auto-style2">
            <asp:DropDownList ID="ddlUnico_Period" runat="server" Width ="100%">
            </asp:DropDownList>
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
        <td width ="40%">
            <asp:LinkButton ID="lnkChangeDate" runat="server" OnClientClick="return confirm('WARNING!!\r\nAre you sure you want to change the Extraction Period?')">Change Period</asp:LinkButton>
        </td>
        <td width ="30%">&nbsp;</td>
    </tr>
    <tr>
      <td width ="30%">&nbsp;</td>
        <td width ="40%">
            <asp:Button ID="btnExtract" runat="server" Text="Extract" cssClass ="button"/>
        </td>
        <td width ="30%">&nbsp;</td>
    </tr>
    <tr>
       <td colspan="3" style="width: 60%">
           
           <asp:Label ID="lblUserName" runat="server" ForeColor="#FF3300" style="font-weight: 700" Visible="False"></asp:Label>
           
           <asp:Label ID="lblPeriod" runat="server" ForeColor="#FF3300" style="font-weight: 700" Visible="False"></asp:Label>
        </td>
    </tr>
</table>

<table align="center" width ="100%" runat="server" id ="tblViewGrid">
    <tr>
        <td colspan="2" width ="100%">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM [GL_TRS] WHERE (([PERIOD] = ?) AND (LEN([ACCT_NO]) = 6) )">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblPeriod" DefaultValue="201402" Name="PERIOD" PropertyName="Text" Type="String" />
                

                
            </SelectParameters>
</asp:SqlDataSource>




                </td>
    </tr>
    <tr>
        <td colspan="2" width ="100%">
            <asp:Button ID="btnRemoveFromList" runat="server" cssClass ="button" Text="Remove Selected" OnClientClick="return confirm('WARNING!!\r\nAre you sure you want to delete the selected item from your extract. This will delete the entire document with this Reference Number?')" Width="110px"/>



                </td>
    </tr>
    <tr>
        <td colspan="2" width ="100%">


            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" width ="100%">


            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" width ="100%">


            <asp:DataGrid ID="dgViewGLTable" runat="server" AllowPaging="False" AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="False" CssClass="gridBorder" DataKeyField="ID" GridLines="Horizontal" ShowFooter="False" Width="100%">
                <PagerStyle Visible="False" />
                <AlternatingItemStyle CssClass="gridAlt" />
                <ItemStyle CssClass="gridItem" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateColumn HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                        <asp:Label ID="lblID" runat="server" CssClass="" Text='<%#DataBinder.Eval(Container,"DataItem.ID")%>'
                                            Visible="false">
                                                    </asp:Label>
                                    </ItemTemplate>
                         
                                    <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateColumn>

                    
                       

                    <asp:TemplateColumn HeaderText="ACCT_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblACCT_NO" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "ACCT_NO")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="TRS_REF">
                        <ItemTemplate>
                            <asp:Label ID="lblTRS_REF" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "TRS_REF")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                 
                    <asp:TemplateColumn HeaderText="TRS_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblTRS_DATE" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "TRS_DATE")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                      <asp:TemplateColumn HeaderText="PERIOD">
                        <ItemTemplate>
                            <asp:Label ID="lblPERIOD" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "PERIOD")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                   
                     <asp:TemplateColumn HeaderText="BATCH_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblBATCH_NO" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "BATCH_NO")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>


                     <asp:TemplateColumn HeaderText="TRS_AMT">
                        <ItemTemplate>
                            <asp:Label ID="lblTRS_AMT" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "TRS_AMT")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                       <asp:TemplateColumn HeaderText="ENTRY_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblENTRY_DATE" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "ENTRY_DATE")%>'>
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
        <td colspan="2" width ="100%">


            <table class="auto-style3" style="display:none;">
                <tr>
                    <td>


        <asp:GridView ID="GridView1" runat="server" Width ="100%" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>

                <asp:BoundField DataField="ACCT_NO" HeaderText="ACCT_NO" SortExpression="ACCT_NO" />
                <asp:BoundField DataField="DEPT_ID" HeaderText="DEPT_ID" SortExpression="DEPT_ID" />
                <asp:BoundField DataField="TRS_DESC" HeaderText="TRS_DESC" SortExpression="TRS_DESC" />
                <asp:BoundField DataField="TRS_REF" HeaderText="TRS_REF" SortExpression="TRS_REF" />
                <asp:BoundField DataField="TRS_DATE" HeaderText="TRS_DATE" SortExpression="TRS_DATE" />
                <asp:BoundField DataField="PERIOD" HeaderText="PERIOD" SortExpression="PERIOD" />
                <asp:BoundField DataField="PREV_PERIOD" HeaderText="PREV_PERIOD" SortExpression="PREV_PERIOD" />
                <asp:BoundField DataField="BATCH_NO" HeaderText="BATCH_NO" SortExpression="BATCH_NO" />
                <asp:BoundField DataField="TRS_SYSTEM" HeaderText="TRS_SYSTEM" SortExpression="TRS_SYSTEM"  />
                <asp:BoundField DataField="TRS_PRT" HeaderText="TRS_PRT" SortExpression="TRS_PRT" />
                <asp:BoundField DataField="TRS_ID" HeaderText="TRS_ID" SortExpression="TRS_ID" />
                <asp:BoundField DataField="LOCKED" HeaderText="LOCKED" SortExpression="LOCKED" />
                <asp:BoundField DataField="TRS_AMT" HeaderText="TRS_AMT" SortExpression="TRS_AMT" />
                <asp:BoundField DataField="SEQ" HeaderText="SEQ" SortExpression="SEQ"/>
                <asp:BoundField DataField="UNIT_CODE" HeaderText="UNIT_CODE" SortExpression="UNIT_CODE"/>
                <asp:BoundField DataField="EMPLOYEE_CODE" HeaderText="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"  />
                <asp:BoundField DataField="COMPANY_ID" HeaderText="COMPANY_ID" SortExpression="COMPANY_ID" />
                <asp:BoundField DataField="CURRENCY_CODE" HeaderText="CURRENCY_CODE" SortExpression="CURRENCY_CODE" />
                <asp:BoundField DataField="EXCHANGE_RATE" HeaderText="EXCHANGE_RATE" SortExpression="EXCHANGE_RATE" />
                <asp:BoundField DataField="PROCESSED" HeaderText="PROCESSED" SortExpression="PROCESSED"  />
                <asp:BoundField DataField="ENTRY_DATE" HeaderText="ENTRY_DATE" SortExpression="ENTRY_DATE" />
            </Columns>
        </asp:GridView>
                    </td>
                </tr>
            </table>
                </td>
    </tr>
    <tr>
        <td width ="50%">
            &nbsp;</td>
        <td width ="50%" align ="right">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <table class="auto-style3">
                <tr>
                    <td width ="30%" align ="left">
            <asp:Button ID="btnBack" runat="server" Text="Back" cssClass ="button"/>
                    </td>
                    <td width ="40%" align ="center">
            <asp:Button ID="btnViewSum" runat="server" Text="View Balances" cssClass ="button" />
                    </td>
                    <td width ="30%" align ="right">
            <asp:Button ID="btnLoad" runat="server" Text="Load" cssClass ="button" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>


<table align="center" width ="100%" runat="server" id ="tblViewBalances">
    <tr>
        <td colspan="2" width ="100%">
            <asp:DataGrid ID="dgBalances" runat="server" AllowPaging="False" AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="False" CssClass="gridBorder" DataKeyField="ID" GridLines="Horizontal" ShowFooter="False" Width="100%">
                <PagerStyle Visible="False" />
                <AlternatingItemStyle CssClass="gridAlt" />
                <ItemStyle CssClass="gridItem" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateColumn HeaderText="S/N" >
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" CssClass="" Text='<%#DataBinder.Eval(Container,"DataItem.ID")%>' Visible="false">
                                                    </asp:Label>
                                        <%#(dgBalances.PageSize * dgBalances.CurrentPageIndex) + Container.ItemIndex + 1%>
                                    </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    


                    <asp:TemplateColumn HeaderText="Transaction RefNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblTRS_REF" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "TRS_REF")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Total Sum">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalSUM" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "TotalSUM")%>'>
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
        <td colspan="2" width ="100%">


            &nbsp;</td>
    </tr>
    <tr>
        <td width ="50%">
            &nbsp;</td>
        <td width ="50%" align ="right">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <table class="auto-style3">
                <tr>
                    <td width ="30%" align ="left">
            <asp:Button ID="btnBakkk" runat="server" Text="Back" cssClass ="button" />
                    </td>
                    <td width ="40%" align ="center">
                        &nbsp;</td>
                    <td width ="30%" align ="right">
                        &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<table align="center" width ="100%" runat="server" id ="tblViewBalancesDetails">
    <tr>
        <td colspan="2" width ="100%">
            <asp:DataGrid ID="dgBalancesDetails" runat="server" AllowPaging="False" AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="False" CssClass="gridBorder"  GridLines="Horizontal" ShowFooter="False" Width="100%">
                <PagerStyle Visible="False" />
                <AlternatingItemStyle CssClass="gridAlt" />
                <ItemStyle CssClass="gridItem" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Line Item" >
                        <ItemTemplate>
                       
                                        <%#(dgBalancesDetails.PageSize * dgBalancesDetails.CurrentPageIndex) + Container.ItemIndex + 1%>
                                    </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    


                    <asp:TemplateColumn HeaderText="Sybase Account">
                        <ItemTemplate>
                            <asp:Label ID="lblACCT_NO" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "ACCT_NO")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="SAP Account">
                        <ItemTemplate>
                            <asp:Label ID="lblSAP_GL_ACCOUNT" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "SAP_GL_ACCOUNT")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                 
                    <asp:TemplateColumn HeaderText="Trans. Date">
                        <ItemTemplate>
                            <asp:Label ID="lblTRS_DATE" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "TRS_DATE")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                      <asp:TemplateColumn HeaderText="Trans RefNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblTRS_REF" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "TRS_REF")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                     <asp:TemplateColumn HeaderText="Batch No.">
                        <ItemTemplate>
                            <asp:Label ID="lblBATCH_NO" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "BATCH_NO")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>


                     <asp:TemplateColumn HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblTRS_AMT" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "TRS_AMT")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                       <asp:TemplateColumn HeaderText="Entry Date">
                        <ItemTemplate>
                            <asp:Label ID="lblENTRY_DATE" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "ENTRY_DATE")%>'>
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
        <td colspan="2" width ="100%">


            &nbsp;</td>
    </tr>
    <tr>
        <td width ="50%">
            &nbsp;</td>
        <td width ="50%" align ="right">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <table class="auto-style3">
                <tr>
                    <td width ="30%" align ="left">
            <asp:Button ID="Button1" runat="server" Text="Back" cssClass ="button"/>
                    </td>
                    <td width ="40%" align ="center">
                        &nbsp;</td>
                    <td width ="30%" align ="right">
                        &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>


        




<table align="center" width ="100%" runat="server" id ="tblViewSuccessFull">
    <tr>
        <td colspan="2" width ="100%">
            Successful</td>
    </tr>
    <tr>
        <td colspan="2" width ="100%">


            You Transactions have been posted successfully for load into SAP. Please note that data loaded will be loaded by the end of the business day -- Do not perform any other Extract as this will overwrite your present Extract.</td>
    </tr>
    <tr>
        <td width ="50%">
            &nbsp;</td>
        <td width ="50%" align ="right">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <table class="auto-style3">
                <tr>
                    <td width ="30%" align ="left">
            <asp:Button ID="Button2" runat="server" Text="Back" cssClass ="button"/>
                    </td>
                    <td width ="40%" align ="center">
                        &nbsp;</td>
                    <td width ="30%" align ="right">
                        &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>


        




