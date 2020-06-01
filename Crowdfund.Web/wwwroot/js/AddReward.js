
// **Add Reward Javascript code**
let rewardCreateSuccessAlert = $('.js-reward-create-success-alert');
let rewardCreateFailedAlert = $('.js-reward-create-fail-alert');
let Title = $('.js-reward-title');
let Description = $('.js-reward-description');
let Price = $('.js-reward-price');
let ProjectId = $('.js-reward-projectId');


let rewardCreateButton = $('.js-create-reward-button');
rewardCreateButton.on('click', () => {
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
        
        rewardCreateSuccessAlert.html(`Reward ${reward.title} created succesfully`);
        rewardCreateSuccessAlert.show();
        $('.js-add-reward-form').trigger("reset");

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