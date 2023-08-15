using GameShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories
{
    public interface IUserRepository //data ile business arasında filtreler
    {
        void Add(UserEntity entity);
        void Delete(int id);
        void Delete(UserEntity entity);
        void Update(UserEntity entity);
        UserEntity GetById(int id); // arama metodu


    }
}
