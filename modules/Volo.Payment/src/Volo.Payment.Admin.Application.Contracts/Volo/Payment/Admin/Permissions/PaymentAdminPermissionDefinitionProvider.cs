using System;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Payment.Localization;

namespace Volo.Payment.Admin.Permissions
{
    public class PaymentAdminPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var paymentGroup = context.GetGroupOrNull(PaymentAdminPermissions.GroupName) ?? context.AddGroup(PaymentAdminPermissions.GroupName, L("Permission:Payment"));

            var planGroup = paymentGroup.AddPermission(PaymentAdminPermissions.Plans.Default, L("Permission:PaymentPlanManagement"));

            planGroup.AddChild(PaymentAdminPermissions.Plans.Create, L("Permission:PaymentPlanManagement.Create"));
            planGroup.AddChild(PaymentAdminPermissions.Plans.Update, L("Permission:PaymentPlanManagement.Update"));
            planGroup.AddChild(PaymentAdminPermissions.Plans.Delete, L("Permission:PaymentPlanManagement.Delete"));

            var gatewayPlanGroup = paymentGroup.AddPermission(PaymentAdminPermissions.Plans.GatewayPlans.Default, L("Permission:PaymentGatewayPlanManagement.Default"));

            gatewayPlanGroup.AddChild(PaymentAdminPermissions.Plans.GatewayPlans.Create, L("Permission:PaymentGatewayPlanManagement.Create"));
            gatewayPlanGroup.AddChild(PaymentAdminPermissions.Plans.GatewayPlans.Update, L("Permission:PaymentGatewayPlanManagement.Update"));
            gatewayPlanGroup.AddChild(PaymentAdminPermissions.Plans.GatewayPlans.Delete, L("Permission:PaymentGatewayPlanManagement.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PaymentResource>(name);
        }
    }
}
