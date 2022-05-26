using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCRUD.Models;

namespace WebApiCRUD.Data.Interfaces
{
    public interface IApiRepository
    {
        public void Add<T>(T entity) where T: class;

        public void Delete<T>(T entity) where T: class;

        

        public  Task<bool> SaveAll();

        public Task<IEnumerable<Usuario>> GetUsuariosAsync();

        public Task<Usuario> GetUsuarioByIdAsync(int id);

        public Task<Usuario> GetUsuarioByNombreAsync(string nombre);
        public Task<IEnumerable<Producto>> GetProductosAsync();
        public Task<Producto> GetProductoByIdAsync(int id);

        public Task<Producto> GetProductoByNombreAsync(string nombre);

        
    }
}