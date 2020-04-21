using System;

namespace SoundCloudLite.Domain
{
    public class Track
    {
        public Guid Id { get; set; }
        public string Title { get; }
        public string Album{ get; }
        public Artist Artist{ get; set; }
        public DateTime ReleaseDate { get; set; }
        public string EmbeddingHtml { get; }

        public Track(string title, string album, DateTime releaseDate, string embeddingHtml)
        {
            Title = title;
            Album = album;
            ReleaseDate = releaseDate;
            EmbeddingHtml = embeddingHtml;
        }

        public Track Copy()
        {
           return new Track(Title, Album, ReleaseDate, EmbeddingHtml);;
        }
    }
}