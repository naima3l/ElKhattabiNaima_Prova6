using ElKhattabiNaima_Prova6.Core.Interfaces;
using ElKhattabiNaima_Prova6.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElKhattabiNaima_Prova6
{
    public class MainBL
    {
        private IUserRepository _userRepo;
        private IPolicyRepository _policyRepo;

        public MainBL(IUserRepository userRepository, IPolicyRepository policyRepository)
        {
            _userRepo = userRepository;
            _policyRepo = policyRepository;
        }

        internal bool GetByCF(string cf)
        {
            return _userRepo.GetByCF(cf);
        }

        internal bool AddUser(User user)
        {
            return _userRepo.Add(user);
        }

        internal Policy GetPolicyByNumber(int num)
        {
            return _policyRepo.GetPolicyByNumber(num);
        }

        internal List<Policy> FetchPolicies()
        {
            return _policyRepo.Fetch();
        }

        internal User GetUserByCF(string cf)
        {
            return _userRepo.GetUserByCF(cf);
        }

        internal bool AddPolicyToExistingUser(Policy policy)
        {
            return _policyRepo.AddPolicyToExistingUser(policy);
        }

        internal bool CheckPolicyNumber(int num)
        {
            return _policyRepo.CheckPolicyNumber(num);
        }

        internal List<Policy> FetchUserPolicies(User user)
        {
            return _policyRepo.FetchUserPolicies(user);
        }

        internal bool PosticipateExpirationDate_Disconnected(Policy policy)
        {
            return _policyRepo.PosticipateExpirationDate_Disconnected(policy);
        }

        internal List<User> FetchUsersByPolicy()
        {
            return _userRepo.FetchUsersByPolicy();
        }
    }
}
