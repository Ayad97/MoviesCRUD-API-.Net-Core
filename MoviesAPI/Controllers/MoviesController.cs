using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Context;
using MoviesAPI.ViewModel;
using MoviesCRUD.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MoviesController(ApplicationDbContext context,IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _context.Movies
                .OrderByDescending(x => x.Rate)
                .Include(m => m.Genre)
                .Select(m => new MovieViewModel
                {
                    Title = m.Title,
                    GenreId = m.GenreId,
                    GenreName = m.Genre.Name,
                    Rate = m.Rate,
                    StoreLine = m.StoreLine,
                    Year = m.Year,
                    Poster = m.Poster,
                })
                .ToListAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var result = await _context.Movies.Include(m=>m.Genre).SingleOrDefaultAsync(m=>m.Id==id);
            if(result==null)
                return NotFound();
            var movie = new MovieViewModel
            {
                Title = result.Title,
                GenreId = result.GenreId,
                GenreName = result.Genre.Name,
                Rate = result.Rate,
                StoreLine = result.StoreLine,
                Year = result.Year,
                Poster = result.Poster,
            };
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]MovieViewModel viewModel)
        {

            var file = Request.Form.Files;
            var Poster = file.FirstOrDefault();
            using var dataStream = new MemoryStream();
            await Poster.CopyToAsync(dataStream);
            viewModel.Poster = dataStream.ToArray();

            var model = new Movies
            {
                Title = viewModel.Title,
                GenreId = viewModel.GenreId,
                Poster = dataStream.ToArray(),
                Rate = viewModel.Rate,
                StoreLine = viewModel.StoreLine,
                Year = viewModel.Year,
                
            };
           await _context.AddAsync(model);
             _context.SaveChanges();

            return Ok(model);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int? id,[FromBody]MovieViewModel viewModel)
        {
            if (id == null)
            return BadRequest();

            var result = await _context.Movies.FindAsync(id);
            if(result == null)
                return NotFound();
            result.Title = viewModel.Title;
            result.GenreId = viewModel.GenreId;
            result.Poster = result.Poster;
            result.Rate = viewModel.Rate;
            result.StoreLine = viewModel.StoreLine;
            result.Year = viewModel.Year;

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteAsync(int? id)
        {
            var result = await _context.Movies.FindAsync(id);
            if(null == result)
                return NotFound();
            _context.Movies.Remove(result);
            _context.SaveChanges();
            return Ok(result);

        }
       
    }
}
