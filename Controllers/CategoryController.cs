using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeusCursos.Data;
using MeusCursos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeusCursos.Controllers
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get(
            [FromServices] DataContext context
        )
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetById(
            int id,
            [FromServices] DataContext context
            )
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return Ok(category);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Post(
            [FromBody] Category model,
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
                return BadRequest(new { message = $"Não foi possivel criar a categoria, erro {ex.Message}" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Category>>> Put(
            int id,
            [FromBody] Category model,
            [FromServices] DataContext context
            )
        {
            if (model.Id != id)
                return NotFound(new { message = "Categoria no encontrada" });

            if (ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = $"Este registro já foi atualizado" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possivel atualizar a categoria, erro: {ex.Message}" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Category>>> Delete(
            int id,
            [FromServices] DataContext context
            )
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });

            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new { message = "Categoria removida com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível remover sua categoria, erro: {ex.Message}" });
            }
        }
    }
}