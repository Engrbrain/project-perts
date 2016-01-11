<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StatementUploadss.ascx.cs" Inherits="DesktopModules_BSPI_StatementUploadss" %>
<style type="text/css">
    .style1
    {
        width: 580px;
    }
</style>
<div>
    <asp:Panel ID="pnllnik" runat="server" Width="100%">
    
       <table border="0" cellpadding="3" cellspacing="0"  style=" width:100%">
                
                 <tr>
            <th align="center">
                Bank Statement Upload<br />
                <asp:HiddenField ID="hdnCompanyCode" runat="server" />
                <asp:HiddenField ID="hdnCurrency" runat="server" />
                <asp:HiddenField ID="hdnAccountId" runat="server" />
                <asp:HiddenField ID="hdnHouseBank" runat="server" />
                <asp:HiddenField ID="hdnBank" runat="server" />
                <asp:HiddenField ID="hdnBU" runat="server" />
                <asp:HiddenField ID="hdnStatement" runat="server" />
            </th>
        </tr>
        <tr>
            <td align="center">
               <asp:Label ID="lblInfo" runat="server" EnableViewState="False"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ValidationGroup="grp1" />
            </td>
        </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlmsg" runat="server" Width="100%">
       <table width="70%" border="0" cellpadding="4" cellspacing="0"  align="center">
                <tr>
                    <td style="height: 25px" align="center"  >
                        <asp:Label ID="lblErrorLabel" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                </tr>
            </table>
        </asp:Panel>
         
       
         <asp:Panel ID="pnlnewprogrm" runat="server" Width="100%" Visible="true" 
        BackColor="#CCCCCC" >
            <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
                <tr>
                    <td align="right" class="style1" >
                        Account Type:</td>
                    <td align="left" style="height: 24px">
                       
                        <asp:DropDownList ID="ddlMemType" runat="server" AppendDataBoundItems="True" CssClass="fltinput"
                            Width="207px" align="left" 
                            onselectedindexchanged="ddlMemType_SelectedIndexChanged" 
                            AutoPostBack="True" ValidationGroup="grp1" >
                            <asp:ListItem Value="0">[Select Bank Statement Type]</asp:ListItem>
                          
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="ddlMemType" Display="Dynamic" ErrorMessage="Account Type is Required" 
                            ForeColor="Red" ValidationGroup="grp1" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                </tr>
                <tr>
                    <td style="text-align: right; " align="right" class="style1">
                        <asp:HiddenField ID="hdnStatementDate" runat="server" />
                        Statement Date:</td>
                    <td style="text-align: left; height: 24px;">
                    <asp:TextBox ID="txtDate" runat="server" ReadOnly="True" Width="207px" 
                            ValidationGroup="grp1"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtDate" 
                    Format="yyyy mm dd" Separator="/" To-Date="" To-Today="True"/>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                            ControlToValidate="txtDate" Display="Dynamic" ErrorMessage="Statement Date is required" 
                            ForeColor="Red" ValidationGroup="grp1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="text-align: right; " class="style1">
                        Statement No:</td>
                    <td style="text-align: left; height: 24px;">
                        <asp:TextBox ID="txtStatement" runat="server" Width="216px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                            ControlToValidate="txtStatement" Display="Dynamic" 
                            ErrorMessage="StatementNo is required" ForeColor="Red" ValidationGroup="grp1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="text-align: right; " class="style1">
                        Upload&nbsp; File:</td>
                    <td style="text-align: left; height: 24px;">
                        <asp:FileUpload ID="txtFileName" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtFileName" Display="Dynamic" ErrorMessage="Select File" 
                            ForeColor="Red" ValidationGroup="grp1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="text-align: right; " class="style1">
                        </td>
                    <td style="text-align: left; height: 26px;">
                        <asp:Button ID="btnUpload" runat="server"  
                            Text="Upload" ValidationGroup="grp1"  Width="104px"  CssClass="fltButton" onclick="btnUpload_Click" 
                            />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; " class="style1">
                        </td>
                    <td align="left" style="height: 21px">
                        &nbsp; &nbsp; 
                        <asp:HiddenField ID="hdnFileName" runat="server" />
                    </td>
                </tr>

              
            </table>
            
            </asp:Panel>
             <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
         <asp:AsyncPostBackTrigger ControlID="ddlMemType" />--%>
             <asp:Panel ID="pnlSample" runat="server" Width="100%" Visible="false" 
        BackColor="#CCCCCC">
            <table width="70%" border="0" cellpadding="4" cellspacing="0"  align="center">
            <tr>
            <td align="center">

                <asp:Label ID="lblInformation" runat="server" ForeColor="Red" 
                  ></asp:Label>
  
            </td>
            </tr>
                <tr>
                    <td align="center">
                     
                         
                        
