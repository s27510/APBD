namespace LegacyRenewalApp.Helpers;

public class BillingGatewayWrapper
{
    public void SaveInvoice(RenewalInvoice invoice)
    {
        LegacyBillingGateway.SaveInvoice(invoice);
    }

    public void SendEmail(Customer c, RenewalInvoice invoice)
    {
        if (string.IsNullOrWhiteSpace(c.Email)) return;

        string subject = "Subscription renewal invoice";
        string body =
            $"Hello {c.FullName}, your renewal for plan {invoice.PlanCode} " +
            $"has been prepared. Final amount: {invoice.FinalAmount:F2}.";

        LegacyBillingGateway.SendEmail(c.Email, subject, body);
    }
}