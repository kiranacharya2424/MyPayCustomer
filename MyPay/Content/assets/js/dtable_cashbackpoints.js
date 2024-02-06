///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;

function BindDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var Type = $("#TypeEnum").val()
            if (Type != "0") {
                $("#tbllist").show();
                table = $('#tbllist').DataTable({
                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No Active Cashback and Reward Points"
                    },
                    "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                    "pageLength": 50,
                    "processing": true,
                    "serverSide": true,
                    "destroy": true,
                    "searching": false,
                    "sorting": false,
                    "order": [[0, "desc"]],
                    "ajax": {
                        "url": "/CashbackPoints/GetAdminCashbacksLists",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            data.Type = $("#TypeEnum").val();
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
                                return '<span class="tb-status spandatetime " > ' + data.UpdatedDateDt + '   </span > ';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                        { "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": false },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var strDisabled = "";
                                var strRegistrationCommissionfn = "";
                                if (data.Type == "1" && ((data.IsKYCApproved == "1"))) {
                                    strDisabled = "readonly";
                                    strRegistrationCommissionfn = "clsRegistrationCommissionMessage";
                                }
                                return '<span class="tb-status text-danger" > * </span><input type="text" id="RegistrationCommission' + data.Id + '" maxlength="10" ' + strDisabled + '  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid ' + strRegistrationCommissionfn + ' " value="' + data.RegistrationCommission + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var strDisabled = "";
                                var strRegistrationCommissionfn = "";
                                if (data.Type == "1" && ((data.IsKYCApproved == "1") || (data.GenderType != "0"))) {
                                    strDisabled = "readonly";
                                    strRegistrationCommissionfn = "clsRegistrationCommissionMessage";
                                }
                                return '<span class="tb-status text-danger" > * </span><input type="text" id="RegistrationRewardPoint' + data.Id + '"  maxlength="10" ' + strDisabled + '   class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid ' + strRegistrationCommissionfn + '" value="' + data.RegistrationRewardPoint + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var strDisabled = "";
                                var strSignUpBonusfn = "";
                                if (data.Type == "1" && ((data.IsKYCApproved == "1") || (data.GenderType != "0"))) {
                                    strDisabled = "readonly";
                                    strSignUpBonusfn = "clsSignUpBonusMessage";
                                }
                                return '<span class="tb-status text-danger" > * </span><input type="text" id="SignUpBonus' + data.Id + '" maxlength="10" ' + strDisabled + '  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid ' + strSignUpBonusfn + ' " value="' + data.SignUpBonus + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var strDisabled = "";
                                var strSignUpBonusfn = "";
                                if (data.Type == "1" && ((data.IsKYCApproved == "1") || (data.GenderType != "0"))) {
                                    strDisabled = "readonly";
                                    strSignUpBonusfn = "clsSignUpBonusMessage";
                                }
                                return '<span class="tb-status text-danger" > * </span><input type="text" id="SignUpBonusRewardPoint' + data.Id + '"  maxlength="10" ' + strDisabled + '   class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid ' + strSignUpBonusfn + '" value="' + data.SignUpBonusRewardPoint + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var strDisabled = "";
                                var strKycCommissionfn = "";
                                if (data.Type == "1" && ((data.IsKYCApproved == "1"))) {
                                    strDisabled = "readonly";
                                    strKycCommissionfn = "clsKycCommissionMessage";
                                }
                                return '<input type="text"   id="KYCCommission' + data.Id + '" maxlength="10" ' + strDisabled + '  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid ' + strKycCommissionfn + '" value="' + data.KYCCommission + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var strDisabled = "";
                                var strKycCommissionfn = "";
                                if (data.Type == "1" && ((data.IsKYCApproved == "1"))) {
                                    strDisabled = "readonly";
                                    strKycCommissionfn = "clsKycCommissionMessage";
                                }
                                return '<input type="text"   id="KYCRewardPoint' + data.Id + '" maxlength="10" ' + strDisabled + '  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid ' + strKycCommissionfn + '" value="' + data.KYCRewardPoint + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="TransactionCommission' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid data-kyc-status" value="' + data.TransactionCommission + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="TransactionRewardPoint' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid data-kyc-status" value="' + data.TransactionRewardPoint + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="TransactionCommissionMinimum' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid data-kyc-status" value="' + data.MinAmountTransactionCommission + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="TransactionCommissionMaximum' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid data-kyc-status" value="' + data.MaxAmountTransactionCommission + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="TransactionRewardPointMinimum' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid data-kyc-status" value="' + data.MinRewardPointTransactionCommission + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="TransactionRewardPointMaximum' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid data-kyc-status" value="' + data.MaxRewardPointTransactionCommission + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                if (data.KYCStatusName != "Verified") {
                                    return '<span id="KYCStatus' + data.Id + '" class="tb-status text-danger data-kyc-status">' + data.KYCStatusName + '  </span>';
                                }
                                else if (data.KYCStatusName == "Verified") {
                                    return '<span id="KYCStatus' + data.Id + '" class="tb-status text-success data-kyc-status">' + data.KYCStatusName + '  </span>';
                                }
                                else {
                                    return '<span id="KYCStatus' + data.Id + '" class="tb-status  data-kyc-status">' + data.KYCStatusName + '  </span>';
                                }
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                if (data.GenderType == "1") {
                                    return '<span id="GenderType' + data.Id + '" class="tb-status text-success">' + data.GenderTypeName + '  </span>';
                                }
                                else if (data.GenderType == "2") {
                                    return '<span id="GenderType' + data.Id + '" class="tb-status text-danger">' + data.GenderTypeName + '  </span>';
                                }
                                else if (data.GenderType == "3") {
                                    return '<span id="GenderType' + data.Id + '" class="tb-status text-warning">' + data.GenderTypeName + '  </span>';
                                }
                                else if (data.GenderType == "0") {
                                    return '<span id="GenderType' + data.Id + '" class="tb-status text-danger">' + data.GenderTypeName + '  </span>';
                                }
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var actioncolumn = '<table><tr>';
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateCashback' + data.Id + '"  class="btn btn-sm btn-success btncommissionAction"  title="Update Cashback" onclick=\'return updatecashback(\"' + data.Id + '\");\'  class="btn btn-primary "> Update </a></td>';
                                actioncolumn = actioncolumn + '</tr></table>';
                                actioncolumn = actioncolumn + '<input type="hidden" id="cashbacktype' + data.Id + '" value="' + data.Type + '"/>';
                                return actioncolumn;
                            },
                            bSortable: false,
                            sTitle: "Action"
                        }
                    ],
                    "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                        $(nRow).addClass('nk-tb-item');
                        debugger;
                        $('td', nRow).addClass('nk-tb-col tb-col-lg');
                        $(nRow.children[0]).addClass(' spandatetime');
                        $(nRow.children[1]).addClass(' spandatetime');
                        $(nRow.children[8]).addClass(' data-kyc-status');
                        $(nRow.children[9]).addClass(' data-kyc-status');
                        $(nRow.children[10]).addClass(' data-kyc-status');
                        $(nRow.children[11]).addClass(' data-kyc-status');
                        $(nRow.children[12]).addClass(' data-kyc-status');
                        $(nRow.children[13]).addClass(' data-kyc-status');
                        $(nRow.children[14]).addClass(' data-kyc-status');
                    }
                });

                if (document.getElementById("tbllist") != undefined) {
                    document.getElementById("tbllist").deleteTFoot();
                }

                $("#tbllist").append(
                    $('<tfoot/>').append($("#tbllist thead tr").clone())
                );
                //if ($("#TypeEnum").val() == "1") {
                //    $(".data-kyc-status").attr('style', 'display:none !important');
                //}
                //else {
                //    $(".data-kyc-status").attr('style', 'table-cell !important');
                //}
            }
            else {
                $("#tbllist").hide();
            }
            $("#anchor_addCommissionRow").show();
            $("#dvMsgSuccess").html("");
            $("#dvMsg").html("");
            $("#tbllist_length").hide();
            $(".dt-buttons btn-group flex-wrap").hide();
            $('#AjaxLoader').hide();
        }, 100);
}


