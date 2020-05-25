using Microsoft.EntityFrameworkCore;
using TechChallenge.Data.EntityModels;

namespace TechChallenge.Data
{
    public partial class HipagesDbContext : DbContext
    {
        public HipagesDbContext()
        {
        }

        public HipagesDbContext(DbContextOptions<HipagesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<jobs> Jobs { get; set; }
        public virtual DbSet<Suburbs> Suburbs { get; set; }

        #region Code for OnConfiguring, OnModelCreating
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=hipages;database=hipages");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.HasIndex(e => e.ParentCategoryId)
                    .HasName("idx_categories_parent_category");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ParentCategoryId)
                    .HasColumnName("parent_category_id")
                    .HasColumnType("int(11) unsigned");
            });

            modelBuilder.Entity<jobs>(entity =>
            {
                entity.ToTable("jobs");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("idx_jobs_category");

                entity.HasIndex(e => e.SuburbId)
                    .HasName("idx_jobs_suburb");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.ContactEmail)
                    .IsRequired()
                    .HasColumnName("contact_email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasColumnName("contact_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhone)
                    .IsRequired()
                    .HasColumnName("contact_phone")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("int(3) unsigned");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'new'");

                entity.Property(e => e.SuburbId)
                    .HasColumnName("suburb_id")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_jobs_category");

                entity.HasOne(d => d.Suburb)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.SuburbId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_jobs_suburb");
            });

            modelBuilder.Entity<Suburbs>(entity =>
            {
                entity.ToTable("suburbs");

                entity.HasIndex(e => e.Postcode)
                    .HasName("idx_suburbs_postcode");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Postcode)
                    .IsRequired()
                    .HasColumnName("postcode")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        #endregion
    }
}
