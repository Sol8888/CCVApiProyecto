namespace CCVApiProyecto.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using global::CCVApiProyecto.Data;

    namespace CCVApiProyecto.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class EstudianteController : ControllerBase
        {
            private readonly AppDbContext _context;

            public EstudianteController(AppDbContext context)
            {
                _context = context;
            }

            // Ver actividades de las clases asignadas
            [HttpGet("GetActivities/{studentId}")]
            public async Task<IActionResult> GetActivities(int studentId)
            {
                var estudiante = await _context.Users.Include(u => u.Role)
                    .Include(u => u.Clases)
                    .ThenInclude(c => c.Actividades)
                    .FirstOrDefaultAsync(u => u.Id == studentId && u.Role.Name == "Estudiante");

                if (estudiante == null) return NotFound("Estudiante no encontrado");

                var actividades = estudiante.Clases.SelectMany(c => c.Actividades).ToList();
                return Ok(actividades);
            }
        }
    }

}
