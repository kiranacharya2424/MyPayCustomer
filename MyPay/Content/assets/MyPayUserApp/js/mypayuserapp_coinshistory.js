var skip = 0;

$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            EarnedCoinsLoad(false);
        }, 10);
});
function EarnedCoins() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#hdnEarnedSkip").val(0);
            $("#hdnEarnedTake").val(10);
            EarnedCoinsLoad(false);
        }, 10);
}
function RedeemedCoins() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#hdnRedeemedTake").val(10);
            $("#hdnRedeemedSkip").val(0);
            RedeemedCoinsLoad(false);
        }, 10);
}
function ShowMore() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            EarnedCoinsLoad(true);
        }, 10);
}

function showmoreRedeemedCoins() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            RedeemedCoinsLoad(true);
        }, 10);
}

function EarnedCoinsLoad(objIsAppend) {
    debugger;
    $('#AjaxLoader').show();
    setTimeout(function () {  $("html, body").animate({ scrollTop: "0" });
        $.ajax({
            type: "POST",
            url: "/MyPayUser/MyPayUserEarnedCoinsList",
            data: '{"Take":"' + $("#hdnEarnedTake").val() + '","Skip":"' + $("#hdnEarnedSkip").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                var objEarnedCoins = '';
                if (response != null) {
                    debugger;
                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        IsValidJson = true;
                        if (jsonData.RewardPoints.length < 10) {
                            if ($("#hdnEarnedSkip").val() > 0) {
                                $("#dvMessage").html("No More Records.");
                                $("#showmore").css("display", "none");
                            }
                        }
                        else {
                            $("#showmore").css("display", "block");
                        }
                    }
                    catch (err) {

                    }
                    if (jsonData != null && jsonData.RewardPoints != null && jsonData.RewardPoints.length > 0) {
                        for (var i = 0; i < jsonData.RewardPoints.length; ++i) {
                            if (jsonData.RewardPoints[i].Sign == 1) {
                                objEarnedCoins += '<li class="nk-support-item px-0">';
                                objEarnedCoins += '<div class="serv-icon">';
                                objEarnedCoins += '<img src="/Content/assets/MyPayUserApp/images/circ_trophy.png" width="35" alt="">';
                                objEarnedCoins += '</div>';
                                objEarnedCoins += '<div class="nk-support-content">';
                                objEarnedCoins += '<span class="d-block fs-12px fw-normal text-soft">' + jsonData.RewardPoints[i].CreatedDateDt + '</span>';
                                objEarnedCoins += '<div class="title">';
                                objEarnedCoins += '<span class="timeline-title mb-0">' + jsonData.RewardPoints[i].Remarks + '</span>';
                                objEarnedCoins += '<span class="amount fw-medium fs-14px m-0 text-green">' + jsonData.RewardPoints[i].Amount + ' Coins</span>';
                                objEarnedCoins += '</div><!--title-->';
                                //objEarnedCoins += '<p class="w-60 text-soft">Available Reward Points: <strong class="text-base">' + jsonData.TotalRewardPoints + ' MP</strong></p>';
                                objEarnedCoins += '</div>';
                                objEarnedCoins += '</li>';

                            }
                        }
                    }
                    else {
                        if (!IsValidJson) {
                            $("#dvMessage").html(response);
                        }
                        $("#showmore").css("display", "none");
                    }
                    if (objIsAppend) {
                        $("#ulEarned").append(objEarnedCoins);
                    }
                    else {
                        $("#ulEarned").html(objEarnedCoins);
                    }
                    var skipEarned = parseInt($("#hdnEarnedSkip").val()) + 1;
                    $("#hdnEarnedSkip").val(skipEarned);
                    $('#AjaxLoader').hide();
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
    }, 10);
}

function RedeemedCoinsLoad(objRedeemAppend) {
    debugger;
    $('#AjaxLoader').show();
    setTimeout(function () {  $("html, body").animate({ scrollTop: "0" });
        $.ajax({
            type: "POST",
            url: "/MyPayUser/MyPayUserRedeemedCoinsList",
            data: '{"Take":"' + $("#hdnRedeemedTake").val() + '","Skip":"' + $("#hdnRedeemedSkip").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                var objRedeemedCoins = '';
                if (response != null) {
                    debugger;
                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        IsValidJson = true;
                        if (jsonData.RewardPoints.length < 10) {
                            if ($("#hdnRedeemedSkip").val() > 0) {
                                $("#dvMessage").html("No More Records.");
                                $("#showmoreRedeemedCoins").css("display", "none");
                            }
                        }
                        else {
                            $("#showmoreRedeemedCoins").css("display", "block");
                        }
                    }
                    catch (err) {

                    }
                    if (jsonData != null && jsonData.RewardPoints != null && jsonData.RewardPoints.length > 0) {
                        for (var i = 0; i < jsonData.RewardPoints.length; ++i) {
                            if (jsonData.RewardPoints[i].Sign == 2) {
                                objRedeemedCoins += '<li class="nk-support-item px-0">';
                                objRedeemedCoins += '<div class="serv-icon">';
                                objRedeemedCoins += '<img src="/Content/assets/MyPayUserApp/images/circ_trophy.png" width="35" alt="">';
                                objRedeemedCoins += '</div>';
                                objRedeemedCoins += '<div class="nk-support-content">';
                                objRedeemedCoins += '<span class="d-block fs-12px fw-normal text-soft">' + jsonData.RewardPoints[i].CreatedDateDt + '</span>';
                                objRedeemedCoins += '<div class="title">';
                                objRedeemedCoins += '<span class="timeline-title mb-0">' + jsonData.RewardPoints[i].Remarks + '</span>';
                                objRedeemedCoins += '<span class="amount fw-medium fs-14px m-0 text-green">' + jsonData.RewardPoints[i].Amount + ' Coins</span>';
                                objRedeemedCoins += '</div>';
                                //objRedeemedCoins += '<p class="w-60 text-soft">Available Reward Points: <strong class="text-base">' + jsonData.TotalRewardPoints + ' MP</strong></p>';
                                objRedeemedCoins += '</div>';
                                objRedeemedCoins += '</li>';
                            }
                        }
                    }
                    else {
                        if (!IsValidJson) {
                            $("#dvMessage").html(response);
                        }
                        $("#showmoreRedeemedCoins").css("display", "none");
                    }
                    if (objRedeemAppend) {
                        $("#ulRedeemed").append(objRedeemedCoins);
                    }
                    else {
                        $("#ulRedeemed").html("");
                        $("#ulRedeemed").append(objRedeemedCoins);
                    }
                    var skipRedeemed = parseInt($("#hdnRedeemedSkip").val()) + 1;
                    $("#hdnRedeemedSkip").val(skipRedeemed);
                    $('#AjaxLoader').hide();
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
    }, 10);
}
