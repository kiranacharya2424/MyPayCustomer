
$(document).ready(function () {
    $("#dvCompleteKYC").hide();
    $("#step1").show();
    $("#citizenship").show();
    //Occupation(0);
    //State(0);
});
function ShowInstructions() {
    $("#InstructionsModal").modal('show');
}
function btnNextStepKyc(objStep) {
    if (objStep == 1) {
        $(".divSteps").hide();
        $("#step1").show();
        $("#liStep1").attr("class", "step current");
        $("#liStep2").attr("class", "step");
        $("#liStep3").attr("class", "step");
        $("#liStep4").attr("class", "step");
        $("#PageTitle").html("Personal Details");
        $("#PageDescription").html("Enter Personal Information ");
    }
    else if (objStep == 2) {
        debugger;
        var firstname = $("#fname").val();
        var lastname = $("#lname").val();
        var gender = $("#gender :selected").val();
        var marital = $("#marital :selected").val();
        var spousefname = $("#spousefname").val();
        var fatherfname = $("#fatherfname").val();
        var motherfname = $("#motherfname").val();
        var grandfname = $("#grandfname").val();
        var occupation = $("#ddlOccupation :selected").val();
        var nationality = $("#nationality :selected").val();
        var StepCompleted = 1;
        debugger;

        if (marital == "2") {
            if (spousefname == "") {
                alert("Please Enter Spouse's Full Name");
                return false;
            }
        }
        if (spousefname != "") {
            if (!spousefname.includes(' ')) {
                alert("Please Enter Spouse's Full Name");
                return false;
            }
        }
        if (fatherfname != "") {
            if (!fatherfname.includes(' ')) {
                alert("Please Enter Father's Full Name");
                return false;
            }
        }
        if (motherfname != "") {
            if (!motherfname.includes(' ')) {
                alert("Please Enter Mother's Full Name");
                return false;
            }
        }
        if (grandfname != "") {
            if (!grandfname.includes(' ')) {
                alert("Please Enter GrandFather's Full Name");
                return false;
            }
        }

        if (firstname == "") {
            alert("Please Enter First Name");
        }
        else if (lastname == "") {
            alert("Please Enter Last Name");
        }
        else if (gender == "0") {
            alert("Please Select Gender");
        }
        else if (occupation == "0") {
            alert("Please Select Occupation");
        }
        else if (marital == "0") {
            alert("Please Select Marital Status");
        }
        else if (fatherfname == "") {
            alert("Please Enter Father's Full Name");
        }
        else if (motherfname == "") {
            alert("Please Enter Mother's Full Name");
        }
        else if (grandfname == "") {
            alert("Please Enter GrandFather's Full Name");
        }
        else if (nationality == "0") {
            alert("Please Select Nationality");
        }
        else {
            $('#AjaxLoader').show();
            setTimeout(
                function () {
                    $.ajax({
                        type: "POST",
                        url: "/MyPayUser/MyPayUserKYCStep1",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: '{"StepCompleted":"' + StepCompleted + '","FirstName":"' + firstname + '","LastName":"' + lastname + '","Gender":"' + gender + '","Marital":"' + marital + '","SpouseName":"' + spousefname + '","FatherName":"' + fatherfname + '","MotherName":"' + motherfname + '","GrandFatherName":"' + grandfname + '","Occupation":"' + occupation + '","Nationality":"' + nationality + '"}',
                        success: function (response) {
                            if (response == "success") {
                                $(".divSteps").hide();
                                $("#step2").show();
                                $("#liStep1").attr("class", "step done");
                                $("#liStep2").attr("class", "step current");
                                $("#liStep3").attr("class", "step");
                                $("#liStep4").attr("class", "step");
                                $("#PageTitle").html("Address Details");
                                $("#PageDescription").html("Current Location Infromation");
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html("");
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
                    return false;
                }, 10);
        }

    }
    else if (objStep == 3) {
        debugger;
        var stateid = $("#ddlstatepradesh :selected").val();
        var districtid = $("#ddldistrict :selected").val();
        var municipalityid = $("#ddlmunicipality :selected").val();
        var state = $("#ddlstatepradesh :selected").html();
        var district = $("#ddldistrict :selected").html();
        var municipality = $("#ddlmunicipality :selected").html();
        var wardnumber = $("#wardnumber").val();
        var streetname = $("#streetname").val();
        var housenumber = $("#housenumber").val();
        var Occupation = $("#ddlOccupation :selected").val();
        StepCompleted = 2;
        if (stateid == "" || stateid == "0" || state == "") {
            alert("Please select state");
        }
        else if (districtid == "" || districtid == "0" || district == "") {
            alert("Please select district");
        }
        else if (municipalityid == "" || municipalityid == "0" || municipality == "") {
            alert("Please select municipality");
        }
        else if (wardnumber == "") {
            alert("Please enter ward number");
        }
        else {
            $('#AjaxLoader').show();
            setTimeout(
                function () {
                    $.ajax({
                        type: "POST",
                        url: "/MyPayUser/MyPayUserKYCStep2",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: '{"StepCompleted":"' + StepCompleted + '","StateId":"' + stateid + '","State":"' + state + '","DistrictId":"' + districtid + '","District":"' + district + '","MunicipalityId":"' + municipalityid + '","Municipality":"' + municipality + '","WardNumber":"' + wardnumber + '","StreetName":"' + streetname + '","HouseNumber":"' + housenumber + '","Occupation":"' + Occupation + '"}',
                        success: function (response) {
                            if (response == "success") {
                                $(".divSteps").hide();
                                $("#step3").show();
                                $("#liStep1").attr("class", "step done");
                                $("#liStep2").attr("class", "step done");
                                $("#liStep3").attr("class", "step current");
                                $("#liStep4").attr("class", "step");
                                $("#PageTitle").html("KYC Document Details");
                                $("#PageDescription").html("Upload KYC Documents Infromation");
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html("");
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
                    return false;
                }, 10);
        }

    }
    else if (objStep == 4) {
        var doctype = $("#listdoc :selected").val();
        debugger;
        var docimage = $("#hdnImage").val();
        var docBackImage = $("#hdnBackImage").val();
        var userimage = $("#hdnUserImage").val();
        if (docimage == "") {
            alert("Please upload document");
            return false;
        }
        if (userimage == "") {
            alert("Please upload Selfie");
            return false;
        }
        if (doctype == "citizenship" || doctype == "passport" || doctype == "nid") {
            if (docBackImage == "") {
                alert("Please upload document back image");
                return false;
            }
        }
        if (doctype == "vid") {
            doctype = "3";
        }
        else if (doctype == "dl") {
            doctype = "2";
        }
        else if (doctype == "passport") {
            doctype = "1";
        }
        else if (doctype == "citizenship") {
            doctype = "4";
        }
        else if (doctype == "nid") {
            doctype = "5";
        }
        StepCompleted = 3;
        var Occupation3 = $("#ddlOccupation :selected").val();
        if (doctype == "" || doctype == "0") {
            alert("Please select your proof type");
            return false;
        }
        else {
            $('#AjaxLoader').show();
            setTimeout(
                function () {
                    $.ajax({
                        type: "POST",
                        url: "/MyPayUser/MyPayUserKYCStep3",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: '{"StepCompleted":"' + StepCompleted + '","DocType":"' + doctype + '","Occupation":"' + Occupation3 + '"}',
                        success: function (response) {
                            if (response == "success") {
                                $(".divSteps").hide();
                                $("#step4").show();
                                $("#liStep1").attr("class", "step done");
                                $("#liStep2").attr("class", "step done");
                                $("#liStep3").attr("class", "step done");
                                $("#liStep4").attr("class", "step current");
                                $("#PageTitle").html("KYC Status");
                                $("#PageDescription").html("KYC Details Submitted");
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html("");
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
                    return false;
                }, 10);
        }

    }
    $("html, body").animate({ scrollTop: "0" });
}

$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            ProfilesLoad();
        }, 100);
});
function ProfilesLoad() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
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
                        var jsonData;
                        try {
                            var kycname;
                            var color = "orange";
                            jsonData = $.parseJSON(response);
                            $("#hdnImage").val(jsonData.NationalIdProofFront);
                            $("#hdnBackImage").val(jsonData.NationalIdProofBack);
                            $("#hdnUserImage").val(jsonData.UserImage);
                            if (jsonData.IsKycVerified == "3") {
                                kycname = "Verified";
                                color = "Green";
                            }
                            else if (jsonData.IsKycVerified == "4") {
                                kycname = "Rejected";
                                color = "Red";
                            }
                            else {
                                kycname = "Pending";
                            }
                            if (jsonData.UserImage == "") {
                                jsonData.UserImage = "/Content/images/avatar/default.png";
                            }
                            debugger;
                            //$("#userImageHome").attr("src", jsonData.UserImage);
                            //$("#userNameHome").html(jsonData.Name);
                            //$("#userkycHome").html(kycname);
                            //$("#userkycHome").css("color", color);
                            //$("#userContactNumberHome").html(jsonData.ContactNumber);

                            //$("#userImage").attr("src", jsonData.UserImage);

                            $("#fname").val(jsonData.FirstName);
                            $("#lname").val(jsonData.LastName);
                            $("#spousefname").val(jsonData.SpouseName);
                            $("#fatherfname").val(jsonData.FatherName);
                            $("#grandfname").val(jsonData.GrandFatherName);
                            $("#motherfname").val(jsonData.MotherName);
                            $("#gender").val(jsonData.Gender);
                            $("#marital").val(jsonData.MeritalStatus);
                            $("#ddlOccupation :selected").val(jsonData.Occupation);
                            $("#nationality").val(jsonData.Nationality);

                            $("#ddlstatepradesh :selected").val(jsonData.CurrentStateId);
                            $("#ddldistrict").val(jsonData.CurrentDistrictId);
                            $("#ddlmunicipality").val(jsonData.CurrentMunicipalityId);
                            $("#wardnumber").val(jsonData.CurrentWardNumber);
                            $("#streetname").val(jsonData.CurrentStreetName);
                            $("#housenumber").val(jsonData.CurrentHouseNumber);
                            Occupation(jsonData.Occupation);
                            State(jsonData.CurrentStateId);
                            District(jsonData.CurrentStateId, jsonData.CurrentDistrictId);
                            Municipality(jsonData.CurrentDistrictId, jsonData.CurrentMunicipalityId);
                            showhideSpouse();
                            $("#imagePreview").css("background-image", "url(" + jsonData.UserImage + ")");
                            debugger;
                            var prooftype = "";
                            if (jsonData.ProofType == "1") {
                                $("#listdoc").val("passport");
                                // showHide("#listdoc");
                                showHide();
                                $("#prev_img_3").attr("src", jsonData.NationalIdProofFront);
                                $("#prev_img_4").attr("src", jsonData.NationalIdProofBack);
                            }
                            else if (jsonData.ProofType == "2") {
                                $("#listdoc").val("dl");
                                showHide();
                                $("#prev_img_2").attr("src", jsonData.NationalIdProofFront);
                            }
                            else if (jsonData.ProofType == "3") {
                                $("#listdoc").val("vid");
                                showHide();
                                $("#prev_img_1").attr("src", jsonData.NationalIdProofFront);
                            }
                            else if (jsonData.ProofType == "4") {
                                $("#listdoc").val("citizenship");
                                showHide();
                                $("#prev_img_5").attr("src", jsonData.NationalIdProofFront);
                                $("#prev_img_6").attr("src", jsonData.NationalIdProofBack);
                            }
                            else if (jsonData.ProofType == "5") {
                                $("#listdoc").val("nid");
                                showHide();
                                $("#prev_img_7").attr("src", jsonData.NationalIdProofFront);
                                $("#prev_img_8").attr("src", jsonData.NationalIdProofBack);
                            }


                            //$("#userkyc").html(kycname);
                            //$("#userkyc").css("color", color);
                            //$("#userContactNumber").html(jsonData.ContactNumber);
                            //$("#userWalletAmount").html(jsonData.TotalAmount);
                            //$("#userMainBalance").html(jsonData.TotalAmount);
                            //$("#userCashback").html(jsonData.TotalAmount);
                            //$("#userMyPayCoins").html(jsonData.TotalAmount);

                            //$("#userDetailsName").html(jsonData.Name);
                            //$("#userDetailsGender").html(jsonData.Gender);
                            //$("#userDetailsMeritalStatus").html(jsonData.MeritalStatus);
                            //$("#userDetailsSpouseFullName").html(jsonData.SpouseName);
                            //$("#userDetailsFatherFullName").html(jsonData.FatherName);
                            //$("#userDetailsMotherFullName").html(jsonData.MotherName);
                            //$("#userDetailsGrandFatherFullName").html(jsonData.GrandFatherName);
                            //$("#userDetailsOccupation").html(jsonData.Occupation);
                            //$("#userDetailsNationality").html(jsonData.Nationality);
                            //$("#userDetailsState").html(jsonData.CurrentState);
                            //$("#userDetailsDistrict").html(jsonData.CurrentDistrict);
                            //$("#userDetailsMunicipality").html(jsonData.CurrentMunicipality);
                            //$("#userDetailsWardNumber").html(jsonData.CurrentWardNumber);
                            //$("#userDetailsStreetName").html(jsonData.CurrentStreetName);
                            //$("#userDetailsHouseNumber").html(jsonData.CurrentHouseNumber);

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
        }, 100);
    $('#AjaxLoader').hide();
}

var number = 1;
do {
    function validateFileType(event, i) {

        if (event.target.files.length > 0) {
            let src = URL.createObjectURL(event.target.files[0]);
            let preview = document.getElementById("prev_img_" + i);
            preview.src = src;
            preview.style.display = "block";
        }
    }
    number++;
}
while (number < 3);


function showHide() {
    debugger;
    var selectid = $("#listdoc :selected").val();
    if (selectid.selectedIndex !== 0) {
        //hide the divs

        for (var i = 0; i < divsO.length; i++) {
            divsO[i].style.display = 'none';
        }
        //unhide the selected div

        document.getElementById(selectid).style.display = 'block';
    }
}

window.onload = function () {
    //get the divs to show/hide
    divsO = document.getElementById("step3").getElementsByClassName('show-hide');
};


function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            jQuery('#imagePreview').css('background-image', 'url(' + e.target.result + ')');
            jQuery('#imagePreview').hide();
            jQuery('#imagePreview').fadeIn(650);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
jQuery(document).on('change', '#imageUpload', function () {
    readURL(this);
});

function Occupation(id) {
    var occupationid = id;
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/GetOccupation",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: null,
                success: function (response) {
                    var jsonData = $.parseJSON(response);
                    $("#ddlOccupation").html("");
                    if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                        /*$('#ddlOccupation').append($("<option></option>").val('0').html('Select Occupation'));*/
                        for (var i = 0; i < jsonData.data.length; ++i) {
                            if (occupationid == jsonData.data[i].Id) {
                                $("#ddlOccupation").append($("<option selected></option>").val(jsonData.data[i].Id).html(jsonData.data[i].CategoryName));
                            }
                            else {
                                $("#ddlOccupation").append($("<option></option>").val(jsonData.data[i].Id).html(jsonData.data[i].CategoryName));
                            }
                        }
                        $('#AjaxLoader').hide();
                        return false;
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

function State(provincecode) {
    debugger;
    var stateid = provincecode;
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/GetState",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: null,
                success: function (response) {
                    debugger;
                    var jsonData = $.parseJSON(response);
                    $("#ddlstatepradesh").html("");
                    if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                        //$('#ddlstatepradesh').empty().append($("<option></option>").val('0').html('Select State'));
                        for (var i = 0; i < jsonData.data.length; ++i) {
                            if (stateid == jsonData.data[i].ProvinceCode) {
                                $("#ddlstatepradesh").append($("<option selected></option>").val(jsonData.data[i].ProvinceCode).html(jsonData.data[i].Province));
                            }
                            else {
                                $("#ddlstatepradesh").append($("<option></option>").val(jsonData.data[i].ProvinceCode).html(jsonData.data[i].Province));
                            }
                        }
                        $('#AjaxLoader').hide();
                        return false;
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

function District(statecode, code) {
    var ProvinceCode = statecode;
    var DistrictCode = code;
    if (ProvinceCode != "" || ProvinceCode != "0") {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/GetDistrict",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"ProvinceCode":"' + ProvinceCode + '"}',
                    success: function (response) {
                        var jsonData = $.parseJSON(response);
                        $("#ddldistrict").html("");
                        if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                            //$('#ddldistrict').empty().append($("<option></option>").val('0').html('Select District'));
                            for (var i = 0; i < jsonData.data.length; ++i) {
                                if (DistrictCode == jsonData.data[i].DistrictCode) {
                                    $("#ddldistrict").append($("<option selected></option>").val(jsonData.data[i].DistrictCode).html(jsonData.data[i].District));
                                }
                                else {
                                    $("#ddldistrict").append($("<option></option>").val(jsonData.data[i].DistrictCode).html(jsonData.data[i].District));
                                }
                            }
                            $('#AjaxLoader').hide();
                            return false;
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

function Municipality(Districtcode, code) {
    var DistrictCode = Districtcode;
    var MunicipalityCode = code;
    if (DistrictCode != "" || DistrictCode != "0") {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/GetMunicipality",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"DistrictCode":"' + DistrictCode + '"}',
                    success: function (response) {
                        var jsonData = $.parseJSON(response);
                        $("#ddlmunicipality").html("");
                        if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                            //$('#ddlmunicipality').empty().append($("<option></option>").val('0').html('Select Municipality'));
                            for (var i = 0; i < jsonData.data.length; ++i) {
                                if (MunicipalityCode == jsonData.data[i].LocalLevelCode) {
                                    $("#ddlmunicipality").append($("<option selected></option>").val(jsonData.data[i].LocalLevelCode).html(jsonData.data[i].LocalLevel));
                                }
                                else {
                                    $("#ddlmunicipality").append($("<option></option>").val(jsonData.data[i].LocalLevelCode).html(jsonData.data[i].LocalLevel));
                                }
                            }
                            $('#AjaxLoader').hide();
                            return false;
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

$("#ddlstatepradesh").on("change", function () {
    var value = $("#ddlstatepradesh :selected").val();
    //$("#State").val(value.text());
    $("#ddldistrict").val(0);
    if (value != 0) {
        District(value, 0);

    }
    else {
        alert("Please select State");
    }
});

$("#ddldistrict").on("change", function () {
    var value = $("#ddldistrict :selected").val();
    //$("#State").val(value.text());
    $("#ddlmunicipality").val(0);
    if (value != 0) {
        Municipality(value, 0);
    }
    else {
        alert("Please select District");
    }
});

function SubmitButtonOnclick(divid, type, imageid) {
    var formData = new FormData();
    var imageFile = document.getElementById("upFile" + type + "" + divid).files[0];
   
    if (type == "Selfie") {
        $("#hdnUserImage").val(imageFile.name);
    }
    else if (type == "Front") {
        $("#hdnImage").val(imageFile.name);
    }
    else {
        $("#hdnBackImage").val(imageFile.name);
    }
    var imageinput = imageid;
    formData.append("imageFile", imageFile);
    formData.append("Type", type);
    //formData.append("coverFile", coverFile);
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/MyPayUser/MyPayUserKYCSaveImages", true);
    xhr.addEventListener("load", function (evt) { UploadComplete(evt, imageinput, type); }, false);
    xhr.addEventListener("error", function (evt) { UploadFailed(evt); }, false);
    xhr.send(formData);
}
function UploadComplete(evt, imageinput, type) {
    debugger;
    if (evt.target.status == 200) {
        var response = evt.currentTarget.response;
        var splitstr = response.split('!@!');
        var hdnimage = "";
        if (response.indexOf('Success') > -1) {
            debugger;
            if (splitstr[0] == "Success") {

                if (type == "Selfie") {
                    //$("#imagePreview").css("background-image", "url(" + jsonData.UserImage + ")");
                    var url = "/UserDocuments/Images/" + splitstr[4];
                    url = url.replaceAll("\"", "");
                    $('#' + imageinput).css("background-image", "url(" + url + ")");
                }
                else if (type == "Front") {
                    $('#' + imageinput).attr("src", "/UserDocuments/Images/" + splitstr[1]);
                }
                else if (type == "Back") {
                    $('#' + imageinput).attr("src", "/UserDocuments/Images/" + splitstr[2]);
                }
            }
        }
        else {
            alert(splitstr[0]);
        }
    }
    else {
        alert("Error Uploading File");
    }
}

function UploadFailed(evt) {
    alert("There was an error attempting to upload the file.");

}

function showhideSpouse() {
    debugger;
    var selectid = $("#marital :selected").val();
    if (selectid !== "0") {
        //hide the divs
        if (selectid == "2") {
            $("#dvSpouse").css("display", "block");
        }
        else {
            $("#dvSpouse").css("display", "none");
        }
    }
}