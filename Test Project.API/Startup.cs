using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Test_Project.API.ActionFilter;
using Test_Project.API.Middlewares;
using Test_Project.BLL.Managers;
using Test_Project.BLL.Managers.Interfaces;
using Test_Project.BLL.Managers.Settings;
using Test_Project.BLL.Profiles;
using Test_Project.DAL.Database.Entities;
using Test_Project.DAL.Repositories;
using Test_Project.DAL.Repositories.Interfaces;
using Test_Project.DAL.UnitOfWork.Interface;

namespace Test_Project.API
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
            AddDbContext(services);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            // Add Mapper
            services.AddSingleton(mapper);

            // Add UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IRelatedPersonRepository, RelatedPersonRepository>();

            // Add Managers
            services.AddScoped<IPersonManager, PersonManager>();
            services.AddScoped<IRelatedPersonManager, RelatedPersonManager>();
            services.AddScoped<IExceptionLogManager, ExceptionLogManager>();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidationActionFilter));
            });
            AddJsonOptions(services);

            services.AddLocalization();
            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();
        }
        private void AddJsonOptions(IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(ApiSettings),
                Configuration.GetSection(nameof(ApiSettings))
                .Get<ApiSettings>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<LocalizationMiddleware>();

            var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ka") };
            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("en")),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            loggerFactory.AddFile(@"../Test Project.Shared/Logs/TestProject-{Date}.txt");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void AddDbContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("TestProjectDataContext");
            int commandTimeOut = int.Parse(Configuration.GetConnectionString("CommandTimeout"));

            services.AddDbContext<TestProjectDataContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.CommandTimeout(commandTimeOut);
                });
            });
        }
    }
}
