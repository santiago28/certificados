namespace Datos.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Persona")]
    public partial class Persona
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Persona()
        //{
        //    Contrato = new HashSet<Contrato>();
        //}
        [Key]
        public int id { get; set; }

        [StringLength(10)]
        public string tipo_documento { get; set; }

        [StringLength(50)]
        public string documento { get; set; }

        [StringLength(70)]
        public string nombre { get; set; }

        [StringLength(20)]
        public string genero { get; set; }

        [StringLength(100)]
        public string direccion { get; set; }

        [StringLength(150)]
        public string comuna { get; set; }

        public int? estrato { get; set; }

        [StringLength(100)]
        public string grupo_poblacional { get; set; }

        [StringLength(25)]
        public string telefono { get; set; }

        [StringLength(20)]
        public string celular { get; set; }

        [StringLength(30)]
        public string correo { get; set; }

        [StringLength(150)]
        public string municipio_domicilio { get; set; }

        [StringLength(150)]
        public string municipio_nacimiento { get; set; }

        [StringLength(150)]
        public string lugar_expedicion_documento { get; set; }

        [StringLength(100)]
        public string banco { get; set; }

        [StringLength(20)]
        public string cuenta { get; set; }

        [StringLength(50)]
        public string tipo_cuenta { get; set; }

    }
}
