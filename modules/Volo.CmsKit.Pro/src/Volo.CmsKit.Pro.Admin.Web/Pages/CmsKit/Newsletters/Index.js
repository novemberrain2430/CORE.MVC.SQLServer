$(function () {
    var l = abp.localization.getResource("CmsKit");

    var newsletterService = volo.cmsKit.admin.newsletters.newsletterRecordAdmin;

    var detailModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CmsKit/Newsletters/Detail",
        modalClass: "newsletterDetailModel"
    });

    var getFilter = function () {
        return {
            preference: $("#Preference").val(),
            source: $("#Source").val()
        };
    };

    let dataTable = $("#NewslettersTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        scrollX: true,
        ordering: false,
        ajax: abp.libs.datatables.createAjax(newsletterService.getList, getFilter),
        columnDefs: [
            {
                title: l("Details"),
                targets: 0,
                rowAction: {
                    items: [
                        {
                            text: l('Detail'),
                            action: function (data) {
                                detailModal.open({
                                    id: data.record.id
                                });
                            }
                        }
                    ]
                }
            },
            {
                title: l("EmailAddress"),
                data: "emailAddress"
            },
            {
                title: l("CreationTime"),
                data: "creationTime",
                render: function (data) {
                    if (!data) {
                        return "";
                    }

                    return new Date(data).toLocaleString("en-CA");
                }
            }
        ]
    }));

    detailModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#ExportCsv").on("click", "", function () {
        var preference = $("#Preference").val();
        var source = $("#Source").val();
        window.location = abp.appPath + "api/cms-kit-admin/newsletter/export-csv?Preference=" + preference + "&Source=" + source;
    });

    $("#RefreshSourceButton").on("click", "", function () {
        dataTable.ajax.reload();
    });

    $("#SourceFilter").keypress(function (event) {
        let keyCode = (event.keyCode ? event.keyCode : event.which);
        if (keyCode == "13") {
            dataTable.ajax.reload();
        }
    })

    $("#PreferenceFilter").on("change", "", function () {
        dataTable.ajax.reload();
    });

    $('#NewslettersTable').on('draw.dt', function () {
        let info = dataTable.page.info();
        let exportCsv = $("#ExportCsv");

        if (info.recordsDisplay === 0) {
            exportCsv.hide();
        } else if (info.recordsDisplay > 0) {
            exportCsv.show();
        }
    });
});
