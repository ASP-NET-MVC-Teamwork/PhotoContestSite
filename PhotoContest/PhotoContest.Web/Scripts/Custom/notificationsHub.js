var proxy = $.connection.notificationsHub;
var notificationsCount = 0;
proxy.client.receiveNotification = function () {
    notificationsCount++;
    console.log('received.');
    $("#notificationContainer span").html(notificationsCount);
};

function sendNotification(username) {
    console.log("sent.");
    proxy.server.sendNotifications(username);
}

$.connection.hub.start();