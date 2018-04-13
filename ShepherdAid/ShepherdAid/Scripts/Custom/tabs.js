
$(document).ready(function () {

    //alert("must work");
    
    //----------remove set active tabs-----------------------------
    $('.nav-tabs > li > a').click(function (event) {

        
        //get displaying tab content jQuery selector
        var active_tab_selector = $('.nav-tabs > li.active > a').attr('href');

        //hide displaying tab content
        $(active_tab_selector).removeClass('active');
        $(active_tab_selector).addClass('hide');

        //find actived navigation and remove 'active' css
        var actived_nav = $('.nav-tabs > li.active');
        actived_nav.removeClass('active');

        //add 'active' css into clicked navigation
        $(this).parents('li').addClass('active');

        var target_tab_selector = $(this).attr('href');
        $(target_tab_selector).removeClass('hide');
        $(target_tab_selector).addClass('active');

        //alert("Great");
    });

    //----------end remove set active tabs-----------------------------

    
    $('#member-li').click(function () {
        
        $.ajax({
            type: 'GET',
            url: '/OrganizationMembers/Index',
            contentType: 'application/html; charset-utf-8',
            dataType: 'html'}).
            success(function (result) {
                $('#divmembers').html(result);
            })
        .error(function (ehr, status){
            alert(status);
        })

    });


    $('#leader-li').click(function () {

        $.ajax({
            type: 'GET',
            url: '/OrganizationLeaders/Index',
            contentType: 'application/html; charset-utf-8',
            dataType: 'html'
        }).
            success(function (result) {
                $('#divleaders').html(result);
            })
        .error(function (ehr, status) {
            alert(status);
        })

    });


    $('#sacrament-li').click(function () {

        $.ajax({
            type: 'GET',
            url: '/MemberSacraments/Index',
            contentType: 'application/html; charset-utf-8',
            dataType: 'html'
        }).
            success(function (result) {
                $('#divsacraments').html(result);
            })
        .error(function (ehr, status) {
            alert(status);
        })

    });

    $('#obligation-li').click(function () {

        $.ajax({
            type: 'GET',
            url: '/MemberObligations/Index',
            contentType: 'application/html; charset-utf-8',
            dataType: 'html'
        }).
            success(function (result) {
                $('#divobligations').html(result);
            })
        .error(function (ehr, status) {
            alert(status);
        })

    });

    //$('#obligation-li').click(function () {
        
    //    $.ajax({
    //        type: 'GET',
    //        url: '/MemberSacraments/Index',
    //        //url: '/MemberObligations/Index',
    //        contentType: 'application/html; charset-utf-8',
    //        dataType: 'html'
    //    }).
    //        success(function (result) {
    //            $('#divobligations').html(result);
    //        })
    //    .error(function (ehr, status) {
    //        alert(status);
    //    })

    //});


    //--------------scripts for member sacrament only -----------------------//


    $('#InstitutionGroupID').change(function () {
        //alert("changed.");
        $("#InstitutionID").empty();
        $("#InstitutionID").append("<option value='0'>--select--</option>");
        $.get("/AspNetGroups/GetInstitutions", { id: $("#InstitutionGroupID").val() }, function (data) {
            $.each(data, function (index, row) {
                $("#InstitutionID").append("<option value='" + row.ID + "'>" + row.InstitutionName + "</option>")
            });
        });

    });

    //--------------end scripts for member sacrament only -----------------------//


    //-----------------------scripts for payment tab-----------------------------//
    $('#member-payment-li').click(function () {
        //alert("payment");
        $.ajax({
            type: 'GET',
            url: '/Payments/MemberPartial',
            contentType: 'application/html; charset-utf-8',
            dataType: 'html'
        }).
            success(function (result) {
                $('#div-member-payment').html(result);
            })
        .error(function (ehr, status) {
            alert(status);
        })

    });

    //------------------end script of payment tab--------------------//

})
