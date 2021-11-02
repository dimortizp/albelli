using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.UseCases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using Data;
using Data.Mappings;
using Api.Mappers;

namespace Api
{
    public class Startup
    {
        private readonly Container _container = new Container();
        private ApiSettings _settings;

        // This method gets called by the runtime. Use this method to add services to the Container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddSimpleInjector(_container, options =>
            {
                options
                    .AddAspNetCore()
                    .AddControllerActivation();

            });

            _settings = new ApiSettings(GetConfiguration());
            services.RegisterPersistence(_container, _settings.ConnectionString);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.AddProfile(new MappingProfileApi());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void InitializeContainer()
        {
            _container.Register<IRequestHandler<IEnumerable<ProductType>>, GetAllOrderTypes>();
            _container.Register<IRequestHandler<string, ProductType>, GetOrderType>();
            _container.Register<IRequestHandler<Order, int>, CreateOrder>();
            _container.Register<IRequestHandler<int, Order>, GetOrder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InitializeContainer();
        }

        private IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            return config;
        }
    }
}
