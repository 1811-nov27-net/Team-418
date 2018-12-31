using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess
{
    public partial class _1811proj1_5Context : DbContext
    {
        public _1811proj1_5Context()
        {
        }

        public _1811proj1_5Context(DbContextOptions<_1811proj1_5Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AlbumSongs> AlbumSongs { get; set; }
        public virtual DbSet<Albums> Albums { get; set; }
        public virtual DbSet<Artists> Artists { get; set; }
        public virtual DbSet<Covers> Covers { get; set; }
        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<PendingRequests> PendingRequests { get; set; }
        public virtual DbSet<Songs> Songs { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<AlbumSongs>(entity =>
            {
                entity.HasKey(e => e.AsId)
                    .HasName("PK_AlbumSongs_ID");

                entity.ToTable("AlbumSongs", "M");

                entity.Property(e => e.AsId).HasColumnName("AS_id");

                entity.Property(e => e.AsAlbum).HasColumnName("AS_album");

                entity.Property(e => e.AsSong).HasColumnName("AS_song");

                entity.HasOne(d => d.AsAlbumNavigation)
                    .WithMany(p => p.AlbumSongs)
                    .HasForeignKey(d => d.AsAlbum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlbumSongs_Albums_ID");

                entity.HasOne(d => d.AsSongNavigation)
                    .WithMany(p => p.AlbumSongs)
                    .HasForeignKey(d => d.AsSong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlbumSongs_Songs_ID");
            });

            modelBuilder.Entity<Albums>(entity =>
            {
                entity.HasKey(e => e.AlId)
                    .HasName("PK_Albums_ID");

                entity.ToTable("Albums", "M");

                entity.Property(e => e.AlId).HasColumnName("AL_id");

                entity.Property(e => e.AlArtist).HasColumnName("AL_artist");

                entity.Property(e => e.AlGenre)
                    .HasColumnName("AL_genre")
                    .HasMaxLength(25);

                entity.Property(e => e.AlName)
                    .IsRequired()
                    .HasColumnName("AL_name")
                    .HasMaxLength(50);

                entity.Property(e => e.AlRelease).HasColumnName("AL_release");

                entity.HasOne(d => d.AlArtistNavigation)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.AlArtist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Albums_Artists_ID");
            });

            modelBuilder.Entity<Artists>(entity =>
            {
                entity.HasKey(e => e.ArId)
                    .HasName("PK_Artists_ID");

                entity.ToTable("Artists", "M");

                entity.Property(e => e.ArId).HasColumnName("AR_id");

                entity.Property(e => e.ArCity)
                    .HasColumnName("AR_city")
                    .HasMaxLength(25);

                entity.Property(e => e.ArCountry)
                    .HasColumnName("AR_country")
                    .HasMaxLength(25);

                entity.Property(e => e.ArFormed).HasColumnName("AR_formed");

                entity.Property(e => e.ArLatestrelease).HasColumnName("AR_latestrelease");

                entity.Property(e => e.ArName)
                    .IsRequired()
                    .HasColumnName("AR_name")
                    .HasMaxLength(50);

                entity.Property(e => e.ArStateprovince)
                    .HasColumnName("AR_stateprovince")
                    .HasMaxLength(25);

                entity.Property(e => e.ArWebsite)
                    .HasColumnName("AR_website")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Covers>(entity =>
            {
                entity.HasKey(e => e.CId)
                    .HasName("PK_Covers_ID");

                entity.ToTable("Covers", "M");

                entity.Property(e => e.CId).HasColumnName("C_id");

                entity.Property(e => e.CCover).HasColumnName("C_cover");

                entity.Property(e => e.COriginal).HasColumnName("C_original");

                entity.HasOne(d => d.CCoverNavigation)
                    .WithMany(p => p.CoversCCoverNavigation)
                    .HasForeignKey(d => d.CCover)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlbumSongs_Songs_CoverID");

                entity.HasOne(d => d.COriginalNavigation)
                    .WithMany(p => p.CoversCOriginalNavigation)
                    .HasForeignKey(d => d.COriginal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlbumSongs_Songs_OriginalID");
            });

            modelBuilder.Entity<Favorites>(entity =>
            {
                entity.HasKey(e => e.FId)
                    .HasName("PK_Favorites_ID");

                entity.ToTable("Favorites", "M");

                entity.Property(e => e.FId).HasColumnName("F_id");

                entity.Property(e => e.FSong).HasColumnName("F_song");

                entity.Property(e => e.FUser).HasColumnName("F_user");

                entity.HasOne(d => d.FSongNavigation)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.FSong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Favorites_Songs_ID");

                entity.HasOne(d => d.FUserNavigation)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.FUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Favorites_Users_ID");
            });

            modelBuilder.Entity<PendingRequests>(entity =>
            {
                entity.HasKey(e => e.PrId)
                    .HasName("PK_PendingRequests_ID");

                entity.ToTable("PendingRequests", "M");

                entity.Property(e => e.PrId).HasColumnName("PR_id");

                entity.Property(e => e.PrAlbumid).HasColumnName("PR_albumid");

                entity.Property(e => e.PrAlbumname)
                    .HasColumnName("PR_albumname")
                    .HasMaxLength(50);

                entity.Property(e => e.PrArtistid).HasColumnName("PR_artistid");

                entity.Property(e => e.PrArtistname)
                    .IsRequired()
                    .HasColumnName("PR_artistname")
                    .HasMaxLength(50);

                entity.Property(e => e.PrSongname)
                    .IsRequired()
                    .HasColumnName("PR_songname")
                    .HasMaxLength(50);

                entity.HasOne(d => d.PrAlbum)
                    .WithMany(p => p.PendingRequests)
                    .HasForeignKey(d => d.PrAlbumid)
                    .HasConstraintName("FK_PendingRequests_Albums_ID");

                entity.HasOne(d => d.PrArtist)
                    .WithMany(p => p.PendingRequests)
                    .HasForeignKey(d => d.PrArtistid)
                    .HasConstraintName("FK_PendingRequests_Artists_ID");
            });

            modelBuilder.Entity<Songs>(entity =>
            {
                entity.HasKey(e => e.SId)
                    .HasName("PK_Songs_ID");

                entity.ToTable("Songs", "M");

                entity.Property(e => e.SId).HasColumnName("S_id");

                entity.Property(e => e.SArtist).HasColumnName("S_artist");

                entity.Property(e => e.SCover).HasColumnName("S_cover");

                entity.Property(e => e.SGenre)
                    .HasColumnName("S_genre")
                    .HasMaxLength(25);

                entity.Property(e => e.SInitialrelease).HasColumnName("S_initialrelease");

                entity.Property(e => e.SLength).HasColumnName("S_length");

                entity.Property(e => e.SLink)
                    .HasColumnName("S_link")
                    .HasMaxLength(200);

                entity.Property(e => e.SName)
                    .IsRequired()
                    .HasColumnName("S_name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.SArtistNavigation)
                    .WithMany(p => p.Songs)
                    .HasForeignKey(d => d.SArtist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Songs_Artists_ID");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK_Users_ID");

                entity.ToTable("Users", "M");

                entity.Property(e => e.UId).HasColumnName("U_id");

                entity.Property(e => e.UAdmin).HasColumnName("U_admin");

                entity.Property(e => e.UName)
                    .IsRequired()
                    .HasColumnName("U_name")
                    .HasMaxLength(25);
            });
        }
    }
}
