///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////
///////////////////////////////////////////////////

var table;

function BindRemittanceUserCurrencyDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MerchantUniqueId = $("#hdnMerchantId").val();

            table = $('#tblmerlist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Wallet Currency"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Remittance/GetRemittanceUserCurrency",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MerchantUniqueId = MerchantUniqueId;
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
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "MerchantUniqueId", "name": "Merchant Unique Id", "autoWidth": true, "bSortable": false },
                    { "data": "ContactName", "name": "Merchant Name", "autoWidth": true, "bSortable": false },
                    //{ "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": false },
                    { "data": "ODL", "name": "ODL", "autoWidth": true, "bSortable": false },
                    { "data": "Prefund", "name": "Prefund", "autoWidth": true, "bSortable": false },
                    //{ "data": "CurrencyID", "name": "Currency Id", "autoWidth": true, "bSortable": false },
                    //{ "data": "CurrencyName", "name": "Currency", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '' + data.CurrencyName + '  ' + '<img ID="targetImage" class="custom-file-inputrounded-top" width="40" src="' + data.Image + '" alt="">';

                        },
                        bSortable: false,
                        sTitle: "Currency"
                    },
                    //{ "data": "Image", "name": "CurrencyImage", "autoWidth": true, "bSortable": false },                    
                    //{ "data": "CountryName", "name": "Country Name", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = '';
                            str = '<a  style="margin:5px;" href="javascript:void(0);" class="btn btn-danger btn-sm btn-icon btn-tooltip"  onclick="return DeleteWalletCurrency(&apos;' + data.Id + '&apos;)" title="Delete" data-original-title="Delete"><em class="icon ni ni-cross"></em></a>';
                            str += '<a style="margin:5px;" href="javascript:void(0);" class="btn btn-danger btn-sm btn-icon btn-tooltip"  onclick="return AddWalletFund(&apos;' + data.Id + '&apos;,&apos;' + data.CurrencyID + '&apos;,&apos;' + data.MerchantUniqueId + '&apos;,&apos;' + data.Type + '&apos;,&apos;' + data.TypeName + '&apos;)" title="Add Fund" data-original-title="Add Fund"><em class="icon ni ni-money"></em></a>';
                            str += '<a style="margin:5px;" href="javascript:void(0);" class="btn btn-danger btn-sm btn-icon btn-tooltip"  onclick="return ConvertFund(&apos;' + data.Id + '&apos;,&apos;' + data.CurrencyID + '&apos;,&apos;' + data.MerchantUniqueId + '&apos;,&apos;' + data.Type + '&apos;,&apos;' + data.TypeName + '&apos;)" title="Convert Amount" data-original-title="Convert Amount"><em class="icon ni ni-exchange"></em></a>';
                            return str;
                        },
                        bSortable: false,
                        sTitle: "Action"
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            document.getElementById("tblmerlist").deleteTFoot();

            $("#tblmerlist").append(
                $('<tfoot/>').append($("#tblmerlist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function DeleteWalletCurrency(Id) {
    $("#dvSuccessMsg").html("");
    $("#dvFailedMsg").html("");
    if (confirm('This Action Will Reset Merchant Keys. Do You Really Want To Continue ??')) {
        $('#AjaxLoader').show();
        setTimeout(
            function () {

                $.ajax({
                    type: "POST",
                    url: "/Remittance/DeleteWalletCurrencies",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"Id":"' + Id + '"}',
                    success: function (response) {
                        if (response == "success") {
                            $("#dvSuccessMsg").html("Successfully Delete Selected Wallet");
                            BindRemittanceUserCurrencyDataTable();
                            $('#AjaxLoader').hide();
                        }
                        else {
                            $("#dvFailedMsg").html(response);
                            $('#AjaxLoader').hide();
                            return false;
                        }
                    },
                    failure: function (response) {
                        JsonOutput = (response.responseText);
                        $('#AjaxLoader').hide();
                    },
                    error: function (response) {
                        JsonOutput = (response.responseText);
                        $('#AjaxLoader').hide();
                    }
                });

                //$('#AjaxLoader').hide();
            }, 100);
    }
}

function AddWalletCurrency() {
    $("#dvSuccessMsg").html("");
    $("#dvFailedMsg").html("");
    var CurrencyId = $("#CurrencyId option:selected").val();
    var Type = $("#hdnType").val();
    var MerchantUniqueId = $("#hdnMerchantId").val();
    if (CurrencyId != "" || CurrencyId != "0") {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/Remittance/AssignWalletCurrencies",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"CurrencyId":"' + CurrencyId + '","MerchantUniqueId":"' + MerchantUniqueId + '","Type":"' + Type + '"}',
                    success: function (response) {
                        if (response == "success") {
                            $("#dvSuccessMsg").html("Successfully Assign Selected Wallet");
                            BindRemittanceUserCurrencyDataTable();
                            $("#CurrencyId").val("0");
                            $('#AjaxLoader').hide();
                        }
                        else {
                            $("#dvFailedMsg").html(response);
                            $('#AjaxLoader').hide();
                            return false;
                        }
                    },
                    failure: function (response) {
                        JsonOutput = (response.responseText);
                        $('#AjaxLoader').hide();
                    },
                    error: function (response) {
                        JsonOutput = (response.responseText);
                        $('#AjaxLoader').hide();
                    }
                });
                //$('#AjaxLoader').hide();
            }, 100);
    }
    else {
        $("dvFailedMsg").html("Please select currency");
    }
}
function AddWalletFund(Id, CurrencyId, MerchantUniqueId, Type, TypeName) {
    $('#AddFund').modal('show');
    $("#txtType").val(TypeName);
    $("#hdnCurrencyId").val(CurrencyId)
}


