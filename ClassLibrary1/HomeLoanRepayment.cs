namespace ClassLibrary1
{
    public class HomeLoanRepayment
    {

        decimal Years;
        decimal PriceLeft;
        decimal Rate;
        decimal AmountOwed;
        string Warning;

        /// <summary>
        ///Converts months to years
        /// </summary>
        public decimal MonthsToYears(decimal Months)
        {
            Years = Months / 12;
            return Years;
        }

        /// <summary>
        /// Calculates the amount owed after the deposit is removed
        /// </summary>
        public decimal DepositRemoved(decimal PropertyPrice, decimal Deposit)
        {
            PriceLeft = PropertyPrice - Deposit;
            return PriceLeft;
        }

        /// <summary>
        /// Devides the interst rate by 100 to convert it to a decimal
        /// </summary>
        public decimal InterestRate(decimal Percentage)
        {
            Rate = Percentage / 100;
            return Rate;
        }

        /// <summary>
        ///Calculates the total amount that will be owed with interest
        ///a = propertyprice - total deposit
        ///b = interest rate / 100
        ///c = repayment months / 12
        /// </summary>
        public decimal TotalRepayment(decimal a, decimal b, decimal c)
        {
            AmountOwed = a * (1 + b * c);
            return AmountOwed;
        }

        /// <summary>
        ///Calcculates the monthly repayment
        ///a = amountowed
        ///b = total months
        /// </summary>
        public decimal MonthlyRepayment(decimal a, decimal b)
        {
            return a / b;
        }

        /// <summary>
        /// Calculates if the monthly repayment is more than a third of the users income
        /// a = gross income
        /// b = monthly repayment
        /// </summary>
        public string RepaymentWarining(decimal a, decimal b)
        {
            if (a - b < a / 3)
            {
                Warning = "The home loan repayment is more than a third of your gross income." + "\nIt is unlikely that the home loan will be approved!";

            }
            else
            {
                Warning = "The home loan repayment is less than a third of your gross income." + "\nIt is likely that the home loan will be approved.";

            }
            return Warning;

        }

    }
}
