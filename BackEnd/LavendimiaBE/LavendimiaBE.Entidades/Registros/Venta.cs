using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LavendimiaBE.Entidades.Registros
{
    public class Venta
    {
        [Required]
        public int idVenta { get; set; }
        [Required]
        public int idCliente { get; set; }
        [Required]  
        public decimal total {get;set;}
        [Required]
        public DateTime fechaVenta {get;set;}
    
    }

    public class vw_ventas
    {
        public int idVenta { get; set; }
        public int idCliente { get; set; }
        public string nombreCompleto { get; set; }

        public decimal total { get; set; }
        //        public DateTime fechaVenta { get; set; }
        public string  fechaVenta { get; set; }

    }

}
