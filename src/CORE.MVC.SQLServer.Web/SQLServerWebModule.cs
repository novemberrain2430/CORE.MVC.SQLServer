using System;
using System.IO;
//using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CORE.MVC.SQLServer.EntityFrameworkCore;
using CORE.MVC.SQLServer.Localization;
using CORE.MVC.SQLServer.MultiTenancy;
using CORE.MVC.SQLServer.Permissions;
using CORE.MVC.SQLServer.Web.Menus;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Account.Admin.Web;
using Volo.Abp.Account.Public.Web;
using Volo.Abp.Account.Public.Web.ExternalProviders;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Commercial;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Lepton;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AuditLogging.Web;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Web;
using Volo.Abp.IdentityServer.Web;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme.Management;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.TextTemplateManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Saas.Host;
//using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
//using Microsoft.AspNetCore.Authentication.Twitter;
//using CORE.MVC.SQLServer.Web.HealthChecks;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Lepton.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Swashbuckle;
using Volo.Chat;
using Volo.Chat.Web;
using Volo.FileManagement.Web;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.MultiTenancy;
using Volo.Abp.BlobStoring.Azure;
using Microsoft.AspNetCore.Http.Features;
using Volo.Abp.IdentityServer.Jwt;
using Volo.Forms.Permissions;
using CORE.MVC.SQLServer;
using Volo.Abp.Caching;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;

