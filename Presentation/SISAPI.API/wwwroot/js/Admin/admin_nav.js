let homepage = document.querySelector("nav ul li:nth-child(1) > a")
let studends = document.querySelector("nav ul li:nth-child(2) > a")
let academics = document.querySelector("nav ul li:nth-child(3) > a")
let profile = document.querySelector(".profile a")

switch (active_index) {

    case 1:
        $('nav ul li:nth-child(1) > a').addClass('active')
        break;

    case 2:
        $('nav ul li:nth-child(2) > a').addClass('active')
        break;

    case 3:
        $('nav ul li:nth-child(3) > a').addClass('active')
        break;

    case 4:
        $(".dropbtn").css({
            backgroundColor: '#ff7b25'
        });
        break;
}

checkbox = $("#check:checked").change(() => {
    let checkState = $("#check").is(":checked");

    if (checkState == true) {
        $(".checkbtn i").addClass('active')
    } else {
        $(".checkbtn i").removeClass('active')
    }
});