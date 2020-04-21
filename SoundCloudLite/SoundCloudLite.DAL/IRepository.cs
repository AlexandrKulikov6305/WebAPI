using System;
using System.Collections.Generic;
using SoundCloudLite.Domain;

namespace SoundCloudLite.DAL
{
    public interface IRepository
    {
        Track GetTrackById(Guid id);
        ICollection<Track> GetTracksByTitle(string title);
        ICollection<Track> GetTracksByAlbum(string album);
        ICollection<Track> GetTracksByArtist(Artist artist);
        Track Save(Track employee);
        ICollection<Track> Save(params Track[] tracks);

        Artist GetArtistById(Guid id);
        ICollection<Artist> GetArtistsByGenre(string genre);
        ICollection<Artist> GetArtistsByCountry(string country);
        ICollection<Artist> GetAllArtists();
        Artist Save(Artist artist);
        ICollection<Artist> Save(params Artist[] artists);
    }
}