
//Para adicionar a arvore de objetos adicionamos uma nova biblioteca JSON
//dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
        //Definimos nossa rota do controller e dizemos que é um controller de API
        [Route("api/[controller]")]
        [ApiController]
    public class UsuarioController:ControllerBase
    {
        GufosContext _contexto = new GufosContext();
        //GET :api/Usuario
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get ()
        {
           var usuarios = await _contexto.Usuario.ToListAsync(); 
           if (usuarios ==null){
               return NotFound();
           }
            return usuarios;
        }

        //GET: api/Usuario/2

        [HttpGet("{id}")]

        public async Task<ActionResult<Usuario>> Get (int id){

            //FindAsync = procura algo especifico no banco

            var usuarios = await  _contexto.Usuario.FindAsync(id);
            if (usuarios ==null){
                return NotFound();
            }
            return usuarios;
        }
            //Post api/Usuario
            [HttpPost]
            public async Task<ActionResult<Usuario>> Post(Usuario usuario){

                try{
                    //Tratamos contra ataques do SQL injection

                    await _contexto.AddAsync(usuario);

                    //Salvamos efetivamente o nosso objeto no Banco de Dados.
                    await _contexto.SaveChangesAsync();
                }catch(DbUpdateConcurrencyException){
                    throw;
                }
                return usuario;


            }
    }
}