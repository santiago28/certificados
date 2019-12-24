

namespace Datos.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Parametros")]
    public partial class Parametros
    {
        [Key]
        public int id { get; set; }

        public string parametro { get; set; }
        public string valor { get; set; }
        public string descripcion { get; set; }
        public bool es_imagen { get; set; }
    }
}
