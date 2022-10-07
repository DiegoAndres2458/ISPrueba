using System;
using System.Collections.Generic;

namespace ISPrueba.Models
{
    public partial class Correo
    {
        public int Id { get; set; }
        public string Correo1 { get; set; } = null!;
        public int IdPersona { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; } = null!;
    }
}
