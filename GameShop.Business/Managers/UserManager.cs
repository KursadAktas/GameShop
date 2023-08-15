using GameShop.Business.Dto;
using GameShop.Business.Services;
using GameShop.Business.Types;
using GameShop.Data.Entities;
using GameShop.Data.Repositories;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Business.Managers
{
    public class UserManager : IUserService //Bir soyut sınıfın alt sınıfları, ancak üst sınıfın tanımladığı soyut sınıfların üzerine yazdığı ve onlara birer işlev tanımladığı zaman örneklenebilir (“instantiation”). > Bu durumda alt sınıflar somut sınıf ( concrete class ) olarak adlandırılır.

    {
        private readonly IRepository<UserEntity> _userRepository; //Di
        private readonly IDataProtector _dataProtector;
        public UserManager(IRepository<UserEntity> userRepository, IDataProtectionProvider dataProtectionProvider)
        {
            _userRepository = userRepository;
            _dataProtector = dataProtectionProvider.CreateProtector("security"); //cripto şifreleme.
        }
        //doğrudan dataya erişmemiz için 
        public ServiceMessage AddUser(AddUserDto addUserDto) //Kullanıcı ekleme. //Servis mesajı dönecek.
        {
            var mail = _userRepository.GetAll(x=>x.Email.ToLower() == addUserDto.Email.ToLower()).ToList(); // girilen email eşleşsin ve listeye çevirsin.

            if(mail.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu e-posta adresi daha önce alınmış."
                };
            }
            var entity = new UserEntity()
            {
                Email = addUserDto.Email,
                FirstName = addUserDto.FirstName,
                LastName = addUserDto.LastName,
                Password = _dataProtector.Protect(addUserDto.Password), // şifre cripto 
                UserType = Data.Enums.UserTypeEnum.User,
            };

            _userRepository.Add(entity);
            return new ServiceMessage()
            {
                IsSucceed = true,
                Message = "Hesap Oluşturuldu."
            };

        }
        public UserInfoDto LoginUser(LoginDto loginDto)
        {
            var userEntity = _userRepository.Get(x => x.Email == loginDto.Email);

            if (userEntity is null)
            {
                return null;
                // eğer form üzerinde gönderilen email adresi ile eşleşen bir veri tabloda yoksa, oturum açılamayacağı için, geriye hiçbir veri dönülmüyor.
            }

            var rawPassword = _dataProtector.Unprotect(userEntity.Password); // şifre açıldı.

            if (loginDto.Password == rawPassword)
            {
                return new UserInfoDto()
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    UserType = userEntity.UserType
                };
            }
            else
            {
                return null;
            }

        }
    }
}
