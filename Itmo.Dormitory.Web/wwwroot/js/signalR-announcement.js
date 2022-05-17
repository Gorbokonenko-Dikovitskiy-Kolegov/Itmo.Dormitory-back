const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/dormitory")
    .build();

hubConnection.on('Notify', function (message) {

    alert(message)

});

hubConnection.start();