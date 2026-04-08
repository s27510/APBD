namespace LegacyRenewalApp.Services;

public class FeeService
{
    public decimal CalculateSupport(string plan, bool include)
    {
        if (!include) return 0;

        return plan switch
        {
            "START" => 250m,
            "PRO" => 400m,
            "ENTERPRISE" => 700m,
            _ => 0m
        };
    }

    public decimal CalculatePayment(string method, decimal subtotal, decimal support)
    {
        decimal baseValue = subtotal + support;

        return method switch
        {
            "CARD" => baseValue * 0.02m,
            "BANK_TRANSFER" => baseValue * 0.01m,
            "PAYPAL" => baseValue * 0.035m,
            "INVOICE" => 0m,
            _ => throw new System.ArgumentException("Unsupported payment method")
        };
    }
}