using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
        public class Report
        {
            public int ArtistId { get; set; }

            public string Artist { get; set; }

            public double TotalIncome { get; set; }

            public int ReleasedSongsNum { get; set; }
        }
}