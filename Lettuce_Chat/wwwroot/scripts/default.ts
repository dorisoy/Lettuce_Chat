﻿namespace Lettuce.Default {
    export function ToggleMainMenu(e) {
        
    }
    export function DragOverChat(e) {
        e.preventDefault();
        e.dataTransfer.dropEffect = "copy";
    }

    export function DropOnChat(e) {
        e.preventDefault();
        if (e.dataTransfer.files.length < 1) {
            return;
        }
        if (Lettuce.Socket.readyState != WebSocket.OPEN) {
            return;
        }
        TransferFile(e.dataTransfer.files);
    }

    export function TransferFile(e) {
        for (var i = 0; i < e.length; i++) {
            dgi("divStatus").innerHTML = "Uploading file...";
            var file = e[i];
            var strPath = "/FileTransfer/Upload/";
            var fd = new FormData();
            fd.append('fileUpload', file);
            var xhr = new XMLHttpRequest();
            xhr.open('POST', strPath, true);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    dgi("divStatus").innerHTML = "Upload completed.";
                    var fileName = xhr.responseText;
                    var url = location.origin + "/FileTransfer/Download/?file=" + fileName;
                    (dgi("textInput") as HTMLTextAreaElement).value = 'File Sharing Link: <a target="_blank" href="' + url + '">' + fileName + '</a>';
                    SubmitMessage();
                }
                else {
                    
                    dgi("divStatus").innerHTML = "Upload failed.";
                    Lettuce.Utilities.ShowDialog("Upload Failed", "File upload failed.");
                }
            };
            xhr.send(fd);
        }
    }
    export function SubmitMessage() {
        var inputElem = (document.getElementById("textInput") as HTMLTextAreaElement);
        if (inputElem.value == "") {
            return;
        }
        var message = inputElem.value.replace("\n", "<br/>");
        inputElem.value = "";
        var divMessage = document.createElement("div");
        divMessage.classList.add("sent-chat");
        divMessage.innerHTML = '<div class="arrow-right"></div><div class="chat-message-header">You at ' + new Date().toLocaleTimeString() + "</div>" + message;
        var messageDiv = document.getElementById("divMessages");
        messageDiv.appendChild(divMessage);
        messageDiv.scrollTop = messageDiv.scrollHeight;
        var encoded = btoa(message);
        var request = {
            "Type": "ChatMessage",
            "Message": encoded
        };
        Lettuce.Socket.send(JSON.stringify(request));
    }
    export function InputKeyDown(e) {
        if (e.key.toLowerCase() == "enter" && e.shiftKey == false) {
            e.preventDefault();
            dgi("buttonSend").click();
        }
        ;
        var request = {
            "Type": "Typing"
        };
        Lettuce.Socket.send(JSON.stringify(request));
    }
}