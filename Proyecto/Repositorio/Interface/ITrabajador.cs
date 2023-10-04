using Proyecto.Models;
namespace Proyecto.Repositorio.Interface{
    public interface ITrabajador{
        IEnumerable<Trabajador> GetTrabajador();
        IEnumerable<Trabajador> GetTrabajador(string nomtrabajador);
        string agregarTrabajador(Trabajador reg);
        string actualizarTrabajador(Trabajador reg);
        string eliminarTrabajador(Trabajador reg);
        Trabajador GetTrabajador(int idtrabajador);
    }
}