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
        //Editor
        editor = new $.fn.dataTable.Editor({
            ajax: "/City/Data",
            table: "#tbldata",
            fields: [{
                name: "CITY.IDCITY"
            }, {
                name: "CITY.CITYNAME"
            }, {
                name: "CITY.COUNTRY",
                type: "select",
                "ipOpts": option_country
            }, {
                name: "CITY.PROVINCE",
                type: "select",
                "ipOpts": option_province
            }
            ]
        });

        editor.field('CITY.COUNTRY').input().addClass('form-control input-sm');
        editor.field('CITY.PROVINCE').input().addClass('form-control input-sm');

        $.getJSON('GetCountryList', function (json) {
            $.each(json, function (i, obj) {
                option_country.push({
                    value: obj.SID,
                    label: obj.COUNTRYNAME
                });

                editor.field('CITY.COUNTRY').update(option_country);
            });
        });

        $.getJSON('GetProvinceList', function (json) {
            $.each(json, function (i, obj) {
                option_province.push({
                    value: obj.SID,
                    label: obj.PROVINCENAME
                });

                editor.field('CITY.PROVINCE').update(option_province);
            });
        });

        // Activate the bubble editor on click of a table cell
        $('#tbldata').on('click', 'tbody td', function (e) {
            if ($(this).index() < 4) {
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
                    //dom: "Bfrtip",
                    //ajax: "/City/Data",
                    //"bPaginate": true,
                    //"processing": true,
                    //"bServerSide": true,
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
                            "searchable": false
                        }, {
                            "className": "center",
                            "targets": 1
                        }, {
                            "className": "actions center",
                            "width": "100px",
                            "targets": 5, "data": "SID", "render": function (data, type, full, meta) {
                                return '<div class="SID" style="display:none;">' + data.CITY.SID + '</div><a href="#" type="button" class="btn btn-sm btn-icon btn-pure btn-default on-default edit-row" data-toggle="tooltip" data-original-title="Edit"><i class="icon md-edit" aria-hidden="true"></i></a>'
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
                //data = this.datatable.row.add(['', '', '', '', '', '', '', actions]);
                $row = this.datatable.row(data[0]).nodes().to$();

                $row
                    .addClass('adding')
                    .find('td:last')
                    .addClass('actions').html(actions);

                //$row.find("td:eq(0)").hide();
                //$row.find("td:eq(4)").hide();
                //$row.find("td:eq(6)").hide();
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
                            $this.html('<select id="ddlCountry" class="form-control input-sm"></select>');
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
                                })
                            });
                        }

                        if ($(this).parent().children().index($(this)) === 3) {
                            $this.html('<select id="ddlProvince" class="form-control input-sm"></select>');
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
                    alert("Data harus diisi");
                }
                else {
                    _self.rowSetActionsDefault($row);
                    if ($row.hasClass('adding')) {
                        this.$addButton.removeAttr('disabled');
                        $row.removeClass('adding');
                    }

                    //values[3] = $CountryName;
                    //values[5] = $ProvName;
                    //this.datatable.row($row.get(0)).data(values);
                    //values[3] = $CountryId;
                    //values[5] = $ProvId;

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



    //(function ($) {

    //    var EditableTable = {

    //        options: {
    //            addButton: '#addToTable',
    //            table: '#exampleAddRow',
    //            dialog: {
    //                wrapper: '#dialog',
    //                cancelButton: '#dialogCancel',
    //                confirmButton: '#dialogConfirm'
    //            }
    //        },

    //        initialize: function () {
    //            this
    //              .setVars()
    //              .build()
    //              .events();
    //        },

    //        setVars: function () {
    //            this.$table = $(this.options.table);
    //            this.$addButton = $(this.options.addButton);

    //            // dialog
    //            this.dialog = {};
    //            this.dialog.$wrapper = $(this.options.dialog.wrapper);
    //            this.dialog.$cancel = $(this.options.dialog.cancelButton);
    //            this.dialog.$confirm = $(this.options.dialog.confirmButton);

    //            return this;
    //        },

    //        build: function () {
    //            this.datatable = this.$table.DataTable({
    //                scrollCollapse: true,
    //                aoColumns: [
    //                  null,
    //                  null,
    //                  null,
    //                  null,
    //                  null,
    //                  null,
    //                  null, {
    //                      "bSortable": false
    //                  }
    //                ],
    //                language: {
    //                    "sSearchPlaceholder": "Search..",
    //                    "lengthMenu": "_MENU_",
    //                    "search": "_INPUT_"
    //                }
    //            });

    //            window.dt = this.datatable;

    //            return this;
    //        },

    //        events: function () {
    //            var _self = this;



    //            this.$table
    //              .on('click', 'a.save-row', function (e) {
    //                  e.preventDefault();

    //                  _self.rowSave($(this).closest('tr'));
    //              })
    //              .on('click', 'a.cancel-row', function (e) {
    //                  e.preventDefault();

    //                  _self.rowCancel($(this).closest('tr'));
    //              })
    //              .on('click', 'a.edit-row', function (e) {
    //                  e.preventDefault();
    //                  _self.rowEdit($(this).closest('tr'));
    //              })
    //              .on('click', 'a.remove-row', function (e) {
    //                  e.preventDefault();

    //                  var $row = $(this).closest('tr');
    //                  bootbox.dialog({
    //                      message: "Are you sure that you want to delete this row?",
    //                      title: "ARE YOU SURE?",
    //                      buttons: {
    //                          danger: {
    //                              label: "Confirm",
    //                              className: "btn-danger",
    //                              callback: function () {
    //                                  var SID = $row.find(".SID").html();
    //                                  $.ajax({
    //                                      type: "POST",
    //                                      url: "/City/Delete",
    //                                      data: '{id: ' + SID + '}',
    //                                      contentType: "application/json; charset=utf-8",
    //                                      dataType: "json",
    //                                      success: function () {
    //                                          _self.rowRemove($row);
    //                                      }
    //                                  });
    //                                  _self.rowRemove($row);
    //                              }
    //                          },
    //                          main: {
    //                              label: "Cancel",
    //                              className: "btn-primary",
    //                              callback: function () { }
    //                          }
    //                      }
    //                  });
    //              });

    //            this.$addButton.on('click', function (e) {
    //                e.preventDefault();

    //                _self.rowAdd();
    //            });

    //            this.dialog.$cancel.on('click', function (e) {
    //                e.preventDefault();
    //                $.magnificPopup.close();
    //            });

    //            return this;
    //        },


    //        // =============
    //        // ROW FUNCTIONS
    //        // =============
    //        rowAdd: function () {
    //            this.$addButton.attr({
    //                'disabled': 'disabled'
    //            });

    //            var actions,
    //              data,
    //              $row;
    //            actions = [
    //              '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-editing save-row" data-toggle="tooltip" data-original-title="Save" hidden><i class="icon md-check-all" aria-hidden="true"></i></a>',
    //              '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-editing cancel-row" data-toggle="tooltip" data-original-title="Delete" hidden><i class="icon md-close" aria-hidden="true"></i></a>',
    //              '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-default edit-row" data-toggle="tooltip" data-original-title="Edit" hidden><i class="icon md-edit" aria-hidden="true"></i></a>',
    //              '<a href="#" class="btn btn-sm btn-icon btn-pure btn-default on-default remove-row" data-toggle="tooltip" data-original-title="Remove" hidden><i class="icon md-delete" aria-hidden="true"></i></a>'
    //            ].join(' ');

    //            data = this.datatable.row.add(['', '', '', '', '', '', '', actions]);
    //            $row = this.datatable.row(data[0]).nodes().to$();

    //            $row
    //              .addClass('adding')
    //              .find('td:last')
    //              .addClass('actions');

    //            $row.find("td:eq(0)").hide();
    //            $row.find("td:eq(4)").hide();
    //            $row.find("td:eq(6)").hide();
    //            this.rowEdit($row);

    //            this.datatable.order([0, 'asc']).draw(); // always show fields
    //        },

    //        rowCancel: function ($row) {
    //            var _self = this,
    //              $actions,
    //              i,
    //              data;

    //            if ($row.hasClass('adding')) {
    //                this.rowRemove($row);
    //            } else {

    //                data = this.datatable.row($row.get(0)).data();
    //                this.datatable.row($row.get(0)).data(data);

    //                $actions = $row.find('td.actions');
    //                if ($actions.get(0)) {
    //                    this.rowSetActionsDefault($row);
    //                }

    //                this.datatable.draw();
    //            }
    //        },

    //        rowEdit: function ($row) {
    //            var _self = this,
    //              data;
    //            data = this.datatable.row($row.get(0)).data();

    //            $row.children('td').each(function (i) {
    //                var $this = $(this);

    //                if ($this.hasClass('actions')) {
    //                    _self.rowSetActionsEditing($row);
    //                } else {
    //                    $this.html('<input type="text" class="form-control input-block" value="' + data[i] + '"/>');
    //                    if ($(this).parent().children().index($(this)) === 3) {
    //                        $this.html('<select id="ddlCountry" class="form-control input-sm"></select>');
    //                        $.getJSON('GetCountryList', function (json) {
    //                            $('#ddlCountry').empty();
    //                            $('#ddlCountry').append($('<option>').text("Select"));
    //                            $.each(json, function (i, obj) {
    //                                var selected = (data[4] === obj.SID) ? 'selected' : '';
    //                                $('#ddlCountry').append($('<option ' + selected + '>').text(obj.COUNTRYNAME).attr('value', obj.SID));
    //                            });
    //                        });
    //                    }

    //                    if ($(this).parent().children().index($(this)) === 5) {
    //                        $this.html('<select id="ddlProvince" class="form-control input-sm"></select>');
    //                        $.getJSON('GetProvinceList', function (json) {
    //                            $('#ddlProvince').empty();
    //                            $('#ddlProvince').append($('<option>').text("Select"));
    //                            $.each(json, function (i, obj) {
    //                                var selected = (data[6] === obj.SID) ? 'selected' : '';
    //                                $('#ddlProvince').append($('<option ' + selected + '>').text(obj.PROVINCENAME).attr('value', obj.SID));
    //                            });
    //                        });
    //                    }
    //                }
    //            });

    //            $('#ddlCountry').change(function () {
    //                $.getJSON('GetProvinceByCountry', { Country: $('#ddlCountry').val()}, function (json) {
    //                    $('#ddlProvince').empty();
    //                    $('#ddlProvince').append($('<option>').text("Select"));
    //                    $.each(json, function (i, obj) {
    //                        $('#ddlProvince').append($('<option>').text(obj.PROVINCENAME).attr('value', obj.SID));
    //                    });
    //                });
    //            })
    //        },

    //        rowSave: function ($row) {
    //            var _self = this,
    //                            $actions,
    //                            $CountryId,
    //                            $CountryName,
    //                            $ProvId,
    //                            $ProvName,
    //                            values = [];

    //            if ($row.hasClass('adding')) {
    //                this.$addButton.removeAttr('disabled');
    //                $row.removeClass('adding');
    //            }

    //            values = $row.find('td').map(function () {
    //                var $this = $(this);

    //                if ($this.hasClass('actions')) {
    //                    _self.rowSetActionsDefault($row);
    //                    return _self.datatable.cell(this).data();
    //                } else {
    //                    if ($(this).parent().children().index($(this)) === 3) {
    //                        $CountryName = $('#ddlCountry').find(":selected").text();
    //                        $CountryId = $('#ddlCountry').find(":selected").val();
    //                        return $('#ddlCountry').find(":selected").val();
    //                    }
    //                    if ($(this).parent().children().index($(this)) === 5)
    //                    {
    //                        $ProvName = $('#ddlProvince').find(":selected").text();
    //                        $ProvId = $('#ddlProvince').find(":selected").val();
    //                        return $('#ddlProvince').find(":selected").val();
    //                    }
    //                    return $.trim($this.find('input').val());
    //                }
    //            });

    //            values[3] = $CountryName;
    //            values[5] = $ProvName;
    //            this.datatable.row($row.get(0)).data(values);
    //            values[3] = $CountryId;
    //            values[5] = $ProvId;

    //            var city = {};
    //            if (values[0]) {
    //                city.SID = values[0];
    //                city.IDCITY = values[1];
    //                city.CITYNAME = values[2];
    //                city.COUNTRY = values[3];
    //                city.PROVINCE = values[5];
    //                $.ajax({
    //                    type: "POST",
    //                    url: "/City/Edit",
    //                    data: '{city: ' + JSON.stringify(city) + '}',
    //                    contentType: "application/json; charset=utf-8",
    //                    dataType: "json"
    //                });
    //            } else {
    //                city.IDCITY = values[1];
    //                city.CITYNAME = values[2];
    //                city.COUNTRY = values[3];
    //                city.PROVINCE = values[5];
    //                $.ajax({
    //                    type: "POST",
    //                    url: "/City/Create",
    //                    data: '{city: ' + JSON.stringify(city) + '}',
    //                    contentType: "application/json; charset=utf-8",
    //                    dataType: "json"
    //                });
    //            }
    //            $actions = $row.find('td.actions');
    //            if ($actions.get(0)) {
    //                this.rowSetActionsDefault($row);
    //            }

    //            this.datatable.draw();
    //        },

    //        rowRemove: function ($row) {
    //            if ($row.hasClass('adding')) {
    //                this.$addButton.removeAttr('disabled');
    //            }

    //            this.datatable.row($row.get(0)).remove().draw();
    //        },

    //        rowSetActionsEditing: function ($row) {
    //            $row.find('.on-editing').removeAttr('hidden');
    //            $row.find('.on-default').attr('hidden', true);
    //        },

    //        rowSetActionsDefault: function ($row) {
    //            $row.find('.on-editing').attr('hidden', true);
    //            $row.find('.on-default').removeAttr('hidden');
    //        }

    //    };

    //    $(function () {
    //        EditableTable.initialize();
    //    });
        
    }).apply(this, [jQuery]);


})(document, window, jQuery);
