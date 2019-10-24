using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Repository
{
    public struct UserInfo
    {
        public Guid Id;
        public string Name;
        public string Email;
    }
    public struct UserBalance
    {
        public UserInfo UserInfo;
        public double Balance;
    }
    public interface IUserRepository : IRepository<Entity.User>
    {
        public Entity.User GetByEmail(string email);

        public IEnumerable<UserInfo> GetInfos();
        public IEnumerable<UserInfo> GetInfosSorted(IComparer<UserInfo> comparer);
        public IEnumerable<UserInfo> GetInfosSortedByName();
        public UserBalance GetBalanceById(Guid Id);
    }
}
