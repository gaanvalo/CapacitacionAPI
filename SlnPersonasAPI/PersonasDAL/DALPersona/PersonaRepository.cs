using Newtonsoft.Json;
using PersonasDTO.DTOs;
using PersonasDTO.LogXNet;
using System;
using System.Collections.Generic;

namespace PersonasDAL.DALPersona
{
    public class PersonaRepository : SqlRepository<PersonaItem>, IPersonaRepository
    {
        private static bool _flag;
        Logger LOG = new Logger(_flag);
        public PersonaRepository(string connectionString, bool flag) : base(connectionString)
        {
            _flag = flag;
        }

        public override void DeleteAsync(int id, out object response)
        {
            throw new NotImplementedException();
        }

        public override List<PersonaItem> GetAllAsync()
        {
            var resultado = new List<PersonaItem>();

            try
            {
                
                var cmd = GetDbSprocCommand("PERSONAS_CONSULTAR");
                cmd.Parameters.Add(CreateParameter("@Id", 0));
                resultado = GetDTOListJSON<PersonaItem>(ref cmd);

            }
            catch (Exception ext)
            {
                throw ext;
            }

            return resultado;
        }

        public override PersonaItem GetAsync(int id)
        {
            var resultado = new PersonaItem();

            try
            {

                var cmd = GetDbSprocCommand("PERSONAS_CONSULTAR");
                cmd.Parameters.Add(CreateParameter("@Id", id));
                resultado = GetSingleDTO<PersonaItem>(ref cmd);

            }
            catch (Exception ext)
            {
                throw ext;
            }
            return resultado;
        }

        public override void InsertAsync(PersonaItem entity, out object response)
        {
            try
            {
               
                var cmd = GetDbSprocCommand("PERSONAS_INSERTAR");
                var Json = JsonConvert.SerializeObject(entity);
                cmd.Parameters.Add(CreateParameter("@Personas", Json));
                response = GetSingleDTO<Respuesta>(ref cmd);

            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        public override void UpdateAsync(int id, PersonaItem entityToUpdate, out object response)
        {
            throw new NotImplementedException();
        }
    }
}
