using MoneyManager.DataAccess.Entity;
using MoneyManager.DataAccess.UnitOfWork;
using MoneyManager.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyManager.Service
{
    class TransactionService
    {
        protected readonly UnitOfWork unitOfWork;

        public TransactionService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Ordering descending by Transaction.Date.
        /// </summary>
        public IQueryable<TransactionInfo> GetInfoByAssetId(Guid assetId, DateTime startDate, DateTime endDate)
        {
            return GetInfoByAssetId(assetId).Where(t => t.Date > startDate && t.Date < endDate);
        }

        public IQueryable<TransactionInfo> GetInfoByAssetId(Guid assetId)
        {
            return unitOfWork.TransactionRepository.GetAllByAssetId(assetId)
                .Select(t => new TransactionInfo(t));
        }
        
        public void DeleteByAssetId(Guid assetId, DateTime startDate, DateTime endDate) =>
            unitOfWork.TransactionRepository.DeleteByAssetId(assetId, startDate, endDate);

        public void DeleteByAssetIdInCurrentMonth(Guid assetId) =>
            unitOfWork.TransactionRepository.DeleteByAssetIdInCurrentMonth(assetId);




        /// <summary>
        /// User by DeleteUserTransactionsInCurrentMonth(Guid userId)
        /// </summary>
        public void DeleteUserTransactions(Guid userId, DateTime startDate, DateTime endDate)
        {
            IQueryable<Asset> assets = unitOfWork.AssetRepository.GetUserAssets(userId);
            foreach (var asset in assets)
                unitOfWork.TransactionRepository.DeleteByAssetId(asset.Id, startDate, endDate);
        }

        /// <summary>
        /// Task: "Write a command to delete all users' (parameter userId) transactions in the current month."
        /// (uses DeleteUserTransactions(Guid userId, DateTime startDate, DateTime endDate))
        /// </summary>
        public void DeleteUserTransactionsInCurrentMonth(Guid userId)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            DateTime firstDay = new DateTime(year, month, 1);
            DeleteUserTransactions(userId, firstDay, firstDay.AddDays(days));
        }



        /// <summary>
        /// Write a query to return the transaction list for the selected user (userId)
        /// ordered descending by Transaction.Date, then ordered ascending by Asset.Name and then ordered ascending by Category.Name.
        /// </summary>
        public IQueryable<TransactionInfo> GetUserTransactionInfos(Guid userId)
        {
            var result = unitOfWork.AssetRepository.GetUserAssets(userId)
                .OrderBy(a => a.Name)
                .Join(
                    unitOfWork.TransactionRepository.GetAll(),
                    a => a.Id,
                    t => t.Asset.Id,
                    (a, t) => t
                ).OrderByDescending(t => t.Date)
                .OrderBy(t => t.Category.Name);
        }

    }
}
