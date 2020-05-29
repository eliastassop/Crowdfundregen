// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    debugger;

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

