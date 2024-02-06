var CandidateData = '';
var GlobalCompetitionId = '';
var ScreenLevel = 0;
var PricePerVote = 0;
var discount = 0;
var payable = 0;

var WebResponse = '';
$(document).ready(function () {

    $('#AjaxLoader').show();
    setTimeout(
        function () {
            VotingsLoad();
        }, 10);
    $("#DivWallet").trigger("click");
});
function VotingsLoad() {
    var objTrans = '';
    $.ajax({
        type: "POST",
        url: "/MyPayUser/MyPayUserVotingList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {

            if (response != null) {

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
                        var VotingStatusMsg = "";
                        var CurrentDate = new Date();
                        var SelectedDate = new Date(jsonData.data[i].EndTimeDt);
                        var publishDate = new Date(jsonData.data[i].PublishTimeDt);
                        if (CurrentDate < publishDate) {
                            VotingStatusMsg = "Start soon";
                        }
                        else if (CurrentDate > SelectedDate) {
                            VotingStatusMsg = "Voting Is Closed";
                        }
                        else {

                            VotingStatusMsg = ("Vote Now");
                        }

                        var delta = 0;
                        var days = 0;
                        var hours = 0;
                        var minutes = 0;
                        if (CurrentDate < publishDate) {
                            delta = Math.abs(new Date(publishDate) - new Date(CurrentDate)) / 1000;
                        }
                        else if (CurrentDate < SelectedDate) {
                            delta = Math.abs(new Date(SelectedDate) - new Date(CurrentDate)) / 1000;

                        }
                        days = Math.floor(delta / 86400);
                        delta -= days * 86400;

                        hours = Math.floor(delta / 3600) % 24;
                        delta -= hours * 3600;

                        minutes = Math.floor(delta / 60) % 60;
                        delta -= minutes * 60;

                        objTrans += '<div class="col-md-6 mb-3" onclick="showvotingdetails(' + jsonData.data[i].Id + ')">';
                        objTrans += '<div class="votingsec p-3 round bg-lighter position-relative">';
                        objTrans += '<img src="' + jsonData.data[i].Image + '" class="w-100">';
                        objTrans += '<div class="d-lg-flex mt-4 align-items-center justify-content-between">';
                        objTrans += '<div class="w-60 w-sm-100">';
                        objTrans += '<h6 class="title">' + jsonData.data[i].Title + ' </h6>';
                        objTrans += '<p>' + jsonData.data[i].Description + '</p>';
                        objTrans += '</div>';
                        objTrans += '<div class="w-40 w-sm-100 text-right">';
                        if (CurrentDate < publishDate) {
                            objTrans += '<a href="javascript:void(0)" class="btn btn-primary btn-sm pt-1 pb-1" ><img src="/Content/assets/MyPayUserApp/images/thumbsup.svg" class="mr-1">' + VotingStatusMsg + '</a>';

                        }
                        else if (CurrentDate < SelectedDate) {
                            objTrans += '<a href="javascript:void(0)" class="btn btn-primary btn-sm pt-1 pb-1" ><img src="/Content/assets/MyPayUserApp/images/thumbsup.svg" class="mr-1">' + VotingStatusMsg + '</a>';

                        }
                        else {
                            objTrans += '<a href="javascript:void(0)" class="btn btn-light btn-sm pt-1 pb-1" >See Details</a>';
                        }
                        objTrans += '</div>';
                        objTrans += '</div>';
                        objTrans += '<div class="vote_timer">';
                        if (CurrentDate < publishDate) {
                            objTrans += '<span>' + days + ' <label>days</label></span>';
                            objTrans += '<span>' + hours + ' <label>hours</label></span>';
                            objTrans += '<span>' + minutes + ' <label>min</label></span>';
                        }
                        else if (CurrentDate < SelectedDate) {
                            objTrans += '<span>' + days + ' <label>days</label></span>';
                            objTrans += '<span>' + hours + ' <label>hours</label></span>';
                            objTrans += '<span>' + minutes + ' <label>min</label></span>';
                        }
                        else {
                            objTrans += '<span class="fw-700  fs-10px btn btn-sm btn-danger ml-auto color-black d-flex align-items-center text-white">Voting is Closed</span>';
                        }
                        objTrans += '</div><!--vote_timer-->';
                        objTrans += '</div><!--votingsec-->';
                        objTrans += '</div>';
                    }
                }
                else {
                    if (!IsValidJson) {
                        $("#dvMessage").html(response);
                    }
                    else {
                        $("#dvMessage").html("No Voting Competition Found");
                    }
                    $("#showmore").css("display", "none");
                }
                $("#dvVotingList").show();
                $("#dvVotingCompetitions").html('');
                $("#dvVotingCompetitions").append(objTrans);
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
            VotingsLoad();
        }, 10);
}
$("#btn_back").click(function () {

    $("#dvPayments").hide();
    $("#DivProceedToPay").hide();
    $("#dvCompetitionDetail").hide();
    $("#dvPackageDetail").hide();
    if (ScreenLevel === 0) {
        window.location.replace("/MyPayUserLogin/Dashboard");
    }
    else if (ScreenLevel === 1) {
        ScreenLevel = 0;
        $("#dvVotingList").show();
    }
    else if (ScreenLevel === 2) {
        ScreenLevel = 1;
        $("#dvCompetitionDetail").show();
        $("#dvEventCandidates").show();
    }
})
function showvotingdetails(Votingid) {

    $('#AjaxLoader').show();
    setTimeout(
        function () {
            if (Votingid != "" || Votingid != "0") {
                var objTrans = '';
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserVotingList",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {

                        if (response != null) {

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
                                    if (jsonData.data[i].Id == Votingid) {

                                        PricePerVote = jsonData.data[i].PricePerVote;
                                        var VotingStatusMsg = "";
                                        var CurrentDate = new Date();
                                        var SelectedDate = new Date(jsonData.data[i].EndTimeDt);
                                        var publishDate = new Date(jsonData.data[i].PublishTimeDt);
                                        if (CurrentDate < publishDate) {
                                            VotingStatusMsg = "Voting will be start";
                                        }
                                        else if (CurrentDate > SelectedDate) {
                                            VotingStatusMsg = "Voting Is Closed";
                                        }
                                        else {

                                            VotingStatusMsg = ("Vote Now");
                                        }
                                        var delta = 0;
                                        var days = 0;
                                        var hours = 0;
                                        var minutes = 0;
                                        if (CurrentDate < publishDate) {
                                            delta = Math.abs(new Date(publishDate) - new Date(CurrentDate)) / 1000;
                                        }
                                        else if (CurrentDate < SelectedDate) {
                                            delta = Math.abs(new Date(SelectedDate) - new Date(CurrentDate)) / 1000;

                                        }
                                        days = Math.floor(delta / 86400);
                                        delta -= days * 86400;

                                        hours = Math.floor(delta / 3600) % 24;
                                        delta -= hours * 3600;

                                        minutes = Math.floor(delta / 60) % 60;
                                        delta -= minutes * 60;


                                        objTrans += '<div class="post_img p-3 round bg-lighter">';
                                        objTrans += '<img src="' + jsonData.data[i].Image + '" class="w-100">';
                                        objTrans += '</div><!--post_img-->';
                                        objTrans += '<div class="card-title d-flex w-100 mt-4 mb-4 align-items-center big-card-title">';
                                        objTrans += '<h4 class="card-title mb-0">' + jsonData.data[i].Title + '</h4><!--card-title-->';
                                        objTrans += '<div class="votinginfo d-flex align-items-center round">';
                                        if (CurrentDate < publishDate) {
                                            objTrans += '<label class="fs-12px mr-1">Voting will be<br> start in: </label>';
                                        }
                                        else if (CurrentDate < SelectedDate) {
                                            objTrans += '<label class="fs-12px mr-1">Voting will be<br> closed in:</label>';
                                        }
                                        objTrans += '<div class="vote_timer">';
                                        if (CurrentDate < publishDate) {
                                            objTrans += '<span>' + days + ' <label>days</label></span>';
                                            objTrans += '<span>' + hours + ' <label>hours</label></span>';
                                            objTrans += '<span>' + minutes + ' <label>min</label></span>';
                                            $("#dvEventCandidates").hide();
                                        }
                                        else if (CurrentDate < SelectedDate) {
                                            objTrans += '<span>' + days + ' <label>days</label></span>';
                                            objTrans += '<span>' + hours + ' <label>hours</label></span>';
                                            objTrans += '<span>' + minutes + ' <label>min</label></span>';
                                            CandidateList(Votingid);
                                        }
                                        else {
                                            objTrans += '<span class="fw-700  fs-10px btn btn-sm btn-danger ml-auto color-black d-flex align-items-center text-white">Voting is Closed</span>';
                                            $("#dvEventCandidates").hide();
                                        }
                                        objTrans += '</div><!--vote_timer-->';
                                        objTrans += '</div><!--votingsec-->';
                                        objTrans += '</div>';
                                        objTrans += '<p class="text-base">' + jsonData.data[i].Description + '</p >';
                                    }
                                }
                            }
                            else {
                                if (!IsValidJson) {
                                    $("#dvMessage").html(response);
                                }
                                $("#showmore").css("display", "none");
                            }
                            ScreenLevel = 1;

                            //CandidateList(Votingid);
                            $("#dvVotingList").hide();
                            $("#dvCompetitionDetail").show();
                            $("#dvEventDetail").html('');
                            $("#dvEventDetail").append(objTrans);
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
            else {
                $("#dvMessage").html("Please select event to see the detail.");
            }
        }, 10);
}
function CandidateList(CompetitionId) {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            if (CompetitionId != "" || CompetitionId != "0") {
                GlobalCompetitionId = CompetitionId;
                CandidateData = '';
                var objTrans = '';
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserVotingCandidateList",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{"CompetitionId":"' + CompetitionId + '"}',
                    async: false,
                    success: function (response) {
                        ;
                        if (response != null) {
                            ;
                            var jsonData;
                            var IsValidJson = false;
                            try {
                                jsonData = $.parseJSON(response);
                                var IsValidJson = true;
                            }
                            catch (err) {

                            }

                            CandidateData = jsonData;
                            if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                                for (var i = 0; i < jsonData.data.length; ++i) {
                                    objTrans += '<div class="col-md-6 mb-3">';
                                    objTrans += '<div class="p-4 bg-lighter round d-flex">';
                                    objTrans += '<div class="participant_info">';
                                    objTrans += '<small class="text-uppercase">' + jsonData.data[i].City + '</small>';
                                    objTrans += '<h6 class="title mt-1">' + jsonData.data[i].Name + '</h6>';
                                    objTrans += '<p class="text-soft">' + jsonData.data[i].Description + '</p>';
                                    objTrans += '<span class="">CN: ' + jsonData.data[i].ContentestNo + '</span>';
                                    objTrans += '</div>';
                                    objTrans += '<div class="text-center">';
                                    objTrans += '<div class="user-avatar xlg mb-3">';
                                    objTrans += '<img src="' + jsonData.data[i].Image + '" class="">';
                                    objTrans += '</div>';
                                    objTrans += '<a href="javascript:void(0)" class="btn btn-primary btn-sm pt-1 pb-1" onclick="PackageList(&apos;' + CompetitionId + '&apos;,&apos;' + jsonData.data[i].UniqueId + '&apos;,&apos;' + jsonData.data[i].Name + '&apos;,&apos;' + jsonData.data[i].Description.replace(/\n|\r/g, "") + '&apos;,&apos;' + jsonData.data[i].ContentestNo + '&apos;,&apos;' + jsonData.data[i].City + '&apos;,&apos;' + jsonData.data[i].Image + '&apos;)"><img src="/Content/assets/MyPayUserApp/images/thumbsup.svg" class="mr-1">Vote</a>';
                                    objTrans += '</div>';
                                    objTrans += '</div>';
                                    objTrans += '</div><!--col-md-4-->';
                                }
                            }
                            else {
                                if (!IsValidJson) {
                                    $("#dvMessage").html(response);
                                }
                                $("#showmore").css("display", "none");
                            }
                            ScreenLevel = 2;

                            $("#dvEventCandidates").show();
                            $("#dvVotingList").hide();
                            $("#dvCompetitionDetail").show();
                            $("#dvSearchResult").html('');
                            $("#dvSearchResult").append(objTrans);
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
            else {
                $("#dvMessage").html("Please select event to see the detail.");
            }
            $('#AjaxLoader').hide();
        }, 10);

}

