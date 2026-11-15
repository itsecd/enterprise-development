using FitnessGym.Domain.Data;
using FitnessGym.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.EFCore.Data;

public class FitnessGymDbContext : DbContext
{
    public FitnessGymDbContext(DbContextOptions<FitnessGymDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<TrainingSession> TrainingSessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>().ToTable("Persons");
        modelBuilder.Entity<Client>().ToTable("Clients");
        modelBuilder.Entity<Trainer>().ToTable("Trainers");

        modelBuilder.Entity<Trainer>()
            .HasOne(t => t.Specialization)
            .WithMany()
            .HasForeignKey("SpecializationId");

        modelBuilder.Entity<TrainingSession>()
            .HasOne(ts => ts.Client)
            .WithMany()
            .HasForeignKey("ClientId");

        modelBuilder.Entity<TrainingSession>()
            .HasOne(ts => ts.Trainer)
            .WithMany()
            .HasForeignKey("TrainerId");

        var specializations = DataSeeder.Specializations
            .Select((s, index) => new
            {
                Id = index + 1,
                Name = s.Name
            }).ToArray();

        modelBuilder.Entity<Specialization>().HasData(specializations);

        var clients = DataSeeder.Clients
            .Select((c, index) => new
            {
                Id = index + 1,
                PassportNumber = c.PassportNumber,
                FullName = c.FullName,
                Gender = c.Gender,
                BirthDate = c.BirthDate,
                Phone = c.Phone,
                SubscriptionStart = c.SubscriptionStart,
                SubscriptionEnd = c.SubscriptionEnd
            }).ToArray();

        modelBuilder.Entity<Client>().HasData(clients);

        var trainers = DataSeeder.Trainers
            .Select((t, index) =>
            {
                var specIndex = DataSeeder.Specializations.ToList().FindIndex(s => s.Name == t.Specialization.Name);
                return new
                {
                    Id = 101 + index,
                    PassportNumber = t.PassportNumber,
                    FullName = t.FullName,
                    Gender = t.Gender,
                    BirthDate = t.BirthDate,
                    ExperienceYears = t.ExperienceYears,
                    SpecializationId = specIndex + 1
                };
            }).ToArray();

        modelBuilder.Entity<Trainer>().HasData(trainers);

        var sessions = DataSeeder.Sessions
            .Select((s, index) =>
            {
                var clientIndex = DataSeeder.Clients.ToList().FindIndex(c => c.PassportNumber == s.Client.PassportNumber);
                var trainerIndex = DataSeeder.Trainers.ToList().FindIndex(t => t.PassportNumber == s.Trainer.PassportNumber);

                return new
                {
                    Id = index + 1,
                    StartsTraining = s.StartsTraining,
                    EndsTraining = s.EndsTraining,
                    HallName = s.HallName,
                    IsTrial = s.IsTrial,
                    ClientId = clientIndex + 1,
                    TrainerId = 101 + trainerIndex
                };
            }).ToArray();

        modelBuilder.Entity<TrainingSession>().HasData(sessions);
    }
}