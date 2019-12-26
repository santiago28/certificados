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

            service.EditarUsuario = function (Usuario, callback) {
                $http.post(URLServices + "Usuario/EditarUsuario/", Usuario)
                    .success(function (response) {
                        callback(response);
                    });
            };

            service.GuardarUsuario = function (Usuario, callback) {
                $http.post(URLServices + "Usuario/GuardarUsuario/", Usuario)
                    .success(function (response) {
                        callback(response);
                    });
            };

            service.CambiarEstadoUsuario = function (id, callback) {
                Usuario = {
                    id: id
                };
                $http.post(URLServices + "Usuario/CambiarEstadoUsuario/", Usuario)
                    .success(function (response) {
                        callback(response);
                    });
            };

            return service;

        }]);