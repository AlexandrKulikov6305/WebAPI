using System;
using System.Collections.Generic;

namespace SoundCloudLite.Domain
{
    public class Artist
    {
        public Guid Id { get; set; }
        public string Name { get; }
        public string Country { get; }
        public string Genre { get; set; }

        public ICollection<Track> TrackList { get; } = new List<Track>();

        public Artist(string name, string country, string genre)
        {
            Name = name;
            Country = country;
            Genre = genre;
        }
    }
}