using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LoginSolo.Models;

namespace LoginSolo.Services
{
  public class MovieService
  {
    private readonly HttpClient _client;

    public MovieService()
    {
      _client = new HttpClient();
      _client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<Movie>> GetPopularMoviesAsync()
    {
      var apiKey = "d1e27c02f69633c91c69884d4a8e25dd";
      var popularMoviesUrl = $"movie/popular?api_key={apiKey}";

      var movies = new List<Movie>();

      try
      {
        HttpResponseMessage response = await _client.GetAsync(popularMoviesUrl);
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        dynamic data = JsonConvert.DeserializeObject(jsonString);

        foreach (var movie in data.results)
        {
          string id = movie.id;
          string title = movie.title;
          string posterPath = movie.poster_path;
          string overview = movie.overview;
          string releaseDate = movie.release_date;


          movies.Add(new Movie { Title = title, PosterPath = posterPath, Overview = overview, Id = id, ReleaseDate = releaseDate });
        }
      }
      catch (HttpRequestException ex)
      {
        Console.WriteLine($"Error retrieving popular movies: {ex.Message}");
      }

      return movies;
    }
    public async Task<Movie> GetMovieDetailAsync(int id)
    {
      var apiKey = "d1e27c02f69633c91c69884d4a8e25dd";
      var movieDetailUrl = $"movie/{id}?api_key={apiKey}";

      try
      {
        HttpResponseMessage response = await _client.GetAsync(movieDetailUrl);
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        dynamic data = JsonConvert.DeserializeObject(jsonString);

        string title = data.title;
        string posterPath = data.poster_path;
        string overview = data.overview;
        string releaseDate = data.release_date;


        var movie = new Movie { Title = title, PosterPath = posterPath, Overview = overview, ReleaseDate = releaseDate };
        return movie;
      }
      catch (HttpRequestException ex)
      {
        Console.WriteLine($"Error retrieving movie details: {ex.Message}");
        return null;
      }
    }

  }

}
