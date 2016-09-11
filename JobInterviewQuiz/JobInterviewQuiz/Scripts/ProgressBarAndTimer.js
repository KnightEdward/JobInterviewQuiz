
function startProgressBar(progressBarClass, startPoint, numberOfSeconds) {

    var progBarStartPoint = startPoint + "%";

    var start = startPoint;

    var piece = (100 - start) / numberOfSeconds;

    var progressBar = $("." + progressBarClass);

    progressBar.css({ 'width': progBarStartPoint });


    window.setInterval(function () {

        start = IncrementProgressBar(progressBar, start, piece);

    }, 1000, true);



}

function startTime(givenHours, givenMinutes, givenSeconds) {
    var hours = $("#hours");
    hours.text(GetWatchNumber(givenHours));


    var minutes = $("#minutes");
    minutes.text(GetWatchNumber(givenMinutes));

    var seconds = $("#seconds");
    seconds.text(GetWatchNumber(givenSeconds));

    var warning = $("#warningMessage");

    var timeLeft = timeToSeconds(parseInt(hours.text()), parseInt(minutes.text()), parseInt(seconds.text()));

    if (timeLeft < 300 && timeLeft > 285)
        warning.show();



    var timer = setInterval(function () {

        hours = $("#hours");
        minutes = $("#minutes");
        seconds = $("#seconds");

        timeLeft = timeToSeconds(parseInt(hours.text()), parseInt(minutes.text()), parseInt(seconds.text()));

        if (hours.text() == "00" && minutes.text() == "05" && seconds.text() == "00") {
            warning.text("You have 5 minutes left!");

            warning.fadeIn();
        }



        if (timeLeft < 285)
            warning.fadeOut();


        var check = IncrementTime(hours, minutes, seconds);

        if (check == 0) {
            $(".buttonNext").hide();
            $(".buttonPrevious").hide();

            warning.text("Time's Up!");

            warning.fadeIn();

            window.location = "Summary";

            clearInterval(timer);
        }




    }, 1000, true);

}

function GetWatchNumber(givenNumber) {
    return givenNumber >= 10 ? givenNumber : "0" + givenNumber;
}

function timeToSeconds(givenHours, givenMinutes, givenSeconds) {
    return givenHours * 3600 + givenMinutes * 60 + givenSeconds;
}

function startTimeAndBar(progressBarClass, startPoint, givenHours, givenMinutes, givenSeconds) {
    startProgressBar(progressBarClass, startPoint, timeToSeconds(givenHours, givenMinutes, givenSeconds));
    startTime(givenHours, givenMinutes, givenSeconds);
}

(document).read

function IncrementProgressBar(progressbar, start, piece) {

    $(document).ready(function () {

        start += piece;
        var advance = start + "%";
        progressbar.css({ 'width': advance });


    });

    return start;

}


function IncrementTime(hours, minutes, seconds) {
    if (hours.text() == "00" && minutes.text() == "00" && seconds.text() == "00")
        return 0;


    if (seconds.text() == "00") {
        if (minutes.text() == "00") {
            hours.text(GetWatchNumber(hours.text() - 1));
            minutes.text("59");
            seconds.text("59");
        }
        else {
            minutes.text(GetWatchNumber(minutes.text() - 1));
            seconds.text("59");
        }
    }
    else {
        seconds.text(GetWatchNumber(seconds.text() - 1));
    }

    return 1;

}

function check() {
    window.setInterval(function () {

        var seconds = $("#seconds");
        seconds.text(GetWatchNumber(sessionStorage["Seconds"]));

    }, 1000, true);


}