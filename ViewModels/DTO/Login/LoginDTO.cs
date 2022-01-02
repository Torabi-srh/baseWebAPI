using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace baseWebAPI.ViewModels.DTO.Login
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int AppUser { get; set; }
        public string UserType { get; set; }
    }
}
