using GameShop.Business.Dto;
using GameShop.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Business.Services
{
    public interface IUserService //abstract olarak arayüzler (soyut)
    {
        ServiceMessage AddUser(AddUserDto addUserDto);
        UserInfoDto LoginUser (LoginDto loginDto);
    }
}
