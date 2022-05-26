using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using WebApiCRUD.Models;
using WebApiCRUD.Services.Interfaces;

namespace WebApiCRUD.Services
{
    public class TokenService : ITokenService
    {

        private readonly SymmetricSecurityKey llave;

        public TokenService(IConfiguration config)
        {
            llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token"]));
        }
        
        public string CreateToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Correo", usuario.Correo)
            };

            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = credenciales

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return  tokenHandler.WriteToken(token);
            
        }
    }
}