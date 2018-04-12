//var name = "";
angular.module('chat', ['SignalR', 'ngRoute'])
    // Handles the events where users log in and log out
    .controller('Login', ['signalRService', '$scope', function (signalRService, $scope) {
        this.loggedIn = false;
        this.scope = $scope;
        this.scope.name = "";
        this.signalRService = signalRService;
        var that = this;
        this.logIn = function () {
            var element = document.getElementById('name');
            var get = document.getElementById("pass");
            var name = element.value;
            var password = get.value;
            if (name == "admin" && password == "123") {
                alert("successfully loged in")
                signalRService.goOnline(name); // allows all clients to see you went online
                signalRService.broadcastMessage("server", "*" + name + " has entered the room*");
                that.scope.name = name;
                that.loggedIn = true;
                element.value = "";
            }
            else {
                $scope.error = "Invalid login details";
            }

        };
      
        this.logOut = function () {
            that.loggedIn = false;
            signalRService.goOffline(that.scope.name); // allows all clients to see you went offline
            signalRService.broadcastMessage("server", "*" + that.scope.name + " has left the room*");
        }
    }])
    // Handles the event where user enter a message in the chat room
    .controller('EnterMessage', ['signalRService', '$scope', function (signalRService, $scope) {
        this.message = "";
        this.enterText = function () {
            var text = document.getElementById('text');
            // only broadcast if text is not empty
            if (text.value !== "") {
                signalRService.broadcastMessage($scope.name, text.value); // broadcast that message to all other Clients
                text.value = "";
            }
        };
    }]);