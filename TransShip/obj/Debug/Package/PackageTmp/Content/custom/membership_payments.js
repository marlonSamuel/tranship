console.log("Define payment Payments");


model.paymentsController = {

    payment: {
        description: ko.observable(""),
        company_name: ko.observable(""),
        address1: ko.observable(""),
        address2: ko.observable(""),
        state: ko.observable(""),
        city: ko.observable(""),
        CountryName: ko.observable(""),
        expiration_date: ko.observable(""),
        full_name: ko.observable(""),
        zipcode: ko.observable(""),
        membershipId: ko.observable(""),
        customerid: ko.observable(""),
        logo: ko.observable("")
    },

    payments: ko.observableArray([]),
    paymentIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    datatable: null,

    editPayment: function (payment) {
        console.log("entra a edit payment");
        console.log(payment);
        var payments = model.paymentsController.payments();
        model.paymentsController.paymentIndex = payments.indexOf(payment);
        console.log(model.paymentsController.paymentIndex);
        //model.countriesController.country = ko.mapping.fromJS(country, model.countriesController.country);
        model.paymentsController.mapPayment(payment);

        //TransShip.Scripts.jsGetHistoryList(payment.customerId, payment.membershipId, function (data) {
        //    console.log("done");
        //    console.log(data);
        //});
        model.historyController.initialize(payment.customerid);

        console.log("edit");
        model.paymentsController.editMode(true);
        model.paymentsController.gridMode(false);
        model.paymentsController.insertMode(false);
        console.log(model.paymentsController.payment);
    },

    mapPayment: function (payment) {
        var ePayment = model.paymentsController.payment;
        ePayment.description(payment.description);
        ePayment.address1(payment.address1);
        ePayment.address2(payment.address2);
        ePayment.company_name(payment.company_name);
        ePayment.state(payment.state);
        ePayment.city(payment.city);
        ePayment.CountryName(payment.CountryName);
        ePayment.expiration_date(payment.expiration_date);
        ePayment.full_name(payment.full_name);
        ePayment.zipcode(payment.zipcode);
        ePayment.customerid(payment.customerid);
        ePayment.membershipId(payment.membershipId);
        ePayment.logo(payment.logo);
    },

   
    cancel: function () {
        model.paymentsController.insertMode(false);
        model.paymentsController.editMode(false);
        model.paymentsController.gridMode(true);

        TransShip.Scripts.jsGetPaymentList(function (data) {
            console.log("done");
            payments = data;
            model.paymentsController.payments(payments);

        });
    },


    initialize: function () {
        console.log("initialize payment controller");
        var selfPayments = this;
        var payments = this.payments();
        if (payments.length <= 0) {
            TransShip.Scripts.jsGetPaymentList(function (data) {
                console.log("done");
                payments = data;
                selfPayments.payments(payments);
                
            });
        }

    }

};

