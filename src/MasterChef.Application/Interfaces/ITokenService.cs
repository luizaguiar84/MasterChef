using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(string userName);

    }
}
