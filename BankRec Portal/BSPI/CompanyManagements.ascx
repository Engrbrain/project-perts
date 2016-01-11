<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CompanyManagements.ascx.cs" Inherits="DesktopModules_BSPI_CompanyManagements" %>
<div>
    <asp:Panel ID="pnlMain" runat="server" Width="100%" >
         <table width="100%">
        <tr>
            <th align="center">
                <br />
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
    <asp:Panel ID="pnlVsbblcnrl" runat="server" Width="100%" >
          <table align="center">
         <tr>
            <td>
                 <asp:LinkButton ID="lnkExisting" runat="server" onclick="lnkExisting_Click"  >[Existing]</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="lnkNew" runat="server" onclick="lnkNew_Click" >[Add New]</asp:LinkButton>
            </td>
        </tr>
        
        
    </table>        
    </asp:Panel>
    
    <asp:Panel ID="pnlExisting" runat="server" Width="100%"  Visible="false" 
        BackColor="#CCCCCC">
         <table align="center" style="width: 100%; height: 279px">
         
             <tr>
                 <td align="center">
                     <asp:Label ID="lblMessage" runat="server"></asp:Label>
                 </td>
             </tr>

             <tr>
              <td>
              
              </td>
             </tr>
        <tr>
            <td align="center">
               <asp:Panel ID="pnlExist" runat="server" Width="100%" Visible="true" >
         <table width="100%" align="center">
                <asp:GridView ID="grvCompany" runat="server" AutoGenerateColumns="False" 
                    EmptyDataText="No record at this time" AllowPaging="True" Width="100%" onpageindexchanging="grvCompany_PageIndexChanging" Font-Names= "Arial" Font-Size="Small"
                    >
                    <FooterStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Name" >
                            <ItemTemplate >
                                <asp:LinkButton ID="lnkCompany" runat="server" 
                                    CommandArgument='<%# Eval("ID") %>' 
                                    Text='<%# Eval("CompanyName") %>' onclick="lnkCompany_Click" ></asp:LinkButton>
                         
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CompanyCode" HeaderText="Company Code" />

                        <asp:BoundField DataField="Status" HeaderText="Status" />
                    </Columns>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
                <asp:HiddenField ID="hdnCurrent" runat="server" />
        </table>
        </asp:Panel>
            </td>
        </tr>
        
    </table>
        
    </asp:Panel>
   <asp:Panel ID="pnlNew" runat="server" Width="100%" Visible="False" 
        BackColor="#CCCCCC" >
         <table width="50%" align="center">
        <tr>
            <th align="center" colspan="2">
                New Company</th>
        </tr>
        <tr>
            <td align="right" class="style3">
                Company Name :
            </td>
            <td>
            <asp:TextBox ID="txtName" runat="server" Width="189px" ValidationGroup="grp1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtName" ErrorMessage="Company name is required" 
                    ValidationGroup="grp1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
             <tr>
                 <td align="right" class="style3">
                     Company Code:</td>
                 <td>
                     <asp:TextBox ID="txtCode" runat="server" Width="189px" ValidationGroup="grp1"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                         ControlToValidate="txtCode" ErrorMessage="Company Code is required" 
                         ValidationGroup="grp1">*</asp:RequiredFieldValidator>
                 </td>
             </tr>
        <tr>
            <td align="right" class="style3">
               Status :
            </td>
            <td>
                <asp:CheckBox ID="chkActive" runat="server" Text="Active" Checked="True" 
                    Enabled="False"/>            
            </td>
        </tr>
        <tr>
            <td align="right" class="style3">
               <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    onclick="btnCancel_Click" Width="87px" 
                   />
            </td>
            <td>
                 <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                     ValidationGroup="grp1" onclick="btnSubmit_Click" Width="87px" 
                     />          
            </td>
        </tr>
             
    </table>
        
    </asp:Panel>
    </div>