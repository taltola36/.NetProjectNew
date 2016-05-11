<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addedSuccessfully.aspx.cs" Inherits="Admin_addedSuccessfully" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
            text-align: center;
        }
        .auto-style2 {
            width: 100%;
            height: 99px;
            margin-left: 457px;
        }
        .auto-style3 {
            width: 70px;
            height: 97px;
        }
        .auto-style4 {
            text-align: center;
        }
        .auto-style5 {
            height: 97px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="auto-style4">
    
        <br class="auto-style1" />
        <br class="auto-style1" />
        <br class="auto-style1" />
        <br class="auto-style1" />
        <span class="auto-style1">Board added successfully</span></div>
        <table class="auto-style2">
            <tr>
                <td class="auto-style3">
                    <asp:Button ID="ButtonAddMore" runat="server" OnClick="ButtonAddMore_Click" Text="add more boards" />
                </td>
                <td class="auto-style5">
                    <asp:Button ID="ButtonBack" runat="server" OnClick="ButtonBack_Click" Text="back to menu" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
