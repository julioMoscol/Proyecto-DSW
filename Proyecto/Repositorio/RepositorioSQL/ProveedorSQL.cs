using Proyecto.Models;
using Proyecto.Repositorio.Interface;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Proyecto.Repositorio.RepositorioSQL{
    public class ProveedorSQL : IProveedor
    {
        private readonly string cadena;
        public ProveedorSQL(){
            
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }
        public IEnumerable<Proveedor> GetProveedor(){
            List<Proveedor> temporal = new List<Proveedor>();
            using(SqlConnection cn = new SqlConnection(cadena)){
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec listar_proveedores", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read()){
                    temporal.Add(new Proveedor(){
                        idProveedor = dr.GetInt32(0),
                        telefono = dr.GetString(1),
                        direccion = dr.GetString(2),
                        empresa = dr.GetString(3),
                        ruc = dr.GetString(4),
                        correo = dr.GetString(5),
                        representante = dr.GetString(6),
                    });
                }
                dr.Close();
            }
            return temporal;
        }
        public IEnumerable<Proveedor> filtroProveedor_Nombre(string empresa)
        {
            List<Proveedor> temporal = new List<Proveedor>();
            using(SqlConnection cn = new SqlConnection(cadena)){
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec listar_proveedores_empresa", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresa", empresa);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read()){
                    temporal.Add(new Proveedor(){
                        idProveedor = dr.GetInt32(0),
                        telefono = dr.GetString(1),
                        direccion = dr.GetString(2),
                        empresa = dr.GetString(3),
                        ruc = dr.GetString(4),
                        correo = dr.GetString(5),
                        representante = dr.GetString(6),
                    });
                }
                dr.Close();
            }
            return temporal;
        }
        public string agregarProveedor(Proveedor reg)
        {
            string mensaje = "";
            using(SqlConnection cn = new SqlConnection(cadena)){
                try{
                    SqlCommand cmd = new SqlCommand("agregar_proveedores", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProveedor", reg.idProveedor);
                    cmd.Parameters.AddWithValue("@Telefono", reg.telefono);
                    cmd.Parameters.AddWithValue("@Direccion", reg.direccion);
                    cmd.Parameters.AddWithValue("@Empresa", reg.empresa);
                    cmd.Parameters.AddWithValue("@RUC", reg.ruc);
                    cmd.Parameters.AddWithValue("@Correo", reg.correo);
                    cmd.Parameters.AddWithValue("@Representante", reg.representante);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha agregado {i} proveedor";
                } catch(Exception ex){
                    mensaje = ex.Message;
                } finally {
                    cn.Close();
                }
            }
            return mensaje;   
        }
        public string actualizarProveedor(Proveedor reg)
        {
            string mensaje = "";
            using(SqlConnection cn = new SqlConnection(cadena)){
                try{
                    SqlCommand cmd = new SqlCommand("actualizar_proveedores", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProveedor", reg.idProveedor);
                    cmd.Parameters.AddWithValue("@Telefono", reg.telefono);
                    cmd.Parameters.AddWithValue("@Direccion", reg.direccion);
                    cmd.Parameters.AddWithValue("@Empresa", reg.empresa);
                    cmd.Parameters.AddWithValue("@RUC", reg.ruc);
                    cmd.Parameters.AddWithValue("@Correo", reg.correo);
                    cmd.Parameters.AddWithValue("@Representante", reg.representante);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {i} proveedor";
                } catch(Exception ex){
                    mensaje = ex.Message;
                } finally {
                    cn.Close();
                }
            }
            return mensaje;
        }
        public string eliminarProveedor(Proveedor reg)
        {
            string mensaje = "";
            using(SqlConnection cn = new SqlConnection(cadena)){
                try{
                    SqlCommand cmd = new SqlCommand("eliminar_proveedores", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProveedor", reg.idProveedor);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} proveedor";
                } catch(Exception ex){
                    mensaje = ex.Message;
                } finally {
                    cn.Close();
                }
            }
            return mensaje;
        }
        public Proveedor Buscar(int ? id = null)
        {
            if (id == null)
                return new Proveedor();
            else
                return GetProveedor().FirstOrDefault(p => p.idProveedor == id.Value);
        }

        public int autogenera()
        {
            int cod = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_autogenera_idproveedor", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@idproveedor", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                cod = (int)cmd.Parameters["@idproveedor"].Value;

                cn.Close();
            }
            return cod;
        }
    }
}