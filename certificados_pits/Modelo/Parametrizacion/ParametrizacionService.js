CertificadosApp.factory('ParametrizacionService',
    ['$http', '$rootScope', '$routeParams',
        function ($http, $rootScope, $routeParams) {
            var service = {};

            service.SubirImagen = function (data, callback) {
                //waitingDialog.show();
                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: URLServices + "Imagen/UploadFile",
                    contentType: false,
                    processData: false,
                    data: data
                }).done(function (responseData, textStatus) {
                    callback(responseData);
                    //waitingDialog.hide();
                }).fail(function () {
                    //waitingDialog.hide();
                });
            };

            service.SubirExcel  = function (data, callback) {
                //waitingDialog.show();
                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: URLServices + "File/UploadFile",
                    contentType: false,
                    processData: false,
                    data: data
                }).done(function (responseData, textStatus) {
                    callback(responseData);
                    //waitingDialog.hide();

                    waitingDialog.hide();


                }).fail(function () {
                    //waitingDialog.hide();
                });
            };

            service.ConsultarParametros = function (callback) {
                $http.get(URLServices + "Parametros/ConsultarParametros/")
                    .success(function (response) {
                        callback(response);
                    });
            };

            service.GuardarParametros = function (Parametros, callback) {
                $http.post(URLServices + "Parametros/GuardarParametros/", Parametros)
                    .success(function (response) {
                        callback(response);
                    });
            };

            return service;

        }]);