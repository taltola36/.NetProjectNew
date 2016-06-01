<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="AdminDelete.aspx.cs" Inherits="Admin_AdminDelete" %>

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
        <span class="auto-style8">Delete Board<br />
        </span><br />
        <br />
    </div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="BoardsDS" AllowSorting="True" DataKeyNames="Board_ID" OnRowDeleting="GridView1_RowDeleting" Style="margin-right: 0px; text-align: center;" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Height="123px" Width="1294px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Board_ID" HeaderText="Board_ID" SortExpression="Board_ID" />
            <asp:BoundField DataField="Structure_Board" HeaderText="Structure_Board" SortExpression="Structure_Board" />
            <asp:CommandField ShowDeleteButton="True" />
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
    <asp:SqlDataSource ID="BoardsDS" runat="server"
        ConnectionString="<%$ ConnectionStrings:.NetProjectConnectionString %>"
        SelectCommand="SELECT * FROM [Board]"
        DeleteCommand="DELETE FROM Board WHERE (Board_ID = @Board_ID)"
        ConflictDetection="CompareAllValues" InsertCommand="INSERT INTO [Board] ([Structure_Board]) VALUES (@Structure_Board)" OldValuesParameterFormatString="original_{0}">
        <InsertParameters>
            <asp:Parameter Name="Structure_Board" Type="String" />
        </InsertParameters>
        <DeleteParameters>
            <asp:Parameter Name="Board_ID" />
        </DeleteParameters>
    </asp:SqlDataSource>

</asp:Content>
