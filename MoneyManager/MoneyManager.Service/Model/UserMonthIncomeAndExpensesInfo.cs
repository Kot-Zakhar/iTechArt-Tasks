namespace MoneyManager.Service.Model
{
    public class UserMonthIncomeAndExpensesInfo
    {
        public UserInfo UserInfo { get; set; }
        public double Income { get; set; }
        public double Expenses { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
