namespace Proyecto.Models{
    public class Proveedor{

        public int idProveedor {get;set;}
        public string telefono {get;set;}
        public string direccion {get;set;}
        public string empresa {get;set;}
        public string ruc {get;set;}
        public string correo {get;set;}
        public string representante {get;set;}

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