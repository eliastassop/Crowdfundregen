let userCreateSuccessAlert = $('.js-user-create-success-alert');

let userCreateFailedAlert = $('.js-user-create-fail-alert');

let createUserForm = $('.js-create-account-form');

createUserForm.submit(function (event) {
    event.preventDefault();
    
    let username = $('.js-create-userName');
    let email = $('.js-create-email');

    let data = {
        Email: email.val(),
        UserName: username.val(),     
    };

    $.ajax({
        type: 'POST',
        url: '/user/create',
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(user => {

        let id = user.userId;        
        localStorage.setItem('userId', id);
        localStorage.setItem('username', user.userName);
        window.location.href = "/user/"+id+"/userpersonalinfo";

    }).fail(failureResponse => {
        userCreateFailedAlert.html(`${failureResponse.responseText}`)
        userCreateFailedAlert.show().fadeOut(1500);
    });

});
