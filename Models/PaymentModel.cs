using System;

namespace ZarinpalGateway.Models
{
    public class PaymentModel
    {
        public PaymentModel(int amount)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            IsSuccess = false;
            DateTime = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Authority { get; set; }
        public DateTime DateTime { get; set; }
        public int Amount { get; set; }
        public bool IsSuccess { get; set; }
    }
}
