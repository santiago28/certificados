CertificadosApp.controller('UsuarioController',
    ['$scope', '$rootScope', '$location', 'UsuarioService', '$routeParams', '$sce',
        function ($scope, $rootScope, $location, UsuarioService, $routeParams, $sce) {


            $scope.Usuario = {};

            $scope.curPage = 0;
            $scope.pageSize = 5;

            jQuery('.numbersOnly').keyup(function () {
                this.value = this.value.replace(/[^0-9\.]/g, '');
            });

            $scope.LimpiarCampos = function () {
                $scope.Usuario.id = "";
                $scope.Usuario.documento = "";
                $scope.Usuario.nombre = "";
                $scope.Usuario.correo_electronico = "";
                $scope.Usuario.telefono = "";
            }

            UsuarioService.ConsultarUsuarios(function (response) {
                if (response.success == true) {
                    $scope.datalists = response.usuarios;
                    $scope.numberOfPages = function () {
                        return Math.ceil($scope.datalists.length / $scope.pageSize);
                    };
                }
            });

            $scope.AbrirModal = function () {
                $scope.LimpiarCampos();
                $("#BtnRegistrar").show();
                $("#BtnEditar").hide();
                $("#ModalUsuarios").modal("show");
                $("#numero_documento").prop('disabled', false);
            }

            $scope.GuardarUsuario = function () {
                $scope.Usuario.id = null;
                if ($scope.Usuario.documento == "") {
                    bootbox.alert({
                        message: "Ingrese el número de documento de identidad",
                        locale: 'es',
                    });
                    return;
                }
                if ($scope.Usuario.nombre == "") {
                    bootbox.alert({
                        message: "Ingrese el nombre del usuario",
                        locale: 'es',
                    });
                    return;
                }

                var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
                if (regex.test($scope.Usuario.correo_electronico.trim())) {

                } else {
                    bootbox.alert({
                        message: "Ingrese un correo electrónico válido",
                        locale: 'es',
                    });
                    return;
                }
                UsuarioService.GuardarUsuario($scope.Usuario, function (response) {
                    if (response.success) {
                        bootbox.alert({
                            message: "Usuario registrado exitosamente",
                            locale: 'es',
                        });
                        $("#ModalUsuarios").modal("hide");
                        UsuarioService.ConsultarUsuarios(function (response) {
                            if (response.success == true) {
                                $scope.datalists = response.usuarios;
                                $scope.numberOfPages = function () {
                                    return Math.ceil($scope.datalists.length / $scope.pageSize);
                                };
                            }
                        });
                    }
                });
            }

            $scope.CambiarEstadoUsuario = function (id) {
                UsuarioService.CambiarEstadoUsuario(id, function (response) {
                    if (response.success) {
                        UsuarioService.ConsultarUsuarios(function (response) {
                            if (response.success == true) {
                                $scope.datalists = response.usuarios;
                                $scope.numberOfPages = function () {
                                    return Math.ceil($scope.datalists.length / $scope.pageSize);
                                };
                            }
                        });
                    }
                });
            }

            $scope.CargarDatosUsuario = function (usuario) {
                $scope.Usuario.id = usuario.id;
                $scope.Usuario.documento = usuario.documento;
                $scope.Usuario.nombre = usuario.nombre;
                $scope.Usuario.correo_electronico = usuario.correo_electronico;
                $scope.Usuario.telefono = usuario.telefono;
                $("#BtnRegistrar").hide();
                $("#BtnEditar").show();
                $("#numero_documento").prop('disabled', true);
                $("#ModalUsuarios").modal("show");

            }

            $scope.EditarUsuario = function () {
                if ($scope.Usuario.documento == "") {
                    bootbox.alert({
                        message: "Ingrese el número de documento de identidad",
                        locale: 'es',
                    });
                    return;
                }
                if ($scope.Usuario.nombre == "") {
                    bootbox.alert({
                        message: "Ingrese el nombre del usuario",
                        locale: 'es',
                    });
                    return;
                }

                var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
                if (regex.test($scope.Usuario.correo_electronico.trim())) {

                } else {
                    bootbox.alert({
                        message: "Ingrese un correo electrónico válido",
                        locale: 'es',
                    });
                    return;
                }
                UsuarioService.EditarUsuario($scope.Usuario, function (response) {
                    if (response.success) {
                        bootbox.alert({
                            message: "Usuario editado exitosamente",
                            locale: 'es',
                        });
                        $("#ModalUsuarios").modal("hide");
                        UsuarioService.ConsultarUsuarios(function (response) {
                            if (response.success == true) {
                                $scope.datalists = response.usuarios;
                                $scope.numberOfPages = function () {
                                    return Math.ceil($scope.datalists.length / $scope.pageSize);
                                };
                            }
                        });
                    }
                });
            }

            $scope.Filtrar = function () {
                var Busqueda = $("#Buscar").val();
                var exp = new RegExp(Busqueda);
                if (Busqueda == "") {
                    UsuarioService.ConsultarUsuarios(function (response) {
                        if (response.success == true) {
                            $scope.datalists = response.usuarios;
                            $scope.numberOfPages = function () {
                                return Math.ceil($scope.datalists.length / $scope.pageSize);
                            };
                        }
                    });
                }
                var Usuario = [];
                Usuario = $scope.datalists.filter(function (item) {

                    if (exp.test(item.documento.toLowerCase()) || exp.test(item.nombre.toLowerCase()) || exp.test(item.correo_electronico.toLowerCase()) || exp.test(item.telefono.toLowerCase())) {
                        return item;
                    }

                });
                $scope.datalists = Usuario;
            };

        }]);