function getUserDetalies(id) {
    $.ajax({
        type: "POST",
        url: "EditUser",
        data: { id: id },
        success: OnSuccess
    });
}

function OnSuccess(data) {
    var holder = $("#editUserModalHolder");
    holder.html(data);
    $("#myModal").modal("show");
}

function DeleteUser(id) {
    var conf = confirm("Are you sure you want to delete?");
    if (conf === true) {
        $.ajax({
            type: "POST",
            url: "DeleteUser",
            data: { id: id },
            success: RefreshFunction
        });
    }
}

function RefreshFunction() {
    window.location.reload(true);
}

function UpdateUser() {
    var form = $("#EditUserForm");
    $.ajax({
        url: form.attr('action'),
        type: form.attr('method'),
        data: $(form).serialize(),
        success: function (result) {
            if (result.success)
                RefreshFunction();
            $(form.data("ajax-update")).html(result);
            $('.modal-backdrop').css("opacity", 0);
            $("#myModal").modal("show");
        }
    });
    return false;
}

$(function () {
    var search = $('#SearchUsers');
    search.keyup(function (event) {
        var textToSearch = $(this).val();
        var id = $('#TableData');
        var cellsToFilter = id.find('tbody tr');
        if (!textToSearch) {
            cellsToFilter.show();
        } else {
            cellsToFilter.each(function (index) {
                var row = $(this);
                var cellToFilter = row.find('td:first').text();
                if (cellToFilter.toLowerCase().indexOf(textToSearch.toLowerCase()) < 0) {
                    row.hide();
                } else {
                    row.show();
                }
            });
        }
    });
});