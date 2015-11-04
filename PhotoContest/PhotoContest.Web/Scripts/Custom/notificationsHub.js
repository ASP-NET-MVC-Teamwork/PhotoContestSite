var proxy = $.connection.notificationsHub;
var notificationsCount = 0;
    
getNotifications();


function updateNotifications(data) {
    return $("#notificationContainer span").html(data);
}

function getNotifications() {
        $.ajax({
            type: "Get",
            url: "http://localhost:11279/Notifications/GetNotificationsCount",
            success: function (data) {
                notificationsCount = data;
                updateNotifications(data);
                return data;
            }
        });
}

proxy.client.receiveNotification = function () {
    console.log('received.');
    updateNotifications(notificationsCount + 1);
};

function sendNotification(username, recieverId, contestId) {

    $.ajax({
        type: "POST",
        url: "http://localhost:11279/Notifications/Create",
        data: {
            RecieverId: recieverId,
            ContestId: contestId
        }
    }); 
    console.log("sent.");
    proxy.server.sendNotifications(username);
}
$.connection.hub.start();

$("#notificationsButton").click(function() {
    $("#notificationsButton").trigger('submit');
});

function markAsSeen(notificationId) {
    console.log("begin");
    $.ajax({
        type: "POST",
        url: "http://localhost:11279/Notifications/Update",
        data: {
            notificationId: notificationId
        }
    });
}