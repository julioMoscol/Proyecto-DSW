using Proyecto.Models;
namespace Proyecto.Repositorio.Interface{
    public interface IProducto{


        IEnumerable<Producto> GetProducto();
        IEnumerable<Producto> GetProducto(string nomproducto);
        IEnumerable<Producto> GetProductoo(string nomproducto);
        string agregarProducto(Producto reg);
        string actualizarProducto(Producto reg);
        string eliminarProducto(Producto reg);
       
        Producto GetProductoID(int? id = null);
        int autogenera();

        IEnumerable<Producto> GetProveedor(int idproveedor);
    }
}