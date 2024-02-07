using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterCompanySharedFolder.pushData
{
    internal class Account
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Account(string name, string password)
        {
            Name=name;
            Password=password;
        }
    }
}
