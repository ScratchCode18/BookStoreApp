using BookStoreApp.Models;
using System.Collections.Generic;

namespace BookStoreApp.ViewModels
{
    public class HomeViewModel
    {
        public List<BookCardViewModel> Books { get; set; } = new List<BookCardViewModel>();
        public List<Author> Authors { get; set; } = new List<Author>();
    }
}