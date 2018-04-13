
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


$(document).ready( function (){

    $("#InstitutionGroupID").on("change", populateInstitutions);
});

