console.log("Define Scripts");


ko.bindingHandlers.stopBinding = {
    init: function () {
        return { controlsDescendantBindings: true };
    }
};

ko.virtualElements.allowedBindings.stopBinding = true;


var changeCountry = function () {
    $.ajax("/api/cuntries/put", {
        type: "PUT",
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


var approveCustomer = function (customerId, userId, callback) {
    $.ajax("/api/provider/ApproveCustomer/", {
        type: "POST",
        data: {
            customerId: customerId,
            userId: userId
        },
        success: function (data) {
            if (data == 1) {
                model.approveresult("success");
                toastr.info("Approve success");
                setTimeout(function () { location.reload(); }, 2000);
            }
            else {
                model.approveresult("error");
                toastr.error("Error to approve");
                setTimeout(function () { location.reload(); }, 2000);
            }
        }
    });
}


TransShip.Scripts =
    {
        changePassword: function (confirmKey) {
            var x = confirmKey;

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
                        bootbox.alert('Well done! Please wait for 5 seconds and sign in again.');
                        setTimeout(TransShip.Scripts.jsTest, 5000);
                    }
                    else {

                        model.chagepasswordresult("error");
                        bootbox.alert('Error, something bad on change your password.');
                    }

                }
            });
        },
        sendRegister: function () {
            $.ajax("/api/provider/createusername", {
                type: "POST",
                data: {
                    username: model.register.username(),
                    keyinformation: model.register.keyinformation(),
                    confirmkey: model.register.confirmkey(),
                    companyname: model.register.companyname(),
                    fullname: model.register.fullname(),
                    title: model.register.title(),
                    phonenumber: model.register.phonenumber(),
                    email: model.register.username(),
                    addr1: model.register.addr1(),
                    addr2: model.register.addr2(),
                    zipcode: model.register.zipcode(),
                    idCountry: model.register.idCountry(),
                    outsideus: model.register.outsideus(),
                    usstate: model.register.usstate,
                    canadaprovince: model.register.canadaprovince(),
                    membership: model.register.membership(),
                    city: model.register.city()
                },
                success: function (data) {
                    if (data == 1) {
                        model.register.registerResult("success");
                        toastr.info("Registration is saved");
                        setTimeout(function () { window.location.href = '/Home/'; }, 2000);

                    }
                    else {
                        model.register.registerResult("error");
                        toastr.error("Registration error");
                    }

                }
            });
        },

        jsapproveCustomer: function (customerId, userId, callback) {
            $.ajax("/api/provider/ApproveCustomer/", {
                type: "POST",
                data: {
                    customerId: customerId,
                    userId: userId
                },
                success: function (data) {
                    if (data == 1) {
                        model.approveresult("success");
                        toastr.info("Approve success");
                        setTimeout(function () { location.reload(); }, 2000);
                    }
                    else {
                        model.approveresult("error");
                        toastr.error("Error to approve");
                        setTimeout(function () { location.reload(); }, 2000);
                    }
                }
            });
        },

        jsrejectCustomer: function (customerId, userId, callback) {
            $.ajax("/api/provider/RejectCustomer/", {
                type: "POST",
                data: {
                    customerId: customerId,
                    userId: userId
                },
                success: function (data) {
                    if (data == 1) {
                        //model.approveresult("success");
                        toastr.info("Reject success");
                        setTimeout(function () { location.reload(); }, 3000);
                    }
                    else {
                        //model.approveresult("error");
                        toastr.error("Error to reject");
                        setTimeout(function () { location.reload(); }, 3000);
                    }

                }
            });
        },

        jsGetPendingApprobalRecord: function (customerid, userid) {
            model.customeridforapproval(customerid);
            $.ajax("/api/provider/getpendingapprovalrecord/" + customerid, {
                type: "GET",
                success: function (data) {
                    model.register.customerId(data.customerId),
                        model.register.userId(data.userId),
                        model.register.username(data.username);
                    model.register.companyname(data.companyname);
                    model.register.fullname(data.fullname);
                    model.register.title(data.title);
                    model.register.phonenumber(data.phonenumber);
                    //model.register.email(data.username);
                    model.register.addr1(data.addr1);
                    model.register.addr2(data.addr2);
                    model.register.zipcode(data.zipcode);
                    model.register.idCountry(data.idpais);
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
        jsGetCountryList: function (callback) {
            $.ajax("/api/cuntries/get", {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get countries");
                }
            });
        },

        jsUpdateCountry: function (country, callback) {
            $.ajax("/api/cuntries/put", {
                type: "PUT",
                data: country,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update");
                }
            });
        },

        jsSaveCountry: function (country, callback) {
            $.ajax("/api/cuntries/post", {
                type: "POST",
                data: country,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save");
                }
            });
        },

        jsDeleteCountry: function (id, callback) {
            $.ajax("/api/cuntries/delete?id=" + id, {
                type: "DELETE",
                data: id,
                success: function (data) {
                    console.log(data);
                    if (data == 'ok') {
                        toastr.info("Country deleted");
                    }
                    else {
                        toastr.error("Delete error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to delete");
                }
            });
        },

        jsGetMembershipLevel: function (callback) {
            $.ajax("/api/provider/GetMembershipLevel", {
                type: "GET",
                success: function (data) {
                    model.membership.removeAll();
                    for (var i = 0; i < data.length; i++) {
                        model.membership.push(data[i]);
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }
            });
        },

        jsGetVendorsList: function (callback) {
            $.ajax("/api/vendors/get", {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to get vendors");
                }
            });
        },

        jsGetVendorsById: function (vendorId, callback) {
            $.ajax("/api/vendors/GetVendorById?vendorId="+vendorId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to get vendors");
                }
            });
        },

        jsGetFileList: function (callback) {
            $.ajax("/api/type_file/get", {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Errot to get type_files");
                }
            });
        },

        jsSaveFileType: function (file, callback) {
            $.ajax("/api/type_file/post", {
                type: "POST",
                data: file,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save files");
                }
            });
        },

        jsUpdateFile: function (file, callback) {
            $.ajax("/api/Type_file/put", {
                type: "PUT",
                data: file,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");

                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update file");
                }
            });
        },

        jsDeleteFile: function (id, callback) {
            $.ajax("/api/type_file/delete?id=" + id, {
                type: "DELETE",
                data: id,
                success: function (data) {
                    console.log(data);
                    if (data == 'ok') {
                        toastr.info("File deleted");

                    }
                    else {
                        toastr.error("Delete error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to delete file");
                }
            });
        },

        jsGetServiceList: function (callback) {
            $.ajax("/api/service_type/get", {
                type: "GET",
                success: function (data) {



                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get type services");

                }
            });
        },
        jsgetOneService: function (id, callback) {
            $.ajax("/api/unit/GetByServiceId?id=" + id, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get type services");

                }
            });
        },

        jsSaveService: function (service, callback) {
            $.ajax("/api/service_type/post", {
                type: "POST",
                data: service,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save services");
                }
            });
        },

        jsShipmentSetVendorPrices: function (shipmentId, callback) {
            $.ajax("/api/ShipmetenService/SetVendorPrices?shipmentId=" + shipmentId, {
                type: "POST",
                data: shipmentId,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update vendor prices");
                }
            });
        },

        jsUpdateService: function (service, callback) {
            $.ajax("/api/service_type/put", {
                type: "PUT",
                data: service,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update services");
                }
            });
        },

        jsDeleteService: function (id, callback) {
            $.ajax("/api/service_type/delete?id=" + id, {
                type: "DELETE",
                data: id,
                success: function (data) {
                    console.log(data);
                    if (data == 'ok') {
                        toastr.info("File service deleted");
                    }
                    else {
                        toastr.error("Deleted error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to delte file services");
                }
            });
        },

        jsGetMemberList: function (callback) {
            $.ajax("/api/membership_level/get", {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get membership level");
                }
            });
        },

        jsSaveMember: function (member, callback) {
            $.ajax("/api/membership_level/post", {
                type: "POST",
                data: member,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save membership level");
                }
            });
        },

        jsUpdateMember: function (member, callback) {
            $.ajax("/api/membership_level/put", {
                type: "PUT",
                data: member,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update member file");
                }
            });
        },

        jsDeleteMember: function (id, callback) {
            $.ajax("/api/membership_level/delete?id=" + id, {
                type: "DELETE",
                data: id,
                success: function (data) {
                    console.log(data);
                    if (data == 'ok') {
                        toastr.info("Membership deleted");
                    }
                    else {
                        toastr.error("Deleted error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.info("Error to delete membership");
                }
            });
        },

        jsGetPaymentList: function (callback) {
            $.ajax("/api/membership_payment/get", {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get payment List");
                }
            });
        },


        jsGetHistoryList: function (idcustomer, callback) {
            $.ajax("/api/membership_payment/getHistory?idCustomer=" + idcustomer, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get memebership level");
                }
            });
        },

        jsUpdateHistory: function (history, callback) {
            $.ajax("/api/membership_payment/put", {
                type: "PUT",
                data: history,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update countries");
                }
            });
        },

        jsSaveHistory: function (history, callback) {
            $.ajax("/api/membership_payment/post", {
                type: "POST",
                data: history,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save member history");
                }
            });
        },

        jsDeleteHistory: function (id, callback) {
            $.ajax("/api/membership_payment/delete?id=" + id, {
                type: "DELETE",
                data: id,
                success: function (data) {
                    console.log(data);
                    if (data == 'ok') {
                        toastr.info("Payment deleted");
                    }
                    else {
                        toastr.error("Deleted error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to delete membership history");
                }
            });
        },

        jsGetAllMeasures: function (callback) {
            $.ajax("/api/Unit/get", {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get measures unit");
                }
            });
        },

        jsGetMeasureList: function (idService, callback) {
            $.ajax("/api/Unit/getByServiceId?id=" + idService, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get measures unit");
                }
            });
        },

        jsUpdateMeasure: function (measure, callback) {
            $.ajax("/api/unit/put", {
                type: "PUT",
                data: measure,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Update is sucessfull");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update measures unit");
                }
            });
        },

        jsSaveMeasure: function (measure, callback) {
            $.ajax("/api/unit/post", {
                type: "POST",
                data: measure,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save measures unit");
                }
            });
        },

        jsDeleteMeasure: function (id, callback) {
            $.ajax("/api/unit/delete?id=" + id, {
                type: "DELETE",
                data: id,
                success: function (data) {
                    console.log(data);
                    if (data == 'ok') {
                        toastr.info("Measure deleted");
                    }
                    else {
                        toastr.error("Deleted error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to delelete measure unit");
                }
            });
        },

        jsGetStateList: function (idService, callback) {
            $.ajax("/api/state/get?id=" + idService, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get States");
                }
            });
        },

        jsUpdateState: function (state, callback) {
            $.ajax("/api/state/put", {
                type: "PUT",
                data: state,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Update is sucessfull");

                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update States");
                }
            });
        },

        jsSaveState: function (state, callback) {
            $.ajax("/api/state/post", {
                type: "POST",
                data: state,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save States");
                }
            });
        },

        jsDeleteStates: function (id, callback) {
            $.ajax("/api/state/delete?id=" + id, {
                type: "DELETE",
                data: id,
                success: function (data) {
                    console.log(data);
                    if (data == 'ok') {
                        toastr.info("State deleted");
                    }
                    else {
                        toastr.error("Deleted error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.info("Error to delelete State");
                }
            });
        },

        jsSaveVendor: function (vendor, callback) {
            $.ajax("/api/vendors/post", {
                type: "POST",
                data: vendor,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    bootbox.alert("Error to save");
                }
            });
        },

        jsUpdateVendor: function (vendor, callback) {
            $.ajax("/api/vendors/put", {
                type: "PUT",
                data: vendor,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.info("Error to update Vendor");
                }
            });
        },

        jsUpdateLogoVendor: function (logo) {
            $.ajax("/api/vendors/UploadLogo", {
                contentType: false,
                processData: false,
                dataType: "JSON",
                type: "PUT",
                data: logo,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    //toastr.error("Error to update Vendor");
                }
            });
        },

        jsDeleteVendor: function (id, callback) {
            $.ajax("/api/vendors/delete?id=" + id, {
                type: "DELETE",
                data: id,
                success: function (data) {
                    console.log(data);
                    if (data === 'ok') {
                        toastr.info("Vendor deleted");

                    }
                    else {
                        toastr.error(data + "Deleted error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    bootbox.alert("Error to delte countries");
                }
            });
        },

        jsGetUser: function (user, callback) {
            $.ajax("/api/users/getUser?email=" + user, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.info("Error to get users");
                }
            });
        },

        jsGetUserByUserId: function (userId, callback) {
            $.ajax("/api/users/GetUserByUserId?userId=" + userId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.info("Error to get users");
                }
            });
        },

        jsGetUserList: function (callback) {
            $.ajax("/api/users/get", {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.info("Error to get countries");
                }
            });
        },

        jsGetUserListU: function (id, callback) {
            $.ajax("/api/users/GetUserEmail?id=" + id, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.info("Error to get users");
                }
            });
        },

        jsUpdateUser: function (user, callback) {
            $.ajax("/api/users/put", {
                type: "PUT",
                data: user,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update countries");
                }
            });
        },

        jsSaveUser: function (user, callback) {
            $.ajax("/api/users/post", {
                type: "POST",
                data: user,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save user");
                }
            });
        },

        jsDeleteUser: function (id, callback) {
            $.ajax("/api/users/delete?id=" + id, {
                type: "DELETE",
                data: id,
                success: function (data) {
                    console.log(data);
                    if (data == 'ok') {
                        toastr.info("User deleted");
                    }
                    else {
                        toastr.error("Deleted error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to delete user");
                }
            });
        },

        jsUpdateProfile: function (user, callback) {
            $.ajax("/api/users/putProfile", {
                type: "PUT",
                data: user,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update profile");
                }
            });
        },

        jsUpdateLogo: function (customerId, logo, callback) {
            $.ajax("/api/users/uploadLogo", {
                contentType: false,
                processData: false,
                dataType: "JSON",
                type: "PUT",
                data: logo,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    //toastr.error("Error to update profile");
                }
            });
        },

        jsChangePass: function (userId, email, password, newPass, callback) {
            $.ajax("/api/users/getPass?id=" + userId + "&email=" + email + "&password=" + password + "&newPass=" + newPass, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                    if (data != null) {
                        toastr.info(data.Message);
                    }
                }, error: function (data) {
                    toastr.error("Error to change password");
                    setTimeout(function () { window.location.href = '/Customer/Customer'; }, 2000);
                }
            });
        },

        jsGetContactList: function (idCustomer, callback) {
            $.ajax("/api/contacts/get?idCustomer=" + idCustomer, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get contacts");
                }
            });
        },

        jsUpdateContact: function (contact, callback) {
            $.ajax("/api/contacts/put", {
                type: "PUT",
                data: contact,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update contacts");
                }
            });
        },

        jsSaveContact: function (contact, callback) {
            $.ajax("/api/contacts/post", {
                type: "POST",
                data: contact,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save contact");
                }
            });
        },

        jsDeleteContact: function (idContact, callback) {
            $.ajax("/api/contacts/delete?idContact=" + idContact, {
                type: "DELETE",
                data: idContact,
                success: function (data) {
                    console.log(data);
                    if (data == 'ok') {
                        toastr.info("Contact deleted");
                    }
                    else {
                        toastr.error("Deleted error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to get contacts");
                }
            });
        },

        jsGetAddressList: function (idCustomer, callback) {
            $.ajax("/api/recurring_addresses/get?idCustomer=" + idCustomer, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get addresses");
                }
            });
        },

        jsGetOneAddress: function (idAddress, callback) {
            $.ajax("/api/Recurring_Addresses/getOne?idAddress=" + idAddress, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get address");
                }
            });
        },

        jsUpdateAddress: function (address, callback) {
            $.ajax("/api/recurring_addresses/put", {
                type: "PUT",
                data: address,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update address");
                }
            });
        },

        jsSaveAddress: function (address, callback) {
            $.ajax("/api/recurring_addresses/post", {
                type: "POST",
                data: address,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Errror to save address");
                }
            });
        },

        jsDeleteAddress: function (idAddress, callback) {
            $.ajax("/api/recurring_addresses/delete?idAddress=" + idAddress, {
                type: "DELETE",
                data: idAddress,
                success: function (data) {
                    console.log(data);
                    if (data == 'ok') {
                        toastr.info("Recurring address deleted");
                    }
                    else {
                        toastr.error("Deleted error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to delete address");
                }
            });
        },


        jsGetVendorList: function (callback) {
            $.ajax("/api/vendors/get", {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get vendors");
                }
            });
        },

        jsGetVendorContactList: function (vendorId, callback) {
            $.ajax("/api/vendorContacts?vendorId=" + vendorId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get vendor contacts");
                }
            });
        },

        jsSaveVendorContact: function (vendorContact, callback) {
            $.ajax("/api/vendorContacts/post", {
                type: "POST",
                data: vendorContact,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Changes saved");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save");
                }
            });
        },

        jsUpdateVendorContact: function (vendorContact, callback) {
            $.ajax("/api/vendorContacts/put", {
                type: "PUT",
                data: vendorContact,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update vendor contact");
                }
            });
        },

        jsDeleteVendorContact: function (vendorContact, callback) {
            $.ajax("/api/vendorContacts/delete?vendorId=" + vendorContact.vendorId + "&contactId=" + vendorContact.contactId, {
                type: "DELETE",
                success: function (data) {
                    console.log(data);
                    if (data == 'ok' || data === 'register deleted') {
                        toastr.info("Contact deleted");
                    }
                    else {
                        toastr.error("Deleted error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to delete");
                }
            });
        },

        jsGetRateList: function (vendorId, callback) {
            $.ajax("/api/rates?vendorId=" + vendorId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get vendor rates");
                }
            });
        },

        jsGetRatesVendorList: function (vendorId, callback) {
            $.ajax("/api/rates?rateVendorId=" + vendorId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get vendor rates");
                }
            });
        },

        jsSaveRate: function (rate, callback) {
            $.ajax("/api/rates/post", {
                type: "POST",
                data: rate,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Error Error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save rate");
                }
            });
        },

        jsDeleteRate: function (rate, callback) {
            $.ajax("/api/rates/delete", {
                type: "DELETE",
                data: rate,
                success: function (data) {
                    console.log(data);
                    if (data == 'ok' || data === 'rate deleted') {
                        toastr.info("Rate deleted");
                    }
                    else {
                        toastr.error("Deleted error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to delete");
                }
            });
        },

        jsUpdateRate: function (rate, callback) {
            $.ajax("/api/rates/put", {
                type: "PUT",
                data: rate,
                success: function (data) {
                    if (data != null) {
                        toastr.info("update successfull");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update");
                }
            });
        },

        jsGetShipmentList: function (idCustomer, callback) {
            $.ajax("/api/shipments/get?idCustomer=" + idCustomer, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get shipments");
                }
            });
        },

        jsGetShipmentListByStatus: function (status, callback) {
            $.ajax("/api/shipments/GetByStatus?status=" + status, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get shipments");
                }
            });
        },

        jsGetShipmentListByStatusAndDateRange: function (status, from, to, callback) {
            $.ajax("/api/shipments/GetByStatusAndDateRange?status=" + status + "&from=" + from + "&to=" + to, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get shipments");
                }
            });
        },

        jsGetCustomers: function (callback) {
            $.ajax("/api/customers/get", {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get customers info");
                }
            });
        },

        jsGetCustomersByCustomerId: function (customerId, callback) {
            $.ajax("/api/customers/GetCustomerByCustomerId?customerId="+customerId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get customers info");
                }
            });
        },

        jsGetOneShipment: function (shipmentId, callback) {
            $.ajax("/api/shipments/getOne?shipmentId=" + shipmentId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get shipments");
                }
            });
        },

        jsUpdateShipment: function (shipment, callback) {
            $.ajax("/api/shipments/put", {
                type: "PUT",
                data: shipment,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update shipment");
                }
            });
        },

        jsShipmentSetStatus: function (shipmentId, status, callback) {
            $.ajax("/api/shipments/SetStatus?shipmentId=" + shipmentId + "&status=" + status, {
                type: "POST",
                data: shipmentId,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update shipment");
                }
            });
        },

        jsSaveShipment: function (shipment, callback) {
            $.ajax("/api/shipments/post", {
                type: "POST",
                data: shipment,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save shipment");
                }
            });
        },

        jsDeleteShipment: function (id, callback) {
            $.ajax("/api/shipments/delete?id=" + id, {
                type: "DELETE",
                data: id,
                success: function (data) {
                    console.log(data);
                    if (data == 'ok') {
                        toastr.info("Shipment deleted");
                    }
                    else {
                        toastr.error("Deleted error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to delete shipment");
                }
            });
        },

        jsGetShipsList: function (shipmentId, callback) {
            $.ajax("/api/ShipmetenService/get?shipmentId=" + shipmentId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get type services");
                }
            });
        },

        jsSaveShip: function (shipment, callback) {
            $.ajax("/api/ShipmetenService/post", {
                type: "POST",
                data: shipment,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save shipment");
                }
            });
        },

        jsUpdateShip: function (shipment, callback) {
            $.ajax("/api/ShipmetenService/put", {
                type: "PUT",
                data: shipment,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Update error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to update shipment service");
                }
            });
        },

        jsDeleteShip: function (shipment, callback) {
            $.ajax("/api/ShipmetenService/delete", {
                type: "DELETE",
                data: shipment,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save shipment");
                }
            });
        },

        jsGetNotesList: function (shipmentId, callback) {
            $.ajax("/api/ShipmentNotes/get?shipmentId=" + shipmentId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get shipment Notes");
                }
            });
        },

        jsSaveNote: function (notes, callback) {
            $.ajax("/api/ShipmentNotes/post", {
                type: "POST",
                data: notes,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save shipment");
                }
            });
        },

        jsUpdateNote: function (notes, callback) {
            $.ajax("/api/ShipmentNotes/put", {
                type: "PUT",
                data: notes,
                success: function (data) {
                    if (data != null) {
                        if (data.status == 'R') {
                            toastr.info("read note");
                        }
                    }
                    else {
                        toastr.error("error to read note");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to read note");
                }
            });
        },

        jsGetFilesList: function (shipmentId, callback) {
            $.ajax("/api/ShipmentFiles/get?shipmentId=" + shipmentId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get shipment Files");
                }
            });
        },

        jsSaveFile: function (files, data, callback) {

            $.ajax("/api/ShipmentFiles/UploadFile", {
                type: "POST",
                contentType: false,
                processData: false,
                dataType: "JSON",
                data: data,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save files");
                }
            });
        },

        jsGetShipmentCostList: function (shipmentId, callback) {
            $.ajax("/api/ShipmentCost/get?shipmentId=" + shipmentId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to get shipment Costs");
                }
            });
        },

        jsSaveShipmentCost: function (shipcost, callback) {
            $.ajax("/api/ShipmentCost/post", {
                type: "POST",
                data: shipcost,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save cost");
                }
            });
        },

        jsDeleteShipmentCost: function (shipcost, callback) {
            $.ajax("/api/ShipmentCost/delete", {
                type: "DELETE",
                data: shipcost,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Cost deleted");
                    }
                    else {
                        toastr.error("delete error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to delete cost");
                }
            });
        },

        jsGetShipServiceCost: function (shipmentId, callback) {
            $.ajax("/api/shipmentapprove/GetShipServiceCost?shipmentId=" + shipmentId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to get shipment services Costs");
                }
            });
        },

        jsGetShipCosts: function (shipmentId, callback) {
            $.ajax("/api/shipmentapprove/GetShipCost?shipmentId=" + shipmentId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                }, error: function (data) {
                    toastr.error("Error to get shipment Costs");
                }
            });
        },

        jsGettracksList: function (shipmentId, callback) {
            $.ajax("/api/ShipmentTracking/GetShipmentTracking?shipmentId=" + shipmentId, {
                type: "GET",
                success: function (data) {
                    if (typeof callback === 'function') {
                        callback(data);
                    }

                }, error: function (data) {
                    toastr.error("Error to get shipment Tracking");
                }
            });
        },

        jsSaveTracking: function (tracking, callback) {
            $.ajax("/api/ShipmentTracking/post", {
                type: "POST",
                data: tracking,
                success: function (data) {
                    if (data != null) {
                        toastr.info("Changes saved");
                    }
                    else {
                        toastr.error("Save error");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to save tracking");
                }
            });
        },

        jsUpdateTracking: function (tracking, callback) {
            $.ajax("/api/ShipmentTracking/put", {
                type: "PUT",
                data: tracking,
                success: function (data) {
                    if (data != null) {
                        toastr.info("update Tracking");
                    }
                    else {
                        toastr.error("error to Tracking");
                    }
                    if (typeof callback === 'function') {
                        callback(data);
                    }
                },
                error: function (data) {
                    toastr.error("Error to Tracking");
                }
            });
        },
    }
