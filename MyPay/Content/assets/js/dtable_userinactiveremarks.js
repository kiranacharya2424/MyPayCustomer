

var table;

function BindUserInActiveDataTable() {

    $('#AjaxLoader').show();
    setTimeout(
        function () {
            // debugger;
            var MemberId = $("#MemberId").val();
            var Mobile = $("#ContactNumber").val();
            var Name = $("#FirstName").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            if (MemberId == "" && Mobile == "" && Name == "" && fromdate == "" && todate == "") {
                alert('select The Field');
            }
            else if (fromdate != "" && todate != "" || MemberId != "" || Mobile != "" || Name != "") {
                table = $('#tblist').DataTable({

                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No User"
                    },
                    "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                    "pageLength": 50,
                    "processing": true,
                    "serverSide": true,
                    //"scrollY": "500px" ,
                    //"scrollX": true,
                    "destroy": true,
                    "order": [[1, "desc"]],
                    "ajax": {
                        "url": "/UserInActiveStatus/GetInActiveUserLists",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            debugger;
                            data.MemberId = MemberId;
                            data.ContactNumber = Mobile;
                            data.StartDate = fromdate;
                            data.ToDate = todate;
                            data.Name = Name;
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
                        { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": true },
                        { "data": "CreatedDateDt", "name": "Date", "autoWidth": true, "bSortable": true },
                        { "data": "MemberId", "name": "Member Id", "autoWidth": true, "bSortable": true },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span>' + data.FirstName + ' ' + data.LastName + '</span >';
                            },
                            bSortable: false,
                            sTitle: "Name"
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span>' + data.ContactNumber + '</span >';
                            },
                            bSortable: false,
                            sTitle: "Contact No"
                        },
                        { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                        { "data": "Action", "name": "Status", "autoWidth": true, "bSortable": false },
                        { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false }
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
            }
            else {
                //// debugger;
                // if (Name != '') {
                //     $("#MemberId").prop('disabled', true);
                //     $("#ContactNumber").prop('disabled', true);
                // }
                // else {
                //     $("#MemberId").prop('disabled', false);
                //     $("#ContactNumber").prop('disabled', false);
                // }

                table = $('#tblist').DataTable({

                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No User"
                    },
                    "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                    "pageLength": 50,
                    "processing": true,
                    "serverSide": true,
                    //"scrollY": "500px" ,
                    //"scrollX": true,
                    "destroy": true,
                    "order": [[1, "desc"]],
                    "ajax": {
                        "url": "/UserInActiveStatus/GetInActiveUserLists",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            debugger;
                            data.MemberId = MemberId;
                            data.ContactNumber = Mobile;
                            data.StartDate = fromdate;
                            data.ToDate = todate;
                            data.Name = Name;
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
                        { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": true },
                        { "data": "CreatedDateDt", "name": "Date", "autoWidth": true, "bSortable": true },
                        { "data": "MemberId", "name": "Member Id", "autoWidth": true, "bSortable": true },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span>' + data.FirstName + ' ' + data.LastName + '</span >';
                            },
                            bSortable: false,
                            sTitle: "Name"
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span>' + data.ContactNumber + '</span >';
                            },
                            bSortable: false,
                            sTitle: "Contact No"
                        },
                        { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                        { "data": "Action", "name": "Status", "autoWidth": true, "bSortable": false },
                        { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false }
                    ],
                    "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                        $(nRow).addClass('nk-tb-item');

                        $('td', nRow).addClass('nk-tb-col tb-col-lg');

                        var oSettings = this.fnSettings();
                        $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                        return nRow;
                    }
                });



            }
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );

            $('#AjaxLoader').hide();

        }, 100);
}