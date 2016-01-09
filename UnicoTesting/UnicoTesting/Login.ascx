<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Login.ascx.vb" Inherits="UnicoTesting.Login" %>
  <link rel='stylesheet' href='assets/css/fullcalendar.css'>
<link rel='stylesheet' href='assets/css/datatables/datatables.css'>
<link rel='stylesheet' href='assets/css/datatables/bootstrap.datatables.css'>
<link rel='stylesheet' href='assets/scss/chosen.css'>
<link rel='stylesheet' href='assets/scss/font-awesome/font-awesome.css'>
<link rel='stylesheet' href='assets/css/app.css'>

<div class="main-content-inner">
           
              <h3 class="form-title form-title-first"><i class="icon-lock"></i> Login Example</h3>
              <div class="form-group">
                <label>Username</label>
                
    <asp:TextBox ID="txtUsername"  class="form-control" placeholder="Enter Username" runat="server"></asp:TextBox>
              </div>
              <div class="form-group">
                <label>Password</label>
                
                  <asp:TextBox ID="txtPassword" type="password"  class="form-control" placeholder="Enter Password" runat="server"></asp:TextBox>
              </div>
              <div class="form-group">
                <div class="checkbox">
                  <label>
                    <input type="checkbox"> Remember me <br />
<asp:Label ID="lblMsg" runat="server" ForeColor="#FF3300" style="font-weight: 700"></asp:Label>
                  </label>
                </div>
              </div>
             
                <asp:Button ID="btnSignin" CssClass ="btn btn-primary btn-lg" runat="server" Text="Sign in" />
                 <asp:Button ID="Button1" CssClass ="btn btn-link" runat="server" Text="Cancel"  Enabled ="false"/>


           
            </div>

