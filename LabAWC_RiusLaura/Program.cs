using Microsoft.EntityFrameworkCore;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.Servicios;
using System.Reflection;
using Restaurante_API.Servicios;
using Restaurante_API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(op => op.UseSqlServer(
    builder.Configuration.GetConnectionString("ConnectionStringEF")));

// Inyecci�n Mapper
//builder.Services.AddScoped<PedidoMapper>(); // PAra mapper manual
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Inyeccion de dependecia de los Servicio, Scoped se crea una nueva instancia por �mbito o sea en cada petici�n HTTP
builder.Services.AddScoped<IClienteServicio, ClienteServicio>();
builder.Services.AddScoped<IMesaServicio, MesaServicio>();
builder.Services.AddScoped<IPedidoService, PedidoServicio>();
builder.Services.AddScoped<ISocioServicio, SocioServicio>();
builder.Services.AddScoped<IEmpleadoServicio, EmpleadoServicio>();
builder.Services.AddScoped<IComandaServicio, ComandaServicio>();
builder.Services.AddScoped<ILogEmpleadoServicio, LogEmpleadoServicio>();

// Configurar la cach� en memoria para las sesiones
builder.Services.AddDistributedMemoryCache();

// Configurar la sesi�n para guardar los logs
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Configurar el tiempo de espera de la sesi�n
    options.Cookie.HttpOnly = true; // Configurar la cookie de sesi�n como HttpOnly
    options.Cookie.IsEssential = true; // Marcar la cookie como esencial
});


var app = builder.Build();

// Usar la sesi�n
app.UseSession();

app.UseMiddleware<LogMiddleware>(); //Inyecto Middleware


//------------------------------------------------------------------------------------------------


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
