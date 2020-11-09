CertificadosApp.controller('CertificadoController',
    ['$scope', '$rootScope', '$location', 'CertificadoService', '$routeParams', '$sce',
        function ($scope, $rootScope, $location, CertificadoService, $routeParams, $sce) {

            $("#MenuPrincipal").css("display", "none");
            $(".barra-menu").css("display", "none");

            $scope.Persona = {
                documento: ""
            };


            jQuery('.numbersOnly').keyup(function () {
                this.value = this.value.replace(/[^0-9\.]/g, '');
            });

            $scope.ConsultarCertificados = function () {
                if ($scope.Persona.documento == "") {
                    bootbox.alert({
                        message: "Ingrese documento válido",
                        locale: 'es',
                    });
                    return;
                }
                CertificadoService.ConsultarContratoxDocumento($scope.Persona, function (response) {
                    if (response.success == true) {
                        if (response.contratos.length == 0) {
                            bootbox.alert({
                                message: "No se encontraron certificados contractuales",
                                locale: 'es',
                            });
                        } else {
                            $("#div_table_certificados").show();
                            $scope.ListaContratos = response.contratos;
                            $scope.DatosPersona = response.persona;
                        }
                        
                    }
                });
            };

            //CertificadoService.ConsultarCdp(function (response) {
            //    console.log(response);
            //});

        }]);