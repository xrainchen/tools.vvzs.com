using System.Collections.Generic;
using tools.vvzs.com.Model.Entity;

namespace tools.vvzs.com.DAL
{
    #region 抽象层

    public abstract class EntityDal<T> : IEntityDal<T> where T : IEntity
    {
        public abstract bool Add(T entity);

        public abstract bool Update(T entity);

        public abstract IList<T> Get(T entity);
    }

    public abstract class IdentityDal<T> : EntityDal<T>, IIdentityDal<T> where T : IIdentityEntity
    {
        public abstract T Get(long id);
        public abstract bool Delete(long id);
        public abstract bool Exist(long id);
    }
    #endregion
}
