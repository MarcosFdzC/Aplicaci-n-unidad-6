using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using System.Linq.Expressions;

namespace negocio
{
    public class DiscoNegocio
    {
        //se utiliza una clase diferente para establecer el acceso a los datos de la DB.
        public List<Disco> Listar()
        {
            List<Disco> lista = new List<Disco>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;


            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=DISCOS_DB; integrated security=true";
                //poniendo ese . nos permite obtener la direccion con mas facilidad. De lo contrario tendriamos que escribir la dirección local completa, así:          (conexion.ConnectionString = "server=MARCOS\SQLEXPRESS") lo que se tendria que cambiar al pasar el código a otra computadora.
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select D.Id, D.Titulo, D.FechaLanzamiento, D.CantidadCanciones, D.UrlImagenTapa, E.Descripcion Estilo, TE.Descripcion Edicion, D.IdEstilo, D.IdTipoEdicion from DISCOS D, ESTILOS E, TIPOSEDICION TE where D.IdEstilo = E.Id and D.IdTipoEdicion = TE.Id";
                comando.Connection = conexion;//indica en que conexion se ejecutará el comando
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    //nos creamos un objeto auxiliar para ir compiando los datos y luego poder leerlos:
                    Disco aux = new Disco();
                    aux.Id = lector.GetInt32(0);//primera forma de obtener los datos de la tabla
                    aux.Titulo = lector.GetString(1);
                    aux.FechaLanzamiento = (DateTime)lector["FechaLanzamiento"];//segunda forma, antes especificamos el dato y luego ponemos el título de la columna.
                    aux.CantCanciones = (int)lector["CantidadCanciones"];

                    //validar lectura NULL
                    if (!(lector["UrlImagenTapa"] is DBNull))
                        aux.ImgUrl = (string)lector["UrlImagenTapa"];

                    aux.Estilo = new Estilo();
                    aux.Estilo.Id = (int)lector["IdEstilo"];
                    aux.Estilo.Descripcion = (string)lector["Estilo"];
                    aux.Edicion = new Edicion();
                    aux.Edicion.Id = (int)lector["IdtipoEdicion"];
                    aux.Edicion.Descripcion = (string)lector["Edicion"];
                    lista.Add(aux);
                }
                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void agregar(Disco nuevo)
        {
            AccesoDatos dato = new AccesoDatos();
            try
            {
                dato.SetearConsulta("Insert into DISCOS (Titulo, FechaLanzamiento, CantidadCanciones, IdEstilo, IdTipoEdicion, UrlImagenTapa) values ( '" + nuevo.Titulo + "', '" + nuevo.FechaLanzamiento.ToString("yyyy-MM-dd") + "', " + nuevo.CantCanciones + ", @IdEstilo, @IdTipoEdicion, @UrlImagen)");
                dato.SetearParametro("@UrlImagen", nuevo.ImgUrl);
                dato.SetearParametro("@IdEstilo", nuevo.Estilo.Id);
                dato.SetearParametro("@IdTipoEdicion", nuevo.Edicion.Id);
                dato.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dato.CerrarConexion();
            }
        }
        public void modificar(Disco modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update DISCOS set Titulo = @titulo, FechaLanzamiento = @fecha, CantidadCanciones = @canciones, UrlImagenTapa = @imagen, IdEstilo = @idEstilo, IdTipoEdicion = @idEdicion where id = @Id");
                datos.SetearParametro("@titulo", modificar.Titulo);
                datos.SetearParametro("@fecha", modificar.FechaLanzamiento);
                datos.SetearParametro("@canciones", modificar.CantCanciones);
                datos.SetearParametro("@imagen", modificar.ImgUrl);
                datos.SetearParametro("@idEstilo", modificar.Estilo.Id);
                datos.SetearParametro("@idEdicion", modificar.Edicion.Id);
                datos.SetearParametro("@id", modificar.Id);

                datos.EjecutarAccion();
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
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("delete from DISCOS where id = @id");
                datos.SetearParametro("@id", id);

                datos.EjecutarAccion();
            }
            catch (Exception ex) 
            { 
                throw ex; 
            }  
        }
    }
}
