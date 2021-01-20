using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Service.Service
{
    public interface IMoviesService
    {
        public IEnumerable<string> GenreQuery();

        public IEnumerable<MovieViewModel> Search(string movieGenre, string searchString);

        public IEnumerable<MovieViewModel> GetAll();

        public MovieViewModel Find(object id);

        public void Add(Movie movie);

        public void Update(Movie movie);

        public void Remove(int id);
    }
}
