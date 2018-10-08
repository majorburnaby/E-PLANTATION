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
    var option_country = [];
    var option_province = [];
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
                    ajax: {
                        url: "/City/Data",
                        type: "POST"
                    },
                    columns: [
                        { data: "CITY.SID" },
                        { data: "CITY.IDCITY" },
                        { data: "CITY.CITYNAME" },
                        { data: "COUNTRY.COUNTRYNAME", editField: "CITY.COUNTRY" },
                        { data: "PROVINCE.PROVINCENAME", editField: "CITY.PROVINCE" }
                        , { data: null, "defaultContent": '' }
                    ],
                    "columnDefs": [
                        {
                            "targets": [0],
                            "visible": false,
                            "searchable": false,
                            orderData: [1]
                        }, {
                            "className": "center",
                            "targets": 1
                        }, {
                            "className": "actions center",
                            "width": "60px",
                            "targets": 5, "data": "SID", "render": function (data, type, full, meta) {
                                return '<div class="SID" style="display:none;">' + data.CITY.SID + '</div><a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-default edit-row" data-toggle="tooltip" data-original-title="Edit"><i class="icon md-edit" aria-hidden="true"></i></a>'
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
                                            url: "/City/Delete",
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

                data = this.datatable.row.add({ "CITY": { "SID": 0, "IDCITY": "", "CITYNAME": "", "COUNTRY": "", "PROVINCE": "" }, "COUNTRY": { "COUNTRYNAME": "" }, "PROVINCE": { "PROVINCENAME": "" } });
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

                    if (arrdata.length < 4) {
                        if (i === 0) {
                            content = arrdata[0].IDCITY;
                        }
                        else if (i === 1) {
                            content = arrdata[0].CITYNAME;
                        }
                        else if (i === 2) {
                            content = arrdata[0].COUNTRY;
                        }
                        else if (i === 3) {
                            content = arrdata[0].PROVINCE;
                        }
                    }
                    else {
                        if (i === 0) {
                            content = arrdata[1].IDCITY;
                        }
                        else if (i === 1) {
                            content = arrdata[1].CITYNAME;
                        }
                        else if (i === 2) {
                            content = arrdata[1].PROVINCE;
                        }
                        else if (i === 2) {
                            content = arrdata[1].COUNTRY;
                        }
                    }

                    if ($this.hasClass('actions')) {
                        _self.rowSetActionsEditing($row);
                    } else {
                        $this.html('<input type="text" class="form-control input-block" value="' + content + '"/>');
                        if ($(this).parent().children().index($(this)) === 2) {
                            $this.html('<select id="ddlCountry" class="form-control input-sm" data-plugin="select2" style="width: 100%"></select>');
                            $.getJSON('GetCountryList', function (json) {
                                $('#ddlCountry').empty();
                                $('#ddlCountry').append($('<option>').text("Select").attr('value', ''));
                                $.each(json, function (i, obj) {
                                    var selected = (data.CITY.COUNTRY === obj.SID) ? 'selected' : '';
                                    $('#ddlCountry').append($('<option ' + selected + '>').text(obj.COUNTRYNAME).attr('value', obj.SID));
                                });

                                $('#ddlCountry').change(function () {
                                    var countryid = $('#ddlCountry').val() === "" ? 0 : $('#ddlCountry').val();
                                    $.getJSON('GetProvinceByCountry', { Country: countryid }, function (json) {
                                        $('#ddlProvince').empty();
                                        $('#ddlProvince').append($('<option>').text("Select").attr('value', ''));
                                        $.each(json, function (i, obj) {
                                            $('#ddlProvince').append($('<option>').text(obj.PROVINCENAME).attr('value', obj.SID));
                                        });
                                    });
                                });
                            });
                        }

                        if ($(this).parent().children().index($(this)) === 3) {
                            $this.html('<select id="ddlProvince" class="form-control input-sm" data-plugin="select2" style="width: 100%"></select>');
                            $.getJSON('GetProvinceList', function (json) {
                                $('#ddlProvince').empty();
                                $('#ddlProvince').append($('<option>').text("Select").attr('value', ''));
                                $.each(json, function (i, obj) {
                                    var selected = (data.CITY.PROVINCE === obj.SID) ? 'selected' : '';
                                    $('#ddlProvince').append($('<option ' + selected + '>').text(obj.PROVINCENAME).attr('value', obj.SID));
                                });
                            });
                        }

                    }
                    $("#ddlCountry").select2({ allowClear: true });
                    $("#ddlProvince").select2({ allowClear: true });
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
                            return $('#ddlCountry').find(":selected").val();
                        }
                        if ($(this).parent().children().index($(this)) === 3) {
                            return $('#ddlProvince').find(":selected").val();
                        }
                        return $.trim($this.find('input').val());
                    }
                });
                
                if (values[0] === "" || values[1] === "" || values[2] === "" || values[3] === "") {
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

                    var city = {};
                    if (values[4].CITY.SID !== 0) {
                        city.SID = values[4].CITY.SID;
                        city.IDCITY = values[0];
                        city.CITYNAME = values[1];
                        city.COUNTRY = values[2];
                        city.PROVINCE = values[3];
                        $.ajax({
                            type: "POST",
                            url: "/City/Edit",
                            data: '{city: ' + JSON.stringify(city) + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json"
                        });
                    } else {
                        city.IDCITY = values[0];
                        city.CITYNAME = values[1];
                        city.COUNTRY = values[2];
                        city.PROVINCE = values[3];
                        $.ajax({
                            type: "POST",
                            url: "/City/Create",
                            data: '{city: ' + JSON.stringify(city) + '}',
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
        });

    }).apply(this, [jQuery]);


})(document, window, jQuery);
