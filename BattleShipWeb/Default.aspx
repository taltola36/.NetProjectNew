<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Battleship Game</title>
<script>
    var GuID;
    var size = 10;
    var boardArr, myJSON_Text, player, senderButton, pairNumber, numberOfSubmarinesLeft, numberOfAllSubmarines, gameOver = false, wait, firstToBegin;
    var xmlHttp_loadBoard, xmlHttp_Register, xmlHttp_ProcessLoadBoard, xmlHttp_changeBoard, xmlHttp_ProcessClose, xmlHttp_Unload, xmlHttp_MakeMove, xmlHttp_MakeMoveProcess;
    var username = '<%=Session["UserName"]%>';
    var winOrClose = "";

    function setWindow() {
        window.moveTo(0, 0);
        window.resizeBy(0, 0);
        window.resizeTo(1100, 1000);
        document.bgColor = "rgb(255,255,255)";
        newXmlHttpReq();
        register();
    }

    function newXmlHttpReq() {
        try {
            xmlHttp_loadBoard = new ActiveXObject("Microsoft.XMLHTTP");
            xmlHttp_Register = new ActiveXObject("Microsoft.XMLHTTP");
            xmlHttp_ProcessLoadBoard = new ActiveXObject("Microsoft.XMLHTTP");
            xmlHttp_ProcessClose = new ActiveXObject("Microsoft.XMLHTTP");
            xmlHttp_Unload = new ActiveXObject("Microsoft.XMLHTTP");
            xmlHttp_MakeMoveProcess = new ActiveXObject("Microsoft.XMLHTTP");
            xmlHttp_MakeMove = new ActiveXObject("Microsoft.XMLHTTP");
            xmlHttp_changeBoard = new ActiveXObject("Microsoft.XMLHTTP");
        }
        catch (e) {
            try {
                xmlHttp_loadBoard = new XMLHttpRequest();
                xmlHttp_Register = new XMLHttpRequest();
                xmlHttp_changeBoard = new XMLHttpRequest();
                xmlHttp_ProcessLoadBoard = new XMLHttpRequest();
                xmlHttp_ProcessClose = new XMLHttpRequest();
                xmlHttp_Unload = new XMLHttpRequest();
                xmlHttp_MakeMove = new XMLHttpRequest();
                xmlHttp_MakeMoveProcess = new ActiveXObject("Microsoft.XMLHTTP");
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
            myJSON_Text = xmlHttp_Register.responseText;
            var result = eval("(" + myJSON_Text + ")");
            GuID = result.indexes;
            pairNumber = result.isHit;
            player = result.boardName;
            loadBoard("load");
        }
    }

    function loadBoard(loadOrChange) {
        if (player == "firstPlayer")
            var url = "Handler.ashx?cmd=loadBoard&playerId=" + GuID + "&playerNumber=1&loadOrChange=" + loadOrChange + "&username=" + username;
        if (player == "secondPlayer")
            var url = "Handler.ashx?cmd=loadBoard&playerId=" + GuID + "&playerNumber=2&loadOrChange=" + loadOrChange + "&username=" + username;

        xmlHttp_loadBoard.open("POST", url, true);
        xmlHttp_loadBoard.onreadystatechange = onRequestLoadBoard;
        xmlHttp_loadBoard.send();
    }

    function onRequestLoadBoard() {
        if (xmlHttp_loadBoard.readyState == 4) {
            var result = eval("(" + xmlHttp_loadBoard.responseText + ")");
            boardArr = result.boardArr;
            numberOfSubmarinesLeft = result.numberOfSubmarinesLeft;
            numberOfAllSubmarines = result.numberOfAllSubmarines;
            wait = result.wait;
            firstToBegin = result.firstToBegin;
            initBoard();
            setRemainingSubs();
            setLabels();

            if (!gameOver)
                ProcessFunctionClose();
            if (wait)
                ProcessFunctionLoadBoard();
            else {
                ProcessFunctionMakeMove();
                gameOver = false;
            }
        }
    }

    function initBoard() {
        for (var i = 0; i < size; i++) {
            for (var j = 0; j < size; j++) {
                var button1 = document.getElementById((i * size + j + size * size).toString());
                var button2 = document.getElementById((i * size + j).toString());

                if ((firstToBegin == 0 && player == "firstPlayer") || (firstToBegin == 1 && player == "secondPlayer"))
                    button2.disabled = wait;
                else
                    button2.disabled = false;

                if (boardArr[i][j] == 1)
                    button1.style.backgroundColor = "rgb(50,100,100)";
            }
        }
    }

    function setRemainingSubs() {
        if (numberOfSubmarinesLeft == "" || numberOfAllSubmarines == "")
            document.getElementById("SubsNumber").textContent = document.getElementById("SubsNumber").style.backgroundColor = "";

        else {
            document.getElementById("SubsNumber").textContent = "Submarines left: " + numberOfSubmarinesLeft + "/" + numberOfAllSubmarines;
            if (player == "firstPlayer")
                document.getElementById("SubsNumber").style.backgroundColor = "orange";
            if (player == "secondPlayer")
                document.getElementById("SubsNumber").style.backgroundColor = "green";
        }
    }

    function setLabels() {
        document.getElementById("pairNameLabel").textContent = "Pair Number " + pairNumber;
        document.getElementById("playerNameLabel").textContent = username;

        if (player == "firstPlayer") {
            document.getElementById("playerNameLabel").style.backgroundColor = "orange";
            document.getElementById("pairNameLabel").style.backgroundColor = "orange";
        }

        if (player == "secondPlayer") {
            document.getElementById("playerNameLabel").style.backgroundColor = "green";
            document.getElementById("pairNameLabel").style.backgroundColor = "green";
        }
        alert(firstToBegin + " " + wait + " " + gameOver + " " + winOrClose);

        if ((firstToBegin == 1 && player == "secondPlayer") || (firstToBegin == 0 && player == "firstPlayer")) {
            if (wait && gameOver) {
                if (winOrClose == "close")
                    setMessage("The other player left. Please wait while another player joins the game", "red", "300px");
                if (winOrClose == "win")
                    setMessage("Please wait while another player joins the game", "red", "350px");
            }
        }

        if (!gameOver)
            setMessage("Please wait while another player joins the game", "red", "350px");

        if (firstToBegin == 0)
            if ((player == "firstPlayer" && !wait) || (player == "secondPlayer" && !wait))
                setMessage("Please wait while the other player makes a move", "red", "350px");

        if (firstToBegin == 1)
            if (player == "firstPlayer" || (player == "secondPlayer" && !wait))
                setMessage("Please wait while the other player makes a move", "red", "350px");
    }

    function setMessage(textContent, color, left) {
        document.getElementById("messages").style.color = color;
        document.getElementById("messages").style.left = left;
        document.getElementById("messages").textContent = textContent;
        document.getElementById("messages").style.fontWeight = "bold";
        document.getElementById("messages").style.fontSize = "20px";
    }

    function ProcessFunctionLoadBoard() {
        var url = "Handler.ashx?cmd=process1&playerId=" + GuID;
        xmlHttp_ProcessLoadBoard.open("POST", url, true);
        xmlHttp_ProcessLoadBoard.onreadystatechange = continueFirstPlayer;
        xmlHttp_ProcessLoadBoard.send();
    }

    function continueFirstPlayer() { //occurs when second player connects
        if (xmlHttp_ProcessLoadBoard.readyState == 4 && xmlHttp_ProcessLoadBoard.responseText != "") {
            var result = eval("(" + xmlHttp_ProcessLoadBoard.responseText + ")");
            numberOfSubmarinesLeft = result.numberOfSubmarinesLeft;
            numberOfAllSubmarines = result.numberOfAllSubmarines;
            setRemainingSubs();

            for (var i = 0; i < size; i++) {
                for (var j = 0; j < size; j++) {
                    var button2 = document.getElementById((i * size + j).toString());
                    button2.disabled = false;
                }
            }
            ProcessFunctionMakeMove();
            setMessage("You can play now", "red", "450px");
            gameOver = false;
        }
    }

    function ProcessFunctionClose() {
        var url = "Handler.ashx?cmd=process2&playerId=" + GuID;
        xmlHttp_ProcessClose.open("POST", url, true);
        xmlHttp_ProcessClose.onreadystatechange = myUnLoad;
        xmlHttp_ProcessClose.send();
    }

    function myUnLoad() {
        if (xmlHttp_ProcessClose.readyState == 4) {
            winOrClose = "close";
            ProcessFunctionClose();
            NewGame();
        }
    }

    function NewGame() {
        gameOver = true;
        clearBoards();
        loadBoard("change");
    }

    function ProcessFunctionMakeMove() {
        var url = "Handler.ashx?cmd=process1&playerId=" + GuID;
        xmlHttp_MakeMoveProcess.open("POST", url, true);
        xmlHttp_MakeMoveProcess.onreadystatechange = makeMoveResponse;
        xmlHttp_MakeMoveProcess.send();
    }

    function leaveGameClick() {
        GameOver();
    }

    function GameOver() {
        removeLabels();
        clearBoards();
        windowClosed();
    }

    function removeLabels() {
        document.getElementById("pairNameLabel").textContent = "";
        document.getElementById("pairNameLabel").style.backgroundColor = "";
        document.getElementById("SubsNumber").textContent = "";
        document.getElementById("SubsNumber").style.backgroundColor = "";
    }

    function clearBoards() {
        for (var i = 0; i < size; i++) {
            for (var j = 0; j < size; j++) {
                var button1 = document.getElementById((i * size + j + size * size).toString());
                var button2 = document.getElementById((i * size + j).toString());
                button1.style.backgroundColor = "";
                button2.style.backgroundColor = "";
                button2.disabled = true;
            }
        }
    }

    function windowClosed() {
        var url = "Handler.ashx?cmd=unregister&playerId=" + GuID;
        xmlHttp_Unload.open("POST", url, true);
        xmlHttp_Unload.send();
        setMessage("Game over!", "red", "470px");
    }

    function removeMessages() {
        document.getElementById("messages").textContent = null;
        document.getElementById("messages").style.backgroundColor = null;
    }

    function buttonClick(sender) {
        if (document.getElementById(sender.id).style.backgroundColor != "red"
            && document.getElementById(sender.id).style.backgroundColor != "green"
            && !gameOver) {
            senderButton = sender;
            var url = "Handler.ashx?cmd=makeMove&playerId=" + GuID + "&indexes=" + senderButton.id;
            xmlHttp_MakeMove.open("POST", url, true);
            xmlHttp_MakeMove.send();
        }
    }

    function makeMoveResponse() {
        if (xmlHttp_MakeMoveProcess.readyState == 4 && xmlHttp_MakeMoveProcess.responseText != "") {
            winOrClose = "";
            myJSON_Text = xmlHttp_MakeMoveProcess.responseText;
            var result = eval("(" + myJSON_Text + ")");

            if (myJSON_Text.substr(1, 6) != "Please") {
                removeMessages();

                if (result.boardName == "w") {
                    gameOver = true;
                    if (result.isHit == "Game over! You won")
                        document.getElementById(result.indexes).style.backgroundColor = "green";
                    if (result.isHit == "Game over! You lost")
                        document.getElementById((size * size + Number(result.indexes)).toString()).style.backgroundColor = "green";
                    EndGame(result.isHit);
                }

                if (result.isHit == "1" && result.boardName == "r") {
                    document.getElementById(result.indexes).style.backgroundColor = "green";
                    document.getElementById("SubsNumber").textContent = "Submarines left: " + result.submarinesLeft + "/" + numberOfAllSubmarines;
                }

                if (result.isHit == "0" && result.boardName == "r") {
                    document.getElementById(result.indexes).style.backgroundColor = "red";
                    document.getElementById("SubsNumber").textContent = "Submarines left: " + result.submarinesLeft + "/" + numberOfAllSubmarines;
                }

                if (result.isHit == "1" && result.boardName == "l") {
                    document.getElementById((size * size + Number(result.indexes)).toString()).style.backgroundColor = "green";
                }

                if (result.isHit == "0" && result.boardName == "l") {
                    document.getElementById((size * size + Number(result.indexes)).toString()).style.backgroundColor = "red";
                }
            } 
            else {
                if (result == "Please wait while another player joins the game") {
                    winOrClose = "close";
                    NewGame();
                } else {
                    setMessage("", "red", "450px");
                    setMessage(result.toString(), "red", "450px");
                }
            }
        }
        if (xmlHttp_MakeMoveProcess.readyState == 4 && !gameOver)
            ProcessFunctionMakeMove();
    }

    function EndGame(message) {
        if (confirm(message + ". New game?")) {
            if (winOrClose != "close")
                winOrClose = "win";
            NewGame();
        }
        else
            GameOver();
    }

    function checkKeyCode(e) {
        var keyCode;
        if (window.event) {
            keyCode = window.event.keyCode;
        } else {
            if (e)
                keyCode = e.which;
        }
        if (keyCode == 116 || (e.ctrlKey && keyCode == 82) || keyCode == 8) {
            if (e.preventDefault) {
                e.preventDefault();
                e.stopPropagation();
            }
            window.event.keyCode = 0;
            window.status = "Refresh is disabled";
        }
    }

    window.onload = setWindow;
    window.onbeforeunload = windowClosed;
    window.document.onkeydown = checkKeyCode;

</script>
</head>

    <body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="refreshed" runat="server" />        
    </form>
</body>
</html>

