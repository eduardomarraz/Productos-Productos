using BusMensajes;
using Microservicio_Productos.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IMessageBus, AzServiceBusMessageBus>();
//Servicio necesario para recibir mensajes.
//*builder.Services.AddSingleton<IAzServiceBusConsumer, AzServiceBusConsumer>();

//Configuracion BD
builder.Services.AddDbContext<ProductsDbContext>(configuracion =>
configuracion.UseSqlServer(
    // Herramienta que sirve para leer la configuracion _
builder.Configuration["ConnectionStrings:conversorCadena"]
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//*app.UseAzServiceBusConsumer();

app.Run();
