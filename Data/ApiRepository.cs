using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCRUD.Data.Interfaces;
using WebApiCRUD.Models;

namespace WebApiCRUD.Data
{
    public class ApiRepository : IApiRepository
    {
        private readonly DataContext _context;

        public ApiRepository(DataContext _context)
        {
            this._context = _context;
        
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
            
        }

    

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(product => product.Id == id);
            return producto;

        }

        public async Task<Producto> GetProductoByNombreAsync(string nombre)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(product => product.Nombre == nombre);
            return producto;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == id);
            return usuario;
        }

        public async Task<Usuario> GetUsuarioByNombreAsync(string nombre)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Nombre == nombre);
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}