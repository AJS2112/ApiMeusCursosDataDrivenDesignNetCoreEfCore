using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeusCursos.Data;
using MeusCursos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeusCursos.Controllers
{
    [Route("courses")]
    public class CourseController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Course>>> Get(
            [FromServices] DataContext context
        )
        {
            var courses = await context.Courses
                                    .AsNoTracking()
                                    .Include(x => x.Category)
                                    .Include(x => x.Technology)
                                    .Include(x => x.Platform)
                                    .Include(x => x.Tutor)
                                    .ToListAsync();

            return Ok(courses);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Course>> GetById(
            int id,
            [FromServices] DataContext context
        )
        {
            var course = await context.Courses
                                .AsNoTracking()
                                .Include(x => x.Category)
                                .Include(x => x.Technology)
                                .Include(x => x.Platform)
                                .Include(x => x.Tutor)
                                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(course);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Course>> Post(
            [FromBody] Course model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Courses.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível criar o curso, erro: {ex.Message}" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Course>> Put(
            int id,
            [FromBody] Course model,
            [FromServices] DataContext context
        )
        {
            if (model.Id != id)
                return NotFound(new { message = "Curso não encontrado" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Course>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível atualizar o curso, erro: {ex.Message}" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Course>> Delete(
            int id,
            [FromServices] DataContext context
        )
        {
            var course = await context.Courses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (course == null)
                return NotFound(new { message = "Curso não encontrado" });

            try
            {
                context.Courses.Remove(course);
                await context.SaveChangesAsync();
                return Ok(new { message = "Curso removido com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível remover o curso, erro: {ex.Message}" });
            }
        }
    }
}