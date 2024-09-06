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

namespace ApiA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private const string BaseUrlApiA = "https://localhost:7101/api/Pelicula";

        private readonly BaseAContext _context;

        public PeliculasController(BaseAContext context)
        {
            _context = context;
        }

        // GET: api/Peliculas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeliculaDTO>>> GetPeliculas()
        {
            var peliculas = await _context.Peliculas.ToListAsync();
            return peliculas.Select(a => ConvertToPeliculaDTO(a)).ToList();
        }

        
    

        // GET: api/Peliculas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PeliculaDTO>> GetPelicula(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);

            if (pelicula == null)
            {
                return NotFound();
            }

            return ConvertToPeliculaDTO(pelicula);
        }

        // PUT: api/Peliculas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPelicula(int id, Pelicula pelicula)
        {
            if (id != pelicula.IdPelicula)
            {
                return BadRequest();
            }

            _context.Entry(pelicula).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculaExists(id))
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

        // POST: api/Peliculas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pelicula>> PostPelicula(Pelicula pelicula)
        {
            _context.Peliculas.Add(pelicula);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPelicula", new { id = pelicula.IdPelicula },ConvertToPeliculaDTO(pelicula));
        }

        // DELETE: api/Peliculas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePelicula(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeliculaExists(int id)
        {
            return _context.Peliculas.Any(e => e.IdPelicula == id);
        }

        private PeliculaDTO ConvertToPeliculaDTO(Pelicula pelicula)
        {
            return new PeliculaDTO
            {
                IdPelicula =pelicula.IdPelicula,
                Titulo = pelicula.Titulo,
                Genero =pelicula.Genero,
                Director = pelicula.Director,
                AnioEstreno = pelicula.AnioEstreno,
                Duracion= pelicula.Duracion,
                Sinopsis=pelicula.Sinopsis,
                
            };
        }

        private Pelicula ConvertToActor(PeliculaDTO peliculaDTO)
        {
            return new Pelicula
            {
                IdPelicula = peliculaDTO.IdPelicula,
                Titulo = peliculaDTO.Titulo,
                Genero = peliculaDTO.Genero,
                Director = peliculaDTO.Director,
                AnioEstreno = peliculaDTO.AnioEstreno,
                Duracion = peliculaDTO.Duracion,
                Sinopsis=peliculaDTO.Sinopsis,
            };
        }
    }
}
