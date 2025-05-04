using System;

namespace BlaBlaApi.Models
{
    public class Review
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Passenger Author { get; set; }
        public Driver Target { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
    }
}
