console.log("Define approve quotes ");

model.approveController = {

    shipCost: {
        shipmentId: ko.observable(""),
        service_typeId: ko.observable(""),
        measure_unitId: ko.observable(""),
        quantity: ko.observable(""),
        estimated_unit_price: ko.observable(""),
        unit_price: ko.observable(""),
        confirmed_unit_price: ko.observable(""),
        type: ko.observable(""),
        description: ko.observable(""),
        totalbyreg: ko.observable(""),
        totalCosts: ko.observable(""),
        totalServices: ko.observable(""),
        total: ko.observable(""),
        vendor: ko.observable(""),
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

    shipServiceCosts: ko.observableArray([]),
    shipCosts: ko.observableArray([]),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    vendorName: ko.observable(""),

    mapShipment: function (shipment) {
        var eShipment = model.approveController.shipment;
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
        var eCustomer = model.approveController.customer;
        eCustomer.company_name(customer.company_name);
        eCustomer.logo(customer.logo);
        eCustomer.customerId(customer.customerId);
    },

    approve: function () {
        bootbox.confirm("¿Are you sure you want to approve?", function (result) {
        if (result) {
           var shipmentId = model.approveController.shipCost.shipmentId();
           TransShip.Scripts.jsShipmentSetStatus(shipmentId, "A", function(data) {
               console.log("approved");
               setTimeout(function () { window.location.href = '/Customer/Shipments'; }, 2000);
        }); 

          }
        })
    },

    reject: function () {
        bootbox.confirm("¿Are you sure you want to reject?", function (result) {
            if (result) {
                var shipmentId = model.approveController.shipCost.shipmentId();
                TransShip.Scripts.jsShipmentSetStatus(shipmentId, "R", function(data) {
                    console.log("rejected");
                    setTimeout(function () { window.location.href = '/Customer/Shipments'; }, 2000);
                });  
            }
        })
    },

    cancel: function () {
        bootbox.confirm("¿Are you sure you want to cancel shipment?", function (result) {
            if (result) {
                var shipmentId = model.approveController.shipCost.shipmentId();
                TransShip.Scripts.jsShipmentSetStatus(shipmentId, "C", function(data) {
                console.log("cancel shipment");
                setTimeout(function () { window.location.href = '/Admin/Shipments'; }, 2000);
                }); 
            }
        })
    },

    delivery: function () {
        bootbox.confirm("¿Are you sure you want to delivery shipment?", function (result) {
            if (result) {
                var shipmentId = model.approveController.shipCost.shipmentId();
                TransShip.Scripts.jsShipmentSetStatus(shipmentId, "D", function (data) {
                    console.log("cancel shipment");
                    setTimeout(function () { window.location.href = '/Admin/Shipments'; }, 2000);
                });
            }
        })
    },

    initialize: function (shipmentId) {
        console.log("initialize approve controller");
        model.approveController.shipCost.shipmentId(shipmentId);
        var totalCosts = model.approveController.shipCost.totalCosts;
        var totalServices = model.approveController.shipCost.totalServices;
        var total = model.approveController.shipCost.total;

        var selfShipServiceCosts = this;
        var shipServiceCosts = this.shipServiceCosts();

        var selfShipCosts = this;
        var shipCosts = this.shipCosts();

        TransShip.Scripts.jsGetOneShipment(shipmentId, function (data) {
            console.log("done");
            console.log(data);
            model.approveController.mapShipment(data);

            if (data.vendorId != null || data.vendorId != undefined)
            {
                TransShip.Scripts.jsGetVendorsById(data.vendorId, function (data) {
                    console.log("done vendor Id");
                    model.approveController.vendorName(data.company_name);
                });
            }
                

            TransShip.Scripts.jsGetCustomersByCustomerId(data.customerId, function (data) {
                console.log(data);
                model.approveController.mapCustomer(data);
            });
        });


        TransShip.Scripts.jsGetShipServiceCost(shipmentId, function (data) {
            console.log("done service shipment");
            console.log(data);
            shipServiceCosts = data;
            selfShipServiceCosts.shipServiceCosts(shipServiceCosts);
            var sum = _.reduce(shipServiceCosts, function(memo, ship){
                return memo + (ship.confirmed_unit_price * ship.quantity); 
            }, 0);
            totalServices(sum);

            TransShip.Scripts.jsGetShipCosts(shipmentId, function (data) {
                console.log("done ship cost");
                console.log(data);
                shipCosts = data;
                selfShipCosts.shipCosts(shipCosts);
                var sum = _.reduce(shipCosts, function(memo, ship){
                    return memo + (ship.unit_price * ship.quantity); 
                }, 0); 
                totalCosts(sum);
    
                total(totalCosts() + totalServices());
            });
        });


        
        
    }
};

