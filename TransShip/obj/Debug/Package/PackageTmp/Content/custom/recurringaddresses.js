console.log("Define Recurring addreses");


model.addressController = {

    address: {
        customerId: ko.observable(""),
        addressId: ko.observable(""),
        countryId: ko.observable(""),
        Country_Name: ko.observable(""),
        consignee_name: ko.observable(""),
        target_company: ko.observable(""),
        phone_number: ko.observable(""),
        state: ko.observable(""),
        stateText: ko.observable(""),
        city: ko.observable(""),
        address1: ko.observable(""),
        address2: ko.observable(""),
        zipcode: ko.observable(""),
    },
    addresses: ko.observableArray([]),
    addressIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    datatable: null,
    validateState: ko.observable(false),
    mandatoryZipcode: ko.observable(false),
    countries: ko.observableArray([]),
    states: ko.observableArray([]),

    editAddress: function (address) {
        console.log(address);
        var addresses = model.addressController.addresses();
        model.addressController.addressIndex = addresses.indexOf(address);
        console.log(model.addressController.addressIndex);
        //model.countriesController.country = ko.mapping.fromJS(country, model.countriesController.country);
        model.addressController.mapAddress(address);

        console.log("edit");
        model.addressController.editMode(true);
        model.addressController.gridMode(false);
        model.addressController.insertMode(false);
        console.log(model.addressController.address);
    },

    mapAddress: function (address) {
        var eAddress = model.addressController.address;
        eAddress.customerId(address.customerId);
        eAddress.addressId(address.addressId);
        eAddress.countryId(address.countryId);
        eAddress.Country_Name(address.Country_Name);
        eAddress.consignee_name(address.consignee_name);
        eAddress.target_company(address.target_company);
        eAddress.phone_number(address.phone_number);
        eAddress.state(address.state);
        eAddress.stateText(address.state);
        eAddress.city(address.city);
        eAddress.address1(address.address1);
        eAddress.address2(address.address2);
        eAddress.zipcode(address.zipcode);
    },

    removeAddress: function (address) {
        console.log("remove");
        console.log(address);



        bootbox.confirm("¿Are you sure you want to delete this register?", function (result) {
            if (result) {
                var addresses = model.addressController.addresses();
                model.addressController.addressIndex = addresses.indexOf(address);
                addresses.splice(model.addressController.addressIndex, 1);
                model.addressController.addresses(addresses);
                //call api remove
                TransShip.Scripts.jsDeleteAddress(address.addressId, function (data) {
                    console.log("done");
                });
            }
        })
    },

    showNewAddress: function () {
        console.log("show new");
        var addresses = model.addressController.addresses();
        model.addressController.addressIndex = addresses.length;
        var address = {
            customerId:"",
            addressId: "",
            countryId: "",
            Country_Name: "",
            consignee_name: "",
            target_company: "",
            phone_number: "",
            state: "",
            city: "",
            address1: "",
            address2: "",
            zipcode: "",
        };
        address.customerId = model.usersController.user.customerId();
        model.addressController.mapAddress(address);

        model.addressController.insertMode(true);
        model.addressController.editMode(false);
        model.addressController.gridMode(false);
    },

    update: function () {
        var datatable = model.addressController.datatable;
        var addresses = model.addressController.addresses();
        var addressIndex = model.addressController.addressIndex;
        var address = model.addressController.address;
        var addressParam = ko.toJS(address);

        if (!model.validateForm('#AddressEdit')) {
            return;
        }

        if (addressParam.stateText != undefined || addressParam.stateText != null) {
            addressParam.state = addressParam.stateText;
        }
        //call api update
        TransShip.Scripts.jsUpdateAddress(addressParam, function () {
            console.log("done");

            TransShip.Scripts.jsGetAddressList(addressParam.customerId, function (data) {
                console.log("done");
                addresses = data;
                model.addressController.addresses(addresses);

                setTimeout(function () {
                    model.addressController.insertMode(false);
                    model.addressController.editMode(false);
                    model.addressController.gridMode(true);
                }, 2000);
            });
        });
    },

    save: function () {
        var addresses = model.addressController.addresses();
        var addressIndex = model.addressController.addressIndex;
        var address = model.addressController.address;
        var addressParam = ko.toJS(address);

        if (!model.validateForm('#AddressEdit')) {
            return;
        }

        if (addressParam.stateText != undefined || addressParam.stateText != null) {
            addressParam.state = addressParam.stateText;
        }
        //call api save
        TransShip.Scripts.jsSaveAddress(addressParam, function (data) {
            console.log("done");
            TransShip.Scripts.jsGetAddressList(addressParam.customerId, function (data) {
                console.log("done");
                addresses = data;
                model.addressController.addresses(addresses);

                setTimeout(function () {
                    model.addressController.insertMode(false);
                    model.addressController.editMode(false);
                    model.addressController.gridMode(true);
                }, 2000);

            });
        });
    },

    countryChange: function (event) {
        console.log(model.addressController.address);
        var countries = ko.toJS(model.addressController.countries);
        var countryId = model.addressController.address.countryId();
        var countrySelected = $.grep(countries, function (country) {
            return country.CountryID === countryId;
        });
        countrySelected = countrySelected[0];
        console.log(countrySelected);

        if (countrySelected != null) {
            model.addressController.validateState(countrySelected.validate_state === 'N' ? false : true);
            model.addressController.mandatoryZipcode(countrySelected.mandatory_zipcode === 'N' ? false : true);
            if (countrySelected.validate_state === 'N') {
                model.addressController.states([]);
            } else {
                TransShip.Scripts.jsGetStateList(countryId, function (data) {
                    model.addressController.states(data);
                });
            }
        }

    },

    cancel: function () {
        model.addressController.insertMode(false);
        model.addressController.editMode(false);
        model.addressController.gridMode(true);

        model.clearErrorMessage("#AddressEdit");
    },


    initialize: function (idCustomer) {
        console.log("initialize address controller");
        var selfAddresses = this;
        var addresses = this.addresses();
        if (addresses.length <= 0) {
            TransShip.Scripts.jsGetAddressList(idCustomer, function (data) {
                console.log("done");
                addresses = data;
                selfAddresses.addresses(addresses);
                
            });

            TransShip.Scripts.jsGetCountryList(function (data) {
                console.log("done");
                model.addressController.countries(data);
            });
        }

    }
};

