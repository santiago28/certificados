using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Modelo;
using Datos.DTO;

namespace LogicaNegocio.LogicaNegocio
{
    public class ParametrosBl
    {
        Modelo entity = new Modelo();

        public List<Parametros> ConsultarParametros()
        {
            var Parametros = (from i in entity.Parametros
                             select i).ToList();

            return Parametros;
        }

        public void GuardarParametros(List<Parametros> oListaParametros)
        {
            foreach (var item in oListaParametros)
            {
                var parametro = (from i in entity.Parametros
                                 where i.id == item.id
                                 select i).FirstOrDefault();

                parametro.valor = item.valor;
                entity.SaveChanges();
            }
        }
    }
}
