using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DTO
{
    public class CertificadoDTO
    {
        public string codigo_convenio { get; set; }

        public int? anio { get; set; }

        public DateTime? fecha_inicio { get; set; }

        public DateTime? fecha_fin { get; set; }

        public string numero_contrato { get; set; }

        public string nombre_convenio { get; set; }

        public string documento { get; set; }
    }
}
