$(document).on('click', '.toggle-password', function () {
    $(this).toggleClass("fa-eye-slash fa-eye");
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});

const toggleOldPassword = document.querySelector(".old-passcode-switch");
const oldpassword = document.querySelector("#OldPassword");

if (toggleOldPassword != undefined) {
    toggleOldPassword.addEventListener("click", function () {
       
        // toggle the type attribute
        const type = oldpassword.getAttribute("type") === "password" ? "text" : "password";
        oldpassword.setAttribute("type", type);

        // toggle the icon
        this.classList.toggle("is-shown");
    });
}
const toggleNewPassword = document.querySelector(".new-passcode-switch");
const newPassword = document.querySelector("#NewPassword");

if (toggleNewPassword != undefined) {
    toggleNewPassword.addEventListener("click", function () {
        
        // toggle the type attribute
        const type = newPassword.getAttribute("type") === "password" ? "text" : "password";
        newPassword.setAttribute("type", type);

        // toggle the icon
        this.classList.toggle("is-shown");
    });
}
const toggleConfirmPassword = document.querySelector(".confirm-passcode-switch");
const confirmPassword = document.querySelector("#ConfirmPassword");

if (toggleConfirmPassword != undefined) {
    toggleConfirmPassword.addEventListener("click", function () {
        
        // toggle the type attribute
        const type = confirmPassword.getAttribute("type") === "password" ? "text" : "password";
        confirmPassword.setAttribute("type", type);

        // toggle the icon
        this.classList.toggle("is-shown");
    });
}

$("#CurrentPassword").keypress(function () {
    if (event.keyCode === 13) {
        $('#btnReset')[0].click();
        event.preventDefault();
        event.stopPropagation();
        return false;
    }
})

$("#btnChangePassword").click(function () {
    ;
    var OldPassword = $("#OldPassword").val();
    if (OldPassword == "") {
        $("#dvMessage").html("Please enter Old Password");
        return false;
    }
    var NewPassword = $("#NewPassword").val();
    if (NewPassword == "") {
        $("#dvMessage").html("Please enter New Password");
        return false;
    }
    var ConfirmPassword = $("#ConfirmPassword").val();
    if (ConfirmPassword == "") {
        $("#dvMessage").html("Please enter Confirm Password");
        return false;
    }
    if (NewPassword != ConfirmPassword) {
        $("#dvMessage").html("Password does not matched");
        return false;
    }
    
    $("#dvMessage").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/ChangePassword",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"OldPassword":"' + OldPassword + '","Password":"' + NewPassword + '","ConfirmPassword":"' + ConfirmPassword + '"}',
                success: function (response) {
                    if (response == "success") {
                        $('#AjaxLoader').hide();
                        $("#PaymentSuccess").modal('show');
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

