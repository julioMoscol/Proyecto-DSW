namespace Proyecto.Models
{
    public class DetalleCompra
    {
        public int codingresopro { get; set; }
        public int idproducto {get; set; }

        public int  preciocompra {set;get;}
        public int cantidad { set; get; }

        public decimal monto { get; set; }
    }
}
