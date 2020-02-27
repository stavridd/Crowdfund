
///-------------------------CREATE OWNER--------------------------
$('.js-submit-owner').on('click', () => {

    $('.js-submit-owner').attr('disabled', true);

    let firstname = $('.js-firstname').val();
    let lastname = $('.js-lastname').val();
    let email = $('.js-email').val();
    let age = parseInt($('.js-age').val());

    let data = JSON.stringify({

        

        firstname: firstname,
        lastname: lastname,
        email: email,
        age: age
    });

    $.ajax({
        url: '/Owner/CreateOwner',
        type: 'POST',
        contentType: 'application/json',
        data: data
    }).done((owner) => {
        $('.alert').hide();

        let $alertArea = $('.js-create-owner-success');
        $alertArea.html(`Successfully added owner with name ${owner.firstname}`);
        $alertArea.show();
        

        $('form.js-create-owner').hide();
    }).fail((xhr) => {
        $('.alert').hide();

        let $alertArea = $('.js-create-owner-alert');
        $alertArea.html(xhr.responseText);
        $alertArea.fadeIn();

        setTimeout(function () {
            $('.js-submit-owner').attr('disabled', false);
        }, 300);
    });
}); 

//-------------------------------CREATE BUYER-----------------------

$('.js-submit-buyer').on('click', () => {

    $('.js-submit-buyer').attr('disabled', true);

    let firstname = $('.js-firstname').val();
    let lastname = $('.js-lastname').val();
    let email = $('.js-email').val();
    let age = parseInt($('.js-age').val());
    

    let data = JSON.stringify({

        firstname: firstname,
        lastname: lastname,
        email: email,
        age: age
    });

    $.ajax({
        url: '/Buyer/CreateBuyer',
        type: 'POST',
        contentType: 'application/json',
        data: data
    }).done((buyer) => {
        $('.alert').hide();

        let $alertArea = $('.js-create-buyer-success');
        $alertArea.html(`Successfully added customer with id ${buyer.id}`);
        $alertArea.show();

        $('form.js-create-buyer').hide();
    }).fail((xhr) => {
        $('.alert').hide();

        let $alertArea = $('.js-create-owner-alert');
        $alertArea.html(xhr.responseText);
        $alertArea.fadeIn();

        setTimeout(function () {
            $('.js-submit-buyer').attr('disabled', false);
        }, 300);
    });
}); 

//------------------------CREATE PROJECT------------------------

let rewards = [];


$('.submit-reward').on('click', () => {
    let $rewardTitle = $('.js-reward-title');
    let $rewardValue = $('.js-reward-value');
    let $rewardDescr = $('.js-reward-descrirpion');

    let rewardTitle = $rewardTitle.val();
    let rewardValue = parseFloat($rewardValue.val());
    let rewardDescr = $rewardDescr.val();

    if (rewardTitle.length === 0 || rewardValue.length === 0) {
        return;
    }
  
    rewards.push({
        title: rewardTitle,
        value: rewardValue,
        description: rewardDescr
    });

    $rewardTitle.val('');
    $rewardValue.val('');
    $rewardDescr.val ('');
});


$('.js-submit-project').on('click', () => {

    $('.js-submit-project').attr('disabled', true);

    let title = $('.js-title').val();
    let Description = $('.js-description').val();
    // let category = ($('.js-category').val());
    let goal = parseFloat($('.js-goal').val());
    let category = $('.js-category').val();
    let photo = $('.js-photo').val();
    debugger;

    let data = JSON.stringify({
        CreateOptions: {
            Title: title,
            Description: Description,
            projectcategory: parseInt(category),
            Goal: goal,
            Multis: photo,
        },
        reward: rewards
    });


 
    $.ajax({
        url: '/Project/CreateProject',
        type: 'POST',
        contentType: 'application/json',
        data: data
    }).done((project) => {
        $('.alert').hide();
        debugger;
        let $alertArea = $('.js-create-project-success');
        $alertArea.html(`Successfully added project with id: ${project.id}  `);
        $alertArea.show();

        $('form.js-create-project').hide();
    }).fail((xhr) => {
        $('.alert').hide();

        let $alertArea = $('.js-create-project-alert');
        $alertArea.html(xhr.responseText);
        $alertArea.fadeIn();

        setTimeout(function () {
            $('.js-submit-project').attr('disabled', false);
        }, 300);
    });
}); 

//----------------------Create reward------------------

$('.js-submit-reward').on('click', () => {

$('.js-submit-reward').attr('disabled', true);

let title = $('.js-title').val();
let description = $('.js-description').val();
    let value = parseFloat( $('.js-value').val());
    let projecttitle = $('.js-ptitle').val();

let data = JSON.stringify({



    title: title,
    description: description,
    value: value,
    projecttitle: projecttitle
   
});

$.ajax({
    url: '/Reward/CreateReward',
    type: 'POST',
    contentType: 'application/json',
    data: data
}).done((reward) => {
    $('.alert').hide();

    let $alertArea = $('.js-create-reward-success');
    $alertArea.html(`Successfully added reward with name ${reward.title}`);
    $alertArea.show();


    $('form.js-create-reward').hide();
}).fail((xhr) => {
    $('.alert').hide();

    let $alertArea = $('.js-create-reward-alert');
    $alertArea.html(xhr.responseText);
    $alertArea.fadeIn();

    setTimeout(function () {
        $('.js-submit-reward').attr('disabled', false);
    }, 300);
});
}); 


///-------------Search----------
$('.js-btn-search').on('click', () => {
    let title = $('.js-project-title').val();
    $.ajax({
        url: '/Home/SearchProjects',
        type: 'GET',
        data: {
            title: title
        }
    }).done((projects) => {
        let $projectsList = $('.js-project-list');
        $projectsList.html('');
        $('.card').hide();
        projects.forEach(element => {
            let listItem =
                //`<tr>
                //    <td>${element.Title}</td>
                //    <td>${element.Description}</td>
                //</tr>`;
                //`
                `
        <img src="#" class="card-img-top" alt="No photo available">
        <div class="card-body js-project-list">
                <h5 class="card-title">${element.Title}</h5>
                <p class="card-text">${element.Description}</p>
                <a href="#" align="center" class="btn btn-primary">Details</a>
    </div>
                        `;

            $projectsList.append(listItem);
            $cards = $('.card-results');
            $cards.show();
        });
    }).fail((xhr) => {
        alert(xhr.responseText);
    });
});

