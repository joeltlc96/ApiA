
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiA.Context;
using ApiA.Models;
using ApiA.DTO;
using System.Text;
using System.Text.Json;

namespace ApiA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase

    {
        private const string BaseUrlApiB = "https://localhost:7101/api/Actor";


        private readonly BaseAContext _context;

        public ActorsController(BaseAContext context)
        {
            _context = context;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> GetActors()
        {
            var actors = await _context.Actors.ToListAsync();
            return actors.Select(a => ConvertToActorDTO(a)).ToList();
        }

        // GET: api/Actors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDTO>> GetActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return ConvertToActorDTO(actor);
        }

        // PUT: api/Actors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, ActorDTO actorDTO)
        {
            if (id != actorDTO.IdActor)
            {
                return BadRequest();
            }

            var actor = ConvertToActor(actorDTO);
            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Actors
        [HttpPost]
        public async Task<ActionResult<ActorDTO>> PostActor(ActorDTO actorDTO)
        {
            var actor = ConvertToActor(actorDTO);
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetActor), new { id = actor.IdActor }, ConvertToActorDTO(actor));
        }

        static async Task PostActorAsync(Actor objActor)
        {
            using (HttpClient client = new HttpClient())
            {
                string contenidoJson = JsonSerializer.Serialize(objActor);
                var contenidoPeticion = new StringContent(contenidoJson, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage respuestaHttp = await client.PostAsync(BaseUrlApiB, contenidoPeticion);

                    if (respuestaHttp.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Actor creado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {respuestaHttp.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Excepción: {e.Message}");
                }
            }
        }

        // DELETE: api/Actors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.IdActor == id);
        }

        private ActorDTO ConvertToActorDTO(Actor actor)
        {
            return new ActorDTO
            {
                IdActor = actor.IdActor,
                Nombre = actor.Nombre,
                Apellido = actor.Apellido,
                FechaNacimiento = actor.FechaNacimiento,
                Nacionalidad = actor.Nacionalidad,
                GeneroBiografia = actor.GeneroBiografia,
                Premios = actor.Premios,
                NumeroPeliculas = actor.NumeroPeliculas,
            };
        }

        private Actor ConvertToActor(ActorDTO actorDTO)
        {
            return new Actor
            {
                IdActor = actorDTO.IdActor,
                Nombre = actorDTO.Nombre,
                Apellido = actorDTO.Apellido,
                FechaNacimiento = actorDTO.FechaNacimiento,
                Nacionalidad = actorDTO.Nacionalidad,
                GeneroBiografia = actorDTO.GeneroBiografia,
                Premios = actorDTO.Premios,
                NumeroPeliculas = actorDTO.NumeroPeliculas,
            };
        }
    }
}