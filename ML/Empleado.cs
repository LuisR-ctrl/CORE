using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        [Display(Name = "NumeroEmpleado"), Required(ErrorMessage = "Num. Empleado es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Formato no válido.")]
        public string NumeroEmpleado { get; set; }
        //[RegularExpression(@"^(((?!(([CcKk][Aa][CcKkGg][AaOo])|([Bb][Uu][Ee][YyIi])|([Kk][Oo](([Gg][Ee])|([Jj][Oo])))|([Cc][Oo](([Gg][Ee])|([Jj][AaEeIiOo])))|([QqCcKk][Uu][Ll][Oo])|((([Ff][Ee])|([Jj][Oo])|([Pp][Uu]))[Tt][Oo])|([Rr][Uu][Ii][Nn])|([Gg][Uu][Ee][Yy])|((([Pp][Uu])|([Rr][Aa]))[Tt][Aa])|([Pp][Ee](([Dd][Oo])|([Dd][Aa])|([Nn][Ee])))|([Mm](([Aa][Mm][OoEe])|([Ee][Aa][SsRr])|([Ii][Oo][Nn])|([Uu][Ll][Aa])|([Ee][Oo][Nn])|([Oo][Cc][Oo])))))[A-Za-zñÑ&][aeiouAEIOUxX]?[A-Za-zñÑ&]{2}(((([02468][048])|([13579][26]))0229)|(\d{2})((02((0[1-9])|1\d|2[0-8]))|((((0[13456789])|1[012]))((0[1-9])|((1|2)\d)|30))|(((0[13578])|(1[02]))31)))[a-zA-Z1-9]{2}[\dAa])|([Xx][AaEe][Xx]{2}010101000))$", ErrorMessage = "Formato no válido de RFC.")]
        public string RFC { get; set; }
        [Display(Name = "Nombre"), Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { get; set; }
        [Display(Name = "ApellidPaterno"), Required(ErrorMessage = "Apellido Paterno es requerido")]
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        [Display(Name = "Email"), Required(ErrorMessage = "Email es requerido")]
        public string Email { get; set; }
        [Display(Name = "Telefono"), Required(ErrorMessage = "Teléfono es requerido")]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$", ErrorMessage = "Formato no válido de Telefono.")]
        public string Telefono { get; set; }
        public string FechaNacimiento { get; set; }
        //[RegularExpression (@"\d{11}", ErrorMessage = "Caracteres no permitidos")]
        public string NSS { get; set; }
        public string FechaIngreso { get; set; }
        public string? Foto { get; set; }
        [Display(Name = "Empresa.IdEmpresa"), Required(ErrorMessage = "Empresa es requerido")]
        public ML.Empresa Empresa { get; set; }
        [Display(Name = "Poliza.IdPoliza"),Required(ErrorMessage = "Poliza es requerido")]
        public ML.Poliza Poliza { get; set; }
        public List<object> Empleados { get; set; }
        //update-add
        public string Action { get; set; }

    }
}
