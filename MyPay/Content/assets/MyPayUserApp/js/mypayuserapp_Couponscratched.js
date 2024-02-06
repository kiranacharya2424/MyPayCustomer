
$(document).ready(function () {
    GetList();
});
function GetList() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var take = $("#hdnTake").val();
            var skip = $("#hdnSkip").val();

            $.ajax({
                url: "MyPayUser/MyPayUserAllCoupons",
                type: "POST",
                data: '{"Take":"' + take + '","Skip":"' + skip + '"}',
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                success: function (response) {
                    var jsonData = $.parseJSON(response);
                    var str = "";
                    for (var i = 0; i < jsonData.data.length; ++i) {
                        str += '<div class="col-md-4 col-lg-3 mb-3">';
                        if (jsonData.data[i].IsScratched == "0") {
                            str += '<div id="div_notScratch' + jsonData.data[i].Id + '" data-toggle="modal" data-target="#myModal" onclick="setValues(&apos;' + jsonData.data[i].Id + '&apos;,&apos;' + jsonData.data[i].CouponCode + '&apos;); " style="background-image: url(' + "./Content/assets/MyPayUserApp/images/scratchcard.png" + ');background-repeat: no-repeat, no-repeat; " class="votingsec p-3 round bg-lighter position-relative">';
                        }
                        else {

                            if (jsonData.data[i].CouponType == 1) {
                                str += '<div id="div_Scratch' + jsonData.data[i].Id + '" class="votingsec p-3 round bg-lighter position-relative couponsscratched-wt">';
                                str += ' <img src="./Content/assets/MyPayUserApp/images/gifts_bunch.png">';

                            }
                            else if (jsonData.data[i].CouponType == 2) {
                                str += '<div id="div_Scratch' + jsonData.data[i].Id + '" class="votingsec p-3 round bg-lighter position-relative couponsscratched-wt">';
                                str += '<img src="./Content/assets/MyPayUserApp/images/couopn-cashback-box.png">';
                            }
                            else if (jsonData.data[i].CouponType == 3) {
                                str += '<div id="div_Scratch' + jsonData.data[i].Id + '" class="votingsec p-3 round bg-lighter position-relative couponsscratched-wt">';
                                str += '<img src="./Content/assets/MyPayUserApp/images/coincredit.png"/>';

                            }
                            else if (jsonData.data[i].CouponType == 4) {
                                str += '<div id="div_Scratch' + jsonData.data[i].Id + '" class="votingsec p-3 round bg-lighter position-relative couponsscratched-wt">';
                                str += '<img src="./Content/assets/MyPayUserApp/images/BetterLuckImage.svg"/>';
                            }

                        }
                        str += '<div class="scratchinner" style="height:160px;display: flex;flex-direction:column;justify-content:center;align-items:center;">';
                        if (jsonData.data[i].IsScratched == "0") {
                            str += '<a id="btn_scratch" class="btn btn-sm btn-danger ml-auto color-black d-flex align-items-center text-white " data-toggle="modal" data-target="#myModal" onclick="setValues(&apos;' + jsonData.data[i].Id + '&apos;,&apos;' + jsonData.data[i].CouponCode + '&apos;); "> Scratch Card</a>';
                        }
                        else {
                            /*str += ' <label class="percent">' + js== 0) onData.data[i].CouponPercentage + '%</label>'*/
                            if (jsonData.data[i].CouponType == 4) {
                                str += ' <label>Better luck Next Time</label>'
                            }
                            else if (jsonData.data[i].CouponType == 3) {
                                str += ' <label>' + jsonData.data[i].CouponAmount + '</label>'
                                str += ' <label>' + jsonData.data[i].Title + '</label>'
                                if (jsonData.data[i].IsUsed == 1) {
                                    str += ' <label style="color:green">Coupon Redeemed </label>'
                                }


                            }
                            else if (jsonData.data[i].CouponType == 2) {
                                /*str += ' <label>' + jsonData.data[i].CouponAmount + '</label>'*/
                                str += ' <label>' + jsonData.data[i].CouponCode + '</label>'
                                str += ' <label>' + jsonData.data[i].Description + '</label>'
                                if (jsonData.data[i].IsUsed == 1) {
                                    str += ' <label style="color:green">Coupon Redeemed </label>'
                                }

                            }

                            else if (jsonData.data[i].CouponType == 1) {
                                str += ' <label>' + jsonData.data[i].CouponCode + '</label>'
                                str += ' <label class="percent">' + jsonData.data[i].CouponPercentage + '%</label>'
                                str += ' <label>' + jsonData.data[i].Description + '</label>'
                                if (jsonData.data[i].IsUsed == 1) {
                                    str += ' <label style="color:green">Coupon Redeemed </label>'
                                }
                                else if (jsonData.data[i].IsExpired == 1) {
                                    str += ' <label style="color:red">Coupon Expired </label>'
                                }
                                else {
                                    str += ' <label style="color:Orange">Coupon will be expired on ' + (jsonData.data[i].ToDate).split('T')[0] + ' </label>'
                                }
                            }
                        }
                        str += '</div></div></div>';
                    }
                    $("#scratchdiv").append(str);
                    $("#waittext").hide();
                    var skipindex = parseInt($("#hdnSkip").val());
                    $("#hdnSkip").val(skipindex + 1);
                },
                error: function () {
                    alert(" errorr");
                }

            });
            $('#AjaxLoader').hide();
        }, 100);
}


