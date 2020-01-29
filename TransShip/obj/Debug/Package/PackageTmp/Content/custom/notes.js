console.log("Define notes ");


model.notesController = {

    shipnote: {
        shipmentId: ko.observable(""),
        shipment_noteId: ko.observable(""),
        userId: ko.observable(""),
        note: ko.observable(""),
        status: ko.observable(""),
        creation_date: ko.observable(""),
        username: ko.observable(""),
        userInSession: ko.observable(""),
    },

    shipment: {
        shipmentId: ko.observable(""),
        consignee_name: ko.observable(""),
        target_company: ko.observable(""),
        shipment_date: ko.observable(""),
        status: ko.observable(""),
        current_tracking_date: ko.observable(""),
        total: ko.observable(""),
        userId: ko.observable(""),
        customerId: ko.observable(""),
        estimated_delivery_date: ko.observable("")
    },

    customer: {
        company_name: ko.observable(""),
        logo: ko.observable(""),
        customerId: ko.observable("")
    },

    notes: ko.observableArray([]),
    noteIndex: 0,
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    datatable: null,
    viewNote: ko.observable(false),
    statusOptions: [{ name: 'Read', value: 'R' }, { name: 'Unread', value: 'U' }],
    vendorName: ko.observable(""),
    

    mapNote: function (shipnote) {
        var eNote = model.notesController.shipnote;
        eNote.shipmentId(shipnote.shipmentId);
        eNote.status(shipnote.status);
        eNote.shipment_noteId(shipnote.shipment_noteId);
        eNote.note(shipnote.note);
        eNote.creation_date(shipnote.creation_date);
        eNote.userId(shipnote.userId);
        eNote.username(shipnote.username);
    },

    mapShipment: function (shipment) {
        var eShipment = model.notesController.shipment;
        eShipment.shipmentId(shipment.shipmentId);
        eShipment.target_company(shipment.target_company);
        eShipment.consignee_name(shipment.consignee_name);
        eShipment.shipment_date(shipment.shipment_date);
        eShipment.current_tracking_date(shipment.current_tracking_date);
        eShipment.status(shipment.status);
        eShipment.total(shipment.total);
        eShipment.userId(shipment.userId);
        eShipment.customerId(shipment.customerId);
        eShipment.estimated_delivery_date(shipment.estimated_delivery_date);
    },

    mapCustomer: function (customer) {
        var eCustomer = model.notesController.customer;
        eCustomer.company_name(customer.company_name);
        eCustomer.logo(customer.logo);
        eCustomer.customerId(customer.customerId);
    },


    showNewNote: function () {
        console.log("show new");
        var notes = model.notesController.notes();
        model.notesController.noteIndex = notes.length;
        var note = {
            shipmentId: "",
            shipment_noteId: "",
            userId: "",
            note: "",
            status: "",
            creation_date: "",
        };

        note.shipmentId = model.notesController.shipment.shipmentId();
        note.userId = model.usersController.user.userId();
        model.notesController.mapNote(note);

        model.notesController.insertMode(true);
        model.notesController.gridMode(false);
    },

    save: function () {
        var notes = model.notesController.notes();
        var notesIndex = model.notesController.notesIndex;
        var shipnote = model.notesController.shipnote;
        
        var noteParam = ko.toJS(shipnote);

        if (!model.validateForm('#notesEdit')) {
            return;
        }
        //call api save
        TransShip.Scripts.jsSaveNote(noteParam, function (data) {
            console.log("done");
            TransShip.Scripts.jsGetNotesList(noteParam.shipmentId, function (data) {
                console.log("done");
                notes = data;
                model.notesController.notes(notes);
            });

            setTimeout(function (note) {
                model.notesController.insertMode(false);
                model.notesController.gridMode(true);
            }, 2000);
        });
    },

    readNote: function (shipnote) {
        console.log(shipnote);
        model.notesController.mapNote(shipnote);
        var userInSession = model.notesController.shipnote.userInSession;
        userInSession = model.usersController.user.userId();
        var shipnote = model.notesController.shipnote;
        var notes = model.notesController.notes();
        var notesIndex = model.notesController.notesIndex;
        var noteParam = ko.toJS(shipnote);

        if (noteParam.userId != userInSession)
        {
            noteParam.status = 'R';
        };

        TransShip.Scripts.jsUpdateNote(noteParam, function () {
            console.log("done");

            TransShip.Scripts.jsGetNotesList(noteParam.shipmentId, function (data) {
                console.log("done");
                notes = data;
                model.notesController.notes(notes);
            });

        });
        model.notesController.insertMode(false);
        model.notesController.gridMode(false);
        model.notesController.viewNote(true);
    },

    cancel: function () {
        model.notesController.viewNote(false);
        model.notesController.insertMode(false);
        model.notesController.gridMode(true);

        model.clearErrorMessage("#notesEdit");
    },


    initialize: function (shipmentId) {
        console.log("initialize note controller");
        var selfNotes = this;
        var notes = this.notes();

        TransShip.Scripts.jsGetOneShipment(shipmentId, function (data) {
            console.log("done note");
            console.log(data);
            model.notesController.mapShipment(data);

            TransShip.Scripts.jsGetVendorsById(data.vendorId, function (data) {
                console.log("done vendor Id");
                model.notesController.vendorName(data.company_name);
            });

            model.approveController.initialize(shipmentId);

            TransShip.Scripts.jsGetCustomersByCustomerId(data.customerId, function (data) {
                console.log(data);
                model.notesController.mapCustomer(data);
            });
        });


        
        TransShip.Scripts.jsGetNotesList(shipmentId, function (data) {
            console.log("done");
            notes = data;
            selfNotes.notes(notes);
        });
    }
};

