using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class LRomanProgramacionNCapasContext : DbContext
    {
        public LRomanProgramacionNCapasContext()
        {
        }

        public LRomanProgramacionNCapasContext(DbContextOptions<LRomanProgramacionNCapasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aseguradora> Aseguradoras { get; set; } = null!;
        public virtual DbSet<Colonium> Colonia { get; set; } = null!;
        public virtual DbSet<Dependiente> Dependientes { get; set; } = null!;
        public virtual DbSet<DependienteTipo> DependienteTipos { get; set; } = null!;
        public virtual DbSet<Direccion> Direccions { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<EmpleadoNew> EmpleadoNews { get; set; } = null!;
        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Municipio> Municipios { get; set; } = null!;
        public virtual DbSet<Pai> Pais { get; set; } = null!;
        public virtual DbSet<Poliza> Polizas { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<SubPoliza> SubPolizas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.; Database= LRomanProgramacionNCapas; Trusted_Connection=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aseguradora>(entity =>
            {
                entity.HasKey(e => e.IdAseguradora)
                    .HasName("PK__Asegurad__8FA1C597DECB5A76");

                entity.ToTable("Aseguradora");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Imagen).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Aseguradoras)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Asegurado__IdUsu__29572725");
            });

            modelBuilder.Entity<Colonium>(entity =>
            {
                entity.HasKey(e => e.IdColonia)
                    .HasName("PK__Colonia__A1580F66C5BA5D18");

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Colonia)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK__Colonia__IdMunic__4222D4EF");
            });

            modelBuilder.Entity<Dependiente>(entity =>
            {
                entity.HasKey(e => e.IdDependiente)
                    .HasName("PK__Dependie__366D0771C3B6FCC1");

                entity.ToTable("Dependiente");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCivil)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Genero)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IdEmpleado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rfc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RFC");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Dependientes)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK__Dependien__IdEmp__0A9D95DB");
            });

            modelBuilder.Entity<DependienteTipo>(entity =>
            {
                entity.HasKey(e => e.IdDependienteTipo)
                    .HasName("PK__Dependie__2C220C62C152AE81");

                entity.ToTable("DependienteTipo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.HasKey(e => e.IdDireccion)
                    .HasName("PK__Direccio__1F8E0C7622E41156");

                entity.ToTable("Direccion");

                entity.Property(e => e.Calle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroExterior)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroInterior)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdColoniaNavigation)
                    .WithMany(p => p.Direccions)
                    .HasForeignKey(d => d.IdColonia)
                    .HasConstraintName("FK__Direccion__IdCol__44FF419A");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Direccions)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Direccion__IdUsu__45F365D3");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.NumeroEmpleado)
                    .HasName("PK__Empleado__44F848FC4F44AB15");

                entity.ToTable("Empleado");

                entity.HasIndex(e => e.NumeroEmpleado, "UQ__Empleado__44F848FD64EBB578")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Empleado__A9D10534082F0642")
                    .IsUnique();

                entity.Property(e => e.NumeroEmpleado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Foto).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nss)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NSS");

                entity.Property(e => e.Rfc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RFC");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK__Empleado__IdEmpr__787EE5A0");

                entity.HasOne(d => d.IdPolizaNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdPoliza)
                    .HasConstraintName("FK__Empleado__IdPoli__797309D9");
            });

            modelBuilder.Entity<EmpleadoNew>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EmpleadoNew");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Foto).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(51)
                    .IsUnicode(false);

                entity.Property(e => e.Nss)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NSS");

                entity.Property(e => e.NumeroEmpleado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rfc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RFC");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PK__Empresa__5EF4033E33841813");

                entity.ToTable("Empresa");

                entity.Property(e => e.DireccionWeb)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.Logo).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__Estado__FBB0EDC1158FCA4E");

                entity.ToTable("Estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Estados)
                    .HasForeignKey(d => d.IdPais)
                    .HasConstraintName("FK__Estado__IdPais__3C69FB99");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio)
                    .HasName("PK__Municipi__61005978687E8B78");

                entity.ToTable("Municipio");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK__Municipio__IdEst__3F466844");
            });

            modelBuilder.Entity<Pai>(entity =>
            {
                entity.HasKey(e => e.IdPais)
                    .HasName("PK__Pais__FC850A7BEDFF88E4");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Poliza>(entity =>
            {
                entity.HasKey(e => e.IdPoliza)
                    .HasName("PK__Poliza__8E3943B338BBE57D");

                entity.ToTable("Poliza");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPoliza)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSubPolizaNavigation)
                    .WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.IdSubPoliza)
                    .HasConstraintName("FK__Poliza__IdSubPol__73BA3083");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Poliza__IdUsuari__74AE54BC");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584C117693A5");

                entity.ToTable("Rol");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubPoliza>(entity =>
            {
                entity.HasKey(e => e.IdSubPoliza)
                    .HasName("PK__SubPoliz__91F56A986C8A11E4");

                entity.ToTable("SubPoliza");

                entity.Property(e => e.IdSubPoliza).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF97C4846E69");

                entity.ToTable("Usuario");

                entity.HasIndex(e => e.Email, "UQ__Usuario__A9D105346C8FA0D5")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "UQ__Usuario__C9F28456B3042554")
                    .IsUnique();

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Curp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CURP");

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Imagen).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.RolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Usuario__Rol__37A5467C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
