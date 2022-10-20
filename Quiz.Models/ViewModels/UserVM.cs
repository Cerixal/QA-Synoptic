using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class UserVM
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Username")]
        public string? Username { get; set; }

        [DisplayName("Password")]
        public string? Password { get; set; }

        [DisplayName("Role")]
        public string? Role { get; set; }

        public List<UserVM> Users { get; set; }
    }
}
