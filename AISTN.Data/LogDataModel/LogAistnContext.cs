using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AISTN.Data.LogDataModel;

public partial class LogAistnContext : DbContext
{
    public LogAistnContext(DbContextOptions<LogAistnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LogApiRequest> LogApiRequests { get; set; }

    public virtual DbSet<LogDbcontext> LogDbcontexts { get; set; }

    public virtual DbSet<LogException> LogExceptions { get; set; }

    public virtual DbSet<LogUserAction> LogUserActions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_100_CI_AS");

        modelBuilder.Entity<LogApiRequest>(entity =>
        {
            entity.ToTable("LOG_ApiRequest");

            entity.HasIndex(e => e.ProcessingStatus, "IX_ProcessingStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Endpoint).HasMaxLength(500);
            entity.Property(e => e.IpAddress).HasMaxLength(50);
        });

        modelBuilder.Entity<LogDbcontext>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LOG_DBContextLog");

            entity.ToTable("LOG_DBContext");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActionTable).HasMaxLength(200);
            entity.Property(e => e.ActionType).HasMaxLength(50);
            entity.Property(e => e.TableRow).HasMaxLength(50);
            entity.Property(e => e.UserActionLogId).HasColumnName("UserActionLogID");

            entity.HasOne(d => d.UserActionLog).WithMany(p => p.LogDbcontexts)
                .HasForeignKey(d => d.UserActionLogId)
                .HasConstraintName("FK_LOG_DBContextLog_UserAction");
        });

        modelBuilder.Entity<LogException>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LOG_ExceptionLog");

            entity.ToTable("LOG_Exceptions");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IpAddress).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(500);
            entity.Property(e => e.UserId).HasMaxLength(128);
        });

        modelBuilder.Entity<LogUserAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LOG_UserActionLog");

            entity.ToTable("LOG_UserAction");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.UserActionType).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
