using Microsoft.EntityFrameworkCore;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.Servicios;
using System.Reflection;
using Restaurante_API.Servicios;
using Restaurante_API.Middleware;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Restaurante_API;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//---------------------------------------JWT Swagger-----------------------------

//builder.Services.AddSwaggerGen();//para que aparezca aut en Swagger
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "JWT", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese Token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
        {
        {new OpenApiSecurityScheme
        {
             Reference = new OpenApiReference
             { Type = ReferenceType.SecurityScheme,
              Id = "Bearer"
             }
        },
        new string[]{}

        }
    });
});

//--------------------------------------------------------------------------------
//-----------------------------JWT---------------------------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    JwtSettings? jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

                    TokenValidationParameters tokenValidationParameters = new TokenValidationParameters();

                    tokenValidationParameters.ValidateIssuerSigningKey = true;
                    tokenValidationParameters.ValidIssuer = jwtSettings.Issuer;
                    tokenValidationParameters.ValidAudience = jwtSettings.Audience;
                    tokenValidationParameters.IssuerSigningKey = key;
                    tokenValidationParameters.ClockSkew = TimeSpan.Zero;

                    options.TokenValidationParameters = tokenValidationParameters;
                });

///----------------------------------------------------------------------------
///----------------------------------------------------------------------------


// **Configuración de Autorización con Roles**
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireSocioRole", policy => policy.RequireRole("Socio"));
    options.AddPolicy("RequireEmpleadoRole", policy => policy.RequireRole("Socio", "Empleado"));
});

//----------------------------------------------------------------------------
builder.Services.AddDbContext<DataContext>(op => op.UseSqlServer(
    builder.Configuration.GetConnectionString("ConnectionStringEF")));

// Inyección Mapper
//builder.Services.AddScoped<PedidoMapper>(); // PAra mapper manual
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Inyeccion de dependecia de los Servicio, Scoped se crea una nueva instancia por ámbito o sea en cada petición HTTP
builder.Services.AddScoped<IClienteServicio, ClienteServicio>();
builder.Services.AddScoped<IMesaServicio, MesaServicio>();
builder.Services.AddScoped<IPedidoService, PedidoServicio>();
builder.Services.AddScoped<ISocioServicio, SocioServicio>();
builder.Services.AddScoped<IEmpleadoServicio, EmpleadoServicio>();
builder.Services.AddScoped<IComandaServicio, ComandaServicio>();
builder.Services.AddScoped<ILogEmpleadoServicio, LogEmpleadoServicio>();
builder.Services.AddScoped<AuthServicio>();
// Configurar la caché en memoria para las sesiones
builder.Services.AddDistributedMemoryCache();

// Configurar la sesión para guardar los logs
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Configurar el tiempo de espera de la sesión
    options.Cookie.HttpOnly = true; // Configurar la cookie de sesión como HttpOnly
    options.Cookie.IsEssential = true; // Marcar la cookie como esencial
});


var app = builder.Build();

// Usar la sesión
app.UseSession();

app.UseMiddleware<LogMiddleware>(); //Inyecto Middleware


//------------------------------------------------------------------------------------------------


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
