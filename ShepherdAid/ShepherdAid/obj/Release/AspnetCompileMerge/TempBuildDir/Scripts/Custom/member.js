function showRegionOrCountyField() {
    
    var selectedNationality = $("#NationalityTypeID").val();
    var countryID = $("#defaultCountry").val();
    //alert("country id " + countryID);
    if (parseInt(selectedNationality) == parseInt(countryID)) {
        //set field to default
        $("#CountyID").prop("selectedIndex", 0);

        $("#divcounties").show();
        $("#divregion").hide();
    }
    else
    {
        //set field to default
        $("#Region").prop("value", "");
        $("#Region").focus();

        $("#divcounties").hide();
        $("#divregion").show();
    }
}

$(document).ready(function () {
   

    //alert("breathe");
    //hide the region until a nationality other than liberian is selected
    $("#divregion").hide();
    $("#NationalityTypeID").on("change", showRegionOrCountyField);

    
});