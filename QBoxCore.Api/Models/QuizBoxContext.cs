using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QBoxCore.Api.Models
{
    public partial class QuizBoxContext : DbContext
    {
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameQuestion> GameQuestion { get; set; }
        public virtual DbSet<Highscore> Highscore { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Question> Question { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"data source=db;initial catalog=Quizbox;integrated security=False;User Id=sa;Password=1qaz2WSX3edc;MultipleActiveResultSets=True;App=EntityFramework;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasIndex(e => e.QuestionId)
                    .HasName("IX_QuestionId");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Answer_dbo.Question_QuestionId");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_CategoryId");

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Game_dbo.Category_CategoryId");
            });

            modelBuilder.Entity<GameQuestion>(entity =>
            {
                entity.HasIndex(e => e.AnswerId)
                    .HasName("IX_AnswerId");

                entity.HasIndex(e => e.GameId)
                    .HasName("IX_GameId");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("IX_QuestionId");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.GameQuestion)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK_dbo.GameQuestion_dbo.Answer_AnswerId");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameQuestion)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.GameQuestion_dbo.Game_GameId");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.GameQuestion)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.GameQuestion_dbo.Question_QuestionId");
            });

            modelBuilder.Entity<Highscore>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_CategoryId");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Highscore)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Highscore_dbo.Category_CategoryId");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_CategoryId");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Question_dbo.Category_CategoryId");
            });
        }
    }
}
