using System;
using System.Collections.Generic;
using System.Linq;
using SoundCloudLite.Domain;
using Microsoft.EntityFrameworkCore;
using SoundCloudLite.DAL.Entities;

namespace SoundCloudLite.DAL
{
    public class SoundCloudLiteContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=SoundCloud;User=sa;Password=Pa55w0rd;Trusted_Connection=False;");
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TrackEntity>()
                .HasOne<ArtistEntity>(t => t.Artist)
                .WithMany(a => a.TrackList)
                .HasForeignKey("ArtistId");
            var artists = new List<Artist>
            {
                new Artist("Wildways", "Russia", "Post-hardcore") {Id = Guid.NewGuid()},
                new Artist("The Black Keys", "USA", "Blues-rock") {Id = Guid.NewGuid()}
            };
            var artistsEntity = artists.Select(a => new ArtistEntity().MapToEntity(a)).ToList();
            modelBuilder.Entity<ArtistEntity>().HasData(new ArtistEntity[]
                {
                    artistsEntity[0],
                    artistsEntity[1]
                }
            );

            //            modelBuilder.Entity<Track>().HasData(
            var wildwaysTracks = new List<Track>{
                new Track("Sky", 
                    "Day X", 
                    new DateTime(2018, 10, 5),
                    "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/419111404&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>") 
                    {Id = Guid.NewGuid(), Artist = artists[0]},
                new Track("Lost",
                    "Day X", 
                    new DateTime(2018, 10, 5),
                    "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/419115020&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>") 
                    {Id = Guid.NewGuid(), Artist = artists[0]}
            };
            var wildwaysTracksEntity =  wildwaysTracks.Select(t =>
                new TrackEntity().MapToEntity(t, artistsEntity[0])).ToList();
            modelBuilder.Entity<TrackEntity>().HasData(new TrackEntity[]{
                    wildwaysTracksEntity[0],
                    wildwaysTracksEntity[1]
                }
            );

            //            modelBuilder.Entity<Track>().HasData(
            var theBlackKeysTracks = new List<Track>{
                new Track("Run Right Back",
                    "El Camino",
                    new DateTime(2011, 7, 10), 
                    "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/28177029&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>") 
                    {Id = Guid.NewGuid(), Artist = artists[1]},
                new Track("In Time",
                    "Turn Blue",
                    new DateTime(2015, 9, 9), 
                    "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/255881185&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>") 
                    {Id = Guid.NewGuid(), Artist = artists[1]},
                new Track("Lo/Hi",
                    "Lo/Hi",
                    new DateTime(2019, 3, 2), 
                    "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/584680629&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>") 
                    {Id = Guid.NewGuid(), Artist = artists[1]}
            };
            var theBlackKeysTracksEntity = theBlackKeysTracks.Select(t =>
                new TrackEntity().MapToEntity(t, artistsEntity[1])).ToList();
            modelBuilder.Entity<TrackEntity>().HasData(new TrackEntity[]{
                theBlackKeysTracksEntity[0],
                theBlackKeysTracksEntity[1],
                theBlackKeysTracksEntity[2]
                }
            );
        }

        public DbSet<TrackEntity> Tracks { get; set; }
        public DbSet<ArtistEntity> Artists { get; set; }
    }
}