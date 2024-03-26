using Microsoft.EntityFrameworkCore;
using Clinics.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Clinics.Core.Models.Authentication;

namespace Clinics.Data
{
    public class ClinicContext : IdentityDbContext<ApplicationUser>
    {
        public ClinicContext(DbContextOptions<ClinicContext> opt) : base(opt)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeIngredient)
            .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredient)
                .HasForeignKey(ri => ri.IngredientId);

            modelBuilder.Entity<Prescription>()
                 .HasOne(p => p.Doctor)
                 .WithMany(d => d.Prescriptions)
                 .HasForeignKey(p => p.DoctorId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Doctor>()
            //.HasOne(d => d.Clinic)
            //.WithMany(c => c.Doctors)
            //.HasForeignKey(d => d.ClinicId)
            //.OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Doctor)
                .WithMany(d => d.Reservations)
                .HasForeignKey(r => r.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reservation>()
                .HasOne(p => p.Patient)
                .WithMany(d => d.Reservations)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PatientHistory>()
                 .HasOne(d => d.Doctor)
                 .WithMany(p => p.PatientHistories)
                 .HasForeignKey(d => d.DoctorId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PatientHistory>()
                .HasOne(p => p.Patient)
                .WithMany(p => p.PatientHistories)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

          

            modelBuilder.Entity<PatientHistory>()
               .HasOne(s => s.Symptom)
               .WithMany(p => p.PatientHistories)
               .HasForeignKey(s => s.SymptomId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DoctorRating>()
               .HasOne(s => s.Doctor)
               .WithMany(p => p.DoctorRatings)
               .HasForeignKey(s => s.DoctorId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DoctorRating>()
            .HasOne(s => s.Patient)
            .WithMany(p => p.DoctorRatings)
            .HasForeignKey(s => s.PatientId)
            .OnDelete(DeleteBehavior.NoAction);

        }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Reservation> Reservations  { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Immunization> Immunizations  { get; set; }
        public DbSet<PatientHistory> PatientHistories { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<DrugDetail> DrugDetails { get; set; }
        public DbSet<DoctorRating> DoctorRatings { get; set; }

    }
}
