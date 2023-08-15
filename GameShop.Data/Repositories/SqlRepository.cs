using GameShop.Data.Context;
using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories
{
    public class SqlRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly GameShopContext _db;
        private readonly DbSet<TEntity> _dbSet; //hangi amaç için çağırılırsa orası için kullanılacak Generic Repository
        public SqlRepository(GameShopContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
           var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
            _db.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate) //sorgulama için firstordefault kullanıldı. veri varsa dönecel yoksa null dönecek.
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is not null? _dbSet.Where(predicate) : _dbSet;//sorgulamada veri varsa predicate dön yoksa veri tabanını dön. turnery if.
        }

        public TEntity GetById(int id)
        {
            var entity = _dbSet.Find(id);
            return entity; //return gerekiyor.
        }

        public void Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
            _db.SaveChanges();
        }
    }
}
