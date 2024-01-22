namespace plantillasFernando.Models
{

    public class Usuario
    {
        public string idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string equipo { get; set; }
        public int presupuesto { get; set; }

        public Usuario()
        {
            nombreUsuario = "";
            equipo = "";
            presupuesto = 1000000;
        }
    }
}
