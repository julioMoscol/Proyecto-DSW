using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class BajaProducto
    {
        [Display(Name = "Código")] public int codbajapro { get; set; }
        [Display(Name = "ID Trabajador")] public int idtrabajador { get; set; }
        [Display(Name = "Fecha")] public DateTime fechabaja { get; set; }
    }
}
