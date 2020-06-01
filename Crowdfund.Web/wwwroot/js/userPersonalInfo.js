let UpdateProfileButton = $('.js-update-profile-btn');
let updatesuccess = $('.js-update-success');
let updatefailed = $('.js-update-failed');

UpdateProfileButton.on('click', () => {

    let userid = window.localStorage.getItem('userId');
    
    let Email = $('.js-update-email');
    //debugger;

    let data = {
        Email : Email.val()
    };

    $.ajax({
        type: 'POST',
        url: "/user/" + userid + "/updateuserpersonalinfo",
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(successResponse => {
        updatesuccess.show().delay(1500);
        updatesuccess.fadeOut();

    }).fail(failureResponse => {
        updatefailed.show().delay(1500);
        updatefailed.fadeOut();

    });



   




});