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

    (function ($) {
        var dt = $('#tbldata').DataTable();

        $('#tbldata tbody').on('click', 'a.remove-row', function (e) {
            e.preventDefault();
            var SID = $(this).data("content");
            console.log(SID);
            bootbox.dialog({
                message: "Are you sure that you want to delete this row?",
                title: "ARE YOU SURE?",
                buttons: {
                    danger: {
                        label: "Confirm",
                        className: "btn-danger",
                        callback: function () {
                            $.ajax({
                                type: "POST",
                                url: "/Bank/Delete",
                                data: '{id: ' + SID + '}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function () {
                                    window.location.reload(false);
                                }
                            });
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

    }).apply(this, [jQuery]);


})(document, window, jQuery);