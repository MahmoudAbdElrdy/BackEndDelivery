using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using BackEnd.BAL.Models;
using RealState.DAL.Context;
using Microsoft.AspNetCore.Identity;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using Project.Options;
using Microsoft.OpenApi.Models;
using BackEnd.Web.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using System.Reflection;
using BackEnd.Service.ISercice;
using BackEnd.Service.Service;
using BackEnd.Service;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Repository;
using AutoMapper;
using BackEnd.Service.MappingProfiles;
using EmailService;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Chat.Hubs;

namespace BackEnd.Web
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
           
            // Inject Appsettings
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            // Serialized Returned Object With Same Format 
            #region AddController
            services.AddMvc()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddControllers().AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();

      }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            #endregion
            // Add service and create Policy with options
            #region services
            services.AddScoped<IResponseDTO, ResponseDTO>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfiles());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
            #region CorsPolicy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            #endregion
            services.AddDbContext<BakEndContext>(options =>
            options.UseLazyLoadingProxies(false)
            .UseSqlServer(Configuration.GetConnectionString("BakEndConnection")));

      services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<BakEndContext>();

      //password configurations

      services.Configure<IdentityOptions>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 4;
      });

      // Generic Config
      services.AddTransient<IBackEndContext, BakEndContext>();
      services.AddTransient(typeof( RoleManager<>), typeof( IdentityRole<>));
      services.AddScoped<ICityServices, CityServices>();
      services.AddScoped<ICountryServices, CountryServices>();
         services.AddScoped<ICategoryServices, CategoryServices>();
      services.AddScoped<ISubCategoryServices, SubCategoryServices>();
     
            services.AddSignalR();
            //----------------------------swagger-------------------------------------
            services.AddSwaggerGen(x =>
      {
        x.SwaggerDoc("v1", new OpenApiInfo { Title = "project Api", Version = "v1" });
        //-----------------------------start jwtSettings swagger ---------------------------------
        var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer",new string[0]}

                };
        x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description =
          "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
        });
        x.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,

                },
                new List<string>()
            }
        });
        //-----------------------------end jwtSettings swagger---------------------------------
       
      });

      //----------------------------jwtSettings-------------------------------------
      var jwtSettings = new ApplicationSettings();
      Configuration.Bind(nameof(ApplicationSettings), jwtSettings);
      services.AddSingleton(jwtSettings);

      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.JWT_Secret)),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = false
      };
      services.AddSingleton(tokenValidationParameters);

      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(x =>
      {
        x.SaveToken = true;
        x.TokenValidationParameters = tokenValidationParameters;
      });
            //----------------------------end jwtSettings-------------------------------------
            var emailConfig = Configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IemailService, emailService>();
            services.AddScoped<IidentityServices, IdentityServices>();
    }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/myapp-{Date}.txt");
            if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      //--------------swagger configuration------------------
      var swaggerOptions = new SwaggerOptions();
      Configuration.GetSection(nameof(swaggerOptions)).Bind(swaggerOptions);
      app.UseSwagger(option => {option.RouteTemplate = swaggerOptions.JsonRoute;});
      app.UseSwaggerUI(option =>
      {
        option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
      });
      //-------------end of swagger configuration-----------
      app.UseCors("CorsPolicy");
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"UploadFiles")),
            //    RequestPath = new PathString("/UploadFiles")
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller}/{action=Index}/{id?}");
            });


            //app.UseSpa(spa => {
            //    spa.Options.SourcePath = "ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});
            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/chathub");
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadFiles")),
                RequestPath = "/wwwroot/UploadFiles"
            });
           
        }
  }
}
