using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginSolo.Services;

namespace LoginSolo.Controllers
{
  public class MovieController : Controller
  {
    private readonly MovieService _movieService;

    public MovieController(MovieService movieService)
    {
      _movieService = movieService;
    }

    public async Task<IActionResult> Index()
    {
      return View();

    }
    public async Task<IActionResult> Detail(int id)
    {
      var movieDetail = await _movieService.GetMovieDetailAsync(id);
      return View(movieDetail);
    }
  }
}