$(function () {
    var l = abp.localization.getResource("Payment");

    var createModal = new abp.ModalManager({ viewUrl: abp.appPath + "Payment/Plans/CreateModal", modalClass: 'createPlan' });
    var updateModal = new abp.ModalManager({ viewUrl: abp.appPath + "Payment/Plans/UpdateModal", modalClass: 'updatePlan' });

    var service = volo.payment.admin.plans.planAdmin;

    var dataTable = $("#PlansTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollCollapse: true,
        scrollX: true,
        ordering: true,
        order: [[1, "desc"]],
        ajax: abp.libs.datatables.createAjax((input, ajaxParams) =>
        {
            input.filter = getFilter();
            return service.getList(input, ajaxParams);
        }),
        columnDefs: [
            {
                title: l("Actions"),
                targets: 0,
                rowAction: {
                    items: [
                        {
                            text: l('GatewayPlans'),
                            visible: abp.auth.isGranted('Payment.Plans.GatewayPlans'),
                            action: function (data) {
                                location.href = 'plans/'+ data.record.id + '/external-plans';
                            }
                        },
                        {
                            text: l('Edit'),
                            visible: abp.auth.isGranted('Payment.Plans.Update'),
                            action: function (data) {
                                updateModal.open({ id: data.record.id });
                            }
                        },
                        {
                            text: l('Delete'),
                            visible: abp.auth.isGranted('Payment.Plans.Delete'),
                            confirmMessage: function (data) {
                                return l("PlanDeletionConfirmationMessage")
                            },
                            action: function (data) {
                                service
                                    .delete(data.record.id)
                                    .then(function () {
                                        dataTable.ajax.reload();
                                    });
                            }
                        }
                    ]
                }
            },
            {
                title: l("DisplayName:Name"),
                orderable: true,
                data: "name"
            }
        ]
    }));

    function getFilter() {
        return  $('#PlansSearchWrapper input.page-search-filter-text').val();
    };

    $('#PlansSearchWrapper form.page-search-form').submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#AbpContentToolbar button[name=CreatePlan]').on('click', function (e) {
        e.preventDefault();
        createModal.open();
    });

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    updateModal.onResult(function () {
        dataTable.ajax.reload();
    });
});