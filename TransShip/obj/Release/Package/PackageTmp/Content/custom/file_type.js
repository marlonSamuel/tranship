console.log("Define type_files");


model.filesController = {

    file: {
        type: ko.observable(""),
        description: ko.observable(""),
        status: ko.observable(""),
        file_typeId: ko.observable("")
    },
    files: ko.observableArray([]),
    fileIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    datatable: null,
    statusOptions: [{ name: 'Active', value: 'A' }, { name: 'Inactive', value: 'I' } ],

    editFile: function (file) {
        console.log(file);
        var files = model.filesController.files();
        model.filesController.fileIndex = files.indexOf(file);
        console.log(model.filesController.fileIndex);
        model.filesController.mapFile(file);

        console.log("edit");
        model.filesController.editMode(true);
        model.filesController.gridMode(false);
        model.filesController.insertMode(false);
        console.log(model.filesController.file);
    },

    mapFile: function (file) {
        var eFile = model.filesController.file;
        eFile.type(file.type);
        eFile.description(file.description);
        eFile.status(file.status);
        eFile.file_typeId(file.file_typeId);
    },

    removeFile: function (file) {
        console.log("remove");
        console.log(file);



        bootbox.confirm("¿Are you sure you want to delete " + file.description + "?", function (result) {
            if (result) {
                var files = model.filesController.files();
                model.filesController.fileIndex = files.indexOf(file);
                files.splice(model.filesController.fileIndex, 1);
                model.filesController.files(files);
                //call api remove
                TransShip.Scripts.jsDeleteFile(file.file_typeId, function (data) {
                    console.log("done");
                });
            }
        })
    },

    showNewFile: function () {
        console.log("show new");
        var files = model.filesController.files();
        model.filesController.fileIndex = files.length;
        var file = {
            description: "",
            status: "",
            file_typId: ""
        };
        model.filesController.mapFile(file);

        model.filesController.insertMode(true);
        model.filesController.editMode(false);
        model.filesController.gridMode(false);
    },

    update: function () {
        var datatable = model.filesController.datatable;
        var files = model.filesController.files();
        var filesIndex = model.filesController.filesIndex;
        var file = model.filesController.file;
        var fileParam = ko.mapping.toJS(model.filesController.file);

        if (!model.validateForm('#FileEdit')) {
            return;
        }
        //call api update
        TransShip.Scripts.jsUpdateFile(fileParam, function () {
            console.log("done");

            files.splice(filesIndex, 1, fileParam);
            model.filesController.files(files);

            setTimeout(function () {
                model.filesController.insertMode(false);
                model.filesController.editMode(false);
                model.filesController.gridMode(true); }, 2000);
        });
    },

    save: function () {
        var files = model.filesController.files();
        var fileIndex = model.filesController.filesIndex;
        var file = model.filesController.file;
        var fileParam = ko.toJS(file);

        if (!model.validateForm('#FileEdit')) {
            return;
        }
        //call api save
        TransShip.Scripts.jsSaveFileType(fileParam, function (data) {
            console.log("done");
            fileParam.file_typeId = data.file_typeId;
            files.push(fileParam);
            model.filesController.files(files);
            setTimeout(function () {
                model.filesController.insertMode(false);
                model.filesController.editMode(false);
                model.filesController.gridMode(true); }, 2000);
        });
    },

    cancel: function () {
        model.filesController.insertMode(false);
        model.filesController.editMode(false);
        model.filesController.gridMode(true);

        model.clearErrorMessage('#FileEdit');
    },


    initialize: function () {
        console.log("initialize files controller");
        var selfFiles = this;
        var files = this.files();
        if (files.length <= 0) {
            TransShip.Scripts.jsGetFileList(function (data) {
                console.log("done");
                files = data;
                selfFiles.files(files);
            });
        }

    }

};

