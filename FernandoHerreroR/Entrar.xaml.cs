using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FernandoHerreroR
{
    /// <summary>
    /// Lógica de interacción para Entrar.xaml
    /// </summary>
    public partial class Entrar : Window
    {
        public Entrar()
        {
            string cod;
            string nom;
            InitializeComponent();
            nom = "";
            cod = "";
        }

        private bool entra(object sender, RoutedEventArgs e)
        {
            bool existe = false;
            string cod = co.Text;
            string nom = no.Text;
            StreamReader fichero;
            if (File.Exists("Personas.txt"))
            {
                using (fichero = File.OpenText("Personas.txt"))
                {
                    
                    string linea = fichero.ReadLine();
                    while (linea != null)
                    {
                        if (linea.Contains("Codigo: " + cod))
                        {
                            linea=fichero.ReadLine();
                            if(linea.Contains("Nombre: "+ nom))
                            existe = true;
                        }
                        linea=fichero.ReadLine();
                    }
                    fichero.Close();
                    if (existe)
                    {
                        MessageBox.Show("El usuario existe, en un futuro entraremos en algún sitio");
                    }
                    else
                    {
                        MessageBox.Show("El usuario no es correcto");
                    }
                }
            }
            return existe;
        }
    }
}
