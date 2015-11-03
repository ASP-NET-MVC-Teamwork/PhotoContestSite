var proxy = $.connection.notificationsHub;
proxy.client.receiveNotification = function (message) {
    console.log('received.');
    $("#notificationContainer").html(message);
    $("#notificationContainer").slideDown(2000);
    setTimeout('$("#notificationContainer").slideUp(2000);', 5000);
};

function sendNotification(reciever, message) {
    console.log("sent.");
    proxy.server.sendNotifications("zxzx", "Hello");
}

$.connection.hub.start();