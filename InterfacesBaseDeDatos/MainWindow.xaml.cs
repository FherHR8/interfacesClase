using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BaseDeDatos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string nom;
        private string con;
        private string ema;
        private int pre;
        private Conexion dBConnect;
        private MySqlCommand cmd;
        private MySqlDataAdapter adaptador;
        private DataTable tablon;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void meter(object sender, RoutedEventArgs e)
        {
            nom = nombre.Text;
            con = contrasena.Text;
            ema = email.Text;
            try
            {
                pre = int.Parse(premios.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("El premio tiene un formato no válido");
            }
            Conexion c = new Conexion();
            c.Insertar(nom,con,ema,pre);
        }

        private void ver(object sender, RoutedEventArgs e)
        {
            dBConnect = new Conexion();
            adaptador = new MySqlDataAdapter("SELECT * from users", dBConnect.Conectar);
            tablon = new DataTable();
            adaptador.Fill(tablon);
            tabla.ItemsSource = tablon.DefaultView;
            dBConnect.CerrarConectar();
        }
    }
}
