using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LavendimiaBE.Entidades.Catalogos
{
    public class Configuracion
    {
        [Required]
        public int idConfiguracion  { get; set; }
         
        public decimal tazaFinanciamiento { get; set; }
        public decimal porcentajeEnganche { get; set; }
        public int plazoMaximo { get; set; }
    }
}
