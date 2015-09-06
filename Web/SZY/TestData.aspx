<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestData.aspx.cs" Inherits="FreezerProPlugin.TestData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" OnClick="Unnamed_Click" Text="dianji" ID="ImportTestData" />
            <br />
            <asp:Label ID="mes" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>