using Model.Mapper;
using Model.Models;
using Model.Repository;
using Model.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace Service.Service
{
    public class MoviesService : IMoviesService
    {
        private readonly IGenericRepository<Movie> _movieRepository;
        private readonly IGenericRepository<Sales> _salesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MoviesService(IGenericRepository<Movie> movieRepository, IGenericRepository<Sales> salesRepository, IUnitOfWork unitOfWork)
        {
            _movieRepository = movieRepository;
            _salesRepository = salesRepository;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<MovieViewModel> GetAll() => _movieRepository.GetAll().Map<Movie, MovieViewModel>(true);

        // 實作 UnitOfWork Add
        public void Add(MovieViewModel movie)
        {
            Movie _movie = movie.Map<MovieViewModel, Movie>(true);
            Sales _sales = movie.Sales.Map<SalesViewModel, Sales>(true);

            //var result1 = _movieRepository.Insert(_movie);
            //_movieRepository.Save();

            //_sales.MovieId = result1.Id;
            //var result2 = _salesRepository.Insert(_sales);
            //_movieRepository.Save();

            try
            {
                var movieUnit = _unitOfWork.GetRepository<Movie>();
                movieUnit.Insert(_movie);

                _sales.MovieId = _movie.Id; //未交易前，當前無法取得Movie id
                var salesUnit = _unitOfWork.GetRepository<Sales>();
                salesUnit.Insert(_sales);
                _unitOfWork.Save();
            }
            catch (System.Exception)
            {
                _unitOfWork.Dispose();
            }

        }

        public void Update(MovieViewModel movie)
        {
            Movie _movie = movie.Map<MovieViewModel, Movie>(true);
            _movieRepository.Update(_movie);
            _movieRepository.Save();
        }

        public void Remove(int id)
        {
            _movieRepository.Delete(id);
            _movieRepository.Save();
        }

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

            var result = movies.Map<Movie, MovieViewModel>(true) ?? new List<MovieViewModel>();

            return result;
        }

        public MovieViewModel Find(object id) => _movieRepository.GetById(id).Map<Movie, MovieViewModel>(true);
    }
}
