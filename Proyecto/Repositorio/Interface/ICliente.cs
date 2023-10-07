using Proyecto.Models;

namespace Proyecto.Repositorio.Interface
{
    public interface ICliente
    {
        IEnumerable<Cliente> GetCliente();
        IEnumerable<Cliente> GetClientes(string nom);
        Cliente buscarCliente(int id);
        string agregarProducto(Cliente reg);
        string eliminarProducto(Cliente id);
        string modificarProducto(Cliente reg);


    }

}
