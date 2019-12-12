using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Modelo;

namespace LogicaNegocio.LogicaNegocio
{
    public class AmpliacionBl
    {
        Modelo entity = new Modelo();
        public List<Cdp> ConsultarCdp()
        {
            var cdp = (from i in entity.Cdp
                       select i).ToList();

            return cdp;
        }
    }
}
