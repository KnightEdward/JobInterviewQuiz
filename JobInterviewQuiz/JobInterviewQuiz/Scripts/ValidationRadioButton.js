$(document).on('submit', 'form', 'buttonNext', function () {

    var validate = true;
    var unanswered = new Array();

    // Loop through available sets
    $('.rdbtn').each(function () {
        // Question text
        var question = $(this).prev().text();
        // Validate
        if (!$(this).find('input').is(':checked')) {
            // Didn't validate ... dispaly alert or do something
            unanswered.push(question);

            validate = false;
        }
    });
    // $swal({ title: "Auto close alert!", text: "I will close in 2 seconds.", timer: 2000, showConfirmButton: false });

    if (unanswered.length > 0) {
        msg = "Please Answer:\n" + unanswered.join('\n');
        alert(msg);

    }
    return validate;
});