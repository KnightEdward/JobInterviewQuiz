function InvalidMsg(textbox, type) {

    if (type == 'warningQuestion') {
        if (textbox.value == '') {
            textbox.setCustomValidity('Please insert a question');
        }
        else {
            textbox.setCustomValidity('');
        }
    }
    else if (type == 'warninAnswer') {
        if (textbox.value == '') {
            textbox.setCustomValidity(' Please insert an answer ');
        }
        else {
            textbox.setCustomValidity('');
        }
    }
    else if (type == 'warningLevel') {
        if (textbox.value == '') {
            textbox.setCustomValidity(' Please insert a level');
        }
        else {
            textbox.setCustomValidity('');
        }
    }

    return true;
}

