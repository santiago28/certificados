using Datos.Modelo;
using LogicaNegocio.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace certificados_pits.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpPost]
        public IHttpActionResult IniciarSesion(Usuario oUsuario)
        {
            try
            {
                UsuarioBl oUsuarioBl = new UsuarioBl();
                var respuesta_usuario = oUsuarioBl.IniciarSesion(oUsuario.documento, oUsuario.contrasena);
                return Ok(new { success = true, usuario = respuesta_usuario.Item1, respuesta = respuesta_usuario.Item2 });
            }
            catch (Exception exc)
            {
                return Ok(new { success = false, exc });
            }
        }

        [HttpGet]
        public IHttpActionResult ConsultarUsuarios()
        {
            try
            {
                UsuarioBl oUsuarioBl = new UsuarioBl();
                return Ok(new { success = true, usuarios = oUsuarioBl.ConsultarUsuarios() });
            }
            catch (Exception exc)
            {
                return Ok(new { success = false, exc });
            }
        }

        [HttpPost]
        public IHttpActionResult GuardarUsuario(Usuario oUsuario)
        {
            try
            {
                UsuarioBl oUsuarioBl = new UsuarioBl();
                oUsuarioBl.GuardarUsuario(oUsuario);
                return Ok(new { success = true });
            }
            catch (Exception exc)
            {
                return Ok(new { success = false, exc });
            }
        }

        [HttpPost]
        public IHttpActionResult CambiarEstadoUsuario(Usuario oUsuario)
        {
            try
            {
                UsuarioBl oUsuarioBl = new UsuarioBl();
                return Ok(new { success = true, usuario = oUsuarioBl.CambiarEstadoUsuario(oUsuario) });
            }
            catch (Exception exc)
            {
                return Ok(new { success = false, exc });
            }
        }

        [HttpPost]
        public IHttpActionResult EditarUsuario(Usuario oUsuario)
        {
            try
            {
                UsuarioBl oUsuarioBl = new UsuarioBl();
                return Ok(new { success = true, usuario = oUsuarioBl.EditarUsuario(oUsuario) });
            }
            catch (Exception exc)
            {
                return Ok(new { success = false, exc });
            }
        }
    }
}