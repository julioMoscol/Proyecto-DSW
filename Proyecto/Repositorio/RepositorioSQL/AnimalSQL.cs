using Microsoft.Data.SqlClient;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;

namespace Proyecto.Repositorio.RepositorioSQL
{
    public class AnimalSQL : IAnimal
    {
        private readonly string cadena;
        public AnimalSQL()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }
        public IEnumerable<Animal> getAnimal()
        {
            List<Animal> temporal= new List<Animal>();
            using(SqlConnection cn = new SqlConnection(cadena)) 
            { 
            cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_animal_list", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) 
                { 
                temporal.Add(new Animal() 
                  { 
                    idanimal=dr.GetInt32(0),
                    descanimal=dr.GetString(1)
                                           
                   });
                }
                dr.Close();
            }
            return temporal;
        }
    }
}
