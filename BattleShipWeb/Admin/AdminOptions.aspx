<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminOptions.aspx.cs" Inherits="Admin_AdminOptions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 29px;
        }
        .auto-style3 {
            height: 27px;
        }
        .auto-style4 {
            height: 28px;
        }
    </style>
</head>
<body style="font-weight: 700; text-align: center; font-size: x-large">
    <form id="form1" runat="server">
    <div>
    
        Choose of the following options:
    
        <br />
    
    </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://localhost:54968/Admin/AdminAdd.aspx" style="font-size: large">Add new board</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="http://localhost:54968/Admin/AdminDelete.aspx" style="font-size: large">Delete board</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="http://localhost:54968/Admin/AdminEdit.aspx" style="font-size: large">Edit Board</asp:HyperLink>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
