var model = {
    view: ko.observable("welcome"),
    rsvp: {
        email: ko.observable(""),
        password: ""
    },
    paises: ko.observableArray([]),
    membership: ko.observableArray([]),
    state: ko.observable("hide"),
    registerresult: ko.observable("hide"),
    modifyresult: ko.observable("hide"),
    eraseresult: ko.observable("hide"),
    erascountryresult: ko.observable("hide"),
    recoversuccess: ko.observable("hide"),
    chagepasswordresult: ko.observable("hide"),
    changecountryresult: ko.observable("hide"),
    iderasableuser: ko.observable("0"),
    iderasablecountry: ko.observable("0"),
    newcountryresult: ko.observable("0"),
    customeridforapproval: ko.observable("0"),
    approveresult: ko.observable("nothing"),
    register: {
        username: ko.observable(""),
        keyinformation: "",
        confirmkey: "",
        companyname: ko.observable(""),
        fullname: ko.observable(""),
        title: ko.observable(""),
        phonenumber: ko.observable(""),
        email: ko.observable(""),
        addr1: ko.observable(""),
        addr2: ko.observable(""),
        zipcode: ko.observable(""),
        idpais: ko.observable(""),
        canadaprovince: ko.observable(""),
        usstate: ko.observable(""),
        outsideus: ko.observable(""),
        membership: ko.observable(""),
        city: ko.observable("")
    },
    editable: {
        EMail: ko.observable(""),
        Password: ko.observable(""),
        PasswordConfirmation: ko.observable(""),
        fullname: ko.observable(""),
        CompanyName: ko.observable(""),
        Rol: ko.observable(""),
        Status: ko.observable(""),
        userId: ko.observable("")
    },
    recover:
    {
        email: ko.observable("")
    },
    changepassword:
    {
        password: "",
        passwordconfirm: "",
        confirmationkey: ""
    },
    newcountry: {
        countryname: ko.observable(""),
        longcountryname: ko.observable(""),
        ISO3: ko.observable(""),
        ISO2: ko.observable(""),
        NumCode: ko.observable(""),
        UNMemberState: ko.observable(""),
        CallingCode: ko.observable(""),
        CCTLD: ko.observable(""),
        id: ko.observable("")
    }
}

var changeCountry = function () {
    $.ajax("/api/provider/ChangeCountry", {
        type: "POST",
        data: {
            Name: model.newcountry.countryname,
            LongCountryName: model.newcountry.longcountryname,
            ISO3: model.newcountry.ISO3,
            ISO2: model.newcountry.ISO2,
            NumCode: model.newcountry.NumCode,
            UNMemberState: model.newcountry.UNMemberState,
            CallingCode: model.newcountry.CallingCode,
            CCTLD: model.newcountry.CCTLD,
            Id: model.newcountry.id
        },
        success: function (data) {
            if (data === 1) {
                model.changecountryresult("success");
            }
            else {
                model.changecountryresult("error");
            }
        }
    });
}

var addCountry = function () {
    $.ajax("/api/provider/addcountry", {
        type: "POST",
        data: {
            Name: model.newcountry.countryname,
            LongCountryName: model.newcountry.longcountryname,
            ISO3: model.newcountry.ISO3,
            ISO2: model.newcountry.ISO2,
            NumCode: model.newcountry.NumCode,
            UNMemberState: model.newcountry.UNMemberState,
            CallingCode: model.newcountry.CallingCode,
            CCTLD: model.newcountry.CCTLD
        },
        success: function (data) {
            if (data === 1) {
                model.newcountryresult("success");
            }
            else {
                model.newcountryresult("error");
            }
        }
    });
}

var deleteCountry = function () {
    var xyz = model.iderasableuser();
    $.ajax("/api/provider/EraseCountry", {
        type: "POST",
        data: {
            Id: model.iderasablecountry()
        },
        success: function (data) {
            if (data == 1) {
                model.erascountryresult("success");
            }
            else {
                model.erascountryresult("error");
            }

        }
    });
}

