CertificadosApp.factory('UsuarioService',
    ['$http', '$rootScope', '$routeParams',
        function ($http, $rootScope, $routeParams) {
            var service = {};

            service.ConsultarUsuarios = function (callback) {
                $http.get(URLServices + "Usuario/ConsultarUsuarios/")
                    .success(function (response) {
                        callback(response);
                    });
            };

            return service;

        }]);