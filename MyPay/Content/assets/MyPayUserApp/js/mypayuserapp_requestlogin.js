
$("#Password").keypress(function (event) {
    if (event.keyCode == 13) {
        CheckRequestLogin();
        event.preventDefault();
        event.stopPropagation();
        return false;
    }
});
function CheckRequestLogin() {
    debugger;
    var ContactNumber = '@ViewBag.ContactNumber';
    var Password = $("#Password").val();
    if (Password == "") {
        $("#dvMessage").html("Please enter Password");
        return false;
    }
    else {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUserLogin/RequestLogin",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"UserName":"' + ContactNumber + '","Password":"' + Password + '"}',
                    success: function (response) {
                        debugger;
                        if (response == "success") {
                            window.location.href = "/MyPayUserLogin/Dashboard";
                            $("#dvMessage").html("Please wait ...");
                            $('#AjaxLoader').hide();
                            return false;
                        }
                        else if (response != "") {
                            debugger;
                            if (response == "12") {
                                $('#AjaxLoader').hide();
                                window.location.href = "/MyPayUserLogin/RegisterVerification?Type=Verification";
                            }
                            else {
                                try {
                                    var arr = $.parseJSON(response); //convert to javascript array
                                    if (arr['status'] == true && arr['ReponseCode'] == "1") {
                                        window.location.href = "/MyPayUserLogin/Dashboard";
                                        $('#AjaxLoader').hide();
                                    }
                                    else {
                                        $('#AjaxLoader').hide();
                                        $("#dvMessage").html(response);
                                    }
                                } catch (e) {
                                    $('#AjaxLoader').hide();
                                    $("#dvMessage").html(response);
                                }

                            }
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html("Invalid Credentials");
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
}

