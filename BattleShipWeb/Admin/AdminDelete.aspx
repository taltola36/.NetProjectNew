<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="AdminDelete.aspx.cs" Inherits="Admin_AdminDelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    something above table and hello!
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
    </div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="BoardsDS" AllowSorting="True" DataKeyNames="Board_ID" OnRowDeleting="GridView1_RowDeleting" Style="margin-right: 0px">
        <Columns>
            <asp:BoundField DataField="Board_ID" HeaderText="Board_ID" SortExpression="Board_ID" />
            <asp:BoundField DataField="Structure_Board" HeaderText="Structure_Board" SortExpression="Structure_Board" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
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
