$(function () {
    // debugger;

    let userId = window.localStorage.getItem('userId');
    let username = window.localStorage.getItem('username');

    if (userId) {        
        $('.user-login').hide();      
        document.querySelector('.js-user-toggle').textContent   = 'Hi '+ username;
        $('.js-user-navbar').show();
        //$('.js-user-logout').show();
    } else {
        $('.user-login').show();
        $('.js-user-navbar').hide();
        //$('.js-user-logout').hide();
    }
});

let logoutbutton = $('.js-user-logout');

logoutbutton.on('click', () => {

    window.localStorage.removeItem('userId');
    window.localStorage.removeItem('username');
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
