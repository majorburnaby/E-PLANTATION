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
    var option_controljob = [];
    (function ($) {

        var Table = {

            options: {
                addButton: '#addToTable',
                table: '#tblstockgroup',
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
                    ajax: "/StockGroup/Data",
                    columns: [
                        { data: "STOCKGROUP.SID" },
                        { data: "STOCKGROUP.IDSTOCKGROUP" },
                        { data: "STOCKGROUP.STOCKGROUPNAME" },
                        { data: "CONTROLJOB.ITEMDESCRIPTION", editField: "STOCKGROUP.CONTROLJOB" }
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
                            "targets": 4, "data": "SID", "render": function (data, type, full, meta) {
                                return '<div class="SID" style="display:none;">' + data.STOCKGROUP.SID + '</div><a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-default edit-row" data-toggle="tooltip" data-original-title="Edit"><i class="icon md-edit" aria-hidden="true"></i></a>'
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
                                          url: "/StockGroup/Delete",
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

                data = this.datatable.row.add({ "STOCKGROUP": { "SID": 0, "IDSTOCKGROUP": "", "STOCKGROUPNAME": "", "CONTROLJOB": "" }, "CONTROLJOB": { "ITEMDESCRIPTION": "" } });
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
                console.log(data);
                var arrdata = $.map(data, function (el) { return el; });
                console.log(arrdata);
                var content;
                $row.children('td').each(function (i) {
                    var $this = $(this);

                    if (arrdata.length < 3) {
                        if (i === 0) {
                            content = arrdata[0].IDSTOCKGROUP;
                        }
                        else if (i === 1) {
                            content = arrdata[0].STOCKGROUPNAME;
                        }
                        else if (i === 2) {
                            content = arrdata[0].CONTROLJOB;
                        }
                    }
                    else {
                        if (i === 0) {
                            content = arrdata[1].IDSTOCKGROUP;
                        }
                        else if (i === 1) {
                            content = arrdata[1].STOCKGROUPNAME;
                        }
                        else if (i === 2) {
                            content = arrdata[1].CONTROLJOB;
                        }
                    }

                    if ($this.hasClass('actions')) {
                        _self.rowSetActionsEditing($row);
                    } else {
                        $this.html('<input type="text" class="form-control input-block" value="' + content + '"/>');
                        if ($(this).parent().children().index($(this)) === 2) {
                            $this.html('<select id="ddlControlJob" class="form-control input-sm"></select>');
                            $.getJSON('GetControlJob', function (json) {
                                $('#ddlControlJob').empty();
                                $('#ddlControlJob').append($('<option>').text("Select").attr('value', ''));
                                $.each(json, function (i, obj) {
                                    var selected = (data[4] === obj.SID) ? 'selected' : '';
                                    $('#ddlControlJob').append($('<option ' + selected + '>').text(obj.ITEMDESCRIPTION).attr('value', obj.SID));
                                });

                                $('#ddlControlJob').val(content);
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
                            return $('#ddlControlJob').find(":selected").val();
                        }
                        return $.trim($this.find('input').val());
                    }
                });

                if (values[0] === "" || values[1] === "" || values[2] === "") {
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

                    var stockgroup = {};
                    if (values[3].STOCKGROUP.SID !== 0) {

                        stockgroup.SID = values[3].STOCKGROUP.SID;
                        stockgroup.IDSTOCKGROUP = values[0];
                        stockgroup.STOCKGROUPNAME = values[1];
                        stockgroup.CONTROLJOB = values[2];
                        $.ajax({
                            type: "POST",
                            url: "/StockGroup/Edit",
                            data: '{stockgroup: ' + JSON.stringify(stockgroup) + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json"
                        });
                    } else {
                        stockgroup.IDSTOCKGROUP = values[0];
                        stockgroup.STOCKGROUPNAME = values[1];
                        stockgroup.CONTROLJOB = values[2];
                        $.ajax({
                            type: "POST",
                            url: "/StockGroup/Create",
                            data: '{stockgroup: ' + JSON.stringify(stockgroup) + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json"
                        });
                    }
                    $actions = $row.find('td.actions');
                    if ($actions.get(0)) {
                        this.rowSetActionsDefault($row);
                    }

                    this.datatable.draw();

                    $('#tblstockgroup').DataTable().ajax.reload();
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