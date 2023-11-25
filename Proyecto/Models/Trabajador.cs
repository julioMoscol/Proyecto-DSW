using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models{
    public class Trabajador{
        [Required, Display(Name = "Código")] public int idtrabajador { get; set; }
        [Required, Display(Name = "Nombres")] public string nomtrabajador { get; set; }
        [Required, Display(Name = "Apellidos")] public string apetrabajador {get;set;}
        [Required, Display(Name = "DNI")] public string dnitrabajador { get; set; }
        [Required, Display(Name = "Teléfono")] public string teltrabajador { get; set; }
        [Required, Display(Name = "Email"), DataType(DataType.EmailAddress)] public string correo { get; set; }
        [Required, Display(Name = "Direccion")] public string direccion { get; set; }
        [Required, Display(Name = "Cargo")] public int cargo { get; set; }
        [Required, Display(Name = "Area")] public int area { get; set; }
        [Required, Display(Name = "Contraseña")] public string password { get; set; }

        [NotMapped]
        public bool manteneractivo { get; set; }

        public Trabajador() {
            idtrabajador = 0;
            nomtrabajador = string.Empty;
            apetrabajador = string.Empty;
            dnitrabajador= string.Empty;
            teltrabajador = string.Empty;
            correo = string.Empty;
            cargo = 0;
            area = 0;
            password = string.Empty;
            direccion = string.Empty;
        }
    }
}