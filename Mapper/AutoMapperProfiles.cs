using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApiCRUD.Dtos;
using WebApiCRUD.Models;

namespace WebApiCRUD.Mapper
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {

            //se mapea el endpoint post o Create
            CreateMap<ProductoCreateDTO, Producto>();

            //PUT o Update

            CreateMap<ProductoUpdateDTO, Producto>();

            //Get o List

            CreateMap<Producto, ProductoToListDto>();

            CreateMap<UsuarioRegisterDto, Usuario>();

            CreateMap<UsuarioLoginDTO, Usuario>();

            CreateMap<Usuario,UsuariosListDto>();



            
        }
        
    }
}