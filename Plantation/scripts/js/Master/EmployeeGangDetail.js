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
                    ajax: "/EmployeeGangDetail/Data",
                    columns: [
                        { data: "EMPLOYEEGANGDETAIL.SID" },
                        { data: "EMPLOYEE.EMPLOYEENAME" }
                        , { data: null, "defaultContent": '' }
                    ],

                    "columnDefs": [
                        {
                            "targets": [0],
                            "visible": false,
                            "searchable": false
                        }, {
                            "className": "center",
                            "targets": 2
                        }
                        , {
                            "className": "actions center",
                            "width": "60px",
                            "targets": 2, "data": "SID", "render": function (data, type, full, meta) {
                                return '<div class="SID" style="display:none;">' + data.EMPLOYEEGANGDETAIL.SID + '</div><a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-default edit-row" data-toggle="tooltip" data-original-title="Edit"><i class="icon md-edit" aria-hidden="true"></i></a>'
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
                                            url: "../../EmployeeGangDetail/Delete",
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

                var actions,
                    data,
                    $row;

                actions = [
                    '<a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-editing save-row" data-toggle="tooltip" data-original-title="Save"><i class="icon md-check-all" aria-hidden="true"></i></a>',
                    '<a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-editing cancel-row" data-toggle="tooltip" data-original-title="Delete"><i class="icon md-close" aria-hidden="true"></i></a>',
                    '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-default edit-row" data-toggle="tooltip" data-original-title="Edit" hidden><i class="icon md-edit" aria-hidden="true"></i></a>',
                    '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-default remove-row" data-toggle="tooltip" data-original-title="Remove" hidden><i class="icon md-delete" aria-hidden="true"></i></a>'
                ].join(' ');
                
                data = this.datatable.row.add({ "EMPLOYEEGANGDETAIL": { "SID": 0, "EMPLOYEEGANG": $('#SID').val(), "EMPLOYEE": 0 }, "EMPLOYEE": { "EMPLOYEENAME": "" } });
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
                var arrdata = $.map(data, function (el) { return el; });
                var content;
                $row.children('td').each(function (i) {
                    var $this = $(this);

                    if (arrdata.length < 2) {
                        if (i === 0) {
                            content = arrdata[0].EMPLOYEE;
                        }
                    }
                    else {
                        if (i === 0) {
                            content = arrdata[1].EMPLOYEE;
                        }
                    }

                    if ($this.hasClass('actions')) {
                        _self.rowSetActionsEditing($row);
                    } else {
                        $this.html('<input type="text" class="form-control input-block" value="' + content + '"/>');

                        if ($(this).parent().children().index($(this)) === 0) {
                            $this.html('<select id="ddlEmployee" class="form-control input-sm"></select>');
                            $.getJSON('../../EmployeeGangDetail/GetEmployeeList', function (json) {
                                $('#ddlEmployee').empty();
                                $('#ddlEmployee').append($('<option>').text("Select").attr('value', '0'));
                                $.each(json, function (i, obj) {
                                    var selected = (data.EMPLOYEEGANGDETAIL.EMPLOYEE == obj.SID) ? 'selected' : '';
                                    $('#ddlEmployee').append($('<option ' + selected + '>').text(obj.EMPLOYEENAME).attr('value', obj.SID));
                                });
                                
                            });
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
                        if ($(this).parent().children().index($(this)) === 0) {
                            return $('#ddlEmployee').find(":selected").val();
                        }

                        return $.trim($this.find('input').val());
                    }
                });
                
                if (values[0] == '') { //Empty Data Validation
                    bootbox.dialog({
                        message: "Please check your data, Column cannot be empty...!!!",
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

                    var EmployeeGangDetail = {};
                    if (values[2].EMPLOYEEGANGDETAIL.SID !== 0) {
                        $.ajax({
                            type: "GET",
                            url: "/EmployeeGangDetail/GetTotalEmployeeGangDetail",
                            data: { EmployeeGang: $('#SID').val() },
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                if ((parseInt(data) + parseInt(values[1])) <= parseInt($('#HECTARAGE_TEMP').val())) {
                                    _self.rowSetActionsDefault($row);

                                    blockusage.SID = values[2].BLOCKUSAGE.SID;
                                    blockusage.BLOCKMASTER = values[2].BLOCKUSAGE.BLOCKMASTER;
                                    blockusage.USAGE = values[0];
                                    blockusage.HECTARAGE = values[1];
                                    $.ajax({
                                        type: "POST",
                                        url: "/BlockUsage/Edit",
                                        data: '{BlockUsage: ' + JSON.stringify(blockusage) + '}',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json"
                                    });

                                    $actions = $row.find('td.actions');
                                    if ($actions.get(0)) {
                                        Table.rowSetActionsDefault($row);
                                    }

                                    Table.datatable.draw();
                                    $('#tbldata').DataTable().ajax.reload();
                                }
                                else {
                                    bootbox.alert("Block usage can not exceed block master hectarage");
                                }
                            }
                        })
                    } else {
                        $.ajax({
                            type: "GET",
                            url: "/BlockUsage/GetTotalBlockUsage",
                            data: {BlockMaster: $('#SID').val() },
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                if ((parseInt(data) + parseInt(values[1])) <= parseInt($('#HECTARAGE_TEMP').val())) {
                                    _self.rowSetActionsDefault($row);
                                    if ($row.hasClass('adding')) {
                                        Table.$addButton.removeAttr('disabled');
                                        $row.removeClass('adding');
                                    }

                                    blockusage.BLOCKMASTER = values[2].BLOCKUSAGE.BLOCKMASTER;
                                    blockusage.USAGE = values[0];
                                    blockusage.HECTARAGE = values[1];
                                    $.ajax({
                                        type: "POST",
                                        url: "/BlockUsage/Create",
                                        data: '{BlockUsage: ' + JSON.stringify(blockusage) + '}',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json"
                                    });

                                    $actions = $row.find('td.actions');
                                    if ($actions.get(0)) {
                                        Table.rowSetActionsDefault($row);
                                    }

                                    Table.datatable.draw();
                                    $('#tbldata').DataTable().ajax.reload();
                                }
                                else {
                                    bootbox.alert("Block usage can not exceed block master hectarage");
                                }
                            }
                        })
                    }
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
        });

    }).apply(this, [jQuery]);


})(document, window, jQuery);
