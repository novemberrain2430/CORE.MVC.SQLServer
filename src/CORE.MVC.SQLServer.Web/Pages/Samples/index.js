$(function () {
    var l = abp.localization.getResource("SQLServer");
	var sampleService = window.CORE.MVC.SQLServer.controllers.samples.sample;
	
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Samples/CreateModal",
        scriptUrl: "/Pages/Samples/createModal.js",
        modalClass: "sampleCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Samples/EditModal",
        scriptUrl: "/Pages/Samples/editModal.js",
        modalClass: "sampleEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            name: $("#NameFilter").val(),
			date1Min: $("#Date1FilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			date1Max: $("#Date1FilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			yearMin: $("#YearFilterMin").val(),
			yearMax: $("#YearFilterMax").val(),
			code: $("#CodeFilter").val(),
			email: $("#EmailFilter").val(),
            isConfirm: (function () {
                var value = $("#IsConfirmFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
			userId: $("#UserIdFilter").val()
        };
    };

    var dataTable = $("#SamplesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(sampleService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('SQLServer.Samples.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('SQLServer.Samples.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    sampleService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "name" },
            {
                data: "date1",
                render: function (date1) {
                    if (!date1) {
                        return "";
                    }
                    
					var date = Date.parse(date1);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
			{ data: "year" },
			{ data: "code" },
			{ data: "email" },
            {
                data: "isConfirm",
                render: function (isConfirm) {
                    return isConfirm ? l("Yes") : l("No");
                }
            },
			{ data: "userId" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewSampleButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
});
