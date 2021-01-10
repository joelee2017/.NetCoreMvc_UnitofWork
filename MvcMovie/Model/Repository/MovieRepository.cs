using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model.Data;
using Model.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Model.Models
{
    public class MovieRepository : IRepository<Movie, MovieViewModel>
    {
        private readonly MvcMovieContext _mvcMovieContext;

        public MovieRepository(MvcMovieContext mvcMovieContext) => _mvcMovieContext = mvcMovieContext;

        public IEnumerable<MovieViewModel> GetAll() => _mvcMovieContext.Movie.Map<Movie, MovieViewModel>();

        public MovieViewModel Find(int id)
        {
            var result = _mvcMovieContext.Movie.Find(id).Map<Movie, MovieViewModel>(true);

            return result;
        }

        public MovieViewModel FirstOrDefault(Expression<Func<Movie, bool>> func)
        {
            var moive = _mvcMovieContext.Movie.FirstOrDefault(func);

            var result = moive.Map<Movie, MovieViewModel>(true);

            return result;
        }


        public MovieViewModel Add(Movie movie)
        {
            var moive = _mvcMovieContext.Add(movie);
            _mvcMovieContext.SaveChanges();
            return moive.Entity.Map<Movie, MovieViewModel>(true);
        }

        public MovieViewModel Update(Movie movie)
        {
            var moive = _mvcMovieContext.Update(movie);
            _mvcMovieContext.SaveChanges();
            return moive.Entity.Map<Movie, MovieViewModel>(true);
        }

        public void Remove(int id)
        {
            var movie = _mvcMovieContext.Movie.Find(id);
            _mvcMovieContext.Remove(movie);

            _mvcMovieContext.SaveChanges();
        }
    }
}
