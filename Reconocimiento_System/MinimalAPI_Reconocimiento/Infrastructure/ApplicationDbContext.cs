using Microsoft.EntityFrameworkCore;

using MinimalAPI_Reconocimiento.Models.ApplicationModel;

using System.Reflection;

namespace MinimalAPI_Reconocimiento.Infrastructure
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        //protected ApplicationDbContext()
        //{

        //}
        public DbSet<PatenteModel>? Patente { get; set; } = null!;

        public DbSet<TraficoModel>? Trafico { get; set; } = null!;

        public ApplicationDbContext()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("No connection string set for database.");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure PatenteModel
            modelBuilder.Entity<PatenteModel>(entity =>
            {
                entity.HasKey(e => e.IdPatente);
                entity.Property(e => e.Patente).HasMaxLength(10).HasColumnName("Patente").IsRequired();
                entity.Property(e => e.Active).HasDefaultValue(true);
                entity.Property(e => e.FechaAlta).HasDefaultValueSql("getdate()");
            });
            modelBuilder.Entity<TraficoModel>(entity =>
            {
                entity.HasKey(e => e.IdTrafico);
                entity.Property(e => e.PatentesReconocidas).HasColumnType("bigint").HasColumnName("PatentesReconocidas").IsRequired();
                entity.Property(e => e.PatentesNoReconocidas).HasColumnType("bigint").HasColumnName("PatentesNoReconocidas").IsRequired();
                entity.Property(e => e.Fecha).HasDefaultValueSql("getdate()");
            });




            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        

    }
}
