using ElKhattabiNaima_Prova6.Core.Interfaces;
using ElKhattabiNaima_Prova6.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElKhattabiNaima_Prova6.EF.Repositories
{
    public class EFPolicyRepository : IPolicyRepository
    {
        private readonly UserContext ctx;

        public EFPolicyRepository()
        {
            ctx = new UserContext();
        }
        public bool Add(Policy item)
        {
            throw new NotImplementedException();
        }

        public bool AddPolicyToExistingUser(Policy policy)
        {
            var user = ctx.Users.Find(policy.User.Id);
            user.policies.Add(new Policy
            {
                PolicyNumber = policy.PolicyNumber,
                ExpirationDate = policy.ExpirationDate,
                MontlyPayment = policy.MontlyPayment,
                Type = policy.Type
            }) ;

            ctx.SaveChanges();
            if (user == null)
            {
                return false;
            }
            else return true;

        }

        public bool CheckPolicyNumber(int num)
        {
            var p = ctx.Policies.FirstOrDefault(x => x.PolicyNumber == num);
            if (p == null)
            {
                return false;
            }
            else return true;
        }

        public bool Delete(Policy item)
        {
            throw new NotImplementedException();
        }

        public List<Policy> Fetch()
        {
            return ctx.Policies.ToList();

        }

        public List<Policy> FetchUserPolicies(User user)
        {
            var policies = ctx.Policies.Where(p => p.UserId == user.Id).Include(u => u.User).ToList();
            return policies;
        }

        public Policy GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Policy GetPolicyByNumber(int num)
        {
            var p = ctx.Policies.FirstOrDefault(x => x.PolicyNumber == num);
            return p;
        }

        public bool PosticipateExpirationDate_Disconnected(Policy policy)
        {
            var pol = ctx.Policies.FirstOrDefault(p => p.Id == policy.Id);
            
            using(var ctx_D = new UserContext())
            {
                ctx_D.Policies.Update(pol);
                ctx_D.SaveChanges();
            }

            if (pol == null)
            {
                return false;
            }
            else return true;
            
        }

        public bool Update(Policy item)
        {
            throw new NotImplementedException();
        }
    }
}