function GetData(Id, CouponCode) {
    $('#AjaxLoader').show();
    setTimeout(
        function () {

            $.ajax({
                url: "MyPayUser/MyPayUserScratchCouponNow",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: '{"Id":"' + Id + '","CouponCode":"' + CouponCode + '"}',
                success: function (response) {
                    console.log("data==", response);
                    var jsonData = $.parseJSON(response);
                    var str = "";


                    var str1 = "";

                    if (jsonData.data.CouponType == 1) {
                        str1 += '<div class="votingsec p-3 round bg-lighter position-relative couponsscratched-wt">';
                        str1 += ' <img src="./Content/assets/MyPayUserApp/images/gifts_bunch.png">';
                        str1 += '<div class="scratchinner" style="height:160px;display: flex;flex-direction:column;justify-content:center;align-items:center;">';
                        str1 += ' <label>' + jsonData.data.CouponCode + '</label>'
                        str1 += ' <br><label class="percent">' + jsonData.data.CouponPercentage + '%</label>'
                        str1 += ' <label>' + jsonData.data.Description + '</label>'
                        if (jsonData.data.IsExpired == 1) {
                            str1 += ' <label style="color:Red">Coupon Expired</label>'
                        }
                        else {
                            str1 += ' <label style="color:Orange">Coupon will be expired on ' + (jsonData.data.ToDate).split('T')[0] + '</label>'
                        }

                    }
                    else if (jsonData.data.CouponType == 2) {
                        str1 += '<div class="votingsec p-3 round bg-lighter position-relative couponsscratched-wt">';
                        str1 += ' <img src="./Content/assets/MyPayUserApp/images/couopn-cashback-box.png">';
                        str1 += '<div class="scratchinner" style="height:160px;display: flex;flex-direction:column;justify-content:center;align-items:center;">';
                        str1 += ' <label>' + jsonData.data.CouponCode + '</label>'
                        str1 += ' <label>' + jsonData.data.Description + '</label>'
                        if (jsonData.data.IsUsed == 1) {
                            str1 += ' <label style="color:green">Coupon Redeemed </label>'
                        }

                    }
                    else if (jsonData.data.CouponType == 3) {
                        str1 += '<div id="div_Scratch' + jsonData.data.Id + '" class="votingsec p-3 round bg-lighter position-relative couponsscratched-wt">';
                        str1 += '<img src="./Content/assets/MyPayUserApp/images/coincredit.png"/>';
                        str1 += '<div class="scratchinner" style="height:160px;display: flex;flex-direction:column;justify-content:center;align-items:center;">';
                        str1 += ' <label>' + jsonData.data.CouponAmount + '</label>'
                        str1 += ' <label>' + jsonData.data.Title + '</label>'
                        if (jsonData.data.IsUsed == 1) {
                            str1 += ' <label style="color:green">Coupon Redeemed </label>'
                        }

                    }
                    else if (jsonData.data.CouponType == 4) {
                        str1 += '<div id="div_Scratch' + jsonData.data.Id + '" class="votingsec p-3 round bg-lighter position-relative couponsscratched-wt">';
                        str1 += '<img src="./Content/assets/MyPayUserApp/images/BetterLuckImage.svg"/>';
                        str1 += '<div class="scratchinner" style="height:160px;display: flex;flex-direction:column;justify-content:center;align-items:center;">';
                        str1 += ' <label>Better luck Next Time</label>'

                    }
                    str1 += ' </div>';
                    $("#div_notScratch" + Id).html("");
                    $("#div_notScratch" + Id).css('background-image', 'none');
                    $("#div_notScratch" + Id).removeClass("votingsec p-3 round bg-lighter position-relative");
                    $("#div_notScratch" + Id).html(str1);

                    if (jsonData.data.CouponType == 1) {

                        str += ' <label>Congratulations!</label>'
                        str += ' <label>You have won a scratch card</label>'
                        str += ' <img src="./Content/assets/MyPayUserApp/images/gifts_bunch.png">';
                        str += ' <label>' + jsonData.data.CouponCode + '</label>'
                        str += ' <br><label class="percent">' + jsonData.data.CouponPercentage + '%</label>'
                        str += ' <label>' + jsonData.data.Description + '</label>'
                        if (jsonData.data.IsExpired == 1) {
                            str += ' <label style="color:Red">Coupon Expired</label>'
                        }
                        else {
                            str += ' <label style="color:Orange">Coupon will be expired on ' + (jsonData.data.ToDate).split('T')[0] + '</label>'
                        }
                    }
                    else if (jsonData.data.CouponType == 2) {
                        str += ' <label>Congratulations!</label>'
                        str += ' <label>You have won a cashback</label>'
                        str += '<img src="./Content/assets/MyPayUserApp/images/couopn-cashback-box.png">';
                        str += ' <label>' + jsonData.data.CouponCode + '</label>'
                        /*  str += ' <label>' + jsonData.data.CouponAmount + '</label>'*/
                        str += ' <label>' + jsonData.data.Description + '</label>'
                    }
                    else if (jsonData.data.CouponType == 3) {
                        str += ' <label>Congratulations!</label>'
                        str += ' <label>You have won MyPay coins</label>'
                        str += '<img src="./Content/assets/MyPayUserApp/images/coincredit.png"/>';
                        str += ' <label>' + jsonData.data.CouponAmount + '</label>'
                        str += ' <label>' + jsonData.data.Title + '</label>'
                    }
                    else if (jsonData.data.CouponType == 4) {
                        str += '<img src="./Content/assets/MyPayUserApp/images/BetterLuckImage.svg"/>';
                        str += ' <label>Better Luck Next Time</label>'
                    }

                    $("#Card").html('');
                    $("#Card").append(str);


                },
                error: function () {
                    alert(" errorr");
                }

            });
            $('#AjaxLoader').hide();
        }, 100);
}
function setValues(Id, CouponCode) {
    $('#hdnId').val(Id);
    $('#hdncouponcode').val(CouponCode);
    $('#popupimage').show();

    var str = "";

    $("#Card").html('');
    $("#Card").append(str);

}
function scratchCouponCard() {
    var Id = $('#hdnId').val();
    var CoupnCode = $('#hdncouponcode').val();
    GetData(Id, CoupnCode);
    $('#popupimage').hide();

}

function ShowMore() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            GetList();
        }, 10);
}

