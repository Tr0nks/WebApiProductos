using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCRUD.Data.Interfaces;
using WebApiCRUD.Models;

namespace WebApiCRUD.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;

        public AuthRepository(DataContext context)
        {
            this.context = context;
        }

        //se valida si existe el usuario
        public async Task<bool> ExisteUsuario(string correo)
        {
            if( await context.Usuarios.AnyAsync(usuario => usuario.Correo == correo))
            {
                return true;
            }
            return false;
        }

        public async  Task<Usuario> Login(string correo, string password)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(user => user.Correo == correo);
            if(usuario == null)
                return null;
            if(!ComprobarPasswordHash(password, usuario.PasswordHash,usuario.PasswordSalt))
                return null;
            return usuario;
        }


        public bool ComprobarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt )
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                        return false;
                }

            }

            return true;
        }


        public async Task<Usuario> Registrar(Usuario usuario, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();
            return usuario;
            
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }

        }
    }
}