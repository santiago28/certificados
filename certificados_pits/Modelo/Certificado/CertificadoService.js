CertificadosApp.factory('CertificadoService',
    ['$http', '$rootScope', '$routeParams',
        function ($http, $rootScope, $routeParams) {
            var service = {};

            service.ConsultarContratoxDocumento = function (Persona, callback) {
                $http.post(URLServices + "Certificado/ConsultarContratoxDocumento/", Persona)
                    .success(function (response) {
                        callback(response);
                    });
            };

            service.ConsultarCdp = function (callback) {
                $http.get(URLServices + "Ampliacion/ConsultarCdp/")
                    .success(function (response) {
                        callback(response);
                    });
            };


            return service;

        }]);