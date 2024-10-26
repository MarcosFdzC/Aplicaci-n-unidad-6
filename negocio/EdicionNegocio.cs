using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class EdicionNegocio
    {
        
        public List<Edicion>Listar()
        { 
            List<Edicion> listaEdiciones = new List<Edicion>();
            AccesoDatos datos = new AccesoDatos();

            try 
            {
                datos.SetearConsulta("select Id, Descripcion from TIPOSEDICION");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Edicion EdicionAux = new Edicion();
                    EdicionAux.Id = (int)datos.Lector["Id"];
                    EdicionAux.Descripcion = (string)datos.Lector["Descripcion"];
                    listaEdiciones.Add(EdicionAux);
                }

                return listaEdiciones; 
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
