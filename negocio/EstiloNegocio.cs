using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using System.CodeDom;

namespace negocio
{
    public class EstiloNegocio
    {
        public List<Estilo> Listar() 
        { 
            List<Estilo> listaEstilos = new List<Estilo>();
            AccesoDatos  Datos = new AccesoDatos();

            try
            {
                Datos.SetearConsulta("select Id, Descripcion from ESTILOS");
                Datos.EjecutarLectura();
                while (Datos.Lector.Read()) 
                {
                    Estilo auxiliar = new Estilo();
                    auxiliar.Id = (int)Datos.Lector["Id"];
                    auxiliar.Descripcion = (string)Datos.Lector["Descripcion"];
                    listaEstilos.Add(auxiliar);
                }

                return listaEstilos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                Datos.CerrarConexion();
            }

        }
    }
}
