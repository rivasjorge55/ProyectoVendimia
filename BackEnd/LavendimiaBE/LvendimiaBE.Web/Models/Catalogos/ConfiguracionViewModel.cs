using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LvendimiaBE.Web.Models.Catalogos
{
    public class ConfiguracionViewModel
    {
        public int idConfiguracion { get; set; }
        public decimal tazaFinanciamiento { get; set; }
        public decimal porcentajeEnganche { get; set; }
        public int plazoMaximo { get; set; }
    }
}
