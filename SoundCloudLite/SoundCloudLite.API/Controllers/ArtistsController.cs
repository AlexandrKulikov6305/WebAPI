using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoundCloudLite.BLL.Contracts;
using SoundCloudLite.Domain;
using SoundCloudLite.DTO.DTO;

namespace SoundCloudLite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        public enum SearchKeys : int
        {
            Genre,
            Country
        }
        private readonly IArtistBll _artistBll;

        public ArtistsController(IArtistBll artistBll)
        {
            _artistBll = artistBll;
        }
        
        [HttpGet]
        public ActionResult<ICollection<ArtistDto>> Get()
        {
            return Ok(_artistBll.GetAll().Select(a => new ArtistDto().MapFromModel(a)).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<ArtistDto> Get(Guid id)
        {
            try
            {
                return Ok(new ArtistDto().MapFromModel(
                    _artistBll.GetById(id)));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.Write(exception.Message);
                return NotFound();
            }
        }

        [HttpGet("{key}&{value}")]
        public ActionResult<ICollection<ArtistDto>> Get(SearchKeys key, string value)
        {
            try
            {
                switch (key)
                {
                    case SearchKeys.Genre:
                        return Ok(_artistBll.GetByGenre(value).Select(a => new ArtistDto().MapFromModel(a)).ToList());
                    case SearchKeys.Country:
                        return Ok(_artistBll.GetByCountry(value).Select(a => new ArtistDto().MapFromModel(a)).ToList());
                    default:
                        throw new ArgumentOutOfRangeException(nameof(key), key, null);
                }
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.Write(exception.Message);
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<ArtistDto> Post()
        {
            var name = Request.Query.FirstOrDefault(r => r.Key == "name").Value;
            var country = Request.Query.FirstOrDefault(r => r.Key == "country").Value;
            var genre = Request.Query.FirstOrDefault(r => r.Key == "genre").Value;
            return Ok(new ArtistDto().MapFromModel(_artistBll.Add(name, country, genre)));
        }

        [HttpPut("{id}")]
        public ActionResult<ArtistDto> Put(Guid id)
        {
            try
            {
                var artist = _artistBll.GetById(id);
                var genre = Request.Query.FirstOrDefault(r => r.Key == "genre").Value;
                _artistBll.ChangeGenre(artist, genre);
                return Ok(new ArtistDto().MapFromModel(artist));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.Write(exception.Message);
                return NotFound();
            }
        }
    }
}