
// **Add media Javascript code**



let mediaCreateForm = $('.js-add-media-form');
mediaCreateForm.submit(function (event) {
    event.preventDefault();

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
        mediaCreateSuccessAlert.show();
        $('.js-add-media-form').trigger("reset");
        $('.js-media-category').prop('selectedIndex', 0);

    }).fail(errorCode => {
        mediaCreateFailedAlert.html(`${errorCode.responseText}`);
        mediaCreateFailedAlert.show();

    });
});

$('.js-add-media-form').keypress(function (e) {
    if (e.which == 13) {
        mediaCreateButton.click();
        return false;    //<---- Add this line
    }
});

//let gotoProjectButton = $('.js-goto-project-button');
//gotoProjectButton.on('click', () => {
//    //debugger;
//    //window.location.href = "/project/" + projectId + "/viewproject";
//}