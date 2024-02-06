
function GetWalletBalance() {
    debugger;
     
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/WalletBalance",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: null,
                    success: function (response) {
                        debugger;
                        if (response != "") {
                            debugger;
                            var jsonData;
                            var IsValidJson = false;
                            try {
                                jsonData = $.parseJSON(response);
                                var IsValidJson = true;
                            }
                            catch (err) {

                            }
                            

                            WalletResponse = response;
                            if (WalletResponse == "Session Expired") {
                                debugger;
                                alert('Logged in from another device.');
                                window.location.href = "/MyPayUserLogin/Index";
                            }
                            else if (WalletResponse == "Invalid Request") {
                                window.location.href = "/MyPayUserLogin/Index";
                            }
                            else if (WalletResponse == "Invalid User Token") {
                                window.location.href = "/MyPayUserLogin/LoginPin";
                            }
                            else if (IsValidJson){
                                var arr = $.parseJSON(response);
                                if (arr['IsBankAdded'] == false && $("#dvDashboardIsBankAdded") != undefined) {
                                    $("#dvDashboardIsBankAdded").css("display", "block");
                                }
                                if ($("#spnCashBack") != undefined) {
                                    $("#spnCashBack").html(arr['TotalCashback']);
                                }
                                if ($("#spnMPCoinsDashboard") != undefined) {
                                    $("#spnMPCoinsDashboard").html(arr['TotalRewardPoints']);
                                }
                                if ($("#spnWalletDashboard") != undefined) {
                                    $("#spnWalletDashboard").html(arr['TotalAmount']);
                                }
                                if ($("#refUrl") != undefined) {
                                    $("#refUrl").val(arr['RefCode']);
                                }
                            }
                        }
                        else {
                            $("#dvMessage").html("Invalid Credentials");
                        }

                         $('#AjaxLoader').hide();
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
            }, 10);
}

$(document).ready(function () {
    GetWalletBalance();
    $("html, body").animate({ scrollTop: "0" });
    $("html, body").animate({ scrollTop: 0 });
});
$(document).ready(function () {
    debugger;
    var KYC = $("#hdnKycStatus").val();
    if (KYC == "0" || KYC == "2" || KYC == "4") {
        debugger;
        if (window.location.pathname.toLowerCase() != "/mypayuser/mypayuserkyc") 
        {
            $("#dvCompleteKYC").show();
        }
    }
    else {
        $("#dvCompleteKYC").hide();
    }


    document.onkeydown = function (e) {
        if (event.keyCode == 123) {
            return false;
        }
        if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
            return false;
        }
        if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
            return false;
        }
        if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
            return false;
        }
    }
});

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function ValidateEnter(el, objButton, checkNumeric, event) {
    debugger;
    if (event.keyCode === 13) {
        $('#' + objButton)[0].click();
        event.preventDefault();
        event.stopPropagation();
        return false;
    }
    else if (checkNumeric) {
        return isNumberKey(el, event)
    }
    else {
        return true;
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

function showhideWallet(obj) {
    event.preventDefault();
    event.stopPropagation();
    $("#" + obj).toggle();
    $("#" + obj + "XXX").toggle();
}

function checkRepeatingDigits(N) {

    // Find the last digit
    var digit = N % 10;

    while (N != 0) {

        // Find the current last digit
        var current_digit = N % 10;

        // Update the value of N
        N = parseInt(N / 10);

        // If there exists any distinct
        // digit, then return No
        if (current_digit != digit) {
            return false;
        }
    }

    // Otherwise, return Yes
    return true;
}
$("#btn_logOut").click(function () {
    $("#confirmLogOut").modal('show');
});