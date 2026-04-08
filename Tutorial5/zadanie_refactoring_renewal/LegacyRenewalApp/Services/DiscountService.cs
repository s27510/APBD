namespace LegacyRenewalApp.Services;

public class DiscountService
{
    public DiscountResult Calculate(Customer c, SubscriptionPlan p, int seats, decimal baseAmount, bool usePoints)
        {
            decimal discount = 0m;
            string notes = "";
            
            if (c.Segment == "Silver") { discount += baseAmount * 0.05m; notes += "silver discount; "; }
            else if (c.Segment == "Gold") { discount += baseAmount * 0.10m; notes += "gold discount; "; }
            else if (c.Segment == "Platinum") { discount += baseAmount * 0.15m; notes += "platinum discount; "; }
            else if (c.Segment == "Education" && p.IsEducationEligible)
            {
                discount += baseAmount * 0.20m; notes += "education discount; ";
            }
            
            if (c.YearsWithCompany >= 5)
            {
                discount += baseAmount * 0.07m; notes += "long-term loyalty discount; ";
            }
            else if (c.YearsWithCompany >= 2)
            {
                discount += baseAmount * 0.03m; notes += "basic loyalty discount; ";
            }
            
            if (seats >= 50)
            {
                discount += baseAmount * 0.12m; notes += "large team discount; ";
            }
            else if (seats >= 20)
            {
                discount += baseAmount * 0.08m; notes += "medium team discount; ";
            }
            else if (seats >= 10)
            {
                discount += baseAmount * 0.04m; notes += "small team discount; ";
            }
            
            if (usePoints && c.LoyaltyPoints > 0)
            {
                int used = c.LoyaltyPoints > 200 ? 200 : c.LoyaltyPoints;
                discount += used;
                notes += $"loyalty points used: {used}; ";
            }

            decimal subtotal = baseAmount - discount;

            if (subtotal < 300m)
            {
                subtotal = 300m;
                notes += "minimum discounted subtotal applied; ";
            }

            return new DiscountResult(discount, subtotal, notes);
        }
}