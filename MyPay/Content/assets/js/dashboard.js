var elemsDate = [];
//elemsDate.push("20 Dec 21");
//elemsDate.push("21 Dec 21");
//elemsDate.push("22 Dec 21");
//elemsDate.push("23 Dec 21");
//elemsDate.push("24 Dec 21");
//elemsDate.push("25 Dec 21");
//elemsDate.push("26 Dec 21");
//elemsDate.push("27 Dec 21");
//elemsDate.push("28 Dec 21");
//elemsDate.push("29 Dec 21");
//elemsDate.push("30 Dec 21");
//elemsDate.push("31 Dec 21");
//elemsDate.push("01 Jan 22");
//elemsDate.push("02 Jan 22");

//$('#input_hidden_elemsDate').val(JSON.stringify(elemsDate)); //store array


var elemsDebitTransactions = [];
//elemsDebitTransactions.push(1740);
//elemsDebitTransactions.push(2500);
//elemsDebitTransactions.push(4323);
//elemsDebitTransactions.push(3213);
//elemsDebitTransactions.push(3212);
//elemsDebitTransactions.push(1232);
//elemsDebitTransactions.push(3312);
//elemsDebitTransactions.push(4122);
//elemsDebitTransactions.push(3321);
//elemsDebitTransactions.push(2123);
//elemsDebitTransactions.push(4111);
//elemsDebitTransactions.push(2212);
//elemsDebitTransactions.push(3322);
//elemsDebitTransactions.push(2212);


var elemsCreditTransactions = [];
//elemsCreditTransactions.push(2740);
//elemsCreditTransactions.push(1500);
//elemsCreditTransactions.push(3323);
//elemsCreditTransactions.push(1213);
//elemsCreditTransactions.push(2212);
//elemsCreditTransactions.push(2232);
//elemsCreditTransactions.push(4330);
//elemsCreditTransactions.push(1122);
//elemsCreditTransactions.push(2321);
//elemsCreditTransactions.push(1123);
//elemsCreditTransactions.push(2111);
//elemsCreditTransactions.push(1212);
//elemsCreditTransactions.push(4322);
//elemsCreditTransactions.push(1212);

$('#input_hidden_elemsDate').val(JSON.stringify(elemsDate)); //store array
$('#input_hidden_elemsDebitTransactions').val(JSON.stringify(elemsDebitTransactions)); //store array
$('#input_hidden_elemsCreditTransactions').val(JSON.stringify(elemsCreditTransactions)); //store array




debugger;
//$.ajax({
//    type: "POST",
//    url: "/AdminLogin/BindDashboardChart",
//    contentType: "application/json; charset=utf-8",
//    dataType: "json",
//    async: false,
//    success: function (response) {
//        debugger;
//        if (response != null) {
//            debugger;
//            for (var i = 0; i < response.length; i++) {
//                elemsDate.push(response[i].TransactionDate);
//                elemsCreditTransactions.push(response[i].CreditNetAmount);
//                elemsDebitTransactions.push(response[i].DebitNetAmount);
//            }
//            $('#input_hidden_elemsDate').val(JSON.stringify(elemsDate)); //store array
//            $('#input_hidden_elemsDebitTransactions').val(JSON.stringify(elemsDebitTransactions)); //store array
//            $('#input_hidden_elemsCreditTransactions').val(JSON.stringify(elemsCreditTransactions)); //store array

//        }
//    }
//});

$(document).ready(function () {
    BindDashboardChart();
});

function BindDashboardChart() {

}
$(document).ready(function () {
    var CurrencyId = "0";
    setTimeout(
        function () {
            $.ajax({
                url: "/Remittance/GetDestinationCurrenciesLists",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: '{"CurrencyId":"' + CurrencyId + '"}',
                success: function (data) {
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    return false;
                }
            });
        }, 100);
});