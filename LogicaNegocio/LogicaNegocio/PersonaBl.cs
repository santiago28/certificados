using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Modelo;

namespace LogicaNegocio.LogicaNegocio
{
    public class PersonaBl
    {
        Modelo entity = new Modelo();

        public Persona ConsultarPersona(Persona oPersona)
        {
            var persona = (from i in entity.Persona
                           where i.documento == oPersona.documento
                           select i).FirstOrDefault();
            return persona;
        }

        public Persona CrearPersona(Persona oPersona) 
        {
            var persona = (from i in entity.Persona
                         where i.documento == oPersona.documento
                         select i).FirstOrDefault();
            if (persona == null)
            {
                entity.Persona.Add(oPersona);
                entity.SaveChanges();
            }

            persona = oPersona;

            return persona;
        }
    }
}
