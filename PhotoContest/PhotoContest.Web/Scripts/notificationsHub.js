$(function () {
    var proxy = $.connection.notificationsHub;
    proxy.client.receiveNotification = function (message) {
        $("#notificationContainer").html(message);
        $("#notificationContainer").slideDown(2000);
        setTimeout('$("#notificationContainer").slideUp(2000);', 5000);
    };
    $("#sendNotification").click(function () {
        console.log("dada");
        proxy.server.sendNotifications("Hello");
    });
    $.connection.hub.start();
});