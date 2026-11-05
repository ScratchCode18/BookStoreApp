using Microsoft.AspNetCore.Mvc;
using BookStoreApp.Models;
using BookStoreApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreApp.Controllers
{
    public class HomeController : Controller
    {
        // Static in-memory lists to persist data across requests
        private static List<Author> Authors = new List<Author>
        {
            new Author { Id = 1, Name = "George Orwell", Country = "United Kingdom" },
            new Author { Id = 2, Name = "J.K. Rowling", Country = "United Kingdom" }
        };

        private static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "1984", Price = 15.99m, AuthorId = 1 },
            new Book { Id = 2, Title = "Animal Farm", Price = 12.50m, AuthorId = 1 },
            new Book { Id = 3, Title = "Harry Potter", Price = 25.00m, AuthorId = 2 }
        };

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Authors = Authors,
                Books = Books.Select(b => new BookCardViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    AuthorName = Authors.FirstOrDefault(a => a.Id == b.AuthorId)?.Name ?? "Unknown"
                }).ToList()
            };

            return View(homeViewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(string title, decimal price, int authorId)
        {
            // Bonus validation: block adding a book with an empty title
            if (!string.IsNullOrWhiteSpace(title))
            {
                int newId = Books.Any() ? Books.Max(b => b.Id) + 1 : 1;
                Books.Add(new Book
                {
                    Id = newId,
                    Title = title,
                    Price = price,
                    AuthorId = authorId
                });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                int newId = Authors.Any() ? Authors.Max(a => a.Id) + 1 : 1;
                Authors.Add(new Author
                {
                    Id = newId,
                    Name = name,
                    Country = country
                });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                Books.Remove(book);
            }

            return RedirectToAction("Index");
        }
    }
}