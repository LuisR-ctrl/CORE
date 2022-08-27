using System;
using System.Collections.Generic;

namespace DL
{
    public partial class SubPoliza
    {
        public SubPoliza()
        {
            Polizas = new HashSet<Poliza>();
        }

        public byte IdSubPoliza { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Poliza> Polizas { get; set; }
    }
}
