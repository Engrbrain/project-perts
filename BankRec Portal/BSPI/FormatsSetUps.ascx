<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FormatsSetUps.ascx.cs" Inherits="DesktopModules_BSPI_FormatsSetUps" %>
<div>
    <asp:Panel ID="pnlMain" runat="server" Width="100%" >
         <table width="100%">
        <tr>
            <th align="center">
                Bank Statement Format&nbsp; Management<br />
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
    <asp:Panel ID="pnlBank" runat="server" Width="100%" Visible="true"  GroupingText=" Filter By Bank"
        BackColor="#CCCCCC" >
         <table width="50%" align="center">
        <tr>
            <th align="center" colspan="2">
               </th>
        </tr>
        <tr>
            <td align="right" class="style3">
                Bank Name :
            </td>
            <td>
                <asp:DropDownList ID="ddlBankEarly" runat="server" AppendDataBoundItems="true" 
                    ValidationGroup="grp1" Width="187px" 
                    onselectedindexchanged="ddlBankEarly_SelectedIndexChanged" 
                    AutoPostBack="True">
                    <asp:ListItem Value="0">[Select Bank]</asp:ListItem>
                    <asp:ListItem Value="1">First Bank Plc.</asp:ListItem>
                    <asp:ListItem Value="2">Diamond Bank</asp:ListItem>
                    <asp:ListItem Value="3">Fidelity Bank</asp:ListItem>
                    <asp:ListItem Value="4">City Bank</asp:ListItem>
                    <asp:ListItem Value="5">Stanbic IBTC Bank</asp:ListItem>
                    <asp:ListItem Value="6">Zenith Bank</asp:ListItem>
                    <asp:ListItem Value="7">Skye Bank</asp:ListItem>
                    <asp:ListItem Value="8">Stanbic IBTC Bank</asp:ListItem>
                    <asp:ListItem Value="9">United Bank</asp:ListItem>
                    <asp:ListItem Value="10">Union Bank</asp:ListItem>
                    <asp:ListItem Value="11">Wema Bank</asp:ListItem>
                    <asp:ListItem Value="12">GT Bank</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="ddlBankEarly" ErrorMessage="Bank Name is required" 
                    InitialValue="0" ValidationGroup="grp1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
             
    </table>
        
    </asp:Panel>
    <asp:Panel ID="pnlExisting" runat="server" Width="100%"  Visible="false" 
        BackColor="#CCCCCC" ForeColor="#CCCCCC">
         <table width="100%" align="center">
         <tr>
            <td align="center">
                <asp:LinkButton ID="lnkExisting" runat="server" onclick="lnkExisting_Click"  >[Existing]</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="lnkNew" runat="server" onclick="lnkNew_Click" >[Add New]</asp:LinkButton>
            </td>
        </tr>
         <tr>
            <th align="center">
                Existing Formats</th>
        </tr>
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
                <asp:GridView ID="grvAccount" runat="server" AutoGenerateColumns="False" 
                    EmptyDataText="No record at this time" AllowPaging="True" Width="100%" onpageindexchanging="grvAccount_PageIndexChanging" Font-Names= "Arial" Font-Size="Small"
                   >
                   <FooterStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="ColumnName" >
                            <ItemTemplate >
                                <asp:LinkButton ID="lnkAccount" runat="server" 
                                    CommandArgument='<%# Eval("ID") %>' 
                                    Text='<%# Eval("ColumnName") %>' onclick="lnkAccount_Click" ></asp:LinkButton>
                         
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:BoundField DataField="SampleText" HeaderText="Sample Text" />
                        <asp:BoundField DataField="BankName" HeaderText="Bank Name" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="moveUp" runat="server" 
                                            CommandName='<%#SortCommandName(Eval("ID"),1) %>' ImageUrl="~/images/up.gif" 
                                            OnClick="SortItem_Click" ToolTip="move up" 
                                            Visible='<%#GetVisibility(Eval("ID"),1) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="moveDown" runat="server" 
                                            CommandName='<%#SortCommandName(Eval("ID"),0) %>' ImageUrl="~/images/dn.gif" 
                                            OnClick="SortItem_Click" ToolTip="move down" 
                                            Visible='<%#GetVisibility(Eval("ID"),0) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnRemove" runat="server" CommandName='<%#EditCommandName(Eval("ID")) %>'
                                        ImageUrl="~/images/delete.gif" OnClick="btnRemove_Click" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                        Style="height: 16px" ToolTip="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
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
                New Column</th>
        </tr>
             <tr>
                 <td align="right" class="style3">
                     Column Name:</td>
                 <td>
                     <asp:TextBox ID="txtColmnNAme" runat="server" Width="189px" 
                         ValidationGroup="grp1"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                         ControlToValidate="txtColmnNAme" ErrorMessage="Colmn Name is required" 
                         ValidationGroup="grp1">*</asp:RequiredFieldValidator>
                 </td>
             </tr>
        <tr>
            <td align="right" class="style3">
                Sample Text:</td>
            <td>
                <asp:TextBox ID="txtSampleText" runat="server" ValidationGroup="grp1" 
                    Width="189px"></asp:TextBox>
            </td>
        </tr>
             <tr>
                 <td align="right" class="style3">
                     Status :
                 </td>
                 <td>
                     <asp:CheckBox ID="chkActive" runat="server" Checked="True" Enabled="False" 
                         Text="Active" />
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
<asp:HiddenField ID="hdnCurrent" runat="server" />
    <asp:HiddenField ID="hdnBank" runat="server" />
    </div>