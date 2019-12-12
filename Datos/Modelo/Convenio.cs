namespace Datos.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Convenio")]
    public partial class Convenio
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Convenio()
        //{
        //    Contrato = new HashSet<Contrato>();
        //}
        [Key]
        public int id { get; set; }

        [StringLength(250)]
        public string nombre { get; set; }

        [StringLength(25)]
        public string codigo_convenio { get; set; }

        public int? anio { get; set; }
    }
}
