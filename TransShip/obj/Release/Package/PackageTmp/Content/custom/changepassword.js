console.log("Define change password");


model.passController = {

    pass: {
        userId: ko.observable(""),
        email: ko.observable(""),
        password: ko.observable(""),
        newPass: ko.observable(""),
        confirmPassword: ko.observable(""),
    },

    mapPass: function (pass) {
        var ePass = model.passController.pass;
        ePass.userId(pass.userId);
        ePass.email(pass.email);
        ePass.password(pass.password);
        ePass.newPass(pass.newPass);
        ePass.confirmPassword(pass.confirmPassword);
    },

    showNewPass: function () {
        console.log("show new");
        var pass = {
            userId: "",
            email: "",
            password: "",
            newPass: "",
        };
        pass.userId = model.usersController.user.userId();
        pass.email = model.usersController.user.email();
        model.measureController.mapMeasure(measure);

        model.measureController.insertMode(true);
        model.measureController.editMode(false);
        model.measureController.gridMode(false);
    },

    update: function () {
        var pass = model.passController.pass;
        var passParam = ko.toJS(pass);

        if (!model.validateForm('#passEdit')) {
            return;
        }
        //call api update
        if (TransShip.Scripts) {
            TransShip.Scripts.jsChangePass(passParam.userId, passParam.email, passParam.password, passParam.newPass, function (data) {
                console.log("done");
                console.log(data);
                if (model.usersController.user.customerId() === 0) {
                    setTimeout(function () { window.location.href = '/Admin/Shipments'; }, 2000);
                } else {
                    setTimeout(function () { window.location.href = '/Customer/Shipments'; }, 2000);
                }
            });
        }
    },


    initialize: function (email,userId) {
        console.log("initialize changePass controller");
        model.passController.pass.email = email;
        model.passController.pass.userId = userId;
    }
};

