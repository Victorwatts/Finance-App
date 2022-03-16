namespace ClassLibrary1
{
    public class MoneyLeft
    {
        /// <summary>
        /// Calculates how much money the user has left after all expenses are taken into account.
        /// </summary>
        public decimal TotalExpense(decimal grossincome, decimal tax, decimal groceries, decimal utilities, decimal travel, decimal cellphone, decimal other, decimal loanrepayment, decimal rental, decimal vehicleloan )
        {
            decimal x = tax + groceries + utilities + travel + cellphone + other + loanrepayment + rental + vehicleloan;
            return grossincome - x;
        }

    }
}
