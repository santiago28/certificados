namespace Datos.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ampliacion")]
    public partial class Ampliacion
    {
        [Key]
        public int id { get; set; }

        public int id_contrato { get; set; }

        [StringLength(50)]
        public string cdp_otro_di { get; set; }

        [StringLength(50)]
        public string rp_otro_si { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_inicio_adicion { get; set; }

        public int? dias_adicion { get; set; }

        public int? dias_ampliacion { get; set; }

        public double? valor_adicion { get; set; }

        [StringLength(150)]
        public string valor_adicion_letras { get; set; }

        public double? contrato_mas_adicion { get; set; }

        [StringLength(150)]
        public string contrato_mas_adicion_letras { get; set; }

        public int? dias_total_con_ampliacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_terminacion_adicion { get; set; }

    }
}
