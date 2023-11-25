using Microsoft.Data.SqlClient;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;

namespace Proyecto.Repositorio.RepositorioSQL
{
    public class AreaSQL : IArea
    {
        private readonly string cadena;

        public AreaSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetConnectionString("sql");
        }
        public IEnumerable<Area> listado()
        {
            List<Area> temporal = new List<Area>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select*from area", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Area()
                    {
                        idarea = dr.GetInt32(0),
                        descripcion = dr.GetString(1),

                    });
                }
                dr.Close();
            }
            return temporal;
        }
    }
}
