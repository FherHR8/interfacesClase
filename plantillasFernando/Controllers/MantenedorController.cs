using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using plantillasFernando.Models;

namespace plantillasFernando.Controllers
{
    public class MantenedorController : Controller
    { //Controlador que gestiona clase http
        IFirebaseClient cliente;

        public MantenedorController()
        {
            IFirebaseConfig config = new FirebaseConfig //variable que interactua con firebase
            {   //Almacenamos la configuración
                AuthSecret = "bMunUt9fcMzaV9d780hxXgsQWJpZA8jagzZQPUIr",
                BasePath = "https://gestor-de-equipos-default-rtdb.firebaseio.com/"
            };
            cliente = new FirebaseClient(config);
            //crear una nueva instancia de cliente con la configuración dada (listo para interactuar con la bbdd)
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        //Sólo se aceptarán solicitudes de creación mediante HttpPost
        public ActionResult Crear(Usuario u)
        {   //Añadimos un contacto en firebase y devuelve una acción.
            string IdGenerado = Guid.NewGuid().ToString("N");
            //Crea un identificador global sin guiones
            List<Usuario> lista = obtenerUsuarios();

            if (lista.Count == 0)
            {
                SetResponse response = cliente.Set("Usuarios/" + IdGenerado, u);
                //realiza la escritura en bbdd, almacenando en la ubicación Contactos con el id generado
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Jugador j = new Jugador();
                        if (i < 5)
                            j.equipoJugador = u.equipo;
                        else
                            j.equipoJugador = "libre";
                        IdGenerado = Guid.NewGuid().ToString("N");
                        response = cliente.Set("Jugadores/" + IdGenerado, j);
                        //Procedemos a hacer lo mismo con jugadores (Que como son varios, hay que iterar con un foreach
                    }
                    //Si la operación es exitosa devuelve la vista de inicio
                    ViewBag.nombre = u.equipo;
                    //Gracias al Viewbag, puedo guardar un valor para mostrarlo en inicio sin importar la clase.


                    return RedirectToAction("Inicio", "Mantenedor");
                }
                else
                {

                    return View();
                }
            }
            else
            {
                //En caso de que haya en la lista algún usuario, se borra y nos devuelve la vista, para volver a perdir datos
                Eliminar();
                return View();
            }
        }
        public List<Jugador> obtenerJugadores()
        {
            List<Jugador> listaJugadores = new List<Jugador>();
            Dictionary<string, Jugador> lista = new Dictionary<string, Jugador>();
            try
            {
                // Realizamos una solicitud GET para obtener todos los usuarios desde Firebase
                FirebaseResponse response = cliente.Get("Jugadores");
                // Verificamos si la solicitud fue exitosa
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // Deserializa (convierte) la respuesta JSON (cadena) en una lista de usuarios
                    lista = JsonConvert.DeserializeObject<Dictionary<string, Jugador>>(response.Body);

                    if (lista != null)
                    {
                        // Recorre la nueva lista y agrega elementos a la lista existente
                        foreach (KeyValuePair<string, Jugador> elemento in lista)
                        {
                            listaJugadores.Add(new Jugador()
                            {
                                idJugador = elemento.Key,
                                nombre = elemento.Value.nombre,
                                apellido1 = elemento.Value.apellido1,
                                apellido2 = elemento.Value.apellido2,
                                valor = elemento.Value.valor,
                                equipoJugador = elemento.Value.equipoJugador
                            });
                        }//Se crea una lista con todos los jugadores para retornarla
                    }
                }
            }
            catch (Exception e)
            {
                TempData["MensajeError"] = "Error inesperado: " + e.Message;
            }

            return listaJugadores;
        }
        public List<Usuario> obtenerUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            Dictionary<string, Usuario> lista = new Dictionary<string, Usuario>();
            try
            {

                FirebaseResponse response = cliente.Get("Usuarios");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    lista = JsonConvert.DeserializeObject<Dictionary<string, Usuario>>(response.Body);


                    if (lista != null)
                    {
                        // Recorremos la nueva lista y agrega elementos a la lista existente
                        foreach (KeyValuePair<string, Usuario> elemento in lista)
                        {
                            listaUsuarios.Add(new Usuario()
                            {
                                idUsuario = elemento.Key,
                                nombreUsuario = elemento.Value.nombreUsuario,
                                equipo = elemento.Value.equipo,
                                presupuesto = elemento.Value.presupuesto
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                TempData["MensajeError"] = "Error inesperado: " + e.Message;
            }

            return listaUsuarios;
        }



        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Inicio()
        {
            List<Usuario> u = obtenerUsuarios();//Mostramos los jugadores
            FirebaseResponse respuestaJugadores = cliente.Get("Jugadores");
            //Dictionary<string, Jugador> lista = JsonConvert.DeserializeObject<Dictionary<string, Jugador>>(respuestaJugadores.Body);
            List<Jugador> listaJugadores = obtenerJugadores();

            /*foreach (KeyValuePair<string, Jugador> elemento in lista)
            {
                listaJugadores.Add(new Jugador()
                {
                    idJugador = elemento.Key,
                    nombre = elemento.Value.nombre,
                    apellido1 = elemento.Value.apellido1,
                    apellido2 = elemento.Value.apellido2,
                    valor = elemento.Value.valor,
                    equipoJugador = elemento.Value.equipoJugador
                });
            }*/
            ViewBag.nombre = u[0].equipo;
            ViewBag.presupuesto = u[0].presupuesto;

            return View(listaJugadores);
        }

        public ActionResult Editar(string idJugador)
        {
            FirebaseResponse response = cliente.Get("Jugadores/" + idJugador);
            Console.WriteLine(response.ToString());
            Jugador jugador = response.ResultAs<Jugador>();
            jugador.idJugador = idJugador;

            List<Usuario> u = obtenerUsuarios();
            ViewBag.nombre = u[0].equipo;
            ViewBag.presupuesto = u[0].presupuesto;

            return View(jugador);//A partir del id, obtenemos el jugador completo
        }
        [HttpPost]
        public ActionResult Editar(Jugador jugador)
        {
            //Proceso de edicion de este jugador. No indico mas ya que hay errores
            Console.WriteLine("Soy el jugador: " + jugador.nombre);
            string idJug = jugador.idJugador;
            jugador.idJugador = null;
            Console.WriteLine(idJug);

            FirebaseResponse response = cliente.Update("Jugadores/" + idJug, jugador);
            Console.WriteLine(jugador.idJugador);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                List<Usuario> u = obtenerUsuarios();
                ViewBag.nombre = u[0].equipo;
                ViewBag.presupuesto = u[0].presupuesto;
                Console.WriteLine(u[0].equipo);
                Console.WriteLine(u[0]);

                return RedirectToAction("Inicio", "Mantenedor");
            }
            else
            {
                return View();
            }
        }

        public void Eliminar()
        {
            //Metodo para eliminar a todos los usuarios y los jugadores
            List<Usuario> users = obtenerUsuarios();
            Console.WriteLine(users[0].idUsuario);
            FirebaseResponse res = cliente.Delete("Usuarios/" + users[0].idUsuario);
            List<Jugador> gamer = obtenerJugadores();
            Console.WriteLine(gamer.Count);
            for (int i = 0; i < gamer.Count; i++)
            {
                res = cliente.Delete("Jugadores/" + gamer[i].idJugador);
            }
        }
    }
}

