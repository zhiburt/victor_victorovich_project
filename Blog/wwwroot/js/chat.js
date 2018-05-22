$(function(){
    $(".chat").niceScroll();
}) 


// The following sample code uses modern ECMAScript 6 features 
// that aren't supported in Internet Explorer 11.
// To convert the sample for environments that do not support ECMAScript 6, 
// such as Internet Explorer 11, use a transpiler such as 
// Babel at http://babeljs.io/. 
//
// See Es5-chat.js for a Babel transpiled version of the following code:

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

connection.on("ReceiveMessage", (user, message) => { 
    const encodedMsg = user + " says " + message;
    var div = document.createElement('div');
    div.className = "";
    div.innerHTML = '\
        <div class="answer left">\
        <div class="avatar">\
        <img src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="User name">\
        <div class="status offline"></div>\
        </div>\
        <div class="name">'+(user)+'</div>\
        <div class="text">'+
        message + 
        '</div>\
        <div class="time">5 min ago</div>\
    </div>';
    document.getElementById("chat-body").appendChild(div);
});

connection.on("Joined", (user) => { 
    const encodedMsg = user;
    var div = document.createElement('div');
    div.className = "";
    div.innerHTML = '\
        <div class="answer left">\
        <div class="avatar">\
        <img src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="User name">\
        <div class="status offline"></div>\
        </div>\
        <div class="name">'+(user)+'</div>\
        <div class="text">'+
        user + 'joined'
        '</div>\
        <div class="time">5 min ago</div>\
    </div>';
    document.getElementById("chat-body").appendChild(div);
});

document.getElementById("sendButton").addEventListener("click", event => {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;    
    connection.invoke("SendMessage", message).catch(err => console.error(err.toString()));
    event.preventDefault();
});

document.getElementById("joinButton").addEventListener("click", event => {
    const roomName = document.getElementById("roomNameInput").value;    
    connection.invoke("JoinRoom", roomName).catch(err => console.error(err.toString()));
    event.preventDefault();
});


connection.start().catch(err => console.error(err.toString()));