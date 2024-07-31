using System;
using System.Collections.Generic;
using System.IO;
using CinemaApp.Models;

namespace CinemaApp.Services
{
    public class DirectorService
    {
        private readonly List<Director> _directors;

        public DirectorService()
        {
            _directors = new List<Director>();
            LoadDirectors();
        }

        private void LoadDirectors()
        {
            var directorFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "directors.txt");

            if (File.Exists(directorFilePath))
            {
                var directorLines = File.ReadAllLines(directorFilePath);
                foreach (var line in directorLines)
                {
                    _directors.Add(new Director { Name = line });
                }
            }
            else
            {
                Console.WriteLine($"The file {directorFilePath} does not exist.");
            }
        }

        public List<Director> GetDirectors() => _directors;
    }
}
