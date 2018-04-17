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
    var option_parameter = [];
    (function ($) {
        //Editor
        editor = new $.fn.dataTable.Editor({
            ajax: "/ParameterValue/Data",
            table: "#tbldata",
            fields: [{
                name: "PARAMETERVALUE.IDPARAMETERVALUE"
            }, {
                name: "PARAMETERVALUE.PARAMETERVALUENAME"
            }, {
                name: "PARAMETERVALUE.PARAMETER",
                type: "select",
                "ipOpts": option_parameter
            }
            ]
        });

        editor.field('PARAMETERVALUE.PARAMETER').input().addClass('form-control input-sm');

        $.getJSON('GetParameterList', function (json) {
            $.each(json, function (i, obj) {
                option_parameter.push({
                    value: obj.SID,
                    label: obj.PARAMETERNAME
                });

                editor.field('PARAMETERVALUE.PARAMETER').update(option_parameter);
            });
        });

        // Activate the bubble editor on click of a table cell
        $('#tbldata').on('click', 'tbody td', function (e) {
            if ($(this).index() < 3) {
                //Make Editor works only if table cell does not have any input form control (show when add row)
                if (!$(this).find('input').length) {
                    if (!$(this).find('select').length) {
                        editor.inline(this, {
                            onBlur: 'submit'
                        });
                    }
                }
            }
        });

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
                    ajax: "/ParameterValue/Data",
                    columns: [
                        { data: "PARAMETERVALUE.SID" },
                        { data: "PARAMETERVALUE.IDPARAMETERVALUE" },
                        { data: "PARAMETERVALUE.PARAMETERVALUENAME" },
                        { data: "PARAMETER.PARAMETERNAME", editField: "PARAMETERVALUE.PARAMETER" }
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
                            "width": "100px",
                            "targets": 4, "data": "SID", "render": function (data, type, full, meta) {
                                return '<div class="SID" style="display:none;">' + data.PARAMETERVALUE.SID + '</div><a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-default edit-row" data-toggle="tooltip" data-original-title="Edit"><i class="icon md-edit" aria-hidden="true"></i></a>'
                                    + ' <a href= "#" type= "button" class="btn btn-sm btn-icon btn-pure btn-default on-default remove-row" data- toggle="tooltip" data- original - title="Remove" > <i class="icon md-delete" aria-hidden="true"></i></a > '
                                    + ' <a href="#" type= "button" class="btn btn-sm btn-icon btn-pure btn-default on-editing save-row" data-toggle="tooltip" data-original-title="Save" hidden><i class="icon md-check-all" aria-hidden="true"></i></a>'
                                    + ' <a href="#" type= "button" class="btn btn-sm btn-icon btn-pure btn-default on-editing cancel-row" data-toggle="tooltip" data-original-title="Delete" hidden><i class="icon md-close" aria-hidden="true"></i></a>'
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
                                            url: "/ParameterValue/Delete",
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

                data = this.datatable.row.add({ "PARAMETERVALUE": { "SID": 0, "IDPARAMETERVALUE": "", "PARAMETERVALUENAME": "", "PARAMETER": "" }, "PARAMETER": { "PARAMETERNAME": "" } });
                $row = this.datatable.row(data[0]).nodes().to$();

                $row
                    .addClass('adding')
                    .find('td:last')
                    .addClass('actions').html(actions);

                //$row.find("td:eq(0)").hide();
                //$row.find("td:eq(4)").hide();
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
                var arrdata = $.map(data, function (el) { return el });
                var content;

                $row.children('td').each(function (i) {
                    var $this = $(this);

                    if (arrdata.length < 3) {
                        if (i === 0) {
                            content = arrdata[0].IDPARAMETERVALUE;
                        }
                        else if (i === 1) {
                            content = arrdata[0].PARAMETERVALUENAME;
                        }
                        else if (i === 2) {
                            content = arrdata[0].PARAMETER;
                        }
                    }
                    else {
                        if (i === 0) {
                            content = arrdata[1].IDPARAMETERVALUE;
                        }
                        else if (i === 1) {
                            content = arrdata[1].PARAMETERVALUENAME;
                        }
                        else if (i === 2) {
                            content = arrdata[1].PARAMETER;
                        }
                    }

                    if ($this.hasClass('actions')) {
                        _self.rowSetActionsEditing($row);
                    } else {
                        $this.html('<input type="text" class="form-control input-block" value="' + content + '"/>');
                        if ($(this).parent().children().index($(this)) === 2) {
                            $this.html('<select id="ddlParameter" class="form-control input-sm"></select>');
                            $.getJSON('GetParameterList', function (json) {
                                $('#ddlParameter').empty();
                                $('#ddlParameter').append($('<option>').text("Select").attr('value', ''));
                                $.each(json, function (i, obj) {
                                    var selected = (data.PARAMETERVALUE.PARAMETER == obj.SID) ? 'selected' : '';
                                    $('#ddlParameter').append($('<option ' + selected + '>').text(obj.PARAMETERNAME).attr('value', obj.SID));
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

                values = $row.find('td').map(function () {
                    var $this = $(this);

                    if ($this.hasClass('actions')) {
                        return _self.datatable.cell(this).data();
                    } else {
                        if ($(this).parent().children().index($(this)) === 2) {
                            return $('#ddlParameter').find(":selected").val();
                        }
                        return $.trim($this.find('input').val());
                    }
                });

                if (values[0] === "" || values[1] === "" || values[2] === "") {
                    alert("Data harus diisi");
                }
                else {
                    _self.rowSetActionsDefault($row);
                    if ($row.hasClass('adding')) {
                        this.$addButton.removeAttr('disabled');
                        $row.removeClass('adding');
                    }

                    //this.datatable.row($row.get(0)).data(values);

                    var parametervalue = {};
                    if (values[3].PARAMETERVALUE.SID !== 0) {
                        parametervalue.SID = values[3].PARAMETERVALUE.SID;
                        parametervalue.IDPARAMETERVALUE = values[0];
                        parametervalue.PARAMETERVALUENAME = values[1];
                        parametervalue.PARAMETER = values[2];
                        $.ajax({
                            type: "POST",
                            url: "/ParameterValue/Edit",
                            data: '{parametervalue: ' + JSON.stringify(parametervalue) + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json"
                        });
                    } else {
                        parametervalue.IDPARAMETERVALUE = values[0];
                        parametervalue.PARAMETERVALUENAME = values[1];
                        parametervalue.PARAMETER = values[2];
                        $.ajax({
                            type: "POST",
                            url: "/ParameterValue/Create",
                            data: '{parametervalue: ' + JSON.stringify(parametervalue) + '}',
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
                //if ($CountryName !== null) {
                //    this.datatable.cell($row, 3).data($CountryName).draw();
                //}   
            }

        };

        $(function () {
            EditableTable.initialize();
        });

        //var EditableTable = {

        //    options: {
        //        addButton: '#addToTable',
        //        table: '#exampleAddRow',
        //        dialog: {
        //            wrapper: '#dialog',
        //            cancelButton: '#dialogCancel',
        //            confirmButton: '#dialogConfirm'
        //        }
        //    },

        //    initialize: function () {
        //        this
        //          .setVars()
        //          .build()
        //          .events();
        //    },

        //    setVars: function () {
        //        this.$table = $(this.options.table);
        //        this.$addButton = $(this.options.addButton);

        //        // dialog
        //        this.dialog = {};
        //        this.dialog.$wrapper = $(this.options.dialog.wrapper);
        //        this.dialog.$cancel = $(this.options.dialog.cancelButton);
        //        this.dialog.$confirm = $(this.options.dialog.confirmButton);

        //        return this;
        //    },

        //    build: function () {
        //        this.datatable = this.$table.DataTable({
        //            aoColumns: [
        //              null,
        //              null,
        //              null,
        //              null,
        //              null, {
        //                  "bSortable": false
        //              }
        //            ],
        //            language: {
        //                "sSearchPlaceholder": "Search..",
        //                "lengthMenu": "_MENU_",
        //                "search": "_INPUT_"
        //            }
        //        });

        //        window.dt = this.datatable;

        //        return this;
        //    },

        //    events: function () {
        //        var _self = this;

        //        this.$table
        //          .on('click', 'a.save-row', function (e) {
        //              e.preventDefault();

        //              _self.rowSave($(this).closest('tr'));
        //          })
        //          .on('click', 'a.cancel-row', function (e) {
        //              e.preventDefault();

        //              _self.rowCancel($(this).closest('tr'));
        //          })
        //          .on('click', 'a.edit-row', function (e) {
        //              e.preventDefault();

        //              _self.rowEdit($(this).closest('tr'));
        //          })
        //          .on('click', 'a.remove-row', function (e) {
        //              e.preventDefault();

        //              var $row = $(this).closest('tr');
        //              bootbox.dialog({
        //                  message: "Are you sure that you want to delete this row?",
        //                  title: "ARE YOU SURE?",
        //                  buttons: {
        //                      danger: {
        //                          label: "Confirm",
        //                          className: "btn-danger",
        //                          callback: function () {
        //                              var SID = $row.find(".SID").html();
        //                              $.ajax({
        //                                  type: "POST",
        //                                  url: "/ParameterValue/Delete",
        //                                  data: '{id: ' + SID + '}',
        //                                  contentType: "application/json; charset=utf-8",
        //                                  dataType: "json",
        //                                  success: function () {
        //                                      _self.rowRemove($row);
        //                                  }
        //                              });
        //                              _self.rowRemove($row);
        //                          }
        //                      },
        //                      main: {
        //                          label: "Cancel",
        //                          className: "btn-primary",
        //                          callback: function () { }
        //                      }
        //                  }
        //              });
        //          });

        //        this.$addButton.on('click', function (e) {
        //            e.preventDefault();

        //            _self.rowAdd();
        //        });

        //        this.dialog.$cancel.on('click', function (e) {
        //            e.preventDefault();
        //            $.magnificPopup.close();
        //        });

        //        return this;
        //    },


        //    // =============
        //    // ROW FUNCTIONS
        //    // =============
        //    rowAdd: function () {
        //        this.$addButton.attr({
        //            'disabled': 'disabled'
        //        });

        //        var actions,
        //          data,
        //          $row;

        //        actions = [
        //          '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-editing save-row" data-toggle="tooltip" data-original-title="Save" hidden><i class="icon md-wrench" aria-hidden="true"></i></a>',
        //          '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-editing cancel-row" data-toggle="tooltip" data-original-title="Delete" hidden><i class="icon md-close" aria-hidden="true"></i></a>',
        //          '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-default edit-row" data-toggle="tooltip" data-original-title="Edit" hidden><i class="icon md-edit" aria-hidden="true"></i></a>',
        //          '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-default remove-row" data-toggle="tooltip" data-original-title="Remove" hidden><i class="icon md-delete" aria-hidden="true"></i></a>'
        //        ].join(' ');

        //        data = this.datatable.row.add(['', '', '', '', '', actions]);
        //        $row = this.datatable.row(data[0]).nodes().to$();

        //        $row
        //          .addClass('adding')
        //          .find('td:last')
        //          .addClass('actions');

        //        $row.find("td:eq(0)").hide();
        //        $row.find("td:eq(4)").hide();
        //        this.rowEdit($row);

        //        this.datatable.order([0, 'asc']).draw(); // always show fields
        //    },

        //    rowCancel: function ($row) {
        //        var _self = this,
        //          $actions,
        //          i,
        //          data;

        //        if ($row.hasClass('adding')) {
        //            this.rowRemove($row);
        //        } else {

        //            data = this.datatable.row($row.get(0)).data();
        //            this.datatable.row($row.get(0)).data(data);

        //            $actions = $row.find('td.actions');
        //            if ($actions.get(0)) {
        //                this.rowSetActionsDefault($row);
        //            }

        //            this.datatable.draw();
        //        }
        //    },

        //    rowEdit: function ($row) {
        //        var _self = this,
        //          data;

        //        data = this.datatable.row($row.get(0)).data();

        //        $row.children('td').each(function (i) {
        //            var $this = $(this);

        //            if ($this.hasClass('actions')) {
        //                _self.rowSetActionsEditing($row);
        //            } else {
        //                $this.html('<input type="text" class="form-control input-block" value="' + data[i] + '"/>');
        //                if ($(this).parent().children().index($(this)) === 3) {
        //                    $this.html('<select id="ddlParameter" class="form-control input-sm"></select>');
        //                    $.getJSON('GetParameterList', function (json) {
        //                        $('#ddlParameter').empty();
        //                        $('#ddlParameter').append($('<option>').text("Select"));
        //                        $.each(json, function (i, obj) {
        //                            var selected = (data[4] == obj.SID) ? 'selected' : '';
        //                            $('#ddlParameter').append($('<option ' + selected + '>').text(obj.PARAMETERNAME).attr('value', obj.SID));
        //                        });
        //                    });
        //                }
        //            }
        //        });
        //    },

        //    rowSave: function ($row) {
        //        var _self = this,
        //          $actions,
        //          $CountryName,
        //          values = [];

        //        if ($row.hasClass('adding')) {
        //            this.$addButton.removeAttr('disabled');
        //            $row.removeClass('adding');
        //        }

        //        values = $row.find('td').map(function () {
        //            var $this = $(this);

        //            if ($this.hasClass('actions')) {
        //                _self.rowSetActionsDefault($row);
        //                return _self.datatable.cell(this).data();
        //            } else { 
        //                if ($(this).parent().children().index($(this)) === 3) {
        //                    $CountryName = $('#ddlParameter').find(":selected").text();
        //                    return $('#ddlParameter').find(":selected").val();
        //                }
        //                return $.trim($this.find('input').val());
        //            }
        //        });

        //        this.datatable.row($row.get(0)).data(values);

        //        var parametervalue = {};
        //        if (values[0]) {
        //            parametervalue.SID = values[0];
        //            parametervalue.IDPARAMETERVALUE = values[1];
        //            parametervalue.PARAMETERVALUENAME = values[2];
        //            parametervalue.PARAMETER = values[3];
        //            $.ajax({
        //                type: "POST",
        //                url: "/ParameterValue/Edit",
        //                data: '{parametervalue: ' + JSON.stringify(parametervalue) + '}',
        //                contentType: "application/json; charset=utf-8",
        //                dataType: "json"
        //            });
        //        } else {
        //            parametervalue.IDPARAMETERVALUE = values[1];
        //            parametervalue.PARAMETERVALUENAME = values[2];
        //            parametervalue.PARAMETER = values[3];
        //            $.ajax({
        //                type: "POST",
        //                url: "/ParameterValue/Create",
        //                data: '{parametervalue: ' + JSON.stringify(parametervalue) + '}',
        //                contentType: "application/json; charset=utf-8",
        //                dataType: "json"
        //            });
        //        }
        //        $actions = $row.find('td.actions');
        //        if ($actions.get(0)) {
        //            this.rowSetActionsDefault($row);
        //        }

        //        this.datatable.draw();
        //    },

        //    rowRemove: function ($row) {
        //        if ($row.hasClass('adding')) {
        //            this.$addButton.removeAttr('disabled');
        //        }

        //        this.datatable.row($row.get(0)).remove().draw();
        //    },

        //    rowSetActionsEditing: function ($row) {
        //        $row.find('.on-editing').removeAttr('hidden');
        //        $row.find('.on-default').attr('hidden', true);
        //    },

        //    rowSetActionsDefault: function ($row) {
        //        $row.find('.on-editing').attr('hidden', true);
        //        $row.find('.on-default').removeAttr('hidden');
        //        //if ($CountryName !== null) {
        //        //    this.datatable.cell($row, 3).data($CountryName).draw();
        //        //}   
        //    }

        //};

        //$(function () {
        //    EditableTable.initialize();
        //});

    }).apply(this, [jQuery]);


})(document, window, jQuery);
