using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibrary1
{
    public class TotalMonthlyExpenditure
    {
        /// <summary>
        /// Calculates the total for the expenses form
        /// <summary>
        public decimal TotalExpenditure(decimal groceries, decimal utilities, decimal travel, decimal cellphone, decimal other)
        {
            return groceries + utilities + travel + cellphone + other;
        }

    }
}
