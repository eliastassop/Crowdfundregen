﻿let success = $('.js-updateproject-success').hide();
let fail = $('.js-updateproject-success').hide();
let UpdateProjectButton = $('.js-update-project');


UpdateProjectButton.on('click', () => {

    let projectId = $('.js-up-projectid').val();

    let Title = $('.js-update-title');
    let Description = $('.js-update-description');
    let TotalFund = $('.js-update-totalfund');
    let Deadline = $('.js-update-deadline');
    let Category = $('.js-update-category');             
   

    let data = {
        Title: Title.val(),
        Description: Description.val(),
        TotalFund: parseFloat(TotalFund.val()),
        Deadline: Deadline.val(),
        Category: Category.val()
    };

    $.ajax({
        type: 'POST',
        url: "/project/" +projectId +"/updateproject",
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(successResponse => {
        window.location.reload();
        success.show().delay(1500);
        success.fadeOut();

    }).fail(failureResponse => {
        fail.show().delay(1500);
        fail.fadeOut();
        

    });


});