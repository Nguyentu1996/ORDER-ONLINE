var student = student || {}
var debochon = 3000;
var deduocchon = 2000;
var dechon = 1000;
var xienbochon = 6000;
var xienduocchon = 5000;
var xienchon = 4000;
function SapXepGia() {
    $.ajax({
        url: "/DataDauVaos/SapSepTheoGia",
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#travelID').empty();
            $.each(data, function (index, value) {
                $('#travelID').append("<tr>" +
                    "<td>" + value.number + " = " + value.ketqua + "</td>" +
                    "</tr>");

            });
        }
    });
}


student.drawTable = function () {
    $.ajax({
        url: "/DataDauVaos/SapSepTheoGia",
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#travelID').empty();
            $.each(data, function (index, value) {
                $('#travelID').append("<tr>" +
                    "<td>" + value.number + " = " + value.ketqua + "</td>" +
                    "</tr>");
            });
        }
    });
};

function luutrudulieu() {
    var numberd = 0;
    $.ajax({
        url: "/LuuTruDatas/Index",
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#luutrudulieu').empty();
            $.each(data, function (index, value) {
                numberd++;
                $('#luutrudulieu').append("<tr><th scope ='row' >" + numberd + "</th ><td>" + value.ngayTao + "</td><td><button type='button' class='btn btn-info' onclick=copyToClipboard('" + "#data" + value.id + "')>Copy"
                    + "</button ><p hidden id=#data" + value.id + ">" + value.noiDung + "</p></td ></tr >");
            });
        }
    });
};

function copyCacLoaiDe(element) {
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val($(element).html()).select();
    document.execCommand("copy");
    $temp.remove();
}


function copyToClipboard(element) {
    var $temp = $("<input>");
    $("#luutrudulieu").append($temp);
    $temp.val($(element).html()).select();
    document.execCommand("copy");
    $temp.remove();
}
student.init = function () {
    student.drawTable();
};

$(document).ready(function () {
    student.init();
});

var numtran
var XuLyGiaTheoSo;
$(document).ready(function () {
    XuLyGiaTheoSo = function () {
        var XienBoString = "";
        var XienChonString = "";
        var XienListChonString = "";
        var DeListChonString = "de";
        var DeChonString = "de";
        var DeBoString = "de";
        var studentObj = {};
        studentObj.De = document.getElementById("de").value;
        studentObj.Xien = document.getElementById("xien").value;
        studentObj.Bc = document.getElementById("bc").value;
        for (var i = 1001; i <= 10000; i++) {
            $('#' + i).empty();
        }
        $.ajax({
            url: "/DataDauVaos/XuLyGiaTheoSo",
            method: 'GET',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: { 'De': studentObj.De, 'Xien': studentObj.Xien, 'BC': studentObj.Bc },

            success: function (data) {
                $.each(data.luuTruDeListChon, function (index, value) {
                    dechon++;
                    $('#' + dechon).empty();
                    $('#' + dechon).append(value.number + " x " + value.ketqua);
                    DeListChonString = DeListChonString + " " + value.number + " x" + value.ketqua
                });
                

                    $.each(data.luuTruDe, function (index, value) {
                        deduocchon++;
                        $('#' + deduocchon).empty();
                        $('#' + deduocchon).append(value.number + " x " + value.ketqua);
                        DeChonString = DeChonString + " " + value.number + " x" + value.ketqua
                    });
                $.each(data.dataDeBo, function (index, value) {
                    debochon++;
                    $('#' + debochon).empty();
                    $('#' + debochon).append(value.number + " x" + value.ketqua);
                    DeBoString = DeBoString + " " + value.number + " x" + value.ketqua
                });

                $('#allDe').empty();
                $('#allDe').append(data.tongTienDeAll + " / " + data.soLuongDe + " con");

                $('#dataDechon').empty();
                $('#dataDechon').append(data.tongTienDeChon + " / " + data.soLuongDeChon + " con");

                $('#dataDeBo').empty();
                $('#dataDeBo').append(data.tongTienDeBo + " / " + data.soLuongDeBo + " con");

                $.each(data.luuTruXienListChon, function (index, value) {
                    xienchon++;
                    $('#' + xienchon).empty();
                    $('#' + xienchon).append(value.number + " x" + value.ketqua);
                    XienListChonString = XienListChonString + " xien " + value.number + " x" + value.ketqua
                });

                $.each(data.luuTruXien, function (index, value) {
                    xienduocchon++;
                    $('#' + xienduocchon).empty();
                    $('#' + xienduocchon).append(value.number + " x" + value.ketqua);
                    XienChonString = XienChonString + " xien " + value.number + " x" + value.ketqua
                });
                $.each(data.dataXienBo, function (index, value) {
                    xienbochon++;
                    $('#' + xienbochon).empty();
                    $('#' + xienbochon).append(value.number + " x" + value.ketqua);
                    XienBoString = XienBoString + " xien " + value.number + " x" + value.ketqua
                });

                $('#allXien').empty();
                $('#allXien').append(data.tongTienXienAll + " / " + data.soLuongXien + " con");

                $('#dataXienchon').empty();
                $('#dataXienchon').append(data.tongTienXienChon + " / " + data.soLuongXienChon + " con");

                $('#dataXienBo').empty();
                $('#dataXienBo').append(data.tongTienXienBo + " / " + data.soLuongXienBo + " con");
                $('#dechonstring').empty();
                $('#dechonstring').append(DeChonString);
                $('#debostring').empty();
                $('#debostring').append(DeBoString);
                $('#delistchonstring').empty();
                $('#delistchonstring').append(DeListChonString);
                $('#xienlistchonstring').empty();
                $('#xienlistchonstring').append(XienListChonString);
                $('#xienchonstring').empty();
                $('#xienchonstring').append(XienChonString);
                $('#xienbostring').empty();
                $('#xienbostring').append(XienBoString);
                
                
                     deduocchon = 2000;
                     dechon = 1000;
                     debochon = 3000;
                     xienbochon = 6000;
                     xienduocchon = 5000;
                     xienchon = 4000;
            }
        });
    };
});

