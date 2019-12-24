using Datos.Modelo;
using LogicaNegocio.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace certificados_pits.Controllers
{
    public class ParametrosController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ConsultarParametros()
        {
            try
            {
                ParametrosBl oParametrosBl = new ParametrosBl();
                return Ok(new { parametros = oParametrosBl.ConsultarParametros(), success = true });
            }
            catch (Exception exc)
            {
                return Ok(new { success = false, exc = exc.Message });
            }
        }

        [HttpPost]
        public IHttpActionResult GuardarParametros(List<Parametros> oParametros)
        {
            try
            {
                ParametrosBl oParametrosBl = new ParametrosBl();
                oParametrosBl.GuardarParametros(oParametros);
                return Ok(new { success = true });
            }
            catch (Exception exc)
            {
                return Ok(new { success = false, exc = exc.Message });
            }
        }

    }
}