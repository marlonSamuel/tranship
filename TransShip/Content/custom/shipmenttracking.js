console.log("Define shipment tracking");


model.shipTrackController = {

    track: {
        shipmentId: ko.observable(""),
        trackingId: ko.observable(""),
        userId: ko.observable("").extend({ required: true }),
        username: ko.observable(""),
        status: ko.observable(""),
        comentaries: ko.observable(""),
        creation_date: ko.observable(""),
    },
    tracks: ko.observableArray([]),
    trackIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),

    editTrack: function (track) {
        console.log(track);
        var tracks = model.shipTrackController.tracks();
        model.shipTrackController.trackIndex = tracks.indexOf(track);
        console.log(model.shipTrackController.trackIndex);
        model.shipTrackController.mapTrack(track);

        console.log("edit");
        model.shipTrackController.editMode(true);
        model.shipTrackController.gridMode(false);
        model.shipTrackController.insertMode(false);
        console.log(model.shipTrackController.track);
    },

    mapTrack: function (track) {
        var eTrack = model.shipTrackController.track;
        eTrack.customerId(contact.customerId);
        eTrack.contactId(contact.contactId);
        eTrack.full_name(contact.full_name);
        eTrack.title(contact.title);
        eTrack.phone_number(contact.phone_number);
        eTrack.email(contact.email);
    },

    removeContact: function (contact) {
        console.log("remove");
        console.log(contact);



        bootbox.confirm("¿Are you sure you want to delete " + contact.full_name + "?", function (result) {
            if (result) {
                var contacts = model.contactsController.contacts();
                model.contactsController.contactIndex = contacts.indexOf(contact);
                contacts.splice(model.contactsController.contactIndex, 1);
                model.contactsController.contacts(contacts);
                //call api remove
                TransShip.Scripts.jsDeleteContact(contact.contactId, function (data) {
                    console.log("done");
                });
            }
        })
    },

    showNewContact: function () {
        console.log("show new");
        var contacts = model.contactsController.contacts();
        model.contactsController.contactIndex = contacts.length;
        var contact = {
            customerId: "",
            contactId: "",
            full_name: "",
            title: "",
            phone_number: "",
            email: "",
        };
        contact.customerId = model.usersController.user.customerId();
        model.contactsController.mapContact(contact);

        model.contactsController.insertMode(true);
        model.contactsController.editMode(false);
        model.contactsController.gridMode(false);
    },

    update: function () {
        var datatable = model.contactsController.datatable;
        var contacts = model.contactsController.contacts();
        var contactIndex = model.contactsController.contactIndex;
        var contact = model.contactsController.contact;
        var contactParam = ko.mapping.toJS(model.contactsController.contact);

        if (!model.validateForm('#ContactEdit')) {
            return;
        }
        //call api update
        TransShip.Scripts.jsUpdateContact(contactParam, function () {
            console.log("done");

            contacts.splice(contactIndex, 1, contactParam);
            model.contactsController.contacts(contacts);

            setTimeout(function () {
                model.contactsController.insertMode(false);
                model.contactsController.editMode(false);
                model.contactsController.gridMode(true);
            }, 2000);
        });
    },

    save: function () {
        var contacts = model.contactsController.contacts();
        var contactIndex = model.contactsController.contactIndex;
        var contact = model.contactsController.contact;
        var contactParam = ko.toJS(contact);

        if (!model.validateForm('#ContactEdit')) {
            return;
        }
        //call api save
        TransShip.Scripts.jsSaveContact(contactParam, function (data) {
            console.log("done");
            contactParam.contactId = data.contactId;
            contacts.push(contactParam);
            model.contactsController.contacts(contacts);
            setTimeout(function () {
                model.contactsController.insertMode(false);
                model.contactsController.editMode(false);
                model.contactsController.gridMode(true);
            }, 2000);
        });
    },

    cancel: function () {
        model.contactsController.insertMode(false);
        model.contactsController.editMode(false);
        model.contactsController.gridMode(true);

        model.clearErrorMessage('#ContactEdit');
    },

    validateForm: function (form) {
        return $(form).validation() && $(form).validation('isValid');
    },

    initialize: function (idCustomer) {
        console.log("initialize contact controller");
        var selfContacts = this;
        var contacts = this.contacts();
        if (contacts.length <= 0) {
            TransShip.Scripts.jsGetContactList(idCustomer, function (data) {
                console.log("done");
                contacts = data;
                selfContacts.contacts(contacts);
            });
        }

    }

};

