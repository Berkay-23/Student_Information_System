let add_labels = $(`label[for^='check']`);
let checks = $(`input[id^='check']`);
var total_ects = 0;

add_labels.click(function () {
    var index = add_labels.index(this);
    let icon = add_labels.eq(index).children();

    icon.toggleClass('fa-regular');
    icon.toggleClass('fa-solid');
    icon.toggleClass('fa-square-plus');
    icon.toggleClass('fa-square-minus');

    if (checks.eq(index).prop('checked') == false) {

        total_ects += Number(add_labels.eq(index).parent().parent().children().eq(3).text());
        $('#ects').text(total_ects);

        if (total_ects <= 40) {
            $('.area-lessons').append(`
                <div class="lesson-${add_labels.eq(index).get()[0].htmlFor.split('-')[1]}">
                    <ul class="list-group list-group-horizontal m-2">
                        <li class="list-group-item col-4">${add_labels.eq(index).parent().parent().children().eq(0).text()}</li>
                        <li class="list-group-item col-8">${add_labels.eq(index).parent().parent().children().eq(1).text()}</li>
                    </ul>
                </div>`);

            $('#ects').text(total_ects);
        }
        else {
            total_ects -= Number(add_labels.eq(index).parent().parent().children().eq(3).text());
            $('#ects').text(total_ects);

            icon.toggleClass('fa-regular');
            icon.toggleClass('fa-solid');
            icon.toggleClass('fa-square-plus');
            icon.toggleClass('fa-square-minus');

            checks.eq(index).click()
        }

    } else {
        total_ects -= Number(add_labels.eq(index).parent().parent().children().eq(3).text());
        $('#ects').text(total_ects);

        let id = add_labels.get()[index].htmlFor.split('-')[1]
        $(`.lesson-${id}`).remove()
    }
    ects_control();
});

function ects_control() {
    d = Math.round(((total_ects / 40)) * 100)
    $('.alert').css({ background: 'rgb(0,212,255)', background: `linear-gradient(90deg, rgba(0,212,255,1) 0%, rgba(9,9,121,1) ${d}%, rgba(0,0,0,1) 100%)` })
}

ects_control();
