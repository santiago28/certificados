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
})
.run(['$rootScope', '$location', '$cookieStore', '$http', '$templateCache',
    function ($rootScope, $location, $cookieStore, $http, $templateCache) {
        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            
        });
    }]);

// create the controller and inject Angular's $scope
CertificadosApp.controller('PrincipalController',
    ['$scope', '$rootScope', 'PrincipalService', '$cookies', '$cookieStore', '$http', '$location',
    function ($scope, $rootScope, PrincipalService, $cookies, $cookieStore, $http, $location) {

        



    }]);

