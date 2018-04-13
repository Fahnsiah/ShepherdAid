function setRegionOrCountyField() {

    var selectedNationality = $("#NationalityTypeID").val();

    if (parseInt(selectedNationality) == 187) {
        //set field to default
        $("#CountyID").prop("selectedIndex", 0);

        $("#divcounties").show();
        $("#divregion").hide();
    }
    else {
        //set field to default
        $("#Region").prop("value", "");
        $("#Region").focus();

        $("#divcounties").hide();
        $("#divregion").show();
    }
}

function showRegionOrCountyField() {
    
    var regionVal = $("#Region").val();

    if (regionVal == null) {
        
        $("#divcounties").show();
        $("#divregion").hide();
    }
    else
    {
        $("#divcounties").hide();
        $("#divregion").show();
    }
}

$(document).ready(function () {
   
    showRegionOrCountyField();

    $("#NationalityTypeID").on("change", setRegionOrCountyField);
});