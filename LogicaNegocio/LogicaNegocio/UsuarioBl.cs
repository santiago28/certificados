using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Modelo;

namespace LogicaNegocio.LogicaNegocio
{
    public class UsuarioBl
    {
        Modelo entity = new Modelo();

        public void GuardarUsuario(Usuario oUsuario)
        {
            var encriptar = SecurityEncode.SecurityEncode.Encriptar(oUsuario.documento);
            oUsuario.estado = true;
            oUsuario.contrasena = encriptar;
            entity.Usuario.Add(oUsuario);
            entity.SaveChanges();
        }

        public Tuple<Usuario,int> IniciarSesion(string usuario, string contrasena)
        {
            var encriptar = SecurityEncode.SecurityEncode.Encriptar(contrasena).ToString();

            var datos = (from i in entity.Usuario
                         where i.documento == usuario
                         && i.contrasena == encriptar
                         select i).FirstOrDefault();

            var respuesta = 0;
            if (datos == null)
            {
                respuesta = 0;
            }else if (datos.estado == false)
            {
                respuesta = 1;
            }
            else
            {
                respuesta = 2;
            }

            return new Tuple<Usuario, int>(datos, respuesta);
        }

        public List<Usuario> ConsultarUsuarios()
        {
            var usuarios = (from i in entity.Usuario
                            select i).ToList();
            return usuarios;
        }

        public Usuario CambiarEstadoUsuario(Usuario oUsuario)
        {
            var usuario = (from i in entity.Usuario
                           where i.id == oUsuario.id
                           select i).FirstOrDefault();

            usuario.estado = !usuario.estado;
            entity.SaveChanges();

            return usuario;
        }

        public Usuario EditarUsuario(Usuario oUsuario)
        {
            var usuario = (from i in entity.Usuario
                           where i.id == oUsuario.id
                           select i).FirstOrDefault();

            usuario.documento = oUsuario.documento;
            usuario.nombre = oUsuario.nombre;
            usuario.correo_electronico = oUsuario.correo_electronico;
            usuario.telefono = oUsuario.telefono;
            entity.SaveChanges();
            return usuario;
        }
    }
}
