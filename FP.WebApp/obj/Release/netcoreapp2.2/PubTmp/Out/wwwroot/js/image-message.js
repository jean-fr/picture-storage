"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/imagehub").build();

connection.on("ReceiveMessage", function (imageUrl, imageTitle) {
    var title = imageTitle.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var $images = $("#image-list");
    var $title = $("<span />").html(title);
    var $image = $("<img />").attr("src", imageUrl);

    $images.append($("<div />").append($title).append($image));

    $("#title").val("");
    $("#image").val("");

});

connection.start().then(function () {
    console.log("SignalR Started");
}).catch(function (err) {
    return console.error(err.toString());
});

 