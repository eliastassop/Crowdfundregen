let UpdateProfileButton = $('.js-update-profile-btn');
let updatesuccess = $('.js-update-success');
let updatefailed = $('.js-update-failed');

UpdateProfileButton.on('click', () => {

    let userid = window.localStorage.getItem('userId');
    
    let Email = $('.js-update-email');
    let UserName = $('.js-update-username');
    //debugger;

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
        updatesuccess.show().fadeOut(1200);
        setTimeout(() => { window.location.reload(); },1000)
        //window.location.reload();

    }).fail(failureResponse => {
        updatefailed.html(`${failureResponse.responseText}`);
        updatefailed.show().delay(1500);
        updatefailed.fadeOut();

    });

});