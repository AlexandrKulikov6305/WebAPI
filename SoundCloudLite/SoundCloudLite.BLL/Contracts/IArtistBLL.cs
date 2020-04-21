using System;
using System.Collections.Generic;
using SoundCloudLite.Domain;

namespace SoundCloudLite.BLL.Contracts
{
    public interface IArtistBll
    {
        /// <summary>
        /// Creates new Artist.
        /// </summary>
        Artist Add(string name, string country, string genre);

        /// <summary>
        /// Changes genre of <paramref name="artist"/> to <paramref name="newGenre"/>
        /// </summary>
        void ChangeGenre(Artist artist, string newGenre);

        /// <summary>
        /// Merges set of artists to one new (e.g. group formation).
        /// Also maybe useful if one artist has several different pseudonyms.
        /// </summary>
        /// <param name="artists">Artists that need to be combined.</param>
        /// <param name="newArtist">Artist with new name, country and genre.</param>
        void MergeArtists(ICollection<Artist> artists, Artist newArtist);

        Artist GetById(Guid id);

        ICollection<Artist> GetByGenre(string genre);

        ICollection<Artist> GetByCountry(string country);
        
        ICollection<Artist> GetAll();
    }
}