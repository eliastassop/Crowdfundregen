
// **Add Reward Javascript code**



let rewardCreateForm = $('.js-add-reward-form');
rewardCreateForm.submit(function (event) {
    event.preventDefault();
    if (!rewardCreateForm.valid()) { return; }

    let rewardCreateSuccessAlert = $('.js-reward-create-success-alert');
    let rewardCreateFailedAlert = $('.js-reward-create-fail-alert');
    let Title = $('.js-reward-title');
    let Description = $('.js-reward-description');
    let Price = $('.js-reward-price');
    let ProjectId = $('.js-reward-projectId');

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
        $('.js-add-reward-form').trigger("reset");
        rewardCreateSuccessAlert.html(`Reward ${reward.title} created succesfully`);
        rewardCreateSuccessAlert.show().fadeOut(1500);

    }).fail(errorCode => {
        rewardCreateFailedAlert.html(`Adding reward failed due to error: ${errorCode.responseText}`);
        rewardCreateFailedAlert.show().fadeOut(1500);

    });
});

jQuery.validator.addMethod('numberpositive', function (value, element) {


    if (value > 0) {
        return true;
    }

    return false;
});
jQuery.validator.unobtrusive.adapters.addBool('numberpositive');
