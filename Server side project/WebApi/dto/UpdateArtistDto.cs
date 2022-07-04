using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.dto
{
    public class UpdateArtistDto
    {
        public int artistId;
        public double totalIncome;
        public int quantity;
        public string customerName;
        public string invoiceDate;
    }
}