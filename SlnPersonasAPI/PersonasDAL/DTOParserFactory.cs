using PersonasDTO;
using PersonasDTO.DTOParsers;
using System;


namespace PersonasDAL
{
    internal static class DTOParserFactory
    {
        internal static DTOParser GetParser(System.Type DTOType)
        {
            switch (DTOType.Name)
            {
                /*************************************************
                 *         PONER EN ORDEN ALFABETICA             *
                 ************************************************/
                case "PersonaItem":
                    return new DTOParserPersona();
                case "Respuesta":
                    return new DTOParserRespuesta();

            }
            throw new Exception("Tipo del DTO desconocido."); //GENERA LA EXCEPCION. 
        }
    }
}