function PackageList(CompetitionId, UniqueId, Name, Description, ContentestNo, City, Image) {
    ScreenLevel = 2;
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            if (CompetitionId != "" || CompetitionId != "0") {
                var objTrans = '';
                var objModal = '';
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserVotingPackagesList",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{"CompetitionId":"' + CompetitionId + '"}',
                    async: false,
                    success: function (response) {

                        if (response != null) {

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
                                    objTrans += '<div class="col-md-6 mb-3" onclick="PaymentDetail(&apos;' + jsonData.data[i].Id + '&apos;,&apos;' + CompetitionId + '&apos;,&apos;' + UniqueId + '&apos;,&apos;' + Name + '&apos;,&apos;' + ContentestNo + '&apos;,&apos;' + City + '&apos;,&apos;' + Image + '&apos;,&apos;' + jsonData.data[i].NoOfVotes + '&apos;,&apos;' + jsonData.data[i].Amount + '&apos;,&apos;VotingPackage&apos;)">';
                                    objTrans += '<div class="card-inner border p-3 votefee payment-method active cursor-pointer">';
                                    objTrans += '<div class="nk-wg-action">';
                                    objTrans += '<div class="d-flex justify-content-between w-100 pr-2 align-items-center">';
                                    objTrans += '<div class="title d-flex align-items-center"><label>' + jsonData.data[i].NoOfVotes + '</label> Vote</div>';
                                    if (jsonData.data[i].Amount == "0") {
                                        objTrans += '<div class="price">Free</div>';
                                    }
                                    else {
                                        objTrans += '<div class="price">Rs.' + jsonData.data[i].Amount + '</div>';
                                    }
                                    objTrans += '</div>';
                                    objTrans += '<a href="javascript:void(0);" class="text-soft"><em class="icon ni ni-forward-ios pt-1"></em></a>';
                                    objTrans += '</div>';
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
                            objModal += '<div class="form-group">';
                            objModal += '<div class="form-control-wrap">';
                            objModal += '<input type="text" class="form-control form-control-lg" id="loadamount" name="loadamount" autocomplete="off" maxlength="3"  placeholder="Enter No. of Votes" onkeypress="return isValidNumber(this, event);">';
                            /*objModal += '<label class="form-label-outlined" for="loadamount">Number of Votes</label>';*/
                            objModal += '</div>';
                            objModal += '</div>';
                            objModal += '<a href="javascript:void(0)" class="btn btn-primary btn-lg d-block" onclick="PaymentDetail(&apos;&apos;,&apos;' + CompetitionId + '&apos;,&apos;' + UniqueId + '&apos;,&apos;' + Name + '&apos;,&apos;' + ContentestNo + '&apos;,&apos;' + City + '&apos;,&apos;' + Image + '&apos;,&apos;&apos;,&apos;&apos;,&apos;Manual&apos;)">Proceed</a>';

                            $("#namecity").html(City);
                            $("#namecandidate").html(Name);
                            $("#contestantno").html(ContentestNo);
                            $("#imgCandidate").attr("src", Image);

                            $("#dvPackageDetail").show();
                            $("#dvEventCandidates").hide();
                            $("#dvVotingList").hide();
                            $("#dvCompetitionDetail").hide();
                            $("#dvPackages").html('');
                            $("#dvPackages").append(objTrans);
                            $("#dvManualVoteBody").html('');
                            $("#dvManualVoteBody").append(objModal);

                            $("#hfCompetitionId").val(CompetitionId);
                            $("#hfCandidateUniqueId").val(UniqueId);
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
            else {
                $("#dvMessage").html("Please select event to see the detail.");
            }
            $('#AjaxLoader').hide();
        }, 10);
}

function PaymentDetail(Id, CompetitionId, CandidateUniqueId, Name, ContentestNo, City, Image, NoofVotes, Amount, Type) {

    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var objTrans = '';
            var NumberVotes = "";
            var ManualAmount = "";
            if (Type == "Manual") {
                NumberVotes = $("#loadamount").val();
                ManualAmount = NumberVotes;
                if (PricePerVote > 0 || PricePerVote !== "") {
                    ManualAmount = NumberVotes * PricePerVote;
                }
            }
            else {
                NumberVotes = NoofVotes;
                ManualAmount = Amount;
            }
            if (CompetitionId != "" || CompetitionId != "0") {
                if (CandidateUniqueId != "" || CandidateUniqueId != "0") {
                    if (Id != "" || Id != "0") {
                        objTrans += '<div class="card-inner-group">';
                        objTrans += '<div class="partrow border-bottom pb-4 mb-4">';
                        objTrans += '<div class="d-flex align-items-center justify-content-between">';
                        objTrans += '<div class="d-flex align-items-center">';
                        objTrans += '<div class="user-avatar lg">';
                        objTrans += '<img src="' + Image + '" class="">';
                        objTrans += '</div>';
                        objTrans += '<div class="participant_info ml-3">';
                        objTrans += '<small class="text-uppercase" >' + City + '</small>';
                        objTrans += '<h6 class="title mt-1" >' + Name + '</h6>';
                        objTrans += '<span class="text-soft" style="font-weight:600">CN: ' + ContentestNo + '</span></span>';
                        objTrans += '</div>';
                        objTrans += '</div>';
                        objTrans += '<div class="votefee d-flex align-items-center m-0">Number of Votes: <label class="m-0 ml-1">' + NumberVotes + '</label></div>';
                        objTrans += '</div>';
                        objTrans += '</div>';
                        objTrans += '<div class="row">';
                        objTrans += '<div class="col-md-10 col-lg-7 col-xl-6 m-auto">';
                        objTrans += '<div class="card-inner bg-lighter p-4 text-center round" style="border:none">';
                        objTrans += '<span class="fw-medium d-block fs-18px">Rs. ' + ManualAmount + '</span>';
                        objTrans += '<label class="m-0 text-soft">Total Payable Amount</label>';
                        //objTrans += '<div class="d-flex justify-content-between mt-4">';
                        //objTrans += '<span class="text-soft">MyPay Points</span>';
                        //objTrans += '<label class="m-0 fw-medium">MP 0</label>';
                        //objTrans += '</div>';
                        objTrans += '</div>';
                        //objTrans += '<div class="text-center mt-1 promocode">';
                        //objTrans += '<a href="javascript:void(0)" class=""><img src="./images/promocode.png">Apply Promo Code</a>';
                        //objTrans += '</div>';
                        objTrans += '<a href="javascript:void(0)" class="mt-4 btn btn-primary btn-lg d-block" onclick="PaymentOptions(&apos;' + CandidateUniqueId + '&apos;,&apos;' + Id + '&apos;,&apos;' + NumberVotes + '&apos;,&apos;' + ManualAmount + '&apos;,&apos;' + Type + '&apos;)">Proceed</a>';

                        $('#manualvote').modal('hide');
                        $("#dvPayments").show();
                        $("#dvPackageDetail").hide();
                        $("#dvEventCandidates").hide();
                        $("#dvVotingList").hide();
                        $("#dvCompetitionDetail").hide();
                        $("#dvPayments").html('');
                        $("#dvPayments").append(objTrans);

                        $("#hfType").val(Type);
                        $("#hfCandidateUniqueId").val(CandidateUniqueId);
                        $("#hfNoOfVotes").val(NumberVotes);
                        $("#hfPackageId").val(Id);

                        $('#AjaxLoader').hide();
                    }
                    else {
                        $("#dvMessage").html("Please select package to vote.");
                    }
                }
                else {
                    $("#dvMessage").html("Please select candiadte to vote.");
                }
            }
            else {
                $("#dvMessage").html("Please select event to see the detail.");
            }
            $('#AjaxLoader').hide();
        }, 10);
}

