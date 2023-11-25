using Proyecto.Models;
using Proyecto.Repositorio.Interface;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Proyecto.Repositorio.RepositorioSQL{
    public class ProductoSQl : IProducto
    {
        private readonly string cadena;
        public ProductoSQl(){
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }
        public IEnumerable<Producto> GetProducto()
        {
            List<Producto> temporal = new List<Producto>();
            using(SqlConnection cn = new SqlConnection(cadena)){
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec lista_productos", cn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read()){
                    temporal.Add(new Producto(){
                        idproducto = dr.GetInt32(0),
                        tipoproducto = dr.GetString(1),
                        idproveedor = dr.GetInt32(2),
                        nomproducto = dr.GetString(3),
                        cantproducto = dr.GetInt16(4),
                        precproducto = dr.GetDecimal(5),
                        stockmin = dr.GetInt16(6),
                        stockmax = dr.GetInt16(7),
                        estadoproducto = dr.GetByte(8),
                        animal = dr.GetString(9),
                        precproveedor = dr.GetDecimal(10),
                    });
                }
                dr.Close();
            }
            return temporal;
        }

        public IEnumerable<Producto> GetProducto(string nomproducto)
        {
            List<Producto> temporal = new List<Producto>();
            using(SqlConnection cn = new SqlConnection(cadena)){
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec buscar_productos_nombre", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nom", nomproducto);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read()){
                    temporal.Add(new Producto(){
                        idproducto = dr.GetInt32(0),
                        tipoproducto = dr.GetString(1),
                        idproveedor = dr.GetInt32(2),
                        nomproducto = dr.GetString(3),
                        cantproducto = dr.GetInt32(4),
                        precproducto = dr.GetDecimal(5),
                        stockmin = dr.GetInt32(6),
                        stockmax = dr.GetInt32(7),
                        estadoproducto = dr.GetByte(8),
                        animal = dr.GetString(9),
                        precproveedor = dr.GetDecimal(10),
                    });
                }
                dr.Close();
            }
            return temporal;
        }

        public string agregarProducto(Producto reg)
        {
            string mensaje = "";
            using(SqlConnection cn = new SqlConnection(cadena)){
                try{
                    SqlCommand cmd = new SqlCommand("agregar_productos", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", reg.idproducto);
                    cmd.Parameters.AddWithValue("@IdTipoPro", reg.tipoproducto);
                    cmd.Parameters.AddWithValue("@IdProveedor", reg.idproveedor);
                    cmd.Parameters.AddWithValue("@Nombre", reg.nomproducto);
                    cmd.Parameters.AddWithValue("@Cantidad", reg.cantproducto);
                    cmd.Parameters.AddWithValue("@Preciopublico", reg.precproducto);
                    cmd.Parameters.AddWithValue("@StockMinimo", reg.stockmin);
                    cmd.Parameters.AddWithValue("@StockMaximo", reg.stockmax);
                    cmd.Parameters.AddWithValue("@estado", reg.estadoproducto);
                    cmd.Parameters.AddWithValue("@IdAnimal", reg.animal);
                    cmd.Parameters.AddWithValue("@PrecioProveedor", reg.precproveedor);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha agregado {i} producto";
                } catch(Exception ex){
                    mensaje = ex.Message;
                } finally {
                    cn.Close();
                }
            }
            return mensaje;
        }

        public string actualizarProducto(Producto reg)
        {
            string mensaje = "";
            using(SqlConnection cn = new SqlConnection(cadena)){
                try{
                    SqlCommand cmd = new SqlCommand("actualizar_productos", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", reg.idproducto);
                    cmd.Parameters.AddWithValue("@IdTipoPro", reg.tipoproducto);
                    cmd.Parameters.AddWithValue("@IdProveedor", reg.idproveedor);
                    cmd.Parameters.AddWithValue("@Nombre", reg.nomproducto);
                    cmd.Parameters.AddWithValue("@Cantidad", reg.cantproducto);
                    cmd.Parameters.AddWithValue("@Preciopublico", reg.precproducto);
                    cmd.Parameters.AddWithValue("@StockMinimo", reg.stockmin);
                    cmd.Parameters.AddWithValue("@StockMaximo", reg.stockmax);
                    cmd.Parameters.AddWithValue("@estado", reg.estadoproducto);
                    cmd.Parameters.AddWithValue("@IdAnimal", reg.animal);
                    cmd.Parameters.AddWithValue("@PrecioProveedor", reg.precproveedor);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {i} producto";
                } catch(Exception ex){
                    mensaje = ex.Message;
                } finally {
                    cn.Close();
                }
            }
            return mensaje;
        }

        public string eliminarProducto(Producto reg)
        {
            string mensaje = "";
            using(SqlConnection cn = new SqlConnection(cadena)){
                try{
                    SqlCommand cmd = new SqlCommand("eliminar_productos", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", reg.idproducto);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} producto";
                } catch(Exception ex){
                    mensaje = ex.Message;
                } finally {
                    cn.Close();
                }
            }
            return mensaje;
        }
       

        public Producto GetProductoID(int? id = null)
        {

            if (id == null)
                return null;
            else
                return GetProducto().FirstOrDefault(p => p.idproducto == id.Value);
        }
    }
}