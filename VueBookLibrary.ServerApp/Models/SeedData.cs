using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VueBookLibrary.ServerApp.Models
{
    public class SeedData
    {
		public static void SeedDatabase(DataContext context)
		{
			context.Database.Migrate();
			if (context.Books.Count() == 0)
			{
				context.Books.AddRange(
					new Book
					{
						BookTitle = "Book1",
						Author = "Author1",
						PublishingYear = "1991",
						DownloadCount = 0,
						BookLink = "https://localhost:44390/files/Book1.pdf"
					},
					new Book
					{
						BookTitle = "Book2",
						Author = "Author2",
						PublishingYear = "1992",
						DownloadCount = 0,
						BookLink = "https://localhost:44390/files/Book2.pdf"
					},
					new Book
					{
						BookTitle = "Book3",
						Author = "Author3",
						PublishingYear = "1993",
						DownloadCount = 0,
						BookLink = "https://localhost:44390/files/Book3.pdf"
					},
					new Book
					{
						BookTitle = "Book4",
						Author = "Author4",
						PublishingYear = "1994",
						DownloadCount = 0,
						BookLink = "https://localhost:44390/files/Book4.pdf"
					}
				);

				context.SaveChanges();
			}

		}
	}
}
