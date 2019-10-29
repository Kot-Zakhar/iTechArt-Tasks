using MoneyManager.DataAccess.UnitOfWork;

namespace MoneyManager.Service
{
    public class UserService
    {
        protected readonly UnitOfWork unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

    }
}
