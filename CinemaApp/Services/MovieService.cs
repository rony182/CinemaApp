using System;
using System.Collections.Generic;
using System.IO;
using CinemaApp.Models;

namespace CinemaApp.Services
{
    public class MovieService
    {
        private readonly List<Movie> _movies;

        public MovieService()
        {
            _movies = new List<Movie>();
            LoadMovies();
        }

        private void LoadMovies()
        {
            var movieFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "movies.txt");

            if (File.Exists(movieFilePath))
            {
                var movieLines = File.ReadAllLines(movieFilePath);
                foreach (var line in movieLines)
                {
                    _movies.Add(new Movie { Name = line });
                }
            }
            else
            {
                Console.WriteLine($"The file {movieFilePath} does not exist.");
            }
        }

        public List<Movie> GetMovies() => _movies;
    }
}
