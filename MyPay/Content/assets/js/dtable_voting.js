///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;

function BindVotingCompetitionDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Voting Competition"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Voting/GetVotingCompetitionList",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.EventStatus = $("#EnumEventStatus").val();
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }
                        location.reload();
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<img ID="targetImage" class="custom-file-inputrounded-top" width="100" src="/Images/VotingCompetition/' + data.Image + '" alt="">';

                        },
                        bSortable: false,
                        sTitle: "Image"
                    },
                    { "data": "Title", "name": "Title", "autoWidth": true, "bSortable": false },
                    { "data": "TotalVotes", "name": "Total Votes", "autoWidth": true, "bSortable": false },
                    { "data": "TotalFreeVotes", "name": "Free Votes", "autoWidth": true, "bSortable": false },
                    { "data": "TotalAmount", "name": "Total Amount", "autoWidth": true, "bSortable": false },
                    { "data": "PublishTimeDt", "name": "Publish Time", "autoWidth": true, "bSortable": true },
                    { "data": "EndTimeDt", "name": "End Time", "autoWidth": true, "bSortable": true },
                    { "data": "PricePerVote", "name": "Price Per Vote", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.EventStatus == "Running") {
                                return '<span  class="tb-status text-success">' + data.EventStatus + '</span>';
                            }
                            else if (data.EventStatus == "Closed") {
                                return '<span  class="tb-status text-danger">' + data.EventStatus + '</span>';
                            }
                            else {
                                return '<span  class="tb-status text-orange">' + data.EventStatus + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    { "data": "Order", "name": "Order", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsActive) {
                                return '<a href="javascript:void(0);" onclick="return BlockUnblock(' + data.Id + ',&apos;&#47;Voting&#47;VotingCompetitionBlockUnblock&apos;,event,&apos;Competition&apos;)" class="btn btn-success btn-sm btn-icon" style="color:white;"  Title="Enable"><em class="icon ni ni-check-circle"></em></a>';
                            }
                            else {
                                return '<a href="javascript:void(0);" onclick="return BlockUnblock(' + data.Id + ',&apos;&#47;Voting&#47;VotingCompetitionBlockUnblock&apos;,event,&apos;Competition&apos;)" class="btn btn-danger btn-sm btn-icon" style="color:white;" Title="Disable"><em class="icon ni ni-na"></em></a>';
                            }

                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str = '<a href="/Voting/Index?Id=' + data.Id + '"  class="btn btn-primary btn-sm btn-icon"><em class="icon ni ni-edit"></em></a>';
                            str += '&nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(0);" onclick="DeleteVotingCompetition(' + data.Id + ')"  class="btn btn-danger btn-sm btn-icon"><em class="icon ni ni-cross-c"></em></a>';
                            str += '&nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(0);" onclick="SumUpdateVotingCompetition(' + data.Id + ')"  class="btn btn-danger btn-sm btn-icon"><em class="icon ni ni-setting"></em></a>';
                            return str;
                        },
                        bSortable: false,
                        sTitle: "Action"
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;

                }
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

$(document).ready(function () {

    $("#btnSearch").click(function () {

    });
});

function BlockUnblock(id, url, e, datatable) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"Id":"' + id + '"}',
        success: function (response) {
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    $("#dvMsgSuccess").html("successfully updated");
                    debugger;
                    if (datatable == "Candidate") {
                        BindVotingCandidateDataTable();
                    }
                    else if (datatable == "Competition") {
                        BindVotingCompetitionDataTable();
                    }
                    else if (datatable == "Package") {
                        BindVotingPackagesDataTable();
                    }

                    e.preventDefault();
                    e.stopPropagation();
                    return false;
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

}

function BindVotingCandidateDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Voting Candidate"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Voting/GetVotingCandidateList",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.VotingCompetitionId = $("#VotingCompetitionID").val();
                        data.Name = $("#Name").val();
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "CompetitionName", "name": "Competition Name", "autoWidth": true, "bSortable": false },
                    { "data": "Name", "name": "Name", "autoWidth": true, "bSortable": false },
                    { "data": "TotalVotes", "name": "Total Votes", "autoWidth": true, "bSortable": false },
                    { "data": "PaidVotes", "name": "Paid Votes", "autoWidth": true, "bSortable": false },
                    { "data": "FreeVotes", "name": "Free Votes", "autoWidth": true, "bSortable": false },
                    { "data": "TotalAmount", "name": "Total Amount", "autoWidth": true, "bSortable": false },
                    { "data": "Rank", "name": "Rank", "autoWidth": true, "bSortable": false },
                    { "data": "ContentestNo", "name": "Contestant No", "autoWidth": true, "bSortable": false },
                    { "data": "State", "name": "State", "autoWidth": true, "bSortable": false },
                    { "data": "City", "name": "City", "autoWidth": true, "bSortable": false },
                    { "data": "EmailID", "name": "Email", "autoWidth": true, "bSortable": false },
                    { "data": "ContactNo", "name": "Contact No", "autoWidth": true, "bSortable": false },
                    { "data": "Age", "name": "Age", "autoWidth": true, "bSortable": false },
                    { "data": "GenderName", "name": "Gender", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    { "data": "UpdatedByName", "name": "Updated By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsActive) {
                                return '<a href="javascript:void(0);" onclick="return BlockUnblock(' + data.Id + ',&apos;&#47;Voting&#47;VotingCandidateBlockUnblock&apos;,event,&apos;Candidate&apos;)" class="btn btn-success btn-sm btn-icon" style="color:white;"  Title="Enable"><em class="icon ni ni-check-circle"></em></a>';
                            }
                            else {
                                return '<a href="javascript:void(0);" onclick="return BlockUnblock(' + data.Id + ',&apos;&#47;Voting&#47;VotingCandidateBlockUnblock&apos;,event,&apos;Candidate&apos;)" class="btn btn-danger btn-sm btn-icon" style="color:white;" Title="Disable"><em class="icon ni ni-na"></em></a>';
                            }

                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str = '<a href="/Voting/AddVotingCandidates?Id=' + data.Id + '"  class="btn btn-primary btn-sm btn-icon"><em class="icon ni ni-edit"></em></a>';
                            str += '&nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(0);" onclick="DeleteVotingCandidate(' + data.Id + ')"  class="btn btn-danger btn-sm btn-icon"><em class="icon ni ni-cross-c"></em></a>';
                            return str;
                        },
                        bSortable: false,
                        sTitle: "Action"
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;

                }
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindVotingPackagesDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Voting Package"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Voting/GetVotingPackagesList",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.VotingCompetitionId = $("#VotingCompetitionID").val();
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }
                        location.reload();
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "CompetitionName", "name": "Competition Name", "autoWidth": true, "bSortable": false },
                    { "data": "NoOfVotes", "name": "No of Votes", "autoWidth": true, "bSortable": false },
                    { "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsActive) {
                                return '<a href="javascript:void(0);" onclick="return BlockUnblock(' + data.Id + ',&apos;&#47;Voting&#47;VotingPackagesBlockUnblock&apos;,event,&apos;Package&apos;)" class="btn btn-success btn-sm btn-icon" style="color:white;"  Title="Enable"><em class="icon ni ni-check-circle"></em></a>';
                            }
                            else {
                                return '<a href="javascript:void(0);" onclick="return BlockUnblock(' + data.Id + ',&apos;&#47;Voting&#47;VotingPackagesBlockUnblock&apos;,event,&apos;Package&apos;)" class="btn btn-danger btn-sm btn-icon" style="color:white;" Title="Disable"><em class="icon ni ni-na"></em></a>';
                            }

                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="/Voting/AddVotingPackage?Id=' + data.Id + '"  class="btn btn-primary btn-sm btn-icon"><em class="icon ni ni-edit"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "Edit"
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');
                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;
                }
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindVotingListDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            debugger;
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Voting List"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Voting/GetVotingList",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberContactNumber = $("#MemberContactNumber").val();
                        data.MemberName = $("#MemberName").val();
                        data.MemberID = $("#MemberID").val();
                        data.VotingCandidateName = $("#VotingCandidateName").val();
                        data.VotingCompetitionId = $("#VotingCompetitionID").val();
                        data.StartDate = $("#fromdate").val();
                        data.EndDate = $("#todate").val();
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }
                        location.reload();
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "CompetitionName", "name": "Competition Name", "autoWidth": true, "bSortable": false },
                    { "data": "VotingCandidateName", "name": "Candidate Name", "autoWidth": true, "bSortable": false },
                    { "data": "NoofVotes", "name": "No Of Votes", "autoWidth": true, "bSortable": false },
                    { "data": "MemberID", "name": "Member Id", "autoWidth": true, "bSortable": false },
                    { "data": "MemberName", "name": "Member Name", "autoWidth": true, "bSortable": false },
                    { "data": "MemberContactNumber", "name": "Member Contact", "autoWidth": true, "bSortable": false },
                    { "data": "FreeVotes", "name": "Free Votes", "autoWidth": true, "bSortable": false },
                    { "data": "PaidVotes", "name": "Paid Votes", "autoWidth": true, "bSortable": false }

                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');
                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;
                }
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function DeleteVotingCandidate(id) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm("Do you really want to delete Candidate ?")) {
        var candidate = new Object();
        candidate.Id = id;
        if (candidate != null) {
            $.ajax({
                type: "POST",
                url: "/Voting/DeleteVotingCandidate",
                data: JSON.stringify(candidate),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindVotingCandidateDataTable();
                        $("#dvMsgSuccess").html("Candidate successfully deleted");
                        return true;
                    } else {
                        $("#dvMsg").html("Something went wrong");
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
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}

function DeleteVotingCompetition(id) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm("Do you really want to delete Competition ?")) {
        var Competition = new Object();
        Competition.Id = id;
        if (Competition != null) {
            $.ajax({
                type: "POST",
                url: "/Voting/DeleteVotingCompetition",
                data: JSON.stringify(Competition),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindVotingCompetitionDataTable();
                        $("#dvMsgSuccess").html("Competition successfully deleted");
                        return true;
                    } else {
                        $("#dvMsg").html("Something went wrong");
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
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}


function SumUpdateVotingCompetition(id) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm("Do you want to update sum for this Competition ?")) {
        var Competition = new Object();
        Competition.Id = id;
        if (Competition != null) {
            $.ajax({
                type: "POST",
                url: "/Voting/SumUpdateVotingCompetition",
                data: JSON.stringify(Competition),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindVotingCompetitionDataTable();
                        $("#dvMsgSuccess").html("Competition Sum Updated successfully.");
                        return true;
                    } else {
                        $("#dvMsg").html("Something went wrong");
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
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}

function BindRunningVotingCandidateDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Voting Candidate"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Voting/GetVotingCandidateList",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.VotingCompetitionId = null;
                        data.Name = null;
                        data.CheckRunning = "1";
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }
                        location.reload();
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "CompetitionName", "name": "Competition Name", "autoWidth": true, "bSortable": false },
                    { "data": "Name", "name": "Name", "autoWidth": true, "bSortable": false },
                    { "data": "TotalVotes", "name": "Total Votes", "autoWidth": true, "bSortable": false },
                    { "data": "PaidVotes", "name": "Paid Votes", "autoWidth": true, "bSortable": false },
                    { "data": "FreeVotes", "name": "Free Votes", "autoWidth": true, "bSortable": false },
                    { "data": "TotalAmount", "name": "Total Amount", "autoWidth": true, "bSortable": false },
                    { "data": "Rank", "name": "Rank", "autoWidth": true, "bSortable": false },
                    { "data": "ContentestNo", "name": "Contestant No", "autoWidth": true, "bSortable": false },
                    { "data": "State", "name": "State", "autoWidth": true, "bSortable": false },
                    { "data": "City", "name": "City", "autoWidth": true, "bSortable": false },
                    { "data": "EmailID", "name": "Email", "autoWidth": true, "bSortable": false },
                    { "data": "ContactNo", "name": "Contact No", "autoWidth": true, "bSortable": false },
                    { "data": "Age", "name": "Age", "autoWidth": true, "bSortable": false },
                    { "data": "GenderName", "name": "Gender", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    { "data": "UpdatedByName", "name": "Updated By", "autoWidth": true, "bSortable": false },


                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;

                }
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindRunningVotingListDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            debugger;
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Voting List"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Voting/GetVotingList",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberContactNumber = $("#runningMemberContactNumber").val();
                        data.MemberName = $("#runningMemberName").val();
                        data.MemberID = $("#runningMemberID").val();
                        data.VotingCandidateName = $("#runningVotingCandidateName").val();
                        data.VotingCompetitionId = null;
                        data.StartDate = $("#runningfromdate").val();
                        data.EndDate = $("#runningtodate").val();
                        data.CheckRunning = "1";
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }
                        location.reload();
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "CompetitionName", "name": "Competition Name", "autoWidth": true, "bSortable": false },
                    { "data": "VotingCandidateName", "name": "Candidate Name", "autoWidth": true, "bSortable": false },
                    { "data": "NoofVotes", "name": "No Of Votes", "autoWidth": true, "bSortable": false },
                    { "data": "MemberID", "name": "Member Id", "autoWidth": true, "bSortable": false },
                    { "data": "MemberName", "name": "Member Name", "autoWidth": true, "bSortable": false },
                    { "data": "MemberContactNumber", "name": "Member Contact", "autoWidth": true, "bSortable": false },
                    { "data": "FreeVotes", "name": "Free Votes", "autoWidth": true, "bSortable": false },
                    { "data": "PaidVotes", "name": "Paid Votes", "autoWidth": true, "bSortable": false }

                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');
                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;
                }
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}


function BindRunningVotingPackagesDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Voting Package"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Voting/GetVotingPackagesList",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.VotingCompetitionId = "";
                        data.CheckRunning = "1";
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }
                        location.reload();
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "CompetitionName", "name": "Competition Name", "autoWidth": true, "bSortable": false },
                    { "data": "NoOfVotes", "name": "No of Votes", "autoWidth": true, "bSortable": false },
                    { "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },


                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');
                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;
                }
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}
function BindRunningVotingCompetitionDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Voting Competition"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Voting/GetVotingCompetitionList",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.EventStatus = "1";
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }
                        location.reload();
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<img ID="targetImage" class="custom-file-inputrounded-top" width="100" src="/Images/VotingCompetition/' + data.Image + '" alt="">';

                        },
                        bSortable: false,
                        sTitle: "Image"
                    },
                    { "data": "Title", "name": "Title", "autoWidth": true, "bSortable": false },
                    { "data": "TotalVotes", "name": "Total Votes", "autoWidth": true, "bSortable": false },
                    { "data": "TotalFreeVotes", "name": "Free Votes", "autoWidth": true, "bSortable": false },
                    { "data": "TotalAmount", "name": "Total Amount", "autoWidth": true, "bSortable": false },
                    { "data": "PublishTimeDt", "name": "Publish Time", "autoWidth": true, "bSortable": true },
                    { "data": "EndTimeDt", "name": "End Time", "autoWidth": true, "bSortable": true },
                    { "data": "PricePerVote", "name": "Price Per Vote", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.EventStatus == "Running") {
                                return '<span  class="tb-status text-success">' + data.EventStatus + '</span>';
                            }
                            else if (data.EventStatus == "Closed") {
                                return '<span  class="tb-status text-danger">' + data.EventStatus + '</span>';
                            }
                            else {
                                return '<span  class="tb-status text-orange">' + data.EventStatus + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    { "data": "Order", "name": "Order", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },

                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;

                }
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}