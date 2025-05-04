using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;

namespace BlaBlaApi.Models
{
    public class Passenger : User
    {
        public List<Booking> Bookings { get; set; } = new();

        public bool BookSeat(Trip trip, PaymentMethod method)
        {
            if (trip.AvailableSeats == 0) return false;

            var booking = new Booking
            {
                Id = Guid.NewGuid().ToString(),
                Passenger = this,
                Driver = trip.Driver,
                Status = BookingStatus.Pending,
                Trip = trip,
            };
            Bookings.Add(booking);
            return true;
        }

        public bool CancelBooking(Booking booking, DateTime currentTime)
        {
            var tripDateTime = booking.TripDateTime;
            if (tripDateTime - currentTime >= TimeSpan.FromHours(2))
            {
                Bookings.Remove(booking);
                return true;
            }
            return false;
        }

        public bool LeaveReview(Trip trip, int rating, string text)
        {
            if (trip.Date < DateTime.Now)
            {
                var review = new Review
                {
                    Id = Guid.NewGuid().ToString(),
                    Author = this,
                    Target = trip.Driver,
                    Rating = rating,
                    Text = text
                };
                return true;
            }
            return false;
        }
    }


}
