using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CRUD
{
    class Conexion
    {
        public static SqlConnection Conectar()
        {
            //-------------------Conexion con la base de datos---------//

            SqlConnection cn = new SqlConnection("SERVER=DESKTOP-J0BDLOG; DATABASE=bdcolegio;integrated security=true");
            cn.Open();
            return cn;
        }
    }
}
