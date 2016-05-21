<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Battleship Game</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="GuID" runat="server" />        
    </form>
</body>
<script>
    var GuID;
    var size = 10;
    var boardArr, myJSON_Text, player, senderButton, pairNumber, numberOfSubmarinesLeft, numberOfAllSubmarines, gameOver = false;
    var xmlHttp_loadBoard, xmlHttp_Register, xmlHttp_ProcessLoadBoard, xmlHttp_ProcessClose, xmlHttp_Unload, xmlHttp_MakeMove, xmlHttp_MakeMoveProcess;

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
        }
        catch (e) {
            try {
                xmlHttp_loadBoard = new XMLHttpRequest();
                xmlHttp_Register = new XMLHttpRequest();
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
            document.getElementById("GuID").value = GuID;
            sessionStorage.setItem("guid", GuID);
            pairNumber = result.isHit;
            player = result.boardName;
            loadBoard();
        }
    }

    function loadBoard() {
        if (player == "firstPlayer")
            var url = "Handler.ashx?cmd=loadBoard&playerId=" + GuID + "&playerNumber=1";
        if (player == "secondPlayer")
            var url = "Handler.ashx?cmd=loadBoard&playerId=" + GuID + "&playerNumber=2";

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
            initBoard();
            setLabels();
            //ProcessFunctionClose();

            if (player == "secondPlayer")
                ProcessFunctionMakeMove();

            if (player == "firstPlayer")
                ProcessFunctionLoadBoard();
        }
    }

    function initBoard() {
        for (var i = 0; i < size; i++) {
            for (var j = 0; j < size; j++) {
                var button1 = document.getElementById((i * size + j + size * size).toString());
                var button2 = document.getElementById((i * size + j).toString());
                if (player == "secondPlayer")       //firstPlayer buttons still disabled until continueFirstPlayer()
                    button2.disabled = false;
                if (boardArr[i][j] == 1)
                    button1.style.backgroundColor = "rgb(50,100,100)";
            }
        }
    }

    function continueFirstPlayer() { //occurs when second player connects
        if (xmlHttp_ProcessLoadBoard.readyState == 4) {
            for (var i = 0; i < size; i++) {
                for (var j = 0; j < size; j++) {
                    var button2 = document.getElementById((i * size + j).toString());
                    button2.disabled = false;
                }
            }
            ProcessFunctionMakeMove();
            setMessage("You can play now", "red", "450px");
        }
    }

    function setLabels() {
        if (player == "firstPlayer") {
            document.getElementById("playerNameLabel").textContent = "First Player";
            document.getElementById("pairNameLabel").textContent = "Pair Number " + pairNumber;
            document.getElementById("SubsNumber").textContent = "Submarines left: " + numberOfSubmarinesLeft +"/" + numberOfAllSubmarines;

            document.getElementById("playerNameLabel").style.backgroundColor = "orange";
            document.getElementById("pairNameLabel").style.backgroundColor = "orange";
            document.getElementById("SubsNumber").style.backgroundColor = "orange";
            alert("Please wait for another player to join the game");
        }
        if (player == "secondPlayer") {
            document.getElementById("playerNameLabel").textContent = "Second Player";
            document.getElementById("pairNameLabel").textContent = "Pair Number " + pairNumber;
            document.getElementById("SubsNumber").textContent = "Submarines left: " + numberOfSubmarinesLeft + "/" + numberOfAllSubmarines;
            document.getElementById("playerNameLabel").style.backgroundColor = "green";
            document.getElementById("pairNameLabel").style.backgroundColor = "green";
            document.getElementById("SubsNumber").style.backgroundColor = "green";
            setMessage("Please wait while the other player makes a move", "red", "370px");
        }
    }

    function setMessage(textContent, color, left) { //works better with requests than alerts
        document.getElementById("messages").style.color = color;
        document.getElementById("messages").style.left = left;
        document.getElementById("messages").textContent = textContent;
        //document.getElementById("messages").style.font.bold()
    }

    function ProcessFunctionLoadBoard() {
        var url = "Handler.ashx?cmd=process1&playerId=" + GuID;
        xmlHttp_ProcessLoadBoard.open("POST", url, true);
        xmlHttp_ProcessLoadBoard.onreadystatechange = continueFirstPlayer;
        xmlHttp_ProcessLoadBoard.send();
    }

    function ProcessFunctionClose() {
        var url = "Handler.ashx?cmd=process2&playerId=" + GuID;
        xmlHttp_ProcessClose.open("POST", url, true);
        xmlHttp_ProcessClose.onreadystatechange = myUnLoad;
        xmlHttp_ProcessClose.send();
    }

    function ProcessFunctionMakeMove() {
        var url = "Handler.ashx?cmd=process1&playerId=" + GuID;
        xmlHttp_MakeMoveProcess.open("POST", url, true);
        xmlHttp_MakeMoveProcess.onreadystatechange = makeMoveResponse;
        xmlHttp_MakeMoveProcess.send();
    }

    function myUnLoad() {
        if (xmlHttp_ProcessClose.readyState == 4) {
            myJSON_Text = xmlHttp_Unload.responseText;

            //never reaches this part. why???
        }
    }

    function windowClosed() {
        var url = "Handler.ashx?cmd=unregister&playerId=" + GuID;
        xmlHttp_Unload.open("POST", url, true);
        xmlHttp_Unload.send();
    }

    //function newBoardClick() {
    //    clearBoards();
    //    if (boardArr == null) {
    //        setLabels();
    //    } else {
    //        loadBoard();
    //    }
    //}

    function clearBoards() {
        var button;
        for (var i = 0; i < size * size; i++) {
            button = document.getElementById(i + size * size);
            button.style.backgroundColor = "";
            button = document.getElementById(i);
            button.style.backgroundColor = "";
        }
    }

    function locResult(indexes, boardName, isHit) {
        this.indexes = indexes;
        this.boardName = boardName;
        this.isHit = isHit;
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
        if (xmlHttp_MakeMoveProcess.readyState == 4) {
            myJSON_Text = xmlHttp_MakeMoveProcess.responseText;
            var result = eval("(" + myJSON_Text + ")");
            //result is either button info or a message to wait for player's turn

            if (myJSON_Text.substr(1, 6) != "Please" && result != "") {
                removeMessages();
                if (result.boardName == "w" && result.isHit == "Game over! You won") {
                    document.getElementById(result.indexes).style.backgroundColor = "green";
                    gameOver = true;
                    alert(result.isHit);
                }

                if (result.boardName == "w" && result.isHit == "Game over! You lost") {
                    document.getElementById((size * size + Number(result.indexes)).toString()).style.backgroundColor = "green";
                    gameOver = true;
                    alert(result.isHit);
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

            if (myJSON_Text.substr(1, 6) == "Please")
                alert(result);
            ProcessFunctionMakeMove();
        }
    }

    window.onload = setWindow;
    window.onclose = windowClosed;

</script>
</html>
