using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models{
    public class Cliente{

        [Required, Display(Name = "C�digo")] public int idcliente { get; set; }
        [Required, Display(Name = "Nombres"), RegularExpression("^[A-Za-z������������\\s]{3,30}$")] public string nomcliente { get; set; }
        [Required, Display(Name = "Apellidos"), RegularExpression("^[A-Za-z������������\\s]{3,30}$")] public string apellcliente { get; set; }
        [Required, Display(Name = "Tel�fono"), RegularExpression("^[9][0-9]{8}$")] public string telcliente { get; set; }
        [Required, Display(Name = "Direcci�n")] public string direccion { get; set; }
        [Required, Display(Name = "Email"), DataType(DataType.EmailAddress)] public string correo { get; set; }
        [Required, Display(Name = "DNI"), RegularExpression("^[0-9]{8}$")] public string dni { get; set; }
        [Required, Display(Name = "Contrase�a")] public string password { get; set; }

        public Cliente()
        {
            idcliente = 0;
            nomcliente = string.Empty;
            apellcliente = string.Empty;
            telcliente = string.Empty;
            direccion = string.Empty;
            correo = string.Empty;
            dni = string.Empty;
            password = string.Empty;
        }

    }
}