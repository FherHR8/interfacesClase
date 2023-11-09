using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FernandoHerreroR
{
    public class Libro
    {
        private string nombre;
        private string opinion;
        private string tipo;

        Libro(string nombre, string opinion,string tipo)
        {
            this.Nombre = nombre;
            this.Opinion = opinion;
            this.Tipo = tipo;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Opinion { get => opinion; set => opinion = value; }
        public string Tipo { get => tipo; set => tipo = value; }
    }
    
}
