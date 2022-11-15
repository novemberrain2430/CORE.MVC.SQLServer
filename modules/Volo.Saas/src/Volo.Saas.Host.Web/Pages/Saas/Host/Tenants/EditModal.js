(function ($) {

    abp.modals.SaaSTenantEdit = function() {

        var initModal = function (publicApi, args) {
            publicApi.getModal()
                .find('#Tenant_ActivationState')
                .change(function () {
                    var $this = $(this);
                    if($this.val() === 'ActiveWithLimitedTime') {
                        var endDate = $('#Tenant_ActivationEndDate');
                        endDate.attr("required", true);
                        endDate.parent().parent().show();
                    }
                    else {
                        var endDate = $('#Tenant_ActivationEndDate');
                        endDate.attr("required", false);
                        endDate.parent().parent().hide();
                    }
                }).change();
        };

        return {
            initModal: initModal
        };
    };

})(jQuery);
