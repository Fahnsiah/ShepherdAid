$(document).ready(function () {

    var tab = localStorage["tab"];
   
    switch(parseInt(tab))
    {
        case 1:
            $('#ul-diocesan-reports').addClass('hidden');
            $('#ul-parish-reports').addClass('hidden');

            $('#ul-national-reports').removeClass('hidden');
            break;

        case 2:
            $('#ul-national-reports').addClass('hidden');
            $('#ul-parish-reports').addClass('hidden');

            $('#ul-diocesan-reports').removeClass('hidden');
            break;

        case 3:
            $('#ul-national-reports').addClass('hidden');
            $('#ul-diocesan-reports').addClass('hidden');

            $('#ul-parish-reports').removeClass('hidden');
            break;
    }


    //alert("testing.");
    $('#div-national-reports').click(function () {
        localStorage["tab"] = 1;
        $('#ul-diocesan-reports').addClass('hidden');
        $('#ul-parish-reports').addClass('hidden');

        $('#ul-national-reports').removeClass('hidden');
    });

    $('#div-diocesan-reports').click(function () {
        localStorage["tab"] = 2;
        $('#ul-national-reports').addClass('hidden');
        $('#ul-parish-reports').addClass('hidden');

        $('#ul-diocesan-reports').removeClass('hidden');
    });

    $('#div-parish-reports').click(function () {
        localStorage["tab"] = 3;
        $('#ul-national-reports').addClass('hidden');
        $('#ul-diocesan-reports').addClass('hidden');

        $('#ul-parish-reports').removeClass('hidden');
    });


    //$('.nav-tabs > li > a').click(function (event) {

    //    //get displaying tab content jQuery selector
    //    var active_tab_selector = $('.nav-tabs > li.active > a').attr('href');

    //    //hide displaying tab content
    //    $(active_tab_selector).removeClass('active');
    //    $(active_tab_selector).addClass('hide');

    //    //find actived navigation and remove 'active' css
    //    var actived_nav = $('.nav-tabs > li.active');
    //    actived_nav.removeClass('active');

    //    //add 'active' css into clicked navigation
    //    $(this).parents('li').addClass('active');

    //    var target_tab_selector = $(this).attr('href');
    //    $(target_tab_selector).removeClass('hide');
    //    $(target_tab_selector).addClass('active');

    //    //alert("Great");
    //});

})
