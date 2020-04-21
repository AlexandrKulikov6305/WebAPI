using System;
using Moq;
using NUnit.Framework;
using SoundCloudLite.BLL.Implementations;
using SoundCloudLite.DAL;
using SoundCloudLite.Domain;

namespace SoundCloudLite.BLL.Test
{
    [TestFixture]
    public class TrackBllTests
    {
        [Test]
        public void RefineReleaseDateTest()
        {
            var repository = new Mock<IRepository>();
            var trackBll = new TrackBll(repository.Object);
            var track = new Track("song", "album", DateTime.Today, "<></>");
            var expectedDate = new DateTime(1977, 5, 13);
            trackBll.RefineReleaseDate(track, expectedDate);
            Assert.AreEqual(track.ReleaseDate, expectedDate);
        }

        [Test]
        public void CopyTest()
        {
            var repository = new Mock<IRepository>();
            var trackBll = new TrackBll(repository.Object);
            var artistBll = new ArtistBll(repository.Object, trackBll);
            
            var track = new Track("song", "album", DateTime.Today, "<></>");
            var originalArtist = new Artist("original", "default", "default");
            originalArtist.TrackList.Add(track);
            
            var anotherArtist = new Artist("another", "default", "default");
            var copy = trackBll.Copy(track, anotherArtist);
            
            Assert.IsNotEmpty(originalArtist.TrackList);
            Assert.IsNotEmpty(anotherArtist.TrackList);
            Assert.True(originalArtist.TrackList.Contains(track));
            Assert.True(anotherArtist.TrackList.Contains(copy));
        }
    }
}