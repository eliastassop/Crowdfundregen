$(function () {
    // debugger;

    let userId = localStorage.getItem('userId');

    if (userId) {
        $('.user-login').hide();
        $('.js-user-logout').show();
    } else {
        $('.user-login').show();
        $('.js-user-logout').hide();
    }
});



let loginbutton = $('.js-login-button');
loginbutton.on('click', () => {

    let successAlert = $('.js-success-alert');
    successAlert.hide();

    let failedAlert = $('.js-fail-alert');
    failedAlert.hide();

    let username = $('.js-userName').val();
    
    let id =
        $.ajax({
            type: 'POST',
            url: '/user',
            contentType: 'application/json',
            data: JSON.stringify(username)
        }).done(function (data) {

            localStorage.setItem(`userId`, data);
            window.location.href = "index";
           // successAlert.html(`User Successfully Logged In`);
           
            successAlert.html(`User Successfully Logged In`)
            successAlert.show()
            delay(1000);
            sucessAlert.fadeOut();
            


           
            //let log = $('.nav - link text - dark');
            //log.val() = "True";
        }).fail(failureResponse => {
            failedAlert.show();
            
        });
});

let logoutbutton = $('.js-user-logout');

logoutbutton.on('click', () => {
 
    localStorage.removeItem('userId');
    
 
    window.location.href = "/Home/index";
    

});







