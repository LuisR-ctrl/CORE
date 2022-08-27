using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int? IdDireccion { get; set; }
        [Required(ErrorMessage = "Calle es requerido")]
        public string Calle { get; set; }
        public string NumeroInterior { get; set; }
        [Required(ErrorMessage = "Numero exterior es requerido")]
        public string NumeroExterior { get; set; }
        public ML.Usuario? Usuario { get; set; }
        public ML.Colonia? Colonia { get; set; }
    }
}
