CertificadosApp.controller('LoginController',
    ['$scope', '$rootScope', '$location', '$cookies', '$cookieStore', 'LoginService', '$routeParams', '$sce',
        function ($scope, $rootScope, $location, $cookies, $cookieStore, LoginService, $routeParams, $sce) {

            $("#MenuPrincipal").css("display", "none");
            $(".barra-menu").css("display", "none");

            $scope.IniciarSesion = function () {
                LoginService.IniciarSesion($scope.Usuario, function (response) {
                    if (response.success == true) {
                        if (response.respuesta == 0 || response.respuesta == 1) {
                            bootbox.dialog({
                                title: "Información",
                                message: "Usuario y/o contraseña incorrectos",
                                buttons: {
                                    success: {
                                        label: "Cerrar",
                                        className: "btn-primary",
                                    }
                                }
                            });
                            return;
                        }
                        $rootScope.globals = {
                            currentUser: {
                                id: response.usuario.id,
                                nombre: response.usuario.nombre,
                                documento: response.usuario.documento
                            }
                        };
                        $cookies.putObject("username", $rootScope.globals);
                        $("#MenuPrincipal").css("display", "block");
                        $(".item-menu").css("display", "flex");
                        $(".barra-menu").css("display", "block");
                        $rootScope.globals = $cookieStore.get('username');
                        window.location = '/Principal.html#/Parametrizacion';
                        //$("#username").text($rootScope.globals.currentUser.nombre + " " + $rootScope.globals.currentUser.apellido);
                    }
                });
            }

        }]);