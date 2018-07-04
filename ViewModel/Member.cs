using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ViewModel
{
    public class Member
    {
        public int MemberID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Json { get; set; }
    }
}
