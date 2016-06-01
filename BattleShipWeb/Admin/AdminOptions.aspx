<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminOptions.aspx.cs" Inherits="Admin_AdminOptions" %>

<%@ Register TagPrefix="uc1" TagName="ThemesUC" Src="~/Admin/ThemesUC.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 72px;
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
<body>
    <form id="form1" runat="server">
        <div  style="text-align: center; font-size: large;" >
            Choose one of the following options:
    
        <br />

            <br />

        </div>
        <table class="auto-style1"  style="text-align: center;">
            <tr>
                <td class="auto-style2">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://localhost:54968/Admin/AdminAdd.aspx" Style="font-size: large">Add new board</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="http://localhost:54968/Admin/AdminDelete.aspx" Style="font-size: large">Delete board</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="http://localhost:54968/Admin/AdminPlayersMoves.aspx" style="font-size: large">View Players Moves</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="http://localhost:54968/Admin/AdminStatistics.aspx" style="font-size: large">View Statistics</asp:HyperLink>
                </td>
            </tr>
        </table>
        <uc1:ThemesUC runat="server" ID="ThemesUC" />
    </form>
</body>
</html>
