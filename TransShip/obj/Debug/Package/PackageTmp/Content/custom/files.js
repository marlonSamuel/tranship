console.log("Define notes ");


model.shipFilesController = {

    shipfile: {
        shipmentId: ko.observable(""),
        file_typeId: ko.observable(""),
        shipment_attachmentId: ko.observable(""),
        userId: ko.observable(""),
        description: ko.observable(""),
        creation_date: ko.observable(""),
        file_name: ko.observable(""),
        username: ko.observable(""),
        type: ko.observable(""),
        customerId: ko.observable(""),
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
        estimated_delivery_date: ko.observable(""),
    },

    customer: {
        company_name: ko.observable(""),
        logo: ko.observable(""),
        customerId: ko.observable("")
    },

    files: ko.observableArray([]),
    typesFiles: ko.observableArray([]),
    fileIndex: 0,
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    vendorName: ko.observable(""),

    mapFile: function (shipfile) {
        var eFile = model.shipFilesController.shipfile;
        eFile.shipmentId(shipfile.shipmentId);
        eFile.file_typeId(shipfile.file_typeId);
        eFile.shipment_attachmentId(shipfile.shipment_attachmentId);
        eFile.description(shipfile.description);
        eFile.file_name(shipfile.file_name);
        eFile.creation_date(shipfile.creation_date);
        eFile.userId(shipfile.userId);
        eFile.username(shipfile.username);
        eFile.type(shipfile.type);
        eFile.customerId(shipfile.customerId);
    },

    mapShipment: function (shipment) {
        var eShipment = model.shipFilesController.shipment;
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
        var eCustomer = model.shipFilesController.customer;
        eCustomer.company_name(customer.company_name);
        eCustomer.logo(customer.logo);
        eCustomer.customerId(customer.customerId);
    },

    showNewFile: function () {
        console.log("show new");
        var files = model.shipFilesController.files();
        model.shipFilesController.fileIndex = files.length;
        var file = {
            shipmentId: "",
            file_typeId: "",
            shipment_attachmentId: "",
            userId: "",
            description: "",
            creation_date: "",
            file_name: "",
            username: "",
            type: "",
            customerId: "",
        };



        file.shipmentId = model.notesController.shipment.shipmentId();
        file.userId = model.usersController.user.userId();
        file.customerId = model.usersController.user.customerId();
        model.shipFilesController.mapFile(file);

        model.shipFilesController.insertMode(true);
        model.shipFilesController.gridMode(false);
    },

    save: function () {
        var files = model.shipFilesController.files();
        var filesIndex = model.shipFilesController.filesIndex;
        var shipfile = model.shipFilesController.shipfile;

        var fileParam = ko.toJS(shipfile);

        if (!model.validateForm('#filesEdit')) {
            return;
        }

        var fd = new FormData();

        var files = $('#fileName').get(0).files;

        if (files.length > 0) {
            fd.append("UploadedFile", files[0]);
        }

        fd.append("model.customerId", fileParam.customerId);
        fd.append("model.shipmentId", fileParam.shipmentId);
        fd.append("model.file_typeId", fileParam.file_typeId);
        fd.append("model.userId", fileParam.userId);
        fd.append("model.description", fileParam.description);
        fd.append("model.file_name", fileParam.file_name);

        //call api save
        TransShip.Scripts.jsSaveFile(fileParam, fd, function (data) {
            console.log("done");
            TransShip.Scripts.jsGetFilesList(fileParam.shipmentId, function (data) {
                console.log("done");
                files = data;
                model.shipFilesController.files(files);
            });

            setTimeout(function () {
                model.shipFilesController.insertMode(false);
                model.shipFilesController.gridMode(true);
            }, 2000);
        });
    },

    cancel: function () {
        model.shipFilesController.insertMode(false);
        model.shipFilesController.gridMode(true);

        model.clearErrorMessage("#filesEdit");
    },


    initialize: function (shipmentId) {
        console.log("initialize note controller");
        var selfFiles = this;
        var files = this.files();

        TransShip.Scripts.jsGetOneShipment(shipmentId, function (data) {
            console.log("done note");
            console.log(data);
            model.shipFilesController.mapShipment(data);

            TransShip.Scripts.jsGetVendorsById(data.vendorId, function (data) {
                console.log("done vendor Id");
                model.shipFilesController.vendorName(data.company_name);
            });

            model.approveController.initialize(shipmentId);

            TransShip.Scripts.jsGetCustomersByCustomerId(data.customerId, function (data) {
                console.log(data);
                model.shipFilesController.mapCustomer(data);
            });
        });

        TransShip.Scripts.jsGetFilesList(shipmentId, function (data) {
            console.log("done");
            files = data;
            selfFiles.files(files);

            model.approveController.initialize(shipmentId);
        });

        TransShip.Scripts.jsGetFileList(function (data) {
            console.log("done");
            model.shipFilesController.typesFiles(data);
        });
    }
};

