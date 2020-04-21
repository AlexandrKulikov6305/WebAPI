using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoundCloudLite.Domain;

namespace SoundCloudLite.DAL.Entities
{
    [Table("Tracks")]
    public class TrackEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Album{ get; set; }
        
        public ArtistEntity Artist{ get; private set; }
        [ForeignKey("ArtistId")]
        public Guid ArtistId{ get; set; }
        public DateTime ReleaseDate { get; set; }
        public string EmbeddingHtml { get; set; }

        public TrackEntity MapToEntity(Track track, ArtistEntity artist)
        {
            Id = track.Id;
            Title = track.Title;
            Album = track.Album;
            ReleaseDate = track.ReleaseDate;
            EmbeddingHtml = track.EmbeddingHtml;
//            Artist = artist;
            ArtistId = artist.Id;

            return this;
        }

        public Track MapToModel(Artist artist)
        {
            return new Track(Title, Album, ReleaseDate, EmbeddingHtml) 
                {Id = Id, Artist = artist};
        }
    }
}
