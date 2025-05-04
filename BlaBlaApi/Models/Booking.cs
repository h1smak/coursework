using System;

namespace BlaBlaApi.Models
{
    public class Booking
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Passenger Passenger { get; set; }
        public Driver Driver { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public Trip Trip { get; set; }
        public DateTime TripDateTime { get; set; }

        public bool Confirm() => Status == BookingStatus.Pending;
        public bool Cancel(DateTime now) => (TripDateTime - now).TotalHours >= 2;
    }

    public enum BookingStatus { Pending, Confirmed, Rejected }


}