function isNumberKey(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    if (charCode == 46 && el.value.indexOf(".") !== -1) {
        return false;
    }

    if (el.value.indexOf(".") !== -1) {
        var range = document.selection.createRange();

        if (range.text != "") {
        }
        else {
            var number = el.value.split('.');
            if (number.length == 2 && number[1].length > 1)
                return false;
        }
    }

    return true;
}


function SubmitAddFund() {
    $("#dvSuccessMsg").html("");
    $("#dvFailedMsg").html("");
    var Sign = $("#drpSign option:selected").val();
    var TxnId = $("#txtTxnId").val();
    var Remarks = $("#txtRemarks").val();
    var Amount = $("#txtAmount").val();
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/Remittance/AddWalletFund",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"CurrencyId":"' + $("#hdnCurrencyId").val() + '","MerchantUniqueId":"' + $("#hdnMerchantId").val() + '","Type":"' + $("#hdnType").val() + '","Sign":"' + Sign + '","TxnId":"' + TxnId + '","Remarks":"' + Remarks + '","Amount":"' + Amount + '"}',
                success: function (response) {
                    if (response == "success") {
                        $("#dvSuccessMsg").html("Successfully Add Fund in Selected Wallet");
                        $("#dvPopupMsg").html("Successfully Add Fund in Selected Wallet");
                        BindRemittanceUserCurrencyDataTable();
                        $('#AddFund').modal('hide');
                        $("#CurrencyId").val("0");
                        $('#AjaxLoader').hide();
                    }
                    else {
                        $("#dvPopupMsg").html(response);
                        $('#AjaxLoader').hide();
                        return false;
                    }
                },
                failure: function (response) {
                    JsonOutput = (response.responseText);
                    $('#AjaxLoader').hide();
                },
                error: function (response) {
                    JsonOutput = (response.responseText);
                    $('#AjaxLoader').hide();
                }
            });
            //$('#AjaxLoader').hide();
        }, 100);
}


function BindCurrencyConversionDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MerchantUniqueId = $("#hdnMerchantId").val();
            table = $('#tblmerlist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Currency Conversion Rate"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Remittance/GetCurrencyConversionLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MerchantUniqueId = MerchantUniqueId;
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
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "SourceCurrencyName", "name": "Source Currency", "autoWidth": true, "bSortable": false },
                    { "data": "DestinationCurrencyName", "name": "Destination Currency", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<input class="form-control form-control-md form-control-outlined" type="text" id="rate' + data.Id + '" value="' + data.ConversionRate + '" autocomplete="off" maxlength="5" onkeypress="return isNumberKey(this, event);"/>';
                        },
                        bSortable: false,
                        sTitle: "Conversion Rate"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<input class="form-control form-control-md form-control-outlined" type="text" id="markup' + data.Id + '" value="' + data.Markup + '" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);"/>';
                        },
                        bSortable: false,
                        sTitle: "Markup"
                    },
                    { "data": "InverseRate", "name": "Inverse Rate", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    { "data": "UpdatedByName", "name": "Updated By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var actioncolumn = '<table><tr>';
                            actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateConversionRate' + data.Id + '"  class="btn btn-sm btn-success"  title="Update COnversion Rate" onclick=\'return updateconversionrate(\"' + data.Id + '\");\' > Save </a></td>';
                            actioncolumn = actioncolumn + '</tr></table>'
                            return actioncolumn;
                        },
                        bSortable: false,
                        sTitle: "Action"
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            document.getElementById("tblmerlist").deleteTFoot();

            $("#tblmerlist").append(
                $('<tfoot/>').append($("#tblmerlist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}
function BindDestinationCurrenciesDataTable() {
    debugger;
    var CurrencyId = $("#CurrencyId option:selected").val();
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblmerlist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Destination Currency"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Remittance/GetDestinationCurrenciesLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.CurrencyId = CurrencyId;
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
                    //{ "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "CurrencyName", "name": "Currency Name", "autoWidth": true, "bSortable": false },
                    { "data": "CurrencyId", "name": "Currency Id", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false }
                    /*{ "data": "UpdatedByName", "name": "Updated By", "autoWidth": true, "bSortable": false }*/
                    //{
                    //    data: null,
                    //    render: function (data, type, row) {
                    //        var str = '';
                    //        //str = '<a  style="margin:5px;" href="javascript:void(0);" class="btn btn-danger btn-sm btn-icon btn-tooltip"  onclick="return DeleteWalletCurrency(&apos;' + data.Id + '&apos;)" title="Delete" data-original-title="Delete"><em class="icon ni ni-cross"></em></a>';
                    //        //str += '<a style="margin:5px;" href="javascript:void(0);" class="btn btn-danger btn-sm btn-icon btn-tooltip"  onclick="return AddWalletFund(&apos;' + data.Id + '&apos;,&apos;' + data.CurrencyID + '&apos;,&apos;' + data.MerchantUniqueId + '&apos;,&apos;' + data.Type + '&apos;,&apos;' + data.TypeName + '&apos;)" title="Add Fund" data-original-title="Add Fund"><em class="icon ni ni-money"></em></a>';
                    //        return str;
                    //    },
                    //    bSortable: false,
                    //    sTitle: "Action"
                    //}
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            document.getElementById("tblmerlist").deleteTFoot();

            $("#tblmerlist").append(
                $('<tfoot/>').append($("#tblmerlist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindSourceDestinationCurrenciesDataTable() {
    var CurrencyId = $("#CurrencyId option:selected").val();
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblmerlist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Destination Currency"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Remittance/GetDestinationCurrenciesLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.CurrencyId = CurrencyId;
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
                    { "data": "CurrencyName", "name": "Currency Name", "autoWidth": true, "bSortable": false },
                    //{ "data": "CurrencyId", "name": "Currency Id", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = '<input type="checkbox" data-Id="' + data.CurrencyId + '" id="chk' + data.CurrencyId + '" Onchange = "return ApproveCurrency(this);"/>';
                            //str = '<a  style="margin:5px;" href="javascript:void(0);" class="btn btn-danger btn-sm btn-icon btn-tooltip"  onclick="return DeleteWalletCurrency(&apos;' + data.Id + '&apos;)" title="Delete" data-original-title="Delete"><em class="icon ni ni-cross"></em></a>';
                            //str += '<a style="margin:5px;" href="javascript:void(0);" class="btn btn-danger btn-sm btn-icon btn-tooltip"  onclick="return AddWalletFund(&apos;' + data.Id + '&apos;,&apos;' + data.CurrencyID + '&apos;,&apos;' + data.MerchantUniqueId + '&apos;,&apos;' + data.Type + '&apos;,&apos;' + data.TypeName + '&apos;)" title="Add Fund" data-original-title="Add Fund"><em class="icon ni ni-money"></em></a>';
                            return str;
                        },
                        bSortable: false,
                        sTitle: "Select"
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            document.getElementById("tblmerlist").deleteTFoot();

            $("#tblmerlist").append(
                $('<tfoot/>').append($("#tblmerlist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}
var ids = "";
function ApproveCurrency(obj) {
    debugger;
    var id = $(obj).attr("data-Id");
    var sourcecurrency = $("#CurrencyId option:selected").val();
    if (sourcecurrency == "" || sourcecurrency == "0") {
        $("#dvMessage").html("Please Select Source Currency.");
        $(obj).prop("checked", false);
        return false;
    }
    else {
        ids += id + ",";
        $("#hdnIds").val(ids);
    }

    return false;
}


function SubmitSourceCurrency() {
    debugger;
    var CurrencyId = $("#CurrencyId option:selected").val();
    var destinationCurrencyIds = $("#hdnIds").val();
    $('#AjaxLoader').show();
    setTimeout(
        function () {

            if (CurrencyId == "" || CurrencyId == "0") {
                $("#dvMessage").html('Please select Source currency');
            }
            else if (destinationCurrencyIds == "" || destinationCurrencyIds == "0") {
                $("#dvMessage").html('Please select Destination currency');
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Remittance/RemittanceSourceCurrency",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"CurrencyId":"' + CurrencyId + '","DestinationCurrencyList":"' + destinationCurrencyIds + '"}',
                    success: function (response) {
                        if (response != null) {
                            if (response == "success") {
                                $("#dvSuccessMessage").html(response);
                            }
                            else {
                                $("#dvMessage").html(response);
                            }
                        }
                        else {
                            JsonOutput = "Something went wrong. Please try again later.";
                        }
                    },
                    failure: function (response) {
                        JsonOutput = (response.responseText);
                        $("#dvMessage").html(JsonOutput);
                    },
                    error: function (response) {
                        JsonOutput = (response.responseText);
                        $("#dvMessage").html(JsonOutput);
                    }
                });
            }
            $('#AjaxLoader').hide();
        }, 100);
}

function updateconversionrate(uniqueid) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#UpdateConversionRate" + uniqueid).attr("disabled", "disabled");
    $("#UpdateConversionRate" + uniqueid).text("Processing..");
    var ConversionRate = $("#rate" + uniqueid).val();
    var Markup = $("#markup" + uniqueid).val();
    if (ConversionRate == "") {
        $("#dvMessage").html("Please enter Conversion Rate");
        return false;
    }
    else if (parseFloat(ConversionRate) <= "0") {
        $("#dvMessage").html("Conversion Rate  can't be less than or equal to 0.");
        return false;
    }
    var Rate = new Object();
    Rate.Id = uniqueid;
    Rate.ConversionRate = ConversionRate;
    Rate.Markup = Markup;
    var dd = JSON.stringify(Rate);
    if (Rate != null) {
        $.ajax({
            type: "POST",
            url: "/Remittance/SetRemittanceConversionRate",
            data: JSON.stringify(Rate),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                if (response != null) {
                    if (response.Id == "0") {
                        $("#dvMessage").html("Records not updated. Please check that Conversion Rate not less than or equal to 0.");
                        return false;
                    }
                    else {
                        BindCurrencyConversionDataTable();
                        $("#dvSuccessMessage").html("Conversion Rate is successfully saved");
                        //$("#anchor_addCommissionRow").show();
                        return true;
                    }
                }
                else {
                    $("#dvMessage").html("Something went wrong. Please try again later.");
                    return false;
                }
            },
            failure: function (response) {
                $("#dvMessage").html(response.responseText);
                return false;
            },
            error: function (response) {
                $("#dvMessage").html(response.responseText);
                return false;
            }
        });
    }
    else {
        return false;
    }
}

function ConvertFund(Id, CurrencyId, MerchantUniqueId, Type, TypeName) {
    $('#ConvertFund').modal('show');
    $("#txtType").val(TypeName);
    $("#hdnCurrencyId").val(CurrencyId)
}
