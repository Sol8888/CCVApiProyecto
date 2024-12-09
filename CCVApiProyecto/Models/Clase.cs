namespace CCVApiProyecto.Models
{
    public class Clase
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 
        public int MateriaId { get; set; }
        public Materia Materia { get; set; }
        public int ProfesorId { get; set; }
        public User Profesor { get; set; }
        public ICollection<User> Estudiantes { get; set; } = new List<User>();
        public ICollection<Actividad> Actividades { get; set; }
    }
}
