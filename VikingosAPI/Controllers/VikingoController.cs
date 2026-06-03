using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VikingosAPI.Models;

namespace VikingosAPI.Controllers
{
    [RoutePrefix("api/vikingo")]
    public class VikingoController : ApiController
    {
        private static List<Vikingo> vikingos = new List<Vikingo>
        {
            new Vikingo{ Id = 1, Nombre = "Vikingo1", BatallasGanadas = 2, ArmaFavoritaId = 1, MuerteGloriosaId = 1, NivelHonorId = 1},
            new Vikingo{ Id = 2, Nombre = "Vikingo2", BatallasGanadas = 5, ArmaFavoritaId = 2, MuerteGloriosaId = 2, NivelHonorId = 2 },
            new Vikingo{ Id = 3, Nombre = "Vikingo3", BatallasGanadas = 8, ArmaFavoritaId = 3, MuerteGloriosaId = 3, NivelHonorId = 1 },
            new Vikingo{ Id = 4, Nombre = "Vikingo4", BatallasGanadas = 4, ArmaFavoritaId = 2, MuerteGloriosaId = 3, NivelHonorId = 3 },
            new Vikingo{ Id = 5, Nombre = "Vikingo5", BatallasGanadas = 3, ArmaFavoritaId = 1, MuerteGloriosaId = 2, NivelHonorId = 1 }
        };

        /// <summary>
        /// Obtiene la lista de vikingos
        /// GET: api/vikingo
        /// </summary>
        [HttpGet]
        [Route("")]
        public IEnumerable<Vikingo> GetVikingos()
        {
            return vikingos;
        }

        /// <summary>
        /// Obtiene un vikingo por Id
        /// GET: api/vikingo/{id}
        /// </summary>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetVikingo(int id)
        {
            var persona = vikingos.FirstOrDefault(p => p.Id == id);
            if (persona == null)
                return NotFound();

            return Ok(persona);
        }

        /// <summary>
        /// Guardar un nuevo vikingo
        /// POST: api/vikingo
        /// </summary>
        [HttpPost]
        [Route("")]
        public IHttpActionResult CrearVikingo([FromBody] Vikingo nuevoVikingo)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            nuevoVikingo.Id = vikingos.Count + 1;
            vikingos.Add(nuevoVikingo);

            return Created($"api/vikingo/{nuevoVikingo.Id}", nuevoVikingo);
        }

        /// <summary>
        /// Editar vikingo existente
        /// PUT: api/vikingo/{id}
        /// </summary>
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult EditarVikingo(int id, [FromBody] Vikingo vikingoEditar)
        {
            var vikingo = vikingos.FirstOrDefault(p => p.Id == id);
            if (vikingo == null)
                return NotFound();

            vikingo.Nombre = vikingoEditar.Nombre;
            vikingo.BatallasGanadas = vikingoEditar.BatallasGanadas;
            vikingo.ArmaFavoritaId = vikingoEditar.ArmaFavoritaId; 
            vikingo.NivelHonorId = vikingoEditar.NivelHonorId;
            vikingo.MuerteGloriosaId = vikingoEditar.MuerteGloriosaId;

            return Ok(vikingo);
        }

        /// <summary>
        /// Eliminar vikingo
        /// DELETE: api/vikingo/{id}
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult EliminarVikingo(int id)
        {
            var vikingo = vikingos.FirstOrDefault(p => p.Id == id);
            if (vikingo == null)
                return NotFound();

            vikingos.Remove(vikingo);
            return Ok($"Vikingo con Id {id} eliminada");
        }
    }
}
