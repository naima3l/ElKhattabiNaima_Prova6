using ElKhattabiNaima_Prova6.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElKhattabiNaima_Prova6.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool GetByCF(string cf);
        User GetUserByCF(string cf);

        List<User> FetchUsersByPolicy();
    }
}
