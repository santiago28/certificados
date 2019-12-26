CertificadosApp.controller('UsuarioController',
    ['$scope', '$rootScope', '$location', 'UsuarioService', '$routeParams', '$sce',
        function ($scope, $rootScope, $location, UsuarioService, $routeParams, $sce) {


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
            

        }]);