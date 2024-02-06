var WebResponse = '';
$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            ProfilesLoad();
        }, 10);
});
function ProfilesLoad() {
    $.ajax({
        type: "POST",
        url: "/MyPayUser/MyPayUserProfileDetails",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            debugger;
            if (response != null) {
                debugger;
                if (response == "Session Expired") {
                    debugger;
                    alert('Logged in from another device.');
                    window.location.href = "/MyPayUserLogin/Index";
                    return;
                }

                var jsonData;
                try {
                    var kycname;
                    var Gendername;
                    var Meritalname;
                    var Nationalityname;
                    var Proofname;
                    var email;

                    var color = "danger";
                    jsonData = $.parseJSON(response);

                    if (jsonData.IsEmailVerified == "1") {
                        email = jsonData.EmailId;

                    }

                    // KYC
                    if (jsonData.IsKycVerified == "3") {
                        kycname = "Verified";
                        color = "success";
                    }
                    else if (jsonData.IsKycVerified == "4") {
                        kycname = "Rejected";
                        color = "danger";
                    }
                    else if (jsonData.IsKycVerified == "0" || jsonData.IsKycVerified == "2") {
                        kycname = "Not Filled";
                        color = "danger";
                    }
                    else if (jsonData.IsKycVerified == "1") {
                        kycname = "Pending";
                        color = "warning";
                    }
                    else {
                        kycname = "Pending";
                        color = "warning";
                    }
                    kycname = "Kyc " + kycname;
                    var kycClass = "user-status badge badge-" + color + " mt-2";
                   // DOCUMENT
                    if (jsonData.UserImage == "") {
                        jsonData.UserImage = "/Content/images/avatar/default.png";
                    }
                    // GENDER
                    if (jsonData.Gender == "0") {
                        Gendername = "Not selected";
                    }
                    else if (jsonData.Gender == "1") {
                        Gendername = "Male";
                    }
                    else if (jsonData.Gender == "2") {
                        Gendername = "Female";
                    }
                    else if (jsonData.Gender == "3") {
                        Gendername = "Other";
                    }
                    // MERITAL STATUS

                    if (jsonData.MeritalStatus == "0") {
                        Meritalname = "Not selected";
                    }
                    else if (jsonData.MeritalStatus == "1") {
                        Meritalname = "Unmarried";
                    }
                    else if (jsonData.MeritalStatus == "2") {
                        Meritalname = "Married";
                    }
                    else if (jsonData.MeritalStatus == "3") {
                        Meritalname = "Divorced";
                    }


                    // NATIONALITY STATUS

                    if (jsonData.Nationality == "0") {
                        Nationalityname = "Not selected";
                    }
                    else if (jsonData.Nationality == "1") {
                        Nationalityname = "Nepalese";
                    }
                    else if (jsonData.Nationality == "2") {
                        Nationalityname = "Others";
                    }
                    else if (jsonData.Nationality == "3") {
                        Nationalityname = "Indian";
                    }


                    // PROOF TYPE STATUS
                    if (jsonData.ProofType == "1") {
                        Proofname = "Passport";
                        $("#imgfront").attr("src", jsonData.NationalIdProofFront);
                        $("#imgback").attr("src", jsonData.NationalIdProofBack);
                        $("#frontimage").attr("href", jsonData.NationalIdProofFront);
                        $("#backimage").attr("href", jsonData.NationalIdProofBack);
                    }
                    else if (jsonData.ProofType == "2") {
                        Proofname = "Driving Licence";
                        $("#imgfront").attr("src", jsonData.NationalIdProofFront);
                        $("#frontimage").attr("href", jsonData.NationalIdProofFront);
                        $("#dvback").css("display", "none");
                    }
                    else if (jsonData.ProofType == "3") {
                        Proofname = "Voter Id";
                        $("#imgfront").attr("src", jsonData.NationalIdProofFront);
                        $("#frontimage").attr("href", jsonData.NationalIdProofFront);
                        $("#dvback").css("display", "none");
                    }
                    else if (jsonData.ProofType == "4") {
                        Proofname = "Citizenship";
                        $("#imgfront").attr("src", jsonData.NationalIdProofFront);
                        $("#imgback").attr("src", jsonData.NationalIdProofBack);
                        $("#frontimage").attr("href", jsonData.NationalIdProofFront);
                        $("#backimage").attr("href", jsonData.NationalIdProofBack);
                    }
                    else if (jsonData.ProofType == "5") {
                        Proofname = "National IdCard";
                        $("#imgfront").attr("src", jsonData.NationalIdProofFront);
                        $("#imgback").attr("src", jsonData.NationalIdProofBack);
                        $("#frontimage").attr("href", jsonData.NationalIdProofFront);
                        $("#backimage").attr("href", jsonData.NationalIdProofBack);
                    }

                    $("#imgselfie").attr("src", jsonData.UserImage);
                    $("#selfieimage").attr("href", jsonData.UserImage);

                    $("#userDetailsProofType").html(Proofname);

                    $("#userImageHome").attr("src", jsonData.UserImage);
                    $("#userNameHome").html(jsonData.Name);
                    $("#userkycHome").html(kycname);
                    $("#userkycHome").css("color", color);
                    $("#userContactNumberHome").html(jsonData.ContactNumber);

                    $("#userImage").attr("src", jsonData.UserImage);
                    $("#userName").html(jsonData.Name);
                    $("#userkyc").html(kycname);
                    $("#userkyc").css("color", color);

                    $("#userDetailsName").html(jsonData.Name);
                    $("#userDetailsNameHead").html(jsonData.Name);

                    $("#userDetailsNameHead").html(jsonData.Email);
                    $("#userDetailsNamePhone").html(jsonData.ContactNumber);
                    $("#userDetailsNameKYC").html(kycname);
                    $("#userDetailsNameKYC").attr("class", kycClass);

                    $("#userDetailsEmail").html(email);
                    $("#userDetailsGender").html(Gendername);
                    $("#userDetailsMeritalStatus").html(Meritalname);
                    $("#userDetailsSpouseFullName").html(jsonData.SpouseName);
                    $("#userDetailsFatherFullName").html(jsonData.FatherName);
                    $("#userDetailsMotherFullName").html(jsonData.MotherName);
                    $("#userDetailsGrandFatherFullName").html(jsonData.GrandFatherName);
                    $("#userDetailsOccupation").html(jsonData.OccupationName);
                    $("#userDetailsNationality").html(Nationalityname);
                    $("#userDetailsState").html(jsonData.CurrentState);
                    $("#userDetailsDistrict").html(jsonData.CurrentDistrict);
                    $("#userDetailsMunicipality").html(jsonData.CurrentMunicipality);
                    $("#userDetailsWardNumber").html(jsonData.CurrentWardNumber);
                    $("#userDetailsStreetName").html(jsonData.CurrentStreetName);
                    $("#userDetailsHouseNumber").html(jsonData.CurrentHouseNumber);

                }
                catch (err) {

                }
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

function viewprofileClick() {
    $("#dvRecordsDisplayHome").show(200);
    $("#dvRecordsDisplay").hide(200);
    $("#dvRecordsDisplayDetails").hide(200);
}
function displayuserprofile() {
    $("#dvRecordsDisplayHome").hide(200);
    $("#dvRecordsDisplay").show(200);
    $("#dvRecordsDisplayDetails").hide(200);
}
function displayuserkycdetails() {
    $("#dvRecordsDisplayHome").hide(200);
    $("#dvRecordsDisplay").hide(200);
    $("#dvRecordsDisplayDetails").show(200);
}

function ShowMore() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#dvSingleProfile").css("display", "none");
            ProfilesLoad();
        }, 10);
}
function SingleProfilesLoad(ProfileUniqueId, ProviderName, Remarks, IndiaDate, StatusName, Amount, CurrentBalance, Cashback, ServiceCharge, NetAmount, Status) {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            debugger;
            $("#dvSingleRecordDisplay").html("");
            $("#dvSingleProfile").css("display", "block");
            $("#dvProfiles").css("display", "none");
            var str = '';
            str += '<span>' + ProviderName + '</span><br/>';
            str += '<span>' + Remarks + '</span ><br/>';
            str += '<span>Profile Id: <strong style="color: #f98c45">#' + ProfileUniqueId + '</strong></span><br/>';
            str += '<span>' + IndiaDate + '</span><br/>';
            if (Status == 1) {
                str += '<span class="tb-status text-success">' + StatusName + '</span><br/>';
            }
            else if (Status == 3) {
                str += '<span class="tb-status text-danger">' + StatusName + '</span><br/>';
            }
            else {
                str += '<span class="tb-status text-orange">' + StatusName + '</span><br/>';
            }

            str += '<a href="/ProfileReceipt/Index?Profileid=' + ProfileUniqueId + '" class="btn btn-block btn-primary">Download Receipt</a>';
            str += '<hr />';
            str += '<span>Amount: Rs' + Amount + '</span><br/>';
            str += '<span>Balance: Rs' + CurrentBalance + '</span><br/>';
            str += '<span>Type: ' + ProviderName + '</span><br/>';
            str += '<span>Cashback: Rs ' + Cashback + '</span><br/>';
            str += '<span>Service Charge: Rs ' + ServiceCharge + '</span><br/>';
            str += '<span>Total Amount: Rs ' + NetAmount + '</span><br/>';

            $("#dvSingleRecordDisplay").append(str);

        }, 10);
    $('#AjaxLoader').hide();
}

function BackToTxns() {
    $("#dvSingleProfile").css("display", "none");
    $("#dvProfiles").css("display", "block");
}
