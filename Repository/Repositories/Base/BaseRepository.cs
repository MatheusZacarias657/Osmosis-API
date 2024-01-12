using Repository.Interfaces.Base;

namespace Repository.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public DataContext datacontext { get; }

        public BaseRepository(DataContext dataContext)
        {
            datacontext = dataContext;
        }

        public void Create(TEntity entity)
        {
            try
            {
                datacontext.Set<TEntity>().Add(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity FindById(int id)
        {
            try
            {
                TEntity? entity = datacontext.Set<TEntity>().Find(id);

                if (entity == null)
                    throw new Exception("Register not exist");

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TEntity> FindByCustomFilter(Dictionary<string, string>? filters)
        {
            try
            {
                if (filters == null)
                    return datacontext.Set<TEntity>().ToList();

                List<TEntity> filteredList = filters.Aggregate(datacontext.Set<TEntity>().AsEnumerable(), (current, filter) => current.Where(a => a.GetType().GetProperty(filter.Key).GetValue(a).ToString().ToLower() == filter.Value.ToLower())).ToList();

                return filteredList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TEntity> GetAllRegisters()
        {
            try
            {
                return datacontext.Set<TEntity>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                datacontext.Set<TEntity>().Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            TEntity entity = FindById(id);
            entity.GetType().GetProperty("active").SetValue(entity, false);
            Update(entity);
        }

        public void Commit()
        {
            try
            {
                datacontext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Abort()
        {
            try
            {
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
