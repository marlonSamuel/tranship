console.log("Define memberships levels");


model.membersController = {

    member: {
        description: ko.observable(""),
        status: ko.observable(""),
        price: ko.observable(""),
        max_number_shipments: ko.observable(""),
        price_markup: ko.observable(""),
        membershipId: ko.observable(""),
    },
    members: ko.observableArray([]),
    memberIndex: 0,
    editMode: ko.observable(false),
    insertMode: ko.observable(false),
    gridMode: ko.observable(true),
    datatable: null,
    statusOptions: [{ name: 'Active', value: 'A' }, { name: 'Inactive', value: 'I' }],

    editMember: function (member) {
        console.log(member);
        var members = model.membersController.members();
        model.membersController.memberIndex = members.indexOf(member);
        console.log(model.membersController.memberIndex);
        //model.countriesController.country = ko.mapping.fromJS(country, model.countriesController.country);
        model.membersController.mapMember(member);

        console.log("edit");
        model.membersController.editMode(true);
        model.membersController.gridMode(false);
        model.membersController.insertMode(false);
        console.log(model.membersController.member);
    },

    mapMember: function (member) {
        var eMember = model.membersController.member;
        eMember.description(member.description);
        eMember.status(member.status);
        eMember.price(member.price);
        eMember.price_markup(member.price_markup);
        eMember.max_number_shipments(member.max_number_shipments);
        eMember.membershipId(member.membershipId);
    },

    removeMember: function (member) {
        console.log("remove");
        console.log(member);



        bootbox.confirm("¿Are you sure you want to delete " + member.description + "?", function (result) {
            if (result) {
                var members = model.membersController.members();
                model.membersController.memberIndex = members.indexOf(member);
                members.splice(model.membersController.memberIndex, 1);
                model.membersController.members(members);
                //call api remove
                TransShip.Scripts.jsDeleteMember(member.membershipId, function (data) {
                    console.log("done");
                    
                });
            }
        })
    },

    showNewMember: function () {
        console.log("show new");
        var members = model.membersController.members();
        model.membersController.memberIndex = members.length;
        var member = {
            description: "",
            status: "",
            price: "",
            price_markup: "",
            max_number_shipments: "",
            membershipId: ""
        };
        model.membersController.mapMember(member);

        model.membersController.insertMode(true);
        model.membersController.editMode(false);
        model.membersController.gridMode(false);
    },

    update: function () {
        var datatable = model.membersController.datatable;
        var members = model.membersController.members();
        var memberIndex = model.membersController.memberIndex;
        var member = model.membersController.member;
        members[memberIndex] = member;
        var memberParam = ko.toJS(member);

        if (!model.validateForm('#MemberEdit')) {
            return;
        }
        //call api update
        TransShip.Scripts.jsUpdateMember(memberParam, function () {
            console.log("done");
            
            members.splice(memberIndex, 1, memberParam);
            model.membersController.members(members);
            
            setTimeout(function () { model.membersController.insertMode(false);
                model.membersController.editMode(false);
                model.membersController.gridMode(true); }, 2000);
        });
    },

    save: function () {
        var members = model.membersController.members();
        var memberIndex = model.membersController.memberIndex;
        var member = model.membersController.member;
        
        var memberParam = ko.toJS(member);

        if (!model.validateForm('#MemberEdit')) {
            return;
        }
        //call api save
        TransShip.Scripts.jsSaveMember(memberParam, function (data) {
            console.log("done");
            memberParam.membershipId = data.membershipId;
            members.push(memberParam);
            model.membersController.members(members);

            setTimeout(function () { model.membersController.insertMode(false);
                model.membersController.editMode(false);
                model.membersController.gridMode(true); }, 2000);
        });
    },

    cancel: function () {
        model.membersController.insertMode(false);
        model.membersController.editMode(false);
        model.membersController.gridMode(true);

        model.clearErrorMessage('#MemberEdit');
    },


    initialize: function () {
        console.log("initialize member controller");
        var selfMembers = this;
        var members = this.members();
        if (members.length <= 0) {
            TransShip.Scripts.jsGetMemberList(function (data) {
                console.log("done");
                members = data;
                selfMembers.members(members);

                
            });
        }

    }

};

