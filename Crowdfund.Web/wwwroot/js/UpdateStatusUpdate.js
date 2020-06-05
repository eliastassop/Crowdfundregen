
// **Update Status Update Javascript code**



let statusUpdateUpdateForm = $('.js-update-statusUpdate-form');
statusUpdateUpdateForm.submit(function (event) {
    event.preventDefault();
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
    }).done(successResponse => {
        //debugger;
        //let projectId = project.projectId;

        statusUpdateUpdateSuccessAlert.html(`Status updated succesfully`);
        statusUpdateUpdateSuccessAlert.show();    
        statusUpdateUpdateSuccessAlert.fadeOut(1500);

    }).fail(failureResponse => {
        statusUpdateUpdateFailedAlert.html(`${failureResponse.responseText}`);
        statusUpdateUpdateFailedAlert.show();
        statusUpdateUpdateFailedAlert.fadeOut(1500);

    });
});
