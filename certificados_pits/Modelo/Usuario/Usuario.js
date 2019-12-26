﻿CertificadosApp.controller('UsuarioController',
    ['$scope', '$rootScope', '$location', 'UsuarioService', '$routeParams', '$sce',
        function ($scope, $rootScope, $location, UsuarioService, $routeParams, $sce) {


            $scope.Usuario = {};

            $scope.curPage = 0;
            $scope.pageSize = 5;

            UsuarioService.ConsultarUsuarios(function (response) {
                if (response.success == true) {
                    $scope.datalists = response.usuarios;
                    $scope.numberOfPages = function () {
                        return Math.ceil($scope.datalists.length / $scope.pageSize);
                    };
                }
            });

            $scope.AbrirModal = function () {
                $("#BtnRegistrar").show();
                $("#BtnEditar").hide();
                $("#ModalUsuarios").modal("show");
            }

            $scope.GuardarUsuario = function () {
                $scope.Usuario.id = null;
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
                $("#ModalUsuarios").modal("show");

            }

            $scope.EditarUsuario = function () {
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