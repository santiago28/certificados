CertificadosApp.factory('LoginService',
    ['$http', '$rootScope', '$routeParams',
        function ($http, $rootScope, $routeParams) {
            var service = {};

            service.IniciarSesion = function (Usuario, callback) {
                $http.post(URLServices + "Usuario/IniciarSesion", Usuario)
                    .success(function (response) {
                        callback(response);
                    });
            };

            return service;

        }]);