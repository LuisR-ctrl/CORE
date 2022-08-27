using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Empleado
    {
        public Empleado()
        {
            Dependientes = new HashSet<Dependiente>();
        }

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

        //Alias

        public string? EmpresaNombre { get; set; }
        public string? PolizaNombre { get; set; }

        public virtual Empresa? IdEmpresaNavigation { get; set; }
        public virtual Poliza? IdPolizaNavigation { get; set; }
        public virtual ICollection<Dependiente> Dependientes { get; set; }
    }
}
