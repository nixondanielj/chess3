var chessApp = angular.module('chessApp', ['ngRoute', 'chessControllers']);

chessApp.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
            .when('/chess', {
                templateUrl: 'partials/chess.html',
                controller: 'chessController'
            })
            .otherwise({
                redirectTo: '/chess'
            })
    }
]);