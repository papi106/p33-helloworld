// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#addMembersButton").click(function () {
        var newcomerName = $("#nameField").val();
        var length = $("#teamMembers").children().length;

        $.ajax({
            url: "/Home/AddTeamMember",
            method: "POST",
            data: {
                teamMember: newcomerName
            },
            success: function (result) {
                // Remember string interpolation
                $("#teamMembers").append(
                    `<li class="member">
                        <span class="name" >${newcomerName}</span>
                        <span class="delete fa fa-remove" onclick="deleteMember(${length})"></span>
                        <span class="pencil fa fa-pencil" ></span>
                    </li>`);

                $("#nameField").val("");
            }
        })
    })
});

function deleteMember(index) {

    $.ajax({
        url: "/Home/RemoveMember",
        method: "DELETE",
        data: {
            memberIndex: index
        },
        success: function (result) {
            location.reload();
        }
    })
}