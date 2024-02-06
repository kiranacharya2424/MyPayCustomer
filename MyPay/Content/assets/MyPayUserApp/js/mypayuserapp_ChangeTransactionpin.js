$(document).on('click', '.toggle-password', function () {
    $(this).toggleClass("fa-eye-slash fa-eye");
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});
 

$("#btnReset").click(function () {
    var TransactionPin = $("#TransactionPin").val();
    if (TransactionPin == "") {
        $("#dvMessage").html("Please enter Transaction Pin");
        return false;
    }
    var ConfirmTransactionPin = $("#ConfirmTransactionPin").val();
    if (ConfirmTransactionPin == "") {
        $("#dvMessage").html("Please enter Confirm Transaction Pin");
        return false;
    }
    if (TransactionPin != ConfirmTransactionPin) {
        $("#dvMessage").html("Transaction Pin does not matched");
        return false;
    }
    var CurrentPassword = $("#CurrentPassword").val();
    if (CurrentPassword == "") {
        $("#dvMessage").html("Please enter Current Password");
        return false;
    }
    $("#dvMessage").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/ChangePin",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Pin":"' + TransactionPin + '","ConfirmPin":"' + ConfirmTransactionPin + '","Password":"' + CurrentPassword + '"}',
                success: function (response) {
                    if (response == "success") {
                        $('#AjaxLoader').hide();
                        alert('Transaction Pin Change Successfully.');
                        window.location.href = '/MyPayUser/MyPayUserChangePin';
                        return false;
                    }
                    else {
                        $("#dvMessage").html(response);
                        $('#AjaxLoader').hide();
                        return false;
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
});

const toggleNewPin = document.querySelector(".new-pin-switch");
const newPin = document.querySelector("#TransactionPin");

if (toggleNewPin != undefined) {
    toggleNewPin.addEventListener("click", function () {

        // toggle the type attribute
        const type = newPin.getAttribute("type") === "password" ? "text" : "password";
        newPin.setAttribute("type", type);

        // toggle the icon
        this.classList.toggle("is-shown");
    });
}

const toggleConfirmPin = document.querySelector(".confirm-pin-switch");
const confirmPin = document.querySelector("#ConfirmTransactionPin");

if (toggleConfirmPin != undefined) {
    toggleConfirmPin.addEventListener("click", function () {

        // toggle the type attribute
        const type = confirmPin.getAttribute("type") === "password" ? "text" : "password";
        confirmPin.setAttribute("type", type);

        // toggle the icon
        this.classList.toggle("is-shown");
    });
}

const togglePassword = document.querySelector(".current-passcode-switch");
const password = document.querySelector("#CurrentPassword");

if (togglePassword != undefined) {
    togglePassword.addEventListener("click", function () {

        // toggle the type attribute
        const type = password.getAttribute("type") === "password" ? "text" : "password";
        password.setAttribute("type", type);

        // toggle the icon
        this.classList.toggle("is-shown");
    });
}