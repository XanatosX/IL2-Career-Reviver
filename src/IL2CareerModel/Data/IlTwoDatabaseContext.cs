using IL2CareerModel.Models;
using IL2CareerModel.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace IL2CareerModel.Data;

public partial class IlTwoDatabaseContext : DbContext
{
    private readonly string? databaseConnectionString;

    public IlTwoDatabaseContext(DbContextOptions<IlTwoDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ace> Aces { get; set; }

    public virtual DbSet<Award> Awards { get; set; }

    public virtual DbSet<Career> Careers { get; set; }

    public virtual DbSet<Config> Configs { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<Personage> Personages { get; set; }

    public virtual DbSet<Pilot> Pilots { get; set; }

    public virtual DbSet<Plane> Planes { get; set; }

    public virtual DbSet<Sortie> Sorties { get; set; }

    public virtual DbSet<Squadron> Squadrons { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ace>(entity =>
        {
            entity.Property(e => e.CareerId).HasDefaultValueSql("- 1");
            entity.Property(e => e.InsDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Award>(entity =>
        {
            entity.Property(e => e.CareerId).HasDefaultValueSql("-1");
            entity.Property(e => e.InsDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.PersonageAwardId).HasDefaultValueSql("'UPPER(UUID())'");
            entity.Property(e => e.PersonageId).HasDefaultValueSql("''");
            entity.Property(e => e.X).HasDefaultValueSql("'0.0'");
            entity.Property(e => e.Y).HasDefaultValueSql("'0.0'");
        });

        modelBuilder.Entity<Career>(entity =>
        {
            entity.Property(e => e.InsDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Config>(entity =>
        {
            entity.Property(e => e.InsDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.CareerId).HasDefaultValueSql("-1");
            entity.Property(e => e.Insdate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Ipar1).HasDefaultValueSql("-1");
            entity.Property(e => e.Ipar2).HasDefaultValueSql("-1");
            entity.Property(e => e.Ipar3).HasDefaultValueSql("-1");
            entity.Property(e => e.Ipar4).HasDefaultValueSql("-1");
            entity.Property(e => e.MissionId).HasDefaultValueSql("-1");
            entity.Property(e => e.PilotId).HasDefaultValueSql("-1");
            entity.Property(e => e.SquadronId).HasDefaultValueSql("-1");
            entity.Property(e => e.Tpar1).HasDefaultValueSql("''");
            entity.Property(e => e.Tpar2).HasDefaultValueSql("''");
            entity.Property(e => e.Tpar3).HasDefaultValueSql("''");
            entity.Property(e => e.Tpar4).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.Property(e => e.CareerId).HasDefaultValueSql("-1");
            entity.Property(e => e.InsDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.Property(e => e.CareerId).HasDefaultValueSql("-1");
            entity.Property(e => e.Fuel0).HasDefaultValueSql("1");
            entity.Property(e => e.Fuel1).HasDefaultValueSql("1");
            entity.Property(e => e.Fuel2).HasDefaultValueSql("1");
            entity.Property(e => e.Fuel3).HasDefaultValueSql("1");
            entity.Property(e => e.Fuel4).HasDefaultValueSql("1");
            entity.Property(e => e.Fuel5).HasDefaultValueSql("1");
            entity.Property(e => e.Fuel6).HasDefaultValueSql("1");
            entity.Property(e => e.Fuel7).HasDefaultValueSql("1");
            entity.Property(e => e.Fuel8).HasDefaultValueSql("1");
            entity.Property(e => e.InsDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.MTemplate).HasDefaultValueSql("''");
            entity.Property(e => e.Modificationsrequired0).HasDefaultValueSql("1");
            entity.Property(e => e.Modificationsrequired1).HasDefaultValueSql("1");
            entity.Property(e => e.Modificationsrequired2).HasDefaultValueSql("1");
            entity.Property(e => e.Modificationsrequired3).HasDefaultValueSql("1");
            entity.Property(e => e.Modificationsrequired4).HasDefaultValueSql("1");
            entity.Property(e => e.Modificationsrequired5).HasDefaultValueSql("1");
            entity.Property(e => e.Modificationsrequired6).HasDefaultValueSql("1");
            entity.Property(e => e.Modificationsrequired7).HasDefaultValueSql("1");
            entity.Property(e => e.Modificationsrequired8).HasDefaultValueSql("1");
            entity.Property(e => e.Pilot0).HasDefaultValueSql("-1");
            entity.Property(e => e.Pilot1).HasDefaultValueSql("-1");
            entity.Property(e => e.Pilot2).HasDefaultValueSql("-1");
            entity.Property(e => e.Pilot3).HasDefaultValueSql("-1");
            entity.Property(e => e.Pilot4).HasDefaultValueSql("-1");
            entity.Property(e => e.Pilot5).HasDefaultValueSql("-1");
            entity.Property(e => e.Pilot6).HasDefaultValueSql("-1");
            entity.Property(e => e.Pilot7).HasDefaultValueSql("-1");
            entity.Property(e => e.Pilot8).HasDefaultValueSql("-1");
            entity.Property(e => e.Plane0).HasDefaultValueSql("' '");
            entity.Property(e => e.Plane1).HasDefaultValueSql("' '");
            entity.Property(e => e.Plane2).HasDefaultValueSql("' '");
            entity.Property(e => e.Plane3).HasDefaultValueSql("' '");
            entity.Property(e => e.Plane4).HasDefaultValueSql("' '");
            entity.Property(e => e.Plane5).HasDefaultValueSql("' '");
            entity.Property(e => e.Plane6).HasDefaultValueSql("' '");
            entity.Property(e => e.Plane7).HasDefaultValueSql("' '");
            entity.Property(e => e.Plane8).HasDefaultValueSql("' '");
            entity.Property(e => e.SquadronId).HasDefaultValueSql("-1");
            entity.Property(e => e.Subtype).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Personage>(entity =>
        {
            entity.Property(e => e.Insdate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Pilot>(entity =>
        {
            entity.Property(e => e.Ailevel).HasDefaultValueSql("1");
            entity.Property(e => e.InsDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Penalty).HasDefaultValueSql("''");
        });

        modelBuilder.Entity<Plane>(entity =>
        {
            entity.Property(e => e.InsDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Sortie>(entity =>
        {
            entity.Property(e => e.FlightTime).HasDefaultValueSql("0");
            entity.Property(e => e.InsDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("0");
            entity.Property(e => e.MissionId).HasDefaultValueSql("-1");
            entity.Property(e => e.PilotId).HasDefaultValueSql("-1");
        });

        modelBuilder.Entity<Squadron>(entity =>
        {
            entity.Property(e => e.InsDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
