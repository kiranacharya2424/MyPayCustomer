var table;
/*var buttontype = "";*/

function BindSampleVoucherDataTable() {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
           
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();

            table = $('#tblvoucherlist').DataTable({
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
                    "url": "/Agent/GetSampleVoucher",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                       
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
                    { "data": "BankName", "name": "Bank Name", "autoWidth": true, "bSortable": false },
                    { "data": "Branch", "name": "Branch", "autoWidth": true, "bSortable": false },
                    { "data": "AccountName", "name": "Account Name", "autoWidth": true, "bSortable": false },
                    { "data": "AccountNumber", "name": "Account Number", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": false },
                    
                    {
                        data: null,
                        render: function (data, type, row) {

                            var str = "";
                            str = '<ul class="nk-tb-actions gx-1">';
                            str += '<li class="">';
                            str += '<a href="/Agent/ViewVoucher?SampleVoucheId='+ data.SampleVoucheId +' " class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="View Details">';
                            //str += '<a href="Agent/AddAgent?AgentUniqueId=' + data.Category + '" class"btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="View Details">';
                            str += '<em class="icon ni ni-eye"></em>';
                            str += '</a>';
                            str += '</li>';


                            str += '<li class="">';
                            str += '<a href="/Agent/AddSampleVoucher?SampleVoucheId=' + data.SampleVoucheId +'  "  class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Edit">';

                            //str += '<a href="Agent/AddAgent?AgentUniqueId=' + data.Category + '" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Edit">';
                            str += '<em class="icon ni ni-edit"></em>';
                            str += '</a>';
                            str += '</li>';


                            str += '<li class="">';
                            //str += '<a href="javascript:void(0);" onclick="return BlockUnblock(&apos;' + data.AgentUniqueId + '&apos;,event)"><em class="icon ni ni-user-check-fill"></em><span>Enable Agent</span></a>';

                            str += '<a href="javascript:void(0);" onclick="return DeleteVoucher(&apos;' + data.SampleVoucheId + '&apos;,event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Delete">';
                            str += '<em class="icon ni ni-trash"></em>';
                            str += '</a>';
                            str += '</li>';

                            str += '</ul>';
                            return str;
                        },
                        bSortable: false,
                        sTitle: "Action"
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            document.getElementById("tblvoucherlist").deleteTFoot();

            $("#tblvoucherlist").append(
                $('<tfoot/>').append($("#tblvoucherlist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function DeleteVoucher(SampleVoucheId, e) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/Agent/DeleteVoucher",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"SampleVoucheId":"' + SampleVoucheId + '"}',
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
                    BindSampleVoucherDataTable(tableId);

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

//function EditVoucher(SampleVoucheId, e) {
//    //var hashedCategoryid = hashPassword(Categoryid);
//    window.location.href = "/Agent/EditVoucher?SampleVoucheId=" + SampleVoucheId
//}

//function ViewVoucher(SampleVoucheId, e) {
//    window.location.href = "/Agent/ViewVoucher?SampleVoucheId=" + SampleVoucheId
//}

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