function PaymentOptions(CandidateUniqueId, PackageId, NoOfVotes, Amount, Type) {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            if (CandidateUniqueId != "" || CandidateUniqueId != "0") {
                GetBankDetail();
                $("#lblAmount").html(Amount);
                $("#Amount").val($("#lblAmount").html(Amount));
                $("#DivProceedToPay").show();
                $("#dvPayments").hide();
                $("#dvPackageDetail").hide();
                $("#dvEventCandidates").hide();
                $("#dvVotingList").hide();
                $("#dvCompetitionDetail").hide();

                $('#AjaxLoader').hide();
            }
            else {
                $("#dvMessage").html("Please select candiadte to vote.");
            }

            $('#AjaxLoader').hide();
        }, 10);
}

function GetBankDetail() {

    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/GetBankDetail",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: null,
                success: function (response) {

                    var objBank = '';
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
                            if (jsonData.data[i].IsPrimary = true) {
                                objBank += ' <div class="nk-wg-action cursor-pointer">';
                                objBank += ' <div class="nk-wg-action-content d-block">';
                                objBank += ' <div class="d-flex align-items-center">';
                                objBank += ' <em class="icon top-0"><img src="' + jsonData.data[i].ICON_NAME + ' " width="22"></em>';
                                objBank += ' <div class="title">' + jsonData.data[i].BankName + ' </div>';
                                objBank += '</div>';
                                $("#hfBankId").val(jsonData.data[i].Id);
                                objBank += '<div class="text-soft">';
                                objBank += '<div class="d-block"><small class="d-block">' + jsonData.data[i].Name + ' </small>' + jsonData.data[i].AccountNumber + '</div>';
                                objBank += '<div class="d-block text-uppercase text-success fw-bold fs-12px mt-1"><img src="/Content/assets/MyPayUserApp/images/dashboard/primary.png" width="12" height="12"> Primary</div>';
                                objBank += '</div>';
                                objBank += '</div>';
                                objBank += '<a href="javascript:void(0);" class="text-soft"><em class="icon ni ni-forward-ios pt-1"></em></a>';

                            }
                            else {
                                objBank += '<div class="nk-wg-action cursor-pointer">';
                                objBank += '<div class="nk-wg-action-content">';
                                objBank += '<em class="icon"><img src="/Content/assets/MyPayUserApp/images/dashboard/banksm.svg" width="22"></em>';
                                objBank += '<div class="title">Link Your Bank </div>';
                                objBank += '</div>';
                                objBank += '<a href="javascript:void(0);" class="text-soft"><em class="icon ni ni-forward-ios pt-1"></em></a>';
                                objBank += '</div>';
                            }
                        }

                        $("#hfIsBankAdded").val("1");
                    }
                    else {
                        $("#hfIsBankAdded").val("0");

                        objBank += '<div class="nk-wg-action cursor-pointer">';
                        objBank += '<div class="nk-wg-action-content">';
                        objBank += '<em class="icon"><img src="/Content/assets/MyPayUserApp/images/dashboard/banksm.svg" width="22"></em>';
                        objBank += '<div class="title">Link Your Bank </div>';
                        objBank += '</div>';
                        objBank += '<a href="javascript:void(0);" class="text-soft"><em class="icon ni ni-forward-ios pt-1"></em></a>';
                        objBank += '</div>';

                        $("#showmore").css("display", "none");
                    }
                    $("#DivBank").html(objBank);
                    $('#AjaxLoader').hide();
                    return false;
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

