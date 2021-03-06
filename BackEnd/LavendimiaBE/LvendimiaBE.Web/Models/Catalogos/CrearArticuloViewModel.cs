﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LvendimiaBE.Web.Models.Catalogos
{
    public class CrearArticuloViewModel
    {
        
        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "“No es posible continuar, debe ingresar la deescripción, es obligatorio")]
        public string descripcion { get; set; }
        public string modelo { get; set; }
        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        public decimal precio { get; set; }
        [Required]
        [Range(0, 99999999)]
        public int existencia { get; set; }
        
    }
}
