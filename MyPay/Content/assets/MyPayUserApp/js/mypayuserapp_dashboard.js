var WebResponse = '';
$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            DashboardLoad();
        }, 10);

});

$(document).ready(function () {
    debugger;
    var KYC = $("#hdnKycStatus").val();
    if (KYC == "0" || KYC == "2" || KYC == "4") {
        if ($("#hrIsFirstLogin").val() == "1")
            $("#kycmodal").modal('show');
    }
    else {
        $("#kycmodal").modal('hide');
    }
});
function DashboardLoad() {
    var objSliderTab = '';
    var objSlider
    var objMarque = '';
    if (window.location.pathname.toLowerCase() == "/mypayuserlogin/dashboard") {
        $.ajax({
            type: "POST",
            url: "/MyPayUser/MyPayUserCashbackOffersList",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                if (response != null) {
                    debugger;
                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        var IsValidJson = true;
                    }
                    catch (err) {

                    }
                    if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {

                        for (var i = 0; i < jsonData.data.length; ++i) {
                            var objIsactive = ""
                            if (i == 0) {
                                objIsactive = ' class="active"';
                            }
                            objSliderTab += ('<li data-target="#adslider" data-slide-to="' + i + ' " ' + objIsactive + ' ></li>');
                            if (i == 0) {
                                objSlider += ('<div class="carousel-item active">');
                            }
                            else {
                                objSlider += ('<div class="carousel-item ">');
                            }
                            objSlider += ('<img  src="' + jsonData.data[i].BannerImage + ' " class="d-block w-100" onclick="redirectToService(&apos;' + jsonData.data[i].Type + '&apos;)" alt="">');
                            objSlider += ('</div>');
                        }

                        if (window.location.pathname.toLowerCase() == "/mypayuserlogin/dashboard");
                        {
                            $("#dvSliderTab").html(objSliderTab);
                            $("#dvSliderContent").html(objSlider);
                        }
                        for (var i = 0; i < jsonData.MarqueList.length; ++i) {
                            objMarque += ('<a target="_blank" href="' + jsonData.MarqueList[i].Link + '">' + jsonData.MarqueList[i].Description + "</a>");
                        }
                    }
                    if (jsonData != null && jsonData.ProviderList != null && jsonData.ProviderList.length > 0) {
                        for (var i = 0; i < jsonData.ProviderList.length; ++i) {
                            if (jsonData.ProviderList[i].Commission != "" && parseInt(jsonData.ProviderList[i].Commission) != 0) {
                                $("#spnDisplayCashback" + jsonData.ProviderList[i].Id).html(jsonData.ProviderList[i].Commission + "% Cashback");
                                $("#spnDisplayCashback" + jsonData.ProviderList[i].Id).show();
                            }
                        }
                    }
                    else {
                        if (!IsValidJson) {
                            $("#dvMessage").html(response);
                        }
                    }
                    if ($("#marqueetxt") != undefined) {
                        //$("#marqueetxt").html(objMarque);
                    }
                    if ($("#marqueetxt2") != undefined) {
                        //$("#marqueetxt2").html(objMarque);
                    }

                }
                else {
                    $("#dvMsg").html("Something went wrong. Please try again later.");
                    return false;
                }
                $('#AjaxLoader').hide();
            },
            failure: function (response) {
                $("#dvMsg").html(response.responseText);
                $('#AjaxLoader').hide();
                return false;
            },
            error: function (response) {
                $("#dvMsg").html(response.responseText);
                $('#AjaxLoader').hide();
                return false;
            }
        });
    }
}
function showComingSoon() {
    $('#ComingSoon').modal("show");
}
function redirectToService(ServiceId) {
   $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserGetServiceUrl",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"ServiceId":"' + ServiceId + '"}',
                success: function (response) {
                    if (response != "Invalid Request") {
                        debugger;
                        if (response != "") {
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html("");
                            window.location.href = response;
                             return false;
                        }
                        else {
                            $('#AjaxLoader').hide();

                        }
                    }

                    else if (response == "Invalid Request") {
                        window.location.href = "/MyPayUserLogin";
                    }
                    else {
                        $('#AjaxLoader').hide();
                        $("#dvMessage").html(response);
                    }

                },
                failure: function (response) {
                    $('#AjaxLoader').hide();
                    JsonOutput = (response.responseText);
                },
                error: function (response) {
                    $('#AjaxLoader').hide();
                    JsonOutput = (response.responseText);
                }
            });
        }, 100);
}