using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Data
{
    public class HotelBookingContextFactory
    {
        public HotelBookingContextFactory()
        {
            var options = new DbContextOptionsBuilder<HotelBookingContext>()
                .UseInMemoryDatabase("HotelBookingDb")
                .Options;
            Context = new HotelBookingContext(options);
        }

        public HotelBookingContext Context { get; }
    }
}
