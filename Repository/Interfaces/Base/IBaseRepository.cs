namespace Repository.Interfaces.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        DataContext datacontext { get; }

        void Abort();
        void Commit();
        void Create(TEntity entity);
        List<TEntity> FindByCustomFilter(Dictionary<string, string>? filters);
        TEntity FindById(int id);
        List<TEntity> GetAllRegisters();
        void Update(TEntity entity);
        void Delete(int id);
    }
}