chessServices.factory('Login', ['$resource',
    function ($resource) {
        return $resource('api/Login', {}, {});
    }
]);