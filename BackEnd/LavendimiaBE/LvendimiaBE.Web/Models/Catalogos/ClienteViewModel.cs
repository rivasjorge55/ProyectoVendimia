using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LvendimiaBE.Web.Models.Catalogos
{
    public class ClienteViewModel
    {
        public int idCliente { get; set; } 
        //[Required]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = "“No es posible continuar, debe ingresar el nombre, es obligatorio")]
        public string nombre { get; set; }
        //[Required]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = "“No es posible continuar, debe ingresar el primer apellido, es obligatorio")]
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        //[Required]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = "“No es posible continuar, debe ingresar el RFC, es obligatorio")]
        public string nombreCompleto { get; set; }
        public string rfc { get; set; }
    }
}
