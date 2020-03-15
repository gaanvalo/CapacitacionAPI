using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PersonasDTO
{
    public abstract class DTOParser
    {
        /// <summary>
        /// metodos abstractos  para llenar los objetos y que se hereden por cada objeto
        /// </summary>
        /// <param name="reader">Lector de consultas de Sql </param>
        /// <returns>Retorna el objeto base que traer los campos inicializadores </returns>
        public abstract CommonBase PopulateDTO(SqlDataReader reader);


        /// <summary>
        /// metodos abstractos  para llenar un lista debido a una consulta Json
        /// </summary>
        /// <param name="reader">Lector de consultas de Sql </param>
        /// <returns>Retorna una lista </returns>
        public abstract List<T> PopulateDTOList<T>(SqlDataReader reader) where T : CommonBase;

        /// <summary>
        /// metodos abstractos para obtner la posicion de los campos segun el nombre del objeto, se hereda en cada objeto 
        /// </summary>
        /// <param name="reader">Lecto de consultas de oracle</param>
        public abstract void PopulateOrdinals(SqlDataReader reader);

       
    }
}
