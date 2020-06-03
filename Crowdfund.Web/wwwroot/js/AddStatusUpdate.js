﻿
// **Add Status Update Javascript code**



let statusUpdateCreateButton = $('.js-create-statusUpdate-button');
statusUpdateCreateButton.on('click', () => {

    let statusUpdateCreateSuccessAlert = $('.js-statusUpdate-create-success-alert');
    let statusUpdateCreateFailedAlert = $('.js-statusUpdate-create-fail-alert');
    let Title = $('.js-statusUpdate-title');
    let Description = $('.js-statusUpdate-description');
    let ProjectId = $('.js-statusUpdate-projectId');
    //debugger;
    statusUpdateCreateSuccessAlert.hide();
    statusUpdateCreateFailedAlert.hide();

    let data = {
        ProjectId: parseInt(ProjectId.val()),
        Title: Title.val(),
        Description: Description.val()
    };

    $.ajax({
        type: 'POST',
        url: '/statusUpdate/create',
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(statusUpdate => {
        //debugger;
        //let projectId = project.projectId;

        statusUpdateCreateSuccessAlert.html(`statusUpdate ${statusUpdate.title} created succesfully`);
        statusUpdateCreateSuccessAlert.show();
        $('.js-add-statusUpdate-form').trigger("reset");

    }).fail(errorCode => {
        statusUpdateCreateFailedAlert.html(`Adding statusUpdate failed due to error: ${errorCode.statusText}`);
        statusUpdateCreateFailedAlert.show();

    });
});

$('.js-add-statusUpdate-form').keypress(function (e) {
    if (e.which == 13) {
        statusUpdateCreateButton.click();
        return false;    //<---- Add this line
    }
});