namespace LegacyRenewalApp.Services;

public class TaxService
{
    public decimal Calculate(string country, decimal amount)
    {
        decimal rate = 0.20m;

        if (country == "Poland") rate = 0.23m;
        else if (country == "Germany") rate = 0.19m;
        else if (country == "Czech Republic") rate = 0.21m;
        else if (country == "Norway") rate = 0.25m;

        return amount * rate;
    }
}