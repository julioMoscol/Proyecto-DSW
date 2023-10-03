namespace Proyecto.Models{
    public class Cliente{
        public int idcliente { get; set; }
        public string nomcliente { get; set; }
        public string apellcliente { get; set;}
        public string telcliente { get; set;}
        public string direccion { get; set; }
        public string correo { get; set; }
        public string dni { get; set; }
        public string password { get; set; }
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