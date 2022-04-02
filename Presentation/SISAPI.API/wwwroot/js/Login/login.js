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

        if (!/[a-z]/.test(character) && !/[0-9]/.test(character)) {
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

    if ($("#studentRadioBtn").prop('checked') == true) {
        inputMail.style.boxShadow = "0px 0px 0px 3px #ffc107";
    } else {
        inputMail.style.boxShadow = "0px 0px 0px 3px #dc3545";
    }
});

inputPassword.addEventListener("focusin", () => {

    if ($("#studentRadioBtn").prop('checked') == true) {
        inputPassword.style.boxShadow = "0px 0px 0px 3px #ffc107";
    } else {
        inputPassword.style.boxShadow = "0px 0px 0px 3px #dc3545";
    }
});

inputMail.addEventListener("focusout", () => {
    inputMail.style.boxShadow = "none";
});

inputPassword.addEventListener("focusout", () => {
    inputPassword.style.boxShadow = "none";
});

function reportWindowSize() {

    if (window.innerWidth < 570) {
        $("#loginForm").width($(".userType").width());
        $("#submit-btn").width($("#studentRadio").width());

        
    } 
    else {
        $("#loginForm").css('width', '450px');
        $("#submit-btn").css('width', '386px');
    }

}
reportWindowSize()
window.onresize = reportWindowSize;