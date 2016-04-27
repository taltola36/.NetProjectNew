var GuID;

function newXmlHttpReq() {
    try {
        xmlHttp_loadBoard = new ActiveXObject("Microsoft.XMLHTTP");
        xmlHttp_Register = new ActiveXObject("Microsoft.XMLHTTP");
        xmlHttp_Process = new ActiveXObject("Microsoft.XMLHTTP");
    }
    catch (e) {
        try {
            xmlHttp_loadBoard = new XMLHttpRequest();
            xmlHttp_Register = new XMLHttpRequest();
            xmlHttp_Process = new XMLHttpRequest();
        }
        catch (e) {
            alert("Error");
        }
    }    
}


function LoadBoard() {
    window.resizeTo(200, 200);
    newXmlHttpReq();
    document.bgColor = "rgb(255,255,255)";
    var url = "Handler.ashx?cmd=loadBoard";
    xmlHttp_loadBoard.open("POST", url, true);
    xmlHttp_loadBoard.onreadystatechange = onRequestLoadBoard;
    xmlHttp_loadBoard.send();
}

function onRequestLoadBoard() {
    if (xmlHttp_loadBoard.readyState == 4) {
        //var myJSON_Text = xmlHttp_Process.responseText;
        //myJSON_boardArr = eval("(" + myJSON_Text + ")");
        //initBoard();
        register();
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
    xmlHttp_loadBoard.open("POST", url, true);
    xmlHttp_loadBoard.send();
}

function buttonClick(sender) {
    document.getElementById(sender.id).style.background = "rgb(25,100,175)";
}

function canMoveClickedButton() {
    if (xmlHttp_Process.readyState == 4) {
    }
}

window.onload = LoadBoard;
window.onbeforeunload = myUnLoad;
