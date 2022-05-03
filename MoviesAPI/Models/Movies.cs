﻿using System.ComponentModel.DataAnnotations;

namespace MoviesCRUD.Models
{
    public class Movies
    {
        public int Id { get; set; }

        [Required,MaxLength(255)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [Required,MaxLength(2500)]
        public string StoreLine { get; set; }
        public byte[] Poster { get; set; }
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
    }
}
