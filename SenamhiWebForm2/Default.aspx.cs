using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration; //acceder a la cadena de configuracion

namespace SenamhiWebForm2
{
    public partial class _Default : Page
    {
        // Llamar al servicio web
        private ServiceReference1.ServicioSenamhiSoapClient servicio;
        private string busqueda = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewRegistros.Visible = true;
            servicio = new ServiceReference1.ServicioSenamhiSoapClient();

            string consulta = tbBusqueda.Text;
            DataSet datos = servicio.Listarlugares();
            GridViewRegistros.DataSource = datos.Tables[0];
            GridViewRegistros.DataBind();
        }

        protected void btBuscar_Click(object Sender, EventArgs e)
        {
            servicio = new ServiceReference1.ServicioSenamhiSoapClient();

            string consulta = tbBusqueda.Text;
            busqueda = consulta;
            DataSet datos = servicio.ListaLocalidad(TNombre: consulta);

            string strDatos = "[['Día', 'Temp. Max', 'Temp. Min'],";
            foreach (DataTable table in datos.Tables)
            {
                for(int i=0; i < table.Rows.Count; i++)
                {   
                    DataRow row = table.Rows[i];

                    string lugar = row["Lugar"].ToString();
                    DateTime fecha = DateTime.Parse(row["Fecha"].ToString());
                    int tmin = int.Parse(row["Tmin"].ToString());
                    int tmax = int.Parse(row["Tmax"].ToString());
                    int tprom = (int)((tmin + tmax) / 2);
                    int esta = int.Parse(row["esta"].ToString());
                    string desc = row["Descripción"].ToString();

                    // Pintar datos de la tabla registros

                    strDatos += "[";
                    strDatos += "'" + fecha.Day + "'," + tmax + "," + tmin;
                    strDatos += "]";

                    // Si se llega al ultimo item
                    if (i == table.Rows.Count - 1)
                    {
                        // Pintar datos de Hoy
                        lbCiudad.Text = lugar;
                        lbTempMinMax.Text = tmin.ToString() + " °C" + " - " + tmax.ToString() + " °C"; ;
                        lbTempHoy.Text = tprom.ToString() + " °C";
                        lbDesc.Text = desc;

                        string[] estadosRutas = { "~/Imagenes/cielo-cubierto.png",
                                              "~/Imagenes/cielo-nubes-dispersas.png",
                                              "~/Imagenes/cielo-despejado.png",
                                              "~/Imagenes/cielo-nublado-lluvia.png",
                                              "~/Imagenes/cielo-cubierto-tormenta.png"};
                        imagenEstado.ImageUrl = estadosRutas[esta - 1];
                    }
                }
                strDatos += "]";
                /*
                foreach (DataRow row in table.Rows)
                {
                    string lugar = row["Lugar"].ToString();
                    DateTime fecha = DateTime.Parse(row["Fecha"].ToString());
                    int tmin = int.Parse(row["Tmin"].ToString());
                    int tmax = int.Parse(row["Tmax"].ToString());
                    int esta = int.Parse(row["esta"].ToString());
                    string desc = row["Descripción"].ToString();

                }*/
            }
            GridViewRegistros.Visible = false;
            GridViewRegistros.DataSource = datos.Tables[0];
            GridViewRegistros.DataBind();
        }

        protected string obtenerDatos()
        {
            servicio = new ServiceReference1.ServicioSenamhiSoapClient();

            string consulta = busqueda;
            if (consulta == "" || consulta == null) return "[['e'], [1]]";
            DataSet datos = servicio.ListaLocalidad(TNombre: consulta);

            string strDatos = "[['Día', 'Temp. Max', 'Temp. Min'],";
            foreach (DataTable table in datos.Tables)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];

                    string lugar = row["Lugar"].ToString();
                    DateTime fecha = DateTime.Parse(row["Fecha"].ToString());
                    int tmin = int.Parse(row["Tmin"].ToString());
                    int tmax = int.Parse(row["Tmax"].ToString());
                    int tprom = (int)((tmin + tmax) / 2);
                    int esta = int.Parse(row["esta"].ToString());
                    string desc = row["Descripción"].ToString();

                    // retornar dichos datos
                    strDatos += "[";
                    strDatos += "'" + fecha.ToShortDateString() + "'," + tmax + "," + tmin;
                    strDatos += "],";
                }
                strDatos += "]";
            }
            System.Diagnostics.Debug.WriteLine("=====" + strDatos);
            
            return strDatos;
        }

        protected void btListarTodo_Click(object Sender, EventArgs e)
        {
            servicio = new ServiceReference1.ServicioSenamhiSoapClient();

            string consulta = tbBusqueda.Text;
            DataSet datos = servicio.Listarlugares();
            GridViewRegistros.DataSource = datos.Tables[0];
            GridViewRegistros.DataBind();
        }
    }
}