using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Models;
using HotelBooking.Data.Repositories;

namespace HotelBooking.BusinessLogic
{
    public class BookingManager
    {
        private readonly IRepository<Booking> bookingRepository = new BookingRepository();
        private readonly IRepository<Room> roomRepository = new RoomRepository();

        public bool CreateBooking(Booking booking)
        {
            int roomId = FindAvailableRoom(booking.StartDate, booking.EndDate);

            if (roomId >= 0)
            {
                booking.RoomId = roomId;
                booking.IsActive = true;
                bookingRepository.Add(booking);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int FindAvailableRoom(DateTime startDate, DateTime endDate)
        {
            if (startDate <= DateTime.Today || startDate > endDate)
                throw new ArgumentException("The start date cannot be in the past or later than the end date.");

            var activeBookings = bookingRepository.GetAll().Where(b => b.IsActive);
            foreach (var room in roomRepository.GetAll())
            {
                var activeBookingsForCurrentRoom = activeBookings.Where(b => b.RoomId == room.Id);
                if (activeBookingsForCurrentRoom.All(b => startDate < b.StartDate &&
                    endDate < b.StartDate || startDate > b.EndDate && endDate > b.EndDate))
                {
                    return room.Id;
                }
            }
            return -1;
        }

        public List<DateTime> GetFullyOccupiedDates(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("The start date cannot be later than the end date.");

            List<DateTime> fullyOccupiedDates = new List<DateTime>();
            int noOfRooms = roomRepository.GetAll().Count();
            var bookings = bookingRepository.GetAll();

            if (bookings.Any())
            {
                for (DateTime d = startDate; d <= endDate; d = d.AddDays(1))
                {
                    var noOfBookings = from b in bookings
                                       where b.IsActive && d >= b.StartDate && d <= b.EndDate
                                       select b;
                    if (noOfBookings.Count() >= noOfRooms)
                        fullyOccupiedDates.Add(d);
                }
            }
            return fullyOccupiedDates;
        }

    }
}
