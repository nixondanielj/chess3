var chessApp = angular.module('chessApp', ['ngRoute', 'chessControllers']);

chessApp.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
            .when('/login', {
                templateUrl: 'client/partials/login.html',
                controller: 'LoginCtrl'
            })
            .when('/chess', {
                templateUrl: 'partials/chess.html',
                controller: 'ChessCtrl'
            })
            .otherwise({
                redirectTo: '/login'
            });
    }
]);