
// **Add media Javascript code**



let mediaCreateForm = $('.js-add-media-form');
mediaCreateForm.submit(function (event) {
    event.preventDefault();
    if (!mediaCreateForm.valid()) { return; }

    let mediaCreateSuccessAlert = $('.js-media-create-success-alert');
    let mediaCreateFailedAlert = $('.js-media-create-fail-alert');
    let MediaLink = $('.js-media-url');
    let Category = $('.js-media-category');
    let ProjectId = $('.js-media-projectId');
    //debugger;
    mediaCreateSuccessAlert.hide();
    mediaCreateFailedAlert.hide();    
    if (Category.val() == "Video") {
        MediaLink = MediaLink.val().replace("watch?v=", "embed/");
    }
    else {
        MediaLink = MediaLink.val();
    }
    let data = {
        ProjectId: parseInt(ProjectId.val()),        
        MediaLink: MediaLink,      
        Category: Category.val(),
    };

    $.ajax({
        type: 'POST',
        url: '/media/create',
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(media => {
        //debugger;
        //let projectId = project.projectId;

        mediaCreateSuccessAlert.html(`Media created succesfully`);
        mediaCreateSuccessAlert.show().fadeOut(1500);;
        $('.js-add-media-form').trigger("reset");
        $('.js-media-category').prop('selectedIndex', 0);

    }).fail(errorCode => {
        mediaCreateFailedAlert.html(`${errorCode.responseText}`);
        mediaCreateFailedAlert.show().fadeOut(1500);

    });
});

