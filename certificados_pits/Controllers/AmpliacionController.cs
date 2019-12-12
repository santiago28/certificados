using Datos.Modelo;
using LogicaNegocio.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace certificados_pits.Controllers
{
    public class AmpliacionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ConsultarCdp()
        {
            try
            {
                AmpliacionBl oAmpliacionBl = new AmpliacionBl();
                var cdps = oAmpliacionBl.ConsultarCdp();
                return Ok(new { cdps, success = true });
            }
            catch (Exception exc)
            {
                return Ok(new { success = false, exc = exc.Message });
            }
        }
    }
}