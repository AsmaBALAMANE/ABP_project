using MainApp.EntityFrameworkCore;
using MainApp.Localization;
using MainApp.MultiTenancy;
using MainApp.Web.Filters;
using MainApp.Web.Menus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TodoListModule;
using TodoListModule.Web;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Security.Claims;
using Volo.Abp.Studio.Client.AspNetCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace MainApp.Web;

[DependsOn(
    typeof(MainAppHttpApiModule),
    typeof(MainAppApplicationModule),
    typeof(MainAppEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpStudioClientAspNetCoreModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpFeatureManagementWebModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
[DependsOn(typeof(TodoListModuleWebModule))]
public class MainAppWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(MainAppResource),
                typeof(MainAppDomainModule).Assembly,
                typeof(MainAppDomainSharedModule).Assembly,
                typeof(MainAppApplicationModule).Assembly,
                typeof(MainAppApplicationContractsModule).Assembly,
                typeof(MainAppWebModule).Assembly
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });

        if (!hostingEnvironment.IsDevelopment())
        {
            PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
            {
                options.AddDevelopmentEncryptionAndSigningCertificate = false;
            });

            PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
            {
                serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx", configuration["AuthServer:CertificatePassPhrase"]!);
                serverBuilder.SetIssuer(new Uri(configuration["AuthServer:Authority"]!));
            });
        }
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        if (!configuration.GetValue<bool>("App:DisablePII"))
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.LogCompleteSecurityArtifact = true;
        }

        if (!configuration.GetValue<bool>("AuthServer:RequireHttpsMetadata"))
        {
            Configure<OpenIddictServerAspNetCoreOptions>(options =>
            {
                options.DisableTransportSecurityRequirement = true;
            });

            Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
            });
        }

        ConfigureBundles();
        ConfigureUrls(configuration);
        ConfigureAuthentication(context);
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureNavigationServices();
        ConfigureAutoApiControllers();
        ConfigureSwaggerServices(context.Services);

        Configure<PermissionManagementOptions>(options =>
        {
            options.IsDynamicPermissionStoreEnabled = true;
        });
    }


    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-scripts.js");
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MainAppWebModule>();
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MainAppWebModule>();

            if (hostingEnvironment.IsDevelopment())
            {
                options.FileSets.ReplaceEmbeddedByPhysical<MainAppDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}MainApp.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MainAppDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}MainApp.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MainAppApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}MainApp.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MainAppApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}MainApp.Application", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MainAppHttpApiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}MainApp.HttpApi", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MainAppWebModule>(hostingEnvironment.ContentRootPath);
            }
        });
    }

    private void ConfigureNavigationServices()
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new MainAppMenuContributor());
        });

        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new MainAppToolbarContributor());
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(MainAppApplicationModule).Assembly);

        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        var configuration = services.GetConfiguration();
        services.AddAbpSwaggerGenWithOAuth(
        configuration["AuthServer:Authority"]!, // authority issuer
        new Dictionary<string, string>
        {                                      // scopes
            {"MainApp", "MainApp"}
        },
        options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Practical Test API",
                Description = ".NET Backend Developer - ABP Framework Practical Test",
            });

            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName), includeControllerXmlComments: true);

            options.DocInclusionPredicate((docName, description) => true);
            options.CustomSchemaIds(type => type.FullName);
            options.DocumentFilter<CustomSwaggerFilter>();
        }
        );
    }


    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        app.UseForwardedHeaders();

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

        app.UseCorrelationId();
        app.MapAbpStaticAssets();
        app.UseAbpStudioLink();
        app.UseRouting();
        app.UseAbpSecurityHeaders();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {

            options.SwaggerEndpoint("/swagger/v1/swagger.json", "MainApp API");
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            options.OAuthClientId("MainApp_Swagger"); // clientId

        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
