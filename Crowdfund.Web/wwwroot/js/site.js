////$('.js-btn-search').on('click', () => {
////    let email = $('.js-email').val();
////    let vatnumber = $('.js-vatnumber').val();

////    $.ajax({
////        url: '/customer/SearchCustomers',
////        type: 'GET',
////        data: {
////            email: email,
////            vatNumber: vatnumber
////        }
////    }).done((customers) => {
////        let $customersList = $('.js-customer-list');
////        $customersList.html('');

////        customers.forEach(element => {
////            let listItem =
////                `<tr>
////                    <td>${element.vatNumber}</td>
////                    <td>${element.email}</td>
////                </tr>`;

////            $customersList.append(listItem);
////        });
////    }).fail((xhr) => {
////        alert(xhr.responseText);
////    });
////});


//$('.js-submit-owner').on('click', () => {

//    $('.js-submit-owner').attr('disabled', true);

//    let firstname = $('.js-firstname').val();
//    let lastname = $('.js-lastname').val();
//    let email = $('.js-email').val();
//    let age = $('.js-age').val();

//    let data = JSON.stringify({

//        firstname: firstname,
//        lastname: lastname,
//        email: email,
//        age: age
//    });

//    $.ajax({
//        url: '/owner/CreateOwner',
//        type: 'POST',
//        contentType: 'application/json',
//        data: data
//    }).done((owner) => {
//        $('.alert').hide();

//        //let $alertArea = $('.js-create-owner-success');
//        //$alertArea.html(`Successfully added customer with id ${customer.id}`);
//        //$alertArea.show();

//        $('form.js-create-owner').hide();
//    }).fail((xhr) => {
//        $('.alert').hide();

//        //let $alertArea = $('.js-create-owner-alert');
//        //$alertArea.html(xhr.responseText);
//        //$alertArea.fadeIn();

//        //setTimeout(function () {
//        //    $('.js-submit-owner').attr('disabled', false);
//        //}, 300);
//    });
//}); 

//$('.js-submit-buyer').on('click', () => {

//    $('.js-submit-buyer').attr('disabled', true);

//    let firstname = $('.js-firstname').val();
//    let lastname = $('.js-lastname').val();
//    let email = $('.js-email').val();
//    let age = $('.js-age').val();

//    let data = JSON.stringify({

//        firstname: firstname,
//        lastname: lastname,
//        email: email,
//        age: age
//    });

//    $.ajax({
//        url: '/buyer/CreateBuyer',
//        type: 'POST',
//        contentType: 'application/json',
//        data: data
//    }).done((owner) => {
//        $('.alert').hide();

//        //let $alertArea = $('.js-create-owner-success');
//        //$alertArea.html(`Successfully added customer with id ${customer.id}`);
//        //$alertArea.show();

//        $('form.js-create-buyer').hide();
//    }).fail((xhr) => {
//        $('.alert').hide();

//        //let $alertArea = $('.js-create-owner-alert');
//        //$alertArea.html(xhr.responseText);
//        //$alertArea.fadeIn();

//        //setTimeout(function () {
//        //    $('.js-submit-owner').attr('disabled', false);
//        //}, 300);
//    });
//}); 

//$('.js-submit-project').on('click', () => {

//    $('.js-submit-project').attr('disabled', true);

//    let title = $('.js-title').val();
//    let Description = $('.js-description').val();
//    let category = $('.js-category').val();
    

//    let data = JSON.stringify({

//        firstname: firstname,
//        lastname: lastname,
//        email: email,
//        age: age
//    });

//    $.ajax({
//        url: '/Project/CreateProject',
//        type: 'POST',
//        contentType: 'application/json',
//        data: data
//    }).done((owner) => {
//        $('.alert').hide();

//        //let $alertArea = $('.js-create-owner-success');
//        //$alertArea.html(`Successfully added customer with id ${customer.id}`);
//        //$alertArea.show();

//        $('form.js-create-project').hide();
//    }).fail((xhr) => {
//        $('.alert').hide();

//        //let $alertArea = $('.js-create-owner-alert');
//        //$alertArea.html(xhr.responseText);
//        //$alertArea.fadeIn();

//        //setTimeout(function () {
//        //    $('.js-submit-owner').attr('disabled', false);
//        //}, 300);
//    });
//}); 