console.log("Define memberships history");


model.billingController = {

    billing: {
        membership_HistoryId: ko.observable(""),
        payment_date: ko.observable(""),
        initial_date: ko.observable(""),
        final_date: ko.observable(""),
        customerId: ko.observable(""),
        membershipId: ko.observable(""),
        description: ko.observable(""),
    },
    billings: ko.observableArray([]),
    billingIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    datatable: null,


    mapBilling: function (billing) {
        var eBilling = model.billingController.billing;
        eBilling.payment_date(billing.payment_date);
        eBilling.initial_date(billing.initial_date);
        eBilling.final_date(billing.final_date);
        eBilling.customerId(billing.customerId);
        eBilling.membershipId(billing.membershipId);
        eBilling.description(billing.description);
        eBilling.membership_HistoryId(billing.membership_HistoryId);
    },

    initialize: function (idCustomer) {
        console.log("initialize billing controller");
        var selfBillings = this;
        var billings = this.billings();

        console.log(typeof TransShip.Scripts.jsGetHistoryList);
        TransShip.Scripts.jsGetHistoryList(idCustomer, function (data) {
            console.log("done");
            billings = data;
            selfBillings.billings(billings);
        });

    }
};

