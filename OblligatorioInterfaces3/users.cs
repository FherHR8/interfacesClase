using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OblligatorioInterfaces3
{
    internal class users
    {
        private int id;
        private string username;
        private string password;
        private string email;
        private int puntos;
        private bool rol;
        users()
        {
            this.id = 0;
            this.username = string.Empty;
            this.password = string.Empty;
            this.email = string.Empty;
            this.puntos = 0;
            this.rol = false;
        }
    }
}
