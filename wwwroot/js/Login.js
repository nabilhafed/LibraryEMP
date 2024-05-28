
let stepper = new Stepper($('#stepper')[0]);
$('#next-button').on('click', function () {
    stepper.next();
});