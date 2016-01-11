<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="IdocReport.ascx.vb" Inherits="UnicoTesting.IdocReport" %>
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
    .auto-style4
    {
        height: 18px;
    }
    </style>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<link href="assets/mycss/form.css" rel="stylesheet" />
<link href="assets/mycss/style.css" rel="stylesheet" />
<table align="center" class="auto-style1" id ="tblSectionA" runat="server">
    <tr>
        <td width ="30%" class="auto-style4">&nbsp;</td>
        <td width ="40%" class="auto-style4">
            </td>
        <td width ="30%" class="auto-style4"></td>
    </tr>
    <tr>
        <td width ="30%">Start Date:</td>
        <td width ="40%">
                        <asp:TextBox ID="txtStartDate" runat="server" Width="98%"></asp:TextBox>
           

              
        </td>
        <td width ="30%">
           

              
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" Format="yyyy/MM/dd"></cc1:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td width ="30%">&nbsp;</td>
        <td width ="40%">
            &nbsp;</td>
        <td width ="30%">
           

              
            &nbsp;</td>
    </tr>
    <tr>
        <td width ="30%" class="auto-style2">End Date:</td>
        <td width ="40%" class="auto-style2">
                        <asp:TextBox ID="txtEndDate" runat="server" Width="98%"></asp:TextBox>
           

              
        </td>
        <td width ="30%" class="auto-style2">
           

              
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" Format="yyyy/MM/dd"></cc1:CalendarExtender>
        </td>
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
            <asp:Button ID="btnExtract" runat="server" Text="View Report" cssClass ="button" />
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
                    <%--<asp:TemplateColumn HeaderText="S/N" >
                        <ItemTemplate>
                           
                                        <%#(dgBalances.PageSize * dgBalances.CurrentPageIndex) + Container.ItemIndex + 1%>
                                    </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    </asp:TemplateColumn>--%>


                    <asp:TemplateColumn HeaderText="Idoc Number">
                        <ItemTemplate>
                            <asp:Label ID="lblDOCNUM" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "DOCNUM")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                  
                    <asp:TemplateColumn HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lblLOGDAT" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "LOGDAT")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Long Text">
                        <ItemTemplate>
                            <asp:Label ID="lblSTATXT" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "STATXT")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="PAR 1">
                        <ItemTemplate>
                            <asp:Label ID="lblSTAPA1" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "STAPA1")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="PAR 2">
                        <ItemTemplate>
                            <asp:Label ID="lblSTAPA2" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "STAPA2")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="PAR 3">
                        <ItemTemplate>
                            <asp:Label ID="lblSTAPA3" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "STAPA3")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="PAR 4">
                        <ItemTemplate>
                            <asp:Label ID="lblSTAPA4" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "STAPA4")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="STATUS">
                        <ItemTemplate>
                           
                            <asp:Image ID="Image1" runat="server" ImageUrl=' <%#GetImageURL(DataBinder.Eval(Container.DataItem, "STATYP"))%>' />
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkViewDetails" runat="server" CommandArgument="View" CssClass="gridButton">Drill Down</asp:LinkButton>
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
            <asp:Button ID="btnBakkk" runat="server" Text="Back" cssClass ="button"/>
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
            <asp:DataGrid ID="dgViewAll" runat="server" AllowPaging="False" AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="False" CssClass="gridBorder"  GridLines="Horizontal" ShowFooter="False" Width="100%">
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


                    <asp:TemplateColumn HeaderText="Idoc Number">
                        <ItemTemplate>
                            <asp:Label ID="lblDOCNUM0" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "DOCNUM")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Date Loaded">
                        <ItemTemplate>
                            <asp:Label ID="lblLOGDAT0" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "LOGDAT")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Long Text">
                        <ItemTemplate>
                            <asp:Label ID="lblSTATXT0" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "STATXT")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="PAR 1">
                        <ItemTemplate>
                            <asp:Label ID="lblSTAPA5" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "STAPA1")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="PAR 2">
                        <ItemTemplate>
                            <asp:Label ID="lblSTAPA6" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "STAPA2")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="PAR 3">
                        <ItemTemplate>
                            <asp:Label ID="lblSTAPA7" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "STAPA3")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="PAR 4">
                        <ItemTemplate>
                            <asp:Label ID="lblSTAPA8" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "STAPA4")%>'>
                                                    </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cpTableHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="STATUS">
                        <ItemTemplate>
                            
                            <asp:Image ID="Image2" runat="server" ImageUrl=' <%#GetImageURL(DataBinder.Eval(Container.DataItem, "STATYP"))%>' />
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
            <asp:Button ID="btnBakkk0" runat="server" Text="Back" cssClass ="button"/>
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


