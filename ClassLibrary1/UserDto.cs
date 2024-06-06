using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserDtoLogin
    {
      

        public string Email { get; set; }

        public string Password { get; set; }

    
    }

    public class UserDtoRegisterAndUpdate
    {

        public int Userid { get; }
        public string Email { get; set; }

        public string FirstName { get; set; } = null;

        public string LastName { get; set; } = null;

        public string Password { get; set; }


    }
    public class UserDtoGet
    {

        public int Userid { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; } = null;

        public string LastName { get; set; } = null;

        public string Password { get; set; }


    }


}
