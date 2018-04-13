
$(document).ready(function () {



    /*------------------------------Available Roles--------------------------------------*/

    //Ensure that at least one available role is selected
    $(document).on("click", "#assign", function () {
        var count = $('#divavailablecheckbox input[type="checkbox"]:checked').length;
        if (count < 1) {
            alert("No role selected. Select at least one available role.");
            return false;
        }
    });//on click assign$('#allavailableroles').click

    //check and uncheck all roles when the Check All is clicked
    $("#allavailableroles").click(function () {
        var i = 0;
        if ($("#allavailableroles").is(':checked')) {
            $('#divavailablecheckbox input[type=checkbox]').each(function () {
                $(this).prop('checked', true);
            });
        }
        else {
            $('#divavailablecheckbox input[type=checkbox]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });//click on allavailableroles

    //check and uncheck the allavailableroles based on roles checked
    $(document).on("click", "#availableroles", function () {

        var allcount = $('#divavailablecheckbox input[type="checkbox"]').length;
        var checkedcount = $('#divavailablecheckbox input[type="checkbox"]:checked').length;

        if (checkedcount == allcount) {
            $("#allavailableroles").prop("checked", true);
        }
        else {
            $("#allavailableroles").prop("checked", false);
        }
    });//click on available roles


    /*------------------------------Assigned Roles--------------------------------------*/

    //Ensure that at least one assigned role is selected
    $(document).on("click", "#revoke", function () {
        var count = $('#divassignedcheckbox input[type="checkbox"]:checked').length;
        if (count < 1) {
            alert("No role selected. Select at least one assigned role.");
            return false;
        }
    });//on click assign

    //check and uncheck all roles when the Check All is clicked
    $("#allassignedroles").click(function () {
        var i = 0;
        if ($("#allassignedroles").is(':checked')) {
            $('#divassignedcheckbox input[type=checkbox]').each(function () {
                $(this).prop('checked', true);
            });
        }
        else {
            $('#divassignedcheckbox input[type=checkbox]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });//click on allavailableroles


    //check and uncheck the allavailableroles based on roles checked
    $(document).on("click", "#assignedroles", function () {

        var allcount = $('#divassignedcheckbox input[type="checkbox"]').length;
        var checkedcount = $('#divassignedcheckbox input[type="checkbox"]:checked').length;

        if (checkedcount == allcount) {
            $("#allassignedroles").prop("checked", true);
        }
        else {
            $("#allassignedroles").prop("checked", false);
        }
    });//click on available roles

});//document ready
