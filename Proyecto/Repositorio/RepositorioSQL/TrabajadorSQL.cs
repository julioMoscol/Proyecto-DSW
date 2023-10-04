using Proyecto.Repositorio.Interface;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using Proyecto.Models;

namespace Proyecto.Repositorio.RepositorioSQL{
    public class TrabajadorSQL : ITrabajador
    {
        private readonly string cadena;
        public TrabajadorSQL(){
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }
        public IEnumerable<Trabajador> GetTrabajador()
        {
            List<Trabajador> temporal = new List<Trabajador>();
            using(SqlConnection cn = new SqlConnection(cadena)){
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec listar_trabajador", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read()){
                    temporal.Add(new Trabajador(){
                        idtrabajador = dr.GetInt32(0),
                        nomtrabajador = dr.GetString(1),
                        dnitrabajador = dr.GetString(2),
                        teltrabajador = dr.GetString(3),
                        correo = dr.GetString(4),
                        direccion = dr.GetString(5),
                        cargo = dr.GetString(6),
                        area = dr.GetString(7),
                        password = dr.GetString(8),
                    });
                }
                dr.Close();
            }
            return temporal;
        }

        public IEnumerable<Trabajador> GetTrabajador(string nomtrabajador)
        {
            List<Trabajador> temporal = new List<Trabajador>();
            using(SqlConnection cn = new SqlConnection(cadena)){
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec listar_trabajador_nombre", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nom", nomtrabajador);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read()){
                    temporal.Add(new Trabajador(){
                        idtrabajador = dr.GetInt32(0),
                        nomtrabajador = dr.GetString(1),
                        dnitrabajador = dr.GetString(2),
                        teltrabajador = dr.GetString(3),
                        correo = dr.GetString(4),
                        direccion = dr.GetString(5),
                        cargo = dr.GetString(6),
                        area = dr.GetString(7),
                        password = dr.GetString(8),
                    });
                }
                dr.Close();
            }
            return temporal;
        }
        public string agregarTrabajador(Trabajador reg)
        {
            string mensaje = "";
            using(SqlConnection cn = new SqlConnection(cadena)){
                try{
                    SqlCommand cmd = new SqlCommand("agregar_trabajador", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdTrabajador", reg.idtrabajador);
                    cmd.Parameters.AddWithValue("@Nombres", reg.nomtrabajador);
                    cmd.Parameters.AddWithValue("@Apellidos", reg.apetrabajador);
                    cmd.Parameters.AddWithValue("@DNI", reg.dnitrabajador);
                    cmd.Parameters.AddWithValue("@Telefono", reg.teltrabajador);
                    cmd.Parameters.AddWithValue("@Correo", reg.correo);
                    cmd.Parameters.AddWithValue("@Direccion", reg.direccion);
                    cmd.Parameters.AddWithValue("@IdCargo", reg.cargo);
                    cmd.Parameters.AddWithValue("@IdTipoArea", reg.area);
                    cmd.Parameters.AddWithValue("@Password", reg.password);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha agregado {i} trabajador";
                } catch(Exception ex){
                    mensaje = ex.Message;
                } finally {
                    cn.Close();
                }
            }
            return mensaje;
        }
        public string actualizarTrabajador(Trabajador reg)
        {
            string mensaje = "";
            using(SqlConnection cn = new SqlConnection(cadena)){
                try{
                    SqlCommand cmd = new SqlCommand("actualizar_trabajador", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdTrabajador", reg.idtrabajador);
                    cmd.Parameters.AddWithValue("@Nombres", reg.nomtrabajador);
                    cmd.Parameters.AddWithValue("@Apellidos", reg.apetrabajador);
                    cmd.Parameters.AddWithValue("@DNI", reg.dnitrabajador);
                    cmd.Parameters.AddWithValue("@Telefono", reg.teltrabajador);
                    cmd.Parameters.AddWithValue("@Correo", reg.correo);
                    cmd.Parameters.AddWithValue("@Direccion", reg.direccion);
                    cmd.Parameters.AddWithValue("@IdCargo", reg.cargo);
                    cmd.Parameters.AddWithValue("@IdTipoArea", reg.area);
                    cmd.Parameters.AddWithValue("@Password", reg.password);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha agregado {i} trabajador";
                } catch(Exception ex){
                    mensaje = ex.Message;
                } finally {
                    cn.Close();
                }
            }
            return mensaje;
        }
        public string eliminarTrabajador(Trabajador reg)
        {
            string mensaje = "";
            using(SqlConnection cn = new SqlConnection(cadena)){
                try{
                    SqlCommand cmd = new SqlCommand("eliminar_trabajador", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdTrabajador", reg.idtrabajador);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} trabajador";
                } catch(Exception ex){
                    mensaje = ex.Message;
                } finally {
                    cn.Close();
                }
            }
            return mensaje;
        }
        public Trabajador GetTrabajador(int idtrabajador)
        {
            throw new NotImplementedException();
        }
    }
}