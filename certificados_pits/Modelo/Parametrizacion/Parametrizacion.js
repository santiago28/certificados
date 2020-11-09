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


            $("#but_upload").click(function () {
                var fd = new FormData();
                var files = $('#file')[0].files[0];
                fd.append('file', files);

                if (fd != null) {
                    $("#cargando").css("display", "block");
                    $('#but_upload').attr("disabled", true);
                    ParametrizacionService.SubirExcel(fd, function (response) {
                        if (response.success == true) {
                            var datos = [];
                            $.each(response.data, function (index, value) {

                                datos.push({
                                    Nun_contrato: value[0], ano: value[1], documento: value[2], nombre: value[3], dato_malo: value[4]
                                });

                            });
                            if (datos.length>0) {
                                alasql('SELECT * INTO XLSX("Registros con problemas.xlsx",{headers:true}) FROM ?', [datos]);
                            }
                            $("#cargando").css("display", "none");
                            $('#but_upload').attr("disabled", false);
                            bootbox.alert({
                                message: "Carga de base de datos exitosa",
                                locale: 'es',
                            });
                        }
                        else {
                            bootbox.alert({
                                message: "Error al cargar el archivo",
                                locale: 'es',
                            });
                        }
                    });
                }

                //$.ajax({
                //    url: "/api/File/UploadFile",
                //    type: 'post',
                //    data: fd,
                //    contentType: false,
                //    processData: false,
                //    success: function (response) {
                //        if (response.success == true) {

                //            var datos = [];
                //            $.each(response.data, function (index, value) {

                //                datos.push({
                //                    Nun_contrato: value[0], ano: value[1], documento: value[2], nombre: value[3], dato_malo: value[4]
                //                });


                //            });
                //            alasql('SELECT * INTO XLSX("Reporte Decomisos.xlsx",{headers:true}) FROM ?', [datos]);
                //        }
                //        else {
                //            alert('file not uploaded');
                //        }
                //    },
                //});
            });



        }]);