
let stepper = new Stepper($('#stepper')[0]);

$("#next-button").click(function (e) { 
    e.preventDefault();
    if($("#username").val().length > 0 )
        stepper.next();
});

$("#username").on("keypress", function (e) {
    if (e.which === 13) { // Check if Enter key is pressed
        e.preventDefault(); // Prevent the form from submitting
        $("#next-button").click();
    }
});