using Proyecto.Models;

namespace Proyecto.Repositorio.Interface
{
    public interface ICliente
    {
        IEnumerable<Cliente> GetCliente();
        IEnumerable<Cliente> GetClientes(string nom);
        Cliente buscarCliente(int id);
        string agregarCliente(Cliente reg);
        string eliminarCliente(Cliente id);
        string modificarCliente(Cliente reg);
    }

}
