using ElKhattabiNaima_Prova6.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElKhattabiNaima_Prova6.Core.Interfaces
{
   public interface IPolicyRepository : IRepository<Policy>
    {
        Policy GetPolicyByNumber(int num);
        bool AddPolicyToExistingUser(Policy policy);
        bool CheckPolicyNumber(int num);
        List<Policy> FetchUserPolicies(User user);
        bool PosticipateExpirationDate_Disconnected(Policy policy);
    }
}
