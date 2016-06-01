<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="Admin" UnobtrusiveValidationMode="None" %>

<%@ Register TagPrefix="uc1" TagName="ThemesUC" Src="~/Admin/ThemesUC.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="animate.css" />

    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: xx-large;
            text-align: center;
        }

        .auto-style2 {
            width: 100%;
            margin-left: 234px;
        }

        .auto-style3 {
            width: 359px;
            text-align: right;
            font-weight: bold;
            height: 26px;
        }

        .auto-style4 {
            width: 191px;
            height: 26px;
        }

        .auto-style5 {
            width: 359px;
            text-align: right;
            height: 25px;
        }

        .auto-style6 {
            width: 191px;
            height: 25px;
        }

        .auto-style7 {
            height: 25px;
        }

        .auto-style8 {
            font-weight: bold;
        }

        .auto-style9 {
            width: 359px;
            text-align: right;
            height: 25px;
            font-weight: bold;
        }

        .auto-style10 {
            height: 26px;
        }

        .auto-style11 {
            width: 480px;
            height: 76px;
        }

        .auto-style12 {
            font-size: large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <strong>
                <br />
                <br />
                        <img alt="battleship text" class="flash" src="battleship-iphone-logo.png" /><br />
                <br />
                <br />
                <br />
                <br />
                <span class="auto-style12">please enter username:<br />
                    <br />
                </span>
            </strong>
        </div>
        <table class="auto-style2">
            <tr>
                <td class="auto-style3">UserName</td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBoxUserName" runat="server" Width="180px"></asp:TextBox>
                </td>
                <td class="auto-style10">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxUserName" CssClass="auto-style8" ErrorMessage="Please enter Username" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style6">
                    <asp:Button ID="Button_Login" runat="server" OnClick="Button_Login_Click" Style="margin-left: 40px; margin-right: 4px" Text="Login" Width="112px" />
                </td>
                <td class="auto-style7">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
            </tr>
        </table>
        <uc1:ThemesUC runat="server" ID="ThemesUC" />
    </form>
</body>
</html>