$("#btnProceedToPay").click(function () {

    var PaymentMode = $("#hfPaymentMode").val();
    if (PaymentMode == "0" || PaymentMode == "") {
        $("#dvMessage").html("Please select payment option");
        $("#txnMsg").html("Please select payment option");
        $("#DivErrMessage").modal("show");
        return false;
    }
    else if (PaymentMode == "1" && parseFloat($("#lblAmount").html()) > parseFloat($("#spnWalletDashboard").html())) {
        $("#txnMsg").html("Insufficient Balance");
        $("#DivErrMessage").modal("show");
    }
    else {
        //ServiceCharge();
        $("#MobilePopup").text($("#hfNoOfVotes").val());
        $("#AmountPopup").text($("#lblAmount").html());
        $('#PaymentPopup').modal('show');
        $("#dvMessage").html("");
        $("#txnMsg").html("");
    }
});
$("#DivWallet").click(function () {
    $("#PopupText").text('')
    $("#hfPaymentMode").val('1');

    $("#DivWallet").css("background", "antiquewhite");
    $("#DivCoin").css("background", "#fff");
    $("#DivBank").css("background", "#fff");

    $("#DivWallet").attr("class", "card-inner border p-3 payment-method mt-2 active paymentoption");
    $("#DivCoin").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
    $("#DivBank").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");

    if ($("#hfKYCStatus").val() != '3') {
        $("#DivCoin").css("background", "#efefef");
        $("#DivBank").css("background", "#efefef");
    }
    /*CheckCoinBalance(0);*/
})
$("#DivBank").click(function () {
    $("#PopupText").text('')
    if ($("#hfKYCStatus").val() != '3') {
        $("#DivWallet").css("background", "#fff");
        $("#DivCoin").css("background", "#efefef");
        $("#DivBank").css("background", "#efefef");
        $("#PopupText").text('Your KYC should be approved to proceed.')
        $('#PopUpMsg').modal('show');
    }
    else {
        $("#hfPaymentMode").val('2');
        $("#DivWallet").css("background", "#fff");
        $("#DivBank").css("background", "antiquewhite");
        $("#DivCoin").css("background", "#fff");


        $("#DivWallet").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
        $("#DivBank").attr("class", "card-inner border p-3 payment-method mt-2 active paymentoption");
        $("#DivCoin").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");

        if ($("#hfKYCStatus").val() == '3') {
            if ($("#hfIsBankAdded").val() == "0") {
                window.location.href = '/MyPayUser/MyPayUserBankListAll';
            }
        }
    }
    //CheckCoinBalance(0);
});
$("#btnOkPopup").click(function () {
    $('#PaymentPopup').modal('hide');
    $("#DivPin").modal("show");
    $("#Pin").val("");
});
$("#btnPin").click(function () {
    Pay();
});

