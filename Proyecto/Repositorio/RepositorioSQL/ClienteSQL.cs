using NuGet.Protocol;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;

namespace Proyecto.Repositorio.RepositorioSQL
{
    public class ClienteSQL : ICliente

    {
        string caneda;
        public ClienteSQL()
        {
            caneda = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("sql");
        }
        public string agregar(Cliente reg)
        {
            throw new NotImplementedException();
        }

        public Cliente buscarCliente(int id)
        {
            throw new NotImplementedException();
        }

        public string eliminar(Cliente id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> getCliente()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> getClientes(string nom)
        {
            throw new NotImplementedException();
        }

        public string modificar(Cliente reg)
        {
            throw new NotImplementedException();
        }
    }
}
