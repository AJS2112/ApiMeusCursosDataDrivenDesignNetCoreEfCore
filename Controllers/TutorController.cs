using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeusCursos.Data;
using MeusCursos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeusCursos.Controllers
{
    [Route("tutors")]
    public class TutorController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Tutor>>> Get(
            [FromServices] DataContext context
        )
        {
            var tutors = await context.Tutors.AsNoTracking().ToListAsync();
            return Ok(tutors);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Tutor>> GetById(
            int id,
            [FromServices] DataContext context
        )
        {
            var tutor = await context.Tutors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(tutor);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Tutor>> Post(
            [FromBody] Tutor model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Tutors.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível criar o tutor, erro: {ex.Message}" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Tutor>> Put(
            int id,
            [FromBody] Tutor model,
            [FromServices] DataContext context
        )
        {
            if (model.Id != id)
                return NotFound(new { message = "Tutor não encontrado" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Tutor>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível atualizar o tutor, erro: {ex.Message}" });
            }

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Tutor>> Delete(
            int id,
            [FromServices] DataContext context
        )
        {
            var tutor = await context.Tutors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (tutor == null)
                return NotFound(new { message = "Tutor não encontrado" });

            try
            {
                context.Tutors.Remove(tutor);
                await context.SaveChangesAsync();
                return Ok(new { message = "Tutor removido com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível remover o tutor, erro: {ex.Message}" });
            }
        }
    }
}