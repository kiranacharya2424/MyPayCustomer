

///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;
$(document).ready(function () {
    BindDataTable("tbllist5");
});

$(function () {
    $(".nav-item").click(function () {
        debugger;
        var tableId = $(this).data("table");
        BindDataTable(tableId);
    });
});

function BindDataTable(tableId) {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var TypeEnumId = $('#TypeEnum').val();
            var FromDate = $('#FromDate').val();
            var ToDate = $('#ToDate').val();
            var StartAmount = $('#StartAmount').val();
            var EndAmount = $('#EndAmount').val();
            $("#tbllist5").hide();
            $("#tbllist6").hide();
            $("#tbllist8").hide();
            $("#tbllist5_wrapper").hide();
            $("#tbllist6_wrapper").hide();
            $("#tbllist8_wrapper").hide();
            if (TypeEnumId != "0") {

                if (TypeEnumId == "5") {
                    $("#tbllist5").show();
                    $("#tbllist5_wrapper").show();
                    table = $('#tbllist5').DataTable({
                        "dom": 'lBfrtip',
                        bFilter: false,
                        "oLanguage": {
                            "sEmptyTable": "No Records  "
                        },
                        "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                        "pageLength": 50,
                        "processing": true,
                        "serverSide": true,
                        "destroy": true,
                        "searching": false,
                        "sorting": false,
                        "order": [[0, "asc"]],
                        "ajax": {
                            "url": "/NRBReports/GetNRBReportAnnexture",
                            "type": "POST",
                            "async": false,
                            data: function (data) {
                                data.Annexture = TypeEnumId;
                                data.StartDate = FromDate;
                                data.EndDate = ToDate;
                                data.StartAmount = StartAmount;
                                data.EndAmount = EndAmount;
                            },
                            error: function (xhr, error, code) {
                                if (xhr.status == 200) {
                                    alert('Session Timeout. Please login again to continue.');
                                }
                                else {
                                    alert("Something went wrong try again later");
                                }
                                location.reload();
                            }
                        },
                        "columns": [
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status text-danger" >' + data.FormOfTransaction + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status" >' + data.ZeroToThousandCount + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status" >' + data.ZeroToThousandSum.toFixed(2) + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status " >' + data.ThousandToFiveThousandCount + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status " >' + data.ThousandToFiveThousandSum.toFixed(2) + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status" >' + data.FiveThousandToTenThousandCount + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status" >' + data.FiveThousandToTenThousandSum.toFixed(2) + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status" >' + data.TenThousandToTwentyThousandCount + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status" >' + data.TenThousandToTwentyThousandSum.toFixed(2) + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status" >' + data.TwentyThousandToTwentyFiveThousandCount + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status" >' + data.TwentyThousandToTwentyFiveThousandSum.toFixed(2) + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status" >' + data.TwentyFiveThousandAboveCount + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status" >' + data.TwentyFiveThousandAboveSum.toFixed(2) + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status text-danger" >' + parseInt(parseInt(data.ZeroToThousandCount) + parseInt(data.ThousandToFiveThousandCount) + parseInt(data.FiveThousandToTenThousandCount) + parseInt(data.TenThousandToTwentyThousandCount) + parseInt(data.TwentyThousandToTwentyFiveThousandCount) + parseInt(data.TwentyFiveThousandAboveCount)) + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status text-danger" >' + parseFloat(parseFloat(data.ZeroToThousandSum) + parseFloat(data.ThousandToFiveThousandSum) + parseFloat(data.FiveThousandToTenThousandSum) + parseFloat(data.TenThousandToTwentyThousandSum) + parseFloat(data.TwentyThousandToTwentyFiveThousandSum) + parseFloat(data.TwentyFiveThousandAboveSum)).toFixed(2) + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            }
                        ]
                    });


                }

                else if (TypeEnumId == "6") {
                    $("#tbllist6").show();
                    $("#tbllist6_wrapper").show();
                    table = $('#tbllist6').DataTable({
                        "dom": 'lBfrtip',
                        bFilter: false,
                        "oLanguage": {
                            "sEmptyTable": "No Records  "
                        },
                        "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                        "pageLength": 50,
                        "processing": true,
                        "serverSide": true,
                        "destroy": true,
                        "searching": false,
                        "sorting": false,
                        "order": [[0, "asc"]],
                        "ajax": {
                            "url": "/NRBReports/GetNRBReportAnnexture",
                            "type": "POST",
                            "async": false,
                            data: function (data) {
                                data.Annexture = TypeEnumId;
                                data.StartDate = FromDate;
                                data.EndDate = ToDate;
                            },
                            error: function (xhr, error, code) {
                                if (xhr.status == 200) {
                                    alert('Session Timeout. Please login again to continue.');
                                }
                                else {
                                    alert("Something went wrong try again later");
                                }
                                location.reload();
                            }
                        },
                        "columns": [
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status text-danger" >' + data.FormOfTransaction + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status text-success" >' + data.Success + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status text-danger" >' + data.Failed + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            }
                        ]
                    });


                }

                else if (TypeEnumId == "8") {
                    $("#tbllist8").show();
                    $("#tbllist8_wrapper").show();
                    table = $('#tbllist8').DataTable({
                        "dom": 'lBfrtip',
                        bFilter: false,
                        "oLanguage": {
                            "sEmptyTable": "No Records  "
                        },
                        "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                        "pageLength": 50,
                        "processing": true,
                        "serverSide": true,
                        "destroy": true,
                        "searching": false,
                        "sorting": false,
                        "order": [[0, "asc"]],
                        "ajax": {
                            "url": "/NRBReports/GetNRBReportAnnexture",
                            "type": "POST",
                            "async": false,
                            data: function (data) {
                                data.Annexture = TypeEnumId;
                                data.StartDate = FromDate;
                                data.EndDate = ToDate;
                            },
                            error: function (xhr, error, code) {
                                if (xhr.status == 200) {
                                    alert('Session Timeout. Please login again to continue.');
                                }
                                else {
                                    alert("Something went wrong try again later");
                                }
                                location.reload();
                            }
                        },
                        "columns": [
                            {
                                data: null,
                                render: function (data, type, row) {
                                    if (data.Wallet == "Active Customer Wallet") {
                                        return '<span class="tb-status text-success" >' + data.Wallet + '</span> ';
                                    }
                                    else {
                                        return '<span class="tb-status text-danger" >' + data.Wallet + '</span> ';
                                    }
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {

                                    if (data.Gender == "Male" || data.Gender == "Female" || data.Gender == "Others") {
                                        return '<span class="tb-status text-success" >' + data.Gender + '</span> ';
                                    }
                                    else if ($.trim(data.Gender).toLowerCase() == "grand total") {
                                        return '<span class="tb-status" >' + data.Gender + '</span> ';
                                    }
                                    else {
                                        return '<span class="tb-status text-danger" >' + data.Gender + '</span> ';

                                    }
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status">' + data.TotalNumber + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            },
                            {
                                data: null,
                                render: function (data, type, row) {
                                    return '<span class="tb-status text-success" >' + data.TotalWalletSum + '</span> ';
                                },
                                autoWidth: true,
                                bSortable: false
                            }
                        ]
                    });


                }

            }
            else {
                $("#tbllist5").hide();
                $("#tbllist6").hide();
                $("#tbllist8").hide();
                $("#tbllist5_wrapper").hide();
                $("#tbllist6_wrapper").hide();
                $("#tbllist8_wrapper").hide();
            }
            $("#dvMsgSuccess").html("");
            $("#dvMsg").html("");
            $("#tbllist5_length").hide();
            $('#AjaxLoader').hide();
        }, 100);
}
$('[id*=btnsearch]').on('click', function () {
    table.draw();
});



$(document).ready(function () {
    $(".NRBExtendedFilter").hide();

    $("#TypeEnum").on("change", function () {
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        $("#tbllist5").hide();
        $("#tbllist6").hide();
        $("#tbllist8").hide();
        $("#tbllist5_wrapper").hide();
        $("#tbllist6_wrapper").hide();
        $("#tbllist8_wrapper").hide();
        var Type = $('#TypeEnum').val();
        if (Type == "0") {
            $("#dvMsg").html("Please select Type");
        }
        else {
            //if (Type == "5") {
            //    $(".NRBExtendedFilter").show();
            //}
            //else {
            //    $(".NRBExtendedFilter").hide();
            //} 
        }
    });
    $("#anchor_SearchReport").click(function () {
        debugger;
        var Type = $('#TypeEnum').val();
        if (Type == "0") {
            $("#dvMsg").html("Please select Type");
        }
        else {
            BindDataTable();
        }
    });
});

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

