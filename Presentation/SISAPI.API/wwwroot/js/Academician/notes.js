$(`input`).each(function (index, element) {
    element.onkeypress = numbersOnly;
    element.maxLength = 6

    element.addEventListener('input', () => {
        var text = element.value
        var virgule_counter = 0;

        for (var i = 0; i < text.length; i++)
            if (text.charAt(i) == ',') virgule_counter += 1;

        if (virgule_counter > 1)
            element.value = text.substring(0, text.length - 1)

        value = element.value.replace(/,/g, '.')
        var value = Number(value)

        if (value > 100) {
            element.value = 100
        }
        else if (value < 0) {
            element.value = 0
        }
    });
});

$(`input`).each(function (index, element) {
    element.addEventListener('blur', () => {
        var student_no = element.name.split('-')[1]

        var inputs = $(`input[name$=${student_no}]`)

        var first_exam = "", second_exam = ""

        inputs.each(function (i, elm) {

            var exam_code = inputs[i].name.split('-')[0]

            switch (exam_code) {

                case 'mid':
                    if (inputs[i].value != "") {
                        first_exam = inputs[i].value.replace(/,/g, '.')
                    }
                    break;

                case 'mk1':
                    if (inputs[i].value != "") {
                        first_exam = inputs[i].value.replace(/,/g, '.')
                    }
                    break;

                case 'fin':
                    if (inputs[i].value != "") {
                        second_exam = inputs[i].value.replace(/,/g, '.')
                    }
                    break;

                case 'mk2':
                    if (inputs[i].value != "") {
                        second_exam = inputs[i].value.replace(/,/g, '.')
                    }
                    break;
            }
        });

        var avg;

        if (second_exam != "") {
            avg = (first_exam * 0.4 + second_exam * 0.6).toFixed(1)
        }
        else {
            avg = Number(first_exam).toFixed(1)
        }

        var avg_th = $(`#${student_no}`).children().last()

        if (avg % parseInt(avg) == 0)
            avg_th[0].textContent = parseInt(avg).toString().replace('.', ',')
        else
            avg_th[0].textContent = avg.toString().replace('.', ',')

    });
});

function numbersOnly(event) {
    var character = String.fromCharCode(event.which);
    if (!/[0-9,]/.test(character)) {
        event.preventDefault();
    }
}