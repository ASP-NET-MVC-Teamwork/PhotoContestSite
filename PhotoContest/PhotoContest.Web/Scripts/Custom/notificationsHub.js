$(function () {
    var proxy = $.connection.notificationsHub;
    proxy.client.receiveNotification = function (message) {
        $("#notificationContainer").html(message);
        $("#notificationContainer").slideDown(2000);
        setTimeout('$("#notificationContainer").slideUp(2000);', 5000);
    };
    $("#sendNotification").click(function () {
        proxy.server.sendNotifications("zxzx","Hello");
    });
    $.connection.hub.start();
});