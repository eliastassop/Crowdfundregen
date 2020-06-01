let userCreateSuccessAlert = $('.js-user-create-success-alert');

let userCreateFailedAlert = $('.js-user-create-fail-alert');




let createbutton = $('.js-create-user-button');

createbutton.on('click', () => {

    debugger;
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
        debugger;
        localStorage.setItem('userId', id);
        userCreateSuccessAlert.html(`User ${user.UserName} was created `);
        userCreateSuccessAlert.show();
        window.location.href = "/user/"+id+"/userpersonalinfo";

    }).fail(failureResponse => {
        userCreateFailedAlert.html(`${failureResponse.responseText}`)
        userCreateFailedAlert.show();

    });

});
