using SoundCloudLite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoundCloudLite.DAL.Entities;

namespace SoundCloudLite.DAL
{
    public class Repository : IRepository
    {
        public Track GetTrackById(Guid id)
        {
            using (var context = new SoundCloudLiteContext())
            {
                return GetTrackById(id, context);
            }
        }
        
        private Track GetTrackById(Guid id, SoundCloudLiteContext context)
        {
            var result = context.Tracks.FirstOrDefault(track => track.Id == id);

            if (result == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            return result.MapToModel(GetArtistById(result.ArtistId));
        }
        
        public ICollection<Track> GetTracksByTitle(string title)
        {
            using (var context = new SoundCloudLiteContext())
            {
                return GetTracksByTitle(title, context);
            }
        }
        
        private ICollection<Track> GetTracksByTitle(string title, SoundCloudLiteContext context)
        {
            var tracks = (from track in context.Tracks 
                where track.Title.Equals(title) 
                select track.MapToModel(track.Artist.MapToModel())).ToList();

            if (tracks.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(title));
            }

            return tracks;
        }

        public ICollection<Track> GetTracksByAlbum(string album)
        {
            using (var context = new SoundCloudLiteContext())
            {
                return GetTracksByAlbum(album, context);
            }
        }
        
        private ICollection<Track> GetTracksByAlbum(string album, SoundCloudLiteContext context)
        {
            var tracks = (from track in context.Tracks 
                where track.Album.Equals(album) 
                select track.MapToModel(track.Artist.MapToModel())).ToList();

            if (tracks.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(album));
            }

            return tracks;
        }

        public ICollection<Track> GetTracksByArtist(Artist artist)
        {
            using (var context = new SoundCloudLiteContext())
            {
                return GetTracksByArtist(artist, context);
            }
        }
        
        private ICollection<Track> GetTracksByArtist(Artist artist, SoundCloudLiteContext context)
        {
            var tracks = (from track in context.Tracks 
                where track.Artist.Name.Equals(artist.Name) && 
                      track.Artist.Country.Equals(artist.Country) &&
                      track.Artist.Genre.Equals(artist.Genre)
                select track.MapToModel(track.Artist.MapToModel())).ToList();

            if (tracks.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(artist));
            }

            return tracks;
        }

        public Track Save(Track employee)
        {
            using (var context = new SoundCloudLiteContext())
            {
                var result = SaveTrack(employee, context);
                context.SaveChanges();
                return result;
            }
        }
        
        private Track SaveTrack(Track track, SoundCloudLiteContext context)
        {
            TrackEntity changingEntity;

            var artist = GetArtistEntityById(track.Artist.Id, context);
            if (track.Id != Guid.Empty)
            {
                changingEntity = context.Tracks.First(
                    x => x.Id == track.Id).MapToEntity(track, artist);
            }
            else
            {
                changingEntity = new TrackEntity().MapToEntity(track, artist);
                changingEntity.Id = Guid.NewGuid();
                context.Tracks.Add(changingEntity);
            }

            var model = changingEntity.MapToModel(artist.MapToModel());

            return model;
        }
        
        public ICollection<Track> Save(params Track[] tracks)
        {
            using (var context = new SoundCloudLiteContext())
            {
                var result = SaveTracks(tracks, context);
                context.SaveChanges();
                return result;
            }
        }
        
        private ICollection<Track> SaveTracks(ICollection<Track> tracks, 
            SoundCloudLiteContext context)
        {
            return tracks.Select(track => SaveTrack(track, context)).ToList();
        }

        private ArtistEntity GetArtistEntityById(Guid id, SoundCloudLiteContext context)
        {
            return context.Artists.Include(nameof(Artist.TrackList)).FirstOrDefault(
                x => x.Id == id);
        }

        public Artist GetArtistById(Guid id)
        {
            using (var context = new SoundCloudLiteContext())
            {
                return GetArtistById(id, context);
            }
        }

        private Artist GetArtistById(Guid id, SoundCloudLiteContext context)
        {
            var result = GetArtistEntityById(id, context);

            if (result == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            return result.MapToModel();
        }

        public ICollection<Artist> GetArtistsByGenre(string genre)
        {
            using (var context = new SoundCloudLiteContext())
            {
                return GetArtistsByGenre(genre, context);
            }
        }

        private ICollection<Artist> GetArtistsByGenre(string genre, SoundCloudLiteContext context)
        {
            var artists = context.Artists.Where(artist => artist.Genre.Equals(genre))
                .Include(nameof(Artist.TrackList)).ToArray()
                .Select(artist => artist.MapToModel()).ToList();

            if (artists.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(genre));
            }

            return artists;
            
        }

        public ICollection<Artist> GetArtistsByCountry(string country)
        {
            using (var context = new SoundCloudLiteContext())
            {
                return GetArtistsByCountry(country, context);
            }
        }

        private ICollection<Artist> GetArtistsByCountry(string country, SoundCloudLiteContext context)
        {
            var artists = (context.Artists.Where(artist => artist.Country.Equals(country))
                .Include(nameof(Artist.TrackList)).ToArray()
                .Select(artist => artist.MapToModel())).ToList();

            if (artists.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(country));
            }

            return artists;

        }

        public ICollection<Artist> GetAllArtists()
        {
            using (var context = new SoundCloudLiteContext())
            {
                return context.Artists.Include(nameof(Artist.TrackList)).ToArray().Select(
                    x => x.MapToModel()).ToArray();
            }
        }

        public Artist Save(Artist artist)
        {
            using (var context = new SoundCloudLiteContext())
            {
                var result = SaveArtist(artist, context);
                context.SaveChanges();
                return result;
            }
        }

        private Artist SaveArtist(Artist artist, SoundCloudLiteContext context)
        {
            ArtistEntity result;

            if (artist.Id != Guid.Empty)
            {
                result = context.Artists.First(x => x.Id == artist.Id).MapToEntity(artist);
            }
            else
            {
                result = new ArtistEntity().MapToEntity(artist);
                result.Id = Guid.NewGuid();
                context.Artists.Add(result);
            }

            return result.MapToModel();
        }

        public ICollection<Artist> Save(params Artist[] artists)
        {
            using (var context = new SoundCloudLiteContext())
            {
                var result = SaveArtists(artists, context);
                context.SaveChanges();
                return result;
            }
        }

        private ICollection<Artist> SaveArtists(ICollection<Artist> artists, 
            SoundCloudLiteContext context)
        {
            return artists.Select(artist => SaveArtist(artist, context)).ToList();
        }
    }
}