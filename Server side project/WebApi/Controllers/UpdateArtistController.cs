using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DATA;
using TestAssignment2020;
using WebApi.dto;

namespace MusicStoreServer.Controllers
{
    // [Authorize]
    public class UpdateArtistController : ApiController
    {
        MusicStoreDbContext db = new MusicStoreDbContext();

        public List<UpdateArtistDto> GET()
        {
            return db.InvoiceLines.Select(x => new UpdateArtistDto()
            {
                artistId = x.Track.Album.ArtistId,
                totalIncome = (double)x.Invoice.Total,
                quantity = x.Quantity,
                customerName = x.Invoice.Customer.FirstName + " " + x.Invoice.Customer.LastName,
                invoiceDate = x.Invoice.InvoiceDate.Day.ToString() + "/" + x.Invoice.InvoiceDate.Month.ToString() + "/" + x.Invoice.InvoiceDate.Year.ToString()
            }).ToList();
        }


        // post request to update data when necessary 
        public HttpResponseMessage POST(int newQuantity, [FromBody] int Track_id)
        {
            int AlbumId = Convert.ToInt32(db.Tracks.Where(x => x.TrackId == Track_id));
            int ArtistId = Convert.ToInt32(db.Albums.Select(x => x.AlbumId == AlbumId));
            Artist a = db.Artists.SingleOrDefault(x => x.ArtistId == ArtistId);

            if (a == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "The artist was not found");
            }

            try
            {
                db.SaveChanges();
            }

            catch (DbUpdateConcurrencyException e)
            {
                var ctx = ((IObjectContextAdapter)db).ObjectContext;
                foreach (var et in e.Entries)
                {
                    //client win
                    ctx.Refresh(System.Data.Entity.Core.Objects.RefreshMode.ClientWins, et.Entity);

                    //store win
                    //ctx.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, et.Entity);
                }
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "The data was updated!");
            }

            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotImplemented, "General exception");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "The update done successfully!");
        }
    }
}