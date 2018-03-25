<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewStatus.aspx.cs" Inherits="Students_ViewStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:RadioButton ID="radioApplication" runat="server" GroupName="a" OnCheckedChanged="radioApplication_CheckedChanged" AutoPostBack="true" Checked="True" />Application
        <asp:RadioButton ID="radioVisa" runat="server"  GroupName="a" OnCheckedChanged="radioVisa_CheckedChanged" AutoPostBack="true" />Visa Status <br/>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
