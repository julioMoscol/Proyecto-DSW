using Proyecto.Models;
namespace Proyecto.Repositorio.Interface{
    public interface IProveedor{

        IEnumerable<Proveedor> GetProveedor();
        IEnumerable<Proveedor> GetProveedor(string empresa);

        string agregarProveedor(Proveedor reg);
        string actualizarProveedor(Proveedor reg);
        string eliminarProveedor(Proveedor reg);
        Proveedor GetProveedor(int idProveedor);
    }
}