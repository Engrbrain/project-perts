<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Testing.aspx.vb" Inherits="UnicoTesting.Testing" %>



<%@ Register src="RegisterUser.ascx" tagname="RegisterUser" tagprefix="uc1" %>



<%@ Register src="IdocReport.ascx" tagname="IdocReport" tagprefix="uc2" %>



<%@ Register src="Extract_From_Legacy.ascx" tagname="Extract_From_Legacy" tagprefix="uc3" %>
<%@ Register src="UnBalancedData.ascx" tagname="UnBalancedData" tagprefix="uc4" %>



<%@ Register src="LoadedData.ascx" tagname="LoadedData" tagprefix="uc5" %>



<%@ Register src="Login.ascx" tagname="Login" tagprefix="uc6" %>
<%@ Register src="AddGL_Account.ascx" tagname="AddGL_Account" tagprefix="uc7" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
       
        
       
        <uc7:AddGL_Account ID="AddGL_Account1" runat="server" />
       
        
       
        <br />
    
    </div>
    </form>
</body>
</html>
