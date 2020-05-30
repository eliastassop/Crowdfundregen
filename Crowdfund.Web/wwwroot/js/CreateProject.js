let projectCreateSuccessAlert = $('.js-project-create-success-alert');

let projectCreateFailedAlert = $('.js-project-create-fail-alert');

// **Create Project javascript code**
let projectbutton = $('.js-project-create-button');
projectbutton.on('click', () => {





    let id = window.localStorage.getItem('userId');
    //debugger;
    let Title = $('.js-title');
    let Description = $('.js-description');
    let TotalFund = $('.js-totalFund');
    let Deadline = $('.js-deadline');
    let Category = $('.js-category');
    let Media = $('.js-media-input');


    let data = {
        Title: Title.val(),
        Description: Description.val(),
        TotalFund: parseFloat(TotalFund.val()),
        Deadline: Deadline.val(),
        CreatorId: parseInt(id),
        Category: Category.val(),
        Media: Media.val()
    };

    $.ajax({
        type: 'POST',
        url: '/project/create',
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(successResponse => {

        projectCreateSuccessAlert.html(`Project created `);

        projectCreateSuccessAlert.show();

    }).fail(failureResponse => {
        projectCreateFailedAlert.show();

    });



});