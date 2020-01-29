console.log("Define rates");


model.ratesController = {

    rate: {
        vendorId: ko.observable(""),
        countryId: ko.observable(""),
        state: ko.observable(""),
        stateId: ko.observable(""),
        service_typeId: ko.observable(""),
        measure_unitId: ko.observable(""),
        price: ko.observable(""),
        status: ko.observable("")
    },
    rates: ko.observableArray([]),
    rateIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(true),
    gridMode: ko.observable(true),
    statusOptions: [{name: 'A', value: 'Active'}, {name: 'I', value: 'Inactive'}],
    countries: ko.observableArray([]),
    states: ko.observableArray([]),
    serviceTypes: ko.observableArray([]),
    measureUnits: ko.observableArray([]),
    validateState: ko.observable(false),

    initObject: function () {
        return {
            vendorId: "",
            countryId: "",
            state: "",
            stateId: "",
            service_typeId: "",
            measure_unitId: "",
            price: "",
            status: ""
        };
    },

    mapping: function (values, koObject) {
        var prop;

        values = values || {};

        for (prop in koObject) {
            if (koObject.hasOwnProperty(prop)) {
                if (typeof values[prop] !== "undefined") {
                    koObject[prop](values[prop]);
                }
            }
        }
    },

    edit: function (rate) {
        var rates = model.ratesController.rates();
        var rateIndex = model.ratesController.rateIndex;
        var eRate = model.ratesController.rate;
        rateIndex = rates.indexOf(rate);
        model.ratesController.mapping(rate, eRate);
        
        console.log("edit");
        model.ratesController.editMode(true);
        model.ratesController.gridMode(true);
        model.ratesController.insertMode(false);

        model.ratesController.rateIndex = rateIndex;
        $('#collapseRate').collapse('show');
    },

    remove: function (rate) {
        
        var rates = model.ratesController.rates();
        var rateIndex = model.ratesController.rateIndex;
        rateIndex = rates.indexOf(rate);
        TransShip.Scripts.jsDeleteRate(rate, function () {
            console.log("done");

            model.ratesController.insertMode(true);
            model.ratesController.editMode(false);
            model.ratesController.gridMode(true);
            rates.splice(rateIndex, 1);
            model.ratesController.rates(rates);

        });
    },

    new: function () {
        console.log("show new");
        var rates = model.ratesController.rates();
        var eRate = model.ratesController.rate;
        var rate = model.ratesController.initObject();
        model.ratesController.mapping(rate, eRate);

        model.ratesController.insertMode(true);
        model.ratesController.editMode(false);
        model.ratesController.gridMode(true);
    },

    cancel: function () {
        console.log("show new");
        var rates = model.ratesController.rates();
        var eRate = model.ratesController.rate;
        var rate = model.ratesController.initObject();
        model.ratesController.mapping(rate, eRate);

        model.ratesController.insertMode(true);
        model.ratesController.editMode(false);
        model.ratesController.gridMode(true);

        $('#collapseRate').collapse('hide');
        model.clearErrorMessage('#ratesFrm');
    },

    update: function () {
        var rates = model.ratesController.rates();
        var rateIndex = model.ratesController.rateIndex;
        var rate = model.ratesController.rate;
        var rateParam = ko.mapping.toJS(rate);

        if (!model.validateForm('#ratesFrm')) {
            return;
        }

        //call api update
        TransShip.Scripts.jsUpdateRate(rateParam, function () {
            console.log("done");
            model.ratesController.insertMode(true);
            model.ratesController.editMode(false);
            model.ratesController.gridMode(true);
            
            var clean = model.ratesController.initObject();
            model.ratesController.mapping(clean, rate);
            model.ratesController.rate = rate;
            
            TransShip.Scripts.jsGetRatesVendorList(rateParam.vendorId, function (data) {
                console.log("Get Rate list of vendor "+ rateParam.vendorId);
                rates = data;
                model.ratesController.rates(rates);
            });

            $('#collapseRate').collapse('hide');
        });
    },

    save: function () {
        var rates = model.ratesController.rates();
        var rateIndex = model.ratesController.rateIndex;
        var rate = model.ratesController.rate;
        var vendorId = model.vendorsController.vendor.vendorId();
        rate.vendorId(vendorId);
        var rateParam = ko.mapping.toJS(model.ratesController.rate);

        if (!model.validateForm('#ratesFrm')) {
            return;
        }
        
        //call api save
        TransShip.Scripts.jsSaveRate(rateParam, function (data) {
            console.log("done");
            model.ratesController.insertMode(true);
            model.ratesController.editMode(false);
            model.ratesController.gridMode(true);
            
            var clean = model.ratesController.initObject();
            model.ratesController.mapping(clean, rate);
            model.ratesController.rate = rate;
            
            TransShip.Scripts.jsGetRatesVendorList(rateParam.vendorId, function (data) {
                console.log("Get Rate list of vendor "+ rateParam.vendorId);
                rates = data;
                model.ratesController.rates(rates);
            });

            $('#collapseRate').collapse('hide');

        });
    },

    countryChange: function (event) {
        
        var countries =  ko.toJS(model.ratesController.countries);
        var countryId = model.ratesController.rate.countryId();
        var countrySelected = $.grep(countries, function (country) {
                return country.CountryID === countryId;
            });
        countrySelected = countrySelected[0];
        
        model.ratesController.validateState(countrySelected.validate_state === 'N'? false: true);
        if (countrySelected.validate_state === 'N') {
            model.ratesController.states([{
                countryId: countryId,
                stateId: 0,
                name: "Any State",
            }]);
            model.ratesController.rate.state("Any State");
        } else {
            TransShip.Scripts.jsGetStateList(countryId, function (data) {
                data.splice(0,0,{
                    countryId: countryId,
                    stateId: 0,
                    name: "Any State",
                });
                model.ratesController.states(data);
            });
        }
    },

    stateChange: function (event) {
        
        var stateId = model.ratesController.rate.stateId();
        if (stateId && stateId !== null) {
            var states =  ko.toJS(model.ratesController.states);
            var stateSelected = $.grep(states, function (state) {
                    return state.stateId === stateId;
                });
            stateSelected = stateSelected[0];
            model.ratesController.rate.state(stateSelected.name);
        }
    },

    serviceTypeChange: function (event) {
        var serviceTypes =  ko.toJS(model.ratesController.serviceTypes);
        var service_typeId = model.ratesController.rate.service_typeId();
        
        TransShip.Scripts.jsGetMeasureList(service_typeId, function (data) {
            model.ratesController.measureUnits(data);
        });
    },

    
    initialize: function (vendorId) {
        console.log("initialize rates controller");
        var selfrates = this;
        var rates = this.rates();
        TransShip.Scripts.jsGetRatesVendorList(vendorId, function (data) {
            console.log("Get Rate list of vendor "+ vendorId);
           //model.ratesController.rates.removeAll();
            rates = data;
            selfrates.rates(rates);
        });
        TransShip.Scripts.jsGetCountryList(function (data) {
            console.log("get countries");
            selfrates.countries(data);
        });
        TransShip.Scripts.jsGetServiceList(function (data) {
            console.log("get service types");
            selfrates.serviceTypes(data);
        });
    }

};

