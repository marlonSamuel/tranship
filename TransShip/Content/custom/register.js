console.log("Define Register");

model.register = {
    customerId: ko.observable(""),
    userId: ko.observable(""),
    username: ko.observable(""),
    keyinformation: ko.observable(""),
    confirmkey: ko.observable(""),
    companyname: ko.observable(""),
    addr1: ko.observable(""),
    addr2: ko.observable(""),
    zipcode: ko.observable(""),

    city: ko.observable(""),
    idCountry: ko.observable(""),
    canadaprovince: ko.observable(""),
    usstate: ko.observable(""),
    outsideus: ko.observable(""),

    //contact information
    fullname: ko.observable(""),
    title: ko.observable(""),
    phonenumber: ko.observable(""),
    // email: ko.observable(""),

    membership: ko.observable(""),

    countries: ko.observableArray([]),
    states: ko.observableArray([]),
    membershipLevels: ko.observableArray([]),
    registerResult: ko.observable(""),
    state: ko.observable("state"),
    stateText: ko.observable(""),
    validateState: ko.observable(false),
    mandatoryZipcode: ko.observable(false),

    initialize: function () {
        console.log("initialize register");
        var selfRegister = this;
        var countries = this.countries();
        var membershipLevels = this.membershipLevels();
        if (countries.length <= 0) {

            TransShip.Scripts.jsGetCountryList(function (data) {
                countries = data;
                selfRegister.countries(countries);
            });
        }
        if (membershipLevels.length <= 0) {
            TransShip.Scripts.jsGetMembershipLevel(function (data) {
                membershipLevels = data;
                selfRegister.membershipLevels(membershipLevels);
            });
        }

    },
    changePassword: function () {
        var x = document.getElementById("confirmkey").value;
        TransShip.Scripts.changePassword(x);
    },
    valid: function () {
        return false;
    },
    jsCountryChange: function () {
        var x = document.getElementById("country").value;
        if (x) {
            switch (x) {
                case '236':
                    model.register.state("us");
                    break;
                case '40':
                    model.register.state("ca");
                    break;
                default:
                    model.register.state("state");
            }
        }
        else {

        }

    },
    sendRegister: function () {

        if (!model.validateForm('#registerForm') || !model.isValidEmail(model.register.username())) {
            model.elementCustomError("#username", "Invalid email ");
            return;
        }
        model.elementClearCustomError("#userName");
        //api call
        var stateText = model.register.stateText();

        if (stateText != undefined || stateText != null || stateText != null)
        {
            model.register.usstate = stateText;
        }

        var email = model.register.username();

        TransShip.Scripts.jsGetUser(email, function (data) {
            console.log("done");
            if (email != null && email == data.Email) {
                bootbox.alert("Username already exist, choose another username");
            } else {
                TransShip.Scripts.sendRegister();
            }
        });
    },

    countryChange: function (event) {

        var countries = ko.toJS(model.register.countries);
        var countryId = model.register.idCountry();
        var countrySelected = $.grep(countries, function (country) {
            return country.CountryID === countryId;
        });
        countrySelected = countrySelected[0];

        if (countrySelected != null) {
            model.register.validateState(countrySelected.validate_state === 'N' ? false : true);
            model.register.mandatoryZipcode(countrySelected.mandatory_zipcode === 'N' ? false : true);
            if (countrySelected.validate_state === 'N') {
                model.register.states([]);
            } else {
                TransShip.Scripts.jsGetStateList(countryId, function (data) {
                    model.register.states(data);
                });
            }
        }

    },

    approveCustomer: function (customerId, userId, action) {
        if (customerId == null || customerId == "" || userId == null || userId == "") {
            return;
        }

        if (action == 'A') {
            TransShip.Scripts.jsapproveCustomer(customerId, userId, function (data) {
                console.log("done");
            });
        } else {
            TransShip.Scripts.jsrejectCustomer(customerId, userId, function (data) {
                console.log("done");
            });
        }
    }
};


