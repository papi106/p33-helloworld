// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#addMembersButton").click(function () {
        var newcomerName = $("#nameField").val();

        // Remember string interpolation
        $("#teamMembers").append(`<li>${newcomerName}</li>`);

        $("#nameField").val("");
    })
});