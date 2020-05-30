
let loginbutton = $('.js-login-button');
loginbutton.on('click', () => {    
    let loginFailedAlert = $('.js-login-fail-alert');
    let username = $('.js-userName').val();
    
    let id =
        $.ajax({
            type: 'POST',
            url: '/user',
            contentType: 'application/json',
            data: JSON.stringify(username)
        }).done(function (data) {

            window.localStorage.setItem(`userId`, data);
            window.location.href = "/home/index";

        }).fail(failureResponse => {

            loginFailedAlert.show().delay(500);
            loginFailedAlert.fadeOut();
            
        });
});
