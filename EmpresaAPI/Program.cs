using System;
using System.IO;
using EmpresaAPI.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddDbContext<EmpresaAPI.EntityModels.EmpresaContext>(
       options => options.UseSqlServer("Server=192.168.100.144;Database=Empresa;User Id=sa;Password=Naomi003;"), 
       ServiceLifetime.Singleton);

#region Swagger
// Add framework services.
builder.Services
    .AddMvc(options =>
    {
        options.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>();
        options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>();
    })
    .AddNewtonsoftJson(opts =>
    {
        opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
    })
    .AddXmlSerializerFormatters();


builder.Services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("1.0", new OpenApiInfo
        {
            Version = "1.0",
            Title = "Empresa",
            Description = "Empresa (Net 6)",
            Contact = new OpenApiContact()
            {
                Name = "William",
                Url = new Uri("http://wquimis.com"),
                Email = "yue@hotmail.es"
            },
            TermsOfService = new Uri("http://wquimis.com")
        });
        c.CustomSchemaIds(type => type.Name);
        c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}EmpresaAPI.xml");

                    // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
                    // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                    c.OperationFilter<GeneratePathParamsValidationFilter>();
    });

#endregion

#region AUtoMapper


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//services.AddAutoMapper(typeof(Startup));

var mappingConfig = new MapperConfiguration(cfg =>
{
    #region Añadir perfiles de mapeo
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
    /*
    //Metodo #2 para definir los perfiles de mapeo de la solucion                
    cfg.AddProfile< TS.Clientes.Negocio.Clientes.Perfiles.PCliente>();
    cfg.AddProfile< TS.Clientes.Infraestructura.Persistencia.Clientes.Perfiles.PCliente>();
    */
    #endregion

    #region Configuraciones adicionales
    cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
    /*property_name -> PropertyName*/
    cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
    cfg.AllowNullCollections = true;
    //mc.AllowNullDestinationValues = true;
    #endregion

});

mappingConfig.CompileMappings();
/*Guardando de forma estática con inyección de dependencia el objeto Mapper*/
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

#region Inyccion de dependencia

// Conexion de Repos siempre la misma por Peticion
builder.Services.AddScoped<EmpresaAPI.EntityModels.EmpresaContext>();
builder.Services.AddScoped<EmpresaAPI.Repository.IClientesRepos, EmpresaAPI.Repository.ClientesRepos>();
builder.Services.AddScoped<EmpresaAPI.Repository.ICuentasRepos, EmpresaAPI.Repository.CuentasRepos>();
builder.Services.AddScoped<EmpresaAPI.Repository.IMovimientosRepos, EmpresaAPI.Repository.MovimientosRepos>();

#endregion

#region Builder
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
        c.SwaggerEndpoint("/swagger/1.0/swagger.json", "Empresa");

        //TODO: Or alternatively use the original Swagger contract that's included in the static files
        // c.SwaggerEndpoint("/swagger-original.json", "Cuentas Original");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion
