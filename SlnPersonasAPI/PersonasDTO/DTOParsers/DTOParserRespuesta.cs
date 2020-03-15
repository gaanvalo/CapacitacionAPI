using Newtonsoft.Json;
using PersonasDTO.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PersonasDTO.DTOParsers
{
    public class DTOParserRespuesta : DTOParser
    {
        private int ord_respuesta;
        public override CommonBase PopulateDTO(SqlDataReader reader)
        {
            var resp = new Respuesta();
            var jsonCal = string.Empty;

            if (!reader.IsDBNull(ord_respuesta))
            {
                jsonCal = reader.GetString(ord_respuesta);
                resp = JsonConvert.DeserializeObject<Respuesta[]>(jsonCal)[0];
            }

            return resp;
        }

        public override List<T> PopulateDTOList<T>(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public override void PopulateOrdinals(SqlDataReader reader)
        {
            ord_respuesta = reader.GetOrdinal("Respuesta");
        }
    }
}
