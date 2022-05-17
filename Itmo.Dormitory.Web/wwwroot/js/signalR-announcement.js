const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/dormitory")
    .build();

hubConnection.on('Notify', function (message) {
    launch_toast()
});

hubConnection.start();



function launch_toast() {
    var x = document.getElementById("toast")
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 5000);
}