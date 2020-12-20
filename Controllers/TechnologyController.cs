using System;
using System.Threading.Tasks;
using MeusCursos.Data;
using MeusCursos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeusCursos.Controllers
{
    [Route("technologies")]
    public class TechonologyController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<Technology>> Get(
            [FromServices] DataContext context
        )
        {
            var technologies = await context.Techonologies.AsNoTracking().ToListAsync();
            return Ok(technologies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Technology>> GetById(
            int id,
            [FromServices] DataContext context
        )
        {
            var technologies = await context.Techonologies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(technologies);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Technology>> Post(
            [FromBody] Technology model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possivel criar a tecnologia, erro {ex.Message}" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Technology>> Put(
            int id,
            [FromBody] Technology model,
            [FromServices] DataContext context
        )
        {
            if (model.Id != id)
                return NotFound(new { message = "Tecnologia não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Technology>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = $"Este registro já foi atualizado" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possivel atualizar a tecnologia, erro: {ex.Message}" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Technology>> Delete(
            int id,
            [FromServices] DataContext context
        )
        {
            var technology = await context.Techonologies.FirstOrDefaultAsync(x => x.Id == id);

            if (technology == null)
                return NotFound(new { message = "Tecnologia não encontrada" });

            try
            {
                context.Techonologies.Remove(technology);
                await context.SaveChangesAsync();
                return Ok(new { message = "Tecnologia removida com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível remover a teconologia, erro: {ex.Message}" });
            }
        }
    }
}