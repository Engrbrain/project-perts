<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UnBalancedData.ascx.vb" Inherits="UnicoTesting.UnBalancedData" %>
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
            <asp:DropDownList ID="ddlUnico_Period" runat="server" Width ="100%" >
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
            <asp:Button ID="btnExtract" runat="server" Text="Extract" cssClass ="button"/>
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


            <asp:DataGrid ID="dgViewGLTable" runat="server" AllowPaging="False" AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="False" CssClass="gridBorder" DataKeyField="ID" GridLines="Horizontal" ShowFooter="False" Width="100%">
                <PagerStyle Visible="False" />
                <AlternatingItemStyle CssClass="gridAlt" />
                <ItemStyle CssClass="gridItem" VerticalAlign="Middle" />
                <Columns>
                   

                    
                       

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





