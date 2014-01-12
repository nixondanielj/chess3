
var chessApp = angular.module("chessApp", []);
chessApp.controller('LoginController', ["scope", function ($scope) {
    $scope.AuthenticationFM = new AuthenticationFM();
}]);