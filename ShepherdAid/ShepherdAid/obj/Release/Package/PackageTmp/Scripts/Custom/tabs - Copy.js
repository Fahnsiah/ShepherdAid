
$(document).ready(function () {

    alert("testing.");


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

        alert("Great");
    });
})
