using PersonasDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PersonasDAL
{
    public abstract class SqlRepository<TEntity> : IGenericRepository<TEntity>
     where TEntity : class
    {
        protected static string _connectionString;
        protected static EDbConnectionTypes _dbType;

        public SqlRepository(string connectionString)
        {
            _dbType = EDbConnectionTypes.Sql;
            _connectionString = connectionString;
        }

        protected static SqlConnection GetDbConnection()
        {
            return DbConnectionFactory.GetDbConnection(_dbType, _connectionString);
        }

        
        public abstract List<TEntity> GetAllAsync();
        public abstract TEntity GetAsync(int id);
        public abstract void InsertAsync(TEntity entity, out object response);
        public abstract void UpdateAsync(int id, TEntity entityToUpdate, out object response);
        public abstract void DeleteAsync(int id, out object response);




        // GetDbSQLCommand
        protected static SqlCommand GetDbSQLCommand(string sqlQuery)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = GetDbConnection();
            command.CommandType = CommandType.Text;
            command.CommandText = sqlQuery;
            return command;
        }



        #region getProcedures

        // GetDbSprocCommand
        protected static SqlCommand GetDbSprocCommand(string sprocName, SqlTransaction trans, SqlConnection con)
        {
            SqlCommand command = new SqlCommand(sprocName);
            command.Connection = con;
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = trans;
            return command;
        }

        // GetDbSprocCommand
        protected static SqlCommand GetDbSprocCommand(string sprocName)
        {
            SqlCommand command = new SqlCommand(sprocName);
            command.Connection = GetDbConnection();
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        #endregion

        #region CreateNullparameters
        protected static SqlParameter CreateNullParameter(string name, SqlDbType paramType)
        {
            SqlCommand parameter = new SqlCommand();
            var p = parameter.CreateParameter();
            p.SqlDbType = paramType;
            p.ParameterName = name;
            p.Value = null;
            p.Direction = ParameterDirection.Input;
            return p;
        }

        // CreateOutputParameter
        protected static SqlParameter CreateOutputParameter(string name, SqlDbType paramType)
        {
            SqlCommand parameter = new SqlCommand();
            var p = parameter.CreateParameter();
            p.SqlDbType = paramType;
            p.ParameterName = name;
            p.Direction = ParameterDirection.Output;
            return p;
        }
        // CreateOutputParameter


        #endregion


        protected static SqlParameter CreateINParameter(string name, SqlDbType paramType)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = paramType;
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        #region CreateParameter



        /// <summary>
        /// Create IN parameter
        /// </summary>
        /// <param name="name">Parameters name.</param>
        /// <param name="value">parameters value.</param>
        /// <returns>SqlParameter</returns>
        protected static SqlParameter CreateParameter(string name, int value)
        {
            SqlParameter parameter = new SqlParameter();
            if (value == CommonBase.Int_NullValue)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Int);
            }
            else
            {
                parameter = CreateINParameter(name, SqlDbType.Int);
                parameter.Value = value;
                return parameter;
            }
        }

        protected static SqlParameter CreateParameter(string name, decimal value)
        {
            SqlParameter parameter = new SqlParameter();
            if (value == CommonBase.Dec_NullValue)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Decimal);
            }
            else
            {
                parameter = CreateINParameter(name, SqlDbType.Decimal);
                parameter.Value = value;
                return parameter;
            }
        }

        /// <summary>
        /// Create IN parameter
        /// </summary>
        /// <param name="name">Parameters name.</param>
        /// <param name="value">parameters value type DateTime.</param>
        /// <returns>Sqlarameter</returns>
        protected static SqlParameter CreateParameter(string name, DateTime value)
        {
            SqlParameter parameter = new SqlParameter();
            if (value == CommonBase.DateTime_NullValue)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.DateTime);
            }
            else
            {
                parameter = CreateINParameter(name, SqlDbType.DateTime);
                parameter.Value = value;
                return parameter;
            }
        }

        /// <summary>
        /// Create IN parameter
        /// </summary>
        /// <param name="name">Parameters name.</param>
        /// <param name="value">parameters value.</param>
        /// <param name="size">parameters size.</param>
        /// <returns>SqlParameter</returns>
        protected static SqlParameter CreateParameter(string name, string value)
        {
            SqlParameter parameter = new SqlParameter();
            if (value == CommonBase.String_NullValue)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.VarChar);
            }
            else
            {
                parameter = CreateINParameter(name, SqlDbType.VarChar);
                parameter.Value = value;
                //parameter.Size = size;
                return parameter;
            }
        }
        #endregion CreateParameter

        #region Singleton
        // GetSingleDTO
        protected static T GetSingleDTO<T>(ref SqlCommand command) where T : CommonBase
        {
            T dto = null;
            try
            {
                SqlDataReader reader;
                DTOParser parser = DTOParserFactory.GetParser(typeof(T));
                command.CommandTimeout = 3000; //tamaño time out  para enviar  en el config
                command.Connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    parser.PopulateOrdinals(reader);
                    dto = (T)parser.PopulateDTO(reader);
                    reader.Close();
                }
            }
            catch (SqlException oEx)
            {
                throw oEx;
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }

            // return the DTO, it's either populated with data or null.
            return dto;
        }
        // GetDTOList
        protected static List<T> GetDTOList<T>(ref SqlCommand command) where T : CommonBase
        {
            List<T> dtoList = new List<T>();
            try
            {
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    // Get a parser for this DTO type and populate
                    // the ordinals.
                    DTOParser parser = DTOParserFactory.GetParser(typeof(T));
                    parser.PopulateOrdinals(reader);

                    // Use the parser to build our list of DTOs.
                    while (reader.Read())
                    {
                        T dto = null;
                        dto = (T)parser.PopulateDTO(reader);
                        dtoList.Add(dto);
                    }

                    reader.Close();
                }
            }
            catch (SqlException oEx)
            {
                throw oEx;
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            return dtoList;
        }

        protected static List<T> GetDTOListJSON<T>(ref SqlCommand command) where T : CommonBase
        {
            List<T> dtoList = new List<T>();
            try
            {
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    // Get a parser for this DTO type and populate
                    // the ordinals.
                    DTOParser parser = DTOParserFactory.GetParser(typeof(T));
                    parser.PopulateOrdinals(reader);

                    while (reader.Read())
                    {
                        dtoList = parser.PopulateDTOList<T>(reader);
                    }
                    reader.Close();
                }
            }
            catch (SqlException oEx)
            {
                throw oEx;
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            return dtoList;
        }


        #endregion

        protected static void ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception e)
            {
                if (command.Connection.State == ConnectionState.Open)
                    command.Connection.Close();
                throw e;
            }
        }




    }
}
