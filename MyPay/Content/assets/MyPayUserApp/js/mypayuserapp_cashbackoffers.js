var WebResponse = '';
$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            CashbackOfferssLoad();
        }, 10);
});
function CashbackOfferssLoad() {
    var objTrans = '';
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
                        objTrans += '<div class="col-md-6 mb-3">';
                        objTrans += '<div class="p-3 round bg-lighter position-relative">';
                        objTrans += '<a href="javascript:void(0)">';
                        objTrans += '<img src="' + jsonData.data[i].BannerImage + '" class="w-100">';
                        objTrans += '</a>';
                        objTrans += '</div>';
                        objTrans += '</div>';
                    }
                }
                else {
                    if (!IsValidJson) {
                        $("#dvMessage").html(response);
                    }
                    $("#showmore").css("display", "none");
                }
                $("#table").append(objTrans);
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

function ShowMore() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            CashbackOfferssLoad();
        }, 10);
}