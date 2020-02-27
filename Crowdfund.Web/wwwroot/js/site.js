
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
    $rewardDescr.val('');
});

$('.js-submit-project').on('click', () => {

    $('.js-submit-project').attr('disabled', true);
    let title = $('.js-title').val();
    let Description = $('.js-description').val();
    let goal = parseFloat($('.js-goal').val());
    let category = $('.js-category').val();
    let photo = $('.js-photo').val();

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
    let value = parseFloat($('.js-value').val());
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
        type: 'POST',
        data: {
            title: title
        }

    }).done((projects) => {
        let $projectsList = $('.cards');
        $projectsList.html('');
        $('.card').hide();
        debugger;
        projects.forEach(p => {
            var progress = p.Goal / p.Contributions;
            let card = `p" src="#" alt="Project Image">
                <div class="card-body">
                    <h5 class="card-title">${p.title}</h5>
                    <p class="card-text">${p.category}</p>
                    <p class="card-text">${p.Description}</p>
                    <p class="card-text">${progress}</p>
                          </div>
                        </div>
                    </label>                
                </div>
            </div>`;
            $projectsList.append(card);
            $projectsList.show();
        });
    }).fail((xhr) => {

        alert(xhr.responseText);
    });
});

//----------------searchCategory------------------

$.ajax({
    url: '/Home/SearchProjectsCategory',
    type: 'POST',
    contentType: 'application/json'

}).done(function (projects) {
    $('js-view-project-list').html('');
    projects.forEach(p => {
        var progress = p.Goal / p.Contributions;
        debugger;
        let row =
            `<tr>
               <td>${p.category}</td>             
               <td>${p.title}</td>
               <td>${p.goal}</td>     
            </tr>`;
        $('.js-project-list').append(row);
    });
}).fail(function (xhr) {

});


//----------------Buy Reward------------------
$('.js-btn-buy').on('click', function () {

    let rewardid = parseInt($('.js-reward-id').text());
    let projectid = parseInt($('.js-project-id').text());
    let data = JSON.stringify({
        rewardid: rewardid,
        projectid: projectid
    });

    $.ajax({
        url: '/Project/BuyProject',
        type: 'POST',
        contentType: 'application/json',
        data: data

    }).done(function (reward) {
        let $alertArea = $('js-buy-reward-success');
        $alertArea.html(`Successfully Buy the reward with name ${reward.title}`);
        $alertArea.show();

    }).fail(function (xhr) {
        debugger;
    });
});


