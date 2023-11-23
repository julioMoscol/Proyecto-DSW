using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models{
    public class Proveedor {

        [Display (Name = "ID Proveedor")] public int idProveedor {get;set;}
        [Display(Name = "Telefono")] public string telefono {get;set;}
        [Display(Name = "Dirección")] public string direccion {get;set;}
        [Display(Name = "Empresa")] public string empresa {get;set;}
        [Display(Name = "RUC")] public string ruc {get;set;}
        [Display(Name = "Correo")] public string correo {get;set;}
        [Display(Name = "Representante")] public string representante {get;set;}

        public Proveedor(){
            idProveedor = 0;
            telefono = "";
            direccion = "";
            empresa = "";
            ruc = "";
            correo = "";
            representante = "";
        }   
    }
}