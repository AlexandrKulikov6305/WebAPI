using System;
using System.Collections.Generic;
using System.Linq;
using SoundCloudLite.DAL.Entities;
using SoundCloudLite.Domain;

namespace SoundCloudLite.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SoundCloudLiteContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SoundCloudLiteContext context)
        {
            var artists = new List<Artist>
            {
                new Artist("Wildways", "Russia", "Post-hardcore"),
                new Artist("The Black Keys", "USA", "Blues-rock")
            };
            var artistsEntity = artists.Select(a => new ArtistEntity().MapToEntity(a)).ToList();
            context.Artists.AddRange(artistsEntity);

            var wildwaysTracks = new List<Track>
            {
                new Track("Sky", 
                    "Day X", 
                    new DateTime(2018, 10, 5),
                    "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/419111404&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>"),
                new Track("Lost",
                    "Day X", 
                    new DateTime(2018, 10, 5),
                    "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/419115020&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>")
            };
            context.Tracks.AddRange(wildwaysTracks.Select(t =>
                new TrackEntity().MapToEntity(t, artistsEntity[0])));
            
            var theBlackKeysTracks = new List<Track>
            {
                new Track("Run Right Back",
                    "El Camino",
                    new DateTime(2011, 7, 10), 
                    "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/28177029&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>"),
                new Track("In Time",
                    "Turn Blue",
                    new DateTime(2015, 9, 9), 
                    "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/255881185&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>"),
                new Track("Lo/Hi",
                    "Lo/Hi",
                    new DateTime(2019, 3, 2), 
                    "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/584680629&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>")
            };
            context.Tracks.AddRange(theBlackKeysTracks.Select(t =>
                new TrackEntity().MapToEntity(t, artistsEntity[1])));

            base.Seed(context);
        }
    }
}