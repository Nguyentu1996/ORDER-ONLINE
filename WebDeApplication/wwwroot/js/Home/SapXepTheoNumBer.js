function SapXepNumber() {
    $.ajax({
        url: "/DataDauVaos/SapSepTheoNumber",
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

