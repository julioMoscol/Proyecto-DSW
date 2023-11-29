using Microsoft.Data.SqlClient;
using NuGet.Protocol;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;
using System.Data;

namespace Proyecto.Repositorio.RepositorioSQL
{
    public class ClienteSQL : ICliente

    {
        string cadena;
        public ClienteSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }
        public string agregarCliente(Cliente reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("agregar_clientes", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCliente", reg.idcliente);
                    cmd.Parameters.AddWithValue("@Nombres", reg.nomcliente);
                    cmd.Parameters.AddWithValue("@Apellidos", reg.apellcliente);
                    cmd.Parameters.AddWithValue("@Telefono", reg.telcliente);
                    cmd.Parameters.AddWithValue("@Direccion", reg.direccion);
                    cmd.Parameters.AddWithValue("@Correo", reg.correo);
                    cmd.Parameters.AddWithValue("@DNI", reg.dni);
                    cmd.Parameters.AddWithValue("@password", reg.password);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha agregado {i} cliente";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }

        public int autogenera()
        {

            int cod = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_autogenera_idcliente", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@idcliente", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                cod = (int)cmd.Parameters["@idcliente"].Value;

                cn.Close();
            }
            return cod;
        }

        public Cliente buscarCliente(int id)
        {

            if (string.IsNullOrEmpty(id.ToString()))
                return new Cliente();
            else
                return GetCliente().FirstOrDefault(x => x.idcliente == id);
        }

        public string eliminarCliente(Cliente id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("eliminar_clientes", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCliente", id.idcliente);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} cliente";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }

        public IEnumerable<Cliente> GetCliente()
        {
            List<Cliente> temporal = new List<Cliente>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_listar_cliente", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Cliente()
                    {
                        idcliente = dr.GetInt32(0),
                        nomcliente = dr.GetString(1),
                        apellcliente = dr.GetString(2),
                        telcliente = dr.GetString(3),
                        direccion = dr.GetString(4),
                        correo = dr.GetString(5),
                        dni = dr.GetString(6),
                        password = dr.GetString(7),

                    });
                }
                dr.Close();
            }
            return temporal;
        }

        public IEnumerable<Cliente> GetClientes(string nom)
        {
            return GetCliente().Where(x => x.nomcliente.StartsWith(nom, StringComparison.CurrentCultureIgnoreCase));
        }

        public string modificarCliente(Cliente reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("actualizar_clientes", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCliente", reg.idcliente);
                    cmd.Parameters.AddWithValue("@Nombres", reg.nomcliente);
                    cmd.Parameters.AddWithValue("@Apellidos", reg.apellcliente);
                    cmd.Parameters.AddWithValue("@Telefono", reg.telcliente);
                    cmd.Parameters.AddWithValue("@Direccion", reg.direccion);
                    cmd.Parameters.AddWithValue("@Correo", reg.correo);
                    cmd.Parameters.AddWithValue("@DNI", reg.dni);
                    cmd.Parameters.AddWithValue("@password", reg.password);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha modificado {i} cliente";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }
    }
}
