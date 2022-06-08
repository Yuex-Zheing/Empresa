// Please see documentation at https:/ / docs.microsoft.com / aspnet / core / client - side / bundling - and - minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
// 2022-06-04T17:32:15.311Z
$(function () {

    function createDialog(idClientePopUp, titulo) {
        //var  = "#dlgClientes";
        $(idClientePopUp).dialog({
            autoOpen: false,
            title: titulo,
            width: 600,
            height: 600,
            modal: true,
            resizable: false,
            show: { effect: 'drop', direction: "left" },
            closeOnEscape: true,
        });
    }

    function getDate(IdDatePicker) {
        //Inicio: '2022-06-04T17:32:15.311Z'
        var _dt1 = $(IdDatePicker).datepicker('getDate')
        return _dt1.getFullYear() + "-" + (_dt1.getMonth() + 1) + "-" + _dt1.getDate();
    }


    $("#ReportMain .dtreport").datepicker({
        showButtonPanel: true,
        dateFormat: "yy-mm-dd",
        defaultDate: new Date()
    });

    $("#ReportMain .dtreport").datepicker("setDate", new Date());


    $("#rptBuscar").click(function () {

        var datajson = {
            dtInicio: getDate("#rptDtInicio"),
            dtFin: getDate("#rptDtFin"),
            IdCliente: $("#ddlclientes").val()
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

    /* Administracion de clientes*/ 

    createDialog("#dlgClientes", "Mantenimiento de Clientes");

    $("#btnDlg_CrearCliente").click(function () {
        $("#dlgClientes").dialog("open");
    });

    $("#dlgClientes #btnCrear").click(function () {

        $.ajax({
            cache: false,
            type: "POST",
            url: './Home/MantenimientoClientes',
            datatype: 'html',
            data: $("form#formClientes").serialize(),
            success: function (data) {
                if (data.objectReturn == null) {
                    var msj = data.error[0].mensajeUsuario;
                    alert(msj);
                }
                document.location.href = "/";
            },
            error: function (req, status, error) {
                alert("No se pudo cargar completamente la pagina");
            }
        });
    });

    /* Administracion de cuetnas */
    createDialog("#dlgCuentas","Mantenimiento de Cuentas");

    $("#btnDlg_CrearCuentas").click(function () {        
        $("#dlgCuentas").dialog("open");
    });

    var ddlReport = $("#ReportMain select#ddlclientes");

    if ($("#dlgCuentas select#ddlclientes").length > 0 || ddlReport.length > 0 ) {

        $.ajax({
            cache: false,
            type: "POST",
            url: './Home/consultaClientesCuentas',
            //datatype: 'html',
            //data: $("form#formClientes").serialize(),
            success: function (data) {
   
                var sel = (ddlReport.length > 0) ? ddlReport : $("#dlgCuentas select#ddlclientes");

                $.each(data, function (idx, itm) {
                    var option = new Option(itm.nombres, itm.idCliente);
                   sel.append($(option));
                });

            },
            error: function (req, status, error) {
                alert("No se pudo cargar completamente la pagina");
            }
        });
    }

    $("#dlgCuentas #btnCrear").click(function () {

        $.ajax({
            cache: false,
            type: "POST",
            url: './Home/MantenimientoCuentas',
            datatype: 'html',
            data: $("form#formCuentas").serialize(),
            success: function (data) {
                if (data.objectReturn == null) {
                    var msj = data.error[0].mensajeUsuario;
                    alert(msj);
                }
                document.location.href = "/";
            },
            error: function (req, status, error) {
                alert("No se pudo cargar completamente la pagina");
            }
        });
    });

    /*Mantenimiento movimientos*/
    createDialog("#dlgMovimientos", "Mantenimiento de Movimientos");

    $("#btnDlg_CrearMovimiento").click(function () {
        $("#dlgMovimientos").dialog("open");
    });

    

    if ($("#dlgMovimientos select#ddlcuentas").length > 0) {

        $.ajax({
            cache: false,
            type: "POST",
            url: './Home/consultaClientesCuentas',
            //datatype: 'html',
            //data: $("form#formClientes").serialize(),
            success: function (data) {
                var sel = $("#dlgMovimientos select#ddlcuentas");

                $.each(data, function (idx, itm) {

                    $.each(itm.infoCuentas, function (i, it) {
                        var names = itm.nombres + "-" + it.numeroCuenta;

                        var option = new Option(names, it.idCuenta);
                        sel.append($(option));
                    });
                   
                });

            },
            error: function (req, status, error) {
                alert("No se pudo cargar completamente la pagina");
            }
        });
    }

    $("#dlgMovimientos #btnCrear").click(function () {

        $.ajax({
            cache: false,
            type: "POST",
            url: './Home/MantenimientoMovimientos',
            datatype: 'html',
            data: $("form#formMovimientos").serialize(),
            success: function (data) {
                if (data.objectReturn == null) {
                    var msj = data.error[0].mensajeTecnico;
                    alert(msj);
                }
                document.location.href = "/";
            },
            error: function (req, status, error) {
                alert("No se pudo cargar completamente la pagina");
            }
        });
    });



});
