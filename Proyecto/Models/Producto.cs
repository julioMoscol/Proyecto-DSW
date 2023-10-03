using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models{
    public class Producto{
        [Display(Name ="Id Producto")] public int idproducto { get; set; }
        [Display(Name ="Tipo de Producto")] public string tipoproducto { get; set; }
        [Display(Name ="Id Proveedor")]  public int idproveedor { get; set;}
        [Display(Name ="Nombre Producto")]  public string nomproducto { get; set; }
        [Display(Name ="Cantidad Producto")] public int cantproducto { get; set; }
        [Display(Name ="Precio Producto")] public double precproducto { get; set; }
        [Display(Name ="Stock Mínimo")] public int stockmin { get; set; }
        [Display(Name ="Stock Máximo")]public int stockmax { get; set;}
        [Display(Name ="Estado Producto")]public int estadoproducto { get; set; }
        [Display(Name ="Tipo Animal")] public string animal { get; set; }
        [Display(Name ="Precio Proveedor")] public double precproveedor { get; set; }
        
        public Producto(){
            idproducto = 0;
            tipoproducto = string.Empty;
            idproveedor = 0;
            nomproducto = string.Empty;
            cantproducto = 0; 
            precproducto = 0; 
            stockmin = 0; 
            stockmax = 0; 
            estadoproducto = 0; 
            animal = string.Empty;
            precproveedor = 0;
        }
    }
}