using System;
using System.Collections.Generic;
using SoundCloudLite.Domain;

namespace SoundCloudLite.BLL.Contracts
{
    public interface ITrackBll
    {
        /// <summary>
        /// Appends new track to <paramref name="artist"/>'s repertoire.
        /// </summary>
        Track Add(string title, 
            string album, 
            DateTime releaseDate, 
            string embeddingHtml, 
            Artist artist);

        /// <summary>
        /// Copy track to track list of another artist.
        /// </summary>
        Track Copy(Track track, Artist target);

        /// <summary>
        /// Updates release date of specified track. 
        /// </summary>
        void RefineReleaseDate(Track track, DateTime releaseDate);

        Track GetById(Guid id);
        
        ICollection<Track> GetTracksByTitle(string title);

        ICollection<Track> GetTracksByAlbum(string album);

        ICollection<Track> GetTracksByArtist(Artist artist);
    }
}