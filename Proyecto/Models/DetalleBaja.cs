using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class DetalleBaja
    {
        public int codbajapro { get; set; }
        [Display(Name = "Código")]public int idproducto { get; set; }
        [Display(Name = "Cantidad")] public int cantidad { get; set; }
        [Display(Name = "ID Tipo de Baja")] public int idtipobaja { get; set; }
    }   
}
