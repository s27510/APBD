using System;

namespace LegacyRenewalApp.Helpers;

public class InvoiceFactory
{
    public RenewalInvoice Create(
        Customer customer,
        string plan,
        string payment,
        int seats,
        decimal baseAmount,
        DiscountResult discount,
        decimal support,
        decimal paymentFee,
        decimal tax,
        decimal final)
    {
        if (final < 500m)
        {
            final = 500m;
            discount.Notes += "minimum invoice amount applied; ";
        }

        return new RenewalInvoice
        {
            InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{customer.Id}-{plan}",
            CustomerName = customer.FullName,
            PlanCode = plan,
            PaymentMethod = payment,
            SeatCount = seats,
            BaseAmount = Round(baseAmount),
            DiscountAmount = Round(discount.Discount),
            SupportFee = Round(support),
            PaymentFee = Round(paymentFee),
            TaxAmount = Round(tax),
            FinalAmount = Round(final),
            Notes = discount.Notes.Trim(),
            GeneratedAt = DateTime.UtcNow
        };
    }

    private decimal Round(decimal v)
        => Math.Round(v, 2, MidpointRounding.AwayFromZero);
}