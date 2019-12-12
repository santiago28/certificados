namespace Datos.Modelo
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Modelo : DbContext
    {
        public Modelo()
            : base("name=Modelo")
        {
        }

        public virtual DbSet<Ampliacion> Ampliacion { get; set; }
        public virtual DbSet<Cdp> Cdp { get; set; }
        public virtual DbSet<Contrato> Contrato { get; set; }
        public virtual DbSet<Convenio> Convenio { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Ampliacion>()
            //    .Property(e => e.cdp_otro_di)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Ampliacion>()
            //    .Property(e => e.rp_otro_si)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Ampliacion>()
            //    .Property(e => e.valor_adicion_letras)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Ampliacion>()
            //    .Property(e => e.contrato_mas_adicion_letras)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Cdp>()
            //    .Property(e => e.numero)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Cdp>()
            //    .Property(e => e.solicitud)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Cdp>()
            //    .HasMany(e => e.Contrato)
            //    .WithRequired(e => e.Cdp)
            //    .HasForeignKey(e => e.id_cdp)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.numero_contrato)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.rp)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.linea)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.componente)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.tipo)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.perfil)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.nivel_educativo)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.profesion)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.objeto)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.actividades)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.productos)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.honorarios_letras)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.valor_contrato_letras)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.eps)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.afp)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.arl)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.adjunta_tp)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.ibc)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.observaciones)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.valor_pagar_letras)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.valor_pagado_letras)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.valor_liberar_letras)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.periodo)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .Property(e => e.observacion_renuncia)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Contrato>()
            //    .HasMany(e => e.Ampliacion)
            //    .WithRequired(e => e.Contrato)
            //    .HasForeignKey(e => e.id_contrato)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Convenio>()
            //    .Property(e => e.nombre)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Convenio>()
            //    .Property(e => e.codigo_convenio)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Convenio>()
            //    .HasMany(e => e.Contrato)
            //    .WithRequired(e => e.Convenio)
            //    .HasForeignKey(e => e.id_convenio)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.tipo_documento)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.documento)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.nombre)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.genero)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.direccion)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.comuna)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.grupo_poblacional)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.telefono)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.celular)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.correo)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.municipio_domicilio)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.municipio_nacimiento)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.lugar_expedicion_documento)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.banco)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.cuenta)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .Property(e => e.tipo_cuenta)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Persona>()
            //    .HasMany(e => e.Contrato)
            //    .WithRequired(e => e.Persona)
            //    .HasForeignKey(e => e.id_persona)
            //    .WillCascadeOnDelete(false);
        }
    }
}
