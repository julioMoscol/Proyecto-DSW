using Microsoft.Data.SqlClient;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;

namespace Proyecto.Repositorio.RepositorioSQL
{
    public class CompraProductoSQL:ICompraProducto
    {
        private readonly string cadena;

        public CompraProductoSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetConnectionString("sql");
        }

        public IEnumerable<DetalleCompra> GetBoleta(int? id = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompraProducto> listado()
        {
            List<CompraProducto> temporal = new List<CompraProducto>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_boleta", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new CompraProducto()
                    {
                        codingresopro = dr.GetInt32(0),
                        fecha = dr.GetDateTime(1),
                        idproveedor= dr.GetInt32(2),
                        MontoTotal=dr.GetDecimal(3),
                        idtrabajador=dr.GetInt32(4),
                        

                    }); 
                }
                dr.Close();
            }
            return temporal;
        }

        public IEnumerable<DetalleCompra> listadoDetalle()
        {
            List<DetalleCompra> temporal = new List<DetalleCompra>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec  usp_boleta_deta", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new DetalleCompra()
                    {
                        codingresopro = dr.GetInt32(0),
                        idproducto = dr.GetInt32(1),
                        preciocompra = dr.GetInt32(2),
                        cantidad = dr.GetInt16(3),
                        monto = dr.GetDecimal(4)

                    });
                }
                dr.Close();
            }
            return temporal;
        }
    }
}
