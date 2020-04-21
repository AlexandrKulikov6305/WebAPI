using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SoundCloudLite.BLL.Implementations;
using SoundCloudLite.DAL;
using SoundCloudLite.Domain;

namespace SoundCloudLite.BLL.Test
{
    [TestFixture]
    public class ArtistBllTests
    {
        [Test]
        public void ChangeGenreTest()
        {
            var repository = new Mock<IRepository>();
            var trackBll = new TrackBll(repository.Object);
            var artistBll = new ArtistBll(repository.Object, trackBll);
            var artist = new Artist("default", "default", "blues");
            artistBll.ChangeGenre(artist, "country");
            
            Assert.NotNull(artist.Genre);
            Assert.AreEqual(artist.Genre, "country");
        }

        [Test]
        public void MergeArtistWithItselfThrowsError()
        {
            var repository = new Mock<IRepository>();
            var trackBll = new TrackBll(repository.Object);
            var artistBll = new ArtistBll(repository.Object, trackBll);
            
            var duplicate = new Artist("one", "one", "one");
            var artistsToMerge = new List<Artist> {duplicate, 
                duplicate, 
                new Artist("two", "two", "two")};
            
            Assert.That(() => artistBll.MergeArtists(artistsToMerge, new Artist("target", "target", "target")), Throws.TypeOf<ArgumentException>());
        }
    }
}