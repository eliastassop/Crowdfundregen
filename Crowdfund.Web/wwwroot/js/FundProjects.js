
// Button to fund a project 
let pledgeForm = $('.js-buy-reward-form');

pledgeForm.submit(function (event) {
    event.preventDefault();
    if (!pledgeForm.valid()) { return; }

    let buyRewardFailedAlert = $('.js-buy-reward-failed');
    let test = this.id;                                    // εδω με τη βοηθεια του this.id περνουμε περνουμε το σωστο @rewardid
    let id = window.localStorage.getItem('userId');        
    let RewardId = $('.js-rewardid.'+test);              
    let Quantity = $('.js-quantity.'+test);            
    let UserId = id;
    
    let data = {
        UserId: parseInt(UserId),
        RewardId: parseInt(RewardId.val()),
        Quantity: parseInt(Quantity.val())
    };

    $.ajax({
        type: 'POST',
        url: '/rewarduser/createOrupdateRewardUser',
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(rewarduser => {
        window.location.reload();     
    }).fail(errorCode => {
        buyRewardFailedAlert.html(`${errorCode.responseText}`);
        buyRewardFailedAlert.show().fadeOut(1500);

    });
});