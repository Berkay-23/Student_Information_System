const inputMail = document.getElementById("input-mail");
const inputPassword = document.getElementById("input-pass");


function isInputNumber(event) {
    var character = String.fromCharCode(event.which);

    if ($("#studentRadioBtn").prop('checked') == true) {

        inputMail.maxLength = "9";

        if (!/[0-9]/.test(character)) {
            event.preventDefault();
        }
    } else {
        if (!/^[a-zA-Z0-9-������-������]*$/.test(character)) {
            event.preventDefault();
        }
        inputMail.maxLength = "20";
    }
}

$("#studentRadioBtn").on("click", function (event) {
    inputMail.value = "";
    inputPassword.value = "";

    $("#submit-btn").even().removeClass("btn-outline-danger");
    $("#submit-btn").even().addClass("btn-outline-warning");

});

$("#academicianRadioBtn").on("click", function (event) {
    inputMail.value = "";
    inputPassword.value = "";

    $("#submit-btn").even().removeClass("btn-outline-warning");
    $("#submit-btn").even().addClass("btn-outline-danger");

});

inputMail.addEventListener("focusin", () => {
    fillBoxShodow(inputMail)
});

inputPassword.addEventListener("focusin", () => {
    fillBoxShodow(inputPassword)
});

inputMail.addEventListener("focusout", () => {
    inputMail.style.boxShadow = "none";
});

inputPassword.addEventListener("focusout", () => {
    inputPassword.style.boxShadow = "none";
});

function fillBoxShodow(element) {
    if ($("#studentRadioBtn").prop('checked') == true) {
        element.style.boxShadow = "0px 0px 0px 3px #ffc107";
    } else if ($("#academicianRadioBtn").prop('checked') == true) {
        element.style.boxShadow = "0px 0px 0px 3px #dc3545";
    } else {
        element.style.boxShadow = "0px 0px 0px 3px #6610f2";
    }
}


$(function () {
    $("form[name='registration']").validate({
        lang: 'tr',
        rules: {
            UserName: {
                required: true,
                minlength: 7
            },
            Password: {
                required: true,
                minlength: 5
            }
        },
        messages: {

            UserName: {
                required: "L�tfen kullan�c� ad�n� giriniz.",
                minlength: "Kullan�c� ad� minimum 7 karakterden olu�mal�."
            },
            Password: {
                required: "L�tfen �ifrenizi giriniz.",
                minlength: "�ifreniz en az 5 karakterden olu�mal�."
            }
        },
        errorPlacement: function (error, element) {

            if (element.attr("name") == "UserName")
                error.insertAfter("#submit-btn");

            else if (element.attr("name") == "Password")
                error.insertBefore("#lower-hr");

        },
        errorElement: 'div',
        errorClass: 'error',
        submitHandler: function (form) {
            form.submit();
        }

    });
});
