using Model.Repository;

namespace Model.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        void Save();
        void Dispose();
    }
}