function updatecashback(id) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#UpdateCashback" + id).attr("disabled", "disabled");
    $("#UpdateCashback" + id).text("Processing..");
    var regcommission = $("#RegistrationCommission" + id).val(), regpoints = $("#RegistrationRewardPoint" + id).val(),
        signupbonus = $("#SignUpBonus" + id).val(), signupbonusrewardpoint = $("#SignUpBonusRewardPoint" + id).val(),
        kyccommission = $("#KYCCommission" + id).val(), kycrewardpoint = $("#KYCRewardPoint" + id).val(),
        transactioncommission = $("#TransactionCommission" + id).val(), transactionrewardpoint = $("#TransactionRewardPoint" + id).val(),
        transactioncommissionMinimum = $("#TransactionCommissionMinimum" + id).val(), transactionrewardpointMinimum = $("#TransactionRewardPointMinimum" + id).val(),
        transactioncommissionMaximum = $("#TransactionCommissionMaximum" + id).val(), transactionrewardpointMaximum = $("#TransactionRewardPointMaximum" + id).val();


    var cashbacktype = $("#cashbacktype" + id).val();

    var cashback = new Object();
    cashback.Id = parseInt(id);
    cashback.Type = cashbacktype;
    cashback.RegistrationCommission = regcommission;
    cashback.RegistrationRewardPoint = regpoints;
    cashback.SignUpBonus = signupbonus;
    cashback.SignUpBonusRewardPoint = signupbonusrewardpoint;
    cashback.KYCCommission = kyccommission;
    cashback.KYCRewardPoint = kycrewardpoint;
    cashback.TransactionCommission = transactioncommission;
    cashback.TransactionRewardPoint = transactionrewardpoint;
    cashback.MinAmountTransactionCommission = transactioncommissionMinimum;
    cashback.MaxAmountTransactionCommission = transactioncommissionMaximum;
    cashback.MinRewardPointTransactionCommission = transactionrewardpointMinimum;
    cashback.MaxRewardPointTransactionCommission = transactionrewardpointMaximum;

    var dd = JSON.stringify(cashback);
    if (cashback != null) {
        $.ajax({
            type: "POST",
            url: "/CashbackPoints/CashbackUpdateCall",
            data: JSON.stringify(cashback),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                if (response != null) {
                    if (response.Id == "0") {
                        $("#dvMsg").html("Records not updated.");
                        return false;
                    }
                    else {
                        BindDataTable();
                        $("#dvMsgSuccess").html("Cashback and Reward Points are successfully updated");
                        //$("#anchor_addCommissionRow").show();
                        return true;
                    }
                }
                else {
                    $("#dvMsg").html("Something went wrong. Please try again later.");
                    return false;
                }
            },
            failure: function (response) {
                $("#dvMsg").html(response.responseText);
                return false;
            },
            error: function (response) {
                $("#dvMsg").html(response.responseText);
                return false;
            }
        });
    }
    else {
        return false;
    }
}

function BindCashbackPointsHistoryDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var Type = $("#TypeEnum").val()
            if (Type != "0") {
                table = $('#tblist').DataTable({
                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No Cashback Points History"
                    },
                    "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                    "pageLength": 50,
                    "processing": true,
                    "serverSide": true,
                    "destroy": true,
                    "order": [[1, "desc"]],
                    "ajax": {
                        "url": "/CashbackPoints/GetCashbackPointsHistoryLists",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            data.Type = $("#TypeEnum").val();
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
                        { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": true },
                        { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                        { "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": true },
                        { "data": "RegistrationCommission", "name": "Reg Commission", "autoWidth": true, "bSortable": true },
                        { "data": "KYCCommission", "name": "KYC Commission", "autoWidth": true, "bSortable": true },
                        { "data": "RegistrationRewardPoint", "name": "Reg Reward Point", "autoWidth": true },
                        { "data": "KYCRewardPoint", "name": "KYC Reward Point", "autoWidth": true, "bSortable": true },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span  class="data-kyc-status">' + data.TransactionCommission + '</span>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span  class="data-kyc-status">' + data.TransactionRewardPoint + '</span>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span  class="data-kyc-status">' + data.MinAmountTransactionCommission + '</span>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span  class="data-kyc-status">' + data.MaxAmountTransactionCommission + '</span>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span  class="data-kyc-status">' + data.MinRewardPointTransactionCommission + '</span>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span  class="data-kyc-status">' + data.MaxRewardPointTransactionCommission + '</span>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                if (data.KYCStatusName != "Verified") {
                                    return '<span class="tb-status text-danger data-kyc-status">' + data.KYCStatusName + '  </span>';
                                }
                                else if (data.KYCStatusName == "Verified") {
                                    return '<span class="tb-status text-success data-kyc-status">' + data.KYCStatusName + '  </span>';
                                }
                                else {
                                    return '<span class="tb-status  data-kyc-status">' + data.KYCStatusName + '  </span>';
                                }
                            },
                            autoWidth: true,
                            bSortable: true
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                if (data.GenderType == "1") {
                                    return '<span class="tb-status text-success">' + data.GenderTypeName + '  </span>';
                                }
                                else if (data.GenderType == "2") {
                                    return '<span class="tb-status text-danger">' + data.GenderTypeName + '  </span>';
                                }
                                else if (data.GenderType == "3") {
                                    return '<span class="tb-status text-warning">' + data.GenderTypeName + '  </span>';
                                }
                                else {
                                    return '<span class="tb-status text-danger">Not Selected</span>';
                                }
                            },
                            autoWidth: true,
                            bSortable: true
                        },
                        { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false }
                    ],
                    "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                        $(nRow).addClass('nk-tb-item');
                        $('td', nRow).addClass('nk-tb-col tb-col-md');
                        $(nRow.children[7]).addClass(' data-kyc-status');
                        $(nRow.children[8]).addClass(' data-kyc-status');
                        $(nRow.children[9]).addClass(' data-kyc-status');
                        $(nRow.children[10]).addClass(' data-kyc-status');
                        $(nRow.children[11]).addClass(' data-kyc-status');
                        $(nRow.children[12]).addClass(' data-kyc-status');
                        $(nRow.children[13]).addClass(' data-kyc-status');
                    }
                });
            }
            else {
                $("#tblist").hide();
            }
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );

            if ($("#TypeEnum").val() == "1") {
                $(".data-kyc-status").attr('style', 'display:none !important');
            }
            else {
                $(".data-kyc-status").attr('style', 'table-cell !important');
            }
            $('#AjaxLoader').hide();
        }, 100);
}

$(document).ready(function () {
    $("#tbllist").hide();
    $("#TypeEnum option[value='0']").remove();
    BindDataTable();

    $(".clsRegistrationCommissionMessage").click(function () {
        alert('Registration commission is valid for Non-Verified user and Non-Selected Gender only.')
    });
    $(".clsSignUpBonusMessage").click(function () {
        alert('SignUp Bonus is valid for Non-Verified user and Non-Selected Gender only.')
    });
    $(".clsKycCommissionMessage").click(function () {
        alert('KYC commission is valid for Non-Verified user only.')
    });

});