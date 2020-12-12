using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LvendimiaBE.Web.Models.Ventas
{
    public class VentaViewModel
    {
        
        public int idVenta { get; set; }
       
        public int idCliente { get; set; }
       
        public decimal total { get; set; }
       
        public DateTime fechaVenta { get; set; }
    }
}
