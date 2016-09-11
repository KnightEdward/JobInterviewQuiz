$(document).ready(function () {
    $('#VisitorDetail').hide();
});

$("#PassId").autocomplete({
    source: function (request, response) {
        var customer = new Array();
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "/Home/Autocomplete",
            data: { "term": request.term },
            success: function (data) {
                for (var i = 0; i < data.length ; i++) {
                    customer[i] = { label: data[i].Value, Id: data[i].Key };
                }
            }
        });
        response(customer);
    },

    select: function (event, ui) {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "/Home/GetDetail",
            data: { "id": ui.item.Id },
            success: function (data) {
                $('#tableQuestion').hide();
                $(".editButtons").removeClass("hidden");
                $(".btnAdd").addClass("hidden");


                $("#QuestionID").val(ui.item.Id);
                $("#Question").val(ui.item.label);
                $('#VisitorDetail').show();

                $("#Answers").empty();
                $('#dropDownTypeUpdate').empty();
                $('#dropDownCategoryUpdate').empty();
                $("#ValueAnswer").empty();
                var valueAnswerList = [];
                var answerList = [];
                var idAnswerList = [];
                IsCheckDeleteList = [];
                for (var ans in data.Answers) {
                    answerList[ans] = data.Answers[ans];
                    var anwerId = "answer" + ans;
                    var answerElement = "<div class='input-group input-group-lg' name='Answers'><input class='form-control' name='Answers' style='width: 280px' type='text' id='" + anwerId + "'/></div>";
                    $("#Answers").append(answerElement);
                    $("#" + anwerId).val(answerList[ans]);
                }
                for (var answer in data.ValueAnswer) {
                    valueAnswerList[answer] = data.ValueAnswer[answer];
                    var valueAnswerId = "ValueAnswer" + answer;
                    if (data.ValueAnswer[answer] == "True")
                        var valueAnswerElement = "<div class='input-group input-group-lg' name='ValueAnswer' id='" + valueAnswerId + "'><select name='ValueAnswer' class='form-control' style='width: 280px'>" + "'<option value='Correct'>Correct</option>'" + "'<option value='Incorrect'>Incorrect</option>' " + "</select></div>";
                    else
                        var valueAnswerElement = "<div class='input-group input-group-lg' name='ValueAnswer' id='" + valueAnswerId + "'><select name='ValueAnswer' class='form-control' style='width: 280px'>" + "'<option value='Incorrect'>Incorrect</option>'" + "'<option value='Correct'>Correct</option>' " + "</select></div>";
                    $("#ValueAnswer").append(valueAnswerElement);
                    $("#" + valueAnswerId).val(valueAnswerList[answer]);
                }
                for (var idAns in data.AnswersIds) {
                    var valueAnswerElement = "<input name='AnswersIds' type='hidden' value='" + data.AnswersIds[idAns] + "'/>";
                    $("#AnswersIds").append(valueAnswerElement);
                }
                var scntDiv1 = $('#dropDownCategoryUpdate');
                var select = "<div class='input-group input-group-lg'> <select class='form-control' style='width: 280px' name='Category' id='dropDownCategoryUpdate'>";
                var numberOfTrueAnswers = 0;
                for (var technology in data.TechnologyList) {
                    select = select + "<option value=" + data.TechnologyList[technology] + ">" + data.TechnologyList[technology] + "</option>";
                    numberOfTrueAnswers++;
                }
                select = select + "</select>" + "</div>";
                $(select).appendTo(scntDiv1);
                $("#Level").val(data.Level);



                var scntDiv = $('#dropDownTypeUpdate');
                $("<div class='input-group input-group-lg'>" + "<select class='form-control' style='width: 280px' name='Type'>" + '<option value="Radio">Radio</option>' + '<option value="Check">Check box</option>' + '</select></div>').appendTo(scntDiv);
                action = data.Action;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Failed to retrieve states.');
            }
        });
    }
});


$(function () {
    var scntDiv = $('#p_scents');
    var scntDiv1 = $('#p_scentsValue');
    var obj = $('#stuff');
    var i = $('#p_scents').size() + 1;
    $('#addScnt').on('click', function () {
        scntDiv.append(obj.html().replace('p_scnt', 'p_scnt' + i).replace('.Answers[' + (i - 1), '.Answers[' + i));
        var idContor = "#p_scnt";
        var num = i;
        var rez = idContor.concat(num);
        $(rez).val('');
        i++;
        return false;
    });
});
function AddQuestion() {
    var form = $("#AddQuestionForm");
    form.submit();
}