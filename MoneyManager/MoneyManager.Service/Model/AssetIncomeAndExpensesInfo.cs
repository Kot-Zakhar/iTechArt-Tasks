using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Service.Model
{
    public class AssetIncomeAndExpensesInfo
    {
        public AssetInfo Asset { get; set; }
        public double Income { get; set; }
        public double Expenses { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
