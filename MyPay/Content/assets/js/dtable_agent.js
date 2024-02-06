var table;
/*var buttontype = "";*/

function BindAgentDataTable() {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            /* var USerName = $("#UserName").val();*/
            var FullName = $("#FullName").val();
            var Mobile = $("#ContactNumber").val();
            var Name = $("#FirstName").val();
            var Email = $("#Email").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();

            table = $('#tblagentlist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Agent"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Agent/GetAgentLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.ContactNumber = Mobile;
                        data.Email = Email;
                        data.StartDate = fromdate;
                        data.ToDate = todate;
                        data.FullName = FullName;

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
                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": false },
                    { "data": "FullName", "name": "Full Name", "autoWidth": true, "bSortable": false },
                    { "data": "ContactNumber", "name": "Contact Number", "autoWidth": true, "bSortable": false },
                    { "data": "AgentCategory", "name": "Category", "autoWidth": true, "bSortable": false },
                    { "data": "EmailID", "name": "Email", "autoWidth": true, "bSortable": false },
                    { "data": "AvailableBalance", "name": "Available Balance", "autoWidth": true, "bSortable": false },
                    { "data": "CommissionBalance", "name": "Commission Balance", "autoWidth": true, "bSortable": false },
                    { "data": "AvailableCoins", "name": "Available Coins", "autoWidth": true, "bSortable": false },
                    { "data": "NoOfAgent", "name": "No of Agent", "autoWidth": true, "bSortable": false },
                    { "data": "NoOfUser", "name": "No of Users", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {

                            if (data.IsActive) {
                                return '<span class="tb-status text-success">Active</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">Suspended</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Status"
                    },
                    { "data": "CreatedBy", "name": "Created By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str = '<ul class="nk-tb-actions gx-1">';
                            str += '<li class="nk-tb-action-hidden">';
                            str += '<a href="Agent/AddAgent?AgentUniqueId=' + data.AgentUniqueId + '" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="View Details">';
                            str += '<em class="icon ni ni-eye"></em>';
                            str += '</a>';
                            str += '</li>';
                            if (data.IsActive) {
                                str += '<li class="nk-tb-action-hidden">';
                                str += '<a href="javascript:void(0);" onclick="return BlockUnblock(&apos;' + data.AgentUniqueId + '&apos;,event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Disable">';
                                str += '<em class="icon ni ni-user-cross-fill"></em>';
                                str += '</a>';
                                str += '</li>';
                            }
                            else {
                                str += '<li class="nk-tb-action-hidden">';
                                str += '<a href="javascript:void(0);" onclick="return BlockUnblock(&apos;' + data.AgentUniqueId + '&apos;,event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Enable">';
                                str += '<em class="icon ni ni-user-check-fill"></em>';
                                str += '</a>';
                                str += '</li>';
                            }
                            str += '<li>';
                            str += '<div class="drodown">';
                            str += '<a href="javascript:void(0);" class="dropdown-toggle btn btn-icon btn-trigger" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>';
                            str += '<div class="dropdown-menu dropdown-menu-right">';
                            str += '<ul class="link-list-opt no-bdr">';
                            str += '<li><a href="Agent/AddAgent?AgentUniqueId=' + data.AgentUniqueId + '""><em class="icon ni ni-eye"></em><span>View Details</span></a></li>';
                            str += '<li class="divider"></li>';
                            //str += '<li><a href="MerchantCommissionSetup?MerchantUniqueId=' + data.MerchantUniqueId + '" title="Setup Commission"><em class="icon ni ni-plus-circle-fill"></em><span>Setup Commission</span></a></li>';
                            if (data.IsActive) {
                                str += '<li><a href="javascript:void(0);" onclick="return BlockUnblock(&apos;' + data.AgentUniqueId + '&apos;,event)"><em class="icon ni ni-user-cross-fill"></em><span>Disable Agent</span></a></li>';
                            }
                            else {
                                str += '<li><a href="javascript:void(0);" onclick="return BlockUnblock(&apos;' + data.AgentUniqueId + '&apos;,event)"><em class="icon ni ni-user-check-fill"></em><span>Enable Agent</span></a></li>';
                            }
                            str += '<li><a href="Agent/CreditDebit?AgentUniqueId=' + data.AgentUniqueId + '"><em class="icon ni ni-wallet-alt"></em><span>Update Wallet</span></a></li>';


                            str += '</ul>';
                            str += '</div>';
                            str += '</div>';
                            str += '</li>';
                            str += '</ul>';
                            return str;
                        },
                        bSortable: false,
                        sTitle: ""
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            document.getElementById("tblagentlist").deleteTFoot();

            $("#tblagentlist").append(
                $('<tfoot/>').append($("#tblagentlist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}
function BlockUnblock(agentid, e) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/Agent/AgentBlockUnblock",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"AgentUniqueId":"' + agentid + '"}',
        success: function (response) {
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    $("#dvMsgSuccess").html("successfully updated");
                    var tableId = $(this).data("table");
                    BindAgentDataTable(tableId);
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

$("#btnSubmitAgentTransfer").click(function () {
    $("#dvMessage").html("");
    if ($("#TransactionAmount").val() == "" || $("#TransactionAmount").val() == "0" || parseFloat($('#TransactionAmount').val()) == 0) {
        $("#dvMessage").html("Please enter Amount.");
        return false;
    }
    else if ($("#TransactionType").val() == "" || $("#TransactionType").val() == "0") {
        $("#dvMessage").html("Please Select Transaction Type.");
        return false;
    }
    else {
        if ($("#Type").val() == "1") {
            return confirm('This will update agent wallet. Do you want to continue ?');
        }
        else if ($("#Type").val() == "0") {
            return confirm('This will update agent MP Coins. Do you want to continue ?');
        }
    }

});




/////--------------------------------------------------------Start Agent Category Module------------------------------------------------------////
function BindAgentCategoryDataTable() {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            debugger;
            /* var USerName = $("#UserName").val();*/
            var Category = $("#drpCategory").val();
            var Status = $("#drpStatus").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();

            table = $('#tblagentlist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Agent"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Agent/GetAgentCategory",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        debugger;
                        data.Status = Status;
                        data.Category = Category;
                        /* data.CategoryId = CategoryId;*/
                        data.StartDate = fromdate;
                        data.ToDate = todate;

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
                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": false },
                    { "data": "Category", "name": "Category Name", "autoWidth": true, "bSortable": false },
                    { "data": "NoOfAssignedAgent", "name": "No. of assigned Agent", "autoWidth": true, "bSortable": false },
                    { "data": "Status", "name": "Status", "autoWidth": true, "bSortable": false },


                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str = '<ul class="nk-tb-actions gx-1">';
                            str += '<li class="nk-tb-action-hidden">';
                            str += '<a href="javascript:void(0);" onclick="return ViewAgentCategory(&apos;' + data.AgentCategoryId + ',' + data.Category + '&apos;,event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="View Details">';
                            //str += '<a href="Agent/AddAgent?AgentUniqueId=' + data.Category + '" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="View Details">';
                            str += '<em class="icon ni ni-eye"></em>';
                            str += '</a>';
                            str += '</li>';


                            str += '<li class="nk-tb-action-hidden">';
                            str += '<a href="javascript:void(0);" onclick="return EditAgentCategory(&apos;' + data.AgentCategoryId + ',' + data.Category + '&apos;,event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Edit">';

                            //str += '<a href="Agent/AddAgent?AgentUniqueId=' + data.Category + '" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Edit">';
                            str += '<em class="icon ni ni-edit"></em>';
                            str += '</a>';
                            str += '</li>';


                            str += '<li class="nk-tb-action-hidden">';
                            //str += '<a href="javascript:void(0);" onclick="return BlockUnblock(&apos;' + data.AgentUniqueId + '&apos;,event)"><em class="icon ni ni-user-check-fill"></em><span>Enable Agent</span></a>';

                            str += '<a href="javascript:void(0);" onclick="return DeleteAgentCategory(&apos;' + data.AgentCategoryId + '&apos;,event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Delete">';
                            str += '<em class="icon ni ni-trash"></em>';
                            str += '</a>';
                            str += '</li>';

                            str += '</ul>';
                            return str;
                        },
                        bSortable: false,
                        sTitle: ""
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    debugger;
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            document.getElementById("tblagentlist").deleteTFoot();

            $("#tblagentlist").append(
                $('<tfoot/>').append($("#tblagentlist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function DeleteAgentCategory(Categoryid, e) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/Agent/DeleteAgentCategory",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"AgentCategoryId":"' + Categoryid + '"}',
        success: function (response) {
            if (response != null) {
                var message = response.message;
                if (response.Id == "1") {
                    // $("#dvMsg").html("Records not updated.");


                    $('#dvMsg').text(message);
                    $('#dvMsg1').show();

                    // Hide the message after 10 seconds
                    setTimeout(function () {
                        $('#dvMsg').text("");
                        $('#dvMsg1').hide();
                    });


                    return false;
                }
                else {
                    //$("#dvMsgSuccess").html("successfully updated");
                    var tableId = $(this).data("table");
                    BindAgentCategoryDataTable(tableId);

                    if (response.Id == "0") {
                        // Display the message
                        $('#dvMsgSuccess').text(message);
                        $('#dvMsgSuccess1').show();

                        // Hide the message after 10 seconds
                        setTimeout(function () {
                            debugger;
                            $('#dvMsgSuccess').text("");
                            $('#dvMsgSuccess1').hide();

                        }, 10000); // 10 seconds in milliseconds
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

function EditAgentCategory(Categoryid, Category, e) {
    //var hashedCategoryid = hashPassword(Categoryid);
    window.location.href = "/Agent/EditAgentCategory?AgentCategoryId=" + Categoryid 
}
//function hashPassword(password) {
//    // Hash the password using SHA-256
//    var hashedPassword = CryptoJS.SHA256(password).toString(CryptoJS.enc.Base64);
//    return hashedPassword;
//}
function ViewAgentCategory(Categoryid, e) {
    window.location.href = "/Agent/ViewAgentCategory?AgentCategoryId=" + Categoryid 
}



/////--------------------------------------------------------End Agent Category Module------------------------------------------------------////

/////////////////----------------------------------------Start Agent Commission Module (Edit of agent category)----------------------------------------------------------/////
$(document).ready(function () {

    $("#agent_SearchCommission").on("click", function () {
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        var CategoryId = $('#hdn_AgentCategoryId').val();
        if (CategoryId == "") {
            $("#dvMsg").html("Category is required.");
        }
        else {
            BindAgentCommissionDataTable("tbllist");
        }
    });
   
    $("#drpProvider").on("change", function () {
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        debugger;
        var ProviderId = $("#drpProvider").val();
        var value = $("#drpProvider option:selected");
        if (ProviderId != 0) {
            $.ajax({
                url: "/Commission/GetProviderServiceList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"ProviderId":"' + ProviderId + '"}',
                success: function (data) {
                    debugger;

                    $('#drpProviderServices')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#drpProviderServices");
                    $.each(data, function (index, item) {
                        debugger;
                        list.append(new Option(item.Text, item.Value));
                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            return;
            // alert("Please select Provider ");
        }
    });

    $("#drpProviderServices").on("change", function () {

        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
    });
    $("#agent_SearchCommission_V").on("click", function () {
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        var CategoryId = $('#hdn_AgentCategoryId_V').val();
        if (CategoryId == "") {
            $("#dvMsg").html("Category is required.");
        }
        else {
            BindAgentCommissionDataTable_View("tbllist");
        }
    });
    $("#drpProvider_V").on("change", function () {
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        debugger;
        var ProviderId = $("#drpProvider_V").val();
        var value = $("#drpProvider option:selected");
        if (ProviderId != 0) {
            $.ajax({
                url: "/Commission/GetProviderServiceList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"ProviderId":"' + ProviderId + '"}',
                success: function (data) {
                    debugger;

                    $('#drpProviderServices_V')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#drpProviderServices_V");
                    $.each(data, function (index, item) {
                        debugger;
                        list.append(new Option(item.Text, item.Value));
                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            return;
            // alert("Please select Provider ");
        }
    });

    $("#drpProviderServices_V").on("change", function () {

        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
    });


  
});

function BindAgentCommissionDataTable(tableId) {
    var CategoryId = $('#hdn_AgentCategoryId').val();
    var AgentCategory = $('#AgentCategory').val();
    var ProviderType = $("#drpProvider").val()
    var ServiceId = $("#drpProviderServices").val()
    //var CategoryId = $("#drpProviderServices").val()
    if (ProviderType != "0" && (CategoryId != "")) {
        $("#tbllist").show();
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                table = $('#tbllist').DataTable({
                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No Active Commissions  "
                    },
                    "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                    "pageLength": 50,
                    "processing": true,
                    "serverSide": true,
                    "destroy": true,
                    "searching": false,
                    "sorting": false,
                    "order": [[0, "desc"]],
                    "ajax": {
                        "url": "/Agent/GetAgentCommissionLists",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            data.IsDeleted = 0;
                            data.AgentCategoryId = $('#hdn_AgentCategoryId').val();
                            data.AgentCategory = $('#AgentCategory').val();
                            data.ProviderType = $("#drpProvider").val();
                            data.ServiceId = $("#drpProviderServices").val();
                            //data.ButtonType = buttontype;
                            //data.GenderType = $("#GenderTypeEnum").val();
                            //data.KycType = $("#KycTypeEnum").val();
                            data.ScheduleStatus = $("#EnumScheduleStatus").val();
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
                        { "data": "Sno.", "name": "Sno", "autoWidth": true, "bSortable": false },
                        { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": false },
                        { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": false },
                        {
                            data: null,
                            render: function (data, type, row) {
                                debugger;
                                if (data.ServiceId > 0) {
                                    return '<input type="hidden" id="ServiceId' + data.Id + '" value="' + data.ServiceId + '"/><span>' + data.ServiceName.toUpperCase() + '</span> ';
                                }
                                else {
                                    var dropdownOptions = data.ProviderservicceList;
                                    // Creating the dropdown HTML
                                    var dropdownHtml = '<select style="width:90px;" id="ServiceId' + data.Id + '" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid">';
                                    // Adding options to the dropdown
                                    dropdownOptions.forEach(function (Options) {
                                        if (Options.Text == "Select Provider Services") {
                                            Options.Text = "Select"
                                        }
                                        dropdownHtml += '<option value="' + Options.Value + '">' + Options.Text + '</option>';
                                    });

                                    // Closing the dropdown HTML
                                    dropdownHtml += '</select>';

                                    // Returning the generated HTML
                                    return dropdownHtml;
                                }

                                //return '<select id="ServiceId' + data.ServiceId + '" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid">';

                                // return '<input type="hidden" id="ServiceId' + data.Id + '" value="' + data.ServiceId + '"/><span>' + data.ServiceName.toUpperCase() + '</span> ';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span class="tb-status text-danger" > * </span><input type="text" id="MinimumAmount' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MinimumAmount + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span class="tb-status text-danger" > * </span><input type="text" id="MaximumAmount' + data.Id + '"  maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MaximumAmount + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        //{
                        //    data: null,
                        //    render: function (data, type, row) {
                        //        return '<input type="text"  id="FixedCommission' + data.Id + '"  maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.FixedCommission + '" onkeypress="return isNumberKey(this, event);">';
                        //    },
                        //    autoWidth: true,
                        //    bSortable: false
                        //},
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="PercentageCommission' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.PercentageCommission + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="PercentageRewardPoints' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.PercentageRewardPoints + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="PercentageRewardPointsDebit' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.PercentageRewardPointsDebit + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MinimumAllowed' + data.Id + '" Minlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MinimumAllowed + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MaximumAllowed' + data.Id + '" maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MaximumAllowed + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                //var objIsDisables = (data.GenderTypeName.toUpperCase() == "ALL" && data.KycTypeName.toUpperCase() == "ALL") ? "" : "disabled";
                                var objIsDisables = "";
                                return '<input type="text" id="ServiceCharge' + data.Id + '" ' + objIsDisables + ' maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.ServiceCharge + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MinimumAllowedSC' + data.Id + '" Minlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MinimumAllowedSC + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MaximumAllowedSC' + data.Id + '" maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MaximumAllowedSC + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },


                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="ChildTxnRate' + data.Id + '" Minlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.ChildTxnRate + '" onkeypress="return isNumberKey(this, event);">';
                            },                           
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).addClass('Clschildcommission'); // Add your custom class name
                            },
                             autoWidth: true,
                            bSortable: false,
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="ChildTxnMinAmt' + data.Id + '" maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.ChildTxnMinAmt + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).addClass('Clschildcommission'); // Add your custom class name
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="ChildTxnMaxAmt' + data.Id + '" Minlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.ChildTxnMaxAmt + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).addClass('Clschildcommission'); // Add your custom class name
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MonthlyMinAmt' + data.Id + '" maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MonthlyMinAmt + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).addClass('Clschildcommission'); // Add your custom class name
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MonthlyMaxAmt' + data.Id + '" Minlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MonthlyMaxAmt + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).addClass('Clschildcommission'); // Add your custom class name
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MonthlyBonus' + data.Id + '" maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MonthlyBonus + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).addClass('Clschildcommission'); // Add your custom class name
                            },
                            autoWidth: true,
                            bSortable: false
                        },




                        //{
                        //    data: null,
                        //    render: function (data, type, row) {
                        //        var objGenderType = '<input type="hidden" id="GenderTypeId' + data.Id + '" value="' + data.GenderType + '"/>';
                        //        objGenderType = objGenderType + '<span  class="tb-status text-' + (data.GenderTypeName == "All" ? "success" : "") + '">' + data.GenderTypeName + '</span>';
                        //        return objGenderType;
                        //    },
                        //    autoWidth: true,
                        //    bSortable: false
                        //},
                        //{
                        //    data: null,
                        //    render: function (data, type, row) {
                        //        var objKycType = '<input type="hidden" id="KycTypeId' + data.Id + '" value="' + data.KycType + '"/>';
                        //        objKycType = objKycType + '<span  class="tb-status text-' + (data.KycTypeName == "NotVerified" ? "danger" : "success") + '">' + data.KycTypeName + '</span>';
                        //        return objKycType;
                        //    },
                        //    autoWidth: true,
                        //    bSortable: false
                        //},
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text" id="FromDateDT' + data.Id + '"  readonly=readonly class="form-control form-control-md form-control-outlined datePicker dtdatecontrol_inGrid" value="' + data.FromDateDT + '">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text" id="ToDateDT' + data.Id + '"  readonly=readonly class="form-control form-control-md form-control-outlined datePicker dtdatecontrol_inGrid" value="' + data.ToDateDT + '">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var objScheduleStatusType = '<span  class="tb-status text-' + (data.ScheduleStatus != "Running" ? "danger" : "success") + '">' + data.ScheduleStatus + '</span>';
                                return objScheduleStatusType;
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var actioncolumn = '<table><tr>';
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateCommission' + data.Id + '"  class="btn btn-sm btn-success btncommissionAction"  title="Update Commission" onclick=\'return updatecommission(\"' + data.Id + '\");\'  class="btn btn-primary "> Save </a></td>';
                                if (data.IsActive == true) {
                                    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);"  class="btn btn-sm btn-success btncommissionAction"   title="UnPublish Commission" onclick=\'return statusupdateAgentcommission(\"' + data.Id + '\",\"' + "false" + '\",);\' "> ' + "UnPublish" + ' </a></td>';
                                }
                                else {
                                    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);"  class="btn btn-sm btn-danger btncommissionAction"  title="Publish Commission" onclick=\'return statusupdateAgentcommission(\"' + data.Id + '\",\"' + "true" + '\",);\' "> ' + "Publish" + ' </a></td>';
                                }
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" onclick=\'return DeleteAgentCommission(\"' + data.Id + '\");\' class="ml-2 btn btn-sm btn-outline-danger btn-icon btn-tooltip btncommissionAction" title="Delete"><em class="icon ni ni-trash"></em></a></td>';
                                actioncolumn = actioncolumn + '</tr></table>'
                                return actioncolumn;
                            },
                            bSortable: false,
                            sTitle: "Action"
                        }


                    ]
                });

                if (document.getElementById("tbllist") != undefined) {
                    document.getElementById("tbllist").deleteTFoot();
                }

                $("#tbllist").append(
                    $('<tfoot/>').append($("#tbllist thead tr").clone())
                );
                $('#AjaxLoader').hide();

                /*buttontype = "";*/
            }, 100);
    }
    else {
        $("#tbllist").hide();
    }
    $("#anchor_addCommissionRow").show();
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#tbllist_length").hide();
    $(".dt-buttons btn-group flex-wrap").hide();

    setTimeout(function () {
        $('.datePicker').datepicker({
            format: 'd M yyyy',
            startDate: '+0d'
        });
    }, 1000);

    //$('.datePicker').datepicker({
    //    format: 'd M yyyy',
    //    startDate: '+0d'
    //});

    //(".btncommissionAction").tooltip();
}
function GetTodayDate() {
    var tdate = new Date();
    var dd = tdate.getDate(); //yields day
    var MM = tdate.getMonth(); //yields month
    var yyyy = tdate.getFullYear(); //yields year
    var currentDate = dd + "-" + (MM + 1) + "-" + yyyy;

    return currentDate;
}
function AddAgentCommissionRow() {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var CategoryId = $('#hdn_AgentCategoryId').val();
    var ProviderId = $("#drpProvider").val();
    var ServiceId = $("#drpProviderServices").val();
    var GenderType = $("#GenderTypeEnum").val();
    var KycType = $("#KycTypeEnum").val();
    if (ProviderId == "0") {
        $("#dvMsg").html("Please select Provider Type");
    }
    //else if (ServiceId == "0") {
    //    $("#dvMsg").html("Please select Provider Service");
    //}
    else if (CategoryId == "") {
        $("#dvMsg").html("Category is required.");
    }
    else {
        var commission = new Object();
        commission.Id = 0;
        commission.ServiceId = $("#drpProviderServices").val();
        commission.ProviderType = $("#drpProvider").val();
        commission.AgentCategoryId = $("#hdn_AgentCategoryId").val();
        commission.GenderType = GenderType;
        commission.KycType = KycType;
        commission.MinimumAmount = 0;
        commission.MaximumAmount = 0;
        commission.FixedCommission = 0;
        commission.PercentageCommission = 0;
        commission.PercentageRewardPoints = 0;
        commission.PercentageRewardPointsDebit = 0;
        commission.ServiceCharge = 0;
        commission.FromDate = GetTodayDate();
        commission.ToDate = GetTodayDate();
        commission.MinimumAllowed = 0;
        commission.MaximumAllowed = 0;
        commission.ChildTxnRate = 0;
        commission.ChildTxnMinAmt = 0;
        commission.ChildTxnMaxAmt = 0;
        commission.MonthlyMinAmt = 0;
        commission.MonthlyMaxAmt = 0;
        commission.MonthlyBonus = 0;

        if (commission != null) {
            $.ajax({
                type: "POST",
                url: "/Agent/AddNewAgentCommissionDataRow",
                data: JSON.stringify(commission),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        /*      buttontype = "add";*/
                        BindAgentCommissionDataTable("tbllist");
                        $("#dvMsgSuccess").html("Add Commissions Values and click Update button in new row.");
                        setTimeout(function () {
                            $("#dvMsgSuccess").html("");
                        }, 10000); // 10 seconds in milliseconds

                        // $("#anchor_addCommissionRow").hide();
                        return true;
                    } else {
                        $("#dvMsg").html("Something went wrong");
                        setTimeout(function () {
                            $("#dvMsg").html("");
                        }, 10000); // 10 seconds in milliseconds
                        return false;
                    }
                },
                failure: function (response) {
                    $("#dvMsg").html(response.responseText);
                    setTimeout(function () {
                        $("#dvMsg").html("");
                    }, 10000); // 10 seconds in milliseconds
                    return false;
                },
                error: function (response) {
                    $("#dvMsg").html(response.responseText);
                    setTimeout(function () {
                        $("#dvMsg").html("");
                    }, 10000); // 10 seconds in milliseconds
                    return false;
                }
            });
        }
    }
}

function updatecommission(id) {
    var check ="0";
    var CategoryId = $('#hdn_AgentCategoryId').val();
    var ProviderType = $("#drpProvider").val()
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#UpdateCommission" + id).attr("disabled", "disabled");
    $("#UpdateCommission" + id).text("Processing..");
    var min = $("#MinimumAmount" + id).val(), max = $("#MaximumAmount" + id).val(),
        fixed = $("#FixedCommission" + id).val(), perc = $("#PercentageCommission" + id).val(),
        percRewards = $("#PercentageRewardPoints" + id).val(), percRewardsDebit = $("#PercentageRewardPointsDebit" + id).val(),
        fromdt = $("#FromDateDT" + id).val(), todt = $("#ToDateDT" + id).val(),
        minallowed = $("#MinimumAllowed" + id).val(),
        maxallowed = $("#MaximumAllowed" + id).val(),
        minallowedSC = $("#MinimumAllowedSC" + id).val(),
        maxallowedSC = $("#MaximumAllowedSC" + id).val();
        ChildTxnMaxAmt = $("#ChildTxnMaxAmt" + id).val();
        ChildTxnMinAmt = $("#ChildTxnMinAmt" + id).val();
        ChildTxnRate = $("#ChildTxnRate" + id).val();
        MonthlyMinAmt = $("#MonthlyMinAmt" + id).val();
        MonthlyMaxAmt = $("#MonthlyMaxAmt" + id).val();
        MonthlyBonus = $("#MonthlyBonus" + id).val();
    var ServiceId = $("#ServiceId" + id).val(),
        ServiceCharge = $("#ServiceCharge" + id).val(),
        GenderType = $("#GenderTypeId" + id).val(),
        KycType = $("#KycTypeId" + id).val();

    if (min == "") {
        $("#dvMsg").html("Please enter Minimum Slab Amount");
        check = "1";
        return false;
    }
    else if (max == "") {
        $("#dvMsg").html("Please enter Maximum Slab Amount");
        check = "1";
        return false;
    }
    else if (fixed == "") {
        $("#dvMsg").html("Please enter Fixed Amount");
        check = "1";
    }
    else if (perc == "") {
        $("#dvMsg").html("Please enter Percent Amount");
        check = "1";
    }
    else if (percRewards == "") {
        $("#dvMsg").html("Please enter MP-Coins Credit.");
        check = "1";
    }
    else if (percRewardsDebit == "") {
        $("#dvMsg").html("Please enter MP-Coins Debit.");
        check = "1";
    }
    else if (maxallowed == "") {
        $("#dvMsg").html("Please enter Maximum Amount Allowed");
        check = "1";
    }
    else if (parseInt(min) > parseInt(max)) {
        $("#dvMsg").html("Please enter Minimum value less than Maximum value");
        check = "1";
    }
    else if (fromdt == "") {
        $("#dvMsg").html("Please enter From Date");
        check = "1";
    }
    else if (todt == "") {
        $("#dvMsg").html("Please enter To Date");
        check = "1";
    }
    else if (parseFloat(perc) >= "100") {
        $("#dvMsg").html("Cashback(%)  can't be greater than equal to 100");
        check = "1";
    }
    else if (parseFloat(percRewards) >= "100") {
        $("#dvMsg").html("MP-Coins Credit(%)  can't be greater than equal to 100");
        check = "1";
    }
    else if (parseFloat(percRewardsDebit) >= "100") {
        $("#dvMsg").html("MP-Coins Debit(%)  can't be greater than equal to 100");
        check = "1";
    }
    else if (parseFloat(ServiceCharge) >= "100") {
        $("#dvMsg").html("ServiceCharge(%)  can't be greater than equal to 100");
        check = "1";
    }
    else if (parseFloat(MonthlyBonus) >= "100") {
        $("#dvMsg").html("MonthlyBonus(%)  can't be greater than equal to 100");
        check = "1";
    }
    else if (parseFloat(ChildTxnRate) >= "100") {
        $("#dvMsg").html("Child TxnRate(%)  can't be greater than equal to 100");
        check = "1";
    }
    else if (ChildTxnMaxAmt == "") {
        $("#dvMsg").html("Please enter child Maximum Amount");
        check = "1";
    }
    else if (ChildTxnMinAmt == "") {
        $("#dvMsg").html("Please enter child Minimum Amount");
        check = "1";
    }
    else if (MonthlyMinAmt == "") {
        $("#dvMsg").html("Please enter monthly Minimum Amount");
        check = "1";
    }
    else if (MonthlyMaxAmt == "") {
        $("#dvMsg").html("Please enter monthly Maximum Amount");
        check = "1";
    }
    if (check == "1") {
        check = "0";
        setTimeout(function () {
            $("#dvMsg").html("");
        }, 10000); // 10 seconds in milliseconds
        return false;
    }
    var commission = new Object();
    commission.Id = parseInt(id);
    commission.ServiceId = ServiceId;
    commission.GenderType = GenderType;
    commission.KycType = KycType;
    commission.MinimumAmount = min;
    commission.MaximumAmount = max;
    commission.FixedCommission = fixed;
    commission.PercentageCommission = perc;
    commission.PercentageRewardPoints = percRewards;
    commission.PercentageRewardPointsDebit = percRewardsDebit;
    commission.FromDate = fromdt;
    commission.ToDate = todt;
    commission.MinimumAllowed = minallowed;
    commission.MaximumAllowed = maxallowed;
    commission.MinimumAllowedSC = minallowedSC;
    commission.MaximumAllowedSC = maxallowedSC;
    commission.ServiceCharge = ServiceCharge;
    commission.AgentCategoryId = CategoryId;
    commission.ProviderType = ProviderType
    commission.ChildTxnRate = ChildTxnRate
    commission.ChildTxnMinAmt = ChildTxnMinAmt
    commission.ChildTxnMaxAmt = ChildTxnMaxAmt
    commission.MonthlyMinAmt = MonthlyMinAmt
    commission.MonthlyMaxAmt = MonthlyMaxAmt
    commission.MonthlyBonus = MonthlyBonus
    var dd = JSON.stringify(commission);
    if (commission != null) {
        $.ajax({
            type: "POST",
            url: "/Agent/AgentCommissionUpdateCall",
            data: JSON.stringify(commission),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                if (response != null) {
                    if (response.resultId == "0") {
                        $("#dvMsg").html("Records not updated. Please check that minimum and maximum values are unique and never defined previously.");
                        setTimeout(function () {
                            $("#dvMsg").html("");
                        }, 10000); // 10 seconds in milliseconds
                        return false;
                    }
                    else {
                        BindAgentCommissionDataTable("tbllist");
                        $("#dvMsgSuccess").html("Commissions are successfully updated");
                        setTimeout(function () {
                            $("#dvMsgSuccess").html("");
                        }, 10000); // 10 seconds in milliseconds
                       
                        $("#anchor_addCommissionRow").show();
                        return true;
                    }
                }
                else {
                    $("#dvMsg").html("Something went wrong. Please try again later.");
                    setTimeout(function () {
                        $("#dvMsg").html("");
                    }, 10000); // 10 seconds in milliseconds
                    return false;
                }
            },
            failure: function (response) {
                $("#dvMsg").html(response.responseText);
                setTimeout(function () {
                    $("#dvMsg").html("");
                }, 10000); // 10 seconds in milliseconds
                return false;
            },
            error: function (response) {
                $("#dvMsg").html(response.responseText);
                setTimeout(function () {
                    $("#dvMsg").html("");
                }, 10000); // 10 seconds in milliseconds
                return false;
            }
        });
    }
    else {
        return false;
    }
}
function DeleteAgentCommission(id) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm("Do you really want to delete commission ?")) {
        var commission = new Object();
        commission.Id = id;
        if (commission != null) {
            $.ajax({
                type: "POST",
                url: "/Agent/DeleteAgentCommissionDataRow",
                data: JSON.stringify(commission),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.resultId == "0") {
                        $("#dvMsg").html("Something went wrong");
                        setTimeout(function () {
                            $("#dvMsg").html("");
                        }, 10000); // 10 seconds in milliseconds
                        return false;
                    } else {
                        BindAgentCommissionDataTable("tbllist");
                        $("#dvMsgSuccess").html("Commissions are successfully deleted");
                        setTimeout(function () {
                            $("#dvMsgSuccess").html("");
                        }, 10000); // 10 seconds in milliseconds

                        return true;
                        
                    }
                },
                failure: function (response) {
                    $("#dvMsg").html(response.responseText);
                    setTimeout(function () {
                        $("#dvMsg").html("");
                    }, 10000); // 10 seconds in milliseconds
                    return false;
                },
                error: function (response) {
                    $("#dvMsg").html(response.responseText);
                    setTimeout(function () {
                        $("#dvMsg").html("");
                    }, 10000); // 10 seconds in milliseconds
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


function statusupdateAgentcommission(id, activestatus) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var commission = new Object();
    commission.Id = parseInt(id);
    commission.IsActive = activestatus;
    var dd = JSON.stringify(commission);
    if (commission != null) {
        $.ajax({
            type: "POST",
            url: "/Agent/StatusUpdateAgentCommissionCall",
            data: JSON.stringify(commission),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                if (response != null) {
                    if (response.resultId != "0") {
                        BindAgentCommissionDataTable("tbllist");
                        if (activestatus == "true")
                            $("#dvMsgSuccess").html("Commissions are successfully Published");
                        else
                            $("#dvMsgSuccess").html("Commissions are successfully Unpublished");
                        $("#anchor_addCommissionRow").show();
                        setTimeout(function () {
                            $("#dvMsgSuccess").html("");
                        }, 10000); // 10 seconds in milliseconds
                        return true;
                    }
                }
                else {
                    $("#dvMsg").html("Something went wrong. Please try again later.");
                    setTimeout(function () {
                        $("#dvMsg").html("");
                    }, 10000); // 10 seconds in milliseconds
                    return false;
                }
            },
            failure: function (response) {
                $("#dvMsg").html(response.responseText);
                setTimeout(function () {
                    $("#dvMsg").html("");
                }, 10000); // 10 seconds in milliseconds
                return false;
            },
            error: function (response) {
                $("#dvMsg").html(response.responseText);
                setTimeout(function () {
                    $("#dvMsg").html("");
                }, 10000); // 10 seconds in milliseconds
                return false;
            }
        });
    }
    else {
        return false;
    }
}

/////////////////----------------------------------------End Agent Commission Module (Edit of agent category)----------------------------------------------------------/////


/////////////////-------------------------------------------Start Agent Commission Module (View of agent category) -----------------------------------------------------------------------/////


function BindAgentCommissionDataTable_View(tableId) {
    var CategoryId = $('#hdn_AgentCategoryId_V').val();
    var AgentCategory = $('#AgentCategory').val();
    var ProviderType = $("#drpProvider_V").val()
    var ServiceId = $("#drpProviderServices_V").val()
    //var CategoryId = $("#drpProviderServices").val()
    if (ProviderType != "0" && (CategoryId != "")) {
        $("#tbllist").show();
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                table = $('#tbllist').DataTable({
                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No Active Commissions  "
                    },
                    "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                    "pageLength": 50,
                    "processing": true,
                    "serverSide": true,
                    "destroy": true,
                    "searching": false,
                    "sorting": false,
                    "order": [[0, "desc"]],
                    "ajax": {
                        "url": "/Agent/GetAgentCommissionLists",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            data.IsDeleted = 0;
                            data.AgentCategoryId = $('#hdn_AgentCategoryId_V').val();
                            data.AgentCategory = $('#AgentCategory').val();
                            data.ProviderType = $("#drpProvider_V").val();
                            data.ServiceId = $("#drpProviderServices_V").val();
                            //data.ButtonType = buttontype;
                            //data.GenderType = $("#GenderTypeEnum").val();
                            //data.KycType = $("#KycTypeEnum").val();
                            data.ScheduleStatus = $("#EnumScheduleStatus_V").val();
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
                        { "data": "Sno.", "name": "Sno", "autoWidth": true, "bSortable": false },
                        { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": false },
                        { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": false },
                        { "data": "ServiceName", "name": "Service Name", "autoWidth": true, "bSortable": false },
                        { "data": "MinimumAmount", "name": "Min Slab Amount", "autoWidth": true, "bSortable": false },
                        { "data": "MaximumAmount", "name": "Max Slab Amount", "autoWidth": true, "bSortable": false },
                        { "data": "PercentageCommission", "name": "Cashback Amount(%)", "autoWidth": true, "bSortable": false },
                        { "data": "PercentageRewardPoints", "name": "MP-Coins Credit(%)", "autoWidth": true, "bSortable": false },
                        { "data": "PercentageRewardPointsDebit", "name": "MP-Coins Debit(%)", "autoWidth": true, "bSortable": false },
                        { "data": "MinimumAllowed", "name": "Min Allowed Cashback", "autoWidth": true, "bSortable": false },
                        { "data": "MaximumAllowed", "name": "Max Allowed  Cashbacke", "autoWidth": true, "bSortable": false },
                        { "data": "ServiceCharge", "name": "Service Charge(%)", "autoWidth": true, "bSortable": false },
                        { "data": "MinimumAllowedSC", "name": "Min Allowed Service Charge", "autoWidth": true, "bSortable": false },
                        { "data": "MaximumAllowedSC", "name": "Max Allowed Service Charge", "autoWidth": true, "bSortable": false },
                        {
                            "data": "ChildTxnRate", "name": "Child Txn Rate (%)",
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).addClass('Clschildcommission'); // Add your custom class name
                            },
                            "autoWidth": true, "bSortable": false
                        },
                        {
                            "data": "ChildTxnMinAmt", "name": "Child Txn Min Amount",
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).addClass('Clschildcommission'); // Add your custom class name
                        },
                            "autoWidth": true, "bSortable": false
                        },
                        {
                            "data": "ChildTxnMaxAmt", "name": "Child Txn Max Amount",
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).addClass('Clschildcommission'); // Add your custom class name
                            },
                            "autoWidth": true, "bSortable": false
                        },
                        {
                            "data": "MonthlyMinAmt", "name": "Monthly Min Amount",
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).addClass('Clschildcommission'); // Add your custom class name
                            },
                            "autoWidth": true, "bSortable": false
                        },
                        {
                            "data": "MonthlyMaxAmt", "name": "Monthly Max Amount",
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).addClass('Clschildcommission'); // Add your custom class name
                            },
                            "autoWidth": true, "bSortable": false
                        },
                        {
                            "data": "MonthlyBonus", "name": "Monthly Bonus (%)",
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).addClass('Clschildcommission'); // Add your custom class name
                            },
                            "autoWidth": true, "bSortable": false
                        },
                        { "data": "FromDateDT", "name": "From Date", "autoWidth": true, "bSortable": false },
                        { "data": "ToDateDT", "name": "To Date", "autoWidth": true, "bSortable": false },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var objScheduleStatusType = '<span  class="tb-status text-' + (data.ScheduleStatus != "Running" ? "danger" : "success") + '">' + data.ScheduleStatus + '</span>';
                                return objScheduleStatusType;
                            },
                            autoWidth: true,
                            bSortable: false
                        }
                                         
                    ]
                });

                if (document.getElementById("tbllist") != undefined) {
                    document.getElementById("tbllist").deleteTFoot();
                }

                $("#tbllist").append(
                    $('<tfoot/>').append($("#tbllist thead tr").clone())
                );
                $('#AjaxLoader').hide();

                /*buttontype = "";*/
            }, 100);
    }
    else {
        $("#tbllist").hide();
    }
    $("#anchor_addCommissionRow").show();
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#tbllist_length").hide();
    $(".dt-buttons btn-group flex-wrap").hide();

    setTimeout(function () {
        $('.datePicker').datepicker({
            format: 'd M yyyy',
            startDate: '+0d'
        });
    }, 1000);

    //$('.datePicker').datepicker({
    //    format: 'd M yyyy',
    //    startDate: '+0d'
    //});

    //(".btncommissionAction").tooltip();
}


