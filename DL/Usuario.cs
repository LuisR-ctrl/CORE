using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Usuario
    {
        public Usuario()
        {
            Aseguradoras = new HashSet<Aseguradora>();
            Direccions = new HashSet<Direccion>();
            Polizas = new HashSet<Poliza>();
        }

        public int IdUsuario { get; set; }
        public string UserName { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string Email { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? Celular { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Curp { get; set; }
        public string? Imagen { get; set; }
        public int? IdRol { get; set; }
        public bool? Estatus { get; set; }
        public string Password { get; set; } = null!;

        //Alias
        public string RolNombre { get; set; }
        public int? IdDireccion { get; set; }
        public string DireccionCalle { get; set; }
        public string DireccionNumeroInterior { get; set; }
        public string DireccionNumeroExterior { get; set; }
        public int? IdColonia { get; set; }
        public string NombreColonia { get; set; }
        public string CodigoPostal { get; set; }
        public int? IdMunicipio { get; set; }
        public string NombreMunicipio { get; set; }
        public int? IdEstado { get; set; }
        public string NombreEstado { get; set; }
        public int? IdPais { get; set; }
        public string NombrePais { get; set; }

        public virtual Rol? RolNavigation { get; set; }
        public virtual ICollection<Aseguradora> Aseguradoras { get; set; }
        public virtual ICollection<Direccion> Direccions { get; set; }
        public virtual ICollection<Poliza> Polizas { get; set; }
    }
}