namespace CORE.MVC.SQLServer.Web
{
    [DependsOn(
        typeof(SQLServerHttpApiModule),
        typeof(SQLServerApplicationModule),
        typeof(SQLServerEntityFrameworkCoreModule),
        typeof(AbpAutofacModule),
        typeof(AbpIdentityWebModule),
        typeof(AbpAccountPublicWebIdentityServerModule),
        typeof(AbpAuditLoggingWebModule),
        typeof(LeptonThemeManagementWebModule),
        typeof(SaasHostWebModule),
        typeof(AbpAccountAdminWebModule),
        typeof(AbpIdentityServerWebModule),
        typeof(LanguageManagementWebModule),
        typeof(AbpAspNetCoreMvcUiLeptonThemeModule),
        typeof(TextTemplateManagementWebModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAspNetCoreSerilogModule)
        )]
    [DependsOn(typeof(ChatSignalRModule))]
    [DependsOn(typeof(ChatWebModule))]
    [DependsOn(typeof(AbpBlobStoringModule))]
    [DependsOn(typeof(FileManagementWebModule))]
    [DependsOn(typeof(AbpBlobStoringFileSystemModule))]
    //[DependsOn(typeof(MasterDataModuleWebModule))]
    //[DependsOn(typeof(OutboundModuleWebModule))]
    //[DependsOn(typeof(ReportModuleWebModule))]
    //[DependsOn(typeof(ShareDataModuleWebModule))]
    //[DependsOn(typeof(InboundModuleWebModule))]
    [DependsOn(typeof(AbpBlobStoringAzureModule))]
    public class SQLServerWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                  typeof(SQLServerResource),
                    typeof(SQLServerDomainModule).Assembly,
                   typeof(SQLServerDomainSharedModule).Assembly,
                    typeof(SQLServerApplicationContractsModule).Assembly,
                    typeof(SQLServerWebModule).Assembly
                );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            ConfigureBundles();
            ConfigureUrls(configuration);
            ConfigurePages(configuration);
            ConfigureCache(configuration);
            //ConfigureRedis(context, configuration, hostingEnvironment);
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureNavigationServices(configuration);
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);
            ConfigureMultiTenancy();
            //ConfigureExternalProviders(context);
            //ConfigureHealthChecks(context);
            ConfigureAbpExceptionHandling(context.Services, hostingEnvironment);
            //Configure<AbpBlobStoringOptions>(options =>
            //{
            //    options.Containers.ConfigureDefault(container =>
            //    {
            //        if (Boolean.Parse(configuration["AzureStorageAccountSettings:ContainersUseAzure"]))
            //        {
            //            container.UseAzure(azure =>
            //            {
            //                azure.ConnectionString = configuration["AzureStorageAccountSettings:ConnectionString"];
            //                azure.CreateContainerIfNotExists = true;
            //            });
            //        }
            //        else
            //        {
            //            container.UseFileSystem(fileSystem =>
            //            {
            //                fileSystem.BasePath = configuration["FileSysBasePath"];
            //                fileSystem.AppendContainerNameToBasePath = true;
            //            });
            //        }
            //    });
            //});
            ConfigureFormCountLimitServices(context.Services);

            //Configure<AbpTenantResolveOptions>(options =>
            //{
            //    if (!hostingEnvironment.IsDevelopment())
            //    {
            //        options.AddDomainTenantResolver("{0}.aoms.vn");
            //    }
            //});
            //context.Services.AddSameSiteCookiePolicy(); // cookie policy to deal with temporary browser incompatibilities
        }

        //private void ConfigureHealthChecks(ServiceConfigurationContext context)
        //{
        //    context.Services.AddAOMSHealthChecks();
        //}
        private void ConfigureMultiTenancy()
        {
            Configure<AbpMultiTenancyOptions>(options => { options.IsEnabled = MultiTenancyConsts.IsEnabled; });
        }

        private void ConfigureCache(IConfiguration configuration)
        {
            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "SQLServer:";
            });
        }
        private void ConfigureBundles()
        {
            Configure<AbpBundlingOptions>(options =>
            {
                options.StyleBundles.Configure(
                    LeptonThemeBundles.Styles.Global,
                    bundle =>
                    {
                        bundle.AddFiles("/global-styles.css");
                    }
                );
            });
        }
    //    private void ConfigureRedis(
    //ServiceConfigurationContext context,
    //IConfiguration configuration,
    //IWebHostEnvironment hostingEnvironment)
    //    {
    //        if (!hostingEnvironment.IsDevelopment())
    //        {
    //            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
    //            context.Services
    //                .AddDataProtection()
    //                .PersistKeysToStackExchangeRedis(redis, "SQLServer-Protection-Keys");
    //        }
    //    }


        private void ConfigurePages(IConfiguration configuration)
        {
            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizePage("/Index", SQLServerPermissions.Dashboard.Index);
                options.Conventions.AuthorizePage("/HostDashboard", SQLServerPermissions.Dashboard.Host);
                options.Conventions.AuthorizePage("/TenantDashboard", SQLServerPermissions.Dashboard.Tenant);
                //options.Conventions.AuthorizePage("/Samples/Index", SQLServerPermissions.Samples.Default);
                options.Conventions.AuthorizePage("/Xamples/Index", SQLServerPermissions.Xamples.Default);
            });
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication()
                   .AddCookie("Cookies", options =>
                   {
                       options.ExpireTimeSpan = TimeSpan.FromDays(365);
                   })
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "CORE.MVC.SQLServer";
                })
                ;
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<SQLServerWebModule>();
            });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<SQLServerWebModule>();

                if (hostingEnvironment.IsDevelopment())
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<SQLServerDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}CORE.MVC.SQLServer.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<SQLServerApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}CORE.MVC.SQLServer.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<SQLServerHttpApiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}CORE.MVC.SQLServer.HttpApi", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<SQLServerWebModule>(hostingEnvironment.ContentRootPath);
                }
            });
        }

        private void ConfigureNavigationServices(IConfiguration configuration)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new SQLServerMenuContributor(configuration));
            });

            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new SQLServerToolbarContributor());
            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(SQLServerApplicationModule).Assembly);
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "AOMS API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );
        }

        private void ConfigureAbpExceptionHandling(IServiceCollection services, IHostEnvironment env)
        {
            services.Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = env.IsDevelopment();
            });
        }

        //private void ConfigureExternalProviders(ServiceConfigurationContext context)
        //{
        //    context.Services.AddAuthentication()
        //        .AddGoogle(GoogleDefaults.AuthenticationScheme, _ => { })
        //        .WithDynamicOptions<GoogleOptions, GoogleHandler>(
        //            GoogleDefaults.AuthenticationScheme,
        //            options =>
        //            {
        //                options.WithProperty(x => x.ClientId);
        //                options.WithProperty(x => x.ClientSecret, isSecret: true);
        //            }
        //        )
        //        .AddMicrosoftAccount(MicrosoftAccountDefaults.AuthenticationScheme, options =>
        //        {
        //            //Personal Microsoft accounts as an example.
        //            options.AuthorizationEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize";
        //            options.TokenEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/token";
        //        })
        //        .WithDynamicOptions<MicrosoftAccountOptions, MicrosoftAccountHandler>(
        //            MicrosoftAccountDefaults.AuthenticationScheme,
        //            options =>
        //            {
        //                options.WithProperty(x => x.ClientId);
        //                options.WithProperty(x => x.ClientSecret, isSecret: true);
        //            }
        //        )
        //        .AddTwitter(TwitterDefaults.AuthenticationScheme, options => options.RetrieveUserDetails = true)
        //        .WithDynamicOptions<TwitterOptions, TwitterHandler>(
        //            TwitterDefaults.AuthenticationScheme,
        //            options =>
        //            {
        //                options.WithProperty(x => x.ConsumerKey);
        //                options.WithProperty(x => x.ConsumerSecret, isSecret: true);
        //            }
        //        );
        //}

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
                app.UseHsts();
            }

            app.UseCookiePolicy(); // added this, Before UseAuthentication or anything else that writes cookies.
            app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseUnitOfWork();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "AOMS API");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }

        private void ConfigureFormCountLimitServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });
        }
    }
}