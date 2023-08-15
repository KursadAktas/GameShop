using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories
{
    public interface IRepository <TEntity> where TEntity : class //Genel için kullanılacak Repository Generic Rep.
    {
        void Add(TEntity entity);
        void Delete(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        TEntity GetById(int id);    
        IQueryable<TEntity>GetAll(Expression<Func<TEntity,bool>>predicate = null);//birden fazla sorgular için parametre olarak linq gönderilecekse Expression(x=>x. gibi kalıplar için. =null ile default değer için null kabul eder.

        TEntity Get(Expression<Func<TEntity, bool>> predicate); //boş olmadığı için null olmaz.
    }
}
