using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LavendimiaBE.Entidades.Catalogos
{
    public class Articulo
    {
        public int idArticulo { get; set; }
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
        public bool activo { get; set; }
    }
}
