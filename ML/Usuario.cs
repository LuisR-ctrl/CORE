using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int? IdUsuario { get; set; }
        [Required (ErrorMessage = "Username es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9]([._-](?![._-])|[a-zA-Z0-9]){3,18}[a-zA-Z0-9]$", ErrorMessage = "Caracteres no permitidos")]
        public string UserName { get; set; }
        [RegularExpression(@"[a-zA-ZÀ-ÖØ-öø-ÿ]+\.?(( |\-)[a-zA-ZÀ-ÖØ-öø-ÿ]+\.?)*", ErrorMessage = "Caracteres no permitidos")]
        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        [Required(ErrorMessage = "Apellido Paterno es requerido")]
        //[RegularExpression(@"([A-Za-zÑñ]+[áéíóú]?[A-Za-z]*){3,36}$/iu", ErrorMessage = "Caracteres no permitidos")]
        public string ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "Email es requerido")]
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Celular es requerido")]
        public string Celular { get; set; }
        public string FechaNacimiento { get; set; }
        public string Curp { get; set; }
        public string? Imagen { get; set; }
        public bool Estatus { get; set; }
        [Required(ErrorMessage = "Contraseña es requerido")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Rol es requerido")]
        public ML.Rol? Rol { get; set; }
        public List<object>? Usuarios { get; set; }
        public ML.Direccion? Direccion { get; set; }

    }
}
