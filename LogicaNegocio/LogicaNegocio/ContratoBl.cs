using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using Datos.DTO;
using System.Linq;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace LogicaNegocio.LogicaNegocio
{
    public class ContratoBl
    {
        Modelo entity = new Modelo();
        public Contrato ConsultarContrato(string nuncontrato, int id_convenio, int anio)
        {
           
            var contrato = (from i in entity.Contrato
                            where i.numero_contrato == nuncontrato && i.id_convenio == id_convenio && i.anio == anio
                            select i).FirstOrDefault();
            return contrato;
        }

        public Convenio Consultar_Convenio(string Cod_convenio)
        {
            
            var convenio = (from i in entity.Convenio
                            where i.codigo_convenio == Cod_convenio
                            select i).FirstOrDefault();
            return convenio;
        }

        public Convenio Crear_Convenio(Convenio oConvenio)
        {
            entity.Convenio.Add(oConvenio);
            entity.SaveChanges();

            return oConvenio;
        }

    }
}
