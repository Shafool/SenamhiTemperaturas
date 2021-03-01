using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Data;
using System.Data.SqlClient;
using System.Configuration; //acceder a la cadena de configuracion

namespace ServidorSemana
{
    /// <summary>
    /// Descripción breve de ServicioSenamhi
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioSenamhi : System.Web.Services.WebService
    {
        // Acceder a la cadena de coneccion
        private static string cadena = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;

        [WebMethod(Description = "Mostrar las por lugar")]
        public DataSet Listarlugares()
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "spListarTodo ";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataSet datos = new DataSet(); // Llevarlo a un buffer de memoria
                adaptador.Fill(datos);

                return datos;
            }
        }

        [WebMethod(Description = "Listar x localidad")]

        public DataSet ListaLocalidad (string TNombre)
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "spListarLocalidad @TNombre";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@TNombre", TNombre);
                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataSet datos = new DataSet(); // Llevarlo a un buffer de memoria
                adaptador.Fill(datos);

                return datos;
            }
        }
    }
}
