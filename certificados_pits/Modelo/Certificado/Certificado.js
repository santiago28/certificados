CertificadosApp.controller('CertificadoController',
    ['$scope', '$rootScope', '$location', 'CertificadoService', '$routeParams', '$sce',
        function ($scope, $rootScope, $location, CertificadoService, $routeParams, $sce) {


            $scope.Persona = {
                documento: ""
            };

            $scope.ConsultarCertificados = function () {
                CertificadoService.ConsultarContratoxDocumento($scope.Persona, function (response) {
                    if (response.success == true) {
                        $("#div_table_certificados").show();
                        $scope.ListaContratos = response.contratos;
                        $scope.DatosPersona = response.persona;
                    }
                });
            };

            //CertificadoService.ConsultarCdp(function (response) {
            //    console.log(response);
            //});

        }]);