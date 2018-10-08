/*!
 * remark (http://getbootstrapadmin.com/remark)
 * Copyright 2017 amazingsurge
 * Licensed under the Themeforest Standard Licenses
 */
(function (document, window, $) {
    'use strict';

    var Site = window.Site;


    $(document).ready(function ($) {
        Site.run();
    });


    // Table Add Row
    // -------------
    (function ($) {
        var Table = {
            options: {
                addButton: '#addToTable',
                table: '#tbldata',
                dialog: {
                    wrapper: '#dialog',
                    cancelButton: '#dialogCancel',
                    confirmButton: '#dialogConfirm'
                }
            },

            initialize: function () {
                this
                    .setVars()
                    .build()
                    .events();
            },

            setVars: function () {
                this.$table = $(this.options.table);
                this.$addButton = $(this.options.addButton);

                // dialog
                this.dialog = {};
                this.dialog.$wrapper = $(this.options.dialog.wrapper);
                this.dialog.$cancel = $(this.options.dialog.cancelButton);
                this.dialog.$confirm = $(this.options.dialog.confirmButton);

                return this;
            },

            build: function () {
                this.datatable = this.$table.DataTable({
                    //dom: "Blfrtip",
                    ajax: "/RoleMaster/Data",
                    columns: [
                        { data: "SID" },
                        { data: null, "defaultContent": '' },
                        { data: "IDROLE" },
                        { data: "ROLENAME" },
                        {
                            data: "ISACTIVE",
                            render: function (data, type, row) {
                                if (type === 'display') {
                                    return '<input type="checkbox" class="editor-active" disabled>';
                                }
                                return data;
                            },
                            className: "center"
                        }
                        , { data: null, "defaultContent": '' }
                    ],
                    "columnDefs": [
                        {
                            "targets": [0],
                            "visible":false,
                            "searchable": false
                        }, {
                            "className": "details-control",
                            "targets": 1
                        }, {
                            "className": "center",
                            "targets": 2
                        }
                        , {
                            "className": "actions center",
                            "width": "60px",
                            "targets": 5, "data": "SID", "render": function (data, type, full, meta) {
                                return '<div class="SID" style="display:none;">' + data.SID + '</div><a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-default edit-row" data-toggle="tooltip" data-original-title="Edit"><i class="icon md-edit" aria-hidden="true"></i></a>'
                                    + ' <a href= "#" type= "button" class="btn btn-sm btn-icon btn-pure btn-default on-default remove-row" data- toggle="tooltip" data- original - title="Remove" > <i class="icon md-delete" aria-hidden="true"></i></a > '
                                    + ' <a href="#" type= "button" class="btn btn-sm btn-icon btn-pure btn-default on-editing save-row" data-toggle="tooltip" data-original-title="Save" hidden><i class="icon md-check-all" aria-hidden="true"></i></a>'
                                    + ' <a href="#" type= "button" class="btn btn-sm btn-icon btn-pure btn-default on-editing cancel-row" data-toggle="tooltip" data-original-title="Delete" hidden><i class="icon md-close" aria-hidden="true"></i></a>';
                            }
                        }
                    ],
                    select: {
                        style: 'os',
                        selector: 'td:first-child'
                    }
                    ,
                    rowCallback: function (row, data) {
                        console.log(data.ISACTIVE == 1);
                        // Set the checked state of the checkbox in the table
                        $('input.editor-active', row).prop('checked', data.ISACTIVE == 1);
                    }
                });

                window.dt = this.datatable;

                return this;
            },

            events: function () {
                var _self = this;

                this.$table
                    .on('click', 'a.save-row', function (e) {
                        e.preventDefault();

                        _self.rowSave($(this).closest('tr'));
                    })
                    .on('click', 'a.cancel-row', function (e) {
                        e.preventDefault();

                        _self.rowCancel($(this).closest('tr'));
                    })
                    .on('click', 'a.edit-row', function (e) {
                        e.preventDefault();
                        _self.rowEdit($(this).closest('tr'));
                    })
                    .on('click', 'a.remove-row', function (e) {
                        e.preventDefault();

                        var $row = $(this).closest('tr');
                        bootbox.dialog({
                            message: "Are you sure that you want to delete this row?",
                            title: "ARE YOU SURE?",
                            buttons: {
                                danger: {
                                    label: "Confirm",
                                    className: "btn-danger",
                                    callback: function () {
                                        var SID = $row.find(".SID").html();
                                        $.ajax({
                                            type: "POST",
                                            url: "/RoleMaster/Delete",
                                            data: '{id: ' + SID + '}',
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function () {
                                                _self.rowRemove($row);
                                            }
                                        });
                                        _self.rowRemove($row);
                                    }
                                },
                                main: {
                                    label: "Cancel",
                                    className: "btn-primary",
                                    callback: function () { }
                                }
                            }
                        });
                    });

                this.$addButton.on('click', function (e) {
                    e.preventDefault();

                    _self.rowAdd();
                });

                this.dialog.$cancel.on('click', function (e) {
                    e.preventDefault();
                    $.magnificPopup.close();
                });

                return this;
            },

            // =============
            // ROW FUNCTIONS
            // =============
            rowAdd: function () {
                this.$addButton.attr({
                    'disabled': 'disabled'
                });
                this.datatable.search('').draw();

                var actions,
                    data,
                    $row;

                actions = [
                    '<a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-editing save-row" data-toggle="tooltip" data-original-title="Save"><i class="icon md-check-all" aria-hidden="true"></i></a>',
                    '<a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-editing cancel-row" data-toggle="tooltip" data-original-title="Delete"><i class="icon md-close" aria-hidden="true"></i></a>',
                    '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-default edit-row" data-toggle="tooltip" data-original-title="Edit" hidden><i class="icon md-edit" aria-hidden="true"></i></a>',
                    '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-default remove-row" data-toggle="tooltip" data-original-title="Remove" hidden><i class="icon md-delete" aria-hidden="true"></i></a>'
                ].join(' ');

                data = this.datatable.row.add({ "SID": 0, "IDROLE": "", "ROLENAME": "", "ISACTIVE": 0 });
                $row = this.datatable.row(data[0]).nodes().to$();

                $row
                    .addClass('adding')
                    .find('td:last')
                    .addClass('actions').html(actions);

                //$row.find("td:eq(0)").hide();
                this.rowEdit($row);
                this.datatable.order([0, 'asc']).draw(); // always show fields
            },

            rowCancel: function ($row) {
                var _self = this,
                    $actions,
                    i,
                    data;

                $row
                    .find('td:first')
                    .addClass('details-control');

                if ($row.hasClass('adding')) {
                    this.rowRemove($row);
                } else {

                    data = this.datatable.row($row.get(0)).data();
                    this.datatable.row($row.get(0)).data(data);

                    $actions = $row.find('td.actions');
                    if ($actions.get(0)) {
                        this.rowSetActionsDefault($row);
                    }

                    this.datatable.draw();
                }
            },

            rowEdit: function ($row) {
                var _self = this,
                    data;
                data = this.datatable.row($row.get(0)).data();
                console.log(data);
                var arrdata = $.map(data, function (el) { return el; });
                //console.log(arrdata);
                var content;
                $row.children('td').each(function (i) {
                    var $this = $(this);

                    if (i === 1 ){
                        content = data.IDROLE;
                    }
                    else if (i === 2) {
                        content = data.ROLENAME;
                    }
                    else if (i === 3) {
                        content = data.ISACTIVE;
                    }

                    if ($this.hasClass('actions')) {
                        _self.rowSetActionsEditing($row);
                    } else {
                        if (i == 0) {
                            $this.removeClass("details-control");
                            $
                        }
                        else if (i > 0) {
                            $this.html('<input type="text" class="form-control input-block" value="' + content + '"/><div data-dte-e="msg-error" class="DTE_Field_Error" style="display: none;">Input not valid</div>');
                        }
                        
                        if ($(this).parent().children().index($(this)) === 3) {
                            var checked = content == 1 ? true : false;
                            if (checked) {
                                $this.html('<input type="checkbox" value="' + content + '" class="editor-active" checked />');
                            } else {
                                $this.html('<input type="checkbox" value="' + content + '" class="editor-active" />');
                            }
                        }
                    }
                });
            },

            rowSave: function ($row) {
                var _self = this,
                    $actions,
                    values = [];

                //Get All Value From Table Cell 
                values = $row.find('td').map(function () {
                    var $this = $(this);

                    if ($this.hasClass('actions')) {
                        return _self.datatable.cell(this).data();
                    } else {
                        if ($(this).parent().children().index($(this)) === 3) {
                            console.log("value : " + $("input[type='checkbox']").is(":checked"));
                            return $this.find("input[type='checkbox']").is(":checked") ? 1 : 0;
                        }
                        return $.trim($this.find('input').val());
                    }
                });

                console.log(values);

                if (values[1] === "" || values[2] === "") { //Empty Data Validation
                    bootbox.dialog({
                        message: "Please check your data, Colomn cannot be empty...!!!",
                        title: "WARNING !",
                        buttons: {
                            danger: {
                                label: "Ok",
                                className: "btn-primary"
                            }
                        }
                    });
                }
                else {
                    $row
                        .find('td:first')
                        .addClass('details-control');

                    _self.rowSetActionsDefault($row);
                    if ($row.hasClass('adding')) {
                        this.$addButton.removeAttr('disabled');
                        $row.removeClass('adding');
                    }

                    $row.find('td').map(function () {
                        var $this = $(this);

                        if ($this.hasClass('actions')) {
                            _self.rowSetActionsDefault($row);
                        } else {
                            $this.addClass('added');
                        }
                    });

                    var rolemaster = {};
                    if (values[4].SID !== 0) {
                        rolemaster.SID = values[4].SID;
                        rolemaster.IDROLE = values[1];
                        rolemaster.ROLENAME = values[2];
                        rolemaster.ISACTIVE = values[3];
                        $.ajax({
                            type: "POST",
                            url: "/RoleMaster/Edit",
                            data: '{rolemaster: ' + JSON.stringify(rolemaster) + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json"
                        });
                    } else {
                        rolemaster.IDROLE = values[1];
                        rolemaster.ROLENAME = values[2];
                        rolemaster.ISACTIVE = values[3];
                        $.ajax({
                            type: "POST",
                            url: "../RoleMaster/Create",
                            data: '{rolemaster: ' + JSON.stringify(rolemaster) + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json"
                        });
                    }
                    $actions = $row.find('td.actions');
                    if ($actions.get(0)) {
                        this.rowSetActionsDefault($row);
                    }

                    this.datatable.draw();

                    $('#tbldata').DataTable().ajax.reload();
                }
            },

            rowRemove: function ($row) {
                if ($row.hasClass('adding')) {
                    this.$addButton.removeAttr('disabled');
                }

                this.datatable.row($row.get(0)).remove().draw();
            },

            rowSetActionsEditing: function ($row) {
                $row.find('.on-editing').removeAttr('hidden');
                $row.find('.on-default').attr('hidden', true);
            },

            rowSetActionsDefault: function ($row) {
                $row.find('.on-editing').attr('hidden', true);
                $row.find('.on-default').removeAttr('hidden');
            }
        };

        $(function () {
            Table.initialize();
            // Add event listener for opening and closing details
            $('#tbldata tbody').on('click', 'td.details-control', function () {
                var tr = $(this).closest('tr');
                var row = Table.datatable.row(tr);
                console.log(row);

                if (row.child.isShown()) {
                    // This row is already open - close it
                    row.child.hide();
                    tr.removeClass('shown');
                }
                else {
                    // Open this row
                    row.child(format(tr.find('.SID').html())).show();

                    CreateTableSubData(tr.find('.SID').html());
                    tr.addClass('shown');
                }
            });

            function format(IDROLE) {
                var content = '<br/><div style="width:900px"><div class="row"><div class="col-xs-12 col-md-6"><div class="m-b-15">';
                content += '<button id="btnSaveSubData_' + IDROLE +  '" class="btn btn-success" type="button"><i class="fa fa-save" aria-hidden="true"></i> Save</button>';
                content += '</div></div></div>';

                content += '<table class="table table-bordered table-hover table-striped" id="tblsubdata_' + IDROLE + '"><thead>';
                content += '<tr><th style="width:150px;">MENU</th>';
                content += '<th style="text-align:center;width:50px;">CAN READ<br/><input type="checkbox" id="chkall_canread"></th>';
                content += '<th style="text-align:center;width:50px;">CAN ADD<br/><input type="checkbox" id="chkall_canadd"></th>';
                content += '<th style="text-align:center;width:50px;">CAN EDIT<br/><input type="checkbox" id="chkall_canedit"></th>';
                content += '<th style="text-align:center;width:50px;">CAN DELETE<br/><input type="checkbox" id="chkall_candelete"></th>';
                content += '</tr></thead><tbody></tbody>';
                content += '</table></div>';

                return content;
            }

            function CreateTableSubData(IDROLE) {
                var table = document.getElementById("tblsubdata_" + IDROLE).getElementsByTagName('tbody')[0];

                var rowcount = table.rows.length;

                $.getJSON("/RoleMenu/GetRoleMenu",
                    {
                        "IDROLE": IDROLE
                    },
                    function (response) {
                        var data = response.data;
                        var newRow, newCellMenu, newCellCanRead, newCellCanAdd, newCellCanEdit, newCellCanDelete; 
                        var isCanRead, isCanAdd, isCanEdit, isCanDelete;
                        for (var i = 0; i < data.length; i++) {
                            newRow = table.insertRow(rowcount);

                            if (data[i].STATUSMENU.toString() == 'Root') {
                                newCellMenu = newRow.insertCell(0);
                                newCellMenu.colSpan = 5;
                                newCellMenu.innerHTML = "<div style='font-weight:bold;padding:5px;'> ROOT MENU : " + data[i].DESCRIPTION + "</div>";
                            }
                            else if (data[i].STATUSMENU.toString() == 'Sub Root') {
                                newCellMenu = newRow.insertCell(0);
                                newCellMenu.colSpan = 5;
                                newCellMenu.innerHTML = "<div style='font-weight:bold;margin-left:10px;padding:5px'> SUB ROOT MENU : " + data[i].DESCRIPTION + "</div>";
                            }
                            else {
                                newCellMenu = newRow.insertCell(0);
                                newCellMenu.innerHTML = "<span style='margin-left:30px;'>" + data[i].DESCRIPTION + "</span>";
                                
                                newCellCanRead = newRow.insertCell(1);
                                newCellCanRead.innerHTML = data[i].ISCANREAD == "1" ? "<input type='checkbox' id='chk_canread_" + data[i].IDMENU + "_" + data[i].IDMENUPARENT + "' checked='true'/>" : "<input type='checkbox' id='chk_canread_" + data[i].IDMENU + "_" + data[i].IDMENUPARENT + "'/>";
                                newCellCanRead.className = "center";

                                newCellCanAdd = newRow.insertCell(2);
                                newCellCanAdd.innerHTML = data[i].ISCANADD == "1" ? "<input type='checkbox' id='chk_canadd_" + data[i].IDMENU + "_" + data[i].IDMENUPARENT + "' checked='true'/>" : "<input type='checkbox' id='chk_canadd_" + data[i].IDMENU + "_" + data[i].IDMENUPARENT + "'/>";
                                newCellCanAdd.className = "center";

                                newCellCanEdit = newRow.insertCell(3);
                                newCellCanEdit.innerHTML = data[i].ISCANEDIT == "1" ? "<input type='checkbox' id='chk_canedit_" + data[i].IDMENU + "_" + data[i].IDMENUPARENT + "' checked='true'/>" : "<input type='checkbox' id='chk_canedit_" + data[i].IDMENU + "_" + data[i].IDMENUPARENT + "'/>";
                                newCellCanEdit.className = "center";

                                newCellCanDelete = newRow.insertCell(4);
                                newCellCanDelete.innerHTML = data[i].ISCANDELETE == "1" ? "<input type='checkbox' id='chk_candelete_" + data[i].IDMENU + "_" + data[i].IDMENUPARENT + "' checked='true'/>" : "<input type='checkbox' id='chk_candelete_" + data[i].IDMENU + "_" + data[i].IDMENUPARENT + "'/>";
                                newCellCanDelete.className = "center";
                            }

                            rowcount++;
                        }
                    });

                $("#chkall_canread").change(function () {
                    var table = $("#tblsubdata_" + IDROLE + " tbody");
                    table.find('tr').each(function (i, el) {
                        var $tds = $(this).find('td');
                        
                        if ($tds.eq(1).length > 0) {
                            if ($("#chkall_canread").is(":checked")) {
                                $tds.eq(1)[0].children[0].checked = true;
                            }
                            else {
                                $tds.eq(1)[0].children[0].checked = false;
                            }
                        }
                    });
                });

                $("#chkall_canadd").change(function () {
                    var table = $("#tblsubdata_" + IDROLE + " tbody");
                    table.find('tr').each(function (i, el) {
                        var $tds = $(this).find('td');

                        if ($tds.eq(1).length > 0) {
                            if ($("#chkall_canadd").is(":checked")) {
                                $tds.eq(2)[0].children[0].checked = true;
                            }
                            else {
                                $tds.eq(2)[0].children[0].checked = false;
                            }
                        }
                    });
                });

                $("#chkall_canedit").change(function () {
                    var table = $("#tblsubdata_" + IDROLE + " tbody");
                    table.find('tr').each(function (i, el) {
                        var $tds = $(this).find('td');

                        if ($tds.eq(1).length > 0) {
                            if ($("#chkall_canedit").is(":checked")) {
                                $tds.eq(3)[0].children[0].checked = true;
                            }
                            else {
                                $tds.eq(3)[0].children[0].checked = false;
                            }
                        }
                    });
                });

                $("#chkall_candelete").change(function () {
                    var table = $("#tblsubdata_" + IDROLE + " tbody");
                    table.find('tr').each(function (i, el) {
                        var $tds = $(this).find('td');

                        if ($tds.eq(1).length > 0) {
                            if ($("#chkall_candelete").is(":checked")) {
                                $tds.eq(4)[0].children[0].checked = true;
                            }
                            else {
                                $tds.eq(4)[0].children[0].checked = false;
                            }
                        }
                    });
                });
                
                $("#btnSaveSubData_" + IDROLE).click(function () {
                    var arrdata = [];

                    var table = $("#tblsubdata_" + IDROLE + " tbody");
                    table.find('tr').each(function (i, el) {
                        var $tds = $(this).find('td');

                        if ($tds.eq(1).length > 0) {
                            var id = $tds.eq(1)[0].children[0].id;
                            var chkISCANREAD = $tds.eq(1)[0].children[0];
                            var chkISCANADD = $tds.eq(2)[0].children[0];
                            var chkISCANEDIT = $tds.eq(3)[0].children[0];
                            var chkISCANDELETE = $tds.eq(4)[0].children[0];
                            
                            arrdata.push({ "IDMENU": id.split('_')[2], "IDROLE": IDROLE, "ISCANREAD": chkISCANREAD.checked ? 1 : 0, "ISCANADD": chkISCANADD.checked ? 1 : 0, "ISCANEDIT": chkISCANEDIT.checked ? 1 : 0, "ISCANDELETE": chkISCANDELETE.checked ? 1 : 0 });
                        }
                    });
                    
                    $.ajax({
                        type: "POST",
                        url: "/RoleMenu/SaveRoleMenu",
                        data: JSON.stringify({ IDROLE: IDROLE, RoleMenu: arrdata }),
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            console.log(data);
                            location.reload();
                        },
                        failure: function (errMsg) {
                            alert(errMsg);
                        }
                    });
                });

            }

            function CreateDatatableSubData(IDROLE)
            {
                var SubTable = {
                    options: {
                        addButton: '#btnAddSubData',
                        table: '#tblsubdata',
                        dialog: {
                            wrapper: '#dialog',
                            cancelButton: '#dialogCancel',
                            confirmButton: '#dialogConfirm'
                        }
                    },

                    initialize: function () {
                        this
                            .setVars()
                            .build()
                            .events();
                    },

                    setVars: function () {
                        this.$table = $(this.options.table);
                        this.$addButton = $(this.options.addButton);

                        // dialog
                        this.dialog = {};
                        this.dialog.$wrapper = $(this.options.dialog.wrapper);
                        this.dialog.$cancel = $(this.options.dialog.cancelButton);
                        this.dialog.$confirm = $(this.options.dialog.confirmButton);

                        return this;
                    },

                    build: function () {
                        this.datatable = this.$table.DataTable({
                            //dom: "Blfrtip",
                            ajax: "/RoleMenu/GetRoleMenu?IDROLE=" + IDROLE,
                            columns: [
                                { data: "IDMENU" },
                                { data: "DESCRIPTION"},
                                {
                                    data: "ISCANREAD",
                                    render: function (data, type, row) {
                                        if (type === 'display' && data === 1) {
                                            return '<input type="checkbox" class="editor-active" checked>';
                                        }
                                        else
                                            return '<input type="checkbox" class="editor-active">';
                                    },
                                    className: "center"
                                },
                                {
                                    data: "ISCANADD",
                                    render: function (data, type, row) {
                                        if (type === 'display' && data === 1) {
                                            return '<input type="checkbox" class="editor-active" checked>';
                                        }
                                        else
                                            return '<input type="checkbox" class="editor-active">';
                                    },
                                    className: "center"
                                },
                                {
                                    data: "ISCANEDIT",
                                    render: function (data, type, row) {
                                        if (type === 'display' && data === 1) {
                                            return '<input type="checkbox" class="editor-active" checked>';
                                        }
                                        else
                                            return '<input type="checkbox" class="editor-active">';
                                    },
                                    className: "center"
                                },
                                {
                                    data: "ISCANDELETE",
                                    render: function (data, type, row) {
                                        if (type === 'display' && data === 1) {
                                            return '<input type="checkbox" class="editor-active" checked>';
                                        }
                                        else
                                            return '<input type="checkbox" class="editor-active">';
                                    }
                                //    "targets": [6],
                                //    "visible": false,
                                //    "searchable": false
                                }
                            ]
                            , "paging": false
                            , "searching": false
                            , "ordering": false
                        });

                        window.dt = this.datatable;

                        return this;
                    },

                    events: function () {
                        var _self = this;

                        this.$table
                            .on('click', 'a.save-row', function (e) {
                                e.preventDefault();

                                _self.rowSave($(this).closest('tr'));
                            })
                            .on('click', 'a.cancel-row', function (e) {
                                e.preventDefault();

                                _self.rowCancel($(this).closest('tr'));
                            })
                            .on('click', 'a.edit-row', function (e) {
                                e.preventDefault();
                                _self.rowEdit($(this).closest('tr'));
                            })
                            .on('click', 'a.remove-row', function (e) {
                                e.preventDefault();

                                var $row = $(this).closest('tr');
                                bootbox.dialog({
                                    message: "Are you sure that you want to delete this row?",
                                    title: "ARE YOU SURE?",
                                    buttons: {
                                        danger: {
                                            label: "Confirm",
                                            className: "btn-danger",
                                            callback: function () {
                                                var SID = $row.find(".SID").html();
                                                $.ajax({
                                                    type: "POST",
                                                    url: "/UnitOfMeasure/Delete",
                                                    data: '{id: ' + SID + '}',
                                                    contentType: "application/json; charset=utf-8",
                                                    dataType: "json",
                                                    success: function () {
                                                        _self.rowRemove($row);
                                                    }
                                                });
                                                _self.rowRemove($row);
                                            }
                                        },
                                        main: {
                                            label: "Cancel",
                                            className: "btn-primary",
                                            callback: function () { }
                                        }
                                    }
                                });
                            });

                        this.$addButton.on('click', function (e) {
                            e.preventDefault();

                            _self.rowAdd();
                        });

                        this.dialog.$cancel.on('click', function (e) {
                            e.preventDefault();
                            $.magnificPopup.close();
                        });

                        return this;
                    },

                    // =============
                    // ROW FUNCTIONS
                    // =============
                    rowAdd: function () {
                        this.$addButton.attr({
                            'disabled': 'disabled'
                        });
                        this.datatable.search('').draw();

                        var actions,
                            data,
                            $row;

                        actions = [
                            '<a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-editing save-row" data-toggle="tooltip" data-original-title="Save"><i class="icon md-check-all" aria-hidden="true"></i></a>',
                            '<a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-editing cancel-row" data-toggle="tooltip" data-original-title="Delete"><i class="icon md-close" aria-hidden="true"></i></a>',
                            '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-default edit-row" data-toggle="tooltip" data-original-title="Edit" hidden><i class="icon md-edit" aria-hidden="true"></i></a>',
                            '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-default remove-row" data-toggle="tooltip" data-original-title="Remove" hidden><i class="icon md-delete" aria-hidden="true"></i></a>'
                        ].join(' ');

                        data = this.datatable.row.add({ "SID": 0, "IDROLE": "", "ROLENAME": "", "ISACTIVE": 0 });
                        $row = this.datatable.row(data[0]).nodes().to$();

                        $row
                            .addClass('adding')
                            .find('td:last')
                            .addClass('actions').html(actions);

                        //$row.find("td:eq(0)").hide();
                        this.rowEdit($row);
                        this.datatable.order([0, 'asc']).draw(); // always show fields
                    },

                    rowCancel: function ($row) {
                        var _self = this,
                            $actions,
                            i,
                            data;
                        

                        if ($row.hasClass('adding')) {
                            this.rowRemove($row);
                        } else {

                            data = this.datatable.row($row.get(0)).data();
                            this.datatable.row($row.get(0)).data(data);

                            $actions = $row.find('td.actions');
                            if ($actions.get(0)) {
                                this.rowSetActionsDefault($row);
                            }

                            this.datatable.draw();
                        }
                    },

                    rowEdit: function ($row) {
                        var _self = this,
                            data;
                        data = this.datatable.row($row.get(0)).data();
                        console.log(data);
                        var arrdata = $.map(data, function (el) { return el; });
                        //console.log(arrdata);
                        var content;
                        $row.children('td').each(function (i) {
                            var $this = $(this);

                            if (i === 1) {
                                content = data.IDROLE;
                            }
                            else if (i === 2) {
                                content = data.ROLENAME;
                            }
                            else if (i === 3) {
                                content = data.ISACTIVE;
                            }

                            if ($this.hasClass('actions')) {
                                _self.rowSetActionsEditing($row);
                            } else {
                                if (i == 0) {
                                    $this.removeClass("details-control");
                                }
                                else if (i > 0) {
                                    $this.html('<input type="text" class="form-control input-block" value="' + content + '"/><div data-dte-e="msg-error" class="DTE_Field_Error" style="display: none;">Input not valid</div>');
                                }

                                if ($(this).parent().children().index($(this)) === 3) {
                                    var checked = content == 1 ? true : false;
                                    if (checked) {
                                        $this.html('<input type="checkbox" value="' + content + '" class="editor-active" checked />');
                                    } else {
                                        $this.html('<input type="checkbox" value="' + content + '" class="editor-active" />');
                                    }
                                }
                            }
                        });
                    },

                    rowSave: function ($row) {
                        var _self = this,
                            $actions,
                            values = [];

                        //Get All Value From Table Cell 
                        values = $row.find('td').map(function () {
                            var $this = $(this);

                            if ($this.hasClass('actions')) {
                                return _self.datatable.cell(this).data();
                            } else {
                                if ($(this).parent().children().index($(this)) === 2) {
                                    console.log("value : " + $("input[type='checkbox']").is(":checked"));
                                    return $this.find("input[type='checkbox']").is(":checked") ? 1 : 0;
                                }
                                return $.trim($this.find('input').val());
                            }
                        });

                        console.log(values);

                        if (values[0] === "" || values[1] === "") { //Empty Data Validation
                            bootbox.dialog({
                                message: "Please check your data, Colomn cannot be empty...!!!",
                                title: "WARNING !",
                                buttons: {
                                    danger: {
                                        label: "Ok",
                                        className: "btn-primary"
                                    }
                                }
                            });
                        }
                        else {
                            _self.rowSetActionsDefault($row);
                            if ($row.hasClass('adding')) {
                                this.$addButton.removeAttr('disabled');
                                $row.removeClass('adding');
                            }

                            $row.find('td').map(function () {
                                var $this = $(this);

                                if ($this.hasClass('actions')) {
                                    _self.rowSetActionsDefault($row);
                                } else {
                                    $this.addClass('added');
                                }
                            });

                            var unitofmeasure = {};
                            if (values[3].SID !== 0) {
                                unitofmeasure.SID = values[3].SID;
                                unitofmeasure.IDUOM = values[0];
                                unitofmeasure.UOMNAME = values[1];
                                unitofmeasure.ISACTIVE = values[2];
                                $.ajax({
                                    type: "POST",
                                    url: "/UnitOfMeasure/Edit",
                                    data: '{unitofmeasure: ' + JSON.stringify(unitofmeasure) + '}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json"
                                });
                            } else {
                                unitofmeasure.IDUOM = values[0];
                                unitofmeasure.UOMNAME = values[1];
                                unitofmeasure.ISACTIVE = values[2];
                                $.ajax({
                                    type: "POST",
                                    url: "../UnitOfMeasure/Create",
                                    data: '{unitofmeasure: ' + JSON.stringify(unitofmeasure) + '}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json"
                                });
                            }
                            $actions = $row.find('td.actions');
                            if ($actions.get(0)) {
                                this.rowSetActionsDefault($row);
                            }

                            this.datatable.draw();

                            $('#tbldata').DataTable().ajax.reload();
                        }
                    },

                    rowRemove: function ($row) {
                        if ($row.hasClass('adding')) {
                            this.$addButton.removeAttr('disabled');
                        }

                        this.datatable.row($row.get(0)).remove().draw();
                    },

                    rowSetActionsEditing: function ($row) {
                        $row.find('.on-editing').removeAttr('hidden');
                        $row.find('.on-default').attr('hidden', true);
                    },

                    rowSetActionsDefault: function ($row) {
                        $row.find('.on-editing').attr('hidden', true);
                        $row.find('.on-default').removeAttr('hidden');
                    }
                };

                SubTable.initialize();


                $('#btnSaveSubData').on('click', function (e) {
                    //var data = SubTable.datatable.rows().data();
                    //data.each(function (value, index) {
                    //    console.log(value);
                    //});
                    var i = 0;
                    $('#tblsubdata > tbody  > tr').each(function () {
                        if (i < 2) 
                            console.log($(this).find('input[type=checkbox]'));

                        i++;
                    });
                });
            }
            
        });

    }).apply(this, [jQuery]);


})(document, window, jQuery);
