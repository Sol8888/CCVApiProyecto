﻿namespace CCVApiProyecto.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Clase> Clases { get; set; }
    }
}
