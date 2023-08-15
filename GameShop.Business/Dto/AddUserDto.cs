using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Business.Dto
{
    public class AddUserDto //formdan (View) gelen giriş verilerini Data transfer object ile businesse taşıyoruz. business içindeki metotlar parametre olarak adduserdto alacak.
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
