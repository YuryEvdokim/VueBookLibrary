using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VueBookLibrary.ServerApp.Models
{
    public class BookData
    {
        [Required]
        public string BookTitle { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string PublishingYear { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Download Count must be at least 0")]
        public int DownloadCount { get; set; }
        [Required]
        public string BookLink { get; set; }

        public Book ToBook() => new Book
        {
            BookTitle = this.BookTitle,
            Author = this.Author,
            PublishingYear = this.PublishingYear,
            DownloadCount = this.DownloadCount,
            BookLink = this.BookLink
        };
    }
}
