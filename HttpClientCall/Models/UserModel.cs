using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientCall.Models
{
    public class UserModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
    public class UserLogin
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
