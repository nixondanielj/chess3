chessServices.factory('Login', ['$resource',
    function ($resource) {
        return $resource('Login', {}, {});
    }
])