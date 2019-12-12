using Datos.Modelo;
using LogicaNegocio.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace certificados_pits.Controllers
{
    public class CertificadoController : ApiController
    {
        [HttpPost]
        public IHttpActionResult ConsultarContratoxDocumento(Persona oPersona)
        {
            try
            {
                CertificadoBL oCertificadoBl = new CertificadoBL();
                PersonaBl oPersonaBl = new PersonaBl();
                var contratos = oCertificadoBl.ConsultarContratoxDocumento(oPersona);
                var persona = oPersonaBl.ConsultarPersona(oPersona);
                return Ok(new { contratos, persona, success = true });
            }
            catch (Exception exc)
            {
                return Ok(new { success = false, exc = exc.Message });
            }
        }
    }
}