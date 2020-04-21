using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SoundCloudLite.BLL.Contracts;
using SoundCloudLite.Domain;
using SoundCloudLite.DTO.DTO;

namespace SoundCloudLite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ITrackBll _trackBll;
        private readonly IArtistBll _artistBll;

        public TracksController(ITrackBll trackBll, IArtistBll artistBll)
        {
            _trackBll = trackBll;
            _artistBll = artistBll;
        }
        
        [HttpGet("{id}")]
        public ActionResult<TrackDto> Get(Guid id)
        {
            try
            {
                return Ok(new TrackDto().MapFromModel(
                    _trackBll.GetById(id)));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.Write(exception.Message);
                return NotFound();
            }
        }

        [HttpGet]
        public ActionResult<ICollection<TrackDto>> Get()
        {
            try
            {
                var artistId = new Guid(Request.Query.FirstOrDefault(r => r.Key == "ArtistId").Value.ToString());
                var artist = _artistBll.GetById(artistId);
                return Ok(artist.TrackList.Select(t => new TrackDto().MapFromModel(t)));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.Write(exception.Message);
                return NotFound();
            }
        }

        [HttpPut]
        public ActionResult<TrackDto> Put()
        {
            var artistId = new Guid(Request.Query.FirstOrDefault(r => r.Key == "ArtistId").Value.ToString());
            var trackId = new Guid(Request.Query.FirstOrDefault(r => r.Key == "TrackId").Value.ToString());

            try
            {
                var target = _artistBll.GetById(artistId);
                var track = _trackBll.GetById(trackId);
                return Ok(new TrackDto().MapFromModel(_trackBll.Copy(track, target)));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.WriteLine(exception);
                return NotFound();
            }
        }
    }
}