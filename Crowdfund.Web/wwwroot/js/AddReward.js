﻿
// **Add Reward Javascript code**



let rewardCreateButton = $('.js-create-reward-button');
rewardCreateButton.on('click', () => {

    let rewardCreateSuccessAlert = $('.js-reward-create-success-alert');
    let rewardCreateFailedAlert = $('.js-reward-create-fail-alert');
    let Title = $('.js-reward-title');
    let Description = $('.js-reward-description');
    let Price = $('.js-reward-price');
    let ProjectId = $('.js-reward-projectId');
    //debugger;
    rewardCreateSuccessAlert.hide();    
    rewardCreateFailedAlert.hide();  

    let data = {
        ProjectId:parseInt(ProjectId.val()),
        Title: Title.val(),
        Description: Description.val(),
        Price: parseFloat(Price.val()),        
    };

    $.ajax({
        type: 'POST',
        url: '/reward/create',
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(reward => {
        //debugger;
        //let projectId = project.projectId;
        $('.js-add-reward-form').trigger("reset");
        rewardCreateSuccessAlert.html(`Reward ${reward.title} created succesfully`);
        rewardCreateSuccessAlert.show();
        success.fadeOut(1500);

    }).fail(errorCode => {
        rewardCreateFailedAlert.html(`Adding reward failed due to error: ${errorCode.statusText}`);
        rewardCreateFailedAlert.show();

    });
});

$('.js-add-reward-form').keypress(function (e) {
    if (e.which == 13) {
        rewardCreateButton.click();
        return false;    //<---- Add this line
    }
});

//let gotoProjectButton = $('.js-goto-project-button');
//gotoProjectButton.on('click', () => {
//    //debugger;
//    //window.location.href = "/project/" + projectId + "/viewproject";
//}