using System;
using ZarinpalGateway.Models.Enums;

namespace ZarinpalGateway.Models
{
    public class PaymentModel
    {
        public PaymentModel(int amount)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            State = States.Redirected;
            DateTime = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Authority { get; set; }
        public DateTime DateTime { get; set; }
        public int Amount { get; set; }
        public States State { get; set; }
    }
}
