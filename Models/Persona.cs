using System;
using System.Collections.Generic;

namespace ISPrueba.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Correos = new HashSet<Correo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Correo> Correos { get; set; }
    }
}
