<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ThemesUC.ascx.cs" Inherits="Admin_ThemesUC" %>
<div style="margin-top: 350px;">
    Themes:
    <asp:RadioButtonList ID="themesRb" AutoPostBack="True" runat="server" RepeatDirection="Horizontal" OnTextChanged="themesRb_SelectedIndexChanged" OnSelectedIndexChanged="themesRb_SelectedIndexChanged" Style="margin-left: 0px">
        <asp:ListItem Text="Black" Value="Black" Selected="True" />
        <asp:ListItem Text="Brown" Value="Brown" />
        <asp:ListItem Text="White" Value="White" />
    </asp:RadioButtonList>
</div>
