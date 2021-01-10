using Model.Models;
using System.Collections.Generic;
using System.Linq;
namespace Service.Service
{
    public class MoviesService : IMoviesService
    {
        private readonly IRepository<Movie, MovieViewModel> _movieRepository;

        public MoviesService(IRepository<Movie, MovieViewModel> movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public IEnumerable<MovieViewModel> GetAll() => _movieRepository.GetAll();

        public MovieViewModel Add(Movie movie) => _movieRepository.Add(movie);

        public MovieViewModel Update(Movie movie) => _movieRepository.Update(movie);

        public void Remove(int id) => _movieRepository.Remove(id);

        public IEnumerable<string> GenreQuery()
        {
            // Use LINQ to get list of genres.
            IEnumerable<string> genreQuery = from m in _movieRepository.GetAll()
                                             orderby m.Genre
                                             select m.Genre;

            return genreQuery.Distinct();
        }

        public IEnumerable<MovieViewModel> Search(string movieGenre, string searchString)
        {
            var movies = from m in _movieRepository.GetAll()
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            return movies;
        }
    }
}
