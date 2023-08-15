using GameShop.Data.Context;
using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories
{
    public class UserRepository : IUserRepository //User için Repository
    {
        private readonly GameShopContext _db; //sanal bir db kopyası oluştu.
        private readonly DbSet<UserEntity> _dbSet;
        public UserRepository(GameShopContext db)
        {
            _db = db;
            _dbSet=_db.Set<UserEntity>();
        }
        public void Add(UserEntity entity) //IUserRepositorylerin detayları burada yazıldı
        {
            _dbSet.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id) // soft Delete
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public void Delete(UserEntity entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
            _db.SaveChanges();
        }

        public UserEntity GetById(int id)
        {
            var entity = _dbSet.Find(id);
            return entity;
        }

        public void Update(UserEntity entity)
        {
            _dbSet.Update(entity);
            entity.ModifiedDate = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
