console.log("Define memberships history");


model.historyController = {

    history: {
        membership_HistoryId: ko.observable(""),
        payment_date: ko.observable(""),
        initial_date: ko.observable(""),
        final_date: ko.observable(""),
        customerId: ko.observable(""),
        membershipId: ko.observable(""),
        description: ko.observable(""),
    },
    histories: ko.observableArray([]),
    members: ko.observableArray([]),
    historyIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    datatable: null,

    editHistory: function (history) {
        console.log(history);
        var histories = model.historyController.histories();
        model.historyController.historyIndex = histories.indexOf(history);
        console.log(model.historyController.historyIndex);
        //model.countriesController.country = ko.mapping.fromJS(country, model.countriesController.country);
        model.historyController.mapHistory(history);

        console.log("edit");
        model.historyController.editMode(true);
        model.historyController.gridMode(false);
        model.historyController.insertMode(false);
        console.log(model.historyController.member);
    },

    mapHistory: function (history) {
        var eHistory = model.historyController.history;
        eHistory.payment_date(history.payment_date);
        eHistory.initial_date(history.initial_date);
        eHistory.final_date(history.final_date);
        eHistory.customerId(history.customerId);
        eHistory.membershipId(history.membershipId);
        eHistory.description(history.description);
        eHistory.membership_HistoryId(history.membership_HistoryId);
    },

    removeHistory: function (history) {
        console.log("remove");
        console.log(history);



        bootbox.confirm("¿Are you sure you want to delete " + history.description + "?", function (result) {
            if (result) {
                var histories = model.historyController.histories();
                model.historyController.historyIndex = histories.indexOf(History);
                histories.splice(model.historyController.historyIndex, 1);
                model.historyController.histories(histories);
                //call api remove
                TransShip.Scripts.jsDeleteHistory(history.membership_HistoryId, function (data) {
                    console.log("done");
                });
            }
        })
    },

    showNewHistory: function () {
        console.log("show new");
        var histories = model.historyController.histories();
        model.historyController.historyIndex = histories.length;
        var history = {
            payment_date: "",
            initial_date: "",
            final_date: "",
            customerId: "",
            description: "",
            membershipId: "",
            membership_HistoryId: ""
        };
        history.customerId = model.paymentsController.payment.customerid();
        model.historyController.mapHistory(history);

        model.historyController.insertMode(true);
        model.historyController.editMode(false);
        model.historyController.gridMode(false);

        TransShip.Scripts.jsGetMemberList(function (data) {
            console.log("done");
            model.membersController.members(data);
        });

        var number_shipments = model.paymentsController.payment.number_shipments();
        var max_number_shipments = model.paymentsController.payment.max_number_shipments();
        var final_date = moment(model.paymentsController.payment.final_date()).format('MM/DD/YYYY');
        number_shipments = 8;
        var date = moment();
        var current_date = moment().add(1, 'M').format('MM/DD/YYYY');

        if (moment(final_date) < moment(current_date)) {
            if (number_shipments < max_number_shipments) {
                bootbox.alert("the customer have an active membership");
                model.historyController.gridMode(true);
                model.historyController.insertMode(false);
            }
        }
    },

    update: function () {
        var datatable = model.historyController.datatable;
        var histories = model.historyController.histories();
        var historiesIndex = model.historyController.historiesIndex;
        var history = model.historyController.history;
        var historyParam = ko.toJS(history);

        if (!model.validateForm('#HistoryEdit')) {
            return;
        }

        //if (historyParam.payment_date < historyParam.initial_date) {
        //    bootbox.alert("errror! the payment date must be greater than the final date");
        //    return;
        //}

        if (historyParam.final_date < historyParam.initial_date) {
            bootbox.alert("errror! the initial date must be greater than the final date");
            return;
        } else {
            //call api update
            if (TransShip.Scripts) {
                TransShip.Scripts.jsUpdateHistory(historyParam, function (data) {
                    console.log("done");

                    TransShip.Scripts.jsGetHistoryList(historyParam.customerId, function (data) {
                        console.log("done");
                        histories = data;
                        model.historyController.histories(histories);
                        setTimeout(function () {
                            model.historyController.insertMode(false);
                            model.historyController.editMode(false);
                            model.historyController.gridMode(true);
                        }, 2000);
                    });
                });
            }
        }
    },

    save: function () {
        var datatable = model.historyController.datatable;
        var histories = model.historyController.histories();
        var historyIndex = model.historyController.historyIndex;
        var history = model.historyController.history;
        var historyParam = ko.toJS(history);

        if (!model.validateForm('#HistoryEdit')) {
            return;
        }

        if (historyParam.payment_date > historyParam.final_date)
        {
            bootbox.alert("Payment Date must be older than Final Date   ");
            return;
        }

        if (moment(historyParam.payment_date).calendar() <= moment(historyParam.initial_date).subtract(1, 'years').calendar()) {
            bootbox.alert("Payment Date must be at most one year lower than the Initial Date");
            return;
        }

        if (historyParam.final_date < historyParam.initial_date) {
            bootbox.alert("Initial Date must be older than Final Date");
            return;
        } else {
            //call api save
            if (TransShip.Scripts) {

                TransShip.Scripts.jsSaveHistory(historyParam, function (data) {
                    console.log("done");

                    TransShip.Scripts.jsGetHistoryList(historyParam.customerId, function (data) {
                        console.log("done");
                        histories = data;
                        model.historyController.histories(histories);
                        TransShip.Scripts.jsGetPaymentList(function (data) {
                            console.log("done");
                            var payments = data;
                            var customerLastPayment = _.findWhere(payments, { customerid: historyParam.customerId });
                            model.paymentsController.editPayment(customerLastPayment);
                            setTimeout(function () {
                                model.historyController.insertMode(false);
                                model.historyController.editMode(false);
                                model.historyController.gridMode(true);

                            }, 2000);
                        });
                        
                    });
                    
                });
            }
        }
    },

    cancel: function () {
        model.historyController.insertMode(false);
        model.historyController.editMode(false);
        model.historyController.gridMode(true);

        model.clearErrorMessage("#HistoryEdit");
    },


    initialize: function (idCustomer) {
        console.log("initialize histories controller");
        var selfHistories = this;
        var histories = this.histories();

            console.log(typeof TransShip.Scripts.jsGetHistoryList);
            TransShip.Scripts.jsGetHistoryList(idCustomer, function (data) {
                console.log("done");
                histories = data;
                selfHistories.histories(histories);
            });

            TransShip.Scripts.jsGetMemberList(function (data) {
                console.log("done");
                model.historyController.members(data);
            });

    }
};

