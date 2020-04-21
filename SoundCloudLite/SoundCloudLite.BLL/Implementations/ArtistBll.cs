using System;
using System.Collections.Generic;
using System.Linq;
using SoundCloudLite.BLL.Contracts;
using SoundCloudLite.DAL;
using SoundCloudLite.Domain;

namespace SoundCloudLite.BLL.Implementations
{
    public class ArtistBll : IArtistBll
    {
        private readonly ITrackBll _trackBll;

        private readonly IRepository _repository;

        public ArtistBll(IRepository repository, ITrackBll trackBll)
        {
            _repository = repository;
            _trackBll = trackBll;
        }
        
        public Artist Add(string name, string country, string genre)
        {
            var newArtist = new Artist(name, country, genre);
            return _repository.Save(newArtist);
        }

        public void ChangeGenre(Artist artist, string newGenre)
        {
            artist.Genre = newGenre;
            _repository.Save(artist);
        }

        public void MergeArtists(ICollection<Artist> artists, Artist newArtist)
        {
            var set = new HashSet<Artist>();
            var hasDuplicates = artists.Any(a => !set.Add(a));
            if (hasDuplicates)
            {
                throw new ArgumentException($"Set of merged artists should be unique.");
            }
            foreach (var artist in artists)
            {
                foreach (var track in artist.TrackList)
                {
                    _trackBll.Copy(track, newArtist);
                }
            }

            _repository.Save(newArtist);
        }

        public Artist GetById(Guid id)
        {
            return _repository.GetArtistById(id);
        }

        public ICollection<Artist> GetByGenre(string genre)
        {
            return _repository.GetArtistsByGenre(genre);
        }

        public ICollection<Artist> GetByCountry(string country)
        {
            return _repository.GetArtistsByCountry(country);
        }

        public ICollection<Artist> GetAll()
        {
            return _repository.GetAllArtists();
        }
    }
}