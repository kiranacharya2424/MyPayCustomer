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
                                objEarnedCoins += '<tr><em class="mr-1"><img src="/Content/assets/MyPayUserApp/images/cashback.svg" width="20" height="20"></em>';
                                objEarnedCoins += '<td><b>' + jsonData.RewardPoints[i].Remarks + '</b></td></tr>';
                                objEarnedCoins += '<tr><td style="text-align:right; color:green;"><b>' + jsonData.RewardPoints[i].Amount + ' Coins</b></td></tr>';
                                objEarnedCoins += '<tr><td style="border-bottom:1px solid #efefef; text-align:right;">' + jsonData.RewardPoints[i].CreatedDateDt + '</td>';
                                objEarnedCoins += '</tr>';
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
                        $("#tableEarned tbody").append(objEarnedCoins);
                    }
                    else {
                        $("#tableEarned tbody").html(objEarnedCoins);
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
                                objRedeemedCoins += '<tr><em class="mr-1"><img src="/Content/assets/MyPayUserApp/images/cashback.svg" width="20" height="20"></em>';
                                objRedeemedCoins += '<td><b>' + jsonData.RewardPoints[i].Remarks + '</b></td></tr>';
                                objRedeemedCoins += '<tr><td style="text-align:right; color:red;"><b>' + jsonData.RewardPoints[i].Amount + ' Coins</b></td></tr>';
                                objRedeemedCoins += '<tr><td style="border-bottom:1px solid #efefef; text-align:right;">' + jsonData.RewardPoints[i].CreatedDateDt + '</td>';
                                objRedeemedCoins += '</tr>';
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
                        $("#tableRedeemed tbody").append(objRedeemedCoins);
                    }
                    else {
                        $("#tableRedeemed tbody").html("");
                        $("#tableRedeemed tbody").append(objRedeemedCoins);
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
