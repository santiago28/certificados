var url = "http://localhost:44393/api/"

$("#ConsultarCertificados").click(function () {
    var documento = $("#documento").val();
    var oPersona = new Object();
    oPersona.documento = documento;

    $.ajax({
        url: "/api/Certificado/ConsultarContratoxDocumento",
        type: 'POST',
        dataType: 'json',
        data: oPersona,
        success: function (response, textStatus, xhr) {
            if (response.success) {
                $("#div_table_certificados").show();
                $("#lbl_documento").text(response.persona.documento);
                $("#lbl_nombre").text(response.persona.nombre);

                var html = "";
                $("#tablacontrato > tbody").empty();
                $.each(response.contratos, function (index, value) {
                    html += "<tr>" +
                        "<td>" + value.anio + "</td>" +
                        "<td>" + value.numero_contrato + "</td>" +
                        "<td>" + value.fecha_inicio.split('T')[0] + "</td>" +
                        "<td>" + value.fecha_fin.split('T')[0] + "</td>" +
                        "<td>" + value.codigo_convenio + " - " + value.nombre_convenio + "</td>" +
                        "<td><a href='/View/Certificados.aspx?documento=" + value.documento + "&numero_contrato=" + value.numero_contrato + "' target='_blank' class='btn btn-primary'>Descargar</a>";
                })
                $("#tablacontrato > tbody").append(html);
            }
        }, 
    });
});