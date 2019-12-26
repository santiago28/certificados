
namespace Datos.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [Key]
        public int id { get; set; }

        public string documento { get; set; }
        public string nombre { get; set; }
        public string correo_electronico { get; set; }
        public string telefono { get; set; }
        public string contrasena { get; set; }
        public bool estado { get; set; }
    }
}
