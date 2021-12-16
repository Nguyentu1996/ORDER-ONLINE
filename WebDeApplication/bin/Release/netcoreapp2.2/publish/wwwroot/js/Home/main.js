//var employee = employee || {};
//var number = 1

//employee.drawTable = function () {
//    $.ajax({
//        url: "https://localhost:44309/DataDauVaos/SapSepTheoGia",
//        method: 'GET',
//        dataType: 'json',
//        contentType: 'application/json; charset=utf-8',
//        success: function (data) {
//            $('#travelID').empty();
//            $.each(data, function (index, value) {
//                $('#travelID').append("<tr>" +
//                    "<td>" + value.number + " = " + value.ketqua + "</td>" +
//                    "</tr>");
//            });
//        }
//    });
//};

//employee.drawTable2 = function () {
//    $.ajax({
//        url: "https://localhost:44309/DataDauVaos/SapSepTheoNumber",
//        method: 'GET',
//        dataType: 'json',
//        contentType: 'application/json; charset=utf-8',
//        success: function (data) {
//            $('#travelNumber').empty();
//            $.each(data, function (index, value) {
//                $('#travelNumber').append("<tr>" +
//                    "<td>" + value.number + " = " + value.ketqua + "</td>" +
//                    "</tr>");
//            });
//        }
//    });
//};




//employee.init = function () {
//    employee.drawTable();
//    employee.drawTable2();
//};

//$(document).ready(function () {
//    employee.init();
//});


