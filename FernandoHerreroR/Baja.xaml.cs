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
    /// Lógica de interacción para Baja.xaml
    /// </summary>
    public partial class Baja : Window
    {
        public Baja()
        {
            InitializeComponent();
        }
        private void borrado(object sender, RoutedEventArgs e)
        {
            StreamReader fichero;
            StreamWriter auxiliar;
            string n = codBorrar.Text;
            int c;
            if (int.TryParse(n, out c))
            {
                if (File.Exists("Personas.txt") && confirmar())
                {
                    using (fichero = File.OpenText("Personas.txt"))
                    using (auxiliar = File.CreateText("auxiliar.txt"))
                    {
                        bool existe = false;
                        string linea = fichero.ReadLine();
                        while (linea != null)
                        {
                            if (linea.Contains("Codigo: " + c))
                            {
                                existe = true;
                                linea = fichero.ReadLine();
                                linea = fichero.ReadLine();
                                linea = fichero.ReadLine();
                                linea = fichero.ReadLine();
                                linea = fichero.ReadLine();
                                linea = fichero.ReadLine();
                            }
                            auxiliar.WriteLine(linea);
                            linea = fichero.ReadLine();
                        }
                        fichero.Close();
                        auxiliar.Close();
                        File.Delete("Personas.txt");
                        File.Move("auxiliar.txt", "Personas.txt");

                        if (existe)
                        {
                            MessageBox.Show("Usuario borrado");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No hay aun ningun archivo");
                }
            }
            else
            {
                MessageBox.Show("El codigo no es correcto");
            }
        }
        private bool confirmar()
        {
            bool ok = false;
            MessageBoxResult respuesta = MessageBox.Show("¿Deseas borrar dicho usuario?",
            "¿Borrar?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (respuesta == MessageBoxResult.Yes)
            {
                ok = true;
            }
            return ok;
        }
    }
}
