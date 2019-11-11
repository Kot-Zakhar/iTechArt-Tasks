using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Service
{
    public static class MonthExtention
    {
        public static DateTime GetFirstDayOfMonth(this DateTime date)
        {
            int month = date.Month;
            int year = date.Year;
            return new DateTime(year, month, 1);
        }

        public static DateTime GetLastDayOfMonth(this DateTime date)
        {
            int month = date.Month;
            int year = date.Year;
            int days = DateTime.DaysInMonth(year, month);
            return new DateTime(year, month, 1);

        }
    }
}
