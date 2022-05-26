using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCRUD.Dtos
{
    public class UsuarioRegisterDto
    {

       
        public string Nombre { get; set; }
        public string Correo { get; set; }   

        public string Password { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }
        

        public UsuarioRegisterDto()
        {
            FechaDeAlta = DateTime.Now;
            Activo = true;

            
        }

    
        
    }
}