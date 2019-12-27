namespace Datos.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contrato")]
    public partial class Contrato
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Contrato()
        //{
        //    Ampliacion = new HashSet<Ampliacion>();
        //}
        [Key]
        public int id { get; set; }

        public int id_convenio { get; set; }

        [StringLength(50)]
        public string numero_contrato { get; set; }


        [StringLength(20)]
        public string rp { get; set; }

        [StringLength(200)]
        public string linea { get; set; }

        [StringLength(500)]
        public string componente { get; set; }

        [StringLength(100)]
        public string tipo { get; set; }

        [StringLength(300)]
        public string perfil { get; set; }

        [StringLength(200)]
        public string nivel_educativo { get; set; }

        [StringLength(100)]
        public string profesion { get; set; }

        [StringLength(3000)]
        public string objeto { get; set; }

        [StringLength(8000)]
        public string actividades { get; set; }

        [StringLength(8000)]
        public string productos { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_contrato { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_inicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_fin { get; set; }

        public double? honorarios { get; set; }

        [StringLength(150)]
        public string honorarios_letras { get; set; }

        public double? duracion_dias { get; set; }

        public int? duracion_contrato { get; set; }

        public double? valor_contrato { get; set; }

        [StringLength(150)]
        public string valor_contrato_letras { get; set; }

        [StringLength(50)]
        public string eps { get; set; }

        [StringLength(50)]
        public string afp { get; set; }

        public int? riesgo_arl { get; set; }

        [StringLength(50)]
        public string arl { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_examen_po { get; set; }

        [StringLength(10)]
        public string adjunta_tp { get; set; }

        [StringLength(50)]
        public string ibc { get; set; }

        public double? valor_eps { get; set; }

        public double? valor_afp { get; set; }

        public double? varlo_arl { get; set; }

        [StringLength(6000)]
        public string observaciones { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_real_terminacion { get; set; }

        public double? valor_pagar { get; set; }

        [StringLength(150)]
        public string valor_pagar_letras { get; set; }

        public double? valor_pagado { get; set; }

        [StringLength(150)]
        public string valor_pagado_letras { get; set; }

        public double? valor_liberar { get; set; }

        [StringLength(150)]
        public string valor_liberar_letras { get; set; }

        [StringLength(50)]
        public string periodo { get; set; }

        [StringLength(2000)]
        public string observacion_renuncia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_terminacion { get; set; }

        public int id_persona { get; set; }

        public int? anio { get; set; }

    }
}
