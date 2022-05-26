namespace WebApiCRUD.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }   
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }

        public byte[] PasswordHash { get; set; }             
        public byte[]  PasswordSalt { get; set; }     
    }

    // Para JWT hay que instalar los siguientes Paquetes Nugget

// dotnet add package Microsoft.IdentityModel.Tokens
// dotnet add package System.IdentityModel.Tokens.Jwt
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

//se Agrega lo siguiente al Services

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
// .AddJwtBearer(options => 
// {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuerSigningKey = true,
//         IssuerSigningKey =  new SymmetricSecurityKey
//         (Encoding.UTF8.GetBytes(builder.Configuration["Token"])),
//         ValidateIssuer = false,
//         ValidateAudience = false


//     };


// });


}