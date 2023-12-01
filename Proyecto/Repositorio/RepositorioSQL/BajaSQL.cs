using Microsoft.Data.SqlClient;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;

namespace Proyecto.Repositorio.RepositorioSQL
{
    public class BajaSQL : IBaja
    {
        private readonly string cadena;
        public BajaSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }

        public IEnumerable<BajaProducto> listado()
        {
            List<BajaProducto> temporal = new List<BajaProducto>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select*from baja_producto", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new BajaProducto()
                    {
                        codbajapro = dr.GetInt32(0),
                        idtrabajador = dr.GetInt32(1),
                        fechabaja = dr.GetDateTime(2),
                    });
                }
                dr.Close();
            }
            return temporal;
        }

        public IEnumerable<DetalleBaja> listadoDetalle()
        {
            List<DetalleBaja> temporal = new List<DetalleBaja>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select*from detalle_baja ", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new DetalleBaja()
                    {
                        idproducto = dr.GetInt32(0),
                        codbajapro = dr.GetInt32(1),
                        cantidad = dr.GetInt16(2),
                        idtipobaja = dr.GetInt32(3),
                    });
                }
                dr.Close();
            }
            return temporal;
        }

        public IEnumerable<DetalleBaja> listadoDetalle(int id)
        {
            return listadoDetalle().Where(x => x.codbajapro==id);
        }
    }
}
