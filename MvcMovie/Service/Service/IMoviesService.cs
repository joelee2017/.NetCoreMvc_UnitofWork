using Model.Models;
using System.Collections.Generic;

namespace Service.Service
{
    public interface IMoviesService
    {
        public IEnumerable<string> GenreQuery();

        public IEnumerable<MovieViewModel> Search(string movieGenre, string searchString);

        public IEnumerable<MovieViewModel> GetAll();

        public MovieViewModel Add(Movie movie);

        public MovieViewModel Update(Movie movie);

        public void Remove(int id);
    }
}
