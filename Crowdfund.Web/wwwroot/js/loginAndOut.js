
let loginForm = $('.js-login-form');
loginForm.submit(function (event) {
    event.preventDefault();  
    let loginFailedAlert = $('.js-login-fail-alert');
    let username = $('.js-userName').val();
    
    //let id =
        $.ajax({
            type: 'POST',
            url: '/user/idfromuser',
            contentType: 'application/json',
            data: JSON.stringify(username)
        }).done(function (data) {
            window.localStorage.setItem(`username`, username);
            window.localStorage.setItem(`userId`, data);
            window.location.href = "/home/index";

        }).fail(failureResponse => {
            loginFailedAlert.html(`${failureResponse.responseText}`);
            loginFailedAlert.show().fadeOut(1500);
            
        });
});
