using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace BeachManage.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Playerstat> Playerstats { get; set; }

    public virtual DbSet<Stattype> Stattypes { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Teamplayer> Teamplayers { get; set; }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=beachvolleydb;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("matches");

            entity.HasIndex(e => e.TeamAid, "FK_Matches_TeamA");

            entity.HasIndex(e => e.TeamBid, "FK_Matches_TeamB");

            entity.HasIndex(e => e.TournamentId, "FK_Matches_Tournaments");

            entity.Property(e => e.DateDeleteFromApp).HasColumnType("datetime");
            entity.Property(e => e.DateInsert)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.MatchDate).HasColumnType("datetime");
            entity.Property(e => e.TeamAid).HasColumnName("TeamAId");
            entity.Property(e => e.TeamBid).HasColumnName("TeamBId");

            entity.HasOne(d => d.TeamA).WithMany(p => p.MatchTeamAs)
                .HasForeignKey(d => d.TeamAid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matches_TeamA");

            entity.HasOne(d => d.TeamB).WithMany(p => p.MatchTeamBs)
                .HasForeignKey(d => d.TeamBid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matches_TeamB");

            entity.HasOne(d => d.Tournament).WithMany(p => p.Matches)
                .HasForeignKey(d => d.TournamentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matches_Tournaments");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("players");

            entity.Property(e => e.DateDeleteFromApp).HasColumnType("datetime");
            entity.Property(e => e.DateInsert)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Playerstat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("playerstats");

            entity.HasIndex(e => e.MatchId, "FK_PlayerStats_Match");

            entity.HasIndex(e => e.PlayerId, "FK_PlayerStats_Player");

            entity.HasIndex(e => e.StatTypeId, "FK_PlayerStats_StatType");

            entity.Property(e => e.DateDeleteFromApp).HasColumnType("datetime");
            entity.Property(e => e.DateInsert)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Match).WithMany(p => p.Playerstats)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerStats_Match");

            entity.HasOne(d => d.Player).WithMany(p => p.Playerstats)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerStats_Player");

            entity.HasOne(d => d.StatType).WithMany(p => p.Playerstats)
                .HasForeignKey(d => d.StatTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerStats_StatType");
        });

        modelBuilder.Entity<Stattype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("stattypes");

            entity.HasIndex(e => e.Description, "Description").IsUnique();

            entity.Property(e => e.DateDeleteFromApp).HasColumnType("datetime");
            entity.Property(e => e.DateInsert)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("teams");

            entity.Property(e => e.DateDeleteFromApp).HasColumnType("datetime");
            entity.Property(e => e.DateInsert)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Teamplayer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("teamplayers");

            entity.HasIndex(e => e.Player1Id, "FK_TeamPlayers_Player1");

            entity.HasIndex(e => e.Player2Id, "FK_TeamPlayers_Player2");

            entity.HasIndex(e => new { e.TeamId, e.Player1Id, e.Player2Id }, "UQ_TeamPlayers").IsUnique();

            entity.Property(e => e.DateDeleteFromApp).HasColumnType("datetime");
            entity.Property(e => e.DateInsert)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Player1).WithMany(p => p.TeamplayerPlayer1s)
                .HasForeignKey(d => d.Player1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeamPlayers_Player1");

            entity.HasOne(d => d.Player2).WithMany(p => p.TeamplayerPlayer2s)
                .HasForeignKey(d => d.Player2Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeamPlayers_Player2");

            entity.HasOne(d => d.Team).WithMany(p => p.Teamplayers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeamPlayers_Team");
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tournaments");

            entity.Property(e => e.DateDeleteFromApp).HasColumnType("datetime");
            entity.Property(e => e.DateInsert)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
