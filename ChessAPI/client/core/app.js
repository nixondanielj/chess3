var chessApp = angular.module('chessApp', ['ngRoute', 'chessControllers']);

chessApp.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
            .when('/login', {
                templateUrl: 'partials/login.html',
                controller: 'loginCtrl'
            })
            .when('/chess', {
                templateUrl: 'partials/chess.html',
                controller: 'chessCtrl'
            })
            .otherwise({
                redirectTo: '/chess'
            });
    }
]);