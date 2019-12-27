$("#but_upload").click(function () {
    var fd = new FormData();
    var files = $('#file')[0].files[0];
    fd.append('file', files);

    $.ajax({
        url: "/api/File/UploadFile",
        type: 'post',
        data: fd,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.success == true) {

                var datos = [];
                $.each(response.data, function (index, value) {

                    datos.push({
                        Nun_contrato: value[0], ano: value[1], documento: value[2], nombre: value[3], dato_malo: value[4]
                    });
                    

                });
                alasql('SELECT * INTO XLSX("Reporte Decomisos.xlsx",{headers:true}) FROM ?', [datos]);
            }
            else {
                alert('file not uploaded');
            }
        },
    });
}); 