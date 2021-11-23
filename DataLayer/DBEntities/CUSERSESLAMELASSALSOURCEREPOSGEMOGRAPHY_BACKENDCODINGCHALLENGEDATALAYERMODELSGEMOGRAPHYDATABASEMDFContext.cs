using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataLayer.DBEntities
{
    public partial class CUSERSESLAMELASSALSOURCEREPOSGEMOGRAPHY_BACKENDCODINGCHALLENGEDATALAYERMODELSGEMOGRAPHYDATABASEMDFContext : DbContext
    {
        public CUSERSESLAMELASSALSOURCEREPOSGEMOGRAPHY_BACKENDCODINGCHALLENGEDATALAYERMODELSGEMOGRAPHYDATABASEMDFContext()
        {
        }

        public CUSERSESLAMELASSALSOURCEREPOSGEMOGRAPHY_BACKENDCODINGCHALLENGEDATALAYERMODELSGEMOGRAPHYDATABASEMDFContext(DbContextOptions<CUSERSESLAMELASSALSOURCEREPOSGEMOGRAPHY_BACKENDCODINGCHALLENGEDATALAYERMODELSGEMOGRAPHYDATABASEMDFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\EslamElassal\\source\\repos\\gemography_backend-coding-challenge\\DataLayer\\Models\\GemographyDatabase.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
