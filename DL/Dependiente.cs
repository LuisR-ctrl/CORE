using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Dependiente
    {
        public int IdDependiente { get; set; }
        public string? IdEmpleado { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? EstadoCivil { get; set; }
        public string Genero { get; set; } = null!;
        public string? Telefono { get; set; }
        public string Rfc { get; set; } = null!;
        public int? IdDependienteTipo { get; set; }

        //Alias

        public string EmpleadoNombre { get; set; }
        public string EmpleadoApellidoPaterno { get; set; }
        public string EmpleadoApellidoMaterno { get; set; }
        public string DependienteTipoNombre { get; set; }

        public virtual Empleado? IdEmpleadoNavigation { get; set; }
    }
}
