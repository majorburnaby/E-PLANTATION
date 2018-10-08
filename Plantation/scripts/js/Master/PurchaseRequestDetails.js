/*!
 * remark (http://getbootstrapadmin.com/remark)
 * Copyright 2017 amazingsurge
 * Licensed under the Themeforest Standard Licenses
 */
(function (document, window, $) {
    //'use strict';

    //var Site = window.Site;


    //$(document).ready(function ($) {
    //    Site.run();
    //});

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
                    //ajax: "/PurchaseRequestDetails/GetPurchaseDetail" + "?IDPURCHASEREQUEST=" + 1,
                    ajax: {
                        "url": "../../PurchaseRequestDetails/GetPurchaseDetail" + "?IDPURCHASEREQUEST=" + $('#SID').val(),
                        'contentType': 'application/json'
                    },
                    columns: [
                        { "data": "SID" },
                        { "data": "STOCKNAME" },
                        { "data": "QUANTITY" },
                        { "data": "UOMNAME" },
                        { "data": "EXPECTDATE"},
                        //{
                        //    "data": "EXPECTDATE", type: 'datetime', 
                        //    format: 'D/M/YYYY',
                        //    def: function () { return new Date(); }
                        //},
                        { "data": "MANAGEBYNAME" },
                        { "data": "ESTIMATEPRICE" },
                        { "data": "REMARK" },
                        { "data": "APPROVEQUANTITY" }
                        , { "data": null, "defaultContent": '' }
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
                            "render": function (data, type, row) {
                                var date = new Date(data);
                                return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
                            },
                            "targets": 4
                        }
                        , {
                            "className": "actions center",
                            "width": "100px",
                            "targets": 9, "data": "SID", "render": function (data, type, full, meta) {
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
                                            url: "../../PurchaseRequestDetails/Delete",
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
                
                data = this.datatable.row.add({ "SID": 0, "IDPURCHASEREQUEST": $('#SID').val(), "ITEMCODE": 0, "IDSTOCK" : 0, "STOCKNAME":"", "QUANTITY": 0, "UOM": 0, "IDUOM" : "", "UOMNAME" : "", "EXPECTDATE": "", "MANAGEBY": 0, "IDMANAGEBY" : "", "MANAGEBYNAME" : "", "ESTIMATEPRICE": 0, "REMARK": "", "APPROVEQUANTITY": 0 });
                //data = this.datatable.row.add({ "data": { "SID": 0, "IDPURCHASEREQUEST": $('#SID').val(), "ITEMCODE": 0, "IDSTOCK": 0, "STOCKNAME": "", "QUANTITY": 0, "UOM": 0, "IDUOM": "", "UOMNAME": "", "EXPECTDATE": "", "MANAGEBY": 0, "IDMANAGEBY": "", "MANAGEBYNAME": "", "ESTIMATEPRICE": 0, "REMARK": "", "APPROVEQUANTITY": 0 }});
                $row = this.datatable.row(data[0]).nodes().to$();

                console.log(data);
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
                console.log(arrdata);
                var content;
                $row.children('td').each(function (i) {
                    var $this = $(this);

                    if (i === 0) {
                        content = arrdata[2];
                    }
                    else if (i === 1) {
                        content = arrdata[5];
                    }
                    else if (i === 2) {
                        content = arrdata[2];
                    }
                    else if (i === 3) {
                        content = arrdata[9];
                    }
                    else if (i === 4) {
                        content = arrdata[10];
                    }
                    else if (i === 5) {
                        content = arrdata[13];
                    }
                    else if (i === 6) {
                        content = arrdata[14];
                    }
                    else if (i === 7) {
                        content = arrdata[15];
                    }

                    if ($this.hasClass('actions')) {
                        _self.rowSetActionsEditing($row);
                    } else {
                        $this.html('<input type="text" class="form-control input-block" value="' + content + '"/>');

                        if ($(this).parent().children().index($(this)) === 0) {
                            $this.html('<select id="ddlStock" class="form-control input-sm"></select>');
                            $.getJSON('../../PurchaseRequestDetails/GetStockList', { CompanySite: $('#COMPANYSITE').val() }, function (json) {
                                $('#ddlStock').empty();
                                $('#ddlStock').append($('<option>').text("Select").attr('value', '0'));
                                $.each(json, function (i, obj) {
                                    var selected = (data.ITEMCODE == obj.SID) ? 'selected' : '';
                                    $('#ddlStock').append($('<option ' + selected + '>').text(obj.STOCKNAME).attr('value', obj.SID));
                                });

                            });
                        }
                        else if ($(this).parent().children().index($(this)) === 2) {
                            $this.html('<select id="ddlUOM" class="form-control input-sm"></select>');
                            $.getJSON('../../PurchaseRequestDetails/GetUnitOfMeasureList', function (json) {
                                $('#ddlUOM').empty();
                                $('#ddlUOM').append($('<option>').text("Select").attr('value', '0'));
                                $.each(json, function (i, obj) {
                                    var selected = (data.UOM == obj.SID) ? 'selected' : '';
                                    $('#ddlUOM').append($('<option ' + selected + '>').text(obj.UOMNAME).attr('value', obj.SID));
                                });

                            });
                        }
                        else if ($(this).parent().children().index($(this)) === 3) {
                            $this.html('<input type="text" class="form-control input-date" value="' + content + '"/>');
                            var date;
                            if (content == "") {
                                date = new Date();
                            } else {
                                date = new Date(content);
                            }

                            $('.input-date').datepicker({
                                format: 'mm/dd/yyyy',
                                autoclose: true
                            }).datepicker("setDate", (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                        }
                        else if ($(this).parent().children().index($(this)) === 4) {
                            $this.html('<select id="ddlManagedBy" class="form-control input-sm"></select>');
                            $.getJSON('../../PurchaseRequestDetails/GetParameterValueManageByList', function (json) {
                                $('#ddlManagedBy').empty();
                                $('#ddlManagedBy').append($('<option>').text("Select").attr('value', '0'));
                                $.each(json, function (i, obj) {
                                    var selected = (data.MANAGEBY == obj.SID) ? 'selected' : '';
                                    $('#ddlManagedBy').append($('<option ' + selected + '>').text(obj.PARAMETERVALUENAME).attr('value', obj.SID));
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
                            return $('#ddlStock').find(":selected").val();
                        }
                        else if ($(this).parent().children().index($(this)) === 2) {
                            return $('#ddlUOM').find(":selected").val();
                        }
                        else if ($(this).parent().children().index($(this)) === 4) {
                            return $('#ddlManagedBy').find(":selected").val();
                        }

                        return $.trim($this.find('input').val());
                    }
                });

                console.log(values);

                var requestdetails = {};

                if (values[0] == '0' || values[1] == '' || values[2] == '0' || values[3] == '' || values[4] == '0' || values[5] == '' || values[7] == '') { //Empty Data Validation
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
                    if (values[8].SID !== 0) {
                        requestdetails.SID = values[8].SID;
                        requestdetails.IDPURCHASEREQUEST = values[8].IDPURCHASEREQUEST;
                        requestdetails.ITEMCODE = values[0];
                        requestdetails.QUANTITY = values[1];
                        requestdetails.UOM = values[2];
                        requestdetails.EXPECTDATE = values[3];
                        requestdetails.MANAGEBY = values[4];
                        requestdetails.ESTIMATEPRICE = values[5];
                        requestdetails.REMARKS = values[6];
                        requestdetails.APPROVEQUANTITY = values[7];

                        $.ajax({
                            type: "POST",
                            url: "../../PurchaseRequestDetails/Edit",
                            data: '{PurchaseRequestDetails: ' + JSON.stringify(requestdetails) + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (e) {
                                _self.rowSetActionsDefault($row);

                                $actions = $row.find('td.actions');
                                if ($actions.get(0)) {
                                    Table.rowSetActionsDefault($row);
                                }

                                Table.datatable.draw();
                                $('#tbldata').DataTable().ajax.reload();
                            }
                        });
                    } else {
                        requestdetails.IDPURCHASEREQUEST = values[8].IDPURCHASEREQUEST;
                        requestdetails.ITEMCODE = values[0];
                        requestdetails.QUANTITY = values[1];
                        requestdetails.UOM = values[2];
                        requestdetails.EXPECTDATE = values[3];
                        requestdetails.MANAGEBY = values[4];
                        requestdetails.ESTIMATEPRICE = values[5];
                        requestdetails.REMARKS = values[6];
                        requestdetails.APPROVEQUANTITY = values[7];

                        $.ajax({
                            type: "POST",
                            url: "../../PurchaseRequestDetails/Create",
                            data: '{PurchaseRequestDetails: ' + JSON.stringify(requestdetails) + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                _self.rowSetActionsDefault($row);
                                if ($row.hasClass('adding')) {
                                    Table.$addButton.removeAttr('disabled');
                                    $row.removeClass('adding');
                                }

                                $actions = $row.find('td.actions');
                                if ($actions.get(0)) {
                                    Table.rowSetActionsDefault($row);
                                }

                                Table.datatable.draw();
                                $('#tbldata').DataTable().ajax.reload();
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
