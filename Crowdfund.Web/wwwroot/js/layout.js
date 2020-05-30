$(function () {
    // debugger;

    let userId = window.localStorage.getItem('userId');

    if (userId) {        
        $('.user-login').hide();
        $('.js-user-pi').show();
        $('.js-user-logout').show();
    } else {
        $('.user-login').show();
        $('.js-user-pi').hide();
        $('.js-user-logout').hide();
    }
});

let logoutbutton = $('.js-user-logout');

logoutbutton.on('click', () => {

    window.localStorage.removeItem('userId');

    window.location.href = "/Home/index";

});

let PersonalInfoButton = $('.js-user-pi');
PersonalInfoButton.on('click', () => {
    //debugger;
    let userid = window.localStorage.getItem('userId');

    if (userid != null) {
        window.location.href = "/user/" + userid + "/userpersonalinfo";
    }
    else {
        window.location.href = "/home/login";
    }
});