console.log("Define Shiments");


model.shipmentsController = {

    shipment: {
        shipmentId: ko.observable(""),
        customerId: ko.observable(""),
        userId: ko.observable(""),
        countryId: ko.observable(""),
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
        measure_unitId: ko.observable("")
    },

    shipments: ko.observableArray([]), //frontend new shipments (status: N, status: P)
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
    validateState: ko.observable(false),
    mandatoryZipcode: ko.observable(false),
    validateInputs: ko.observable(false),
    validateSelect: ko.observable(true),
    isChecked: ko.observable(false),
    statusOptions: [{ name: 'N', value: 'Saved' }, { name: 'P', value: 'Sent' }],
    incoterms: [{ name: 'FOB', value: 'FOB' }, { name: 'CIF', value: 'CIF' }],
    currentStep: ko.observable(1),

    editShipment: function (shipment) {
        console.log("entra a edit shipment");
        console.log(shipment);
        var shipments = model.shipmentsController.shipments();
        model.shipmentsController.shipmentIndex = shipments.indexOf(shipment);
        console.log(model.shipmentsController.shipmentIndex);
        //model.countriesController.country = ko.mapping.fromJS(country, model.countriesController.country);
        model.shipmentsController.mapShipment(shipment);
        model.shipServicesController.initialize(shipment.shipmentId);
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
        eShipment.userId(shipment.userId);
        eShipment.countryId(shipment.countryId);
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
            userId: "",
            countryId: "",
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
            newAddress: false
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
        if (shipmentParam.newAddress) {
            TransShip.Scripts.jsSaveAddress(shipmentParam, function (data) {
                shipmentParam.addressId = data.addressId;
            });
        }

        //call api update
        TransShip.Scripts.jsUpdateShipment(shipmentParam, function () {
            //setTimeout(function () {
            //    location.reload(1);
            //}, 2000);
        });
    },


    send: function () {
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

        shipmentParam.status = "P";
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
            shipmentParam.shipmentId = data.shipmentId;
            model.shipmentsController.shipment.shipmentId(shipmentParam.shipmentId);
            model.shipServicesController.initialize(shipmentParam.shipmentId);
            model.notesController.initialize(shipmentParam.shipmentId);
            model.shipFilesController.initialize(shipmentParam.shipmentId);
            //model.shipmentsController.editMode(true);
            //model.shipmentsController.gridMode(false);
            //model.shipmentsController.insertMode(false);
        });
    },

    getServicesType: function (event)
    {
        var idType = model.shipmentsController.shipment.service_typeId();
        if (idType && idType !== null && idType !== "") {
            //refresh combo box measures unit (step Container)
            TransShip.Scripts.jsgetOneService(idType, function (data) {
                model.shipmentsController.services(data);
            });
        }
    },

    isValidStepOne: function() {
        var valid = model.validateForm('#destinationFrm');
        if (!valid) {
            toastr.error("Please complete step 1");
        }
        return valid;
    },

    isValidStepTwo: function() {
        var valid = model.validateForm('#informationFrm');
        if (!valid) {
            toastr.error("Please complete step 2");
        }

        return valid; 
    },

    isValidStepThree: function() {
        var valid = true;
        var shipservices = model.shipServicesController.shipservices();
        if (shipservices.length <= 0) {
            toastr.error("Please complete step 3");
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
        if (currentStep < 5) {
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
        location.reload(1);
        model.shipmentsController.insertMode(false);
        model.shipmentsController.editMode(false);
        model.shipmentsController.gridMode(true);
        
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
        console.log(addressId);
        var addressSelected = $.grep(consignees, function (address) {
            return address.addressId === addressId;
        });
        addressSelected = addressSelected[0];
        console.log(addressSelected);
        if (typeof addressSelected !== "undefined" && addressSelected !== null)
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

    initialize: function (idCustomer) {
        console.log("initialize shipment controller");
        var selfShipments = this;
        model.shipmentsController.shipment.service_typeId.subscribe(function () {
            //update combo of measure units
            model.shipmentsController.getServicesType();
        });

        TransShip.Scripts.jsGetShipmentList(idCustomer, function (data) {
            console.log("done");
            var shipments = $.grep(data, function(ship) {
                //return ship.status === "N" || ship.status === "P";
                //Pending Quoted on Backend
                return ship.status === "N";
            });
            selfShipments.shipments(shipments);

            var shipmentsPending = $.grep(data, function(ship) {
                return ship.status === "P" || ship.status === "Q";
            });
            selfShipments.shipmentsPending(shipmentsPending);

            var shipmentsProcess = $.grep(data, function(ship) {
                return ship.status === "A";
            });
            selfShipments.shipmentsProcess(shipmentsProcess);

            var shipmentsHistory = $.grep(data, function(ship) {
                return ship.status === "R" || ship.status === "C" || ship.status === "D";
            });
            selfShipments.shipmentsHistory(shipmentsHistory);

        });
        TransShip.Scripts.jsGetCountryList(function (data) {
            model.shipmentsController.countries(data);
        });

        TransShip.Scripts.jsGetAddressList(idCustomer, function (data) {
            model.shipmentsController.consignees(data);
        });

        TransShip.Scripts.jsGetServiceList(function (data) {
            var containers = $.grep(data, function(cn){
                return cn.user_input === "Y" && cn.status === "A";
            });
            model.shipmentsController.containers(containers);
        });

    }
};