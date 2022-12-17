using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
