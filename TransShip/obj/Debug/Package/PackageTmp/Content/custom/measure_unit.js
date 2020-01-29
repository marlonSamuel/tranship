console.log("Define measure types");


model.measureController = {

    measure: {
        service_typeId: ko.observable(""),
        measure_unitId: ko.observable(""),
        description: ko.observable(""),
        status: ko.observable(""),
    },
    measures: ko.observableArray([]),
    measureIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    viewUnit: ko.observable(false),
    datatable: null,
    statusOptions: [{ name: 'Active', value: 'A' }, { name: 'Inactive', value: 'I' }],

    editMeasure: function (measure) {
        console.log(measure);
        var measures = model.measureController.measures();
        model.measureController.measureIndex = measures.indexOf(measure);
        console.log(model.measureController.measureIndex);
        model.measureController.mapMeasure(measure);

        console.log("edit");
        model.measureController.editMode(true);
        model.measureController.gridMode(false);
        model.measureController.insertMode(false);
        console.log(model.measureController.measure);
    },

    mapMeasure: function (measure) {
        var eMeasure = model.measureController.measure;
        eMeasure.service_typeId(measure.service_typeId);
        eMeasure.measure_unitId(measure.measure_unitId);
        eMeasure.description(measure.description);
        eMeasure.status(measure.status);
    },

    removeMeasure: function (measure) {
        console.log("remove");
        console.log(measure);



        bootbox.confirm("¿Are you sure you want to delete " + measure.description + "?", function (result) {
            if (result) {
                var measures = model.measureController.measures();
                model.measureController.measureIndex = measures.indexOf(measure);
                measures.splice(model.measureController.measureIndex, 1);
                model.measureController.measures(measures);
                //call api remove
                TransShip.Scripts.jsDeleteMeasure(measure.measure_unitId, function (data) {
                    console.log("done");
                });
            }
        })
    },

    showNewMeasure: function () {
        console.log("show new");
        var measures = model.measureController.measures();
        model.measureController.measureIndex = measures.length;
        var measure = {
            service_typeId: "",
            measure_unitId: "",
            description: "",
            status: "",
        };
        measure.service_typeId = model.servicesController.service.service_typeId();
        model.measureController.mapMeasure(measure);

        model.measureController.insertMode(true);
        model.measureController.editMode(false);
        model.measureController.gridMode(false);
    },

    update: function () {
        var datatable = model.measureController.datatable;
        var measures = model.measureController.measures();
        var measureIndex = model.measureController.measureIndex;
        var measure = model.measureController.measure;
        var measureParam = ko.toJS(measure);

        if (!model.validateForm('#MeasureEdit')) {
            return;
        }
        //call api update
        if (TransShip.Scripts) {
            TransShip.Scripts.jsUpdateMeasure(measureParam, function (data) {
                console.log("done");

                measures.splice(measureIndex, 1, measureParam);
                model.measureController.measures(measures);

                setTimeout(function () {
                    model.measureController.insertMode(false);
                    model.measureController.editMode(false);
                    model.measureController.gridMode(true); }, 2000);
            });
        }
    },

    save: function () {
        var measures = model.measureController.measures();
        var measureIndex = model.measureController.measureIndex;
        var measure = model.measureController.measure;
        var measureParam = ko.toJS(measure);
        if (!model.validateForm('#MeasureEdit')) {
            return;
        }
        
        //call api save
        if (TransShip.Scripts) {
            TransShip.Scripts.jsSaveMeasure(measureParam, function (data) {
                console.log("done");
                measureParam.measure_unitId = data.measure_unitId;
                measures.push(measureParam);

                model.measureController.measures(measures);

                setTimeout(function () {
                    model.measureController.insertMode(false);
                    model.measureController.editMode(false);
                    model.measureController.gridMode(true); }, 2000);
            });
        }
    },

    cancel: function () {
        model.measureController.insertMode(false);
        model.measureController.editMode(false);
        model.measureController.gridMode(true);

        model.clearErrorMessage("#MeasureEdit");
    },


    initialize: function (idService) {
        console.log("initialize measures controller");
        var selfMeasures = this;
        var measures = this.measures();

        TransShip.Scripts.jsGetMeasureList(idService, function (data) {
            console.log("done");
            measures = data;
            selfMeasures.measures(measures);
        });

    }
};

