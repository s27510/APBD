namespace LegacyRenewalApp;

public class DiscountResult
{
    public decimal Discount { get; }
    public decimal Subtotal { get; }
    public string Notes { get; set; }

    public DiscountResult(decimal discount, decimal subtotal, string notes)
    {
        Discount = discount;
        Subtotal = subtotal;
        Notes = notes;
    }
}