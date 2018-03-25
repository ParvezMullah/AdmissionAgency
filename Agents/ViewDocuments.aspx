<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDocuments.aspx.cs" Inherits="Agents_ViewDocuments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:RadioButton ID="radioCurrentStudents" runat="server" GroupName="a"  AutoPostBack="true" OnCheckedChanged="radioCurrentStudents_CheckedChanged" Checked="True" />CurrentStudents
        <asp:RadioButton ID="radioPreviousStudents" runat="server"  GroupName="a"  AutoPostBack="true" OnCheckedChanged="radioPreviousStudents_CheckedChanged"  />Previous Students <br />
        <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
