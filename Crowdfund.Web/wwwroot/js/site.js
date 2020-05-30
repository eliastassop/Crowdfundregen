// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



let successAlert = $('.js-success-alert');
successAlert.hide();

let failedAlert = $('.js-fail-alert');
failedAlert.hide();

//let loginFirstAlert = $('.js-login-first-alert');
//loginFirstAlert.hide();



// **Personal Info javascript code**
let profilepage = $('.profile page');
let UpdateInfoButton = $('.js-upinfo');

UpdateInfoButton.on('click', () => {
    window.location.href = "/home/login";
});


let PersonalInfoButton = $('.js-user-pi');
PersonalInfoButton.on('click', () => {    
    let userid = localStorage.getItem('userId');

    if (userid != null) {
        window.location.href = "user/" + userid + "/userpersonalinfo";
    }
    else {
        window.location.href = "/home/login";         
    }
});

// **Create Project javascript code**
let projectbutton = $('.js-project-create-button');
projectbutton.on('click', () => {



    let successAlert = $('.js-success-alert');
    successAlert.hide();

    let failedAlert = $('.js-fail-alert');
    failedAlert.hide();

    let id = localStorage.getItem('userId');
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

        successAlert.html(`Project created `);

        successAlert.show();

    }).fail(failureResponse => {
        failedAlert.show();

    });    



});


//$('form#profile-form button.js-submit-profile-btn') ***** !!
    

