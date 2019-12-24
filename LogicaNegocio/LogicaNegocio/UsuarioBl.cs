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
            oUsuario.contrasena = encriptar;
            entity.Usuario.Add(oUsuario);
            entity.SaveChanges();
        }

        public Tuple<Usuario,int> IniciarSesion(string usuario, string contrasena)
        {
            var encriptar = SecurityEncode.SecurityEncode.Encriptar(contrasena);

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
    }
}
