chessControllers.controller('LoginCtrl', ['$scope', 'Login', function ($scope, Login) {
    $scope.fm = new AuthenticationFM();
    $scope.login = function () {
        Login.save($scope.fm);
    }
}]);