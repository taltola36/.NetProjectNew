
var GuID;
var size = 10;
var boardArr, myJSON_Text, player, message;
var xmlHttp_loadBoard, xmlHttp_Register, xmlHttp_Process, xmlHttp_Unload;

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
        myJSON_Text = xmlHttp_Register.responseText;
        GuID = myJSON_Text.substr(0, myJSON_Text.length - 6);
        message = eval(myJSON_Text.substr(myJSON_Text.length - 6));
        setAlert();
        if (message == "wait")
            ProcessFunction();
    }
}

function setAlert() {
    if (message == "wait") {
        document.getElementById("playerNameLabel").textContent = "First Player";
        player = 1;
        alert("Please wait for another player to join the game");
    }
    if (message == "play") {
        document.getElementById("playerNameLabel").textContent = "Second Player";
        player = 2;
        alert("Play!");
        loadBoard();
    }
}

function loadBoard() {
    if (xmlHttp_Process.readyState == 4 || player == 2) {
        if (player == 1)
            alert("Play!");

        var url = "Handler.ashx?cmd=loadBoard&playerId=" + GuID;
        xmlHttp_loadBoard.open("POST", url, true);
        xmlHttp_loadBoard.onreadystatechange = onRequestLoadBoard;
        xmlHttp_loadBoard.send();
    }
}

function onRequestLoadBoard() {
    if (xmlHttp_loadBoard.readyState == 4) {
        var myJSON_Text = xmlHttp_loadBoard.responseText;
        boardArr = eval(myJSON_Text);
        initBoard();
    }
}

function initBoard() {
    k = 0;
    for (var i = 0; i < size; i++) {
        for (var j = 0; j < size; j++) {
            var button = document.getElementById((k + size * size).toString());
            if (boardArr[i][j] == 1)
                button.style.backgroundColor = "rgb(50,100,100)";
            k++;
        }
    }
}

function ProcessFunction() {
    var url = "Handler.ashx?cmd=process&playerId=" + GuID;
    xmlHttp_Process.open("POST", url, true);
    xmlHttp_Process.onreadystatechange = canMoveClickedButton;
    xmlHttp_Process.send();
}

function myUnLoad() {
    var url = "Handler.ashx?cmd=unregister&playerId=" + GuID;
    xmlHttp_Unload.open("POST", url, true);
    //xmlHttp_Unload.onreadystatechange = unregisterResponse; ~~~~~~~~~DO NOT DELETE~~~~~~~~~~
    xmlHttp_Unload.send();
}

//function unregisterResponse() {
//    if (xmlHttp_Unload.readyState == 4) {
//        myJSON_Text = xmlHttp_Unload.responseText;
//        var messageEndGame = eval(myJSON_Text);
//        alert(messageEndGame);
//    }
//}

function newGameClick() {
    clearBoards();
    if (boardArr == null) {
        setAlert();
    } else {
        var url = "Handler.ashx?cmd=loadBoard&playerId=" + GuID;
        //alert(xmlHttp_Process.readyState);
        xmlHttp_loadBoard.open("POST", url, true);
        xmlHttp_loadBoard.onreadystatechange = onRequestLoadBoard;
        xmlHttp_loadBoard.send();
    }
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

function guessShipLocation() {
    if (xmlHttp_Process.readyState == 4) {
    }
}

window.onload = setWindow;
window.onbeforeunload = myUnLoad;
