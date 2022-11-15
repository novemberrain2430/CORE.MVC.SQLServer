﻿$(function () {
    var l = abp.localization.getResource("Payment");
    var planId = $('#GatewayPlansWrapper').data('plan-id');

    var createModal = new abp.ModalManager({ viewUrl: abp.appPath + "Payment/Plans/GatewayPlans/CreateModal", modalClass: 'createGatewayPlan' });
    var updateModal = new abp.ModalManager({ viewUrl: abp.appPath + "Payment/Plans/GatewayPlans/UpdateModal", modalClass: 'updateGatewayPlan' });

    var service = volo.payment.admin.plans.planAdmin;

    var dataTable = $("#GatewayPlansTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollCollapse: true,
        scrollX: true,
        ordering: true,
        order: [[1, "desc"]],
        ajax: abp.libs.datatables.createAjax((input, ajaxParams) => {
            input.filter = getFilter();
            return service.getGatewayPlans(planId, input, ajaxParams)
        }),
        columnDefs: [
            {
                title: l("Actions"),
                targets: 0,
                rowAction: {
                    items: [
                        {
                            text: l('Edit'),
                            visible: abp.auth.isGranted('Payment.Plans.Update'),
                            action: function (data) {
                                updateModal.open({ planId: planId, gateway: data.record.gateway });
                            }
                        },
                        {
                            text: l('Delete'),
                            visible: abp.auth.isGranted('Payment.Plans.Update'),
                            confirmMessage: function (data) {
                                return l("GatewayPlanDeletionConfirmationMessage")
                            },
                            action: function (data) {
                                service
                                    .deleteGatewayPlan(planId, data.record.gateway)
                                    .then(function () {
                                        dataTable.ajax.reload();
                                    });
                            }
                        }
                    ]
                }
            },
            {
                title: l("DisplayName:Gateway"),
                orderable: true,
                data: "gateway"
            },
            {
                title: l("DisplayName:ExternalId"),
                orderable: true,
                data: "externalId"
            }
        ]
    }));

    function getFilter() {
        return $('#GatewayPlansSearchWrapper input.page-search-filter-text').val();
    };


    $('#GatewayPlansSearchWrapper form.page-search-form').submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#AbpContentToolbar button[name=CreateGatewayPlan]').on('click', function (e) {
        e.preventDefault();
        createModal.open({ planId: planId });
    });

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    updateModal.onResult(function () {
        dataTable.ajax.reload();
    });
});