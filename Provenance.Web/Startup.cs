using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Provenance.Common.Configurations;
using Provenance.ServiceLayer.Bootstrapper;
using Provenance.Web.Modules;
using Provenance.Web.Modules.Middlewares;
using Provenance.Web.Modules.Policies;
using System;
using System.IO;
using System.Reflection;

namespace Provenance.Web
{
	public class Startup
	{

		public Startup (IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }



		public void ConfigureServices (IServiceCollection services)
		{
			services.Init();

			services.AddControllers();

			services.AddHttpContextAccessor();

			ConfigureIdentityServerValidation(services);

			ConfigureSwagger(services);

		}

		private void ConfigureIdentityServerValidation (IServiceCollection services)
		{
			services.Configure<IdentitySetting>(Configuration.GetSection("IdentitySetting"));

			var _IdentitySetting = Configuration.GetSection(nameof(IdentitySetting)).Get<IdentitySetting>();


			services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
				.AddIdentityServerAuthentication(options =>
				{
					options.Authority = _IdentitySetting.Authority;
					options.ApiName = _IdentitySetting.ClientId;
					//refrence token //options.ApiSecret = _IdentitySetting.ClientSecret;

					options.EnableCaching = true;
					options.RequireHttpsMetadata = false;

				});

			services.AddAuthorization(options =>
			{
				options.AddPolicy(Policies.ACTIVE, policy => policy.Requirements.Add(new UserIsActiveRequirement()));
				options.AddPolicy(RoleTypes.ADMIN, policy => policy.RequireClaim(CustomClaims.ROLE, RoleTypes.ADMIN));
				options.AddPolicy(RoleTypes.USER, policy => policy.Requirements.Add(new UserIsInRoleRequirement(RoleTypes.USER)));
			});

			services.AddTransient<IClaimsTransformation, ClaimsExtender>();
		}

		private static void ConfigureSwagger (IServiceCollection services)
		{
			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Version = "v1",
					Title = "API",
					Description = "WTXHUB.COM Provenance System API",
					Contact = new Microsoft.OpenApi.Models.OpenApiContact()
					{
						Name = "Mohammadreza Tarkhan",
						Email = "mohammadreza.tarkhan@gmail.com",
						Url = new Uri("https://www.wtxhub.com/")
					}
				});

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please insert JWT with Bearer into field",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
						},
						new string[] { }
					}
				});

				c.IncludeXmlComments(xmlPath);
			});
		}

		public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//app.UseMiddleware<ExceptionMiddleware>();

			var forwardOptions = new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedProto,
			};

			app.UseForwardedHeaders(forwardOptions);

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseProcessTokenDataMiddleware();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Provenance API V1");
			});
		}



	}
}
