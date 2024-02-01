using LoginSolo.Data;
using Microsoft.Extensions.DependencyInjection;
using LoginSolo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using LoginSolo.Services;

namespace LoginSolo.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly MovieService _movieService;


        public HomeController(DataContext context, ILogger<HomeController> logger, MovieService movieService)
        {
            _context = context;
            _logger = logger;
            _movieService = movieService;

        }




        public async Task<IActionResult> Index()
        {
            var popularMovies = await _movieService.GetPopularMoviesAsync();
            return View(popularMovies);
        }




        public async Task<IActionResult> Detail(int id)
        {
            var movieDetail = await _movieService.GetMovieDetailAsync(id);
            return View(movieDetail);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }





    }
}
