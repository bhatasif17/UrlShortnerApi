using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

#nullable disable

namespace URLShortner.Models
{
    public partial class URLShortnerContext : DbContext
    {
        public URLShortnerContext()
        {
        }

        public URLShortnerContext(DbContextOptions<URLShortnerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Urlstore> Urlstores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                  .AddJsonFile("appsettings.json")
                  .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                // optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;user=sa;password=Asif@678;database=URLShortner;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Urlstore>(entity =>
            {
                entity.ToTable("URLStore");

                entity.Property(e => e.Id)
                    .HasMaxLength(6)
                    .HasColumnName("ID");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("URL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
