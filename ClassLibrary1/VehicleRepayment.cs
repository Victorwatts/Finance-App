namespace ClassLibrary1
{
    public class VehicleRepayment
    {
        decimal PriceLeft;
        decimal Rate;
        decimal AmountOwed;

        /// <summary>
        ///Remove the depost from the vehicle amount
        /// <summary>
        public decimal DepositRemoved(decimal VehiclePrice, decimal Deposit)
        {
            PriceLeft = VehiclePrice - Deposit;
            return PriceLeft;
        }

        /// <summary>
        /// Converts the interest rate to a decimal by dividing the value by 100
        /// <summary>
        public decimal InterestRate(decimal Percentage)
        {
            Rate = Percentage / 100;
            return Rate;
        }

        /// <summary>
        /// Calculates the total repayment for the vehicle including intrest
        /// a = vehicleprice - totaldeposit
        /// b = intersetrate / 100
        /// <summary>
        public decimal TotalRepayment(decimal a, decimal b)
        {
            AmountOwed = a * (1 + b * 5);
            return AmountOwed;
        }

        /// <summary>
        /// Calculates the monthly repayment including the insurance premium
        /// a = amountowed
        /// b= insurance premium
        /// <summary>
        public decimal MonthlyRepayment(decimal a, decimal b)
        {
            decimal x = a / 60;
            decimal Repayment = x + b;
            return Repayment;
        }
    }
}
