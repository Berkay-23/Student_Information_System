const notes = document.querySelectorAll("#note-table-area input");

notes.forEach(element => {
    element.onkeypress = numbersOnly; // Sadece rakam ve virg�l giri�i oldu�unu kontrol ediyor.
    element.maxLength = 6 // CSS �zerinden max legth de�i�memesi i�in max length her press'de yenileniyor

    element.addEventListener('input', () => {
        var text = element.value
        var virgule_counter = 0;

        for (var i = 0; i < text.length; i++)
            if (text.charAt(i) == ',') virgule_counter += 1; // text i�inde 2. virg�l�n varsa tespiti yap�l�yor

        if (virgule_counter > 1)
            element.value = text.substring(0, text.length - 1) // varsa 2. virg�l siliniyor.

        value = element.value.replace(/,/g, '.') // Float d�n���m� yapmak i�in virg�l noktaya �evriliyor.
        var value = Number(value)

        if (value > 100) {
            element.value = 100 // Girilen not 100'den y�ksekse 100'e �eviriyor
        } else if (value < 0) {
            element.value = 0 // Girilen not 0'dan azsa 0'a �eviriyor
        }
    });
});

function numbersOnly(event) {
    var character = String.fromCharCode(event.which);
    if (!/[0-9,]/.test(character)) {
        event.preventDefault();
    }
}

notes.forEach(element => {
    element.addEventListener('blur', () => {
        var lesson_id = element.name.split('-')[1] // Ders id'sini ald� (1, 30, 27, 23 ...)
        var inputs = $(`input[name$="-${lesson_id}"]`) // Ders id'sine ba�l� inputlar� ald�.

        var first_exam = null,
            second_exam = null

        inputs.each(i => {

            var exam_code = inputs[i].name.split('-')[0]

            switch (exam_code) { // inputlardaki de�erler 1. ve 2. s�nav bilgisini doldurmak i�in switch yap�s�na girdi

                case 'mid':
                    first_exam = inputs[i].value.replace(/,/g, '.')
                    break;

                case 'mk1':
                    first_exam = inputs[i].value.replace(/,/g, '.')
                    break;

                case 'fin':
                    second_exam = inputs[i].value.replace(/,/g, '.')
                    break;

                case 'mk2':
                    second_exam = inputs[i].value.replace(/,/g, '.')
                    break;
            }
        })

        var avg = (first_exam * 0.4 + second_exam * 0.6).toFixed(1) // Ortalama hesapland� format: (70.0)
        var avg_th = $(`th[name="avg-${lesson_id}"]`) // Ortalaman�n yazd��� <th> nesnesi al�nd�

        /* G�ncel ortalama <th> nesnesine yaz�ld� */
        if (avg % parseInt(avg) == 0)
            avg_th[0].textContent = parseInt(avg).toString().replace('.', ',')
        else
            avg_th[0].textContent = avg.toString().replace('.', ',')
    });
});

let search_btn = document.querySelector(".search-button");
let search_input = document.querySelector(".search-input");

search_btn.addEventListener('click', () => {
    location.href = `${base}#${search_input.value}`;
});

search_input.addEventListener('keyup', function (event){

    if (event.keyCode == 13) {
        location.href = `${base}#${search_input.value}`;
        event.preventDefault();
    }
});