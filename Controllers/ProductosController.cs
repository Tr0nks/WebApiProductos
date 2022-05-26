using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiCRUD.Data.Interfaces;
using WebApiCRUD.Dtos;
using WebApiCRUD.Models;

namespace WebApiCRUD.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IApiRepository _repo;
        private readonly IMapper mapper;

        public ProductosController(IApiRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this.mapper = mapper;
        }

    [HttpGet]
    public async Task<ActionResult<List<ProductoToListDto>>> Get()
    {
        var productos = await _repo.GetProductosAsync();
        return mapper.Map<List<ProductoToListDto>>(productos);
    }
  
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoToListDto>> Get(int id)
    {

        var producto = await _repo.GetProductoByIdAsync(id);
         if(producto == null )
            return NotFound("Producto no encontrado");

        return mapper.Map<ProductoToListDto>(producto);

    }

    [HttpGet("{nombre}")]
    public async Task<ActionResult<ProductoToListDto>> Get(string nombre)
    {
        var producto = await _repo.GetProductoByNombreAsync(nombre);
        if(producto == null)
            return NotFound("No se encontro el producto con este el nombre  : ");
        
        return mapper.Map<ProductoToListDto>(producto);


    }



    [HttpPost]

    public async Task<ActionResult> Post(ProductoCreateDTO productoDto)
    {
        
        var productoToCreate = mapper.Map<Producto>(productoDto);
       
       
        _repo.Add(productoToCreate);
        if(await _repo.SaveAll())
            return Ok(productoToCreate);
        return BadRequest();

    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(ProductoUpdateDTO productoDto, int id)
    {
        if(id != productoDto.Id)
            return BadRequest(" Los IDs no coinciden");

        var productoToUpdate =   await _repo.GetProductoByIdAsync(productoDto.Id);
        if(productoToUpdate == null)
            return BadRequest();

            //var productoToUpdate = mapper.Map<Producto>(productoDto);
            mapper.Map(productoDto,productoToUpdate);
          //_repo.Update(productoToUpdate);  
        if(!await _repo.SaveAll())
            return NoContent();
        return Ok(productoToUpdate);

    }

    
        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var producto = await _repo.GetProductoByIdAsync(id);
            if(producto == null)
                return NotFound("Producto no Encontrado");
            
            _repo.Delete(producto);
            if(!await _repo.SaveAll())
                return BadRequest("No se pudo borrar el Producto");
            
            return Ok("Prodcuto Borrado");
        }

   



    }
}