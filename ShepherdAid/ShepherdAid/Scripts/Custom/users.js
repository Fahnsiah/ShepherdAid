
function populateInstitutions() {
    //alert("working!");
    $("#InstitutionID").empty();
    $("#InstitutionID").append("<option value='0'>--select--</option>");
    $.get("/Users/GetInstitutions", { id: $("#InstitutionGroupID").val() }, function (data) {
        $.each(data, function (index, row) {
            $("#InstitutionID").append("<option value='" + row.ID + "'>" + row.InstitutionName + "</option>")
        });
    });
}

function populateGroups() {
    //alert("working in group!");
    $("#UserGroupID").empty();
    $("#UserGroupID").append("<option value='0'>--select--</option>");
    $.get("/Users/GetGroups", { id: $("#InstitutionID").val() }, function (data) {
        $.each(data, function (index, row) {
            $("#UserGroupID").append("<option value='" + row.ID + "'>" + row.Name + "</option>")
        });
    });
}

$(document).ready(function () {

    $("#InstitutionGroupID").on("change", populateInstitutions);

    $("#InstitutionID").on("change", populateGroups);
});