/////////////////----------------------------------------End Agent Commission Module (View of agent category)----------------------------------------------------------/////


///////////////////--------------------------------------Start Agent Other Commission -----------------------------------------/////////////////////
function BindAgentOtherCommissionDataTable() {
    table = $('#tbllist').DataTable({
        "dom": 'lBfrtip',
        bFilter: false,
        "oLanguage": {
            "sEmptyTable": "No Active Commissions  "
        },
        "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
        "pageLength": 50,
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "searching": false,
        "sorting": false,
        "order": [[0, "desc"]],
        "ajax": {
            "url": "/Agent/GetOtherCommissionLists",
            "type": "POST",
            "async": false,
            data: function (data) {
                data.IsDeleted = 0;

            },
            error: function (xhr, error, code) {
                debugger;
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
            { "data": "Sno.", "name": "Sno", "autoWidth": true, "bSortable": false },
            {
                data: null,
                render: function (data, type, row) {
                    return '<span class="tb-status text-danger" > * </span><input type="text" id="AgentCreationCommission' + data.OtherCommissionId + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.AgentCreationCommission + '" onkeypress="return isNumberKey(this, event);">';
                },
                autoWidth: true,
                bSortable: false
            },
            {
                data: null,
                render: function (data, type, row) {
                    return '<span class="tb-status text-danger" > * </span><input type="text" id="UserCreationCommission' + data.OtherCommissionId + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.UserCreationCommission + '" onkeypress="return isNumberKey(this, event);">';
                },
                autoWidth: true,
                bSortable: false
            },
            {
                data: null,
                render: function (data, type, row) {
                    return '<span class="tb-status text-danger" > * </span><input type="text" id="KYCCommission' + data.OtherCommissionId + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.KYCCommission + '" onkeypress="return isNumberKey(this, event);">';
                },
                autoWidth: true,
                bSortable: false
            },
            {
                data: null,
                render: function (data, type, row) {
                    return '<span class="tb-status text-danger" > * </span><input type="text" id="Value' + data.OtherCommissionId + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.Value + '" onkeypress="return isNumberKey(this, event);">';
                },
                autoWidth: true,
                bSortable: false
            },
            {
                data: null,
                render: function (data, type, row) {
                    return '<span class="tb-status text-danger" > * </span><input type="text" id="MinAmount' + data.OtherCommissionId + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MinAmount + '" onkeypress="return isNumberKey(this, event);">';
                },
                autoWidth: true,
                bSortable: false
            },
            {
                data: null,
                render: function (data, type, row) {
                    return '<span class="tb-status text-danger" > * </span><input type="text" id="MaxAmount' + data.OtherCommissionId + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MaxAmount + '" onkeypress="return isNumberKey(this, event);">';
                },
                autoWidth: true,
                bSortable: false
            },
            {
                data: null,
                render: function (data, type, row) {
                    return '<span class="tb-status text-danger" > * </span><input type="text" id="CASHINCommission' + data.OtherCommissionId + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.CASHINCommission + '" onkeypress="return isNumberKey(this, event);">';
                },
                autoWidth: true,
                bSortable: false
            },
            {
                data: null,
                render: function (data, type, row) {
                    return '<span class="tb-status text-danger" > * </span><input type="text" id="CASHOUTCommission' + data.OtherCommissionId + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.CASHOUTCommission + '" onkeypress="return isNumberKey(this, event);">';
                },
                autoWidth: true,
                bSortable: false
            },
      
            {
                data: null,
                render: function (data, type, row) {
                    
                    var actioncolumn = '<td><a href="javascript:void(0);" id="UpdateOtherCommission' + data.OtherCommissionId + '"  class="btn btn-sm btn-success btncommissionAction1"  title="Update Commission" onclick=\'return updateothercommission(\"' + data.OtherCommissionId + '\");\'  class="btn btn-primary "> Update </a></td>';
                    //actioncolumn = actioncolumn + '</tr></table>'
                    return actioncolumn;
                },
                bSortable: false,
                sTitle: "Action"
            }

        ],
        "createdRow": function (row, data, dataIndex) {
            debugger;
            // Add class to specific cells
            $(row).find('td').addClass('Clsothercommissionth ');
            //$(row).find('td:last').removeClass('Clsothercommissionth');
           // $(row).find('td:last').removeClass('card table tr:first-child th, card table tr:first-child td');


            //$(row).find('td :eq(9)').removeClass('Clsothercommissionth ');
        }
 
        //"rowGroup": {
        //    "dataSrc": ["Agent Creation Commission", "Value(%)", "Min Amount", "Max Amount", "Action"]
        //}

    });

    if (document.getElementById("tbllist") != undefined) {
        document.getElementById("tbllist").deleteTFoot();
    }

    $("#tbllist").append(
        $('<tfoot/>').append($("#tbllist thead tr").clone())
    );
    $('#AjaxLoader').hide();

};

function updateothercommission(id) {
    debugger;
    var check = "0";

    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#UpdateOtherCommission" + id).attr("disabled", "disabled");
    $("#UpdateOtherCommission" + id).text("Processing..");
    var OtherCommissionId = id,
        AgentCreationCommission = $("#AgentCreationCommission" + id).val(),
        MaxAmount = $("#MaxAmount" + id).val(),
        UserCreationCommission = $("#UserCreationCommission" + id).val(),
        MinAmount = $("#MinAmount" + id).val(),
        KYCCommission = $("#KYCCommission" + id).val(),
        Value = $("#Value" + id).val(),
        CASHINCommission = $("#CASHINCommission" + id).val(),
        CASHOUTCommission = $("#CASHOUTCommission" + id).val()
       
   

    if (AgentCreationCommission == "") {
        $("#dvMsg").html("Please enter Agent Creation Commission Slab Amount");
        check = "1";
        return false;
    }
    else if (UserCreationCommission == "") {
        $("#dvMsg").html("Please enter User Creation Commission Slab Amount");
        check = "1";
        return false;
    }
    else if (KYCCommission == "") {
        $("#dvMsg").html("Please enter KYC Commission Amount");
        check = "1";
    }
    else if (Value == "") {
        $("#dvMsg").html("Please enter Percent Amount");
        check = "1";
    }
    else if (MinAmount == "") {
        $("#dvMsg").html("Please enter Minimum Amount .");
        check = "1";
    }
    else if (MaxAmount == "") {
        $("#dvMsg").html("Please enter Maximum Amount.");
        check = "1";
    }
    else if (CASHINCommission == "") {
        $("#dvMsg").html("Please enter CASHIN Commission Amount .");
        check = "1";
    }
    else if (parseInt(MinAmount) > parseInt(MaxAmount)) {
        $("#dvMsg").html("Please enter Minimum value less than Maximum value");
        check = "1";
    }
    else if (CASHOUTCommission == "") {
        $("#dvMsg").html("Please enter CASHOUT Commission Amount .");
        check = "1";
    }
    
    if (check == "1") {
        check = "0";
        setTimeout(function () {
            $("#dvMsg").html("");
        }, 10000); // 10 seconds in milliseconds
        return false;
    }
    var Othercommissions = new Object();
    Othercommissions.OtherCommissionId = parseInt(OtherCommissionId);
    Othercommissions.MinAmount = MinAmount;
    Othercommissions.MaxAmount = MaxAmount;
    Othercommissions.AgentCreationCommission = AgentCreationCommission;
    Othercommissions.UserCreationCommission = UserCreationCommission;
    Othercommissions.KYCCommission = KYCCommission;
    Othercommissions.CASHINCommission = CASHINCommission;
    Othercommissions.CASHOUTCommission = CASHOUTCommission;
    Othercommissions.value = Value;
   
    var dd = JSON.stringify(Othercommissions);
    if (Othercommissions != null) {
        $.ajax({
            type: "POST",
            url: "/Agent/OtherCommissionUpdate",
            data: JSON.stringify(Othercommissions),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                if (response != null) {
                    if (response.resultId == "0") {
                        $("#dvMsg").html("Records not updated. Please check that minimum and maximum values are unique and never defined previously.");
                        setTimeout(function () {
                            $("#dvMsg").html("");
                        }, 10000); // 10 seconds in milliseconds
                        return false;
                    }
                    else {
                        BindAgentOtherCommissionDataTable("tbllist");
                        $("#dvMsgSuccess").html("Commissions are successfully updated");
                        setTimeout(function () {
                            $("#dvMsgSuccess").html("");
                        }, 10000); // 10 seconds in milliseconds

                        $("#anchor_addCommissionRow").show();
                        return true;
                    }
                }
                else {
                    $("#dvMsg").html("Something went wrong. Please try again later.");
                    setTimeout(function () {
                        $("#dvMsg").html("");
                    }, 10000); // 10 seconds in milliseconds
                    return false;
                }
            },
            failure: function (response) {
                $("#dvMsg").html(response.responseText);
                setTimeout(function () {
                    $("#dvMsg").html("");
                }, 10000); // 10 seconds in milliseconds
                return false;
            },
            error: function (response) {
                $("#dvMsg").html(response.responseText);
                setTimeout(function () {
                    $("#dvMsg").html("");
                }, 10000); // 10 seconds in milliseconds
                return false;
            }
        });
    }
    else {
        return false;
    }
};


