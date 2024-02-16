using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CatSalon.Models.DataAccess
{
    public partial class CatSalonContext : DbContext
    {
        public CatSalonContext()
        {
        }

        public CatSalonContext(DbContextOptions<CatSalonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Cat> Cats { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<AppointmentService>  AppointmentServices { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json");
                IConfiguration configuration = builder.Build();
                string connectionString = configuration.GetConnectionString("CatSalon");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Associative Entity 
            modelBuilder.Entity<AppointmentService>(entity =>
            {
                entity.ToTable("Appointment_Service");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                //entity.HasKey(e => e.Id);
                //entity.HasKey(e => new { e.AppointmentId, e.ServiceId });
                entity.Property(e => e.AppointmentId);
                entity.Property(e => e.ServiceId);

                //entity.HasOne(d => d.Appointment)
                //    .WithMany(p => p.AppointmentService)
                //    .HasForeignKey(d => d.AppointmentId)
                //    .HasConstraintName("FK_AppointmentService_AppointmentId");

                //entity.HasOne(d => d.Service)
                //    .WithMany(p => p.AppointmentsService)
                //    .HasForeignKey(d => d.ServiceId)
                //    .HasConstraintName("FK_AppointmentService_ServiceId");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.Id);

                entity.Property(e => e.CatId).HasColumnName("Cat_Id");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");

                entity.Property(e => e.ScheduledDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Scheduled_Date");

                //entity.HasMany(d => d.Services)
                //    .WithMany(p => p.Appointments)
                //    .UsingEntity<Dictionary<string, object>>(
                //        "AppointmentService",
                //        l => l.HasOne<Service>().WithMany().HasForeignKey("ServiceId").HasConstraintName("FK_AppointmentService_ServiceId"),
                //        r => r.HasOne<Appointment>().WithMany().HasForeignKey("AppointmentId").HasConstraintName("FK_AppointmentService_AppointmentId"),
                //        j =>
                //        {
                //            j.HasKey("AppointmentId", "ServiceId").HasName("PK__Appointm__329C47C2400B8D2F");

                //            j.ToTable("Appointment_Service");
                //        });
            });

            modelBuilder.Entity<Cat>(entity =>
            {
                entity.ToTable("Cat");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("Birth_Date");

                entity.Property(e => e.Breed).HasMaxLength(50);

                entity.Property(e => e.HealthCondition)
                    .HasMaxLength(250)
                    .HasColumnName("Health_Condition");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OwnerId).HasColumnName("Owner_Id");

                //entity.HasOne(d => d.Owner)
                //    .WithMany(p => p.Cats)
                //    .HasForeignKey(d => d.OwnerId)
                //    .HasConstraintName("FK_ToOwner");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("Owner");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(75);

                entity.Property(e => e.Phone).HasMaxLength(12);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.Id);

                entity.Property(e => e.Duration).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
