using System;

namespace BlaBlaApi.Models
{
    public class Trip
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Driver Driver { get; set; }
        public Location From { get; set; }
        public Location To { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public int Seats { get; set; }
        public int AvailableSeats { get; set; }
        public List<Passenger> Passengers { get; set; } = new();

        public bool ReserveSeat(Passenger p)
        {
            if (AvailableSeats > 0)
            {
                AvailableSeats--;
                Passengers.Add(p);
                return true;
            }
            return false;
        }

        public bool Cancel()
        {
            return Passengers.Count == 0;
        }
    }


}
