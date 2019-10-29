using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Service.Model
{
    class AssetIncomeAndExpensesInfo
    {
        public AssetInfo AssetId { get; set; }
        public double Income { get; set; }
        public double Expenses { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
