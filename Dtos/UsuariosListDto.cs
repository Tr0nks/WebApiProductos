using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCRUD.Dtos
{
    public class UsuariosListDto
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }   
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }
        
    }
}