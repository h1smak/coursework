using System;

namespace BlaBlaApi.Models
{
    public class Payment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public PaymentMethod Method { get; set; }

        public bool Process(Passenger p, PaymentMethod method)
        {
            if (Amount <= 0) return false;
            return true; 
        }
    }

    public enum PaymentStatus { Pending, Completed, Failed }
    public enum PaymentMethod { Card, Cash, BankTransfer }


}
