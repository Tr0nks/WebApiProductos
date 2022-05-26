using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCRUD.Models;

namespace WebApiCRUD.Services.Interfaces
{
    public interface ITokenService
    {

        string CreateToken(Usuario usuario);
        
    }
}