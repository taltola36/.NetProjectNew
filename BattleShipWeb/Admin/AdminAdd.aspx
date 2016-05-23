<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPageAdmin.master" CodeFile="AdminAdd.aspx.cs" Inherits="Admin_AdminAdd" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style4 {
            color: #FF0000;
        }
        .auto-style5 {
            font-weight: bold;
            color: #FF0000;
        }
        .auto-style6 {
            font-size: large;
        }
        .auto-style7 {
            font-size: xx-large;
        text-align: center;
    }
    .auto-style8 {
        font-size: larger;
        text-align: center;
    }
    .auto-style9 {
        text-align: center;
    }
        .auto-style10 {
            height: 27px;
            width: 722px;
        }
        .auto-style28 {
            height: 0px;
            width: 754px;
            margin-left: 8px;
        }
    </style>
</asp:Content>

<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    &nbsp;
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="auto-style9"><strong><span class="auto-style7" style="text-align: center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
    &nbsp;<span class="auto-style8">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Add New Board</span><br />
    </span></strong>
    </span>
    <br />
    <table class="auto-style1" style="height: 357px; width: 382px; margin-left: 445px; margin-right: 3px; margin-top: 0px; margin-bottom: 0px;">
        <tr>
            <td class="auto-style14"></td>
            <td class="auto-style5">A</td>
            <td class="auto-style5">B</td>
            <td class="auto-style5">C</td>
            <td class="auto-style5">D</td>
            <td class="auto-style5">E</td>
            <td class="auto-style5">F</td>
            <td class="auto-style5">G</td>
            <td class="auto-style5">H</td>
            <td class="auto-style5">I</td>
            <td class="auto-style5">J</td>
        </tr>
        <tr>
            <td class="auto-style5">1</td>
            <td class="auto-style6">
                <asp:CheckBox ID="CheckBox1" runat="server" Text=" " />
            </td>
            <td class="auto-style14">
                <asp:CheckBox ID="CheckBox2" runat="server" Text=" " />
            </td>
            <td class="auto-style14">
                <asp:CheckBox ID="CheckBox3" runat="server" Text=" " />
            </td>
            <td class="auto-style14">
                <asp:CheckBox ID="CheckBox4" runat="server" Text=" " />
            </td>
            <td class="auto-style14">
                <asp:CheckBox ID="CheckBox5" runat="server" Text=" " />
            </td>
            <td class="auto-style14">
                <asp:CheckBox ID="CheckBox6" runat="server" Text=" " />
            </td>
            <td class="auto-style14">
                <asp:CheckBox ID="CheckBox7" runat="server" Text=" " />
            </td>
            <td class="auto-style14">
                <asp:CheckBox ID="CheckBox8" runat="server" Text=" " />
            </td>
            <td class="auto-style14">
                <asp:CheckBox ID="CheckBox9" runat="server" Text=" " />
            </td>
            <td class="auto-style18">
                <asp:CheckBox ID="CheckBox10" runat="server" Text=" " />
            </td>
        </tr>
        <tr>
            <td class="auto-style5">2</td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox11" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox12" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox13" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox14" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox15" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox16" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox17" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox18" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox19" runat="server" Text=" " />
            </td>
            <td class="auto-style27">
                <asp:CheckBox ID="CheckBox20" runat="server" Text=" " />
            </td>
        </tr>
        <tr>
            <td class="auto-style5">3</td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox21" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox22" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox23" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox24" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox25" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox26" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox27" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox28" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox29" runat="server" Text=" " />
            </td>
            <td class="auto-style27">
                <asp:CheckBox ID="CheckBox30" runat="server" Text=" " />
            </td>
        </tr>
        <tr>
            <td class="auto-style5">4</td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox31" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox32" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox33" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox34" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox35" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox36" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox37" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox38" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox39" runat="server" Text=" " />
            </td>
            <td class="auto-style27">
                <asp:CheckBox ID="CheckBox40" runat="server" Text=" " />
            </td>
        </tr>
        <tr>
            <td class="auto-style5">5</td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox41" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox42" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox43" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox44" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox45" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox46" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox47" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox48" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox49" runat="server" Text=" " />
            </td>
            <td class="auto-style27">
                <asp:CheckBox ID="CheckBox50" runat="server" Text=" " />
            </td>
        </tr>
        <tr>
            <td class="auto-style5">6</td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox51" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox52" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox53" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox54" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox55" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox56" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox57" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox58" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox59" runat="server" Text=" " />
            </td>
            <td class="auto-style27">
                <asp:CheckBox ID="CheckBox60" runat="server" Text=" " />
            </td>
        </tr>
        <tr>
            <td class="auto-style5">7</td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox61" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox62" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox63" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox64" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox65" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox66" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox67" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox68" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox69" runat="server" Text=" " />
            </td>
            <td class="auto-style27">
                <asp:CheckBox ID="CheckBox70" runat="server" Text=" " />
            </td>
        </tr>
        <tr>
            <td class="auto-style5">8</td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox71" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox72" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox73" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox74" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox75" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox76" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox77" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox78" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox79" runat="server" Text=" " />
            </td>
            <td class="auto-style27">
                <asp:CheckBox ID="CheckBox80" runat="server" Text=" " />
            </td>
        </tr>
        <tr>
            <td class="auto-style5">9</td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox81" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox82" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox83" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox84" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox85" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox86" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox87" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox88" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox89" runat="server" Text=" " />
            </td>
            <td class="auto-style27">
                <asp:CheckBox ID="CheckBox90" runat="server" Text=" " />
            </td>
        </tr>
        <tr>
            <td class="auto-style5">10</td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox91" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox92" runat="server" Text=" " />
            </td>
            <td class="auto-style26">
                <asp:CheckBox ID="CheckBox93" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox94" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox95" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox96" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox97" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox98" runat="server" Text=" " />
            </td>
            <td class="auto-style25">
                <asp:CheckBox ID="CheckBox99" runat="server" Text=" " />
            </td>
            <td class="auto-style27">
                <asp:CheckBox ID="CheckBox100" runat="server" Text=" " />
            </td>
        </tr>
    </table>
    <table class="auto-style28">
        <tr>
            <td class="auto-style10">
                <asp:Button ID="ButtonAddBoard" runat="server" Style="margin-left: 545px" Text="Add" Width="47px" Height="24px" />
                <asp:Button ID="ButtonBack" runat="server" OnClick="ButtonBack_Click" Style="margin-left: 15px" Text="Back To Menu" Height="24px" Width="115px" />
            </td>
        </tr>
    </table>
    <script src="JSAdmin.js"></script>
</asp:Content>
