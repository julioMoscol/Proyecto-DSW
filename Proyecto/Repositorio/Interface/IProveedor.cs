using Proyecto.Models;
namespace Proyecto.Repositorio.Interface{
    public interface IProveedor{

        IEnumerable<Proveedor> GetProveedor();
        IEnumerable<Proveedor> filtroProveedor_Nombre(string empresa);

        string agregarProveedor(Proveedor reg);
        string actualizarProveedor(Proveedor reg);
        string eliminarProveedor(Proveedor reg);
        int autogenera();
        Proveedor Buscar(int ? id = null);
    }
}