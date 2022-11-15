(function ($) {
    var l = abp.localization.getResource("CmsKit");

    abp.widgets.CmsContact = function ($widget) {
        var widgetManager = $widget.data("abp-widget-manager");

        function init() {
            $widget.find(".contact-form").on('submit', '', function (e) {
                e.preventDefault();

                var formAsObject = $(this).serializeFormToObject();

                volo.cmsKit.public.contact.contactPublic.sendMessage(
                    {
                        name: formAsObject.name,
                        subject: formAsObject.subject,
                        email: formAsObject.emailAddress,
                        message: formAsObject.message,
                        recaptchaToken: formAsObject.recaptchaToken
                    }
                ).then(function () {
                    abp.message.success(l("ContactSuccess"))
                        .then(function () {
                            widgetManager.refresh($widget);
                        });
                })
            });
        }

        return {
            init: init
        }
    };
})(jQuery);
