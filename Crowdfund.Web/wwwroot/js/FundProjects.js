
// Button to fund a project 
let pledgebutton = $('.js-pledge-button');


pledgebutton.click( function() {
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
        alert("Reward bought")

    }).fail(errorCode => {
        alert("den mphkan ta xrhmata")

    });
});