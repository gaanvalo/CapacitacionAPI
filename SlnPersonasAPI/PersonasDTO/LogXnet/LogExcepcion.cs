

using PersonasDTO.DTOs;
using System;

namespace PersonasDTO.LogXNet
{
    public class LogExcepcion
    {
        private Respuesta _Objresponse;


        public LogExcepcion()
        {
            _Objresponse = new Respuesta();

        }
        public Respuesta SetException(Exception Excepcion, ILogAPI _logger, Type TypeClass)
        {
            try
            {
                string erro = "";
                if (Excepcion.InnerException != null)
                {
                    erro = Excepcion.Message.ToString() + " InnerException " + (Excepcion.InnerException.InnerException == null ? Excepcion.InnerException.Message : Excepcion.InnerException.InnerException.Message);
                }
                else
                {
                    erro = Excepcion.Message.ToString();
                }
                _logger.Error(Excepcion.StackTrace.ToString() + " " + erro, TypeClass);
                _Objresponse.statusCode = "001";
                _Objresponse.statusDesc = erro;
                return _Objresponse;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
