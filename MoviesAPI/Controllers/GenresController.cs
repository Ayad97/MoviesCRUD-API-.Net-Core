#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _context.genres.OrderBy(m => m.Name).ToListAsync();

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreViewModel viewModel)
        {
            if (viewModel == null)
                return BadRequest(ModelState);

            var genres = new Genre { Name = viewModel.Name };
            await _context.genres.AddAsync(genres);
            _context.SaveChanges();

            return Ok(genres);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int?  id,[FromBody] GenreViewModel viewModel)
        {
            if(id == null)
                return BadRequest();
            var result = await _context.genres.SingleOrDefaultAsync(g => g.GenreId == id);
            if(result == null)
                return NotFound();
            result.Name = viewModel.Name;
            _context.SaveChanges();

            return Ok(result);
        }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteAsync(int? id)
        {
            if(id==null)
                return BadRequest();
            var result =await _context.genres.SingleOrDefaultAsync(g => g.GenreId == id);
            if(result==null)
                 return NotFound();
            _context.genres.Remove(result);
            _context.SaveChanges();
            return Ok(result);
        }
    }
}
