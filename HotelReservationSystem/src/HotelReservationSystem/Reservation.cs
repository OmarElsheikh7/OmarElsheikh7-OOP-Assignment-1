using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationSystem
{
    public enum ReservationStatus{ Pending, Confirmed, CheckedIn, CheckedOut, Cancelled}
    public class Reservation
    {
        public Guid ReservationId { get;}
        public DateTime CheckInDate { get;}
        public DateTime CheckOutDate { get; }
        public ReservationStatus reservationStatus { get;}
        public Room Room { get;}

        public Reservation(DateTime checkInDate , DateTime checkOutDate, ReservationStatus reservationstatus)
        {
            ReservationId = new Guid();
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            reservationStatus = reservationstatus;
        }
    }
}
