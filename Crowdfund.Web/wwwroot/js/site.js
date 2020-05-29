// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
   // debugger;

    let userId = localStorage.getItem('userId');

    if (userId) {
        $('.user-login').hide();
        $('.user-logout').show();
    } else {
        $('.user-login').show();
        $('.user-logout').hide();
    }
});

let successAlert = $('.js-success-alert');
successAlert.hide();

let failedAlert = $('.js-fail-alert');
failedAlert.hide();


let button = $('.js-project-create-button');
button.on('click', () => {


    let successAlert = $('.js-success-alert');
    successAlert.hide();

    let failedAlert = $('.js-fail-alert');
    failedAlert.hide();

    let id = localStorage.getItem('userId');  
    debugger;
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

        window.location.href = "index";
        //let log = $('.nav - link text - dark');
        //log.val() = "True";
    }).fail(failureResponse => {
        failedAlert.show();

    });



});

    

