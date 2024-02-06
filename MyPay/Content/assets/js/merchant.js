$(document).ready(function () {
    $("#btnSubmitChangeMerchantPassword").click(function (e) {
        var txtpassword = $("#Password").val();
        var passwordconfirm = $("#passwordconfirm").val();
        var oldpassword = $("#OldPassword").val();
        var Merchantoldpassword = $("#MerchantOldPassword").val();
        if (oldpassword == "") {
            $("#errormsg").html('Please enter old password.');
            e.preventDefault();
            e.stopPropagation();
            return false;
        }
        else if (oldpassword != Merchantoldpassword) {
            $("#errormsg").html('Old password does not match.');
            e.preventDefault();
            e.stopPropagation();
            return false;
        }
        if (txtpassword == "") {
            $("#errormsg").html('Please enter password.');
            e.preventDefault();
            e.stopPropagation();
            return false;

        }
        else if (passwordconfirm == "") {
            $("#errormsg").html('Please confirm password');
            e.preventDefault();
            e.stopPropagation();
            return false;

        }
        else if (txtpassword != passwordconfirm) {
            $("#errormsg").html('Password donot match. Please try again.');
            e.preventDefault();
            e.stopPropagation();
            return false;
        }
        else if (txtpassword.length < 8) {
            $("#errormsg").html('Minimum Password length should be 8 characters.');
            e.preventDefault();
            e.stopPropagation();
            return false;
        }
        else if (!isPassword(txtpassword)) {
            $("#errormsg").html('Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character');
            e.preventDefault();
            e.stopPropagation();
            return false;
        }
    });

    function isPassword(password) {
        var regex = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$/;
        return regex.test(password);
    }
});
