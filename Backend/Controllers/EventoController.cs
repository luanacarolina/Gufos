using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Para adicionar a arvore de objetos adicionamos uma nova biblioteca JSON
//dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
namespace Backend.Controllers {
    //Definimos nossa rota do controller e dizemos que é um controller de API
    [Route ("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase {
        GufosContext _contexto = new GufosContext ();
        //GET : api/Evento/
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get () {
            //Include("")
            var eventos = await _contexto.Evento.Include ("Categoria").Include ("Localizacao").ToListAsync ();
            if (eventos == null) {
                return NotFound ();
            }

            return eventos;
        }

        //GET : api/Evento/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Evento>> Get (int id) {

            //FindAsync = procura algo especifico no banco
            var evento = await _contexto.Evento.Include ("Categoria").Include ("Localizacao").FirstOrDefaultAsync (e => e.EventoId == id);
            if (evento == null) {
                return NotFound ();
            }

            return evento;
        }

        //Post api/Evento
        [HttpPost]
        public async Task<ActionResult<Evento>> Post (Evento evento) {

            try {

                //Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync (evento);
                //Salvamos efetivamente o nosso objeto no Banco de dados.
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }

            return evento;

        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Evento evento) {

            // Se o Id do objeto não existir
            //ele retorna erro 404
            if (id != evento.CategoriaId) {
                return BadRequest ();
            }
            //Comparamos os atributos que foram modificados através do EF
            _contexto.Entry (evento).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {

                //Verificamos se o objeto inserido  realmente existe no banco
                var categoria_valido = await _contexto.Evento.FindAsync (id);

                if (categoria_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }

            }
            //  Nocontent = Retorna 204, sem nada
            return NoContent ();
        }

        //DELETE api/evento/id
        [HttpDelete ("{id}")]
        public async Task<ActionResult<Evento>> Delete (int id) {

            var evento = await _contexto.Evento.FindAsync (id);
            if (evento == null) {
                return NotFound ();
            }
            _contexto.Evento.Remove (evento);
            await _contexto.SaveChangesAsync ();

            return evento;
        }
    }
}