$(document).ready(function () {
    ListSoHienThi = function () {


        for (var i = 3001; i <= 3500; i++) {
            $('#' + i).empty();
        }
        $.ajax({
            url: "/DataDauVaos/ListSoHienThi",
            method: 'GET',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $.each(data, function (index, value) {
                    numberlist++;
                    $('#' + numberlist).empty();
                    $('#' + numberlist).append(value.number + " x " + value.ketqua);
                });

                
            }

        });
    };
});

function Them() {
    $.ajax({
        url: "/DataDauVaos/ABC",
        method: 'GET',
        dataType: 'json',

    });
}

function SaveData() {
    $.ajax({
        url: "/DataDauVaos/LuuTruData",
        method: 'GET',
        dataType: 'json',
        
    });
}

function ChonGiaChonSo() {
    
        var studentObj = {};
    studentObj.Number = document.getElementById("number").value;
    studentObj.Result = document.getElementById("ketqua").value;
    $.ajax({
        url: "/DataDauVaos/XuLyGia",
        method: 'POST',
        data: { 'Number': studentObj.Number, 'Result': studentObj.Result },
        
        success: function (data) {
            
            $('#ListSoChon').empty();
            $('#ListSoChon').append("<tr>" +
                "<td>" + "Tổng tiền  = " + data.tongTienChon + "</td>" +
                "</tr>");
            $.each(data.dataNhan, function (index, value) {
                $('#ListSoChon').append("<tr>" +
                    "<td>" + value.number + " = " + value.ketqua + "</td>" +
                    "</tr>");
            });
            
            $('#DataBo').empty();
            $('#DataBo').append("<tr>" +
                "<td>" + "Tổng tiền  = " + data.tongTienBo + "</td>" +
                "</tr>");
            $.each(data.dataBo, function (index, value) {
                $('#DataBo').append("<tr>" +
                    "<td>" + value.number + " = " + value.ketqua + "</td>" +
                    "</tr>");
            });
        }
        //success: function (data) {
        //    $('#travelID').empty();
        //    $.each(data, function (index, value) {
        //        $('#travelID').append("<tr>" +
        //            "<td>" + value.number + " = " + value.ketqua + "</td>" +
        //            "</tr>");
        //    });
        //}
    });
    
};