function Pay() {
    var Type = $("#hfType").val();
    var CandidateUniqueId = $("#hfCandidateUniqueId").val();
    var NoOfVotes = $("#hfNoOfVotes").val();
    var PackageId = $("#hfPackageId").val();
    var Amount= $("#lblAmount").html();

    if (CandidateUniqueId == "") {
        $("#dvMessage").html("Please select Candidate.");
        return false;
    }

    var Mpin = $("#Pin").val();
    if (Mpin == "") {
        $("#dvMessage").html("Please enter Pin");
        return false;
    }
    var PaymentMode = $("#hfPaymentMode").val();
    if (PaymentMode == "0") {
        $("#dvMessage").html("Please select Payment Mode");
        return false;
    }
    else {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserVoting",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"CandidateUniqueId":"' + CandidateUniqueId + '","Type":"' + Type + '","NoOfVotes":"' + NoOfVotes + '","Mpin":"' + Mpin + '","PaymentMode":"' + PaymentMode + '","PackageId":"' + PackageId + '","Amount":"' + Amount + '","BankId":"' + $("#hfBankId").val() + '"}',
                    success: function (response) {
                        if (response == "success") {
                            $("#DivPin").modal("hide");
                            $("#PaymentSuccess").modal("show");
                            $("#dvMessage").html("Success");
                            $('#AjaxLoader').hide();
                            VotingsLoad();
                            return false;
                        }
                        else if (response == "Session Expired") {

                            alert('Logged in from another device.');
                            window.location.href = "/MyPayUserLogin/Index";
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html(response);
                        }
                        else {

                            $('#AjaxLoader').hide();
                            $("#DivPin").modal("hide");
                            $("#txnMsg").html(response);
                            $("#DivErrMessage").modal("show");
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

$("#Pin").keypress(function (event) {
    if (event.keyCode == 13) {
        $('#btnPin')[0].click();
        event.preventDefault();
        event.stopPropagation();
        return false;
    }
});
$("#btnPinBack").click(function () {
    VotingsLoad();
});
$("#btnPinForgot").click(function () {
    window.location.href = "/MyPayUser/MyPayUserChangePin";
});


$('#txtSearch').keyup(function () {
    // Search text
    var text = $(this).val();
    if (text != "") {
        $('.parent').hide();


        // Search and show
        $('.parent .title:contains("' + text + '")').closest('.parent').show();
    }
    else {
        $('.parent').show();
    }

});



$("#search").keyup(function () {
    $("#dvSearchResult").html('');
    $("#dvMessage").html('');
    var searchVal = $("#search").val().toLowerCase();
    var filteredCandidate = CandidateData.data.filter((item) => item.Name.toLowerCase().includes(searchVal));

    var objTrans = '';
    if (filteredCandidate != null && filteredCandidate.length > 0) {
        for (var i = 0; i < filteredCandidate.length; ++i) {
            objTrans += '<div class="col-md-6 mb-3">';
            objTrans += '<div class="p-4 bg-lighter round d-flex">';
            objTrans += '<div class="participant_info">';
            objTrans += '<small class="text-uppercase">' + filteredCandidate[i].City + '</small>';
            objTrans += '<h6 class="title mt-1">' + filteredCandidate[i].Name + '</h6>';
            objTrans += '<p class="text-soft">' + filteredCandidate[i].Description + '</p>';
            objTrans += '<span class="">CN: ' + filteredCandidate[i].ContentestNo + '</span>';
            objTrans += '</div>';
            objTrans += '<div class="text-center">';
            objTrans += '<div class="user-avatar xlg mb-3">';
            objTrans += '<img src="' + filteredCandidate[i].Image + '" class="">';
            objTrans += '</div>';
            objTrans += '<a href="javascript:void(0)" class="btn btn-primary btn-sm pt-1 pb-1" onclick="PackageList(&apos;' + GlobalCompetitionId + '&apos;,&apos;' + filteredCandidate[i].UniqueId + '&apos;,&apos;' + filteredCandidate[i].Name + '&apos;,&apos;' + filteredCandidate[i].Description.replace(/\n|\r/g, "") + '&apos;,&apos;' + filteredCandidate[i].ContentestNo + '&apos;,&apos;' + filteredCandidate[i].City + '&apos;,&apos;' + filteredCandidate[i].Image + '&apos;)"><img src="/Content/assets/MyPayUserApp/images/thumbsup.svg" class="mr-1">Vote</a>';
            objTrans += '</div>';
            objTrans += '</div>';
            objTrans += '</div><!--col-md-4-->';
        }
        $("#dvSearchResult").html('');
        $("#dvSearchResult").append(objTrans);
    }
    else {

        $("#dvMessage").html("No Data Found.");
    }
})

function GetScratchedCoupons() {
    var ServiceId = $("#hdnServiceID").val();
    console.log(ServiceId);

    $('#AjaxLoader').show();
    setTimeout(
        function () {

            $.ajax({
                url: "MyPayUser/MyPayUserGetScratchedCoupon",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                data: '{"ServiceId":"' + ServiceId + '"}',
                success: function (response) {
                    var jsonData = $.parseJSON(response);
                    var str = "";
                    for (var i = 0; i < jsonData.data.length; ++i) {
                        str += '<div class="col-md-2 mb-3">';
                        str += '<div class="votingsec p-3 round bg-lighter position-relative couponsscratched-wt">';
                        str += '<div class="scratchinner">';
                        str += ' <label>' + jsonData.data[i].CouponCode + '</label>'
                        //str += ' <label>' + jsonData.data[i].MaximumAmount + '</label>'
                        str += ' <label class="percent">' + jsonData.data[i].CouponPercentage + '%</label>'
                        str += ' <label>' + jsonData.data[i].Description + '</label>'
                        str += '<a id="btn_applycoupon" class="btn btn-sm btn-danger ml-auto color-black d-flex align-items-center text-white " data-dismiss="modal" onclick="sethiddenvalue(&apos;' + jsonData.data[i].CouponPercentage + '&apos;,&apos;' + jsonData.data[i].CouponCode + '&apos;,&apos;' + jsonData.data[i].MaximumAmount + '&apos;,&apos;' + jsonData.data[i].MinimumAmount + '&apos;) "> ApplyCoupon</a>';
                        str += '</div></div></div>';
                    }
                    $("#scratchdiv").html('');
                    $("#scratchdiv").append(str);


                },
                error: function () {
                    alert(" errorr");
                }

            });
            $('#AjaxLoader').hide();
        }, 100);
}


function sethiddenvalue(CouponPercentage, CouponCode, MaximumAmount, MinimumAmount) {
    console.log(CouponCode);
    $('#hdncp').val(CouponPercentage);
    $('#btnapplycoupon').hide();
    $('#btnremovecoupon').show();
    var AmountToPay = parseFloat($("#Amount").val());
    console.log(AmountToPay);
    var percent = parseFloat(CouponPercentage);
    discount = parseFloat(AmountToPay * percent) / 100;
    if (MaximumAmount > 0 && MaximumAmount < discount) {
        discount = parseFloat(MaximumAmount);
    }
    if (MinimumAmount > 0 && MinimumAmount > discount) {
        discount = parseFloat(MinimumAmount);
    }

    var fee = $('#spnCreditCardFee').text();
    if (fee != '') {
        AmountToPay = AmountToPay + parseFloat(fee);
    }
    payable = AmountToPay - discount;
   
    $("#lblAmount").text(payable);
    $("#lblDelAmount").text(AmountToPay);
    $("#dvcouponapply").show();
    $('#hdncouponcode').val(CouponCode);
    
}

function RemoveCoupon() {

    $('#btnapplycoupon').show();
    $('#btnremovecoupon').hide();
    discount = 0;
    var AmountToPay = parseFloat($("#Amount").val());
    var fee = $('#spnCreditCardFee').text();
    if (fee != '') {
        AmountToPay = AmountToPay + parseFloat(fee);
    }

    $("#lblAmount").text(AmountToPay);
    $("#dvcouponapply").hide();
}
function isValidNumber(el, evt) {
   var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode == 46) {
        return false;
    }

    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}