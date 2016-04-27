<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Battleship Game</title>
</head>
<body>
    <form id="form1" runat="server">
    </form>
</body>
<script>
    var GuID;
    var size = 10;
    var boardArr;

    function setWindow() {
        window.moveTo(0, 0);
        window.resizeBy(0, 0);
        window.resizeTo(1050, 900);
        document.bgColor = "rgb(255,255,255)";
        newXmlHttpReq();
        register();
    }

    function newXmlHttpReq() {
        try {
            xmlHttp_loadBoard = new ActiveXObject("Microsoft.XMLHTTP");
            xmlHttp_Register = new ActiveXObject("Microsoft.XMLHTTP");
            xmlHttp_Process = new ActiveXObject("Microsoft.XMLHTTP");
            xmlHttp_Unload = new ActiveXObject("Microsoft.XMLHTTP");
        }
        catch (e) {
            try {
                xmlHttp_loadBoard = new XMLHttpRequest();
                xmlHttp_Register = new XMLHttpRequest();
                xmlHttp_Process = new XMLHttpRequest();
                xmlHttp_Unload = new XMLHttpRequest();
            }
            catch (e) {
                alert("Error");
            }
        }
    }

    function register() {
        var url = "Handler.ashx?cmd=register";
        xmlHttp_Register.open("POST", url, true);
        xmlHttp_Register.onreadystatechange = getResponse_Connect;
        xmlHttp_Register.send();
    }

    function getResponse_Connect() {
        if (xmlHttp_Register.readyState == 4) {
            GuID = xmlHttp_Register.responseText;
            ProcessFunction();
            loadBoard();
        }
    }

    function loadBoard() {
        var url = "Handler.ashx?cmd=loadBoard&guid=" + GuID;
        xmlHttp_loadBoard.open("POST", url, true);
        xmlHttp_loadBoard.onreadystatechange = onRequestLoadBoard;
        xmlHttp_loadBoard.send();
    }

    function onRequestLoadBoard() {
        if (xmlHttp_loadBoard.readyState == 4) {
            var myJSON_Text = xmlHttp_loadBoard.responseText;
            var message = eval(myJSON_Text.substr(myJSON_Text.length - 6));
            boardArr = eval(myJSON_Text.substr(0, myJSON_Text.length - 6));
            initBoard(message);
            ProcessFunction();
        }
    }

    function initBoard(message) {
        k = 0;
        for (var i = 0; i < size; i++) {
            for (var j = 0; j < size; j++) {
                var button = document.getElementById((k + size * size).toString());
                if (boardArr[i][j] == 1)
                    button.style.backgroundColor = "rgb(50,100,100)";
                k++;
            }
        }

        if (message == "wait") {
            document.getElementById("playerNameLabel").textContent = "First Player";
            alert("Please wait for another player to join the game");
        }
        else {
            document.getElementById("playerNameLabel").textContent = "Second Player";
            alert("Play!");
        }
    }

    function ProcessFunction() {
        var url = "Handler.ashx?cmd=process&guid=" + GuID;
        xmlHttp_Process.open("POST", url, true);
        xmlHttp_Process.onreadystatechange = canMoveClickedButton;
        xmlHttp_Process.send();
    }

    function myUnLoad() {
        var url = "Handler.ashx?cmd=unregister&guid=" + GuID;
        xmlHttp_Unload.open("POST", url, true);
        xmlHttp_Unload.send();
    }

    function newGameClick() {
        clearBoards();
        loadBoard();
    }

    function clearBoards() {
        var button;
        for (var i = 0; i < size * size; i++) {
            button = document.getElementById(i + size * size);
            button.style.backgroundColor = "";
            button = document.getElementById(i);
            button.style.backgroundColor = "";
        }
    }

    function buttonClick(sender) {
        //document.getElementById(sender.id).style.background = "rgb(25,100,175)";
    }

    function canMoveClickedButton() {
        if (xmlHttp_Process.readyState == 4) {
        }
    }

    window.onload = setWindow;
    window.onbeforeunload = myUnLoad;

</script>
</html>
