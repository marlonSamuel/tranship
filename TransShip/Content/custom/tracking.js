console.log("Define tracking ");


model.trackingController = {

    tracking: {
        shipmentId: ko.observable(""),
        shipment_trackingId: ko.observable(""),
        userId: ko.observable(""),
        comentaries: ko.observable(""),
        status: ko.observable(""),
        creation_date: ko.observable(""),
        username: ko.observable(""),
    },

    shipment: {
        shipmentId: ko.observable(""),
        consignee_name: ko.observable(""),
        target_company: ko.observable(""),
        shipment_date: ko.observable(""),
        status: ko.observable(""),
        current_tracking_date: ko.observable(""),
        total: ko.observable(""),
        userId: ko.observable(""),
        estimated_delivery_date: ko.observable(""),
        purchase_order: ko.observable("")
    },

    customer: {
        company_name: ko.observable(""),
        logo: ko.observable(""),
        customerId: ko.observable("")
    },

    tracks: ko.observableArray([]),
    trackIndex: 0,
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    vendorName: ko.observable(""),

    mapTracking: function (tracking) {
        var eTraking = model.trackingController.tracking;
        eTraking.shipmentId(tracking.shipmentId);
        eTraking.status(tracking.status);
        eTraking.shipment_trackingId(tracking.shipment_trackingId);
        eTraking.comentaries(tracking.comentaries);
        eTraking.creation_date(tracking.creation_date);
        eTraking.userId(tracking.userId);
        eTraking.username(tracking.username);
    },

    mapShipment: function (shipment) {
        var eShipment = model.trackingController.shipment;
        eShipment.shipmentId(shipment.shipmentId);
        eShipment.target_company(shipment.target_company);
        eShipment.consignee_name(shipment.consignee_name);
        eShipment.shipment_date(shipment.shipment_date);
        eShipment.current_tracking_date(shipment.current_tracking_date);
        eShipment.status(shipment.status);
        eShipment.total(shipment.total);
        eShipment.userId(shipment.userId);
        eShipment.estimated_delivery_date(shipment.estimated_delivery_date);
        eShipment.purchase_order(shipment.purcharse_order);
    },

    mapCustomer: function (customer) {
        var eCustomer = model.trackingController.customer;
        eCustomer.company_name(customer.company_name);
        eCustomer.logo(customer.logo);
        eCustomer.customerId(customer.customerId);
    },


    showNew: function () {
        console.log("show new");
        var tracks = model.trackingController.tracks();
        model.trackingController.trackingIndex = tracks.length;
        var tracking = {
            shipmentId: "",
            trackingId: "",
            userId: "",
            comentaries: "",
            status: "",
            creation_date: "",
        };

        tracking.shipmentId = model.trackingController.shipment.shipmentId();
        tracking.userId = model.usersController.user.userId();
        model.trackingController.mapTracking(tracking);

        model.trackingController.insertMode(true);
        model.trackingController.gridMode(false);
    },

    save: function () {
        var tracks = model.trackingController.tracks();
        var tracksIndex = model.trackingController.tracksIndex;
        var tracking = model.trackingController.tracking;
        
        var trackingParam = ko.toJS(tracking);

        console.log(typeof (trackingParam.creation_date));

        if (!model.validateForm('#trackEdit')) {
            return;
        }
        //call api save
        TransShip.Scripts.jsSaveTracking(trackingParam, function (data) {
            console.log("done");
            TransShip.Scripts.jsGettracksList(trackingParam.shipmentId, function (data) {
                console.log("done");
                tracks = data;
                model.trackingController.tracks(tracks);
            });

            setTimeout(function (tracking) {
                model.trackingController.insertMode(false);
                model.trackingController.gridMode(true);
            }, 2000);
        });
    },

    
    readtracking: function (tracking) {
        console.log(tracking);
        model.trackingController.maptracking(tracking);
        var tracking = model.trackingController.tracking;
        var tracks = model.trackingController.tracks();
        var tracksIndex = model.trackingController.tracksIndex;
        var trackingParam = ko.toJS(tracking);

        TransShip.Scripts.jsUpdateTraking(trackingParam, function () {
            console.log("done");

            TransShip.Scripts.jsGettracksList(trackingParam.shipmentId, function (data) {
                console.log("done");
                tracks = data;
                model.trackingController.tracks(tracks);
            });

        });
        model.trackingController.insertMode(false);
        model.trackingController.gridMode(false);
        model.trackingController.viewtracking(true);
    },

    cancel: function () {
        model.trackingController.insertMode(false);
        model.trackingController.gridMode(true);

        model.clearErrorMessage("#trackEdit");
    },


    initialize: function (shipmentId) {
        console.log("initialize tracking controller");
        var selftracks = this;
        var tracks = this.tracks();

        TransShip.Scripts.jsGetOneShipment(shipmentId, function (data) {
            console.log("done tracking");
            console.log(data);
            model.trackingController.mapShipment(data);

            if (data.vendorId != null || data.vendorId != undefined)
            {
                TransShip.Scripts.jsGetVendorsById(data.vendorId, function (data) {
                    console.log("done vendor Id");
                    model.trackingController.vendorName(data.company_name);
                });
            }

            model.approveController.initialize(shipmentId);

            TransShip.Scripts.jsGetCustomersByCustomerId(data.customerId, function (data) {
                console.log(data);
                model.trackingController.mapCustomer(data);
            });
        });

        
        TransShip.Scripts.jsGettracksList(shipmentId, function (data) {
            console.log("done");
            tracks = data;
            selftracks.tracks(tracks);

        });
        
    }
};

