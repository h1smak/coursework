using BlaBlaApi.Models;

namespace BlaBlaApi.Controllers
{
    public static class DataStore
    {
        public static List<Trip> Trips { get; } = new();
        public static List<Booking> Bookings { get; } = new();

        public static List<Review> reviews = new();
    }

}
