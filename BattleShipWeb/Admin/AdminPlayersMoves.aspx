<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPageAdmin.master" CodeFile="AdminPlayersMoves.aspx.cs" Inherits="Admin_AdminPlayersMoves" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style7 {
            text-align: center;
        }

        .auto-style8 {
            font-size: larger;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    &nbsp;
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="auto-style7" style="font-size: xx-large; font-weight: 700">
        <br />
        <span class="auto-style8">Players Moves
        <br />
        </span>
        <br />
    </div>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AllowPaging="True" AllowSorting="True" Height="123px" Style="margin-left: 36px; margin-top: 0px" Width="1294px">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Player" HeaderText="Player" SortExpression="Player" />
            <asp:BoundField DataField="IndexHit" HeaderText="IndexHit" SortExpression="IndexHit" />
            <asp:BoundField DataField="Hit" HeaderText="Hit" SortExpression="Hit" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:.NetProjectConnectionString %>" SelectCommand="SELECT * FROM [Move]"></asp:SqlDataSource>
    <br />
    <br />


</asp:Content>
