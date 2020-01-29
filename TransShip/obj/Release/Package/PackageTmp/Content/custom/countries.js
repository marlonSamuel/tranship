console.log("Define Countries");


model.countriesController = {

    country: {
        ISO2: ko.observable(""),
        CountryName: ko.observable(""),
        LongCountryName: ko.observable(""),
        ISO3: ko.observable(""),
        NumCode: ko.observable(""),
        UNMemberState: ko.observable(""),
        CallingCode: ko.observable(""),
        CCTLD: ko.observable(""),
        CountryID: ko.observable(""),
        currency: ko.observable(""),
        currencySymbol: ko.observable(""),
        status: ko.observable(""),
        validate_state: ko.observable(""),
        mandatory_zipcode: ko.observable("")
    },
    countries: ko.observableArray([]),
    countryIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    datatable: null,
    statusOptions: [{ name: 'Active', value: 'A' }, { name: 'Inactive', value: 'I' }],
    validateStateOptions: [{ name: 'No', value: 'N' }, { name: 'Yes', value: 'Y' }],
    mandatoryZipcodeOptions: [{ name: 'No', value: 'N' }, { name: 'Yes', value: 'Y' }],

    editCountry: function (country) {
        console.log(country);
        var countries = model.countriesController.countries();
        model.countriesController.countryIndex = countries.indexOf(country);
        console.log(model.countriesController.countryIndex);
        model.countriesController.mapCountry(country);
        
        console.log("edit");
        model.countriesController.editMode(true);
        model.countriesController.gridMode(false);
        model.countriesController.insertMode(false);
        console.log(model.countriesController.country);

        model.countriesController.getStates(country);
    },

    mapCountry: function (country) {
        var eCountry = model.countriesController.country;
        eCountry.CountryName(country.CountryName);
        eCountry.LongCountryName(country.LongCountryName);
        eCountry.ISO2(country.ISO2);
        eCountry.ISO3(country.ISO3);
        eCountry.NumCode(country.NumCode);
        eCountry.UNMemberState(country.UNMemberState);
        eCountry.CallingCode(country.CallingCode);
        eCountry.CCTLD(country.CCTLD);
        eCountry.CountryID(country.CountryID);
        eCountry.currency(country.currency);
        eCountry.currencySymbol(country.currencySymbol);
        eCountry.status(country.status);
        eCountry.validate_state(country.validate_state);
        eCountry.mandatory_zipcode(country.mandatory_zipcode);
    },

    removeCountry: function (country) {
        console.log("remove");
        console.log(country);

        

        bootbox.confirm("¿Are you sure you want to delete " + country.CountryName + "?", function (result) {
            if (result) {
                var countries = model.countriesController.countries();
                model.countriesController.countryIndex = countries.indexOf(country);
                countries.splice(model.countriesController.countryIndex, 1);
                model.countriesController.countries(countries);
                //call api remove
                TransShip.Scripts.jsDeleteCountry(country.CountryID, function (data) {
                    console.log("done");

                });
            }
        });
    },

    showNewCountry: function () {
        console.log("show new");
        var countries = model.countriesController.countries();
        model.countriesController.countryIndex = countries.length;
        var country = {
            ISO2: "",
            Name: "",
            LongCountryName: "",
            ISO3: "",
            NumCode: "",
            UNMemberState: "",
            CallingCode: "",
            CCTLD: "",
            CountryID: "",
            validate_state: "N",
            mandatory_zipcode: "N"
        };
        model.countriesController.mapCountry(country);

        model.countriesController.insertMode(true);
        model.countriesController.editMode(false);
        model.countriesController.gridMode(false);
    },

    getStates(country) {
        console.log("entry getStates");
        console.log(country);
        model.countriesController.mapCountry(country);
        model.statesController.initialize(country.CountryID, null);
        model.statesController.viewUnit(true);
    },

    update: function () {
        var datatable = model.countriesController.datatable;
        var countries = model.countriesController.countries();
        var countryIndex = model.countriesController.countryIndex;
        var country = model.countriesController.country;
        var countryParam = ko.mapping.toJS(model.countriesController.country);

        if (!model.validateForm('#CountryEdit')) {
            return;
        }
        //call api update
        TransShip.Scripts.jsUpdateCountry(countryParam, function () {
            console.log("done");

            countries.splice(countryIndex, 1, countryParam);
            model.countriesController.countries(countries);

            setTimeout(function () {
                model.countriesController.insertMode(false);
                model.countriesController.editMode(false);
                model.statesController.viewUnit(false);
                model.countriesController.gridMode(true);
            }, 2000);
        });
    },

    save: function () {
        var countries = model.countriesController.countries();
        var countryIndex = model.countriesController.countryIndex;
        var country = model.countriesController.country;
        var countryParam = ko.toJS(country);
        if (!model.validateForm('#CountryEdit')) {
            return;
        }
        //call api save
        TransShip.Scripts.jsSaveCountry(countryParam, function (data) {
            console.log("done");
            countryParam.CountryID = data.CountryID;
            countries.push(countryParam);
            model.countriesController.countries(countries);
            setTimeout(function () {
                model.countriesController.insertMode(false);
                model.countriesController.editMode(false);
                model.statesController.viewUnit(false);
                model.countriesController.gridMode(true);
            }, 2000);
            
        });
    },

    cancel: function () {
        model.countriesController.insertMode(false);
        model.countriesController.editMode(false);
        model.statesController.viewUnit(false);
        model.countriesController.gridMode(true);

        model.clearErrorMessage('#CountryEdit');
    },


    initialize: function () {
        console.log("initialize countries controller");
        var selfCountries = this;
        var countries = this.countries();
        if (countries.length <= 0) {
            TransShip.Scripts.jsGetCountryList(function (data) {
                console.log("done");
                countries = data;
                selfCountries.countries(countries);
            });
        }
       
    }

};

