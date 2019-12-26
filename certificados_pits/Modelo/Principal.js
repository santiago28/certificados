// script.js

// create the module and name it scotchApp
var CertificadosApp = angular.module('CertificadosApp', ['ngRoute', 'ngCookies']);



// configure our routes
CertificadosApp.config(function ($routeProvider) {
    $routeProvider

        // route for the home page
        .when('/Certificado', {
            templateUrl: 'View/CertificadoView.html',
            controller: 'CertificadoController'
        })
        .when('/Parametrizacion', {
            templateUrl: 'View/ParametrizacionView.html',
            controller: 'ParametrizacionController'
        })
        .when('/Usuario', {
            templateUrl: 'View/UsuarioView.html',
            controller: 'UsuarioController'
        })
        .when('/Login', {
            templateUrl: 'View/LoginView.html',
            controller: 'LoginController'
        })
})
    .run(['$rootScope', '$location', '$cookieStore', '$http', '$templateCache',
        function ($rootScope, $location, $cookieStore, $http, $templateCache) {
            $rootScope.$on('$locationChangeStart', function (event, next, current) {

                $rootScope.globals = $cookieStore.get('username');

                if ($rootScope.globals == undefined) {
                    if ($location.path() == "/Certificado") {
                        $("#MenuPrincipal").css("display", "none");
                        $(".barra-menu").css("display", "none");
                        return;
                    }
                } else if ($rootScope.globals != undefined) {
                    if ($location.path() !== "/Login" && !$rootScope.globals) {

                    } else {
                        $("#MenuPrincipal").css("display", "block");
                        $(".item-menu").css("display", "flex");
                        $(".barra-menu").css("display", "block");
                    }
                }
                if ($location.path() !== '/Login' && !$rootScope.globals) {
                    $location.path('/Login');
                }
            });
        }]);

// create the controller and inject Angular's $scope
CertificadosApp.controller('PrincipalController',
    ['$scope', '$rootScope', 'PrincipalService', '$cookies', '$cookieStore', '$http', '$location',
        function ($scope, $rootScope, PrincipalService, $cookies, $cookieStore, $http, $location) {


            $scope.CerrarSesion = function () {
                $cookies.remove("username");
                $location.url('/Login');
            };

        }]);

