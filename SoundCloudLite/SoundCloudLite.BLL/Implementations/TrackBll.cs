using System;
using System.Collections.Generic;
using SoundCloudLite.BLL.Contracts;
using SoundCloudLite.DAL;
using SoundCloudLite.Domain;

namespace SoundCloudLite.BLL.Implementations
{
    public class TrackBll : ITrackBll
    {
        private readonly IRepository _repository;

        public TrackBll(IRepository repository)
        {
            _repository = repository;
        }

        public Track Add(string title, 
            string album,
            DateTime releaseDate,
            string embeddingHtml,
            Artist artist)
        {
            var newTrack = new Track(title, album, releaseDate, embeddingHtml) {Artist = artist};
            return _repository.Save(newTrack);
        }

        public Track Copy(Track track, Artist target)
        {
            var copy = track.Copy();
            copy.Artist = target;
            target.TrackList.Add(copy);
            _repository.Save(copy);
            return copy;
        }

        public void RefineReleaseDate(Track track, DateTime releaseDate)
        {
            track.ReleaseDate = releaseDate;
            _repository.Save(track);
        }

        public Track GetById(Guid id)
        {
            return _repository.GetTrackById(id);
        }

        public ICollection<Track> GetTracksByTitle(string title)
        {
            return _repository.GetTracksByTitle(title);
        }

        public ICollection<Track> GetTracksByAlbum(string album)
        {
            return _repository.GetTracksByAlbum(album);
        }

        public ICollection<Track> GetTracksByArtist(Artist artist)
        {
            return _repository.GetTracksByArtist(artist);
        }
    }
}