using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApiCRUD.Data;
using WebApiCRUD.Data.Interfaces;
using WebApiCRUD.Mapper;
using WebApiCRUD.Services;
using WebApiCRUD.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//se agrega a servicios el repositorio para poder hacer la inyeccion para que se pueda usar en los controladores
builder.Services.AddScoped<IApiRepository, ApiRepository>();

//se agrega a los servicios el repositrorio de IAuthRepository para poder hacer la injeccion de dependencias
builder.Services.AddScoped<IAuthRepository,AuthRepository>();

//se agrega a los servicios el automapper para poder hacer la inyeccion 
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);


//Se agrega como servicio el Context para poder hacer la inyeccion de dependencia

builder.Services.AddDbContext<DataContext>( options => 
                                            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//se agrega Token Service

builder.Services.AddScoped<ITokenService, TokenService>();

//se agrega el servicio de Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =  new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Token"])),
        ValidateIssuer = false,
        ValidateAudience = false


    };


});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//se agrega este middleware para que permita cuando enviamos el Token
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
