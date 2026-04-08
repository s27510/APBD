using System;
using LegacyRenewalApp.Helpers;
using LegacyRenewalApp.Services;

namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        private readonly CustomerRepository _customerRepository = new();
        private readonly SubscriptionPlanRepository _planRepository = new();

        private readonly DiscountService _discountService = new();
        private readonly FeeService _feeService = new();
        private readonly TaxService _taxService = new();
        private readonly InvoiceFactory _invoiceFactory = new();
        private readonly BillingGatewayWrapper _billing = new();

        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints)
        {
            InputValidator.Validate(customerId, planCode, seatCount, paymentMethod);

            string normalizedPlanCode = planCode.Trim().ToUpperInvariant();
            string normalizedPaymentMethod = paymentMethod.Trim().ToUpperInvariant();

            var customer = _customerRepository.GetById(customerId);
            var plan = _planRepository.GetByCode(normalizedPlanCode);

            if (!customer.IsActive)
                throw new InvalidOperationException("Inactive customers cannot renew subscriptions");

            decimal baseAmount = (plan.MonthlyPricePerSeat * seatCount * 12m) + plan.SetupFee;

            var discount = _discountService.Calculate(customer, plan, seatCount, baseAmount, useLoyaltyPoints);

            decimal supportFee = _feeService.CalculateSupport(normalizedPlanCode, includePremiumSupport);
            decimal paymentFee = _feeService.CalculatePayment(normalizedPaymentMethod, discount.Subtotal, supportFee);

            decimal tax = _taxService.Calculate(customer.Country, discount.Subtotal + supportFee + paymentFee);

            decimal finalAmount = discount.Subtotal + supportFee + paymentFee + tax;

            var invoice = _invoiceFactory.Create(
                customer,
                normalizedPlanCode,
                normalizedPaymentMethod,
                seatCount,
                baseAmount,
                discount,
                supportFee,
                paymentFee,
                tax,
                finalAmount);

            _billing.SaveInvoice(invoice);
            _billing.SendEmail(customer, invoice);

            return invoice;
        }
    }
}