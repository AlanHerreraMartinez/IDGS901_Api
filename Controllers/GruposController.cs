using IDGS901_Api.context;
using IDGS901_Api.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IDGS901_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : ControllerBase
    {
        public readonly AppDbContext _Context;

        public GruposController(AppDbContext context)
        {
            _Context = context;
        }
        [HttpGet]//api/<Grupos>
        public IActionResult Get()
        {
            try
            {
                return Ok(_Context.Alumnos.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}",Name ="Alumnos")]
        public IActionResult Get(int id)
        {
            try
            {
                var alum=_Context.Alumnos.FirstOrDefault(x => x.Id == id);
                return Ok(alum);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<Alumnos> Post([FromBody]Alumnos alum)
        {
            try
            {
                _Context.Alumnos.Add(alum);
                _Context.SaveChanges();
                return CreatedAtRoute("Alumnos", new {id=alum.Id},alum);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Alumnos alum)
        {
            try
            {
                if (alum.Id == id)
                {
                    _Context.Entry(alum).State =EntityState.Modified;
                    _Context.SaveChanges();
                    return CreatedAtRoute("Alumnos", new {id=alum.Id},alum);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var alum = _Context.Alumnos.FirstOrDefault(x => x.Id == id);
                if (alum != null)
                {
                    _Context.Remove(alum);
                    _Context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
