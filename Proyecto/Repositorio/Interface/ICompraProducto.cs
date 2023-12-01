using Proyecto.Models;

namespace Proyecto.Repositorio.Interface
{
    public interface ICompraProducto
    {
        IEnumerable<DetalleCompra> listadoDetalle();
        IEnumerable<DetalleCompra> GetBoleta(string id);
        IEnumerable<CompraProducto> listado();
    }
}