//function AddOtherCommissionRow() {
//    debugger;
//    $("#dvMsgSuccess").html("");
//    $("#dvMsg").html("");
     
//        var othercommission = new Object();
//        othercommission.OtherCommissionId = 0;
       
//        othercommission.MinAmount = 0;
//        othercommission.MaxAmount = 0;
//        othercommission.KYCCommission = 0;
//        othercommission.AgentCreationCommission = 0;
//        othercommission.UserCreationCommission = 0;
//        othercommission.Value = 0;
//        othercommission.CASHINCommission = 0;
//        othercommission.CASHOUTCommission = 0;
        

//    if (othercommission != null) {
//            $.ajax({
//                type: "POST",
//                url: "/Agent/AddAgentOtherCommissionDataRow",
//                data: JSON.stringify(othercommission),
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                success: function (response) {
//                    if (response != null) {
//                        /*      buttontype = "add";*/
//                        BindAgentOtherCommissionDataTable("tbllist");
//                        $("#dvMsgSuccess").html("Add Commissions Values and click Update button in new row.");
//                        setTimeout(function () {
//                            $("#dvMsgSuccess").html("");
//                        }, 10000); // 10 seconds in milliseconds

//                        // $("#anchor_addCommissionRow").hide();
//                        return true;
//                    } else {
//                        $("#dvMsg").html("Something went wrong");
//                        setTimeout(function () {
//                            $("#dvMsg").html("");
//                        }, 10000); // 10 seconds in milliseconds
//                        return false;
//                    }
//                },
//                failure: function (response) {
//                    $("#dvMsg").html(response.responseText);
//                    setTimeout(function () {
//                        $("#dvMsg").html("");
//                    }, 10000); // 10 seconds in milliseconds
//                    return false;
//                },
//                error: function (response) {
//                    $("#dvMsg").html(response.responseText);
//                    setTimeout(function () {
//                        $("#dvMsg").html("");
//                    }, 10000); // 10 seconds in milliseconds
//                    return false;
//                }
//            });
//        }
   
//}
////////////////-----------------------------------------End Agent Other Commission ----------------------------------------//////////


