using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVVM_Basics.Models
{
    public partial class MusicPlayerVpContext : DbContext
    {
        public MusicPlayerVpContext()
        {
        }

        public MusicPlayerVpContext(DbContextOptions<MusicPlayerVpContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddedAlbum> AddedAlbums { get; set; } = null!;
        public virtual DbSet<Album> Albums { get; set; } = null!;
        public virtual DbSet<AlbumArtist> AlbumArtists { get; set; } = null!;
        public virtual DbSet<AlbumSong> AlbumSongs { get; set; } = null!;
        public virtual DbSet<Artist> Artists { get; set; } = null!;
        public virtual DbSet<FollowingArtist> FollowingArtists { get; set; } = null!;
        public virtual DbSet<LikedSong> LikedSongs { get; set; } = null!;
        public virtual DbSet<Playlist> Playlists { get; set; } = null!;
        public virtual DbSet<PlaylistSong> PlaylistSongs { get; set; } = null!;
        public virtual DbSet<Song> Songs { get; set; } = null!;
        public virtual DbSet<SongArtist> SongArtists { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserPlay> UserPlays { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=MUSIC_PLAYER_VP;Integrated Security=True;TrustServerCertificate=True");
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["MusicPlayerDatabase"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddedAlbum>(entity =>
            {
                entity.HasKey(e => new { e.UsersId, e.AlbumId })
                    .HasName("PK__added_al__D1A9CC9063635206");

                entity.ToTable("added_album");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.Property(e => e.AlbumId).HasColumnName("album_id");

                entity.Property(e => e.AddedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("added_date");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.AddedAlbums)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__added_alb__album__46E78A0C");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.AddedAlbums)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__added_alb__users__45F365D3");
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("album");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("release_date");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<AlbumArtist>(entity =>
            {
                entity.HasKey(e => new { e.AlbumId, e.ArtistId })
                    .HasName("PK__album_ar__B62CD9B2F2CD0A01");

                entity.ToTable("album_artist");

                entity.Property(e => e.AlbumId).HasColumnName("album_id");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.AdditionalInfo)
                    .HasMaxLength(1)
                    .HasColumnName("additional_info");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.AlbumArtists)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__album_art__album__3A81B327");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.AlbumArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__album_art__artis__3B75D760");
            });

            modelBuilder.Entity<AlbumSong>(entity =>
            {
                entity.HasKey(e => new { e.SongId, e.AlbumId })
                    .HasName("PK__album_so__9E3BB3C7C5ACAC30");

                entity.ToTable("album_song");

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.Property(e => e.AlbumId).HasColumnName("album_id");

                entity.Property(e => e.AdditionalInfo)
                    .HasMaxLength(1)
                    .HasColumnName("additional_info");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.AlbumSongs)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__album_son__album__300424B4");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.AlbumSongs)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__album_son__song___2F10007B");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("artist");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<FollowingArtist>(entity =>
            {
                entity.HasKey(e => new { e.UsersId, e.ArtistId })
                    .HasName("PK__followin__EC6AD54B29B8D139");

                entity.ToTable("following_artist");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.FollowDate)
                    .HasColumnType("datetime")
                    .HasColumnName("follow_date");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.FollowingArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__following__artis__3F466844");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.FollowingArtists)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__following__users__3E52440B");
            });

            modelBuilder.Entity<LikedSong>(entity =>
            {
                entity.HasKey(e => new { e.UsersId, e.SongId })
                    .HasName("PK__liked_so__30F48BAAA0949733");

                entity.ToTable("liked_song");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.Property(e => e.AdditionalInfo)
                    .HasMaxLength(1)
                    .HasColumnName("additional_info");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.LikedSongs)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__liked_son__song___4316F928");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.LikedSongs)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__liked_son__users__4222D4EF");
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.ToTable("playlist");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Playlists)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__playlist__users___2C3393D0");
            });

            modelBuilder.Entity<PlaylistSong>(entity =>
            {
                entity.HasKey(e => new { e.SongId, e.PlaylistId, e.AddedDate })
                    .HasName("PK__playlist__BA8C6F5DA1AA1FE5");

                entity.ToTable("playlist_song");

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");

                entity.Property(e => e.AddedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("added_date");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.PlaylistSongs)
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__playlist___playl__37A5467C");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.PlaylistSongs)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__playlist___song___36B12243");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.ToTable("song");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LastestPlayDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastest_play_date");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.PcLink).HasColumnName("pc_link");

                entity.Property(e => e.Plays).HasColumnName("plays");

                entity.Property(e => e.StorageLink).HasColumnName("storage_link");
            });

            modelBuilder.Entity<SongArtist>(entity =>
            {
                entity.HasKey(e => new { e.SongId, e.ArtistId })
                    .HasName("PK__song_art__A3F8AA1C9233D7EA");

                entity.ToTable("song_artist");

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.AdditionalInfo)
                    .HasMaxLength(1)
                    .HasColumnName("additional_info");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.SongArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__song_arti__artis__33D4B598");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.SongArtists)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__song_arti__song___32E0915F");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("datetime")
                    .HasColumnName("join_date");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<UserPlay>(entity =>
            {
                entity.HasKey(e => new { e.UsersId, e.SongId })
                    .HasName("PK__user_pla__30F48BAAF89985C4");

                entity.ToTable("user_play");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.Property(e => e.LastestPlayDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastest_play_date");

                entity.Property(e => e.Play).HasColumnName("play");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.UserPlays)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_play__song___03F0984C");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.UserPlays)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_play__users__02FC7413");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
