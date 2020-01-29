console.log("Define users");


model.usersController = {

    user: {
        userId: ko.observable(""),
        customerId: ko.observable(""),
        full_name: ko.observable(""),
        email: ko.observable(""),
        password: ko.observable(""),
        confirmPassword: ko.observable(""),
        user_type: ko.observable(""),
        status: ko.observable(""),
        role: ko.observable(""),
        confirmationKey: ko.observable(""),
        company_name: ko.observable(""),
        address1: ko.observable(""),
        address2: ko.observable(""),
        state: ko.observable(""),
        stateText: ko.observable(""),
        city: ko.observable(""),
        country: ko.observable(""),
        countryId: ko.observable(""),
        zipcode: ko.observable(""),
        logo: ko.observable(""),
        customerId: ko.observable(""),
    },

    users: ko.observableArray([]),
    userIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    datatable: null,
    countries: ko.observableArray([]),
    states: ko.observableArray([]),
    roleOptions: [{ name: 'A', value: 'Administrator' }, { name: 'B', value: 'Operator' }],
    statusOptions: [{ name: 'A', value: 'Active' }, { name: 'I', value: 'Inactive' }],
    validateState: ko.observable(false),
    mandatoryZipcode: ko.observable(false),
    loadingVisible: ko.observable(false),
    isVisible: ko.observable(false),
    validateRol: ko.observable(""),

    edit: function (user) {
        var users = model.usersController.users();
        var userIndex = model.usersController.userIndex;
        var eUser = model.usersController.user;
        userIndex = users.indexOf(user);
        model.usersController.mapping(user, eUser);
        model.usersController.countryChange();
        console.log("edit");
        model.usersController.editMode(true);
        model.usersController.gridMode(false);
        model.usersController.insertMode(false);
        console.log(model.usersController.user);
    },

    mapping: function (user) {
        var eUser = model.usersController.user;
        eUser.email(user.email);
        eUser.full_name(user.full_name);
        eUser.status(user.status);
        eUser.role(user.role);
        eUser.user_type(user.user_type);
        eUser.password(user.password);
        eUser.confirmPassword(user.password);
        eUser.confirmationKey(user.confirmationKey);
        eUser.userId(user.userId);
        eUser.address1(user.address1);
        eUser.address2(user.address2);
        eUser.company_name(user.company_name);
        eUser.state(user.state);
        eUser.stateText(user.state);
        eUser.city(user.city);
        eUser.country(user.country);
        eUser.countryId(user.countryId);
        eUser.zipcode(user.zipcode);
        eUser.customerId(user.customerId);
        eUser.logo(user.logo);
        eUser.customerId(user.customerId);
    },

    isValidUserName: function () {
        return model.isValidEmail(model.usersController.user.email());
    },
    editUser: function (user) {
        console.log("editUser " + user);
        $("#usersBox").removeClass("hidden");
        var selfusers = this;
        var users = this.users();
        //model.usersController.editMode(true);
        var Us = model.usersController.user;
        var users = model.usersController.users();
        model.usersController.userIndex = users.indexOf(user);
        console.log(model.usersController.userIndex);
        //model.countriesController.country = ko.mapping.fromJS(country, model.countriesController.country);
        model.usersController.countryChange();
        TransShip.Scripts.jsGetCountryList(function (data) {
            console.log("done");
            model.usersController.countries(data);
            TransShip.Scripts.jsGetUser(user, function (data) {
                console.log("done");
                Us = data;
                model.usersController.validateRol(Us.role);
                model.usersController.mapping(Us);

                
                    //get users by idCustomer
                    if (Us.role == 'A') {
                        model.usersController.getUserId(Us.customerId);
                    }
                    if (typeof model.contactsController !== "undefined") {
                        //get contacts by idCustomer
                        model.contactsController.initialize(Us.customerId);
                    }
                    if (typeof model.addressController !== "undefined") {
                        //get addresses by idCustomer
                        model.addressController.initialize(Us.customerId);
                    }
                    if (typeof model.passController !== "undefined") {
                        //get infoPass
                        model.passController.initialize(Us.Email, Us.userId);
                    }

                    if (typeof model.billingController !== "undefined") {
                        //get billings
                        model.billingController.initialize(Us.customerId);
                    }
                    if (typeof model.shipmentsController !== "undefined") {
                        //get addresses by idCustomer for shipments initial screen
                        model.shipmentsController.initialize(Us.customerId);
                    }

                    console.log("edit");
                    model.usersController.editMode(false);
                    model.usersController.gridMode(true);
                    model.usersController.insertMode(false);
                    console.log(model.usersController.user);

            });
        });


    },

    remove: function (user) {

        bootbox.confirm("¿Are you sure you want to delete " + user.full_name + "?", function (result) {
            if (result) {
                var users = model.usersController.users();
                model.usersController.userIndex = users.indexOf(user);
                //users.splice(model.usersController.userIndex, 1);
                model.usersController.users(users);
                //call api remove
                TransShip.Scripts.jsDeleteUser(user.userId, function (data) {
                    console.log("done");
                    model.usersController.initialize();
                });
            }
        })

    },

    new: function () {
        console.log("show new");
        var users = model.usersController.users();
        var userIndex = model.usersController.userIndex;
        var user = model.usersController.user;
        userIndex = users.length;
        newUser = {
            userId: "",
            customerId: "",
            full_name: "",
            email: "",
            password: "",
            confirmPassword: "",
            user_type: "T",
            status: "A",
            role: "O",
            confirmationKey: ""
        }
        var customerId = model.usersController.user.customerId();
        if (typeof customerId !== "undefined" && customerId !== null && customerId !== "" && customerId > 0) {
            newUser.customerId = model.usersController.user.customerId();
            newUser.user_type = "C"; 
        }
        model.usersController.mapping(newUser);

        model.usersController.insertMode(true);
        model.usersController.editMode(false);
        model.usersController.gridMode(false);
    },

    update: function () {
        var users = model.usersController.users();
        var userIndex = model.usersController.userIndex;
        var user = model.usersController.user;
        var userParam = ko.toJS(user);

        if (!model.validateForm('#UserEdit') || !model.isValidEmail(user.email())) {
            model.elementCustomError("#userName", "Invalid email ");
            return;
        }
        model.elementClearCustomError("#userName");

        //call api update
        TransShip.Scripts.jsUpdateUser(userParam, function (data) {
            console.log("done");

            /*TransShip.Scripts.jsGetUserListU(userParam.customerId, function (data) {
                console.log("done");
                users = data;
                model.usersController.users(users);

                setTimeout(function () {
                    model.usersController.insertMode(false);
                    model.usersController.editMode(false);
                    model.usersController.gridMode(true);
                }, 2000);

            });*/
            setTimeout(function () {
                location.reload(1);
            }, 2000);
        });
    },

    updateProfile: function () {
        var users = model.usersController.users();
        var userIndex = model.usersController.userIndex;
        var user = model.usersController.user;
        var userParam = ko.toJS(user);

        if (userParam.stateText != undefined || userParam.stateText != null) {
            userParam.state = userParam.stateText;
        }

        if (!model.validateForm('#editProfile')) {
            return;
        }

        //call api update
        TransShip.Scripts.jsUpdateProfile(userParam, function () {
            console.log("done");

            model.usersController.updateLogo(userParam);

            model.usersController.insertMode(false);
            model.usersController.editMode(false);
            model.usersController.gridMode(true);
            setTimeout(function () { window.location.href = '/Customer/Shipments'; }, 2000);
        });
    },

    updateLogo: function (userParam) {
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

        fd.append("model.customerId", userParam.customerId);

        TransShip.Scripts.jsUpdateLogo(userParam.customerId, fd, function () {
            console.log("done");

            model.usersController.insertMode(false);
            model.usersController.editMode(false);
            model.usersController.gridMode(true);
            setTimeout(function () { window.location.href = '/Customer/Customer'; }, 2000);
        });
    },

    PreviewImage: function () {
        var oFReader = new FileReader();
        oFReader.readAsDataURL(document.getElementById("logo").files[0]);

        oFReader.onload = function (oFREvent) {
            document.getElementById("previewLogo").src = oFREvent.target.result;
        };
    },

    save: function () {
        var users = model.usersController.users();
        var userIndex = model.usersController.userIndex;
        var user = model.usersController.user;
        var userParam = ko.toJS(user);

        if (!model.validateForm('#UserEdit') || !model.isValidEmail(user.email())) {
            model.elementCustomError("#userName", "Invalid email ");
            return;
        }
        model.elementClearCustomError("#userName");

        TransShip.Scripts.jsGetUser(userParam.email, function (data) {
            console.log(data);
            if (data.Email != null && data.Email == userParam.email) {
                bootbox.alert("Username already exist, choose another username");
            } else {
                TransShip.Scripts.jsSaveUser(userParam, function (data) {
                    console.log("done");
                    userParam.userId = data.userId;
                    users.push(userParam);
                    model.usersController.users(users);
                    setTimeout(function () {
                        model.usersController.insertMode(false);
                        model.usersController.editMode(false);
                        model.usersController.gridMode(true);
                    }, 2000);
                });
            }
        });
    },

    countryChange: function (event) {
        console.log(model.usersController.user);
        var countries = ko.toJS(model.usersController.countries);
        var countryId = model.usersController.user.countryId();
        var countrySelected = $.grep(countries, function (country) {
            return country.CountryID === countryId;
        });
        countrySelected = countrySelected[0];
        console.log(countrySelected);

        if (countrySelected != null) {
            model.usersController.validateState(countrySelected.validate_state === 'N' ? false : true);
            model.usersController.mandatoryZipcode(countrySelected.mandatory_zipcode === 'N' ? false : true);
            if (countrySelected.validate_state === 'N') {
                model.usersController.states([]);
            } else {
                TransShip.Scripts.jsGetStateList(countryId, function (data) {
                    model.usersController.states(data);
                });
            }
        }

    },

    cancel: function () {
        model.usersController.insertMode(false);
        model.usersController.editMode(false);
        model.usersController.gridMode(true);
        model.elementClearCustomError("#userName");
        model.clearErrorMessage("#UserEdit");
    },

    getUserId: function (customerId) {
        console.log("CustomerId " + customerId + "of User");
        model.usersController.editMode(false);
        model.usersController.gridMode(true);
        if (customerId && customerId !== null && customerId !== "" && customerId > 0) {
            var selfusers = this;
            var users = this.users();
            var users = model.usersController.users();
            users.splice(model.usersController.userIndex, 1);
            model.usersController.users(users);

            TransShip.Scripts.jsGetUserListU(customerId, function (data) {
                console.log("done");
                users = data;
                selfusers.users(users);

            });
        }

    },

    initialize: function () {
        console.log("initialize users controller");
        $("#usersBox").removeClass("hidden");
        var selfrates = this;
        var selfusers = this;
        var users = this.users();
        TransShip.Scripts.jsGetUserList(function (data) {
            console.log("done");
            users = data;
            selfusers.users(users);
        });
    }

};

