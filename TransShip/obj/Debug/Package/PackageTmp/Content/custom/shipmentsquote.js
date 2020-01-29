console.log("Define Shiments");


model.shipmentsController = {

    shipment: {
        shipmentId: ko.observable(""),
        customerId: ko.observable(""),
        company_name: ko.observable(""),
        userId: ko.observable(""),
        countryId: ko.observable(""),
        CountryName: ko.observable(""),
        service_typeId: ko.observable(""),
        consignee_name: ko.observable(""),
        target_company: ko.observable(""),
        phone_number: ko.observable(""),
        state: ko.observable(""),
        stateText: ko.observable(""),
        city: ko.observable(""),
        address1: ko.observable(""),
        address2: ko.observable(""),
        addressId: ko.observable(""),
        zipcode: ko.observable(""),
        shipping_terms: ko.observable(""),
        tnc_accepted: ko.observable(false),
        shipment_date: ko.observable(""),
        shipment_number: ko.observable(""),
        preferred_vendor: ko.observable(""),
        estimated_delivery_date: ko.observable(""),
        description: ko.observable(""),
        current_tracking: ko.observable(""),
        current_tracking_date: ko.observable(""),
        status: ko.observable(""),
        newAddress: ko.observable(false),
        measure_unitId: ko.observable(""),
        vendorId: ko.observable(""),
        totalContainer: ko.observable(0),
        totalCosts: ko.observable(0),
        total: ko.pureComputed(function(){
            return model.shipmentsController.shipment.totalContainer() + model.shipmentsController.shipment.totalCosts(); 
        }, this)
    },

    shipments: ko.observableArray([]), //frontend new shipments (status: P)
    shipmentsPending: ko.observableArray([]), //frontend Quoted (status: Q)
    shipmentsProcess:  ko.observableArray([]), //frontend Approved (status: A)
    shipmentsHistory:  ko.observableArray([]), //frontend (status: R, C, D)
    shipmentIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    countries: ko.observableArray([]),
    states: ko.observableArray([]),
    consignees: ko.observableArray([]),
    containers: ko.observableArray([]),
    services: ko.observableArray([]),
    otherservices: ko.observableArray([]),
    otherunits: ko.observableArray([]),
    customers: ko.observableArray([]),
    vendors: ko.observableArray([]),
    validateState: ko.observable(false),
    mandatoryZipcode: ko.observable(false),
    validateInputs: ko.observable(false),
    validateSelect: ko.observable(true),
    isChecked: ko.observable(false),
    statusOptions: [{ name: 'N', value: 'Saved' }, { name: 'P', value: 'Sent' }],
    incoterms: [{ name: 'FOB', value: 'FOB' }, { name: 'CIF', value: 'CIF' }],
    currentStep: ko.observable(2),
    historyDateFrom: ko.observable(moment().subtract(1,'months')),
    historyDateTo: ko.observable(moment()),

    editShipment: function (shipment) {
        console.log("entra a edit shipment");
        console.log(shipment);
        var shipments = model.shipmentsController.shipments();
        model.shipmentsController.shipmentIndex = shipments.indexOf(shipment);
        console.log(model.shipmentsController.shipmentIndex);
        //model.countriesController.country = ko.mapping.fromJS(country, model.countriesController.country);
        model.shipmentsController.mapShipment(shipment);
        model.shipServicesController.initialize(shipment.shipmentId);
        model.shipCostController.initialize(shipment.shipmentId);
        model.notesController.initialize(shipment.shipmentId);
        model.shipFilesController.initialize(shipment.shipmentId);
        model.shipmentsController.countryChange();
        if (shipment.addressId && shipment.addressId !== null) {
            model.shipmentsController.validateInputs(false);
            model.shipmentsController.validateSelect(true);
        } else {
            model.shipmentsController.validateInputs(true);
            model.shipmentsController.validateSelect(false);
        }

        TransShip.Scripts.jsGetAddressList(shipment.customerId, function (data) {
            model.shipmentsController.consignees(data);
        });

        console.log("edit");
        model.shipmentsController.editMode(true);
        model.shipmentsController.gridMode(false);
        model.shipmentsController.insertMode(false);
        console.log(model.shipmentsController.shipment);
    },

    mapShipment: function (shipment) {
        var eShipment = model.shipmentsController.shipment;
        eShipment.shipmentId(shipment.shipmentId);
        eShipment.customerId(shipment.customerId);
        eShipment.company_name(shipment.company_name);
        eShipment.userId(shipment.userId);
        eShipment.countryId(shipment.countryId);
        eShipment.CountryName(shipment.CountryName);
        eShipment.service_typeId(shipment.service_typeId);
        eShipment.consignee_name(shipment.consignee_name);
        eShipment.target_company(shipment.target_company);
        eShipment.phone_number(shipment.phone_number);
        eShipment.state(shipment.state);
        eShipment.stateText(shipment.state);
        eShipment.city(shipment.city);
        eShipment.address1(shipment.address1);
        eShipment.address2(shipment.address2);
        eShipment.addressId(shipment.addressId);
        eShipment.zipcode(shipment.zipcode);
        eShipment.shipping_terms(shipment.shipping_terms);
        eShipment.tnc_accepted(shipment.tnc_accepted === 'N' ? false : true);
        eShipment.shipment_date(shipment.shipment_date);
        eShipment.shipment_number(shipment.shipment_number);
        eShipment.preferred_vendor(shipment.preferred_vendor);
        eShipment.estimated_delivery_date(shipment.estimated_delivery_date);
        eShipment.description(shipment.description);
        eShipment.current_tracking(shipment.current_tracking);
        eShipment.current_tracking_date(shipment.current_tracking_date);
        eShipment.status(shipment.status);
        eShipment.service_typeId(shipment.service_typeId);
        eShipment.newAddress(shipment.newAddress);
        eShipment.measure_unitId(shipment.measure_unitId);
        eShipment.vendorId(shipment.vendorId);
        eShipment.totalContainer(0);
        eShipment.totalCosts(0);
    },

    removeShipment: function (shipment) {
        console.log("remove");
        console.log(shipment);



        bootbox.confirm("¿Are you sure you want to delete the Shipment?", function (result) {
            if (result) {
                var shipments = model.shipmentsController.shipments();
                model.shipmentsController.shipmentIndex = shipments.indexOf(shipment);

                //call api remove
                TransShip.Scripts.jsDeleteShipment(shipment.shipmentId, function (data) {
                    setTimeout(function () {
                        location.reload(1);
                    }, 2000);
                });
            }
        });
    },

    showNewShipment: function () {
        console.log("show new");
        var shipments = model.shipmentsController.shipments();
        model.shipmentsController.shipmentIndex = shipments.length;
        var shipment = {
            shipmentId: "",
            customerId: "",
            company_name: "",
            userId: "",
            countryId: "",
            CountryName: "",
            service_typeId: "",
            consignee_name: "",
            target_company: "",
            phone_number: "",
            state: "",
            stateText: "",
            addressId: "",
            city: "",
            address1: "",
            address2: "",
            zipcode: "",
            shipping_terms: "",
            tnc_accepted: "N",
            shipment_date: "",
            shipment_number: "",
            preferred_vendor: "",
            estimated_delivery_date: "",
            description: "",
            current_tracking: "",
            current_tracking_date: "",
            status: "N",
            newAddress: false,
            vendorId: "",
            totalContainer: 0,
            totalCosts: 0
        };
        shipment.customerId = model.usersController.user.customerId();
        shipment.userId = model.usersController.user.userId();
        model.shipmentsController.mapShipment(shipment);
        model.shipServicesController.shipservices([]);

        model.shipmentsController.insertMode(true);
        model.shipmentsController.editMode(false);
        model.shipmentsController.gridMode(false);
    },

    update: function () {
        var shipments = model.shipmentsController.shipments();
        var shipmentIndex = model.shipmentsController.shipmentIndex;
        var shipment = model.shipmentsController.shipment;
        var shipmentParam = ko.toJS(shipment);

        if (shipmentParam.stateText !== null || shipmentParam.stateText !== undefined) {
            shipmentParam.state = shipmentParam.stateText;
        }
        if (shipmentParam.tnc_accepted === true) {
            shipmentParam.tnc_accepted = "Y";
        } else {
            shipmentParam.tnc_accepted = "N";
        }

        if (!model.shipmentsController.isValidStepOne() || !model.shipmentsController.isValidStepTwo()) {
            return;
        }

        //validate the checkbox value
        //if (shipmentParam.newAddress) {
        //    TransShip.Scripts.jsSaveAddress(shipmentParam, function (data) {
        //        shipmentParam.addressId = data.addressId;
        //    });
        //}

        //call api update
        TransShip.Scripts.jsUpdateShipment(shipmentParam, function () {
        //    setTimeout(function () {
        //        location.reload(1);
        //    }, 2000);
        });
    },

    setVendorPrices: function () {
        var shipments = model.shipmentsController.shipments();
        var shipmentIndex = model.shipmentsController.shipmentIndex;
        var shipment = model.shipmentsController.shipment;
        var shipmentParam = ko.toJS(shipment);

        if (shipmentParam.stateText !== null || shipmentParam.stateText !== undefined) {
            shipmentParam.state = shipmentParam.stateText;
        }
        if (shipmentParam.tnc_accepted === true) {
            shipmentParam.tnc_accepted = "Y";
        } else {
            shipmentParam.tnc_accepted = "N";
        }

        //call api update
        TransShip.Scripts.jsUpdateShipment(shipmentParam, function () {
            TransShip.Scripts.jsShipmentSetVendorPrices(shipmentParam.shipmentId, function () {
                model.shipServicesController.initialize(shipmentParam.shipmentId);
            });
            
        });
    },

    quote: function () {
        var shipments = model.shipmentsController.shipments();
        var shipmentIndex = model.shipmentsController.shipmentIndex;
        var shipment = model.shipmentsController.shipment;
        var shipmentParam = ko.toJS(shipment);

        if (shipmentParam.stateText !== null || shipmentParam.stateText !== undefined) {
            shipmentParam.state = shipmentParam.stateText;
        }
        if (shipmentParam.tnc_accepted === true) {
            shipmentParam.tnc_accepted = "Y";
        } else {
            shipmentParam.tnc_accepted = "N";
        }

        if (!model.shipmentsController.isValidStepOne() || !model.shipmentsController.isValidStepTwo() 
            || !model.shipmentsController.isValidStepThree() ) {
            return;
        }

        var total = model.shipmentsController.shipment.total();
        if (total <= 0) {
            toastr.error("The shipment total is 0");
            return;
        }

        shipmentParam.status = "Q";
        //call api update
        TransShip.Scripts.jsUpdateShipment(shipmentParam, function () {
            
            setTimeout(function () { 
                location.reload(1);
            }, 2000);
        });
    },

    save: function () {
        var shipments = model.shipmentsController.shipments();
        var shipmentIndex = model.shipmentsController.shipmentIndex;
        var shipment = model.shipmentsController.shipment;
        var shipmentParam = ko.toJS(shipment);
        shipmentParam.userId = model.usersController.user.userId();
        shipmentParam.customerId = model.usersController.user.customerId();

        if (shipmentParam.stateText !== null || shipmentParam.stateText !== undefined)
        {
            shipmentParam.state = shipmentParam.stateText;
        }
        if (shipmentParam.tnc_accepted === true) {
            shipmentParam.tnc_accepted = "Y";
        } else {
            shipmentParam.tnc_accepted = "N";
        }

        if (!model.shipmentsController.isValidStepOne() || !model.shipmentsController.isValidStepTwo() ) {
            return;
        }
        
        //validate the checkbox value
        if (shipmentParam.newAddress) {
            TransShip.Scripts.jsSaveAddress(shipmentParam, function (data) {
                shipmentParam.addressId = data.addressId;
            });
        }
        //call api save
        TransShip.Scripts.jsSaveShipment(shipmentParam, function (data) {
            setTimeout(function () {
                location.reload(1);
            }, 2000);
        });
    },

    getServicesType: function (event)
    {
        var idType = model.shipmentsController.shipment.service_typeId();
        if (idType && idType !== null && idType !== "") {
            //refresh combo box measures unit (step Container)
            TransShip.Scripts.jsgetOneService(idType, function (data) {
                var services = $.grep(data, function(os){
                    return os.status === "A";
                });
                model.shipmentsController.services(services);
            });
        }
    },

    getOtherUnits: function (event)
    {
        var idType = model.shipCostController.shipcost.service_typeId();
        if (idType && idType !== null && idType !== "") {
            //refresh combo box measures unit (step Container)
            TransShip.Scripts.jsgetOneService(idType, function (data) {
                var otherunits = $.grep(data, function(os){
                    return os.status === "A";
                });
                model.shipmentsController.otherunits(otherunits);
            });
        }
    },

    isValidStepOne: function() {
        var valid = model.validateForm('#destinationFrm');
        if (!valid) {
            toastr.error("Please complete Destination");
        }
        return valid;
    },

    isValidStepTwo: function() {
        var valid = model.validateForm('#informationFrm');
        if (!valid) {
            toastr.error("Please complete Information");
        }

        return valid; 
    },

    isValidStepThree: function() {
        var valid = true;
        var shipservices = model.shipServicesController.shipservices();
        if (shipservices.length <= 0) {
            toastr.error("Please complete Container");
            valid = false;
        }
        return valid;
    },

    step: function(toStep) {
        var currentStep = model.shipmentsController.currentStep();
        if (toStep === 2) {
            if (!model.shipmentsController.isValidStepOne()) {
                return;
            }
        } else if (toStep === 3) {
            if (!model.shipmentsController.isValidStepOne() || !model.shipmentsController.isValidStepTwo()) {
                return;
            }

        } else if (toStep === 4 || toStep === 5) {
            if (!model.shipmentsController.isValidStepOne() || !model.shipmentsController.isValidStepTwo()
              || !model.shipmentsController.isValidStepThree()) {
                return;
            }
        }
        if (toStep >= 3) {
            var shipmentId = model.shipmentsController.shipment.shipmentId();
            if (typeof shipmentId === "undefined" || shipmentId === "" || shipmentId === "0") {
                toastr.error("Please save the shipment");
                return;
            }
        }
        model.shipmentsController.currentStep(toStep);
    },

    nextStep: function() {
        var currentStep = model.shipmentsController.currentStep();
        if (currentStep < 4) {
            model.shipmentsController.step(currentStep + 1);
        }
        
    },

    prevStep: function() {
        var currentStep = model.shipmentsController.currentStep();
        if (currentStep > 1) {
            currentStep--;
            model.shipmentsController.currentStep(currentStep);
        }
    },

    cancel: function () {
        model.shipmentsController.insertMode(false);
        model.shipmentsController.editMode(false);
        model.shipmentsController.gridMode(true);
        location.reload(1);
    },

    countryChange: function (event) {

        var countries = ko.toJS(model.shipmentsController.countries);
        var countryId = model.shipmentsController.shipment.countryId();
        var countrySelected = $.grep(countries, function (country) {
            return country.CountryID === countryId;
        });
        countrySelected = countrySelected[0];
        
        if (countrySelected !== null) {
            model.shipmentsController.validateState(countrySelected.validate_state === 'N' ? false : true);
            model.shipmentsController.mandatoryZipcode(countrySelected.mandatory_zipcode === 'N' ? false : true);
            if (countrySelected.validate_state === 'N') {
                model.shipmentsController.states([]);
            } else {
                TransShip.Scripts.jsGetStateList(countryId, function (data) {
                    model.shipmentsController.states(data);
                });
            }
        }

    },

    getAddress: function (event)
    {
        var consignees = ko.toJS(model.shipmentsController.consignees);
        var addressId = model.shipmentsController.shipment.addressId();
        var addressSelected = $.grep(consignees, function (address) {
            return address.addressId === addressId;
        });
        addressSelected = addressSelected[0];
        console.log(addressSelected);
        if (addressSelected !== null)
        {
            model.shipmentsController.shipment.target_company(addressSelected.target_company);
            model.shipmentsController.shipment.consignee_name(addressSelected.consignee_name);
            model.shipmentsController.shipment.phone_number(addressSelected.phone_number);
            model.shipmentsController.shipment.address1(addressSelected.address1);
            model.shipmentsController.shipment.address2(addressSelected.address2);
            model.shipmentsController.shipment.zipcode(addressSelected.zipcode);
            model.shipmentsController.shipment.countryId(addressSelected.countryId);
            model.shipmentsController.shipment.state(addressSelected.state);
            model.shipmentsController.shipment.stateText(addressSelected.state);
            model.shipmentsController.shipment.city(addressSelected.city);
        }

    },

    //function to clean the fields for a new address
    newAddres: function ()
    {
        if (model.shipmentsController.validateInputs()) {
            model.shipmentsController.validateInputs(false);
            model.shipmentsController.validateSelect(true);
        } else {
            model.shipmentsController.validateInputs(true);
            model.shipmentsController.validateSelect(false);
        }
        model.shipmentsController.shipment.target_company("");
        model.shipmentsController.shipment.consignee_name("");
        model.shipmentsController.shipment.phone_number("");
        model.shipmentsController.shipment.address1("");
        model.shipmentsController.shipment.address2("");
        model.shipmentsController.shipment.zipcode("");
        model.shipmentsController.shipment.countryId("");
        model.shipmentsController.shipment.state("");
        model.shipmentsController.shipment.stateText("");
        model.shipmentsController.shipment.city("");
        model.shipmentsController.shipment.addressId("");
    },

    refreshHistory: function () {
        var countries = model.shipmentsController.countries();
        var customers = model.shipmentsController.customers();
        var from = model.shipmentsController.historyDateFrom();
        var to = model.shipmentsController.historyDateTo();
        from = moment(from).format('YYYY-MM-DD');
        to = moment(to).format('YYYY-MM-DD');
        var history = [];
        TransShip.Scripts.jsGetShipmentListByStatusAndDateRange("C", from, to, function (shipmentsC) {
            shipmentsC.forEach(function (ship) {
                var country = _.findWhere(countries, {CountryID: ship.countryId});
                ship.CountryName = country.CountryName;
                var customer = _.findWhere(customers, {customerId: ship.customerId});
                ship.company_name = customer.company_name;
            });
            $.merge(history, shipmentsC);
            TransShip.Scripts.jsGetShipmentListByStatusAndDateRange("R", from, to, function (shipmentsR) {
                shipmentsR.forEach(function (ship) {
                    var country = _.findWhere(countries, {CountryID: ship.countryId});
                    ship.CountryName = country.CountryName;
                    var customer = _.findWhere(customers, {customerId: ship.customerId});
                    ship.company_name = customer.company_name;
                });
                $.merge(history, shipmentsR);
                TransShip.Scripts.jsGetShipmentListByStatusAndDateRange("D", from, to, function (shipmentsD) {
                    shipmentsD.forEach(function (ship) {
                        var country = _.findWhere(countries, {CountryID: ship.countryId});
                        ship.CountryName = country.CountryName;
                        var customer = _.findWhere(customers, {customerId: ship.customerId});
                        ship.company_name = customer.company_name;
                    });
                    $.merge(history, shipmentsD);
                    model.shipmentsController.shipmentsHistory(history);
                });
            });
        });
    },

    initialize: function (idCustomer) {
        console.log("initialize shipment quote controller");
        var selfShipments = this;
        model.shipmentsController.shipment.service_typeId.subscribe(function () {
            //update combo of measure units
            model.shipmentsController.getServicesType();
        });
        model.shipCostController.shipcost.service_typeId.subscribe(function () {
            //update combo of measure units
            model.shipmentsController.getOtherUnits();
        });

        TransShip.Scripts.jsGetCountryList(function (countries) {
            model.shipmentsController.countries(countries);

            TransShip.Scripts.jsGetCustomers(function (customers) {
                model.shipmentsController.customers(customers);

                TransShip.Scripts.jsGetShipmentListByStatus("P", function (shipments) {
                    shipments.forEach(function (ship) {
                        var country = _.findWhere(countries, {CountryID: ship.countryId});
                        ship.CountryName = country.CountryName;
                        var customer = _.findWhere(customers, {customerId: ship.customerId});
                        ship.company_name = customer.company_name;
                    });
                    selfShipments.shipments(shipments);
                });
                TransShip.Scripts.jsGetShipmentListByStatus("Q", function (shipments) {
                    shipments.forEach(function (ship) {
                        var country = _.findWhere(countries, {CountryID: ship.countryId});
                        ship.CountryName = country.CountryName;
                        var customer = _.findWhere(customers, {customerId: ship.customerId});
                        ship.company_name = customer.company_name;
                    });
                    selfShipments.shipmentsPending(shipments);
                });
                TransShip.Scripts.jsGetShipmentListByStatus("A", function (shipments) {
                    shipments.forEach(function (ship) {
                        var country = _.findWhere(countries, {CountryID: ship.countryId});
                        ship.CountryName = country.CountryName;
                        var customer = _.findWhere(customers, {customerId: ship.customerId});
                        ship.company_name = customer.company_name;
                    });
                    selfShipments.shipmentsProcess(shipments);
                });

                model.shipmentsController.refreshHistory();
            });
        });

        TransShip.Scripts.jsGetServiceList(function (data) {
            var containers = $.grep(data, function(cn){
                return cn.user_input === "Y" && cn.status === "A";
            });
            var otherservices = $.grep(data, function(os){
                return os.user_input === "N" && os.status === "A";
            });
            model.shipmentsController.containers(containers);
            model.shipmentsController.otherservices(otherservices);
        });

        TransShip.Scripts.jsGetVendorsList(function (data) {
            var vendors = _.sortBy(data, 'company_name');
            model.shipmentsController.vendors(vendors);
        });

    }
};