let input = document.getElementById("myInput");


// **Create Project javascript code**
let projectbutton = $('.js-project-create-button');

projectbutton.on('click', () => {
    let projectCreateSuccessAlert = $('.js-project-create-success-alert');
    projectCreateSuccessAlert.hide();

    let projectCreateFailedAlert = $('.js-project-create-fail-alert');
    projectCreateFailedAlert.hide();

    let id = window.localStorage.getItem('userId');
    //debugger;
    let Title = $('.js-title');
    let Description = $('.js-description');
    let TotalFund = $('.js-totalFund');
    let Deadline = $('.js-deadline');
    let Category = $('.js-category');
    let MediaLink = $('.js-media-input');


    let data = {
        Title: Title.val(),
        Description: Description.val(),
        TotalFund: parseFloat(TotalFund.val()),
        Deadline: Deadline.val(),
        CreatorId: parseInt(id),
        Category: Category.val(),
        MediaLink: MediaLink.val()
    };

    $.ajax({
        type: 'POST',
        url: '/project/create',
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(project => {
        let projectId = project.projectId;
        //debugger;
        projectCreateSuccessAlert.html(`Project ${project.title} created succesfully`);

        projectCreateSuccessAlert.show();
        window.location.href = "/project/"+projectId+"/addreward";
    }).fail(errorCode => {
        projectCreateFailedAlert.html(`Project failed due to error: ${errorCode.statusText}`);
        projectCreateFailedAlert.show();

    });
});

$('.js-create-project-form').keypress(function (e) {
    if (e.which == 13) {
        projectbutton.click();
        return false;    //<---- Add this line
    }
});