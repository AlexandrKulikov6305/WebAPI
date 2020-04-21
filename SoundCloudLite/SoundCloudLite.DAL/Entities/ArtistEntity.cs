using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SoundCloudLite.Domain;

namespace SoundCloudLite.DAL.Entities
{
    [Table("Artists")]
    public class ArtistEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Genre { get; set; }
        public ICollection<TrackEntity> TrackList { get; set; } = new List<TrackEntity>();

        public ArtistEntity MapToEntity(Artist artist)
        {
            Id = artist.Id;
            Name = artist.Name;
            Country = artist.Country;
            Genre = artist.Genre;
            return this;
        }

        public Artist MapToModel()
        {
            var artist = new Artist(Name, Country, Genre) {Id = Id};
            ((List<Track>)artist.TrackList).AddRange(TrackList.Select(track => 
                track.MapToModel(artist)));
            return artist;
        }
    }
}