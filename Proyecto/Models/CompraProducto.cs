namespace Proyecto.Models
{
    public class CompraProducto
    {
        public  string codingresopro{ get; set; }
        
        public int idproveedor { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime fecha { get; set; }
        public int idtrabajador { get; set; }
       

    }
}