var deleteUser = function () {
    var xyz = model.iderasableuser();
    $.ajax("/api/provider/EraseUser", {
        type: "POST",
        data: {
            id: model.iderasableuser()
        },
        success: function (data) {
            if (data == 1) {
                model.eraseresult("success");
            }
            else {
                model.eraseresult("error");
            }

        }
    });
}

var changePassword = function () {
    var x = document.getElementById("confirmkey").value;

    $.ajax("/api/provider/changepassword", {
        type: "POST",
        data: {
            password: model.changepassword.password,
            confirmPassword: model.changepassword.confirmPassword,
            APIKey: x
        },
        success: function (data) {
            if (data == 1) {
                model.chagepasswordresult("success");
                setTimeout(TransShip.Scripts.jsTest, 5000);
            }
            else {
                model.chagepasswordresult("error");
            }

        }
    });
}

var getCountryList = function () {
    $.ajax("/api/provider", {
        type: "GET",
        success: function (data) {
            model.paises.removeAll();
            for (var i = 0; i < data.length; i++) {
                model.paises.push(data[i]);
            }
        }
    });
}

var sendRsvp = function () {
    $.ajax("/api/validations", {
        type: "POST",
        data: {
            email: model.rsvp.email,
            password: model.rsvp.password
        },
        success: function (data) {
            if (data === 1) {
                model.view("autenticado");
                $("#login-modal").modal('hide');
            }
            else {
                model.view("error");
            }
        }
    });
}

var sendRegister = function () {
    $.ajax("/api/provider/createusername", {
        type: "POST",
        data: {
            username: model.register.username,
            keyinformation: model.register.keyinformation,
            confirmkey: model.register.confirmkey,
            companyname: model.register.companyname,
            fullname: model.register.fullname,
            title: model.register.title,
            phonenumber: model.register.phonenumber,
            email: model.register.username,
            addr1: model.register.addr1,
            addr2: model.register.addr2,
            zipcode: model.register.zipcode,
            idpais: model.register.idpais,
            outsideus: model.register.outsideus,
            usstate: model.register.usstate,
            canadaprovince: model.register.canadaprovince,
            membership: model.register.membership,
            city: model.register.city
        },
        success: function (data) {
            if (data == 1) {
                model.registerresult("success");
            }
            else {
                model.registerresult("error");
            }

        }
    });
}

var recoverPassword = function () {
    $.ajax("/api/provider/recoverpassword", {
        type: "POST",
        data: {
            recoverEmail: model.recover.email,
        },
        success: function (data) {
            if (data == 1) {
                model.recoversuccess("success");
            }
            else {
                model.recoversuccess("error");
            }

        }
    });
}

var modifyUserInformation = function () {
    var pass1 = document.getElementById('edituserPassword');
    var pass2 = document.getElementById('edituserPasswordConfirmation');
    var goodColor = "#66cc66";
    var badColor = "#ff6666";
    if (pass1.value == pass2.value) {
        pass1.style.backgroundColor = goodColor;
        pass2.style.backgroundColor = goodColor;
        $.ajax("/api/provider/ModifyUserName", {
            type: "POST",
            data: {
                id: model.editable.userId,
                EMail: model.editable.EMail,
                Password: model.editable.Password,
                PasswordVerify: model.editable.PasswordConfirmation,
                FullName: model.editable.fullname,
                Role: model.editable.Rol,
                Status: model.editable.Status
            },
            success: function (data) {
                if (data == 1) {
                    model.modifyresult("success");
                }
                else {
                    model.modifyresult("error");
                }
            }
        });
    } else {
        pass1.style.backgroundColor = badColor;
        pass2.style.backgroundColor = badColor;
        model.modifyresult("passworderror");
    }
}


