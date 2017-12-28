using System;
using System.Collections.Generic;

namespace AuthenticationWebWcf.Common.DataContracts
{
    public class AuthenticatedDto
    {
        public string Guid { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaExpiracion { get; set; }

        public IList<string> Permisos { get; set; } 
    }
}
