namespace plantillasFernando.Models
{

    public class Jugador
    {

        public Jugador()
        {
            nombre = obtenerNombre();
            apellido1 = obtenerApellido();
            apellido2 = obtenerApellido();
            valor = obtenerValor();
        }
        public string idJugador { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public int valor { get; set; }
        public string equipoJugador { get; set; }

        /*Métodos para obtener los campos*/
        public string obtenerNombre()
        {
            Random r = new Random();
            string[] nombres = {"Cesar", "Pablo", "Mario", "Denice", "Fernando", "Rubén", "Andrés", "Andrea", "Juan", "Adnet", "Antonio", "Alberto",
            "Raul", "Ángel", "Javier", "Alejandro", "Eva", "Isabel", "Saray", "Carlos" };

            int pos = r.Next(0, nombres.Length);
            return nombres[pos];
        }
        public string obtenerApellido()
        {
            Random r = new Random();
            string[] apellidos = {"Cabo", "Herrero", "Benito", "Canepa", "Ricote", "Carretero", "López", "Varillas", "Sima", "Fuentes", "Luna",
            "Mateo", "García", "Ancín", "Neila", "Martinez", "Arconada", "Hoya", "Rodríguez","Sánchez"};
            int pos = r.Next(0, apellidos.Length);
            return apellidos[pos];
        }
        public int obtenerValor()
        {
            Random r = new Random();
            int val = r.Next(50000, 125001);
            return val;
        }
    }
}

