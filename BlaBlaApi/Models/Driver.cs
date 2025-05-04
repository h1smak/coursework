using System;

namespace BlaBlaApi.Models
{
    public class Driver : User
    {
        public List<Trip> Trips { get; set; } = new();

        public Trip CreateTrip(Location from, Location to, DateTime date, TimeSpan time, int seats)
        {
            if (date < DateTime.Now.Date)
                throw new InvalidOperationException("Дата в минулому");

            return new Trip
            {
                Id = Guid.NewGuid().ToString(),
                Driver = this,
                From = from,
                To = to,
                Date = date,
                DepartureTime = time,
                Seats = seats,
                AvailableSeats = seats
            };
        }

        public bool EditTrip(string tripId, Trip newTrip)
        {
            var trip = Trips.FirstOrDefault(t => t.Id == tripId);
            if (trip == null) return false;

            trip.From = newTrip.From;
            trip.To = newTrip.To;
            trip.Date = newTrip.Date;
            trip.DepartureTime = newTrip.DepartureTime;
            trip.Seats = newTrip.Seats;
            trip.AvailableSeats = newTrip.AvailableSeats;
            return true;
        }

        public bool CancelTrip(string tripId)
        {
            var trip = Trips.FirstOrDefault(t => t.Id == tripId && t.Passengers.Count == 0);
            if (trip == null) return false;

            Trips.Remove(trip);
            return true;
        }

        public bool ConfirmRequest(Booking booking)
        {
            var trip = Trips.FirstOrDefault(t => t.Id == booking.Trip.Id);
            if (trip?.AvailableSeats > 0)
            {
                booking.Status = BookingStatus.Confirmed;
                trip.AvailableSeats--;
                return true;
            }
            return false;
        }

        public bool RejectRequest(Booking booking)
        {
            booking.Status = BookingStatus.Rejected;
            return true;
        }
    }
}
