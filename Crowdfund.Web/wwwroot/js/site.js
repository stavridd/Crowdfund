//import { Alert } from "../lib/bootstrap/dist/js/bootstrap.bundle";

///-------------------------SEARCH OWNER--------------------------
$('.js-btn-search').on('click', () => {

    let email = $('.js-email').val();
    let firstname = $('.js-firstname').val();
    let lastname = $('.js-lastname').val();

    $.ajax({
        url: '/Owner/SearchOwner',
        type: 'GET',
        data: {
            email: email,
            firstname: firstname,
            lastname: lastname
        }
    }).done((owners) => {
        let ownersList = $('.js-owner-list');
        ownersList.html('');

        owners.forEach(element => {
            let listItem =
                `<tr>
                    <td>${element.firstname}</td>
                    <td>${element.lastname}</td>
                    <td>${element.email}</td>
                </tr>`;

            ownersList.append(listItem);
        });
    }).fail((xhr) => {
        alert(xhr.responseText);
    });
});

///-------------------------CREATE OWNER--------------------------
$('.js-submit-owner').on('click', () => {

    $('.js-submit-owner').attr('disabled', true);

    let firstname = $('.js-firstname').val();
    let lastname = $('.js-lastname').val();
    let email = $('.js-email').val();
    let age = $('.js-age').val();

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

//-------------------------------SEARCH BUYER-------------------------
$('.js-btn-search').on('click', () => {

    let email = $('.js-email').val();
    let firstname = $('.js-firstname').val();
    let lastname = $('.js-lastname').val();

    $.ajax({
        url: '/Buyer/SearchBuyer',
        type: 'GET',
        data: {
            email: email,
            firstname: firstname,
            lastname: lastname
        }
    }).done((buyers) => {
        let buyersList = $('.js-buyer-list');
        buyersList.html('');

        buyers.forEach(element => {
            let listItem =
                `<tr>
                    <td>${element.firstname}</td>
                    <td>${element.lastname}</td>
                    <td>${element.email}</td>
                </tr>`;

            buyersList.append(listItem);
        });
    }).fail((xhr) => {
        alert(xhr.responseText);
    });
});

//-------------------------------CREATE BUYER-----------------------

$('.js-submit-buyer').on('click', () => {

    $('.js-submit-buyer').attr('disabled', true);

    let firstname = $('.js-firstname').val();
    let lastname = $('.js-lastname').val();
    let email = $('.js-email').val();
    let age = $('.js-age').val();

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
            $('.js-submit-owner').attr('disabled', false);
        }, 300);
    });
}); 

//------------------------SEARCH PROJECT----------------------
$('.js-btn-search').on('click', () => {

    let email = $('.js-email').val();
    let firstname = $('.js-firstname').val();
    let lastname = $('.js-lastname').val();

    $.ajax({
        url: '/Buyer/SearchBuyer',
        type: 'GET',
        data: {
            email: email,
            firstname: firstname,
            lastname: lastname
        }
    }).done((buyers) => {
        let buyersList = $('.js-buyer-list');
        buyersList.html('');

        buyers.forEach(element => {
            let listItem =
                `<tr>
                    <td>${element.firstname}</td>
                    <td>${element.lastname}</td>
                    <td>${element.email}</td>
                </tr>`;

            buyersList.append(listItem);
        });
    }).fail((xhr) => {
        alert(xhr.responseText);
    });
});

//------------------------CREATE PROJECT------------------------


$('.js-submit-project').on('click', () => {

    $('.js-submit-project').attr('disabled', true);

    let title = $('.js-title').val();
    let Description = $('.js-description').val();
    let category = $('.js-category').val();
    

    let data = JSON.stringify({

        firstname: firstname,
        lastname: lastname,
        email: email,
        age: age
    });

    $.ajax({
        url: '/Project/CreateProject',
        type: 'POST',
        contentType: 'application/json',
        data: data
    }).done((project) => {
        $('.alert').hide();

        let $alertArea = $('.js-create-project-success');
        $alertArea.html(`Successfully added customer  `);
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