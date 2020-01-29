console.log("Define vendorContacts");


model.vendorContactsController = {

    vendorContact: {
        full_name: ko.observable(""),
        title: ko.observable(""),
        phone_number: ko.observable(""),
        email: ko.observable(""),
        vendorId: ko.observable(""),
        contactId: ko.observable("")
    },
    vendorContacts: ko.observableArray([]),
    vendorContactIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(true),
    gridMode: ko.observable(true),
    statusOptions: [{ name: 'A', value: 'Active' }, { name: 'I', value: 'Inactive' }],
    countriesList: [],
    state: "state",
    outsideus: "",

    initObject: function () {
        return {
            full_name: "",
            title: "",
            phone_number: "",
            email: "",
            vendorId: "",
            contactId: ""
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

    mappingToArray: function (arrayValues, jsonValues) {
        var prop;
        arrayValues = arrayValues || [];
        for (prop in jsonValues) {
            arrayValues.push(jsonValues[prop]);
        }
    },

    edit: function (vendorContact) {
        console.log("edit");
        var vendorContacts = model.vendorContactsController.vendorContacts();
        var vendorContactIndex = model.vendorContactsController.vendorContactIndex;
        var eVendorContact = model.vendorContactsController.vendorContact;
        vendorContactIndex = vendorContacts.indexOf(vendorContact);
        model.vendorContactsController.mapping(vendorContact, eVendorContact);

        model.vendorContactsController.editMode(true);
        model.vendorContactsController.gridMode(true);
        model.vendorContactsController.insertMode(false);

        model.vendorContactsController.vendorContactIndex = vendorContactIndex;
        console.log(vendorContactIndex);
        $('#collapseVendorContact').collapse('show');
    },

    remove: function (vendorContact) {

        var vendorContacts = model.vendorContactsController.vendorContacts();
        var vendorContactIndex = model.vendorContactsController.vendorContactIndex;
        vendorContactIndex = vendorContacts.indexOf(vendorContact);
        bootbox.confirm("Are you sure you want to delete " + vendorContact.full_name + "?", function (result) {
            if (result) {
                TransShip.Scripts.jsDeleteVendorContact(vendorContact, function () {
                    console.log("done");

                    model.vendorContactsController.insertMode(true);
                    model.vendorContactsController.editMode(false);
                    model.vendorContactsController.gridMode(true);
                    vendorContacts.splice(vendorContactIndex, 1);
                    model.vendorContactsController.vendorContacts(vendorContacts);

                });
            }
        });
    },

    new: function () {
        console.log("show new");
        var vendorContacts = model.vendorContactsController.vendorContacts();
        var vendorContactIndex = model.vendorContactsController.vendorContactIndex;
        vendorContactIndex = vendorContacts.length;
        var eVendorContact = model.vendorContactsController.vendorContact;
        var vendorContact = model.vendorContactsController.initObject();
        model.vendorContactsController.mapping(vendorContact, eVendorContact);

        model.vendorContactsController.insertMode(true);
        model.vendorContactsController.editMode(false);
        model.vendorContactsController.gridMode(true);
    },

    cancel: function () {
        console.log("show new");
        var vendorContacts = model.vendorContactsController.vendorContacts();
        var vendorContactIndex = model.vendorContactsController.vendorContactIndex;
        var eVendorContact = model.vendorContactsController.vendorContact;
        var vendorContact = model.vendorContactsController.initObject();
        model.vendorContactsController.mapping(vendorContact, eVendorContact);

        model.vendorContactsController.insertMode(true);
        model.vendorContactsController.editMode(false);
        model.vendorContactsController.gridMode(true);

        $('#collapseVendorContact').collapse('hide');
        model.clearErrorMessage('#vendorContactsFrm');
    },

    update: function () {
        var vendorContacts = model.vendorContactsController.vendorContacts();
        var vendorContactIndex = model.vendorContactsController.vendorContactIndex;
        var vendorContact = model.vendorContactsController.vendorContact;
        var vendorContactParam = ko.toJS(vendorContact);
        if (!model.isValidEmail(vendorContact.email())) {
            model.elementCustomError("#contactEmail", "Invalid email");
            return;
        }
        model.elementClearCustomError("#contactEmail");
        if (!model.validateForm('#vendorContactsFrm')) {
            return;
        }

        //call api update
        TransShip.Scripts.jsUpdateVendorContact(vendorContactParam, function () {
            model.vendorContactsController.insertMode(true);
            model.vendorContactsController.editMode(false);
            model.vendorContactsController.gridMode(true);

            var clean = model.vendorContactsController.initObject();
            model.vendorContactsController.mapping(clean, vendorContact);
            model.vendorContactsController.vendorContact = vendorContact;

            vendorContacts.splice(vendorContactIndex, 1, vendorContactParam);
            model.vendorContactsController.vendorContacts(vendorContacts);
            $('#collapseVendorContact').collapse('hide');
        });
    },

    save: function () {
        var vendorContacts = model.vendorContactsController.vendorContacts();
        var vendorContactIndex = model.vendorContactsController.vendorContactIndex;
        var vendorContact = model.vendorContactsController.vendorContact;
        var vendorId = model.vendorsController.vendor.vendorId();
        vendorContact.vendorId(vendorId);
        //vendorContacts.push(vendorContact);
        if (!model.isValidEmail(vendorContact.email())) {
            model.elementCustomError("#contactEmail", "Invalid email");
            return;
        }
        model.elementClearCustomError("#contactEmail");
        var vendorContactParam = ko.toJS(model.vendorContactsController.vendorContact);
        if (!model.validateForm('#vendorContactsFrm')) {
            return;
        }
        //call api save
        TransShip.Scripts.jsSaveVendorContact(vendorContactParam, function (data) {
            console.log("done");
            model.vendorContactsController.insertMode(true);
            model.vendorContactsController.editMode(false);
            model.vendorContactsController.gridMode(true);
            
            vendorContactParam.contactId = data.contactId;
            vendorContact.contactId(data.contactId);
            vendorContacts.push(vendorContactParam);
            model.vendorContactsController.vendorContacts(vendorContacts);
            var clean = model.vendorContactsController.initObject();
            model.vendorContactsController.mapping(clean, vendorContact);
            $('#collapseVendorContact').collapse('hide');
        });
    },

    initialize: function (vendorId) {
        console.log("initialize vendorContacts controller");
        var selfvendorContacts = this;
        var vendorContacts = this.vendorContacts();
        TransShip.Scripts.jsGetVendorContactList(vendorId, function (data) {
            console.log("get contacts of vendor " + vendorId);
            //model.vendorContactsController.vendorContacts.removeAll();
            vendorContacts = data;
            selfvendorContacts.vendorContacts(vendorContacts);
        });
    }

};

