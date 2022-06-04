// Please see documentation at https:/ / docs.microsoft.com / aspnet / core / client - side / bundling - and - minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// 2022-06-04T17:32:15.311Z
$(function () {

    $("#ReportMain .dtreport").datepicker({
        showButtonPanel: true,
        dateFormat: "yy-mm-dd"
    });

    $("#rptBuscar").click(function () {

        var dateInicio = $('#rptDtInicio').datepicker('getDate');

        $.ajax({
            cache: false,
            type: "POST",
            url: './Reportes/ReportePorRangoFechas',
            datatype: 'html',
            data: { Inicio: '2022-06-04T17:32:15.311Z' },
            success: function (data) {
                //$("#DivGridResultadoConsultaChequesEmergencia").empty().html(data);
                alert("ok");
            },
            error: function (req, status, error) {
                alert("No se pudo cargar completamente la pagina");
            }
        });

    });




});
