<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MKForum.Register" %>

<%@ Register Src="~/ucRegister.ascx" TagPrefix="uc1" TagName="ucRegister" %>
<%@ Register Src="~/ucLogin.ascx" TagPrefix="uc1" TagName="ucLogin" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        <uc1:ucRegister runat="server" ID="ucRegister" />

        <uc1:ucLogin runat="server" ID="ucLogin" />

    </form>
</body>



   
</html>
