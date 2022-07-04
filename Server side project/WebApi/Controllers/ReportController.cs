using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi.Models;
using TestAssignment2020;
using WebApi.dto;


namespace MusicStoreServer.Controllers
{
    public class ReportController : ApiController
    {
        MusicStoreDbContext db = new MusicStoreDbContext();

        // GET api/values
        [HttpGet]
        [Route("api/Report")]
        public List<Report> Get()
        {
            List<Report> reports = new List<Report>();
            foreach (var artist in db.Artists)
            {
                var report = new Report();
                report.Artist = artist.Name;
                report.ArtistId = artist.ArtistId;

                report.ReleasedSongsNum = db.Albums.Join(db.Tracks,
                 album => album.AlbumId,
                 track => track.AlbumId,
                 (album, track) => new { Album = album, Track = track })
                 .Where(albumAndTrack => albumAndTrack.Album.AlbumId == albumAndTrack.Track.AlbumId)
                 .Where(albumAndTrack => albumAndTrack.Album.ArtistId == artist.ArtistId)
                 .ToList()
                 .Distinct()
                 .Count();

                reports.Add(report);
            }
            return reports;
        }


        [HttpGet]
        [Route("api/CreateReportByArtistName/{artistName}")]
        public List<Report> CreateReportByArtistName(string artistName)
        {
            List<Report> reports = new List<Report>();
            if (artistName.Contains('_'))
            {
                artistName = artistName.Replace('_', '/');
            }
            List<Artist> artists = db.Artists.Where(x => x.Name.StartsWith(artistName) == true).ToList();

            if (artists == null)   // if there is none artist name starts with artistName parameter
            {
                return null;
            }

            foreach (var artistWithPrefix in artists)
            {
                var report = new Report();
                report.Artist = artistWithPrefix.Name;
                report.ArtistId = artistWithPrefix.ArtistId;

                report.ReleasedSongsNum = db.Albums.Join(db.Tracks,
                album => album.AlbumId,
                track => track.AlbumId,
                (album, track) => new { Album = album, Track = track })
                .Where(albumAndTrack => albumAndTrack.Album.AlbumId == albumAndTrack.Track.AlbumId)
                .Where(albumAndTrack => albumAndTrack.Album.ArtistId == artistWithPrefix.ArtistId)
                .ToList().Distinct().Count();

                reports.Add(report);
            }
            return reports;
        }


        [HttpGet]
        [Route("api/CreateReportByMinIncomeTotal/{MinNumSongs}")]
        public List<Report> CreateReportByMinNumSongs(double MinNumSongs)
        {
            List<Report> reports = new List<Report>();
            var reportsNum =0;
            foreach (var artistsMinIncome in db.Artists)
            {
                var report = new Report();
                report.Artist = artistsMinIncome.Name;
                report.ArtistId = artistsMinIncome.ArtistId;

                var NumofSongs = db.Albums.Join(db.Tracks,
                album => album.AlbumId,
                track => track.AlbumId,
                (album, track) => new { Album = album, Track = track })
                .Where(albumAndTrack => albumAndTrack.Album.AlbumId == albumAndTrack.Track.AlbumId)
                .Where(albumAndTrack => albumAndTrack.Album.ArtistId == artistsMinIncome.ArtistId)
                .ToList().Distinct().Count();

                if(NumofSongs >= MinNumSongs)
                {
                    reportsNum++;
                    reports.Add(report);
                }
            }

            if (reportsNum == 0)
            {
                return null;
            }

            return reports;
        }


        [HttpGet]
        [Route("api/Report/GetAdditionalDetails/{artistName}/{rowTable}")]
        public List<AdditionalDetailsReportDto> GetAdditionalDetails(string artistName, int rowTable)
        {
            var artistId = rowTable - 2;      // because id count stars from row 3 in table
            if (artistName.Contains('_'))
            {
                artistName = artistName.Replace('_', '/');
            }

            //Invoices
            var AdditionalData = db.InvoiceLines.Select(x => new AdditionalDetailsReportDto()
            {
                ArtistId = artistId,
                ArtistName = artistName,
                customerName = x.Invoice.Customer.FirstName + " " + x.Invoice.Customer.LastName,
                customerEmail = x.Invoice.Customer.Email,
                invoiceDate = x.Invoice.InvoiceDate.Day.ToString() + "/" + x.Invoice.InvoiceDate.Month.ToString() + "/" + x.Invoice.InvoiceDate.Year.ToString(),
                total = (double)x.Invoice.Total
            }).ToList();

            return AdditionalData;
        }
    }
}

