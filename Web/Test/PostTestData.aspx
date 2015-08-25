<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostTestData.aspx.cs" Inherits="RuRo.Web.Test.PostTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="i">Ip</label>
            <asp:TextBox runat="server" ID="i"></asp:TextBox>
            <label for="u">账号</label>
            <asp:TextBox runat="server" ID="u"></asp:TextBox>
            <label for="p">密码</label>
            <asp:TextBox runat="server" ID="p"></asp:TextBox>
            <asp:Button  runat="server" OnClick="Unnamed_Click" Text="提交"/>
            <br />
            <label for="d">数据</label>
            <br />
            <asp:TextBox runat="server" ID="d" TextMode="MultiLine" Height="300px" Width="600px" BorderStyle="Dashed"></asp:TextBox>
            <br />
            <label for="r">结果</label>
            <br />
            <label id="r" runat="server" ></label>

        </div>
    </form>
</body>
</html>
