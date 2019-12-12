namespace Datos.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cdp")]
    public partial class Cdp
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Cdp()
        //{
        //    Contrato = new HashSet<Contrato>();
        //}
        [Key]
        public int id { get; set; }

        [StringLength(20)]
        public string numero { get; set; }

        [StringLength(50)]
        public string solicitud { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Contrato> Contrato { get; set; }
    }
}
