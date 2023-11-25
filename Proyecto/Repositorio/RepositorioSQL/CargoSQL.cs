using Microsoft.Data.SqlClient;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;

namespace Proyecto.Repositorio.RepositorioSQL
{
    public class CargoSQL : ICargo
    {
        private readonly string cadena;

        public CargoSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetConnectionString("sql");
        }
        public IEnumerable<Cargo> listado()
        {
            List<Cargo> temporal = new List<Cargo>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select*from cargo", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Cargo()
                    {
                        idcargo = dr.GetInt32(0),
                        descripcion = dr.GetString(1),

                    });
                }
                dr.Close();
            }
            return temporal;
        }
    }
}
