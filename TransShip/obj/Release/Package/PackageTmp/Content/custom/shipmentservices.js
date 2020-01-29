console.log("Define Contacts");


model.shipServicesController = {

    shipservice: {
        shipmentId: ko.observable(""),
        service_typeId: ko.observable(""),
        measure_unitId: ko.observable(""),
        description: ko.observable(""),
        quantity: ko.observable(""),
        estimated_unit_price: ko.observable(""),
        confirmed_unit_price: ko.observable(""),
    },
    shipservices: ko.observableArray([]),
    shipserviceIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(true),
    gridMode: ko.observable(true),
    datatable: null,

    edit: function (shipservice) {
        console.log(shipservice);
        var shipservices = model.shipServicesController.shipservices();
        var shipserviceIndex = shipservices.indexOf(shipservice);
        console.log(model.shipServicesController.shipserviceIndex);
        model.shipServicesController.mapShip(shipservice);

        console.log("edit");
        model.shipServicesController.editMode(true);
        model.shipServicesController.gridMode(true);
        model.shipServicesController.insertMode(false);
        model.shipServicesController.shipserviceIndex = shipserviceIndex;
        $('#collapseUnit').collapse('show');
    },

    initObject: function () {
        return {
            
                shipmentId: "",
                service_typeId: "",
                measure_unitId: "",
                description: "",
                quantity: "",
                estimated_unit_price: "",
                confirmed_unit_price: ""
            
        }
    },

    mapShip: function (shipservice) {
        var eShipservice = model.shipServicesController.shipservice;
        eShipservice.shipmentId(shipservice.shipmentId);
        eShipservice.service_typeId(shipservice.service_typeId);
        eShipservice.measure_unitId(shipservice.measure_unitId);
        eShipservice.quantity(shipservice.quantity);
        eShipservice.estimated_unit_price(shipservice.estimated_unit_price);
        eShipservice.confirmed_unit_price(shipservice.confirmed_unit_price);
        eShipservice.description(shipservice.description);
    },

    remove: function (shipservice) {
        console.log("remove");
        console.log(shipservice);



        bootbox.confirm("¿Are you sure you want to delete this shipment service?", function (result) {
            if (result) {
                var shipServices = model.shipServicesController.shipservices();
                model.shipServicesController.shipserviceIndex = shipServices.indexOf(shipservice);
                
                //call api remove
                TransShip.Scripts.jsDeleteShip(shipservice, function (data) {
                    console.log("done");
                    shipServices.splice(model.shipServicesController.shipserviceIndex, 1);
                    model.shipServicesController.shipservices(shipServices);
                });
            }
        })
    },

    update: function () {
        var shipservices = model.shipServicesController.shipservices();
        var shipserviceIndex = model.shipServicesController.shipserviceIndex;
        var shipservice = model.shipServicesController.shipservice;
        var shipserviceParam = ko.mapping.toJS(shipservice);
    
        if (!model.validateForm('#containerFrm')) {
            return;
        }

        //call api update
        TransShip.Scripts.jsUpdateShip(shipserviceParam, function () {
            console.log("done");
            model.shipServicesController.insertMode(true);
            model.shipServicesController.editMode(false);
            model.shipServicesController.gridMode(true);
            
            var clean = model.shipServicesController.initObject();
            model.shipServicesController.mapShip(clean);
            model.shipServicesController.shipservice = shipservice;
            
            model.shipServicesController.initialize(shipserviceParam.shipmentId);

            $('#collapseUnit').collapse('hide');
        });
    },

    save: function () {
        var shipservices = model.shipServicesController.shipservices();
        var shipIndex = model.shipServicesController.shipserviceIndex;
        var ship = model.shipServicesController.shipservice;
        var shipParam = ko.toJS(ship);
        shipParam.shipmentId = model.shipmentsController.shipment.shipmentId();
        shipParam.service_typeId = model.shipmentsController.shipment.service_typeId();
        if (!model.validateForm("#containerFrm")) {
            return;
        }
        //call api save
        TransShip.Scripts.jsSaveShip(shipParam, function (data) {
            console.log("done");
            shipParam.description =  model.shipServicesController.getDescription(shipParam.measure_unitId);
            shipservices.push(shipParam);
            model.shipServicesController.shipservices(shipservices);
            model.shipServicesController.insertMode(false);
            model.shipServicesController.editMode(false);
            model.shipServicesController.gridMode(true);

            var shipservice = model.shipServicesController.shipservice;
            var clean = model.shipServicesController.initObject();
            model.shipServicesController.mapShip(clean, shipservice);
            model.shipServicesController.shipservice = shipservice;

            $('#collapseUnit').collapse('hide');
            model.clearErrorMessage("#containerFrm");
            
        });
    },

    cancel: function () {
        model.shipServicesController.insertMode(true);
        model.shipServicesController.editMode(false);
        model.shipServicesController.gridMode(true);

        var shipservice = model.shipServicesController.shipservice;
        var clean = model.shipServicesController.initObject();
        model.shipServicesController.mapShip(clean, shipservice);
        model.shipServicesController.shipservice = shipservice;
        
        //model.shipServicesController.initialize(shipservice.shipmentId());

        $('#collapseUnit').collapse('hide');
        
        model.clearErrorMessage("#containerFrm");
    },

   getDescription: function(measure_unitId) {
        var services = model.shipmentsController.services();
        for (i = 0; i < services.length; i++) {
            if (services[i].measure_unitId === measure_unitId) {
                return services[i].description;
            }
        }
        return "";
    },

    initialize: function (shipmentId) {
        console.log("initialize shipservice controller");
        var selfShips = this;
        var ships = this.shipservices();

        model.shipServicesController.shipservices.subscribe(function() {
            
            if (typeof model.shipmentsController.shipment.totalContainer === "function") {
                var services = model.shipServicesController.shipservices();
                var sum = _.reduce(services, function(memo, ship){
                     return memo + (ship.confirmed_unit_price * ship.quantity); 
                }, 0);
                model.shipmentsController.shipment.totalContainer(sum);
            }
            
        });
        var idType = model.shipmentsController.shipment.service_typeId();
        if (idType && idType !== null && idType !== "") {
            //refresh combo box measures unit (step Container)
            TransShip.Scripts.jsgetOneService(idType, function (data) {
                var services = $.grep(data, function (os) {
                    return os.status === "A";
                });
                model.shipmentsController.services(services);
                //refresh grid Ship Services
                TransShip.Scripts.jsGetShipsList(shipmentId, function (data) {
                    console.log("ship service data");
                    ships = data;
                    ships.forEach(function (ship) {
                        ship.description = model.shipServicesController.getDescription(ship.measure_unitId);
                    });
                    console.log(ships);
                    selfShips.shipservices(ships);
                });
            });
        }
        
    }

};

