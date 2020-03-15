using Newtonsoft.Json;
using PersonasDTO.DTOs;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonasDTO.DTOParsers
{
    public class DTOParserPersona : DTOParser
    {

        private int Ord_Personas; 
        public override CommonBase PopulateDTO(SqlDataReader reader)
        {
            var resp = new PersonaItem();
            var jsonCal = string.Empty;

            if (!reader.IsDBNull(Ord_Personas))
            {
                jsonCal = reader.GetString(Ord_Personas);
                resp = JsonConvert.DeserializeObject<PersonaItem[]>(jsonCal)[0];
            }

            return resp;
        }

       

        public override List<PersonaItem> PopulateDTOList<PersonaItem>(SqlDataReader reader)
        {
            var res = new List<PersonaItem>();
            var jsonCal = string.Empty;

            if (!reader.IsDBNull(Ord_Personas))
            {
                jsonCal = reader.GetString(Ord_Personas);
                return JsonConvert.DeserializeObject<List<PersonaItem>>(jsonCal);
            }

            return res;
        }

        public override void PopulateOrdinals(SqlDataReader reader)
        {
            Ord_Personas = reader.GetOrdinal("Respuesta");
        }
    }
}
