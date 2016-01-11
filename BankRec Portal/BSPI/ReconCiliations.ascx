<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReconCiliations.ascx.cs" Inherits="DesktopModules_BSPI_ReconCiliations" %>
    <style type="text/css">
        .style1
        {
            width: 561px;
        }
    </style>
    <div>
    <script type="text/javascript" language="javascript">
        function printdiv(printpage) {

            var headstr = "<html><head><title></title></head><body>";
            var footstr = "</body>";
            var newstr = document.getElementById(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
</script>
    <asp:Panel ID="pnllnik" runat="server" Width="100%">
    
       <table border="0" cellpadding="3" cellspacing="0"  style=" width:100%">
                
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
        <asp:Panel ID="pnlmsg" runat="server" Width="100%">
       <table width="70%" border="0" cellpadding="4" cellspacing="0"  align="center">
                <tr>
                    <td style="height: 25px" align="center"  >
                        <asp:Label ID="lblErrorLabel" runat="server" ForeColor="Red"></asp:Label>
                        <asp:HiddenField ID="hdnStatementDate" runat="server" />
                        <asp:HiddenField ID="hdnAccountNo" runat="server" />
                        </td>
                </tr>
            </table>
        </asp:Panel>
         <asp:Panel ID="pnlnewprogrm" runat="server" Width="100%" Visible="true" 
        BackColor="#CCCCCC" >
            <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
                <tr>
                    <td align="right" class="style1"  >
                        Main GL Account No:</td>
                    <td align="left" >
                       
                        <asp:TextBox ID="txtAccountNo" runat="server" Width="208px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtAccountNo" Display="Dynamic" ErrorMessage="GL AccountNo is Required" 
                            ForeColor="Red" ValidationGroup="grp1" >*</asp:RequiredFieldValidator>
                        </td>
                </tr>
                <tr>
                    <td align="right" class="style1">
                        Statement Date:</td>
                    <td align="left">
                       <asp:TextBox ID="txtDate" runat="server" ReadOnly="True" ValidationGroup="grp1" 
                            Width="207px"></asp:TextBox>
                        <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtDate" 
                            Format="yyyy mm dd" Separator="/" To-Date="" To-Today="True" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                            ControlToValidate="txtDate" Display="Dynamic" 
                            ErrorMessage="Statement Date is required" ForeColor="Red" 
                            ValidationGroup="grp1">*</asp:RequiredFieldValidator>
                        <%--<asp:Calendar ID="txtDate" runat="server" ></asp:Calendar>--%>
                    </td>
                </tr>
                

              
                <tr>
                    <td align="right" class="style1">
                        Client ID:</td>
                    <td align="left">
                        &nbsp;<asp:DropDownList ID="ddlClient" runat="server" AppendDataBoundItems="true" 
                     Width="187px" 
 
                   >
                    <asp:ListItem Value="400">400</asp:ListItem>
                    <asp:ListItem Value="410">410</asp:ListItem>
                    <asp:ListItem Value="420">420</asp:ListItem>
             <asp:ListItem Value="450">450</asp:ListItem>
                    
                </asp:DropDownList></td>
                </tr>
                

              
                <tr>
                    <td align="right" class="style1"  >
                        &nbsp;</td>
                    <td align="left">
                        <asp:Button ID="Button1" runat="server" Text="Fetch" Width="132px" ValidationGroup="grp1"
                            onclick="Button1_Click" style="height: 26px" />
                    </td>
                </tr>
                

              
            </table>
            
            </asp:Panel>
             
            <asp:Panel ID="pnlexistingprog" runat="server" Width="100%" 
        Visible="false" BackColor="#CCCCCC">
             <table width="70%" border="0" cellpadding="4" cellspacing="0"  align="center">
              <tr>
            <th align="left">
                <asp:LinkButton ID="lnkNew" runat="server" onclick="lnkNew_Click" >[View Summary]</asp:LinkButton>
                </th>
        </tr>
       
         <tr>
               <td>
                <asp:Panel ID="pnlUnc" runat="server" Width="100%"  GroupingText="Uncleared Section"
        Visible="true" BackColor="#CCCCCC">
           
         <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
         <tr>
         <td>
          <asp:Panel ID="pnlUnclearedPayments" runat="server" Width="100%" 
        Visible="false" BackColor="#CCCCCC">
         <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
          <tr>
                     <th align="center">
                         Un-Cleared Incoming Transactions:&nbsp;&nbsp;&nbsp;   <asp:Label ID="lblUnclearedP" runat="server" ></asp:Label>
                         <br />
                     </th>
                 </tr>
                 </table>
         <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
          
                <tr>
                    <td align="center" >
                     
                    <asp:GridView ID="grdCleared" runat="server"  AutoGenerateColumns="False" SkinID="gv" 
                            CellPadding="4" ForeColor="#909090" GridLines="None" Width="100%"  
                            Font-Names= "Arial" Font-Size="Small" AllowPaging="True" onpageindexchanging="grdCleared_PageIndexChanging"  
                         >
                     <FooterStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                <Columns>
                            
                            <asp:BoundField DataField="GLAccount" HeaderText="GLAccount" />
                             <asp:BoundField DataField="DocumentNo" HeaderText="Document No" />
                              <asp:BoundField DataField="Amount" HeaderText="Amount" />
                              <asp:BoundField DataField="Running Balance" HeaderText="Running Balance" />
                              <asp:BoundField DataField="PostingDate" HeaderText="Posting Date" />
                         <asp:BoundField DataField="Descriptions" HeaderText="Descriptions" />
                        <%--<asp:BoundField DataField="OType" HeaderText="Transaction Type" />--%>
                                     
                                        </Columns>
                 <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
                                    
                        <asp:HiddenField ID="hdnBU" runat="server" />
                                    
                        </td>
                </tr>   
                 <tr>
                   <td align="center">
                        <asp:Button ID="btnExport" runat="server" onclick="btnExport_Click" 
                            Text="Export to Excel" Width="162px" />
               </td>
                    </tr> 
                    </table>
        </asp:Panel>
         </td>
         </tr>
          <tr>
               <td>
                <asp:Panel ID="pnlUnclearedDeposit" runat="server" Width="100%" 
        Visible="false" BackColor="#CCCCCC">
        <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
          <tr>
                     <th align="center">
                          Uncleared Outgoing Transactions:   <asp:Label ID="lblUncelaredDepo" runat="server" ></asp:Label>
                         <br />
                     </th>
                 </tr>
                 </table>
         <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
               
                <tr>
                    <td align="center" >
                     
                    <asp:GridView ID="grdTest" runat="server"  AutoGenerateColumns="False" SkinID="gv"   onpageindexchanging="grdTest_PageIndexChanging" 
                            CellPadding="4" ForeColor="#909090" GridLines="None" Width="100%"  
                            Font-Names= "Arial" Font-Size="Small" AllowPaging="True"  
                         >
                     <FooterStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                <Columns>
                            
                            <asp:BoundField DataField="GLAccount" HeaderText="GLAccount" />
                             <asp:BoundField DataField="DocumentNo" HeaderText="Document No" />
                              <asp:BoundField DataField="Amount" HeaderText="Amount" />
                              <asp:BoundField DataField="Running Balance" HeaderText="Running Balance" />
                              <asp:BoundField DataField="PostingDate" HeaderText="Posting Date" />
                         <asp:BoundField DataField="Descrption" HeaderText="Descriptions" />
                         <%-- <asp:BoundField DataField="OType" HeaderText="Transaction Type" />--%>
                        
                                     
                                        </Columns>
                 <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
                                    
                        </td>
                </tr>                            
                  <tr>
            <th align="center">
                <asp:Button ID="btncd" runat="server" Text="ExportToExcel" Width="127px" 
                    onclick="btncd_Click" />
                      </th>
        </tr>
        </table>
        </asp:Panel>
          </td>
        </tr>
        </table>
        </asp:Panel>
        </td>
        </tr>

         <tr>
               <td>
                <asp:Panel ID="pnlCl" runat="server" Width="100%"  GroupingText="Cleared Section"
        Visible="true" BackColor="#CCCCCC">
           
         <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
         <tr>
         <td>
         <tr>
               <td>
                <asp:Panel ID="pnlCleared" runat="server" Width="100%" 
        Visible="false" BackColor="#CCCCCC">
           
         <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
        <tr>
                     <th align="center">
                         Cleared Transactions: <asp:Label ID="lblCleared" runat="server"></asp:Label><br />
                     </th>
                 </tr>
                 </table>
         
         <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
                 
                 <tr>
                    <td align="center">
                     
                    <asp:GridView ID="grdUncleared" runat="server"  AutoGenerateColumns="False" SkinID="gv" 
                            CellPadding="4" ForeColor="#909090" GridLines="None" Width="100%" 
                            Font-Names= "Arial" Font-Size="Small" AllowPaging="True" onpageindexchanging="grdUncleared_PageIndexChanging"  
                         >
                     <FooterStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                <Columns>
                           <asp:BoundField DataField="GLAccount" HeaderText="GLAccount" />
                             <asp:BoundField DataField="DocumentNo" HeaderText="Document No" />
                              <asp:BoundField DataField="Amount" HeaderText="Amount" />
                            <asp:BoundField DataField="Running Balance" HeaderText="Running Balance" />
                           <asp:BoundField DataField="PostingDate" HeaderText="Posting Date" />
                         <asp:BoundField DataField="Descriptions" HeaderText="Descriptions" />
                                     
                                        </Columns>
                 <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
                                    
                        </td>
                </tr> 
                <tr>
                    <td align="center">

                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" 
                            Width="162px" onclick="btnExportToExcel_Click"  />

                    </td> 
                    </tr>   
                    </table>



                     <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
        <tr>
                     <th align="center">
                         Uncleared Transactions on Bank Statement: <asp:Label ID="lblUnrec" runat="server"></asp:Label><br />
                     </th>
                 </tr>
                 </table>
         
      <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
                 
                 <tr>
                    <td align="center">
                     
                    <asp:GridView ID="grdUnrec" runat="server"  AutoGenerateColumns="False" SkinID="gv" onpageindexchanging="grdUnrec_PageIndexChanging" 
                            CellPadding="4" ForeColor="#909090" GridLines="None" Width="100%" 
                            Font-Names= "Arial" Font-Size="Small" AllowPaging="True"   
                         >
                     <FooterStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                <Columns>
                          
                             <asp:BoundField DataField="DocumentNo" HeaderText="Document No" />
                              <asp:BoundField DataField="Amount" HeaderText="Amount" />
                            <asp:BoundField DataField="RunningAmount" HeaderText="Running Balance" />
                           <asp:BoundField DataField="PostingDate" HeaderText="Posting Date" />
                         <asp:BoundField DataField="Descrption" HeaderText="Descriptions" />
                                     
                                        </Columns>
                 <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#909090" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
                                    
                        </td>
                </tr> 
                <tr>
                    <td align="center">

                        <asp:Button ID="btnExportUR" runat="server" Text="Export to Excel" 
                            Width="162px" onclick="btnExportUR_Click"  />

                    </td> 
                    </tr>   
                    </table>
                    </asp:Panel>
                    </td>    
                    </tr> 
                </td> 
                </tr>
                </table>
                </asp:Panel>
                </td>
                </tr>   
            </table>
            
            <br />
            </asp:Panel>
           <asp:Panel ID="pnlSummary" runat="server" Width="100%" 
        Visible="false" >
        <div id="div_print">
        <table width="80%" border="0" cellpadding="4" cellspacing="0"  align="center"  id="Printable" runat="server">
               <tr>
               <td>
         <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center">
              <tr>
             <td colspan="2" align="center">
           
                <asp:Label ID="Label1" runat="server" Text="Transaction Summary" 
                     Font-Bold="True" Font-Size="X-Large"></asp:Label>
                </td>
            
              
         
        </tr>
        <tr>
        <td align="left">
           
                <asp:Label ID="lblAccountNo" runat="server" Font-Bold="True" Font-Size="Large" ></asp:Label>
                </td>
            <td align="left">
           
                <asp:Label ID="lblDate" runat="server"  
                     Font-Bold="True" Font-Size="Large" ></asp:Label>
                
                </td>
              
         
        </tr>
        </table>
        </td>
           </tr>
           <tr>
               <td>
                <table width="100%" border="0" cellpadding="4" cellspacing="0"  align="center"  class="imagetable">
             
                <tr>
                    <td align="right" colspan="2"  >
                        <asp:Label ID="Label8" runat="server" Font-Bold="True" 
                            Text="Account Description:"></asp:Label>    
                        <asp:Label ID="lblAccountNAme" runat="server"></asp:Label>
                    </td>
                    <td align="left" >
                       
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="AccountType:"></asp:Label>
                        <asp:Label ID="lblAccounType" runat="server" ></asp:Label>
                    </td>
                </tr>
                  
                 
                    <tr>
                        <td align="right" colspan="2">
                            <b>Bank statement Opening&nbsp; Balance:</b></td>
                        <td align="left">
                            <asp:Label ID="lblOPeningBalance" runat="server"></asp:Label>
                        </td>
                    </tr>
                  
                 
                 <tr>
                     <td align="right" colspan="2" >
                         &nbsp;</td>
                     <td align="left" >
                         &nbsp;</td>
                 </tr>
                  
                 
                 <tr>
                     <td align="right" colspan="2" >
                         <b>Cleared Transactions for the period</b></td>
                     <td align="left">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td align="right" colspan="2" >
                         Sum of Cleared Checks and Payments:</td>
                     <td align="left">
                         <asp:Label ID="lblDepositCount" runat="server"></asp:Label>
                         <asp:Label ID="lblClearedDepositFigure" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td align="right"  colspan="2">
                         Sum of Cleared Deposit</td>
                     <td align="left" >
                         <asp:Label ID="lblClearedCount" runat="server"></asp:Label>
                         <asp:Label ID="lblClearedFigure" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td align="right" colspan="2">
                         Sum:</td>
                     <td align="left" >
                         <asp:Label ID="lblclearedSum" runat="server"></asp:Label>
                     </td>
                 </tr>
                    <tr>
                        <td align="right" colspan="2">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                 <tr>
                      <td align="right" colspan="2" ><strong>Bank Statement Closing Balance<b> :</b> </b></strong></td>
                     <td align="left">
                         <asp:Label ID="lblBSCB" runat="server" ></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td align="right"colspan="2" >
                         &nbsp;</td>
                     <td align="left">
                         &nbsp;</td>
                 </tr>
                  
                 
                 <tr>
                        <td align="right" colspan="2">
                            <strong>UnReconciled Cheques:</strong></td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                                 
                 <tr>
                     <td align="right" colspan="2">
                         Incomming Cheques Processed:</td>
                     <td align="left">
                         <asp:Label ID="lblUnclearedCheckCount" runat="server"></asp:Label>
                         <asp:Label ID="UnclearedCheckFigure" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td align="right" colspan="2" >
                         Outgoing Cheques Processed:</td>
                     <td align="left">
                         <asp:Label ID="lblUncleraedDepositCount" runat="server"></asp:Label>
                         <asp:Label ID="lblUnclearedDepositFigure" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td align="right" colspan="2">
                         Sum:</td>
                     <td align="left">
                         <asp:Label ID="Label7" runat="server"></asp:Label>
                     </td>
                 </tr>

                  <tr>
                        <td align="right" colspan="2">
                            <b>UnReconciled Payments :</b></td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                                 
                 <tr>
                     <td align="right" colspan="2">
                         Other Incoming Payments:</td>
                     <td align="left">
                         <asp:Label ID="Label3" runat="server"></asp:Label>
                         <asp:Label ID="Label4" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td align="right" colspan="2" >
                         Other Outgoing Payments:</td>
                     <td align="left">
                         <asp:Label ID="Label5" runat="server"></asp:Label>
                         <asp:Label ID="Label6" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td align="right" colspan="2">
                         Sum:</td>
                     <td align="left">
                         <asp:Label ID="lblUnclearedSum" runat="server"></asp:Label>
                     </td>
                 </tr>
                   
                 <tr>
                     <td align="right" colspan="2" >
                         &nbsp;</td>
                     <td align="left">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td align="right" colspan="2" >
                         <b>Ledger Balance:</b></td>
                     <td align="left">
                         <asp:Label ID="lblRegisteredbalance" runat="server"></asp:Label>
                     </td>
                 </tr>
                  <tr>
                         <td align="right" colspan="2">
                             <b>Uncleared Items on Bank Statement:</b></td>
                         <td align="left">
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td align="right" colspan="2">
                             Quantity of Uncleared Items on Bank Statement:</td>
                         <td align="left">
                             <asp:Label ID="lblA" runat="server" Text=""></asp:Label>
                         </td>
                    </tr>
                     <tr>
                         <td align="right" colspan="2">
                             Value of UnCleared Items on Bank Statement (CR):</td>
                         <td align="left">
                             <asp:Label ID="lblCR" runat="server"></asp:Label>
                         </td>
                     </tr>
                 
                    <tr>
                        <td align="right" colspan="2">
                            Value of Uncleared Items on Bank Statement:(DR):</td>
                        <td align="left">
                            <asp:Label ID="lblDR" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            Sum :</td>
                        <td align="left">
                            <asp:Label ID="lblB" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                 
                    <tr>
                        <td align="right" colspan="2">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                    <%--<tr>
                         <td align="right" colspan="2">
                             <b>Expected Bank Statement Closing Balance:</b></td>
                         <td align="left">
                             <asp:Label ID="lblExpected" runat="server"></asp:Label>
                         </td>
                     </tr>
                 --%>
                    <tr>
                        <td align="right" colspan="2">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                 
                    <tr>
                        <td align="right" colspan="1" width="25%">
                            Prepared By:</td>
                         <td align="left" colspan="1" width="25%">
                            Name:</td>
                             <td align="left" width="50%">
                            Signature/Date:</td>
                             
                    </tr>
                    <tr>
                        <td align="right" colspan="1" width="25%">
                            Reviewed By:</td>
                         <td align="left" colspan="1" width="25%">
                            Name:</td>
                             <td align="left" width="50%">
                            Signature/Date:</td>
                            
                    </tr>
                    <tr>
                        <td align="right" colspan="1" width="25%">
                            Approved By:</td>
                         <td align="left" colspan="1" width="25%">
                            Name:</td>
                             <td align="left" width="50%">
                            Signature/Date:</td>
                             
                    </tr>
                  
                 
            </table>
               </td>
               </tr>
           </table>
        </div>
             <table width="70%" border="0" cellpadding="4" cellspacing="0"  align="center">
               <tr>
               <td align="right" >
                     
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="132px" 
                            onclick="btnCancel_Click" />
                    </td>
                    <td align="left" >
                     
                        <input name="b_print" type="button" class="ipt"   
                            onClick="printdiv('div_print');" value=" Print ">
                    </td>
                </tr>
        </table>
            
            <br />
            </asp:Panel>
    </div>

