console.log("Define vendors");

model.vendorsController = {

    vendor: {
        vendorId: ko.observable(""),
        countryId: ko.observable(""),
        company_name: ko.observable(""),
        stateText: ko.observable(""),
        state: ko.observable(""),
        city: ko.observable(""),
        address1: ko.observable(""),
        address2: ko.observable(""),
        zipcode: ko.observable(""),
        status: ko.observable(""),
        logo: ko.observable("")
    },
    vendors: ko.observableArray([]),
    vendorIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    statusOptions: [{name: 'A', value: 'Active'}, {name: 'I', value: 'Inactive'}],
    countries: ko.observableArray([]),
    states: ko.observableArray([]),
    validateState: ko.observable(false),
    mandatoryZipcode: ko.observable(false),
    isVisible: ko.observable(false),
    logoIsVisible: ko.observable(false),
    
    initObject: function () {
        return {
            vendorId: "",
            countryId: "",
            company_name: "",
            stateText: "",
            state: "",
            city: "",
            address1: "",
            address2: "",
            zipcode: "",
            status: "",
            logo: ""
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

    loadCombosData: function () {
        if (model.vendorsController.countries.length <= 0) { 
            TransShip.Scripts.jsGetCountryList(function (data) { 
                var countries = data; 
                model.vendorsController.countries(countries);
            }); 
        }
    },

    edit: function (vendor) {

        console.log(vendor);
        var vendors = model.vendorsController.vendors();
        var vendorIndex = model.vendorsController.vendorIndex;
        var eVendor = model.vendorsController.vendor;
        vendorIndex = vendors.indexOf(vendor);
        model.vendorsController.mapping(vendor, eVendor);
        eVendor.stateText(vendor.state);
        model.vendorsController.countryChange();
        model.vendorsController.editMode(true);
        model.vendorsController.gridMode(false);
        model.vendorsController.insertMode(false);
        model.vendorContactsController.initialize(vendor.vendorId);
        model.ratesController.initialize(vendor.vendorId);
        model.vendorsController.vendorIndex = vendorIndex;
    },

    

    remove: function (vendor) {

      bootbox.confirm("¿Are you sure you want to delete vendor?", function (result) {
        if (result) {
        
        var vendors = model.vendorsController.vendors();
        var vendorIndex = model.vendorsController.vendorIndex;
        vendorIndex = vendors.indexOf(vendor);
        vendors.splice(vendorIndex, 1);
        var countRates = 0;
        var countContacts = 0;
        //validate if vendor have information (contacts, rates)
        TransShip.Scripts.jsGetRatesVendorList(vendor.vendorId, function (dataRates) {
            countRates = dataRates.length;
            TransShip.Scripts.jsGetVendorContactList(vendor.vendorId, function (dataContacts) {
                countContacts = dataContacts.length;

                if (countRates === 0 && countContacts === 0) {
                    TransShip.Scripts.jsDeleteVendor(vendor.vendorId, function () {
                        console.log("done");

                        model.vendorsController.insertMode(false);
                        model.vendorsController.editMode(false);
                        model.vendorsController.gridMode(true);

                        model.vendorsController.vendors(vendors);

                    });
                } else {
                    bootbox.alert("Cannot delete vendor, has conctacs or prices");
                }
            });
        });

         }
      })
        
    },

    new: function () {
        console.log("show new");
        var vendors = model.vendorsController.vendors();
        var vendorIndex = model.vendorsController.vendorIndex;
        vendorIndex = vendors.length;
        var vendor = model.vendorsController.vendor;
        var clean = model.vendorsController.initObject();
        model.vendorsController.mapping(clean, vendor);
        model.vendorsController.loadCombosData();
        model.vendorsController.insertMode(true);
        model.vendorsController.editMode(false);
        model.vendorsController.gridMode(false);
        var vendorContacts = model.vendorContactsController.vendorContacts();
        var rates = model.ratesController.rates();
        if (vendorContacts.length > 0) {
            model.vendorContactsController.vendorContacts([]);
        }
        if (rates.length > 0) {
            model.ratesController.rates([]);
        }
    },

    update: function () {
        var vendors = model.vendorsController.vendors();
        var vendor = model.vendorsController.vendor;
        var vendorParam = ko.mapping.toJS(vendor);
        if (model.vendorsController.validateState() === false) {
            vendorParam.state = vendorParam.stateText;
        }
        if (!model.validateForm('#vendorFrm')) {
            return;
        }

        //call api update
        TransShip.Scripts.jsUpdateVendor(vendorParam, function () {
            model.vendorsController.uploadLogo(vendorParam);
            setTimeout(function () { location.reload(1); }, 2000);
        });
        
        
    },

    save: function () {
        var vendors = model.vendorsController.vendors();
        var vendor = model.vendorsController.vendor;
        
        var vendorParam = ko.mapping.toJS(model.vendorsController.vendor);
        if (vendorParam.state === "") {
            vendorParam.state = vendorParam.stateText;
        }

        if (!model.validateForm('#vendorFrm')) {
            return;
        }

        //call api save
        TransShip.Scripts.jsSaveVendor(vendorParam, function (data) {

            model.vendorsController.uploadLogo(data);

            vendorParam.vendorId = data.vendorId;
            vendor.vendorId(data.vendorId);
            vendors.push(vendorParam);
            model.vendorsController.vendors(vendors);
            setTimeout(function () { location.reload(1); }, 2000);
        });
    },


    uploadLogo: function (vendorParam) {
        //get file img upload
        var fd = new FormData();

        var l = $('#logo').get(0);

        if (l == undefined || l == null) {
            return;
        }

        var logo = l.files;

        if (logo.length > 0) {
            fd.append("UploadLogo", logo[0]);
        } else {
            return;
        }

        fd.append("model.vendorId", vendorParam.vendorId);

        TransShip.Scripts.jsUpdateLogoVendor(fd, function () {
            console.log("done");
        });
    },


    PreviewImage: function() {
    var oFReader = new FileReader();
    oFReader.readAsDataURL(document.getElementById("logo").files[0]);

    oFReader.onload = function (oFREvent) {
        document.getElementById("previewLogo").src = oFREvent.target.result;
    };
  },


    cancel: function () {
        var vendor = model.vendorsController.vendor;
        var clean = model.vendorsController.initObject();
        model.vendorsController.mapping(clean, vendor);
        /*var vendorContacts = model.vendorContactsController.vendorContacts();
        var rates = model.ratesController.rates();
        if (vendorContacts.length > 0) {
            model.vendorContactsController.vendorContacts([]);
        }
        if (rates.length > 0) {
            model.ratesController.rates([]);
        }
        model.vendorsController.insertMode(false);
        model.vendorsController.editMode(false);
        model.vendorsController.gridMode(true);
        */
        //$("#vendorFrm").data('validator').resetForm();
        setTimeout(function () { location.reload(1); });
    },

    countryChange: function (event) {

        var countries = ko.toJS(model.vendorsController.countries);
        var countryId = model.vendorsController.vendor.countryId();
        if (typeof countryId !== "undefined") {

            var countrySelected = $.grep(countries, function (country) {
                return country.CountryID === countryId;
            });
            countrySelected = countrySelected[0];

            model.vendorsController.validateState(countrySelected.validate_state === 'N' ? false : true);
            model.vendorsController.mandatoryZipcode(countrySelected.mandatory_zipcode === 'N' ? false : true);
            if (countrySelected.validate_state === 'N') {
                model.vendorsController.states([]);
                //model.vendorsController.vendor.state("");
            } else {
                TransShip.Scripts.jsGetStateList(countryId, function (data) {
                    model.vendorsController.states(data);
                });
            }
        }
    },

    

    getCountryName: function(countryId) {
        var countries = model.vendorsController.countries();
        for (i = 0; i < countries.length; i++) {
            if (countries[i].CountryID === countryId) {
                return countries[i].CountryName;
            }
        }
        return "";
    },

    initialize: function () {
        console.log("initialize vendors controller");
        var selfvendors = this;
        var vendors = this.vendors();
        TransShip.Scripts.jsGetCountryList(function (data) { 
            var countries = data; 
            model.vendorsController.countries(countries);
            TransShip.Scripts.jsGetVendorList(function (data) {
                console.log("done vendors");
                vendors = data;
                vendors.forEach(function(vendor) {
                    vendor.country = model.vendorsController.getCountryName(vendor.countryId);
                });
                selfvendors.vendors(vendors);
            });
        });
        
    }

};

