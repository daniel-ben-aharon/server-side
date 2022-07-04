using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TestAssignment2020;
using WebApi.dto;

namespace MusicStoreServer.Controllers
{
    // [Authorize]
    public class PurchaseSearchController : ApiController
    {
        MusicStoreDbContext db = new MusicStoreDbContext();

        //GET api/<controller>
        [HttpGet]
        public List<PurchaseDetailsDto> Get()
        {
            return db.Tracks.Select(x => new PurchaseDetailsDto()
            {
                ArtistId = x.Album.Artist.ArtistId,
                ArtistName = x.Album.Artist.Name,
                AlbumName = x.Album.Title,
                GenreName = x.Genre.Name ,
                TrackName = x.Name,
                TrackId = x.TrackId,
                UnitPrice = (double)(x.UnitPrice)
            }).ToList();
        }

        [HttpGet]
        [Route("api/FilterByArtistName/{artistName}")]
        public List<PurchaseDetailsDto> FilterByArtistName(string artistName)
        {
            if (artistName.Contains('_'))
            {
                artistName = artistName.Replace('_', '/');
            }
            return db.Tracks.Select(x => new PurchaseDetailsDto()
            {
                ArtistId = x.Album.Artist.ArtistId,
                ArtistName = x.Album.Artist.Name,
                AlbumName = x.Album.Title,
                GenreName = x.Genre.Name,
                TrackName = x.Name,
                UnitPrice = (double)(x.UnitPrice)
            })
            .Where(t => t.ArtistName.StartsWith(artistName))
            .ToList();
        }

        [HttpGet]
        [Route("api/FilterByGenre/{genre}")]
        public List<PurchaseDetailsDto> FilterByGenre(string genre)
        {
            if (genre.Contains('_'))
            {
                genre = genre.Replace('_', '/');
            }
            return db.Tracks.Select(x => new PurchaseDetailsDto()
            {
                ArtistId = x.Album.Artist.ArtistId,
                ArtistName = x.Album.Artist.Name,
                AlbumName = x.Album.Title,
                GenreName = x.Genre.Name,
                TrackName = x.Name,
                UnitPrice = (double)(x.UnitPrice)
            })
            .Where(t => t.GenreName.StartsWith(genre))
            .ToList();
        }

        [HttpGet]
        [Route("api/FilterByAlbumName/{albumName}")]
        public List<PurchaseDetailsDto> FilterByAlbumName(string albumName)
        {
            if (albumName.Contains('_'))
            {
                albumName = albumName.Replace('_', '/');
            }
            return db.Tracks.Select(x => new PurchaseDetailsDto()
            {
                ArtistId = x.Album.Artist.ArtistId,
                ArtistName = x.Album.Artist.Name,
                AlbumName = x.Album.Title,
                GenreName = x.Genre.Name,
                TrackName = x.Name,
                UnitPrice = (double)(x.UnitPrice)
            })
            .Where(t => t.AlbumName.StartsWith(albumName))
            .ToList();
        }

        [HttpGet]
        [Route("api/FilterByTrackName/{trackName}")]
        public List<PurchaseDetailsDto> FilterByTrackName(string trackName)
        {
            if (trackName.Contains('_'))
            {
                trackName = trackName.Replace('_', '/');
            }
            return db.Tracks.Select(x => new PurchaseDetailsDto()
            {
                ArtistId = x.Album.Artist.ArtistId,
                ArtistName = x.Album.Artist.Name,
                AlbumName = x.Album.Title,
                GenreName = x.Genre.Name,
                TrackName = x.Name,
                UnitPrice = (double)(x.UnitPrice)
            })
            .Where(t => t.TrackName.StartsWith(trackName))
            .ToList();
        }



        //public List<PurchaseDetailsDto> FilterSearch(string name)
        //{
        //    if (name.Contains('_'))
        //    {
        //        name = name.Replace('_', '/');
        //    }

        //    return db.Tracks.Select(x => new PurchaseDetailsDto()
        //    {
        //        ArtistId = x.Album.Artist.ArtistId,
        //        ArtistName = x.Album.Artist.Name,
        //        AlbumName = x.Album.Title,
        //        GenreName = x.Genre.Name,
        //        TrackName = x.Name,
        //        UnitPrice = (double)(x.UnitPrice)
        //    })
        //    .Where(t => t.ArtistName.StartsWith(name))
        //    .ToList();
        //}
    }
}