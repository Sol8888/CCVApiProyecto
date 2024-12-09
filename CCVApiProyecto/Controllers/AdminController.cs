using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCVApiProyecto.Data;
using CCVApiProyecto.Models;

namespace CCVApiProyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // Crear un nuevo usuario (Profesor o Estudiante)
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        // Crear una nueva materia
        [HttpPost("CreateMateria")]
        public async Task<IActionResult> CreateMateria([FromBody] Materia materia)
        {
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
            return Ok(materia);
        }

        // Crear una nueva clase
        [HttpPost("CreateClase")]
        public async Task<IActionResult> CreateClase([FromBody] Clase clase)
        {
            _context.Clases.Add(clase);
            await _context.SaveChangesAsync();
            return Ok(clase);
        }

        // Asignar un estudiante a una clase
        [HttpPost("AssignStudentToClass")]
        public async Task<IActionResult> AssignStudentToClass(int claseId, int studentId)
        {
            var clase = await _context.Clases.Include(c => c.Estudiantes).FirstOrDefaultAsync(c => c.Id == claseId);
            if (clase == null) return NotFound("Clase no encontrada");

            var student = await _context.Users.FirstOrDefaultAsync(u => u.Id == studentId && u.Role.Name == "Estudiante");
            if (student == null) return NotFound("Estudiante no encontrado");

            clase.Estudiantes.Add(student);
            await _context.SaveChangesAsync();
            return Ok(clase);
        }
    }
}
