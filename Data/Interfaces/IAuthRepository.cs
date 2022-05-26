using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCRUD.Models;

namespace WebApiCRUD.Data.Interfaces
{
    public interface IAuthRepository
    {

        public Task<Usuario> Registrar(Usuario usuario, string password);

        public Task<Usuario> Login(string correo, string password);

        public Task<bool> ExisteUsuario(string correo);
        
    }
}