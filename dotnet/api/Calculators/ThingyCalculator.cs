using database.Entities;

namespace api.Calculators;

public  record Deposit(string Date, decimal Amount, decimal ContibutionDateValue);

public class ReturnCalculator
{
    public double CalculateReturn(KnownValue startValue, KnownValue endValue, IEnumerable<Deposit> deposits)
    {
        return 0;
    }
}