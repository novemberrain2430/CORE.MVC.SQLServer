using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;
using Volo.Abp.AutoMapper;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Payment.Admin.Permissions;
using Volo.Payment.Admin.Web.Menus;
using Volo.Payment.Localization;

namespace Volo.Payment.Admin.Web
{
    [DependsOn(
        typeof(AbpPaymentWebModule),
        typeof(AbpPaymentAdminHttpApiModule)
        )]
    public class AbpPaymentAdminWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(PaymentResource),
                    typeof(AbpPaymentDomainSharedModule).Assembly,
                    typeof(AbpPaymentAdminWebModule).Assembly,
                    typeof(AbpPaymentAdminApplicationContractsModule).Assembly
                );
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpPaymentAdminWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new PaymentAdminMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpPaymentAdminWebModule>("Volo.CmsKit.Admin.Web");
            });

            context.Services.AddAutoMapperObjectMapper<AbpPaymentAdminWebModule>();
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<AbpPaymentAdminWebModule>(validate: true); });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizeFolder("/Payment/Plans/", PaymentAdminPermissions.Plans.Default);
                options.Conventions.AuthorizeFolder("/Payment/Plans/CreateModal", PaymentAdminPermissions.Plans.Create);
                options.Conventions.AuthorizeFolder("/Payment/Plans/UpdateModal", PaymentAdminPermissions.Plans.Update);
            });

            Configure<AbpPageToolbarOptions>(options =>
            {
                options.Configure<Volo.Payment.Admin.Web.Pages.Payment.Plans.IndexModel>(
                    toolbar =>
                    {
                        toolbar.AddButton(
                            LocalizableString.Create<PaymentResource>("NewPlan"),
                            icon: "plus",
                            name: "CreatePlan",
                            requiredPolicyName: PaymentAdminPermissions.Plans.Create
                        );
                    }
                );

                options.Configure<Volo.Payment.Admin.Web.Pages.Payment.Plans.GatewayPlans.IndexModel>(toolbar =>
                {
                    toolbar.AddButton(
                        LocalizableString.Create<PaymentResource>("NewGatewayPlan"),
                        icon: "plus",
                        name: "CreateGatewayPlan",
                        requiredPolicyName: PaymentAdminPermissions.Plans.Create
                    );
                });
            });
        }
    }
}
