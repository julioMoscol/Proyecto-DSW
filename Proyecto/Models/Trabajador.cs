using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models{
    public class Trabajador{ 
        public int idtrabajador { get; set; }
        public string nomtrabajador { get; set; }
        public string apetrabajador {get;set;}
        public string dnitrabajador { get; set; }
        public string teltrabajador { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string cargo { get; set; }
        public string area { get; set; }
        public string password { get; set; }

        [NotMapped]
        public bool manteneractivo { get; set; }

        public Trabajador() {
            idtrabajador = 0;
            nomtrabajador = string.Empty;
            apetrabajador = string.Empty;
            dnitrabajador= string.Empty;
            teltrabajador = string.Empty;
            correo = string.Empty;
            cargo = string.Empty;
            area = string.Empty;
            password = string.Empty;
            direccion = string.Empty;
        }
    }
}