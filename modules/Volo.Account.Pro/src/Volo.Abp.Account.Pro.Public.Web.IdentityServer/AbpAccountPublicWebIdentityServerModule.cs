using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account.Public.Web;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Account.Web.ExtensionGrantValidators;

namespace Volo.Abp.Account.Web
{
    [DependsOn(
        typeof(AbpAccountPublicWebModule),
        typeof(AbpIdentityServerDomainModule)
        )]
    public class AbpAccountPublicWebIdentityServerModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpIdentityAspNetCoreOptions>(options =>
            {
                options.ConfigureAuthentication = false;
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAccountPublicWebIdentityServerModule).Assembly);
            });

            PreConfigure<IIdentityServerBuilder>(identityServerBuilder =>
            {
                identityServerBuilder.AddExtensionGrantValidator<LinkLoginExtensionGrantValidator>();
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpAccountPublicWebIdentityServerModule>();
            });

            Configure<IdentityServerOptions>(options =>
            {
                options.UserInteraction.ConsentUrl = "/Consent";
                options.UserInteraction.ErrorUrl = "/Account/Error";
            });

            Configure<AbpRequestLocalizationOptions>(options =>
            {
                options.RequestLocalizationOptionConfigurators.Add((serviceProvider, localizationOptions) =>
                {
                    localizationOptions.RequestCultureProviders.InsertAfter(
                        x => x.GetType() == typeof(QueryStringRequestCultureProvider),
                        new IdentityServerReturnUrlRequestCultureProvider());
                    return Task.CompletedTask;
                });
            });

            //TODO: Try to reuse from AbpIdentityAspNetCoreModule
            context.Services
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = IdentityConstants.ApplicationScheme;
                    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            LicenseChecker.Check<AbpAccountPublicWebIdentityServerModule>(context);
        }
    }
}
