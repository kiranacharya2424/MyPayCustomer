

///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;
$(document).ready(function () {
    BindDataTable("tbllist");
});

$(function () {
    $(".nav-item").click(function () {
        debugger;
        var tableId = $(this).data("table");
        BindDataTable(tableId);
    });
});

function BindDataTable(tableId) {
    debugger;
    var SelectedService = $("#drpProviderServices").val();
    if (SelectedService !== "0") {
        $("#tbllist").show();
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                table = $('#tbllist').DataTable({
                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No Active Deal and Offer"
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
                        "url": "/DealsAndOffer/GetAdminDealsAndOffersLists",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            data.IsDeleted = 0;
                            data.ServiceId = $("#drpProviderServices").val();
                            data.KycStatus = $("#KycStatus").val();
                            data.GenderStatus = $("#GenderStatus").val();
                            data.ScheduleStatus = $("#drpScheduleStatus").val();
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
                        { "data": "Sno.", "name": "Sno", "autoWidth": true, "bSortable": false },
                        { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": false },
                        { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": false },
                        {
                            data: null,
                            render: function (data, type, row) {
                                if (data.ServiceName !== "0") {
                                    return '<input type="hidden" id="ServiceId' + data.Id + '" value="' + data.ServiceId + '"/><span>' + data.ServiceName.toUpperCase() + '</span> ';
                                }
                                else {
                                    return '<input type="hidden" id="ServiceId' + data.Id + '" value="' + data.ServiceId + '"/><span>-</span> ';
                                }
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<textarea id="Title' + data.Id + '"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid"  style="width:250px;height:60px;min-width:250px;min-height:60px; resize: none;" >' + data.Title + '</textarea>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<textarea id="Description' + data.Id + '"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid"  style="width:250px;height:60px;min-width:250px;min-height:60px; resize: none;" >' + data.Description + '</textarea>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="PromoCode' + data.Id + '" maxlength="20" style="width: 170px;" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.PromoCode + '">  ';
                                    

                            },
                            autoWidth: true,
                            bSortable: false
                        }, 
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="CouponPercentage' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.CouponPercentage + '" onkeypress="return isNumberKey(this, event);">';

                            },
                            autoWidth: true,
                            bSortable: false
                        },

                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="MinimumAmount' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MinimumAmount + '" onkeypress="return isNumberKey(this, event);">';

                            },
                            autoWidth: true,
                            bSortable: false
                        },

                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="MaximumAmount' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MaximumAmount + '" onkeypress="return isNumberKey(this, event);">';

                            },
                            autoWidth: true,
                            bSortable: false
                        },
                   
                        {
                            data: null,
                            render: function (data, type, row) {
                                var isChecked = "";
                                if (data.IsOneTime) {
                                    isChecked = "checked";
                                }

                                return '<div class="custom-control  custom-control-sm custom-switch">    <input type="checkbox" ' + isChecked + '  class="custom-control-input" id="OneTimeSwitch' + data.Id + '">    <label class="custom-control-label" for="OneTimeSwitch' + data.Id + '"></label></div>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var isChecked = "";
                                if (data.IsOneTimePerDay) {
                                    isChecked = "checked";
                                }

                                return '<div class="custom-control  custom-control-sm custom-switch">    <input type="checkbox" ' + isChecked + '  class="custom-control-input" id="OneTimeSwitch24Hrs' + data.Id + '">    <label class="custom-control-label" for="OneTimeSwitch24Hrs' + data.Id + '"></label></div>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var objGenderType = '<input type="hidden" id="GenderTypeId' + data.Id + '" value="' + data.GenderType + '"/>';
                                objGenderType = objGenderType + '<span  class="tb-status text-' + (data.GenderTypeName == "All" ? "success" : "") + '">' + data.GenderStatusName + '</span>';
                                return objGenderType;

                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var objKycType = '<input type="hidden" id="KycTypeId' + data.Id + '" value="' + data.KycType + '"/>';
                                objKycType = objKycType + '<span  class="tb-status text-' + (data.KycTypeName == "NotVerified" ? "danger" : "success") + '">' + data.KycStatusName + '</span>';
                                return objKycType;

                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        //{
                        //    data: null,
                        //    render: function (data, type, row) {
                        //        var str = "";
                        //        str = '<a class="gallery-image popup-image" href="/Images/DealsandOffers/' + data.Image + '" style="min-height:auto; height:auto;">';
                        //        if (data.Image == null || data.Image == "") {
                        //            str += '<img ID="targetImage' + data.Id + '" class="custom-file-inputrounded-top" width="120" src="/Content/assets/Images/noimageblank.png" alt="">';
                        //        }
                        //        else {
                        //            str += '<img ID="targetImage' + data.Id + '" class="custom-file-inputrounded-top" width="200" src="/Images/DealsandOffers/' + data.Image + '" alt="">';
                        //        }
                        //        str += '</a>';
                        //        return str;
                        //    },
                        //    autoWidth: true,
                        //    bSortable: false
                        //},
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text" id="FromDateDT' + data.Id + '"  readonly=readonly class="form-control form-control-md form-control-outlined datePicker dtdatecontrol_inGrid" value="' + data.FromDateDT + '">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text" id="ToDateDT' + data.Id + '"  readonly=readonly class="form-control form-control-md form-control-outlined datePicker dtdatecontrol_inGrid" value="' + data.ToDateDT + '">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var objScheduleStatusType = '<span  class="tb-status text-' + (data.ScheduleStatus != "Running" ? "danger" : "success") + '">' + data.ScheduleStatus + '</span>';
                                return objScheduleStatusType;
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="CouponValue' + data.Id + '" maxlength="20" style="width: 170px;" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.CouponValue + '">';
                               
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="CouponQuantity' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.CouponQuantity + '" onkeypress="return isNumberKey(this, event);">';

                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var actioncolumn = '<table><tr>';
                                //if (data.IsActive == true) {
                                //    actioncolumn = actioncolumn + '<td><a  href="/DealsAndOffer/AddDealsandOffersImage?Id=' + data.Id + '" id="UpdateCoupon' + data.Id + '"   class="btn btn-sm btn-info btnCouponAction" style="white-space:nowrap;"  title="Add Image"> + Image</a></td>';

                                //}
                                //else {
                                //    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateNotificationCampaign' + data.Id + '"   class="btn btn-sm btn-info btnCouponAction" style="white-space:nowrap;"  title="Add Image"  onclick=\'alert("Image Will Not Update As Deals and Offers Already Published.")\' > + Image</a></td>';
                                //}
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateCoupon' + data.Id + '"  class="btn btn-sm btn-success btnCouponAction"  title="Update Coupon" onclick=\'return updateCoupon(\"' + data.Id + '\");\'  class="btn btn-primary "> Save </a></td>';
                                if (data.IsActive == true) {
                                    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);"  class="btn btn-sm btn-success btnCouponAction"   title="UnPublish Coupon" onclick=\'return statusupdateCoupon(\"' + data.Id + '\",\"' + "false" + '\",);\' "> ' + "UnPublish" + ' </a></td>';
                                }
                                else {
                                    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);"  class="btn btn-sm btn-danger btnCouponAction"  title="Publish Coupon" onclick=\'return statusupdateCoupon(\"' + data.Id + '\",\"' + "true" + '\",);\' "> ' + "Publish" + ' </a></td>';
                                }
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" onclick=\'return DeleteCoupon(\"' + data.Id + '\");\' class="ml-2 btn btn-sm btn-outline-danger btn-icon btn-tooltip btnCouponAction" title="Delete"><em class="icon ni ni-trash"></em></a></td>';
                                actioncolumn = actioncolumn + '</tr></table>'
                                return actioncolumn;
                            },
                            bSortable: false,
                            sTitle: "Action"
                        }


                    ]
                });

                if (document.getElementById("tbllist") != undefined) {
                    document.getElementById("tbllist").deleteTFoot();
                }

                $("#tbllist").append(
                    $('<tfoot/>').append($("#tbllist thead tr").clone())
                );
                $('#AjaxLoader').hide();
            }, 100);
    }
    else {
        $("#tbllist").hide();
    }
    $("#anchor_addCouponRow").show();
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#tbllist_length").hide();
    $(".dt-buttons btn-group flex-wrap").hide();

    setTimeout(function () {
        $('.datePicker').datepicker({
            format: 'd M yyyy',
            startDate: '+0d'
        });
    }, 1000);


}
$('[id*=btnsearch]').on('click', function () {
    table.draw();
});



