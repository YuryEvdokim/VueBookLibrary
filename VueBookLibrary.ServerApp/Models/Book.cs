using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueBookLibrary.ServerApp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string PublishingYear { get; set; }
        public int DownloadCount { get; set; }
        public string BookLink { get; set; }
    }
}
