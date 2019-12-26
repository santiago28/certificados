CertificadosApp.controller('ParametrizacionController',
    ['$scope', '$rootScope', '$location', 'ParametrizacionService', '$routeParams', '$sce',
        function ($scope, $rootScope, $location, ParametrizacionService, $routeParams, $sce) {


            $scope.ImagenActual = "";

            ParametrizacionService.ConsultarParametros(function (response) {
                if (response.success) {
                    $scope.Parametros = response.parametros;
                }
            });

            $scope.GuardarParametros = function () {
                ParametrizacionService.GuardarParametros($scope.Parametros, function (response) {
                    if (response.success == true) {
                        bootbox.alert({
                            message: "Parametros guardado exitosamente",
                            locale: 'es',
                        });
                    }
                });
            }

            $scope.UploadFileWebImage = function (id) {
                $scope.ImagenActual = id;
                $("#fileUploadWebImage" + id).trigger('click');
            };


            $scope.CargarImagenServidor = function (id) {
                dataweb = new FormData();

                var files = $("#fileUploadWebImage" + id).get(0).files;

                dataweb.append("UploadedImage", files[0]);
                if (dataweb != null) {
                    ParametrizacionService.SubirImagen(dataweb, function (response) {
                        if (response.success == true) {
                            $.each($scope.Parametros, function (index, value) {
                                if (value.id == id) {
                                    $scope.Parametros[index].valor = response.path;
                                }
                            });
                            bootbox.alert({
                                message: "Imagen cargada exitosamente",
                                locale: 'es',
                            });
                        }
                    });
                }
            }


        }]);