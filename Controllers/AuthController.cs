using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiCRUD.Data.Interfaces;
using WebApiCRUD.Dtos;
using WebApiCRUD.Models;
using WebApiCRUD.Services.Interfaces;

namespace WebApiCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AuthController(IAuthRepository repo, ITokenService tokenService, IMapper mapper)
        {
            this.repo = repo;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        [HttpPost("register")]

        public async Task<ActionResult<UsuariosListDto>> Register(UsuarioRegisterDto usuarioDto)
        {
            usuarioDto.Correo = usuarioDto.Correo.ToLower();
            var existe = await repo.ExisteUsuario(usuarioDto.Correo);
            if(existe)
                return  BadRequest("El usuario ya existe");

            
            var usuarioNuevo = mapper.Map<Usuario>(usuarioDto);
            var usuarioCreado = await repo.Registrar(usuarioNuevo, usuarioDto.Password);
            var usuarioCreadoDto = mapper.Map<UsuariosListDto>(usuarioCreado);
        
            return Ok(usuarioCreadoDto);           
                  
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UsuariosListDto>> Login(UsuarioLoginDTO usuarioLoginDto)
        {

            var usuarioFromRepo = await repo.Login(usuarioLoginDto.Correo, usuarioLoginDto.Password);

            if(usuarioFromRepo == null)
                 return Unauthorized();
            var usuario = mapper.Map<UsuariosListDto>(usuarioFromRepo);
            var token = tokenService.CreateToken(usuarioFromRepo);

            return Ok(new {
                token = token, 
                usuario = usuario


            });

        }

    }

        

        
    
}