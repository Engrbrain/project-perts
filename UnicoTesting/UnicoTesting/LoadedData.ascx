<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="LoadedData.ascx.vb" Inherits="UnicoTesting.LoadedData" %>
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
        <td width ="30%">Year:</td>
        <td width ="40%">
            <asp:DropDownList ID="ddlUnicoYear" runat="server" Width ="100%" >
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
        <td width ="40%">&nbsp;</td>
        <td width ="30%">&nbsp;</td>
    </tr>
    <tr>
      <td width ="30%">&nbsp;</td>
        <td width ="40%">
            <asp:Button ID="btnExtract" runat="server" Text="Extract" CssClass ="button" />
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
        <td colspan="2" width ="100%">
            <asp:DataGrid ID="dgBalances" runat="server" AllowPaging="False" AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="False" CssClass="gridBorder"  GridLines="Horizontal" ShowFooter="False" Width="100%">
                <PagerStyle Visible="False" />
                <AlternatingItemStyle CssClass="gridAlt" />
                <ItemStyle CssClass="gridItem" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateColumn HeaderText="S/N" >
                        <ItemTemplate>
                           
                                        <%#(dgBalances.PageSize * dgBalances.CurrentPageIndex) + Container.ItemIndex + 1%>
                                    </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    


                    <asp:TemplateColumn HeaderText="Document Number">
                        <ItemTemplate>
                            <asp:Label ID="lblBELNR" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "BELNR")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Fiscal Year">
                        <ItemTemplate>
                            <asp:Label ID="lblGJAHR" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "GJAHR")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Document Type">
                        <ItemTemplate>
                            <asp:Label ID="lblBLART" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "BLART")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Posting Date">
                        <ItemTemplate>
                            <asp:Label ID="lblBUDAT" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "BUDAT")%>'>
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
            <asp:Button ID="btnBakkk" runat="server" Text="Back" CssClass ="button" />
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
                    <asp:TemplateColumn HeaderText="S/N" >
                        <ItemTemplate>
                           
                                        <%#(dgBalancesDetails.PageSize * dgBalancesDetails.CurrentPageIndex) + Container.ItemIndex + 1%>
                                    </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    


                    <asp:TemplateColumn HeaderText="Document Number">
                        <ItemTemplate>
                            <asp:Label ID="lblBELNR" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "BELNR")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="GL Account">
                        <ItemTemplate>
                            <asp:Label ID="lblHKONT" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "HKONT")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Document Type">
                        <ItemTemplate>
                            <asp:Label ID="lblBUZEI" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "BUZEI")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Document Date">
                        <ItemTemplate>
                            <asp:Label ID="lblBLDAT" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "BLDAT")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Document Date">
                        <ItemTemplate>
                            <asp:Label ID="lblBLDAT" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "BLDAT")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                     <asp:TemplateColumn HeaderText="Period">
                        <ItemTemplate>
                            <asp:Label ID="lblMONAT" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "MONAT")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                       <asp:TemplateColumn HeaderText="Posting Key">
                        <ItemTemplate>
                            <asp:Label ID="lblBSCHL" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "BSCHL")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                      <asp:TemplateColumn HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblDMBTR" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "DMBTR")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Tax">
                        <ItemTemplate>
                            <asp:Label ID="lblMWSKZ" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "MWSKZ")%>'>
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
            <asp:Button ID="btnBakkk0" runat="server" Text="Back" CssClass ="button"/>
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


