const notes = document.querySelectorAll("#note-table-area input");

notes.forEach(element => {
    element.onkeypress = numbersOnly;
    element.maxLength = 6
    element.addEventListener('blur', () => {
        value = element.value.replace(/,/g, '.')
        var value = Number(value)
        if (value > 100) {
            element.value = 100
        } else if (value < 0) {
            element.value = 0
        }
    });
});

function numbersOnly(event) {
    var character = String.fromCharCode(event.which);
    if (!/[0-9-,]/.test(character)) {
        event.preventDefault();
    }
}