function AddDealsAndOffersRow() {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var ProviderId = $("#drpProvider").val();
    var ServiceId = $("#drpProviderServices").val();
    var GenderStatus = $("#GenderStatus").val();
    var KycStatus = $("#KycStatus").val();
    var ScheduleStatus = $("#drpScheduleStatus").val();
    if (ProviderId == "0") {
        $("#dvMsg").html("Please select Category Type");
        return;
    }
    else if (ServiceId == "0") {
        $("#dvMsg").html("Please select Provider Service");
        return;
    }

    var coupon = new Object();
    coupon.Id = 0;
    coupon.ServiceId = $("#drpProviderServices").val();
    coupon.GenderStatus = GenderStatus;
    coupon.KycStatus = KycStatus;
    coupon.FromDate = GetTodayDate();
    coupon.ToDate = GetTodayDate();
    coupon.Status = ScheduleStatus;

    if (coupon != null) {
        $.ajax({
            type: "POST",
            url: "/DealsAndOffer/AddNewDealandOffersDataRow",
            data: JSON.stringify(coupon),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null) {
                    BindDataTable("tbllist");
                    $("#dvMsgSuccess").html("Add Deal and Offers Values and click Update button in new row.");
                    // $("#anchor_addCouponRow").hide();
                    return true;
                } else {
                    $("#dvMsg").html("Something went wrong");
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
}


function updateCoupon(id) {
    debugger;

    var Title = $("#Title" + id).val(), Description = $("#Description" + id).val(), Amount = $("#Amount" + id).val(), perc = $("#CouponPercentage" + id).val(), fromdt = $("#FromDateDT" + id).val(), todt = $("#ToDateDT" + id).val();
    var ServiceId = $("#ServiceId" + id).val();
    //var TxnCount = $("#TxnCount" + id).val();
    //var TxnVolume = $("#TxnVolume" + id).val();
    var MinimumAmount = $("#MinimumAmount" + id).val();
    var MaximumAmount = $("#MaximumAmount" + id).val();
    var OneTimeSwitch = $("#OneTimeSwitch" + id).is(":checked");
    var OneTimeSwitch24Hrs = $("#OneTimeSwitch24Hrs" + id).is(":checked");
    var PromoCode = $("#PromoCode" + id).val();
    var CouponQuantity = $("#CouponQuantity" + id).val();
    var CouponValue = $("#CouponValue" + id).val();
    if (Title == "") {
        $("#dvMsg").html("Please enter title");
        return false;
    }
    else if (Description == "") {
        $("#dvMsg").html("Please enter Description");
        return false;
    }
    else if (Amount == "") {
        $("#dvMsg").html("Please enter  Amount");
        return false;
    }
    else if (perc == "") {
        $("#dvMsg").html("Please enter Coupon Percentage");
        return false;
    }
    //else if (CouponsCount == "") {
    //    $("#dvMsg").html("Please enter Coupons Count.");
    //    return false;
    //}
    else if (PromoCode == "") {
        $("#dvMsg").html("Please enter Promo Code.");
        return false;
    }

    else if (fromdt == "") {
        $("#dvMsg").html("Please enter From Date");
        return false;
    }
    else if (todt == "") {
        $("#dvMsg").html("Please enter To Date");
        return false;
    }
    else if (parseFloat(perc) >= "100") {
        $("#dvMsg").html("Coupon(%)  can't be greater than equal to 100");
        return false;
    }
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#UpdateCoupon" + id).attr("disabled", "disabled");
    $("#UpdateCoupon" + id).text("Processing..");
    var Coupon = new Object();
    Coupon.Id = parseInt(id);
    Coupon.ServiceId = ServiceId;
    Coupon.Title = Title;
    Coupon.Description = Description;
    Coupon.Amount = Amount;
    Coupon.CouponPercentage = perc;
    Coupon.FromDate = fromdt;
    Coupon.ToDate = todt;
    Coupon.IsOneTime = OneTimeSwitch;
    Coupon.IsOneTimePerDay = OneTimeSwitch24Hrs;
    Coupon.MinimumAmount = MinimumAmount;
    Coupon.MaximumAmount = MaximumAmount;
    Coupon.PromoCode = PromoCode;
    Coupon.CouponQuantity = CouponQuantity;
    Coupon.CouponValue  = CouponValue

    var dd = JSON.stringify(Coupon);
    if (Coupon != null) {
        $.ajax({
            type: "POST",
            url: "/DealsAndOffer/DealandOffersUpdate",
            data: JSON.stringify(Coupon),
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
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Deals and Offers are successfully updated");
                        $("#anchor_addCouponRow").show();
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




function statusupdateCoupon(id, activestatus) {

    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var Coupon = new Object();
    Coupon.Id = parseInt(id);
    Coupon.IsActive = activestatus;
    var dd = JSON.stringify(Coupon);
    if (Coupon != null) {
        $.ajax({
            type: "POST",
            url: "/DealsAndOffer/StatusUpdateDealsandOffers",
            data: JSON.stringify(Coupon),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                if (response != null) {
                    if (response.Id != "0") {
                        BindDataTable("tbllist");
                        if (activestatus == "true")
                            $("#dvMsgSuccess").html("Deals and Offers are successfully Published");
                        else
                            $("#dvMsgSuccess").html("Deals and Offers are successfully Unpublished");
                        $("#anchor_addCouponRow").show();
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


function DeleteCoupon(id) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm("Do you really want to delete Coupon ?")) {
        var Coupon = new Object();
        Coupon.Id = id;
        if (Coupon != null) {
            $.ajax({
                type: "POST",
                url: "/DealsAndOffer/DeleteDealsandOffersDataRow",
                data: JSON.stringify(Coupon),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Deals and Offers are successfully deleted");
                        return true;
                    } else {
                        $("#dvMsg").html("Something went wrong");
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
    else {
        return false;
    }
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

function GetTodayDate() {
    var tdate = new Date();
    var dd = tdate.getDate(); //yields day
    var MM = tdate.getMonth(); //yields month
    var yyyy = tdate.getFullYear(); //yields year
    var currentDate = dd + "-" + (MM + 1) + "-" + yyyy;

    return currentDate;
}

$(document).ready(function () {

    $("#btnSearch").click(function () {

    });

    //$("#drpCoupenType").on("change", function () {
    //    debugger;

    //    $("#KycStatus").empty();
    //    KycSelect.forEach(function (kyc) {
    //        $("#KycStatus").append($("<option></option>").val(kyc.Value).html(kyc.Text));
    //    });
    //    $("#GenderStatus").empty();
    //    GenderSelect.forEach(function (Gender) {
    //        $("#GenderStatus").append($("<option></option>").val(Gender.Value).html(Gender.Text));
    //    });
    //    $("#drpProvider").empty();
    //    CategorySelect.forEach(function (Category) {
    //        $("#drpProvider").append($("<option></option>").val(Category.Value).html(Category.Text));
    //    });

    //    $("#drpProviderServices").empty();

    //    $('#div_Provider').hide();
    //    $('#div_ProviderServices').hide();
    //    $('#div_KycType').show();
    //    $('#div_GenderStatus').show();
    //    var SelectedCoupenType = $("#drpCoupenType").val();
    //    if (SelectedCoupenType === "1") {
    //        $('#div_Provider').show();
    //        $('#div_ProviderServices').show();
    //    } else if (SelectedCoupenType === "4") {
    //        $('#div_KycType').hide();
    //        $('#div_GenderStatus').hide();
    //    }

    //})
    $("#drpProvider").on("change", function () {
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        var ProviderId = $("#drpProvider").val();
        var value = $("#drpProvider option:selected");
        if (ProviderId != 0) {
            $.ajax({
                url: "/DealsAndOffer/GetProviderServiceList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"ProviderId":"' + ProviderId + '"}',
                success: function (data) {
                    $('#drpProviderServices')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#drpProviderServices");
                    $.each(data, function (index, item) {
                        list.append(new Option(item.Text, item.Value));
                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            alert("Please select Provider ");
        }
    });

    $("#drpProviderServices").on("change", function () {

        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
    });
    $("#anchor_SearchCoupons").on("click", function () {
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        var ProviderId = $("#drpProvider").val();
        if (ProviderId == "0") {
            alert('Please select category');
        }
        else {
            var ProviderServicesId = $('#drpProviderServices').val();
            if (ProviderServicesId == "0" || ProviderServicesId == "") {
                $("#dvMsg").html("Please select Provider Service");
            }
            else {
                BindDataTable("tbllist");
            }
        }
    });
});
