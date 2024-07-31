using System;
using System.Collections.Generic;
using System.Linq;
using CinemaApp.Models;

namespace CinemaApp.Services
{
    public class FunctionService
    {
        private const int MaxMoviesPerDay = 10;
        private const int MaxInternationalMoviesPerWeek = 8;
        private readonly List<Function> _functions;

        public FunctionService()
        {
            _functions = new List<Function>();
        }

        public void AddFunction(Function function)
        {
            if (CanAddFunction(function))
            {
                _functions.Add(function);
                Console.WriteLine("Function added successfully.");
            }
            else
            {
                Console.WriteLine("Cannot add function due to constraints.");
            }
        }

        public void ModifyFunction(int index, Function newFunction)
        {
            if (index >= 0 && index < _functions.Count)
            {
                var oldFunction = _functions[index];
                _functions.RemoveAt(index);

                if (CanAddFunction(newFunction))
                {
                    _functions.Insert(index, newFunction);
                    Console.WriteLine("Function modified successfully.");
                }
                else
                {
                    _functions.Insert(index, oldFunction);
                    Console.WriteLine("Cannot modify function due to constraints.");
                }
            }
            else
            {
                Console.WriteLine("Invalid function index.");
            }
        }

        public void DeleteFunction(int index)
        {
            if (index >= 0 && index < _functions.Count)
            {
                _functions.RemoveAt(index);
                Console.WriteLine("Function deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid function index.");
            }
        }

        public void ListFunctions()
        {
            if (_functions.Count == 0)
            {
                Console.WriteLine("No functions found. Add a function!");
                return;
            }

            foreach (var function in _functions)
            {
                Console.WriteLine($"{function.Date.ToShortDateString()} {function.ScheduleHour} {function.MovieName} by {function.DirectorName} - ${function.Price}");
            }
        }

        public List<Function> GetFunctions() => _functions;

        private bool CanAddFunction(Function function, int? modifyIndex = null)
        {
            var functionsByDirectorOnDay = _functions
                .Where((f, i) => f.Date.Date == function.Date.Date
                                 && f.DirectorName == function.DirectorName
                                 && (!modifyIndex.HasValue || i != modifyIndex.Value))
                .ToList();

            if (functionsByDirectorOnDay.Count >= MaxMoviesPerDay)
            {
                return false;
            }

            if (function.IsInternational)
            {
                var weekStart = function.Date.AddDays(-(int)function.Date.DayOfWeek);
                var weekEnd = weekStart.AddDays(7);

                var internationalMoviesThisWeek = _functions
                    .Where((f, i) => f.IsInternational
                                     && f.Date >= weekStart
                                     && f.Date < weekEnd
                                     && (!modifyIndex.HasValue || i != modifyIndex.Value))
                    .Count();

                if (internationalMoviesThisWeek >= MaxInternationalMoviesPerWeek)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
