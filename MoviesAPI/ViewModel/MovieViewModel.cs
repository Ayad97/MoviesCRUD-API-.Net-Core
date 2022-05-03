using MoviesCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.ViewModel
{
    public class MovieViewModel
    {
        public int id { get; set; }
        [Required, MaxLength(255)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [Required, MaxLength(2500)]
        public string StoreLine { get; set; }
        public byte[] Poster { get; set; }
        public byte  GenreId { get; set; }
        public string GenreName { get; set; }

    }
}
