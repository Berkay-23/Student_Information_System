$(`nav ul li:nth-child(${active_index}) > a`).addClass('active')

if (active_index == 6) {
    $(".dropbtn").css({ backgroundColor: '#ff7b25' });
}

checkbox = $("#check:checked").change(() => {
    let checkState = $("#check").is(":checked");

    if (checkState == true) {
        $(".checkbtn i").addClass('active')
    } else {
        $(".checkbtn i").removeClass('active')
    }
});