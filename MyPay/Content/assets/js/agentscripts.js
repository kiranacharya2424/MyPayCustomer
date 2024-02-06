function IsPersonal(evt, value) {
    debugger;
    if (value == "Personal") {
        $("#Personaltab").addClass("active");
        $("#Firmtab").removeClass("active");
        $("#Personal").css("display", "block");
        $("#Firm").css("display", "none");
        $("#IsPersonalorFirm").val("Personal")

    }
    else if (value == "Firm") {
        $("#Personaltab").removeClass("active");
        $("#Firmtab").addClass("active");
        $("#Personal").css("display", "none");
        $("#Firm").css("display", "block");
        $(".cls_persontab_addressdetails").css("display", "none");
        $(".cls_persontab_documentdetails").css("display", "none");
        $(".cls_persontab_bankdetails").css("display", "none");
        $(".cls_persontab_personaldetails").css("display", "block");
        $("#IsPersonalorFirm").val("Firm")
    }
    
}


$(document).ready(function (e) {
    var isvalidate = 0;
    if ($("#AgentUniqueId").val() > 0) {
        var gender = $('#hdn_GenderId').val();
        $('#GenderId').val(gender).trigger('change');

        debugger;
        if ($("#IsPersonalorFirm").val() == "Personal") {
            $("#Personaltab").addClass("active");
            $("#Firmtab").removeClass("active");
            $("#Personal").css("display", "block");
            $("#Firm").css("display", "none");



            if ($("#DocumentTypeId").val() == "4") {
                $("#show_expirydate").css("display", "none");
                $("#show_backimage").css("display", "block");
            }
            else {
                $("#show_backimage").css("display", "none");
                $("#show_expirydate").css("display", "block");
            }


            if ($("#PermanentDistrictId").val!="") {
                $('#PermanentDistrictId').removeAttr("disabled", "disabled");
            }
            if ($("#CurrentDistrictId").val != "") {
                $('#CurrentDistrictId').removeAttr("disabled", "disabled");
            }
            if ($("#PermanentMunicipalityId").val != "") {
                $('#PermanentMunicipalityId').removeAttr("disabled", "disabled");
            }
            if ($("#CurrentMunicipalityId").val != "") {
                $('#CurrentMunicipalityId').removeAttr("disabled", "disabled");
            }

        }
        else {
            $("#Personaltab").removeClass("active");
            $("#Firmtab").addClass("active");
            $("#Personal").css("display", "none");
            $("#Firm").css("display", "block");
        }

    }
    else {
        $("#IsPersonalorFirm").val("Personal");
    }
   
    /////1    TAB START *******************************-----------IN CASE OF PERSON---------****************************************** */


    $(".cls_persontab_addressdetails").css("display", "none");
    $(".cls_persontab_documentdetails").css("display", "none");
    $(".cls_persontab_bankdetails").css("display", "none");


    //A START----------------------PERSONAL DETAILS---------------------------//
    $("#GenderId").change(function () {
        debugger;
        var value = $("#GenderId option:selected").text();
        $("#GenderName").val(value);
        if ($("#GenderId").val() > 0) {
            $("#spanGenderId").text("");
        }
        else {
            $("#spanGenderId").text("Required");
        }
    });
    $("#AgentCategoryId").change(function () {
        debugger;
        var value = $("#AgentCategoryId option:selected").text();
        $("#GenderName").val(value);
        if ($("#AgentCategoryId").val() > 0) {
            $("#spanAgentCategoryId").text("");
        }
        else {
            $("#spanAgentCategoryId").text("Required");
        }
    });
    $("#PEPId").change(function () {
        var value = $("#PEPId option:selected").text();
        $("#PEP").val(value);
       
    });
    $("#OccupationId").change(function () {
        var value = $("#OccupationId option:selected").text();
        $("#Occupation").val(value);
        if ($("#OccupationId").val() > 0) {
            $("#spanOccupationId").text("");
        }
        else {
            $("#spanOccupationId").text("Required");
        }
    });
    $("#NationalityId").change(function () {
        debugger;
        var value = $("#NationalityId option:selected").text();
        $("#Nationality").val(value);
        if ($("#NationalityId").val() > 0) {
            $("#spanNationalityId").text("");
        }
        else {
            $("#spanNationalityId").text("Required");
        }
    });
    $("#MaritalStatusId").change(function () {
        var value = $("#MaritalStatusId option:selected").text();
        $("#MaritalStatus").val(value);
        if ($("#MaritalStatusId").val() > 0) {
            $("#spanMaritalStatusId").text("");
        }
        else {
            $("#spanMaritalStatusId").text("Required");
        }
    });


    function checkvalidation_personal_Details() {

        var FullName = $("#FullName").val();
        var ContactNumber = $("#ContactNumber").val();
        var EmailID = $("#EmailID").val();
        var GenderId = $("#GenderId").val();
        var MaritalStatusId = $("#MaritalStatusId").val();
        var MotherName = $("#MotherName").val();
        var DOB = $("#DOB").val();
        var FatherName = $("#FatherName").val();
        var GrandfatherName = $("#GrandfatherName").val();
        var OccupationId = $("#OccupationId").val();
        var SpouseName = $("#SpouseName").val();
        var NationalityId = $("#NationalityId").val();
        if (FullName == "") {
            $("#spanFullName").text("Required");
            isvalidate = 1
        }
        else
        {
            if (!atleasttwowordsvalidation(FullName)) {
                $("#spanFullName").text("Invalid full name");
                isvalidate = 1
            }
            
        }

        if (ContactNumber == "") {
            $("#spanContactNumber").text("Required");
            isvalidate = 1
        }
        else
        {
            if (!isMobile(ContactNumber)) {
                $("#spanContactNumber").text("Contact number is not valid");
                isvalidate = 1
            }
        }

        if (DOB == "") {
            $("#spanDOB").text("Required");
            isvalidate = 1
        }

        if (EmailID == "") {
            $("#spanEmailID").text("Required");
            isvalidate = 1
        }
        else {
            if (!isEmail(EmailID)) {
                $("#spanEmailID").text("Email is not valid");
                isvalidate = 1
            }
        }

        if (GenderId == "0") {
            $("#spanGenderId").text("Required");
            isvalidate = 1
        }
        if (MaritalStatusId == "0") {
            $("#spanMaritalStatusId").text("Required");
            isvalidate = 1
        }
        if (MotherName == "") {
            $("#spanMotherName").text("Required");
            isvalidate = 1
        }
        else {
            if (!atleasttwowordsvalidation(MotherName)) {
                $("#spanMotherName").text("Invalid mother name");
                isvalidate = 1
            }
        }
        if (FatherName == "") {
            $("#spanFatherName").text("Required");
            isvalidate = 1
        }
        else
        {
            if (!atleasttwowordsvalidation(FatherName)) {
                $("#spanFatherName").text("Invalid father name");
                isvalidate = 1
            }
        }
        if (GrandfatherName == "") {
            $("#spanGrandfatherName").text("Required");
            isvalidate = 1
        }
        else
        {
            if (!atleasttwowordsvalidation(GrandfatherName)) {
                $("#spanGrandfatherName").text("Invalid grandfather name");
                isvalidate = 1
            }
        }
        if (OccupationId == "0") {
            $("#spanOccupationId").text("Required");
            isvalidate = 1
        }

        if (NationalityId == "0") {
            $("#spanNationalityId").text("Required");
            isvalidate = 1
        }

    }

    $("#btnContinue_PERSONAL").click(function (e) {
        debugger;
        isvalidate = 0;
        checkvalidation_personal_Details();
        if (isvalidate == 1) {
            return false;
        }
        $(".cls_persontab_personaldetails").css("display", "none");
        $(".cls_persontab_documentdetails").css("display", "none");
        $(".cls_persontab_bankdetails").css("display", "none");
        $(".cls_persontab_addressdetails").css("display", "block");
        isvalidate = 0;

    });


    //A End----------------------PERSONAL DETAILS---------------------------//



    //B START----------------------Address DETAILS---------------------------//

    $("#PermanentMunicipalityId").change(function () {
        var value = $("#PermanentMunicipalityId option:selected").text();
        $("#PermanentMunicipality").val(value);
        if ($("#PermanentMunicipalityId").val() > 0) {
            $("#spanPermanentMunicipalityId").text("");
        }
        else {
            $("#spanPermanentMunicipalityId").text("Required");
        }
    });
    $("#CurrentMunicipalityId").change(function () {
        var value = $("#CurrentMunicipalityId option:selected").text();
        $("#CurrentMunicipality").val(value);
        if ($("#CurrentMunicipalityId").val() > 0) {
            $("#spanCurrentMunicipalityId").text("");
        }
        else {
            $("#spanCurrentMunicipalityId").text("Required");
        }
    });

    $("#PermanentStateId").change(function () {
        $('#PermanentDistrictId').attr("disabled", "disabled");
        $('#PermanentMunicipalityId').attr("disabled", "disabled");
        var value = $("#PermanentStateId option:selected").text();
        $("#PermanentState").val(value);
        var PermanentStateId = $("#PermanentStateId").val();
        $('#PermanentDistrictId').find('option').remove().end().append(
            '<option value = "0">Select District</option>');
        $('#PermanentMunicipalityId').find('option').remove().end().append(
            '<option value = "0">Select Municipality</option>');
        if (PermanentStateId != 0) {
            $("#spanPermanentStateId").text("");
            $.ajax({
                url: "/User/GetDistrictList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"StateId":"' + PermanentStateId + '"}',
                success: function (data) {
                    $('#PermanentDistrictId')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#PermanentDistrictId");
                    $.each(data, function (index, item) {
                        list.append(new Option(item.Text, item.Value));
                    });
                    $('#PermanentDistrictId').removeAttr("disabled");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            $("#spanPermanentStateId").text("Required");
            return false;
        }
    });

    $("#PermanentDistrictId").on("change", function () {
        $('#PermanentMunicipalityId').attr("disabled", "disabled");
        var PermanentDistrictId = $("#PermanentDistrictId").val();
        var value = $("#PermanentDistrictId option:selected");
        $("#PermanentDistrict").val(value.text());
        $('#PermanentMunicipalityId').find('option').remove().end().append(
            '<option value = "0">Select Municipality</option>');
        if (PermanentDistrictId != 0) {
            $("#spanPermanentDistrictId").text("");
            $.ajax({
                url: "/User/GetMunicipalityList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"DistrictId":"' + PermanentDistrictId + '"}',
                success: function (data) {
                    $('#PermanentMunicipalityId')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#PermanentMunicipalityId");
                    $.each(data, function (index, item) {
                        list.append(new Option(item.Text, item.Value));
                    });
                    $('#PermanentMunicipalityId').removeAttr("disabled");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            $("#spanPermanentDistrictId").text("Required");
            return false;
        }
    });

    $("#CurrentStateId").change(function () {
        $('#CurrentDistrictId').attr("disabled", "disabled");
        $('#CurrentMunicipalityId').attr("disabled", "disabled");
        var value = $("#CurrentStateId option:selected").text();
        $("#CurrentState").val(value);
        var CurrentStateId = $("#CurrentStateId").val();
        $('#CurrentDistrictId').find('option').remove().end().append(
            '<option value = "0">Select District</option>');
        $('#CurrentMunicipalityId').find('option').remove().end().append(
            '<option value = "0">Select Municipality</option>');
        if (CurrentStateId != 0) {
            $("#spanCurrentStateId").text("");
            $.ajax({
                url: "/User/GetDistrictList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"StateId":"' + CurrentStateId + '"}',
                success: function (data) {
                    $('#CurrentDistrictId')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#CurrentDistrictId");
                    $.each(data, function (index, item) {
                        debugger;
                        list.append(new Option(item.Text, item.Value));
                    });
                    $('#CurrentDistrictId').removeAttr("disabled");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            $("#spanCurrentStateId").text("Required");
            return false;
        }
    });

    $("#CurrentDistrictId").on("change", function () {
        var CurrentDistrictId = $("#CurrentDistrictId").val();
        var value = $("#CurrentDistrictId option:selected");
        $("#CurrentDistrict").val(value.text());
        $('#CurrentMunicipalityId').find('option').remove().end().append(
            '<option value = "0">Select Municipality</option>');
        if (CurrentDistrictId != 0) {
            $("#spanCurrentDistrictId").text("");
            $.ajax({
                url: "/User/GetMunicipalityList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"DistrictId":"' + CurrentDistrictId + '"}',
                success: function (data) {
                    $('#CurrentMunicipalityId')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#CurrentMunicipalityId");
                    $.each(data, function (index, item) {
                        list.append(new Option(item.Text, item.Value));
                    });
                    $('#CurrentMunicipalityId').removeAttr("disabled");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            $("#spanCurrentDistrictId").text("Required");
            return false;
        }
    });

    function checkvalidation_personal_AddressDetails() {
        var PermanentAddress = $("#PermanentAddress").val();
        var PermanentStateId = $("#PermanentStateId").val();
        var PermanentDistrictId = $("#PermanentDistrictId").val();
        var PermanentMunicipalityId = $("#PermanentMunicipalityId").val();
        var Permanentward = $("#Permanentward").val();
        var CurrentAddress = $("#CurrentAddress").val();
        var CurrentStateId = $("#CurrentStateId").val();
        var CurrentDistrictId = $("#CurrentDistrictId").val();
        var CurrentMunicipalityId = $("#CurrentMunicipalityId").val();
        var Currentward = $("#Currentward").val();


        if (PermanentAddress == "") {
            $("#spanPermanentAddress").text("Required");
            isvalidate = 1
        }
        if (PermanentStateId == "0") {
            $("#spanPermanentStateId").text("Required");
            isvalidate = 1
        }
        if (PermanentDistrictId == "0") {
            $("#spanPermanentDistrictId").text("Required");
            isvalidate = 1
        }

        if (PermanentMunicipalityId == "0") {
            $("#spanPermanentMunicipalityId").text("Required");
            isvalidate = 1
        }
        if (Permanentward == "") {
            $("#spanPermanentward").text("Required");
            isvalidate = 1
        }
        if (CurrentAddress == "") {
            $("#spanCurrentAddress").text("Required");
            isvalidate = 1
        }
        if (CurrentStateId == "0") {
            $("#spanCurrentStateId").text("Required");
            isvalidate = 1
        }
        if (CurrentDistrictId == "0") {
            $("#spanCurrentDistrictId").text("Required");
            isvalidate = 1
        }
        if (CurrentMunicipalityId == "0") {
            $("#spanCurrentMunicipalityId").text("Required");
            isvalidate = 1
        }

        if (Currentward == "") {
            $("#spanCurrentward").text("Required");
            isvalidate = 1
        }
    }

    $("#btnContinue_ADDRESS").click(function (e) {
        isvalidate = 0;
        checkvalidation_personal_AddressDetails();
        if (isvalidate == 1) {
            return false;
        }
        $(".cls_persontab_personaldetails").css("display", "none");
        $(".cls_persontab_addressdetails").css("display", "none");
        $(".cls_persontab_documentdetails").css("display", "block");
        $(".cls_persontab_bankdetails").css("display", "none");
        isvalidate = 0;
    });

    $("#btnback_ADDRESS").click(function (e) {
        $(".cls_persontab_personaldetails").css("display", "block");
        $(".cls_persontab_addressdetails").css("display", "none");
        $(".cls_persontab_documentdetails").css("display", "none");
        $(".cls_persontab_bankdetails").css("display", "none");

    });
    //B END----------------------ADDRESS DETAILS---------------------------//



    //C START----------------------DOCUMENT DETAILS---------------------------//
    $("#DocumentTypeId").change(function () {
        var value = $("#DocumentTypeId option:selected").text();
        $("#DocumentType").val(value);
        if ($("#DocumentTypeId").val() > 0) {
            $("#spanDocumentTypeId").text("");
            if ($("#DocumentTypeId").val() == "4") {
                $("#show_expirydate").css("display", "none");
                $("#show_backimage").css("display", "block");
            }
            else {
                $("#show_backimage").css("display", "none");
                $("#show_expirydate").css("display", "block");
            }
        }
        else {
            $("#spanDocumentTypeId").text("Required");
        }

    });

    $("#NationalIdProofFront").change(function () {
        if ($("#NationalIdProofFront").val() != "") {
            $("#spanNationalIdProofFront").text("");
        }
        else {
            $("#spanNationalIdProofFront").text("Required");
        }

    });
    $("#NationalIdProofBack").change(function () {
        if ($("#NationalIdProofBack").val() != "") {
            $("#spanNationalIdProofBack").text("");
        }
        else {
            $("#spanNationalIdProofBack").text("Required");
        }

    });
    $("#AgentImage").change(function () {
        if ($("#AgentImage").val() != "") {
            $("#spanAgentImage").text("");
        }
        else {
            $("#spanAgentImage").text("Required");
        }
    });
    $("#PanImage").change(function () {
        if ($("#PanImage").val() != "") {
            $("#spanPanImage").text("");
        }
        else {
            $("#spanPanImage").text("Required");
        }

    });
    $("#IssueDate").change(function () {
        if ($("#IssueDate").val() == "") {
            $("#spanIssueDate").text("Required");
        }
        else {
            $("#spanIssueDate").text("");
        }
    });
    $("#ExpiryDate").change(function () {
        if ($("#ExpiryDate").val() == "") {
            $("#spanExpiryDate").text("Required");
        }
        else {
            $("#spanExpiryDate").text("");
        }
    });
    function checkvalidation_personal_DocumentDetails() {
        var DocumentTypeId = $("#DocumentTypeId").val();
        var DocumentNumber = $("#DocumentNumber").val();
        var IssueDate = $("#IssueDate").val();
        var ExpiryDate = $("#ExpiryDate").val();
        var IssuePlace = $("#IssuePlace").val();
        var NationalIdProofFrontFile = $("#NationalIdProofFrontFile").val();
        var NationalIdProofBackFile = $("#NationalIdProofBackFile").val();
        var AgentImageFile = $("#AgentImageFile").val();
        var PanNumberFile = $("#PanNumberFile").val();
        var PanImageFile = $("#PanImageFile").val();
        var NationalIdProofFront = $("#NationalIdProofFront").val();
        var NationalIdProofBack = $("#NationalIdProofBack").val();
        var AgentImage = $("#AgentImage").val();
        var PanNumber = $("#PanNumber").val();
        var PanImage = $("#PanImage").val();

        if (DocumentTypeId == "0") {
            $("#spanDocumentTypeId").text("Required");
            isvalidate = 1
        }
        if (DocumentTypeId == "4") {

            if (NationalIdProofBack == "") {
                if (NationalIdProofBackFile == "") {
                    $("#spanNationalIdProofBack").text("Required");
                    isvalidate = 1
                }
                
            }

        }
        else {
            if (ExpiryDate == "") {
                $("#spanExpiryDate").text("Required");
                isvalidate = 1
            }
        }
        if (DocumentNumber == "") {
            $("#spanDocumentNumber").text("Required");
            isvalidate = 1
        }
        if (IssueDate == "") {
            $("#spanIssueDate").text("Required");
            isvalidate = 1
        }

        if (NationalIdProofFront == "") {
            if (NationalIdProofFrontFile == "") {
                $("#spanNationalIdProofFront").text("Required");
                isvalidate = 1
            }
          
        }

        if (AgentImage == "" ) {
            if (AgentImageFile == "") {
                $("#spanAgentImage").text("Required");
                isvalidate = 1
            }
          
        }
        if (PanNumber == "" ) {
            if (PanNumberFile == "") {
                $("#spanPanNumber").text("Required");
                isvalidate = 1
            }
          
        }
        if (PanImage == "" ) {
            if (PanImageFile == "") {
                $("#spanPanImage").text("Required");
                isvalidate = 1
            }
           
        }

    }
    $("#btnContinue_DOCUMENT").click(function (e) {
        isvalidate = 0;
        checkvalidation_personal_DocumentDetails();
        if (isvalidate == 1) {
            return false;
        }
        $(".cls_persontab_personaldetails").css("display", "none");
        $(".cls_persontab_addressdetails").css("display", "none");
        $(".cls_persontab_documentdetails").css("display", "none");
        $(".cls_persontab_bankdetails").css("display", "block");
        isvalidate = 0;
    });
    $("#btnback_DOCUMENT").click(function (e) {
        $(".cls_persontab_personaldetails").css("display", "none");
        $(".cls_persontab_addressdetails").css("display", "block");
        $(".cls_persontab_documentdetails").css("display", "none");
        $(".cls_persontab_bankdetails").css("display", "none");

    });

    //C END----------------------DOCUMENT DETAILS---------------------------//



    //D START----------------------BANK DETAILS---------------------------//
    $("#btnback_BANK").click(function (e) {
        isvalidate = 0;
        $(".cls_persontab_personaldetails").css("display", "none");
        $(".cls_persontab_addressdetails").css("display", "none");
        $(".cls_persontab_documentdetails").css("display", "block");
        $(".cls_persontab_bankdetails").css("display", "none");
        if ($("#Username").val()==null) {
            $("#spanUsername").text("Required");
            isvalidate = 1
        }
        if ($("#Password").val() == null) {
            $("#spanPassword").text("Required");
            isvalidate = 1
        }
        if ($("#AgentCategoryId").val() == null) {
            $("#spanAgentCategoryId").text("Required");
            isvalidate = 1
        }
        if (isvalidate==1) {
            return false;
        }
        isvalidate = 0;
    });
    $("#BANK_CD").change(function () {
        debugger;
        var id = $("#BANK_CD").val();
        if (id != null) {
            var value = $("#BANK_CD option:selected");
            $("#BankName").val(value.text());
            $.ajax({
                type: 'POST',
                async: false,
                url: "/Agent/GetBranchNameFromBank",
                dataType: 'json',
                data: { Bankid: id },
                success: function (data) {                
                    $("#BranchName").val(data);
                    $('#BranchName').attr('readonly', true);
                  
                }
            });
        }
    });
    $("#OrganizationBANK_CD").change(function () {
        debugger;
        var id = $("#OrganizationBANK_CD").val();
        if (id != null) {
            var value = $("#OrganizationBANK_CD option:selected");
            $("#OrganizationBankName").val(value.text());
            $.ajax({
                type: 'POST',
                async: false,
                url: "/Agent/GetBranchNameFromBank",
                dataType: 'json',
                data: { Bankid: id },
                success: function (data) {
                    $("#OrganizationBranchName").val(data);
                    $('#OrganizationBranchName').attr('readonly', true);
                }
            });
        }
    });
    //D END----------------------BANK DETAILS---------------------------//


    /////1    TAB END *******************************-----------IN CASE OF PERSON---------****************************************** */



    /////2   TAB START *******************************-----------IN CASE OF FIRM---------****************************************** */

    $(".cls_Firmtab_director").css("display", "none");
    $(".cls_Firmtab_addressdetail").css("display", "none");
    $(".cls_Firmtab_document").css("display", "none");
    $(".cls_Firmtab_bankdetails").css("display", "none");

    $("#btnContinueOrg_ORGANIZATION").click(function (e) {
        isvalidate = 0;
        checkvalidation_Firm_Details();
        if (isvalidate == 1) {
            return false;
        }       
        
        $(".cls_Firmtab_director").css("display", "none");
        $(".cls_Firmtab_organizationdetails").css("display", "none");
        $(".cls_Firmtab_addressdetail").css("display", "block");
        $(".cls_Firmtab_document").css("display", "none");
        $(".cls_Firmtab_bankdetails").css("display", "none");
        isvalidate = 0;

    });
    function checkvalidation_Firm_Details() {
        var OrganizationFullName = $("#OrganizationFullName").val();
        var OrganizationContactNumber = $("#OrganizationContactNumber").val();
        var OrganizationEmail = $("#OrganizationEmail").val();
        var OrganizationRegistrationDate = $("#OrganizationRegistrationDate").val();
        var OrganizationRegistrationNumber = $("#OrganizationRegistrationNumber").val();
        var OrganizationPAN_VATId = $("#OrganizationPAN_VATId").val();
        var OrganizationPAN_VATNumber = $("#OrganizationPAN_VATNumber").val();

        if (OrganizationFullName == "") {
            $("#spanOrganizationFullName").text("Required");
            isvalidate = 1
        }
        if (OrganizationContactNumber == "") {
            $("#spanOrganizationContactNumber").text("Required");
            isvalidate = 1
        }
        else {
            if (!isMobile(OrganizationContactNumber)) {
                $("#spanOrganizationContactNumber").text("Invalid contact number");
                isvalidate = 1
            }
        }

        if (OrganizationEmail == "") {
            $("#spanOrganizationEmail").text("Required");
            isvalidate = 1
        }
        else {
            if (!isEmail(OrganizationEmail)) {
                $("#spanOrganizationEmail").text("Invalid email");
                isvalidate = 1
            }
        }
        if (OrganizationRegistrationDate == "") {
            $("#spanOrganizationRegistrationDate").text("Required");
            isvalidate = 1
        }

        if (OrganizationRegistrationNumber == "") {
            $("#spanOrganizationRegistrationNumber").text("Required");
            isvalidate = 1
        }

        if (OrganizationPAN_VATId == "0") {
            $("#spanOrganizationPAN_VATId").text("Required");
            isvalidate = 1
        }
        if (OrganizationPAN_VATNumber == "") {
            $("#spanOrganizationPAN_VATNumber").text("Required");
            isvalidate = 1
        }

    }
    $("#OrganizationPAN_VATId").change(function () {
        var value = $("#OrganizationPAN_VATId option:selected").text();
        $("#OrganizationPAN_VAT").val(value);
        if ($("#OrganizationPAN_VATId").val() > 0) {
            $("#spanOrganizationPAN_VATId").text("");
        }
        else {
            $("#spanOrganizationPAN_VATId").text("Required");
        }
    });

    $("#btnContinueOrg_ADDRESS").click(function (e) {
        isvalidate = 0;
        checkvalidation_Firm_address();
        if (isvalidate == 1) {
            return false;
        }
        $(".cls_Firmtab_organizationdetails").css("display", "none");
        $(".cls_Firmtab_director").css("display", "none");
        $(".cls_Firmtab_addressdetail").css("display", "none");
        $(".cls_Firmtab_document").css("display", "block");
        $(".cls_Firmtab_bankdetails").css("display", "none");
        isvalidate = 0;

    });
    function checkvalidation_Firm_address() {
        var OrganizationStateId = $("#OrganizationStateId").val();
        var OrganizationDistrictId = $("#OrganizationDistrictId").val();
        var OrganizationMunicipalityId = $("#OrganizationMunicipalityId").val();
        var Organizationward = $("#Organizationward").val();

        if (OrganizationStateId == "0") {
            $("#spanOrganizationStateId").text("Required");
            isvalidate = 1
        }
        if (OrganizationDistrictId == "0") {
            $("#spanOrganizationDistrictId").text("Required");
            isvalidate = 1
        }

        if (OrganizationMunicipalityId == "0") {
            $("#spanOrganizationMunicipalityId").text("Required");
            isvalidate = 1
        }
        if (Organizationward == "") {
            $("#spanOrganizationward").text("Required");
            isvalidate = 1
        }

    }

    $("#OrganizationStateId").change(function () {
        $('#OrganizationDistrictId').attr("disabled", "disabled");
        $('#OrganizationMunicipalityId').attr("disabled", "disabled");
        var value = $("#OrganizationStateId option:selected").text();
        $("#OrganizationState").val(value);
        var OrganizationStateId = $("#OrganizationStateId").val();
        $('#OrganizationDistrictId').find('option').remove().end().append(
            '<option value = "0">Select District</option>');
        $('#OrganizationMunicipalityId').find('option').remove().end().append(
            '<option value = "0">Select Municipality</option>');
        if (OrganizationStateId != 0) {
            $("#spanOrganizationStateId").text("");
            $.ajax({
                url: "/User/GetDistrictList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"StateId":"' + OrganizationStateId + '"}',
                success: function (data) {
                    $('#OrganizationDistrictId')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#OrganizationDistrictId");
                    $.each(data, function (index, item) {
                        list.append(new Option(item.Text, item.Value));
                    });
                    $('#OrganizationDistrictId').removeAttr("disabled");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            $("#spanOrganizationStateId").text("Required");
            return false;
        }
    });

    $("#OrganizationDistrictId").on("change", function () {
        $('#OrganizationMunicipalityId').attr("disabled", "disabled");
        var OrganizationDistrictId = $("#OrganizationDistrictId").val();
        var value = $("#OrganizationDistrictId option:selected");
        $("#OrganizationDistrict").val(value.text());
        $('#OrganizationMunicipalityId').find('option').remove().end().append(
            '<option value = "0">Select Municipality</option>');
        if (OrganizationDistrictId != 0) {
            $("#spanOrganizationDistrictId").text("");
            $.ajax({
                url: "/User/GetMunicipalityList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"DistrictId":"' + OrganizationDistrictId + '"}',
                success: function (data) {
                    $('#OrganizationMunicipalityId')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#OrganizationMunicipalityId");
                    $.each(data, function (index, item) {
                        list.append(new Option(item.Text, item.Value));
                    });
                    $('#OrganizationMunicipalityId').removeAttr("disabled");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            $("#spanOrganizationDistrictId").text("Required");
            return false;
        }
    });

    $("#OrganizationMunicipalityId").change(function () {
        var value = $("#OrganizationMunicipalityId option:selected").text();
        $("#OrganizationMunicipality").val(value);
        if ($("#OrganizationMunicipalityId").val() > 0) {
            $("#spanOrganizationMunicipalityId").text("");
        }
        else {
            $("#spanOrganizationMunicipalityId").text("Required");
        }
    });
   

    $("#btnContinueOrg_DOCUMENT").click(function (e) {
        isvalidate = 0;
        checkvalidation_Firm_document();
        if (isvalidate == 1) {
            return false;
        }
        $(".cls_Firmtab_organizationdetails").css("display", "none");
        $(".cls_Firmtab_director").css("display", "block");
        $(".cls_Firmtab_addressdetail").css("display", "none");
        $(".cls_Firmtab_document").css("display", "none");
        $(".cls_Firmtab_bankdetails").css("display", "none");
        isvalidate = 0;

    });

    function checkvalidation_Firm_document() {
        var OrganizationPAN_VATDocumentFile = $("#OrganizationPAN_VATDocumentFile").val();
        var OrganizationRegistrationDocumentFile = $("#OrganizationRegistrationDocumentFile").val();
        var OrganizationPAN_VATDocument = $("#OrganizationPAN_VATDocument").val();
        var OrganizationRegistrationDocument = $("#OrganizationRegistrationDocument").val();
        var OrganizationMunicipalityId = $("#OrganizationMunicipalityId").val();
        var Organizationward = $("#Organizationward").val();

        if (OrganizationPAN_VATDocument == "") {
            if (OrganizationPAN_VATDocumentFile == "") {
                $("#spanOrganizationPAN_VATDocument").text("Required");
                isvalidate = 1
            }
            
        }
        if (OrganizationRegistrationDocument == "") {
            if (OrganizationRegistrationDocumentFile=="") {
                $("#spanOrganizationRegistrationDocument").text("Required");
                isvalidate = 1
            }
            
        }

    }
    $("#OrganizationPAN_VATDocument").change(function () {
        if ($("#OrganizationPAN_VATDocument").val() != "") {
            $("#spanOrganizationPAN_VATDocument").text("");
        }
        else {
            $("#spanOrganizationPAN_VATDocument").text("Required");
        }

    });
    $("#OrganizationRegistrationDocument").change(function () {
        if ($("#OrganizationRegistrationDocument").val() != "") {
            $("#spanOrganizationRegistrationDocument").text("");
        }
        else {
            $("#spanOrganizationRegistrationDocument").text("Required");
        }

    });
    $("#btnContinueOrg_DIRECTOR").click(function (e) {
        isvalidate = 0;
        checkvalidation_Firm_director();
        if (isvalidate == 1) {
            return false;
        }
        
        $(".cls_Firmtab_organizationdetails").css("display", "none");
        $(".cls_Firmtab_director").css("display", "none");
        $(".cls_Firmtab_addressdetail").css("display", "none");
        $(".cls_Firmtab_document").css("display", "none");
        $(".cls_Firmtab_bankdetails").css("display", "block");
        isvalidate = 0;

    });

    function checkvalidation_Firm_director() {
        var DirectorFullName = $("#DirectorFullName").val();
        var DirectorContactNumber = $("#DirectorContactNumber").val();
        var DirectorEmail = $("#DirectorEmail").val();
        var Directorprofileimage = $("#Directorprofileimage").val();
        var Directorcitizenbackimage = $("#Directorcitizenbackimage").val();
        var Directorcitizenfrontimage = $("#Directorcitizenfrontimage").val();
        var DirectorprofileimageFile = $("#DirectorprofileimageFile").val();
        var DirectorcitizenbackimageFile = $("#DirectorcitizenbackimageFile").val();
        var DirectorcitizenfrontimageFile = $("#DirectorcitizenfrontimageFile").val();

        if (DirectorFullName == "") {
            $("#spanDirectorFullName").text("Required");
            isvalidate = 1
        }
        else {
            if (!atleasttwowordsvalidation(DirectorFullName)) {
                $("#spanDirectorFullName").text("Invalid full name");
                isvalidate = 1
            }
        }
        if (DirectorContactNumber == "") {
            $("#spanDirectorContactNumber").text("Required");
            isvalidate = 1
        }
        else {
            if (!isMobile(DirectorContactNumber)) {
                $("#spanDirectorContactNumber").text("Invalid contact number");
                isvalidate = 1
            }
        }
        if (DirectorEmail == "") {
            $("#spanDirectorEmail").text("Required");
            isvalidate = 1
        }
        else {
            if (!isEmail(DirectorEmail)) {
                $("#spanDirectorEmail").text("Invalid email");
                isvalidate = 1
            }
        }
        if (Directorprofileimage == "") {
            if (DirectorprofileimageFile == "") {
                $("#spanDirectorprofileimage").text("Required");
                isvalidate = 1
            }
           
        }
        if (Directorcitizenfrontimage == "") {
            if (DirectorcitizenfrontimageFile == "") {
                $("#spanDirectorcitizenfrontimage").text("Required");
                isvalidate = 1
            }
           
        }
        if (Directorcitizenbackimage == "") {
            if (DirectorcitizenbackimageFile == "") {
                $("#spanDirectorcitizenbackimage").text("Required");
                isvalidate = 1
            }
           
        }

    }
    $("#btnbackOrg_BANK").click(function (e) {
        isvalidate = 0;
        $(".cls_Firmtab_organizationdetails").css("display", "none");
        $(".cls_Firmtab_director").css("display", "block");
        $(".cls_Firmtab_addressdetail").css("display", "none");
        $(".cls_Firmtab_document").css("display", "none");
        $(".cls_Firmtab_bankdetails").css("display", "none");
        isvalidate = 0;

    });
    $("#btnbackOrg_DIRECTOR").click(function (e) {
        isvalidate = 0;
        $(".cls_Firmtab_organizationdetails").css("display", "none");
        $(".cls_Firmtab_director").css("display", "none");
        $(".cls_Firmtab_addressdetail").css("display", "none");
        $(".cls_Firmtab_document").css("display", "block");
        $(".cls_Firmtab_bankdetails").css("display", "none");
        isvalidate = 0;

    });
    $("#btnbackOrg_DOCUMENT").click(function (e) {
        isvalidate = 0;
        $(".cls_Firmtab_organizationdetails").css("display", "none");
        $(".cls_Firmtab_director").css("display", "none");
        $(".cls_Firmtab_addressdetail").css("display", "block");
        $(".cls_Firmtab_document").css("display", "none");
        $(".cls_Firmtab_bankdetails").css("display", "none");
        isvalidate = 0;

    });
    $("#btnbackOrg_ADDRESS").click(function (e) {
        isvalidate = 0;
        $(".cls_Firmtab_organizationdetails").css("display", "block");
        $(".cls_Firmtab_director").css("display", "none");
        $(".cls_Firmtab_addressdetail").css("display", "none");
        $(".cls_Firmtab_document").css("display", "none");
        $(".cls_Firmtab_bankdetails").css("display", "none");
        isvalidate = 0;

    });
    //// 2    TAB END *******************************-----------IN CASE OF FIRM---------****************************************** */

  
    //$("#btnContinue_BANK").click(function (e) {
    //    debugger;
    //    isvalidate = 0;
    //    checkvalidation_personal_Details();
    //    if (isvalidate == 1) {
    //        return false;
    //    }
    //    $(".cls_persontab_personaldetails").css("display", "none");
    //    $(".cls_persontab_documentdetails").css("display", "none");
    //    $(".cls_persontab_bankdetails").css("display", "none");
    //    $(".cls_persontab_addressdetails").css("display", "block");
    //    isvalidate = 0;
    //    $("#AddAgentform").submit(); 
    //});
   

});

function validate(event) {
    debugger;
    var id = event.currentTarget.id;
    $("#span" + id + "").text("");
}
function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
function isMobile(mobilenumber) {
    var regex = /^[9][0-9]*$/;
    return regex.test(mobilenumber);
}
function atleasttwowordsvalidation(name) {
    var regex = /^[A-Za-z]+(\s\({0,1}[A-Za-z]+\.{0,1}[A-Za-z]*\.{0,1}\){0,1})+$/;
    return regex.test(name);
}

function PreviewImage(input, divid, size) {
    debugger;
    if (input.files && input.files[0]) {
        var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
        if ($.inArray(input.files[0].name.split('.').pop().toLowerCase(), fileExtension) == -1) {
            //ViewMessage("Only formats are allowed : " + fileExtension.join(', '), "warning", '');
            ViewMessage("Only formats are allowed : " + fileExtension.join(', '), "warning");
            $(input).replaceWith($(input).val('').clone(true));
            $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
            $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
            return false;
        }
        if (parseInt(input.files[0].size / 1000) > size) {
            //ViewMessage("Allowed file size exceeded. (Max. " + size + " KB)", "warning", '');
            alert("Allowed file size exceeded. (Max. " + size + " KB)", "warning");
            $(input).replaceWith($(input).val('').clone(true));
            $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
            $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
            return false;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            if (input.files[0].name.split('.').pop().toLowerCase() != "pdf" && input.files[0].name.split('.').pop().toLowerCase() != "txt") {
                $('#target' + divid).attr('src', e.target.result);
                $('#ContentPlaceHolder1_target' + divid).attr("src", e.target.result);
            }
            else {
                $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
                $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
            }
        }
        $("#span" + divid + "").text("");
        reader.readAsDataURL(input.files[0]);
    }
}