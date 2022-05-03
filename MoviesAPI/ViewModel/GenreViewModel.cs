using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.ViewModel
{
    public class GenreViewModel
    {
        public int id { get; set; }
        [Required, MaxLength(200)]
        [Display(Name = "GenreName")]
        public string Name { get; set; }
    }
}
