using System;
using System.Collections.Generic;

namespace DL
{
    public partial class EmpleadoNew
    {
        public string NumeroEmpleado { get; set; } = null!;
        public string Rfc { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Nss { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? Foto { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdPoliza { get; set; }
    }
}
