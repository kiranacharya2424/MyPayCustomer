var WebResponse = '';
$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#dvSingleTransaction").css("display", "none");
            TransactionsLoad();
        }, 10);
});

function TransactionsLoad() {
    var isTransferByPhone = $("#hdnTransferByPhone").val();
    var type = $("#ddlTransactionType").val();
    var DateFrom = $("#fromdate").val();
    var DateTo = $("#todate").val();
    var selectedDateFilter = $.map($('input[name="chk_dateFilter"]:checked'), function (c) { return c.value; });
    var DateFilterType = "";
    if (selectedDateFilter.length > 0) {
        DateFilterType = selectedDateFilter[0];
    }
    var take = $("#hdnTake").val();
    var skip = $("#hdnSkip").val();
    if (type !== "0" || DateFrom !== "" || DateTo !== "" || DateFilterType !== "") {
        take = "";
        skip = "";
        $("#hdnSkip").val("0");
    }
    var objTrans = '';
    $("#dvSingleTransaction").css("display", "none");
    $("#dvTransactionParent").css("display", "block");

    $.ajax({
        type: "POST",
        url: "/MyPayUser/MyPayUserTransactionList",
        data: '{"Take":"' + take + '","Skip":"' + skip + '","Type":"' + type + '","DateFilterType":"' + DateFilterType + '","DateFrom":"' + DateFrom + '","DateTo":"' + DateTo + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            if (response != null) {
                ;
                var jsonData;
                var IsValidJson = false;
                try {
                    jsonData = $.parseJSON(response);
                    var IsValidJson = true;
                    if (jsonData.Transactions.length < 10) {

                        if ($("#hdnSkip").val() > 0) {
                            $("#dvMessage").html("No More Records.");
                            $("#showmore").css("display", "none");
                        }
                        else {
                            $("#showmore").css("display", "block");
                        }
                    }
                    else {
                        $("#showmore").css("display", "block");
                    }
                    if (type !== "0" || DateFrom !== "" || DateTo !== "" || DateFilterType !== "") {
                        $("#showmore").css("display", "none");
                    }
                }
                catch (err) {

                }
                /* $("#ddlTransactionType").val("");*/

                $("#fromdate").val("");
                $("#todate").val("");


                $("#dvMessage").html('');
                if (jsonData != null && jsonData.Transactions != null && jsonData.Transactions.length > 0) {
                    console.log("jsonData", jsonData);
                    if (isTransferByPhone != "") {
                        $("#dvTransactionParent").css("display", "none");
                        SingleTransactionsLoad(jsonData.Transactions[0].Reference, jsonData.Transactions[0].Type, jsonData.Transactions[0].TransactionUniqueId, jsonData.Transactions[0].ProviderName, jsonData.Transactions[0].Remarks, jsonData.Transactions[0].IndiaDate, jsonData.Transactions[0].StatusName, jsonData.Transactions[0].Amount, jsonData.Transactions[0].CurrentBalance, jsonData.Transactions[0].CashBack, jsonData.Transactions[0].ServiceCharge, jsonData.Transactions[0].NetAmount, jsonData.Transactions[0].Status, jsonData.Transactions[0].MPCoinsCredit, jsonData.Transactions[0].MPCoinsDebit, jsonData.Transactions[0].RecieverName, jsonData.Transactions[0].RecieverContactNumber, jsonData.Transactions[0].TransferType, jsonData.Transactions[0].CustomerID, jsonData.Transactions[0].Description, jsonData.Transactions[0].CouponDiscount)
                    }

                    objTrans += "";
                    for (var i = 0; i < jsonData.Transactions.length; ++i) {

                        objTrans += '<li class="nk-support-item px-0 cursor-pointer" onclick="SingleTransactionsLoad(&apos;' + jsonData.Transactions[i].Reference + '&apos;,&apos;' + jsonData.Transactions[i].Type + '&apos;,&apos;' + jsonData.Transactions[i].TransactionUniqueId + '&apos;,&apos;' + jsonData.Transactions[i].ProviderName + '&apos;,&apos;' + jsonData.Transactions[i].Remarks + '&apos;,&apos;' + jsonData.Transactions[i].IndiaDate + '&apos;,&apos;' + jsonData.Transactions[i].StatusName + '&apos;,&apos;' + jsonData.Transactions[i].Amount + '&apos;,&apos;' + jsonData.Transactions[i].CurrentBalance + '&apos;,&apos;' + jsonData.Transactions[i].CashBack + '&apos;,&apos;' + jsonData.Transactions[i].ServiceCharge + '&apos;,&apos;' + jsonData.Transactions[i].NetAmount + '&apos;,&apos;' + jsonData.Transactions[i].Status + '&apos;,&apos;' + jsonData.Transactions[i].MPCoinsCredit + '&apos;,&apos;' + jsonData.Transactions[i].MPCoinsDebit + '&apos;,&apos;' + jsonData.Transactions[i].RecieverName + '&apos;,&apos;' + jsonData.Transactions[i].RecieverContactNumber + '&apos;,&apos;' + jsonData.Transactions[i].TransferType + '&apos;,&apos;' + jsonData.Transactions[i].CustomerID + '&apos;,&apos;' + jsonData.Transactions[i].Description + '&apos;,&apos;' + jsonData.Transactions[i].CouponDiscount + '&apos;);">';
                        objTrans += '<div class="serv-icon">';
                        objTrans += '<img src="/Content/assets/MyPayUserApp/images/dashboard/services-icon/load-money.svg" width="30" alt="load money">';
                        objTrans += '</div>';
                        objTrans += '<div class="nk-support-content">';
                        objTrans += '<div class="title">';
                        objTrans += '<span>' + jsonData.Transactions[i].ProviderName + ' </span>';
                        objTrans += '<div class="d-flex">';
                        objTrans += 'रु ' + jsonData.Transactions[i].NetAmount;
                        var SignColor = "green";
                        if (jsonData.Transactions[i].Sign == "2") {
                            SignColor = "red";
                        }
                        objTrans += '  <img src="/Content/assets/MyPayUserApp/images/dashboard/arr_' + SignColor + '.svg" class="ml-2" alt="">';
                        objTrans += '</div>';
                        objTrans += '</div>';
                        objTrans += '<div style="display: flex; justify-content: space-between;">';
                        if (jsonData.Transactions[i].Type == 22) {
                            if (jsonData.Transactions[i].TransferType == 2) {
                                objTrans += '<p style="flex-basis: 60%;">रु ' + jsonData.Transactions[i].Amount + ' has been received from ' + jsonData.Transactions[i].RecieverName + '(' + jsonData.Transactions[i].RecieverContactNumber + ')' + ' | Money added to wallet</p>';
                            }
                            else {
                                objTrans += '<p style="flex-basis: 60%;">रु ' + jsonData.Transactions[i].Amount + ' has been transferred To ' + jsonData.Transactions[i].RecieverName + '(' + jsonData.Transactions[i].RecieverContactNumber + ')' + ' | Money debited from wallet</p>';
                            }
                        }
                        else if (jsonData.Transactions[i].Type == 84) {
                            objTrans += '<p style="flex-basis: 60%;">Payment of रु ' + jsonData.Transactions[i].Amount + ' To ' + jsonData.Transactions[i].CustomerID + '</p>';
                        }
                        else {
                            objTrans += '<p style="flex-basis: 60%;">' + jsonData.Transactions[i].Remarks + '</p>';
                        }
                        objTrans += '<p style="flex-basis: 40%;text-align:right;font-weight: bold;">Balance  ' + jsonData.Transactions[i].CurrentBalance + '</p>';
                        objTrans += '</div>';
                        objTrans += '<div class="d-flex justify-content-between mt-2">';
                        objTrans += '<span class="time">' + jsonData.Transactions[i].IndiaDate + ' </span>';

                        var bgColor = "orange";
                        if (jsonData.Transactions[i].Status == 1) {
                            bgColor = "success"
                        }
                        else if (jsonData.Transactions[i].Status == 3) {
                            bgColor = "danger"
                        }
                        else {
                            bgColor = "orange"
                        }
                        objTrans += '<span class="badge bg-' + bgColor + '  ms-1 text-white rounded-pill">' + jsonData.Transactions[i].StatusName + '</span>';
                        objTrans += '</div>';
                        objTrans += '</div> ';
                        objTrans + '</li> ';
                    }
                    objTrans += "";


                }
                else {
                    if (!IsValidJson) {
                        $("#dvMessage").html(response);
                    }
                    $("#showmore").css("display", "none");
                }
                var skipTrans = parseInt($("#hdnSkip").val()) + 1;
                $("#dvTransactions").append(objTrans);
                $("#hdnSkip").val(skipTrans);
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

    $('#AjaxLoader').hide();
}

$("#icon_filter").click(function () {
    $("#DivFilter").modal('show');
})
$("#icon_Download").click(function () {
    $("#DivDownload").modal('show');
})
function handleCheckbox(value) {
    var isChecked = false;
    if ($("#" + value).prop('checked') == true) {
        isChecked = true;
    }
    $("#chk_3months").prop("checked", false);
    $("#chk_6months").prop("checked", false);
    $("#chk_12months").prop("checked", false);
    $("#chk_all").prop("checked", false);
    $("#" + value).prop("checked", isChecked);
}

function ShowMore() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#dvSingleTransaction").css("display", "none");
            TransactionsLoad();
        }, 10);
}
function SingleTransactionsLoad(Reference, Type, TransactionUniqueId, ProviderName, Remarks, IndiaDate, StatusName, Amount, CurrentBalance, Cashback, ServiceCharge, NetAmount, Status, MPCoinsCredit, MPCoinsDebit, RecieverName, RecieverContactNumber, TransferType, CustomerID, Description, CouponDiscount) {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            debugger;
            $("#dvSingleRecordDisplay").html("");
            $("#dvSingleTransaction").css("display", "block");
            $("#dvTransactionParent").css("display", "none");
            var str = '';
            str += ' <div class="col-12 col-sm-12 ">';
            str += '     <div class="nk-wg-action-content w-100 d-flex align-items-center justify-content-between pr-0">';
            str += '         <div class="d-flex align-items-center">';
            str += '             <em class="icon top-0"><img src="/Content/assets/MyPayUserApp/images/custom-icon/light/cashback.svg" class="dark_i"  width="30" alt=""></em>';
            str += '                 <div class="title">' + ProviderName + ' </div>';
            str += '         </div>';
            var ObjStatusColor = "success"
            if (Status == 1) {
                ObjStatusColor = "success";
            }
            else if (Status == 3) {
                ObjStatusColor = "danger";
            }
            else {
                ObjStatusColor = "orange";
            }

            str += '<span class="badge bg-' + ObjStatusColor + '  ms-1 text-white rounded-pill">' + StatusName + ' </span>';
            str += '</div>';
            if (Type == 73) {
                str += '<p class="mt-1  mb-1 d-flex align-items-center"><strong>' + Description + '</strong></p>';
            }
            if (Type == 22) {
                if (TransferType == 2) {
                    str += '<p class="mt-4  mb-1 d-flex align-items-center"><strong>रु ' + Amount + ' has been received from ' + RecieverName + '(' + RecieverContactNumber + ')' + '</strong></p>';
                }
                else {
                    str += '<p class="mt-4  mb-1 d-flex align-items-center"><strong>Wallet Transfer of रु ' + Amount + ' To ' + RecieverName + '(' + RecieverContactNumber + ')' + '</strong></p>';
                }
            }
            if (Type == 84) {
                str += '<p class="mt-4  mb-1 d-flex align-items-center"><strong>Payment of रु ' + Amount + ' To ' + CustomerID + '</strong></p>';
            }
            str += '         <p class="mt-1  mb-1 d-flex align-items-center">';
            str += '              ( <strong class="ml-1 mr-1 transid">' + (Type == 22 ? ' Customer Txn Id ' : ' Transaction Id ') + ' : <span style="color:#E45415;">' + (Type == 22 ? Reference : TransactionUniqueId) + ' </span> </strong> )';
            if (Type == 22) {
                str += '<a href="javascript:void(0);" class="copy"onclick=Copy(&apos;' + Reference + '&apos;)><em class="clipboard-icon icon ni ni-copy"></em></a>';
            }
            else {
                str += '<a href="javascript:void(0);" class="copy"onclick=Copy(&apos;' + TransactionUniqueId + '&apos;)><em class="clipboard-icon icon ni ni-copy"></em></a>';
            }
            str += '<a id="label_copy" href="javascript:void(0);"class="btn btn-primary btn-sm ml-2"  style="display:none;">Copied</a>'
            str += '         </p>';
            str += '         <div class="nk-support-content m-0">';
            str += '             <span class="time">' + IndiaDate + ' </span>';
            str += '         </div>';
            str += '';
            str += '         <div class="border-top mt-4 pt-4">';
            str += '             <div class="row">';
            str += '                 <div class="col-lg-4 col-sm-4 mb-3">';
            str += '                     <div class="form-group">';
            str += '                         <div class="form-control-wrap">';
            str += '                             <div class="data-label w-100">Amount</div>';
            str += '                             <div class="data-value ">रु ' + Amount + ' </div>';
            str += '                         </div>';
            str += '                     </div>';
            str += '                 </div> ';
            str += '                 <div class="col-lg-4 col-sm-4 mb-3">';
            str += '                     <div class="form-group">';
            str += '                         <div class="form-control-wrap">';
            str += '                             <div class="data-label w-100">Type</div>';
            str += '                             <div class="data-value ">' + ProviderName + ' </div>';
            str += '                         </div>';
            str += '                     </div>';
            str += '                 </div> ';
            if (MPCoinsCredit != "0" && MPCoinsCredit != "") {
                str += '                 <div class="col-lg-4 col-sm-4 mb-3">';
                str += '                     <div class="form-group">';
                str += '                         <div class="form-control-wrap">';
                str += '                             <div class="data-label w-100">MyPay Coins Credited</div>';
                str += '                             <div class="data-value ">' + MPCoinsCredit + ' </div>';
                str += '                         </div>';
                str += '                     </div>';
                str += '                 </div> ';
            }
            if (MPCoinsDebit != "0" && MPCoinsDebit != "") {
                str += '                 <div class="col-lg-4 col-sm-4 mb-3">';
                str += '                     <div class="form-group">';
                str += '                         <div class="form-control-wrap">';
                str += '                             <div class="data-label w-100">MyPay Coins Debited</div>';
                str += '                             <div class="data-value ">' + MPCoinsDebit + ' </div>';
                str += '                         </div>';
                str += '                     </div>';
                str += '                 </div> ';
            }
            str += '                 <div class="col-lg-4 col-sm-4 mb-3">';
            str += '                     <div class="form-group">';
            str += '                         <div class="form-control-wrap">';
            str += '                             <div class="data-label w-100">Cashback</div>';
            str += '                             <div class="data-value ">रु ' + Cashback + ' </div>';
            str += '                         </div>';
            str += '                     </div>';
            str += '                 </div> ';
            str += '                 <div class="col-lg-4 col-sm-4 mb-3">';
            str += '                     <div class="form-group">';
            str += '                         <div class="form-control-wrap">';
            str += '                             <div class="data-label w-100">Service Charge</div>';
            str += '                             <div class="data-value ">रु ' + ServiceCharge + ' </div>';
            str += '                         </div>';
            str += '                     </div>';
            str += '                 </div> ';
            str += '                 <div class="col-lg-4 col-sm-4 mb-3">';
            str += '                     <div class="form-group">';
            str += '                         <div class="form-control-wrap">';
            str += '                             <div class="data-label w-100">Total Amount</div>';
            str += '                             <div class="data-value ">रु ' + NetAmount + ' </div>';
            str += '                         </div>';
            str += '                     </div>';
            str += '                 </div>';
            str += '                 <div class="col-lg-4 col-sm-4 mb-3">';
            str += '                     <div class="form-group">';
            str += '                         <div class="form-control-wrap">';
            str += '                             <div class="data-label w-100">Remarks</div>';
            str += '                             <div class="data-value ">' + Remarks + ' </div>';
            str += '                         </div>';
            str += '                     </div>';
            str += '                 </div>';
            if (CouponDiscount > 0) {
                str += '                 <div class="col-lg-4 col-sm-4 mb-3">';
                str += '                     <div class="form-group">';
                str += '                         <div class="form-control-wrap">';
                str += '                             <div class="data-label w-100">CouponDiscount</div>';
                str += '                             <div class="data-value ">' + CouponDiscount + ' </div>';
                str += '                         </div>';
                str += '                     </div>';
                str += '                 </div>';
            }
            str += '             </div>';
            str += '         </div>';
            str += '         <div class="form-group text-center mt-5">';
            if (Type == "13") {
                str += '             <a href="/KhanepaniTransactionReceipt/Index?transactionid=' + TransactionUniqueId + '" class="btn btn-primary btn-lg">Download Receipt</a>';
            }
            else if (Type == "12") {
                str += '             <a href="/NEAElectricityTransactionReceipt/Index?transactionid=' + TransactionUniqueId + '" class="btn btn-primary btn-lg">Download Receipt</a>';
            }
            else {
                str += '             <a href="/TransactionReceipt/Index?transactionid=' + TransactionUniqueId + '" class="btn btn-primary btn-lg">Download Receipt</a>';
            }

            str += '         </div>';
            str += '     </div>';

            $("#dvSingleRecordDisplay").append(str);
            $("html, body").animate({ scrollTop: "0" });

        }, 10);
    $('#AjaxLoader').hide();
}

function BackToTxns() {
    $("#dvSingleTransaction").css("display", "none");
    $("#dvTransactionParent").css("display", "block");
    $("html, body").animate({ scrollTop: "0" });

}

$("#btnValidateOk").click(function () {
    $('#ValidationPopup').modal('hide');

})
function filterData() {
    if ($("#fromdate").val() === "" && $("#todate").val() !== "") {
        $('#err_message').text("Please select from Date")
        $('#ValidationPopup').modal('show');
        return;
    }
    if ($("#fromdate").val() !== "" && $("#todate").val() === "") {
        $('#err_message').text("Please select to Date")
        $('#ValidationPopup').modal('show');
        return;
    }
    if (new Date($("#fromdate").val()) > new Date($("#todate").val())) {
        $('#err_message').text("To date should be greater than From date")
        $('#ValidationPopup').modal('show');
        return;
    }
    $('#AjaxLoader').show();
    $('#showmore').hide();
    setTimeout(
        function () {
            $('#DivFilter').modal('hide');
            $("#dvTransactions").html('');
            TransactionsLoad();
        }, 10);
}
function Copy(id) {
    navigator.clipboard.writeText(id);
    $("#label_copy").show();
    setTimeout(function () {
        $("#label_copy").hide();
    }, 3000);
}