<asp:DataGrid ID="grdSamples"  runat="server" AllowPaging="True" AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="True"  GridLines="Horizontal" ShowFooter="False" Width="100%">
                            <PagerStyle Visible="False" />
                            <AlternatingItemStyle CssClass="gridAlt" />
                            <ItemStyle CssClass="gridItem" VerticalAlign="Middle" />
                            
                            <HeaderStyle CssClass="gridHeader" />
                        </asp:DataGrid>
                       
                                    
                        </td>
                </tr>                
               
            </table>
            </asp:Panel>
               <%-- </Triggers>

</asp:UpdatePanel>--%>
            <asp:Panel ID="pnlexistingprog" runat="server" Width="100%" 
        Visible="false" BackColor="#CCCCCC">
             <table width="70%" border="0" cellpadding="4" cellspacing="0"  align="center">
                <tr>
                <td align="center">
                <asp:DataGrid ID="grdprogrammes"  runat="server" AllowPaging="True" 
                        AlternatingItemStyle-CssClass="altRow" AutoGenerateColumns="True"  
                        GridLines="Horizontal" ShowFooter="False" Width="100%" PageSize="8000">
                            <PagerStyle Visible="False" />
                            <AlternatingItemStyle CssClass="gridAlt" />
                            <ItemStyle CssClass="gridItem" VerticalAlign="Middle" />
                            
                            <HeaderStyle CssClass="gridHeader" />
                        </asp:DataGrid>
                     
                       <%-- <asp:GridView ID="grdprogrammes" runat="server" CellPadding="4" 
                            ForeColor="#909090 " GridLines="None" Width="100%" >
                            <FooterStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>--%>
                                    
                        </td>
                    <%--<td align="center">
                     
                    <asp:GridView ID="grdprogrammes" runat="server"  AutoGenerateColumns="False" SkinID="gv"  
                            CellPadding="4" ForeColor="#909090" GridLines="None" Width="100%" onpageindexchanging="grdprogrammes_PageIndexChanging"  
                         >
                     <FooterStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                <Columns>
                            <asp:BoundField DataField="Sl No#" HeaderText="Sl No." />
                            <asp:BoundField DataField="Date" HeaderText="Date" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="Txn Srl No#" HeaderText="Txn Srl No." />
                              <asp:BoundField DataField="Check No#" HeaderText="Check No." />
                                <asp:BoundField DataField="Cr/Dr" HeaderText="Cr/Dr" />
                                  <asp:BoundField DataField="Transaction Amount" HeaderText="Transaction Amount" />
                                   <asp:BoundField DataField="Balance Amount" HeaderText="Balance Amount" />
                                     <asp:BoundField DataField="Txn# Memo" HeaderText="Txn. Memo" />
                                      <asp:BoundField DataField="Txn# Category" HeaderText="Txn. Category" />
                                     <asp:BoundField DataField="Txn# Pst# Date#" HeaderText="Txn. Pst. Date" />
                                     <asp:BoundField DataField="Txn# Value Date#" HeaderText="Txn. Value Date" />

                                     
                                        </Columns>
                 <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
                                    
                        </td>--%>
                </tr>                
                 <tr>
                     <td align="center" style="height: 24px">
                         <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                             CssClass="fltButton" Width="95px" onclick="btnCancel_Click" 
                             />
                         &nbsp;&nbsp;
            
            <asp:Button ID="btnProcess" runat="server" Text="Process" 
                             CssClass="fltButton" Width="145px" onclick="btnProcess_Click"  /></td>
                 </tr>
            </table>
            
            <br />
            </asp:Panel>
           
    
    </div>
