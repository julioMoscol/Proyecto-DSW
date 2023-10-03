using Proyecto.Models;
namespace Proyecto.Repositorio.Interface{
    public interface IProveedor{

        IEnumerable<proveedor> GetProveedor();
        IEnumerable<proveedor> GetProveedor(string empresa);

        string agregarProveedor(proveedor reg);
        string actualizarProveedor(proveedor reg);
        string eliminarProveedor(proveedor reg);
        proveedor GetProveedor(int idProveedor);
    }
}