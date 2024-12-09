namespace CCVApiProyecto.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using global::CCVApiProyecto.Data;
    using global::CCVApiProyecto.Models;

    namespace CCVApiProyecto.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ProfesorController : ControllerBase
        {
            private readonly AppDbContext _context;

            public ProfesorController(AppDbContext context)
            {
                _context = context;
            }

            // Crear una actividad para una clase
            [HttpPost("CreateActivity")]
            public async Task<IActionResult> CreateActivity([FromBody] Actividad actividad)
            {
                var clase = await _context.Clases.FirstOrDefaultAsync(c => c.Id == actividad.ClaseId);
                if (clase == null) return NotFound("Clase no encontrada");

                _context.Actividades.Add(actividad);
                await _context.SaveChangesAsync();
                return Ok(actividad);
            }

            // Ver las actividades de una clase
            [HttpGet("GetActivities/{claseId}")]
            public async Task<IActionResult> GetActivities(int claseId)
            {
                var actividades = await _context.Actividades.Where(a => a.ClaseId == claseId).ToListAsync();
                return Ok(actividades);
            }
        }
    }

}
