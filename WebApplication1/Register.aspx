<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication1.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    	Username<p>
			<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
			<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
		</p>
		<p>
			First Name</p>
		<p>
			<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
		</p>
		<p>
			Last Name</p>
		<p>
			<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
		</p>
		D.O.B<p>
			<asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
		</p>
		Phone<p>
			<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
		</p>
		<p>
			<asp:CheckBox ID="CheckBox1" runat="server" Text="Stay Anonymous"/>
		</p>
		<asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
    </form>
</body>
</html>
