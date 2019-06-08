<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="staffLogin.aspx.cs" Inherits="WebApplication1.staffLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        	Username</div>
    	<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
		<br />
		<br />
		Password<br />
		<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
		<br />
		<br />
		<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
		<br />
		<br />
		<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="LOGIN" />
    </form>
</body>
</html>
