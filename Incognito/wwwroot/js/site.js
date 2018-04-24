// Write your JavaScript code.

//JS for TimeAgo
jQuery(document).ready(function () {
    jQuery("time.timeago").timeago();
});

//JS for Bootbox
$(document).ready(function () {
    //js-delete-message
    $(".js-delete-message").click(function (e) {
        var link = $(e.target);
        bootbox.dialog({
            message: "Sure you want to delete the message?",
            //title: "Confirm",
            buttons: {
                no: {
                    label: "No",
                    className: "btn-default",
                    callback: function () {
                        bootbox.hideAll();
                    }
                },
                yes: {
                    label: "Yes",
                    className: "btn-danger",
                    callback: function () {
                        $.ajax({
                            url: "/api/v1/message/" + link.attr("message-id"),
                            method: "DELETE"
                        })
                            .done(function () {
                                link.parents("li").fadeOut(function () {
                                    $(this).remove();
                                });
                            })
                            .fail(function () {
                                alert("Something failed!");
                            });
                    }
                }
            }
        });
    });

    //js-archive-message
    $(".js-archive-message").click(function (e) {
        var link = $(e.target);
        $.ajax({
            url: "/api/v1/message/" + link.attr("message-id"),
            method: "PUT"
        })
            .done(function () {
                link.parents("li").fadeOut(function () {
                    $(this).remove();
                });
            })
            .fail(function () {
                alert("Something failed!");
            });
    });

    //js-report-message
    $(".js-report-message").click(function (e) {
        var link = $(e.target);
        bootbox.prompt({
            title: "What is your name?",
            callback: function (result) {
                $.ajax({
                    method: "POST",
                    url: "/api/v1/message/report/" + link.attr("message-id"),
                    data: { id: link.attr("message-id"), reason: result }
                    //dataType: "json",
                })
                    //$.post("/api/v1/message/" + link.attr("message-id"), { id: link.attr("message-id"), reason: result },,
                    //    function (returnedData) {
                    //        console.log(returnedData);
                    //    }
                    //)
                    .done(function () {
                        console.log("Success");
                    })
                    .fail(function () {
                        alert("Something failed!");
                    });
            }
        });
    });

    //js-delete-role
    $(".js-delete-role").click(function (e) {
        var link = $(e.target);
        bootbox.dialog({
            message: "Sure you want to delete the role?",
            buttons: {
                no: {
                    label: "No",
                    className: "btn-default",
                    callback: function () {
                        bootbox.hideAll();
                    }
                },
                yes: {
                    label: "Yes",
                    className: "btn-danger",
                    callback: function () {
                        $.ajax({
                                url: "/api/v1/admin/" + link.attr("role-id"),
                                method: "DELETE"
                            })
                            .done(function () {
                                link.parents("tr").fadeOut(function () {
                                    $(this).remove();
                                });
                            })
                            .fail(function () {
                                alert("Something failed!");
                            });
                    }
                }
            }
        });
    });
});