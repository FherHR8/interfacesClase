using Microsoft.AspNetCore.Mvc;
using FireSharp;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography.X509Certificates;
using Pepe.Models;

namespace Pepe.Controllers
{
    public class MantenedorController : Controller
    {
        IFirebaseClient cliente;

        public MantenedorController()
        {
            IFirebaseConfig config = new FirebaseConfig()
            {
                AuthSecret = "bMunUt9fcMzaV9d780hxXgsQWJpZA8jagzZQPUIr",
                BasePath = "https://gestor-de-equipos-default-rtdb.firebaseio.com/"
            };
            cliente = new FirebaseClient(config);    
        }
        [HttpPost]
        public ActionResult Crear(contacto oContacto)
        {   //Añadimos un contacto en firebase y añade a la etiqueta contactos
            string IdGenerado = Guid.NewGuid().ToString("N");

            SetResponse response = cliente.Set("Contactos/" + IdGenerado, oContacto);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return View();
            }
            else
            {
                return View();
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Inicio()
        {
            Dictionary<string, contacto> lista = new Dictionary<string, contacto>();
            FirebaseResponse response = cliente.Get("Contactos");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                lista = JsonConvert.DeserializeObject<Dictionary<string, contacto>>(response.Body);

            List<contacto> listaContacto = new List<contacto>();

            foreach(KeyValuePair<string, contacto> elemento in lista)
            {
                listaContacto.Add(new contacto()
                {
                    idContacto = elemento.Key,
                    Nombre = elemento.Value.Nombre,
                    Correo = elemento.Value.Correo,
                    Telefono = elemento.Value.Telefono
                });
            }
            return View(listaContacto);
        }
        public IActionResult Crea()
        {
            return View();
        }
        public IActionResult Editar()
        {
            return View();
        }
        public IActionResult Eliminar()
        {
            return View();
        }
    }
}
