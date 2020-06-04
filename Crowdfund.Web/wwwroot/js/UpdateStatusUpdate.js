
// **Update Status Update Javascript code**



let statusUpdateUpdateButton = $('.js-update-statusUpdate-button');
statusUpdateUpdateButton.click(function() {
    //let test = this.id;
    let statusUpdateUpdateSuccessAlert = $('.js-statusUpdate-update-success-alert');
    let statusUpdateUpdateFailedAlert = $('.js-statusUpdate-update-fail-alert');
    let Title = $('.js-statusUpdate-title');
    let Description = $('.js-statusUpdate-description');
    let StatusUpdateId = this.id;
    //debugger;
    statusUpdateUpdateSuccessAlert.hide();
    statusUpdateUpdateFailedAlert.hide();

    let data = {
        StatusUpdateId: parseInt(StatusUpdateId),
        Title: Title.val(),
        Description: Description.val()
    };

    $.ajax({
        type: 'PATCH',
        url: '/statusUpdate/update',
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(statusUpdate => {
        //debugger;
        //let projectId = project.projectId;

        statusUpdateUpdateSuccessAlert.html(`statusUpdate ${statusUpdate.title} updated succesfully`);
        statusUpdateUpdateSuccessAlert.show();
        $('.js-update-statusUpdate-form').trigger("reset");

    }).fail(error => {
        statusUpdateUpdateFailedAlert.html(`Updating statusUpdate failed due to error: ${error.responseText}`);
        statusUpdateUpdateFailedAlert.show();

    });
});

$('.js-update-statusUpdate-form').keypress(function (e) {
    if (e.which == 13) {
        statusUpdateUpdateButton.click();
        return false;    //<---- Add this line
    }
});