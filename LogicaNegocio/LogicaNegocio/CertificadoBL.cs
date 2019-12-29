using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Modelo;
using Datos.DTO;


namespace LogicaNegocio.LogicaNegocio
{
    public class CertificadoBL
    {
        Modelo entity = new Modelo();

        public List<CertificadoDTO> ConsultarContratoxDocumento(Persona oPersona)
        {
            List<CertificadoDTO> ListaCertificadoDTO = new List<CertificadoDTO>();

                     
            var contrato = (from c in entity.Contrato
                            join p in entity.Persona on c.id_persona equals p.id
                            where p.documento == oPersona.documento
                            select c).ToList();

            foreach (var item in contrato)
            {
                var convenio = (from i in entity.Convenio
                                where i.id == item.id_convenio
                                select i).FirstOrDefault();

                var persona = (from i in entity.Persona
                               where i.id == item.id_persona
                               select i).FirstOrDefault();

                CertificadoDTO oCertificadoDTO = new CertificadoDTO();
                oCertificadoDTO.codigo_convenio = convenio.codigo_convenio;
                oCertificadoDTO.anio = item.anio;
                oCertificadoDTO.fecha_inicio = item.fecha_inicio;
                oCertificadoDTO.fecha_fin = item.fecha_fin;
                oCertificadoDTO.nombre_convenio = convenio.nombre;
                oCertificadoDTO.numero_contrato = item.numero_contrato;
                oCertificadoDTO.documento = persona.documento;
                oCertificadoDTO.duracion_dias = item.duracion_dias;
                ListaCertificadoDTO.Add(oCertificadoDTO);
            }

            return ListaCertificadoDTO;
        }
    }
}
