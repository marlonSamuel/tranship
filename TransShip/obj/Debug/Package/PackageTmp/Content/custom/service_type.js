console.log("Define service type");


model.servicesController = {

    service: {
        descripcion: ko.observable(""),
        user_input: ko.observable(""),
        status: ko.observable(""),
        service_typeId: ko.observable("")
    },
    services: ko.observableArray([]),
    serviceIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    datatable: null,
    statusOptions: [{ name: 'Active', value: 'A' }, { name: 'Inactive', value: 'I' }],
    userInputOptions: [{ name: 'Yes', value: 'Y' }, { name: 'No', value: 'N' }],

    editService: function (service) {
        console.log(service);
        var services = model.servicesController.services();
        model.servicesController.serviceIndex = services.indexOf(service);
        console.log(model.servicesController.serviceIndex);
        //model.countriesController.country = ko.mapping.fromJS(country, model.countriesController.country);
        model.servicesController.mapService(service);

        console.log("edit");
        model.servicesController.editMode(true);
        model.servicesController.gridMode(false);
        model.servicesController.insertMode(false);
        console.log(model.servicesController.service);
    },

    mapService: function (service) {
        var eService = model.servicesController.service;
        eService.descripcion(service.descripcion);
        eService.user_input(service.user_input);
        eService.status(service.status);
        eService.service_typeId(service.service_typeId);
    },

    removeService: function (service) {
        console.log("remove");
        console.log(service);



        bootbox.confirm("¿Are you sure you want to delete " + service.descripcion + "?", function (result) {
            if (result) {
                var services = model.servicesController.services();
                model.servicesController.serviceIndex = services.indexOf(service);
                services.splice(model.servicesController.serviceIndex, 1);
                model.servicesController.services(services);
                //call api remove
                TransShip.Scripts.jsDeleteService(service.service_typeId, function (data) {
                    console.log("done");
                });
            }
        })
    },

    showNewService: function () {
        console.log("show new");
        var services = model.servicesController.services();
        model.servicesController.serviceIndex = services.length;
        var service = {
            description: "",
            user_imput: "",
            status: "",
            service_typeId: ""
        };
        model.servicesController.mapService(service);

        model.servicesController.insertMode(true);
        model.servicesController.editMode(false);
        model.servicesController.gridMode(false);
    },
  
    getMeasure(service) {
        console.log("entry getmeasure");
        console.log(service);
        model.servicesController.mapService(service);
        model.measureController.initialize(service.service_typeId, null);
        model.measureController.viewUnit(true);
    },


    update: function () {
        var datatable = model.servicesController.datatable;
        var services = model.servicesController.services();
        var serviceIndex = model.servicesController.serviceIndex;
        var service = model.servicesController.service;
        var serviceParam = ko.mapping.toJS(model.servicesController.service);

        if (!model.validateForm('#ServiceEdit')) {
            return;
        }
        //call api update
        TransShip.Scripts.jsUpdateService(serviceParam, function () {
            console.log("done");

            services.splice(serviceIndex, 1, serviceParam);
            model.servicesController.services(services);

            setTimeout(function () {
                model.servicesController.insertMode(false);
                model.servicesController.editMode(false);
                model.servicesController.gridMode(true); }, 3000);
        });
    },

    save: function () {
        var services = model.servicesController.services();
        var serviceIndex = model.servicesController.servicesIndex;
        var service = model.servicesController.service;
        var serviceParam = ko.toJS(service);

        if (!model.validateForm('#ServiceEdit')) {
            return;
        }
        
        //call api save
        TransShip.Scripts.jsSaveService(serviceParam, function (data) {
            console.log("done");
            serviceParam.service_typeId = data.service_typeId;
            services.push(serviceParam);
            model.servicesController.services(services);
            //toastr.info("type file added");
            setTimeout(function () {
                model.servicesController.insertMode(false);
                model.servicesController.editMode(false);
                model.servicesController.gridMode(true);  }, 2000);
        });
    },

    cancel: function () {
        model.servicesController.insertMode(false);
        model.servicesController.editMode(false);
        model.servicesController.gridMode(true);

        model.clearErrorMessage("#ServiceEdit");
    },


    initialize: function () {
        console.log("initialize files controller");
        var selfServices = this;
        var services = this.services();
        if (services.length <= 0) {
            TransShip.Scripts.jsGetServiceList(function (data) {
                console.log("done");
                services = data;
                selfServices.services(services);
            });
        }

    }

};

