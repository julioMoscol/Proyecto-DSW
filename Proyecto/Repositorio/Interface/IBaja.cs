using Proyecto.Models;

namespace Proyecto.Repositorio.Interface
{
    public interface IBaja
    {
        IEnumerable<BajaProducto> listado();
        IEnumerable<DetalleBaja> listadoDetalle();
        IEnumerable<DetalleBaja> listadoDetalle(int id);
    }
}
