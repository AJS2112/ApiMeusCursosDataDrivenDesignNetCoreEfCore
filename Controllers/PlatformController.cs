using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeusCursos.Data;
using MeusCursos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeusCursos.Controllers
{
    [Route("platforms")]
    public class PlatformController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Platform>>> Get(
            [FromServices] DataContext context
        )
        {
            var platforms = await context.Platforms.AsNoTracking().ToListAsync();
            return Ok(platforms);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Platform>> Get(
            int id,
            [FromServices] DataContext context
        )
        {
            var platform = await context.Platforms.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(platform);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Platform>> Post(
            [FromBody] Platform model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Platforms.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível criar a plataforma, erro: {ex.Message}" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Platform>> Put(
            int id,
            [FromBody] Platform model,
            [FromServices] DataContext context
        )
        {
            if (model.Id != id)
                return NotFound(new { message = "Plataforma não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Platform>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível atualizar a plataforma, erro: {ex.Message}" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Platform>> Delete(
            int id,
            [FromServices] DataContext context
        )
        {
            var platform = await context.Platforms.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (platform == null)
                return NotFound(new { message = $"Plataforma não encontrada" });

            try
            {
                context.Platforms.Remove(platform);
                await context.SaveChangesAsync();
                return Ok(new { message = "Plataforma removida com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível remover a plataforma, erro: {ex.Message}" });
            }

        }

    }
}