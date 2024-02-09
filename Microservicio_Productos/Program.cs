using BusMensajes;
using Extensions;
using MediatR;
using Microservicio_Productos.DbContexts;
using Microservicio_Productos.Messaging;
using Microservicio_Productos.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IMessageBus, AzServiceBusMessageBus>();

// Configuración de MediatR
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
//Servicio necesario para recibir mensajes.

builder.Services.AddSingleton<IAzServiceBusConsumer, AzServiceBusConsumer>();

//Configuracion BD
builder.Services.AddDbContext<ProductsDbContext>(configuracion =>
configuracion.UseSqlServer(
    // Herramienta que sirve para leer la configuracion _
builder.Configuration.GetConnectionString("conexionAzure"),
sqlServerOptionsAction: sqlOptions =>
{
    sqlOptions.EnableRetryOnFailure(); // Puedes especificar números de error específicos aquí
}
    )
);
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAzServiceBusConsumer();

app.Run();
