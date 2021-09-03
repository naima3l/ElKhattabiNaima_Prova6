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
    public class EFUserRepository : IUserRepository
    {
        private readonly UserContext ctx;
        public EFUserRepository()
        {
            ctx = new UserContext();
        }
        public bool Add(User item)
        {
            var user = ctx.Users.Add(new User
            {
                CF = item.CF,
                Name = item.Name,
                LastName = item.LastName,
                policies = item.policies
            });

            ctx.SaveChanges();
            if (user == null)
            {
                return false;
            }
            else return true;

        }

        public bool Delete(User item)
        {
            throw new NotImplementedException();
        }

        public List<User> Fetch()
        {
            throw new NotImplementedException();
        }

        public List<User> FetchUsersByPolicy()
        {
            var users = ctx.Users.Include(u => u.policies).Where(x => x.policies.Any(y => y.Type == (EnumPolicyType)2)).ToList();
            return users;
        }

        public bool GetByCF(string cf)
        {
            var u = ctx.Users.FirstOrDefault(x => x.CF == cf);
            if (u == null)
            {
                return false;
            }
            else return true;
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByCF(string cf)
        {
            var u = ctx.Users.FirstOrDefault(x => x.CF == cf);
            return u;
        }

        public bool Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
