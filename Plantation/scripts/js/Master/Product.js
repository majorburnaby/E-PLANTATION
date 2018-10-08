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
    var editor;
    var option_crop = [];
    var option_loadtype = [];
    var option_uom = [];
    (function ($) {

        var EditableTable = {

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
                    //dom: "Bfrtip",
                    ajax: "/Product/Data",
                    columns: [
                        { data: "PRODUCT.SID" },
                        { data: "PRODUCT.IDPRODUCT" },
                        { data: "PRODUCT.PRODUCTNAME" },
                        { data: "CROP.CROPNAME", editField: "PRODUCT.CROP" },
                        { data: "LOADTYPE.LOADTYPENAME", editField: "PRODUCT.LOADTYPE" },
                        { data: "UNITOFMEASURE.UOMNAME", editField: "PRODUCT.UOM" }
                        , { data: null, "defaultContent": '' }
                    ],
                    "columnDefs": [
                        {
                            "targets": [0],
                            "visible": false,
                            "searchable": false
                        }, {
                            "className": "center",
                            "targets": 1
                        }, {
                            "className": "actions center",
                            "width": "60px",
                            "targets": 6, "data": "SID", "render": function (data, type, full, meta) {
                                return '<div class="SID" style="display:none;">' + data.PRODUCT.SID + '</div><a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-default edit-row" data-toggle="tooltip" data-original-title="Edit"><i class="icon md-edit" aria-hidden="true"></i></a>'
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
                                            url: "/Product/Delete",
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

                data = this.datatable.row.add({ "PRODUCT": { "SID": 0, "IDPRODUCT": "", "PRODUCTNAME": "", "CROP": "", "LOADTYPE": "", "UOM": "" }, "CROP": { "CROPNAME": "" }, "LOADTYPE": { "LOADTYPENAME": "" }, "UNITOFMEASURE": { "UOMNAME": "" } });
                $row = this.datatable.row(data[0]).nodes().to$();

                $row
                    .addClass('adding')
                    .find('td:last')
                    .addClass('actions').html(actions);

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

                    if (arrdata.length < 5) {
                        if (i === 0) {
                            content = arrdata[0].IDPRODUCT;
                        }
                        else if (i === 1) {
                            content = arrdata[0].PRODUCTNAME;
                        }
                        else if (i === 2) {
                            content = arrdata[0].CROP;
                        }
                        else if (i === 3) {
                            content = arrdata[0].LOADTYPE;
                        }
                        else if (i === 4) {
                            content = arrdata[0].UOM;
                        }
                    }
                    else {
                        if (i === 0) {
                            content = arrdata[1].IDPRODUCT;
                        }
                        else if (i === 1) {
                            content = arrdata[1].PRODUCTNAME;
                        }
                        else if (i === 2) {
                            content = arrdata[1].CROP;
                        }
                        else if (i === 3) {
                            content = arrdata[1].LOADTYPE;
                        }
                        else if (i === 4) {
                            content = arrdata[1].UOM;
                        }
                    }

                    if ($this.hasClass('actions')) {
                        _self.rowSetActionsEditing($row);
                    } else {
                        $this.html('<input type="text" class="form-control input-block" value="' + content + '"/>');
                        if ($(this).parent().children().index($(this)) === 2) {
                            $this.html('<select id="ddlCrop" class="form-control input-sm" data-plugin="select2" style="width: 100%"></select>');
                            $.getJSON('GetCropList', function (json) {
                                $('#ddlCrop').empty();
                                $('#ddlCrop').append($('<option>').text("Select").attr('value', ''));
                                $.each(json, function (i, obj) {
                                    var selected = (data.PRODUCT.CROP === obj.SID) ? 'selected' : '';
                                    $('#ddlCrop').append($('<option ' + selected + '>').text(obj.CROPNAME).attr('value', obj.SID));
                                });
                            });
                        }

                        if ($(this).parent().children().index($(this)) === 3) {
                            $this.html('<select id="ddlLoadType" class="form-control input-sm" data-plugin="select2" style="width: 100%"></select>');
                            $.getJSON('GetLoadTypeList', function (json) {
                                $('#ddlLoadType').empty();
                                $('#ddlLoadType').append($('<option>').text("Select").attr('value', ''));
                                $.each(json, function (i, obj) {
                                    var selected = (data.PRODUCT.LOADTYPE === obj.SID) ? 'selected' : '';
                                    $('#ddlLoadType').append($('<option ' + selected + '>').text(obj.LOADTYPENAME).attr('value', obj.SID));
                                });
                            });
                        }

                        if ($(this).parent().children().index($(this)) === 4) {
                            $this.html('<select id="ddlUnitOfMeasure" class="form-control input-sm" data-plugin="select2" style="width: 100%"></select>');
                            $.getJSON('GetUnitOfMeasureList', function (json) {
                                $('#ddlUnitOfMeasure').empty();
                                $('#ddlUnitOfMeasure').append($('<option>').text("Select").attr('value', ''));
                                $.each(json, function (i, obj) {
                                    var selected = (data.PRODUCT.UOM === obj.SID) ? 'selected' : '';
                                    $('#ddlUnitOfMeasure').append($('<option ' + selected + '>').text(obj.UOMNAME).attr('value', obj.SID));
                                });
                            });
                        }
                    }
                    $("#ddlCrop").select2({ allowClear: true });
                    $("#ddlLoadType").select2({ allowClear: true });
                    $("#ddlUnitOfMeasure").select2({ allowClear: true });
                });
            },

            rowSave: function ($row) {
                var _self = this,
                    $actions,
                    values = [];

                values = $row.find('td').map(function () {
                    var $this = $(this);

                    if ($this.hasClass('actions')) {
                        return _self.datatable.cell(this).data();
                    } else {
                        if ($(this).parent().children().index($(this)) === 2) {
                            return $('#ddlCrop').find(":selected").val();
                        }
                        if ($(this).parent().children().index($(this)) === 3) {
                            return $('#ddlLoadType').find(":selected").val();
                        }
                        if ($(this).parent().children().index($(this)) === 4) {
                            return $('#ddlUnitOfMeasure').find(":selected").val();
                        }
                        return $.trim($this.find('input').val());
                    }
                });

                if (values[0] === "" || values[1] === "" || values[2] === "" || values[3] === "" || values[4] === "") {
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
                    //this.datatable.row($row.get(0)).data(values);
                    var product = {};
                    if (values[5].PRODUCT.SID !== 0) {
                        product.SID = values[5].PRODUCT.SID;
                        product.IDPRODUCT = values[0];
                        product.PRODUCTNAME = values[1];
                        product.CROP = values[2];
                        product.LOADTYPE = values[3];
                        product.UOM = values[4];
                        $.ajax({
                            type: "POST",
                            url: "/Product/Edit",
                            data: '{product: ' + JSON.stringify(product) + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json"
                        });
                    } else {
                        product.IDPRODUCT = values[0];
                        product.PRODUCTNAME = values[1];
                        product.CROP = values[2];
                        product.LOADTYPE = values[3];
                        product.UOM = values[4];
                        $.ajax({
                            type: "POST",
                            url: "/Product/Create",
                            data: '{product: ' + JSON.stringify(product) + '}',
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
            EditableTable.initialize();
        });

    }).apply(this, [jQuery]);


})(document, window, jQuery);
