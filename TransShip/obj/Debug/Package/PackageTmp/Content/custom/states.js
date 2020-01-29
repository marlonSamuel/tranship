console.log("Define states");


model.statesController = {

    state: {
        countryId: ko.observable(""),
        stateId: ko.observable(""),
        name: ko.observable(""),
    },
    states: ko.observableArray([]),
    stateIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    viewUnit: ko.observable(false),
    disabledId: ko.observable(true),
    datatable: null,

    editState: function (state) {
        console.log(state);
        model.statesController.disabledId(true);
        var states = model.statesController.states();
        model.statesController.stateIndex = states.indexOf(state);
        console.log(model.statesController.stateIndex);
        //model.countriesController.country = ko.mapping.fromJS(country, model.countriesController.country);
        model.statesController.mapState(state);

        console.log("edit");
        model.statesController.editMode(true);
        model.statesController.gridMode(false);
        model.statesController.insertMode(false);
        console.log(model.statesController.measure);
    },

    mapState: function (state) {
        var eState = model.statesController.state;
        eState.countryId(state.countryId);
        eState.stateId(state.stateId);
        eState.name(state.name);
    },

    removeState: function (state) {
        console.log("remove");
        console.log(state);



        bootbox.confirm("¿Are you sure you want to delete " + state.name + "?", function (result) {
            if (result) {
                var states = model.statesController.states();
                model.statesController.stateIndex = states.indexOf(state);
                states.splice(model.statesController.stateIndex, 1);
                model.statesController.states(states);
                //call api remove
                TransShip.Scripts.jsDeleteStates(state.stateId, function (data) {
                    console.log("done");
                    
                });
            }
        })
    },

    showNewState: function () {
        console.log("show new");
        model.statesController.disabledId(false);
        var states = model.statesController.states();
        model.statesController.stateIndex = states.length;
        var state = {
            countryId: "",
            stateId: "",
            name: "",
        };
        state.countryId = model.countriesController.country.CountryID();
        model.statesController.mapState(state);

        model.statesController.insertMode(true);
        model.statesController.editMode(false);
        model.statesController.gridMode(false);
    },

    update: function () {
        var datatable = model.statesController.datatable;
        var states = model.statesController.states();
        var stateIndex = model.statesController.stateIndex;
        var state = model.statesController.state;
        var stateParam = ko.toJS(state);

        if (!model.validateForm('#StateEdit')) {
            return;
        }
        //call api update
        if (TransShip.Scripts) {
            TransShip.Scripts.jsUpdateState(stateParam, function (data) {
                console.log("done");

                states.splice(stateIndex, 1, stateParam);
                model.statesController.states(states);

                model.statesController.insertMode(false);
                model.statesController.editMode(false);
                model.statesController.gridMode(true);
                //toastr.info("Country updated");
                //window.location.reload();
                
            });
        }
    },

    save: function () {
        var datatable = model.statesController.datatable;
        var states = model.statesController.states();
        var stateIndex = model.statesController.stateIndex;
        var state = model.statesController.state;
        var stateParam = ko.toJS(state);
        if (!model.validateForm('#StateEdit')) {
            return;
        }
        //call api save
        if (TransShip.Scripts) {
            TransShip.Scripts.jsSaveState(stateParam, function (data) {
                console.log("done");
                stateParam.stateId = data.stateId;
                states.push(stateParam);
                model.statesController.states(states);

                model.statesController.insertMode(false);
                model.statesController.editMode(false);
                model.statesController.gridMode(true);
                
            });
        }
    },

    cancel: function () {
        model.statesController.insertMode(false);
        model.statesController.editMode(false);
        model.statesController.gridMode(true);

        model.clearErrorMessage("#StateEdit");
    },

    initialize: function (idCountry) {
        console.log("initialize states controller");
        var selfStates = this;
        var states = this.states();

        TransShip.Scripts.jsGetStateList(idCountry, function (data) {
            console.log("done");
            states = data;
            selfStates.states(states);
            
        });

    }
    
};

