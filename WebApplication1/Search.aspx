<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="WebApplication1.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<style type="text/css">
		#form1 {
			text-align: left;
		}
		.auto-style1 {
			text-align: center;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
        	<br />
        	Bank</div>
    	<div class="auto-style1">
			<asp:DropDownList ID="DropDownList1" runat="server">
				<asp:ListItem>PickOne</asp:ListItem>
				<asp:ListItem>ANZ</asp:ListItem>
				<asp:ListItem>Westpac</asp:ListItem>
				<asp:ListItem>Commonwealth</asp:ListItem>
				<asp:ListItem>ING</asp:ListItem>
			</asp:DropDownList>
			<br />
			<br />
			Bank Service<br />
			<asp:DropDownList ID="DropDownList2" runat="server">
				<asp:ListItem>PickOne</asp:ListItem>
				<asp:ListItem> Saving Accounts</asp:ListItem>
				<asp:ListItem>Loans</asp:ListItem>
				<asp:ListItem>Online Banking</asp:ListItem>
			</asp:DropDownList>
			<br />
			<br />
			Gender<br />
			<asp:DropDownList ID="DropDownList3" runat="server" style="margin-bottom: 0px">
				<asp:ListItem>PickOne</asp:ListItem>
				<asp:ListItem>Male</asp:ListItem>
				<asp:ListItem>Female</asp:ListItem>
			</asp:DropDownList>
			<br />
			<br />
			State<br />
			<asp:DropDownList ID="DropDownList4" runat="server">
				<asp:ListItem>PickOne</asp:ListItem>
				<asp:ListItem>QLD</asp:ListItem>
				<asp:ListItem>SA</asp:ListItem>
				<asp:ListItem>TAS</asp:ListItem>
				<asp:ListItem>VIC</asp:ListItem>
				<asp:ListItem>WA</asp:ListItem>
				<asp:ListItem>ACT</asp:ListItem>
				<asp:ListItem>NT</asp:ListItem>
				<asp:ListItem>NSW</asp:ListItem>
			</asp:DropDownList>
			<br />
			<br />
			<asp:Button ID="Button1" runat="server" Text="SEARCH" OnClick="Button1_Click" />
			<br />
			<br />
		</div>
    	<asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="214px" style="text-align: center" Width="699px" >
			<AlternatingRowStyle BackColor="White" />
			<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
			<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
			<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
			<RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
			<SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
			<SortedAscendingCellStyle BackColor="#FDF5AC" />
			<SortedAscendingHeaderStyle BackColor="#4D0000" />
			<SortedDescendingCellStyle BackColor="#FCF6C0" />
			<SortedDescendingHeaderStyle BackColor="#820000" />
		</asp:GridView>
    </form>
</body>
</html>
