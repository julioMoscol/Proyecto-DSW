using Microsoft.Data.SqlClient;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;

namespace Proyecto.Repositorio.RepositorioSQL
{
    public class CategoriaSQL : ICategoria
    {
        readonly private string cadena;
        public CategoriaSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }
        public IEnumerable<Categoria> getCategoria()
        {
            List<Categoria> temporal = new List<Categoria>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_tipoproducto_list", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Categoria()
                    {
                        idcat = dr.GetInt32(0),
                        nomcat = dr.GetString(1)
                    });




                }


                dr.Close();



            }
            return temporal;
        }
    }
}
