using SoundCloudLite.Domain;
using System;
using System.Runtime.Serialization;

namespace SoundCloudLite.DTO.DTO
{
    [DataContract]
    public class TrackDto
    {
        [DataMember]
        public Guid Id { get; set; } = Guid.Empty;
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Album{ get; set; }
        [DataMember]
        public DateTime ReleaseDate { get; set; }
        [DataMember]
        public string EmbeddingHtml { get; set; }

        public TrackDto MapFromModel(Track track)
        {
            Id = track.Id;
            Title = track.Title;
            Album = track.Album;
            ReleaseDate = track.ReleaseDate;
            EmbeddingHtml = track.EmbeddingHtml;

            return this;
        }

        public Track MapToModel(Artist artist)
        {
            var track = new Track(Title, Album, ReleaseDate, EmbeddingHtml) {Artist = artist};
            return track;
        }
    }
}