using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SoundCloudLite.Domain;

namespace SoundCloudLite.DTO.DTO
{
    [DataContract]
    public class ArtistDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Genre { get; set; }
        [DataMember]
        public ICollection<TrackDto> TrackList { get; set; } = new List<TrackDto>();

        public ArtistDto MapFromModel(Artist artist)
        {
            var tracks = artist.TrackList.Select(track => 
                new TrackDto().MapFromModel(track)).ToList();
            TrackList = tracks;
            
            Id = artist.Id;
            Name = artist.Name;
            Country = artist.Country;
            Genre = artist.Genre;

            return this;
        }

        public Artist MapToModel()
        {
            var artist = new Artist(Name, Country, Genre) { Id = Id };
            ((List<Track>)artist.TrackList).AddRange(TrackList.Select(track => 
                track.MapToModel(artist)));
            return artist;
        }
    }
}