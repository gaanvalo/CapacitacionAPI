using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonasDAL.DALPersona;
using PersonasDTO.DTOs;
using PersonasDTO.LogXNet;

namespace PersonasAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaRepository _PersonaRepo;
        private ILogAPI _logger;
        private LogExcepcion _Excep;

        public PersonasController(IPersonaRepository PersonaRepo, ILogAPI logger)
        {
            _PersonaRepo = PersonaRepo;
            _logger = logger;
            _Excep = new LogExcepcion();
        }

        [HttpGet]
        public IActionResult GetAllPersonas()
        {
            try
            {
                RespuestaList response = new RespuestaList();
                var Listpersonas = _PersonaRepo.GetAllAsync();
                if (Listpersonas.Count() > 0)
                {
                    response.StatusCode = "000";
                    response.StatusDesc = "Transaccion Exitósa";
                    response.ListPersonas = Listpersonas;
                }
                else
                {
                    response.StatusCode = "001";
                    response.StatusDesc = "Consulta Inválida";
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Ok(_Excep.SetException(ex, _logger, typeof(PersonasController)));
            }    
        }


        [HttpGet("{id}")]
        public IActionResult GetPersonaBydId([FromRoute] int id)
        {
            try
            {
                RespuestaObject response = new RespuestaObject();
                var ObjPersona = _PersonaRepo.GetAsync(id);
                if (ObjPersona != null)
                {
                    response.StatusCode = "000";
                    response.StatusDesc = "Transaccion Exitósa";
                    response.Schema = ObjPersona;
                }
                else
                {
                    response.StatusCode = "001";
                    response.StatusDesc = "Consulta Inválida";
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Ok(_Excep.SetException(ex, _logger, typeof(PersonasController)));
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] PersonaItem ObjPersonas)
        {
            try
            {
                Respuesta ObjResponse = new Respuesta();
                object response = null;


                _PersonaRepo.InsertAsync(ObjPersonas, out response);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Ok(_Excep.SetException(ex, _logger, typeof(PersonasController)));
            }
        }

    }
}