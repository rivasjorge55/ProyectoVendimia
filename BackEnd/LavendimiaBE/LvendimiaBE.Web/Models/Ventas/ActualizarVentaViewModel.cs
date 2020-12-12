using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LvendimiaBE.Web.Models.Ventas
{
    public class ActualizarVentaViewModel
    {
        [Required]
        public int idVenta { get; set; }
        [Required]
        public int idCliente { get; set; }
        [Required]
        public decimal total { get; set; }
        [Required]
        public DateTime fechaVenta { get; set; }
    }
}
