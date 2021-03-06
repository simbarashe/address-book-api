﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Model.Entities;
using Model.Repositories;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using AddressBookApi.Dtos;

namespace AddressBookApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddOptions();

            services.Configure<Setting>(Configuration.GetSection("ConnectionStrings"));

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                            .AllowAnyMethod()
                                                             .AllowAnyHeader()));
            //services.AddRouting();
            services.AddMvc()
                    .AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddSingleton<IContactRepository, ContactRepository>();

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            //services.AddSwaggerGen();
            services.AddSwaggerGen(c=>c.SingleApiVersion(new Swashbuckle.Swagger.Model.Info { Title = "Address Book API", Version = "v1"}));
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "Addressbook API", Version = "v1" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            //Use the new policy globally
            app.UseCors("AllowAll");
             //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=contacts}/{action=Index}/{id?}");
            });
            ConfigureMappings();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();
        }

        private static void ConfigureMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Contact, ContactDto>()
                   .ForMember(dest => dest.Tags,
                   opts => opts.MapFrom(src => Mapper.Map<IEnumerable<ContactTag>, IEnumerable<ContactTagDto>>(src.ContactTags)))
                   .ForMember(dest => dest.CommunicationDetails,
                   opts => opts.MapFrom(src => Mapper.Map<IEnumerable<ContactCommunicationDetail>, IEnumerable<ContactCommunicationDetailDto>>(src.ContactCommunicationDetails)));
                cfg.CreateMap<ContactTag, ContactTagDto>()
                 .ForMember(dest => dest.TagId, opts => opts.MapFrom(src => src.Tag.Id))
                   .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Tag.Description));
                cfg.CreateMap<ContactCommunicationDetail, ContactCommunicationDetailDto>()
                .ForMember(dest => dest.Detail, opts => opts.MapFrom(src => src.Detail))
                 .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.CommunicationType.Description));
                cfg.CreateMap<Tag, TagDto>();
            });
        }
    }
}
