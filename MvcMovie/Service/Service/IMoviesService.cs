using Model.Models;
using System.Collections.Generic;

namespace Service.Service
{
    public interface IMoviesService
    {
        public IEnumerable<string> GenreQuery();

        public IEnumerable<MovieViewModel> Search(string movieGenre, string searchString);

        public IEnumerable<MovieViewModel> GetAll();

        public MovieViewModel Find(object id);

        public void Add(MovieViewModel movie);

        public void Update(MovieViewModel movie);

        public void Remove(int id);
    }
}
