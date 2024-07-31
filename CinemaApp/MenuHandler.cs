using System;
using CinemaApp.Models;
using CinemaApp.Services;

namespace CinemaApp
{
    public class MenuHandler
    {
        private readonly FunctionService _functionService;
        private readonly MovieService _movieService;
        private readonly DirectorService _directorService;

        public MenuHandler(FunctionService functionService, MovieService movieService, DirectorService directorService)
        {
            _functionService = functionService;
            _movieService = movieService;
            _directorService = directorService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Add Function");
                Console.WriteLine("2. Modify Function");
                Console.WriteLine("3. Delete Function");
                Console.WriteLine("4. List Functions");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddFunction();
                        break;
                    case "2":
                        ModifyFunction();
                        break;
                    case "3":
                        DeleteFunction();
                        break;
                    case "4":
                        _functionService.ListFunctions();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void AddFunction()
        {
            var function = GetFunctionDetails();
            if (function != null)
            {
                _functionService.AddFunction(function);
            }
        }

        private void ModifyFunction()
        {
            var functions = _functionService.GetFunctions();
            if (functions.Count == 0)
            {
                Console.WriteLine("No functions to modify.");
                return;
            }

            Console.WriteLine("Select a function to modify:");
            for (int i = 0; i < functions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {functions[i].Date.ToShortDateString()} {functions[i].ScheduleHour} {functions[i].MovieName} by {functions[i].DirectorName} - ${functions[i].Price}");
            }

            Console.Write("Enter the number of the function: ");
            if (!int.TryParse(Console.ReadLine(), out int functionIndex) || functionIndex < 1 || functionIndex > functions.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            var newFunction = GetFunctionDetails();
            if (newFunction != null)
            {
                _functionService.ModifyFunction(functionIndex - 1, newFunction);
            }
        }

        private void DeleteFunction()
        {
            var functions = _functionService.GetFunctions();
            if (functions.Count == 0)
            {
                Console.WriteLine("No functions to delete.");
                return;
            }

            Console.WriteLine("Select a function to delete:");
            for (int i = 0; i < functions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {functions[i].Date.ToShortDateString()} {functions[i].ScheduleHour} {functions[i].MovieName} by {functions[i].DirectorName} - ${functions[i].Price}");
            }

            Console.Write("Enter the number of the function: ");
            if (!int.TryParse(Console.ReadLine(), out int functionIndex) || functionIndex < 1 || functionIndex > functions.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            _functionService.DeleteFunction(functionIndex - 1);
        }

        private Function GetFunctionDetails()
        {
            var movies = _movieService.GetMovies();
            var directors = _directorService.GetDirectors();

            if (movies.Count == 0)
            {
                Console.WriteLine("No movies available.");
                return null;
            }

            if (directors.Count == 0)
            {
                Console.WriteLine("No directors available.");
                return null;
            }

            Console.WriteLine("Select a movie:");
            for (int i = 0; i < movies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {movies[i].Name}");
            }

            Console.Write("Enter the number of the movie: ");
            if (!int.TryParse(Console.ReadLine(), out int movieIndex) || movieIndex < 1 || movieIndex > movies.Count)
            {
                Console.WriteLine("Invalid selection.");
                return null;
            }

            var selectedMovie = movies[movieIndex - 1];

            Console.WriteLine("Select a director:");
            for (int i = 0; i < directors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {directors[i].Name}");
            }

            Console.Write("Enter the number of the director: ");
            if (!int.TryParse(Console.ReadLine(), out int directorIndex) || directorIndex < 1 || directorIndex > directors.Count)
            {
                Console.WriteLine("Invalid selection.");
                return null;
            }

            var selectedDirector = directors[directorIndex - 1];

            Console.WriteLine("Enter the date (yyyy-mm-dd):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Invalid date.");
                return null;
            }

            Console.WriteLine("Enter the schedule hour (hh:mm):");
            if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan scheduleHour))
            {
                Console.WriteLine("Invalid schedule hour.");
                return null;
            }

            Console.WriteLine("Enter the price:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price.");
                return null;
            }

            Console.WriteLine("Is the movie international? (true/false):");
            if (!bool.TryParse(Console.ReadLine(), out bool isInternational))
            {
                Console.WriteLine("Invalid input.");
                return null;
            }

            return new Function
            {
                MovieName = selectedMovie.Name,
                DirectorName = selectedDirector.Name,
                Date = date,
                ScheduleHour = scheduleHour,
                Price = price,
                IsInternational = isInternational
            };
        }
    }
}
