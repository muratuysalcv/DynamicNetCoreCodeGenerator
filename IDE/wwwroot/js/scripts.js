/*!
* Start Bootstrap - Simple Sidebar v6.0.6 (https://startbootstrap.com/template/simple-sidebar)
* Copyright 2013-2023 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-simple-sidebar/blob/master/LICENSE)
*/
// 
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});
function openWindowFullSize(url, title = '') {
    var width = parseInt(window.outerWidth * 0.9);
    var height = parseInt(window.outerHeight * 0.75);
    new $.Zebra_Dialog({
        type: false,                            // no icon
        custom_class: "ZebraDialog_iFrame",     // a custom class to remove default paddings
        source: {
            // iFrame's width and height are automatically set
            // to fit the dialog box's width and height
            iframe: {
                src: url
            }
        },
        buttons: [],
        //title: title,
        width: width,
        height: height     // unless explicitly specified, the height of the dialog box will
        // be determined by the value of the "max_height" property
    });
}
