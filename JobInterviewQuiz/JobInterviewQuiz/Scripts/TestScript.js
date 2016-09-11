function MessageCheck(errorMessage, successMessage) {

    var error = $("#errorMessage");
    var success = $("#successMessage");

    if (errorMessage == 0 && successMessage == 0) {
        error.hide();
        success.hide();
    }
    else if (errorMessage == 0) {
        error.hide();
        success.show();
    }
    else if (successMessage == 0) {
        error.show();
        success.hide();
    }

    error.fadeOut(9000);
    success.fadeOut(9000);

}

function DeleteTest(testId, rowId, givenModel) {
    $.ajax({

        type: "POST",
        url: 'DeleteTest',
        data: { id: testId, test: 'intra sau nu', model: givenModel }

    });

    $("#" + rowId).remove();

}

