using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FernandoHerreroR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void entra(object sender, RoutedEventArgs e)
        {
            Entrar en = new Entrar();
            en.Show();
        }

        private void registro(object sender, RoutedEventArgs e)
        {
            Inscripcion i = new Inscripcion();
            i.Show();
        }

        private void baja(object sender, RoutedEventArgs e)
        {
            Baja b = new Baja();
            b.Show();
        }

        private void sale(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void anade(object sender, RoutedEventArgs e)
        {

        }

        private void busca(object sender, RoutedEventArgs e)
        {

        }
    }
}
