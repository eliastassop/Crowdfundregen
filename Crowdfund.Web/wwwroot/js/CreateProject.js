$(function () {
    // debugger;

    let userId = window.localStorage.getItem('userId');
    let username = window.localStorage.getItem('username');

    if (userId) {
        $('.js-must-login-first').hide();        
        $('.js-create-project-form').show();
        
    } else {
        $('.js-must-login-first').show();
        $('.js-create-project-form').hide();
        
    }
});


// **Create Project javascript code**
let projectForm = $('.js-create-project-form');
projectForm.submit(function (event) {
    event.preventDefault();
    if (!projectForm.valid()) { return; }

    let projectCreateSuccessAlert = $('.js-project-create-success-alert');
    projectCreateSuccessAlert.hide();

    let projectCreateFailedAlert = $('.js-project-create-fail-alert');
    projectCreateFailedAlert.hide();

    let id = window.localStorage.getItem('userId');    
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
        projectCreateSuccessAlert.html(`Project ${project.title} created succesfully`);

        projectCreateSuccessAlert.show().fadeOut(1500);
        window.location.href = "/project/"+projectId+"/addreward";
    }).fail(errorCode => {
        projectCreateFailedAlert.html(`Project failed due to error: ${errorCode.responseText}`);
        projectCreateFailedAlert.show().fadeOut(1500);

    });
});

jQuery.validator.addMethod('datevalid', function (value, element) {
    let datenow = new Date();
    let test = new Date(value);
    if (test > datenow) {


        return true;
    }

    return false;
});
jQuery.validator.unobtrusive.adapters.addBool('datevalid');

jQuery.validator.addMethod('numberpositive', function (value, element) {
    
   
    if (value > 0) {
        return true;
    }

    return false;
});
jQuery.validator.unobtrusive.adapters.addBool('numberpositive');

