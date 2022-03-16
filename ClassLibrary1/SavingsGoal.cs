using System;

namespace ClassLibrary1
{
    public class SavingsGoal
    {
        public decimal SavingsCalculation(decimal InterestRate, decimal Period, decimal Goal)
        {

            decimal x = ((InterestRate / 100) / 12);
            decimal y = Period * 12;
            decimal a = 1 + x;
            decimal z = (decimal)Math.Pow((double)a, (double)y);

            decimal result = (Goal * x) / (z - 1);
            return result;
        }
    }
}