var approveCustomer = function () {
    $.ajax("/api/provider/ApproveCustomer/", {
        type: "POST", 
        data: {
            customerId: model.customeridforapproval() 
        },
        success: function (data) {
            if (data == 1) {
                model.approveresult("success");
            }
            else {
                model.approveresult("error");
            }
        }
    });
}

var rejectCustomer = function () {
    $.ajax("/api/provider/RejectCustomer/", {
        type: "POST",
        data: {
            customerId: model.customeridforapproval()
        },
        success: function (data) {
            if (data == 1) {
                model.approveresult("success");
            }
            else {
                model.approveresult("error");
            }

        }
    });
}

var TransShip = {};
TransShip.Scripts =
    {
    jsGetPendingApprobalRecord: function (customerid, userid) {
        model.customeridforapproval(customerid);
            $.ajax("/api/provider/getpendingapprovalrecord/" + customerid, {
                type: "GET",
                success: function (data) {
                    model.register.username(data.username);
                    model.register.companyname(data.companyname);
                    model.register.fullname(data.fullname);
                    model.register.title(data.title);
                    model.register.phonenumber(data.phonenumber);
                    model.register.email(data.email);
                    model.register.addr1(data.addr1);
                    model.register.addr2(data.addr2);
                    model.register.zipcode(data.zipcode);
                    model.register.idpais(data.idpais);
                    model.register.canadaprovince(data.canadaprovince);
                    model.register.usstate(data.usstate);
                    model.register.outsideus(data.outsideus);
                    model.register.membership(data.membership);
                    model.register.city(data.city);
                    $("#pendingaprovalrecord").modal();
                }
            });
        },
        jsDeleteCountryConfirm: function (id) {
            model.iderasablecountry(id);
            $("#confirmDeleteCountry").modal();
        },
        jsDeleteConfirm: function (id) {
            model.iderasableuser(id);
            $("#confirmDeleteUser").modal();
        },
        jsGetCountryInformation: function (id) {
            $.ajax("/api/provider/GetCountryInformation/" + id, {
                type: "GET",
                success: function (data) {
                    model.newcountry.countryname(data.Name);
                    model.newcountry.longcountryname(data.LongCountryName);
                    model.newcountry.ISO3(data.ISO3);
                    model.newcountry.ISO2(data.ISO2);
                    model.newcountry.NumCode(data.NumCode);
                    model.newcountry.UNMemberState(data.UNMemberState);
                    model.newcountry.CallingCode(data.CallingCode);
                    model.newcountry.CCTLD(data.CCTLD);
                    model.newcountry.id(id);
                    $("#editcountry").modal();
                }
            });
        },
        jsGetUserInformation: function (id) {
            $.ajax("/api/provider/GetUserInformation/" + id, {
                type: "GET",
                success: function (data) {
                    model.editable.fullname(data.FullName);
                    model.editable.EMail(data.EMail);
                    model.editable.Rol(data.Role);
                    model.editable.Status(data.Status);
                    model.editable.userId(id);
                    $("#edituser").modal();
                }
            });
        },
        jsTest: function () {
            window.location.assign("/")
        },
        jsCountryChange: (function () {
            var x = document.getElementById("country").value;
            if (x) {
                switch (x) {
                    case '236':
                        model.state("us");
                        break;
                    case '40':
                        model.state("ca");
                        break;
                    default:
                        model.state("state");
                }
            }
            else {

            }

        }),
        jsGetCountryList: function () {
            $.ajax("/api/provider/GetCountryList", {
                type: "GET",
                success: function (data) {
                    model.paises.removeAll();
                    for (var i = 0; i < data.length; i++) {
                        model.paises.push(data[i]);
                    }
                }
            });
        },
        jsGetMembershipLevel: function () {
            $.ajax("/api/provider/GetMembershipLevel", {
                type: "GET",
                success: function (data) {
                    model.membership.removeAll();
                    for (var i = 0; i < data.length; i++) {
                        model.membership.push(data[i]);
                    }
                }
            });
        }
    }
