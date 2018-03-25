<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Application.aspx.cs" Inherits="Application" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:MultiView ID="MultiView1" runat="server" OnActiveViewChanged="setPage">
        <asp:View ID="view1" runat="server">
        Field of interest:<asp:DropDownList ID="ddField" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddField_SelectedIndexChanged">
            </asp:DropDownList><br />
            Program of interest:<asp:DropDownList ID="ddProgram" runat="server">
            </asp:DropDownList><br />
            <asp:Button ID="btnNext" runat="server" Text="Next" onclick="btnNext_Click" />
        </asp:View>
        <asp:View ID="view2" runat="server">
        Select University in which you want to apply:
            <asp:DropDownList ID="ddUniversity" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddUniversity_SelectedIndexChanged">
            </asp:DropDownList>
            <div id="divAgent" runat="server">
            Recommended Agent:<br />
            Name :<asp:Label ID="lblAgentName" runat="server" Text=""></asp:Label><br />
            Gender :<asp:Label ID="lblAgentGender" runat="server" Text=""></asp:Label><br />
            Contact Details:
            Phone:<asp:Label ID="lblAgentPhone" runat="server" Text=""></asp:Label><br />
            Email:<asp:Label ID="lblAgentEmail" runat="server" Text=""></asp:Label><br />
            Success Rate:<asp:Label ID="lblAgentSuccessRate" runat="server" Text=""></asp:Label><br />
                <asp:RadioButtonList ID="rdOptions" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="rdOptions_SelectedIndexChanged">
                <asp:ListItem Text="Select the recommended agent" Value="0"></asp:ListItem>
                <asp:ListItem Text="Select another agent" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div id="divAgentList" runat="server" style="display:none">
            <asp:GridView ID="gvAgents" runat="server" ShowFooter="true" AutoGenerateColumns="false" BackColor="White" BorderColor="#336666" BorderStyle="Double" GridLines="Both" Font-Size="Medium">
<AlternatingRowStyle BackColor="#DCDCDC" />
<Columns>
    <asp:BoundField datafield="firstname" headertext="First Name"/>
    <asp:BoundField datafield="middlename" headertext="Middle Name"/>
    <asp:BoundField datafield="lastname" headertext="Last Name"/>
    <asp:BoundField datafield="EmailID" headertext="EmailID"/>
    <asp:BoundField datafield="gender" headertext="Gender"/>
    <asp:BoundField datafield="phone" headertext="Phone"/>
    
    <asp:BoundField datafield="successrate" headertext="successrate(in %)"/>
    </Columns>
            </asp:GridView>
            <asp:DropDownList ID="ddlPrefferedAgent" runat="server" ValidationGroup="gp">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="gp" ControlToValidate="ddlPrefferedAgent" InitialValue="0" ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                onclick="btnSubmit_Click" ValidationGroup="gp" />
            </div>
            <asp:Button ID="btnSubmitRecommended" runat="server" Text="Submit" 
                onclick="btnSubmitRecommended_Click" />
        </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>
