// Please see documentation at https:/ / docs.microsoft.com / aspnet / core / client - side / bundling - and - minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// 2022-06-04T17:32:15.311Z
$(function () {

    function getDate(IdDatePicker) {
        //Inicio: '2022-06-04T17:32:15.311Z'
        var _dt1 = $(IdDatePicker).datepicker('getDate')
        return _dt1.getFullYear() + "-" + (_dt1.getMonth() + 1) + "-" + _dt1.getDate();
    }

    $("#ReportMain .dtreport").datepicker({
        showButtonPanel: true,
        dateFormat: "yy-mm-dd"
    });

    $("#rptBuscar").click(function () {

        var datajson = {
            dtInicio: getDate("#rptDtInicio"),
            dtFin: getDate("#rptDtFin"),
            IdCliente: $("#txtcliente").val()
        };

        $.ajax({
            cache: false,
            type: "POST",
            url: './Reportes/ReportePorRangoFechas',
            datatype: 'html',
            data: datajson,
            success: function (data) {
                $("#divReportRangoFechas").empty().html(data);
                
            },
            error: function (req, status, error) {
                alert("No se pudo cargar completamente la pagina");
            }
        });

    });




});
