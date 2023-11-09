using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FernandoHerreroR
{
    public class Persona
    {
        private int codigo;
        private string nombre;
        private List<Libro> libros=new List<Libro>();
        Persona(int codigo, string nombre,List<Libro> libros) { 
            this.Codigo = codigo; 
            this.Nombre = nombre;
            this.Libros = libros;
        }

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        internal List<Libro> Libros { get => libros; set => libros = value; }
    }
}
