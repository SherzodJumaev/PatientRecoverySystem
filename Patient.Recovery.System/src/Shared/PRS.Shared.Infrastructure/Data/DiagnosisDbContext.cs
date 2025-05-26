using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRS.Shared.Models;
using PRS.Shared.Models.DiagnosisModels;
using PRS.Shared.Models.MedicalHistoryModels;
using PRS.Shared.Models.PatientModels;

namespace PRS.Shared.Infrastructure.Data
{
    public class DiagnosisDbContext : DbContext
    {
        public DiagnosisDbContext(DbContextOptions<DiagnosisDbContext> options) : base(options) { }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Physician> Physicians { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Diagnosis Entity
            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Symptoms).HasMaxLength(2000);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(d => d.PatientId);

                entity.HasOne(d => d.Physician)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(d => d.PhysicianId);
            });
        }
    }
}