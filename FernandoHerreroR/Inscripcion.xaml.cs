using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Lógica de interacción para Inscripcion.xaml
    /// </summary>
    public partial class Inscripcion : Window
    {
        string nom;
        int cod;
        string lib;
        string opi;
        string tip;
        public Inscripcion()
        {
            InitializeComponent();
            cod = 0;
            nom = "";
            lib = "";
            opi = "";
            tip = "";


        }


        private void registro(object sender, RoutedEventArgs e)
        {
            if (validar())
            {
                cod = obtenerCodigo();
                MessageBox.Show(""+cod);
                codigo.Text = cod.ToString();
                escribir(cod);
            }
        }
        private bool validar()
        {
            bool ok = true;
            if (string.IsNullOrWhiteSpace(nombre.Text))
            {
                ok = false;
                MessageBox.Show("El nombre no puede estar vacío");
            }
            else if (string.IsNullOrWhiteSpace(libro.Text))
            {
                ok = false;
                MessageBox.Show("Debes indicar un libro");
            }
            else if (string.IsNullOrWhiteSpace(opinion.Text))
            {
                ok = false;
                MessageBox.Show("Debes escribir una opinion de tu primer libro");
            }
            else if (tipo.SelectedIndex == -1)
            {
                ok = false;
                MessageBox.Show("Debes elegir un tipo");
            }
            return ok;
        }
        private int obtenerCodigo()
        {
            int cod = 0;
            StreamReader fichero;
            string linea;
            if (File.Exists("personas.txt"))
            {
                fichero = File.OpenText("personas.txt");
                linea = fichero.ReadLine();
                while (linea != null)
                {
                    if (linea.StartsWith("Codigo: "))
                    {
                        cod = int.Parse(linea.Substring(8));
                    }
                    linea = fichero.ReadLine();
                }
            }
            cod++;
            return cod;
        }
        private void escribir(int cod)
        {
            Microsoft.Win32.SaveFileDialog guardar = new Microsoft.Win32.SaveFileDialog();
            guardar.DefaultExt = ".txt";
            guardar.Filter = "Documentos de texto (.txt)|*.txt";
            Nullable<bool> result = guardar.ShowDialog();
            if (result == true)
            {
                string documento = guardar.FileName;
                using (StreamWriter fichero = File.AppendText("personas.txt"))
                {
                    fichero.WriteLine("Codigo: " + cod);
                    fichero.WriteLine("Nombre: " + nombre.Text);
                    fichero.WriteLine("Libros: " + libro.Text);
                    fichero.WriteLine("Opinion: " + opinion.Text);
                    fichero.WriteLine("Tipo: " + tipo.Text);
                    fichero.WriteLine("");
                    fichero.Close();
                    MessageBox.Show("Registro realizado");
                }
            }
        }
    }
}

