using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.dto
{
    public class AdditionalDetailsReportDto
    { 
        public int ArtistId;
        public string ArtistName;
        public string customerName;
        public string customerEmail;
        public int customerId;
        public string invoiceDate;
        public double total;
    }
}