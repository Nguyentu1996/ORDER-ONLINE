var a = null;
var b = null;
a = document.getElementById("number").value;
b = document.getElementById("ketqua").value;
function ChonGiaChonSo() {
    $.ajax({
        url: "/DataDauVaos/XuLyGia",
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: { 'Number': a, 'Result': b },

        success: function (data) {
            $('#ListSoChon').empty();
            $.each(data.dataNhan, function (index, value) {
                console.log(data.dataNhan)
                $('#ListSoChon').append("<tr>" +
                    "<td>" + value.number + " = " + value.ketqua + "</td>" +
                    "</tr>");
            });


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
