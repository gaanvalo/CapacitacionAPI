using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PersonasDTO.DTOs
{
    public class Respuesta: CommonBase
    {
        /// <summary>
        /// Identificador de error de TM Corresponsal
        /// </summary>
        /// <value>Identificador de error de TM Corresponsal</value>
        [DataMember(Name = "statusCode")]
        public string statusCode { get; set; }

        /// <summary>
        /// Descripción del error
        /// </summary>
        /// <value>Descripción del error</value>
        [DataMember(Name = "statusDesc")]
        public string statusDesc { get; set; }

        [DataMember(Name = "Afectados")]
        public int Afectados { get; set; }
    }
}
