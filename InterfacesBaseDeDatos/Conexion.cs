using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace BaseDeDatos
{
    public class Conexion
    {
        private MySqlConnection conectar;
        private string servidor;
        private string baseDatos;
        private string nombre;
        private string contrasena;

        public Conexion(){
            Iniciar();
        }

        public void Iniciar()
        {
            servidor = "www.mimartinezgmg.com";
            baseDatos = "mimartin_educainova";
            nombre = "mimartin_educainova";
            contrasena = "YGGc]{smfJ)57ZY*VV3D";
            String cadenaConectar = "SERVER=" + servidor + ";" + "DATABASE=" + baseDatos + ";" + "UID=" + nombre + ";" + "PASSWORD=" + contrasena+";";
            conectar=new MySqlConnection(cadenaConectar);
        }
        public bool AbrirConectar(){
            try
            {
                conectar.Open();
                MessageBox.Show("Conecta");
                return true;
            }catch (MySqlException ex)
            {
                switch(ex.Number){
                    case 0:
                        MessageBox.Show("No se puede conectar con el servidor");
                        break;
                    case 1045:
                        MessageBox.Show("Contraseña o usuario no válidos");
                        break;
                    default:
                        MessageBox.Show("Otro error"+ex.Message+ex.Number);
                        break;
                }
                return false;
            }
        }
        public bool CerrarConectar()
        {
            try
            {
                conectar.Close();
                MessageBox.Show("Cerrado con éxito");
                return true;
            }catch (MySqlException ex)
            {
                MessageBox.Show("Error al cerrar");
                return false;
            }
        }
        public void Insertar(string nom, string con, string ema, int pre)
        {
            string meter = "INSERT INTO users (username,password,email,puntos) VALUES ('"+nom+"','"+con+"','"+ema+"',"+pre+")";
            if (this.AbrirConectar())
            {
                MySqlCommand cmd = new MySqlCommand(meter, conectar);

                cmd.ExecuteNonQuery();

                this.CerrarConectar();
            }
        }
        public MySqlConnection Conectar { get => conectar; set => conectar = value; }
        public string Servidor { get => servidor; set => servidor = value; }
        public string BaseDatos { get => baseDatos; set => baseDatos = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Contrasena { get => contrasena; set => contrasena = value; }
    }
}
