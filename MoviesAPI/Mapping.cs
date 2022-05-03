using AutoMapper;
using MoviesAPI.ViewModel;
using MoviesCRUD.Models;

namespace MoviesAPI
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Movies, MovieViewModel>()
                .ForMember(dest => dest.GenreName, src => src.MapFrom(src => src.Genre.Name));

            CreateMap<MovieViewModel, Movies>()
                 .ForMember(src => src.Poster, opt => opt.Ignore());

            CreateMap<Genre, GenreViewModel>();
            
        }
    }
}
