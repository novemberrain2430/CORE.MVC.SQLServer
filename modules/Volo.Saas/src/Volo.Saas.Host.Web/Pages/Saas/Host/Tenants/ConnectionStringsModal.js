(function ($) {

    abp.modals.TenantConnectionStringManagement = function() {

        var initModal = function (publicApi, args) {
            publicApi.getModal()
                .find('input[name="ConnectionStrings.UseSharedDatabase"]')
                .change(function () {
                    var $this = $(this);
                    $("#Tenant_ConnectionStrings_Wrap").toggleClass("d-none");
                    $this.val($this.prop("checked"));
                });

            publicApi.getModal()
                .find('#AddDatabaseConnectionString')
                .click(function () {
                    var DatabaseNameSelect = $("#DatabaseName");
                    var databaseName = DatabaseNameSelect.val();

                    var databaseConnectionStringInput = $("#ConnectionString");
                    var databaseConnectionString = databaseConnectionStringInput.val();
                    if(!databaseName || !databaseConnectionString) {
                        databaseConnectionStringInput.focus();
                        return;
                    }

                    var databaseNameElement = $("<td></td>").text(databaseName).append($("<input type='hidden'>").val(databaseName));
                    var databaseConnectionStringElement = $("<td></td>").text(databaseConnectionString).append($("<input type='hidden'>").val(databaseConnectionString))
                    var databaseDeleteElement = $('<td><button type="button" class="btn btn-danger btn-sm"><i class="fa fa-trash"></i></button></td>');

                    $("#ConnectionStringsTBody").append($('<tr></tr>').append(databaseNameElement).append(databaseConnectionStringElement).append(databaseDeleteElement))

                    databaseConnectionStringInput.val("");
                    DatabaseNameSelect.find("option[value=" + databaseName + "]").remove()

                    $.each(publicApi.getForm().find("tbody tr"), function(index, tr) {
                        var inputs = $(tr).find("input[type='hidden']");
                        inputs.first().attr("name", "ConnectionStrings.Databases[" + index + "].DatabaseName");
                        inputs.last().attr("name", "ConnectionStrings.Databases[" + index + "].ConnectionString");
                    });
                });

            publicApi.getModal()
                .find('#ConnectionStringsTBody')
                .on("click", "button", function () {
                    var vaule = $(this).parents("tr").find("input[type='hidden']")[0].value;
                    var option = $("<option></option>").val(vaule).text(vaule);
                    $("#DatabaseName").append(option);
                    $(this).parents("tr").remove();
                });
        };

        return {
            initModal: initModal
        };
    };

})(jQuery);
