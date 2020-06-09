let UpdateProfileForm = $('.js-update-user-form');
let updatesuccess = $('.js-update-success');
let updatefailed = $('.js-update-failed');

UpdateProfileForm.submit(function (event) {
    event.preventDefault();
    if (!UpdateProfileForm.valid()) { return; }

    let userid = window.localStorage.getItem('userId');    
    let Email = $('.js-update-email');
    let UserName = $('.js-update-username');


    let data = {
        UserName: UserName.val(),
        Email : Email.val()
    };

    $.ajax({
        type: 'POST',
        url: "/user/" + userid + "/updateuserpersonalinfo",
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(successResponse => {
        localStorage.setItem('username', UserName.val());
        updatesuccess.show().fadeOut(1200);
        setTimeout(() => { window.location.reload(); },1000)
        //window.location.reload();

    }).fail(failureResponse => {
        updatefailed.html(`${failureResponse.responseText}`);
        updatefailed.show();
        updatefailed.fadeOut(1500);

    });

});