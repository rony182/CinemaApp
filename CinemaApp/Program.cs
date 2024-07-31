using CinemaApp.Services;

namespace CinemaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var movieService = new MovieService();
            var directorService = new DirectorService();
            var functionService = new FunctionService();

            var menuHandler = new MenuHandler(functionService, movieService, directorService);
            menuHandler.ShowMenu();
        }
    }
}
