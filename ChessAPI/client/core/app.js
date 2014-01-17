var chessApp = angular.module('chessApp', ['ngRoute', 'chessControllers']);

chessApp.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
            .when('/chess', {
                templateUrl: 'holdon',
                controller: 'chessController'
            })
            .otherwise({
                redirectTo: '/chess'
            })
    }
]);