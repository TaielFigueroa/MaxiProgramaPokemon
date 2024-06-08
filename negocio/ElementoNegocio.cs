using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ElementoNegocio
    {
        public List<Elemento> listar()
        {
			List<Elemento> elementos = new List<Elemento>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
			{
                accesoDatos.SetearConsulta("Select Id, Descripcion from ELEMENTOS");
                accesoDatos.ejecutarLectura();
                while(accesoDatos.Lector.Read())
                {
                    Elemento aux = new Elemento();
                    aux.Id = (int) accesoDatos.Lector["Id"];
                    aux.Descripcion = (string) accesoDatos.Lector["Descripcion"];
                    elementos.Add(aux);
                }
                return elementos;
			}
			catch (Exception ex)
			{

				throw ex;
			}
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }
    }
}
