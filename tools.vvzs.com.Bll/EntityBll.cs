using System;
using System.Collections.Generic;
using tools.vvzs.com.DAL;
using tools.vvzs.com.Model.Entity;

namespace tools.vvzs.com.Bll
{
    public class EntityBll<T> where T : IEntity
    {
        private readonly IEntityDal<T> _entityDal;

        public EntityBll(IEntityDal<T> entityDal)
        {
            _entityDal = entityDal;
        }

        public bool Add(T entity)
        {
            return _entityDal.Add(entity);
        }

        public bool Update(T entity)
        {
            return _entityDal.Update(entity);
        }

        public IList<T> Get(T entity)
        {
            return _entityDal.Get(entity);
        }
    }

    public class IdentityBll<T> : EntityBll<T> where T : IIdentityEntity
    {
        private readonly IIdentityDal<T> _identityEntityDal;

        public IdentityBll(IIdentityDal<T> identityEntityDal) : base(identityEntityDal)
        {
            _identityEntityDal = identityEntityDal;
        }

        public T Get(long id)
        {
            return _identityEntityDal.Get(id);
        }
        public bool Delete(long id)
        {
            return _identityEntityDal.Delete(id);
        }

        public bool Exist(long id)
        {
            return _identityEntityDal.Exist(id);
        }
    }

    public class BaseIdentityBll<TModel> where TModel : IIdentityEntity
    {
        public readonly IdentityBll<TModel> Common;

        public BaseIdentityBll(IIdentityDal<TModel> identityEntityDal)
        {
            Common = new IdentityBll<TModel>(identityEntityDal);
        }

        public bool Save(TModel entity)
        {
            if (entity.Id > 0)
            {
                return Common.Update(entity);
            }
            return Common.Add(entity);
        }
    }
}
