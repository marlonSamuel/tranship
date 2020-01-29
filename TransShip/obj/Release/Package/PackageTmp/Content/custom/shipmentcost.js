console.log("Define Contacts");


model.shipCostController = {

    shipcost: {
        shipmentId: ko.observable(""),
        service_typeId: ko.observable(""),
        service_description: ko.observable(""),
        measure_unitId: ko.observable(""),
        description: ko.observable(""),
        quantity: ko.observable(""),
        unit_price: ko.observable(""),
        price: ko.observable(""),
        vendor: ko.observable(""),
    },
    shipcosts: ko.observableArray([]),
    allmeasures: ko.observableArray([]),
    shipcostIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    datatable: null,
    

    mapShip: function (shipcost) {
        var eshipcost = model.shipCostController.shipcost;
        eshipcost.service_typeId(shipcost.service_typeId);
        eshipcost.measure_unitId(shipcost.measure_unitId);
        eshipcost.service_description(shipcost.service_description);
        eshipcost.description(shipcost.description);
        eshipcost.quantity(shipcost.quantity);
        eshipcost.unit_price(shipcost.unit_price);
        eshipcost.price(shipcost.price);
        eshipcost.vendor(shipcost.vendor);
    },

    remove: function (shipcost) {
        console.log("remove");
        console.log(shipcost);



        bootbox.confirm("¿Are you sure you want to delete this shipment cost?", function (result) {
            if (result) {
                var shipcosts = model.shipCostController.shipcosts();
                model.shipCostController.shipcostIndex = shipcosts.indexOf(shipcost);
                
                //call api remove
                TransShip.Scripts.jsDeleteShipmentCost(shipcost, function (data) {
                    console.log("done");
                    shipcosts.splice(model.shipCostController.shipcostIndex, 1);
                    model.shipCostController.shipcosts(shipcosts);
                });
            }
        })
    },

    save: function () {
        var shipcosts = model.shipCostController.shipcosts();
        var shipIndex = model.shipCostController.shipcostIndex;
        var ship = model.shipCostController.shipcost;
        var shipParam = ko.toJS(ship);
        shipParam.shipmentId = model.shipmentsController.shipment.shipmentId();
        
        if (!model.validateForm("#costsFrm")) {
            return;
        }
        //call api save
        TransShip.Scripts.jsSaveShipmentCost(shipParam, function (data) {
            console.log("done");
            var service_description = _.findWhere(model.shipmentsController.otherservices(),{service_typeId: shipParam.service_typeId});
            shipParam.service_description = service_description.descripcion;
            var unit_description = _.findWhere(model.shipCostController.allmeasures(),{service_typeId: shipParam.service_typeId, measure_unitId: shipParam.measure_unitId});
            shipParam.description = unit_description.description;
            shipcosts.push(shipParam);
            model.shipCostController.shipcosts(shipcosts);
            model.shipCostController.insertMode(true);
            model.shipCostController.editMode(false);
            model.shipCostController.gridMode(true);
            
        });
    },

    initialize: function (shipmentId) {
        console.log("initialize shipcost controller");
        var selfShips = this;
        var ships = this.shipcosts();

        model.shipCostController.shipcosts.subscribe(function() {
            var shipcosts = model.shipCostController.shipcosts();
            var sum = _.reduce(shipcosts, function(memo, ship){
                 return memo + (ship.unit_price * ship.quantity); 
            }, 0);
            model.shipmentsController.shipment.totalCosts(sum);
        });
        
        TransShip.Scripts.jsGetAllMeasures(function(mesaures) {
            selfShips.allmeasures(mesaures);
            TransShip.Scripts.jsGetShipmentCostList(shipmentId, function (data) {
                console.log("ship service data");
                ships = data;
                ships.forEach(function(ship) {
                    var service_description = _.findWhere(model.shipmentsController.otherservices(), { service_typeId: ship.service_typeId });
                    if (service_description) {
                        ship.service_description = service_description.descripcion;
                        var unit_description = _.findWhere(mesaures, { service_typeId: ship.service_typeId, measure_unitId: ship.measure_unitId });
                        ship.description = unit_description.description;
                    } else {
                        ship.service_description = "";
                        ship.description = "";
                    }
                    
                });
                console.log(ships);
                selfShips.shipcosts(ships);
            });
        });
    }

};

