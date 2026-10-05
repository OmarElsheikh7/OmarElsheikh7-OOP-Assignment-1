using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace HotelReservationSystem
{
    public enum ReservationStatus{ Pending, Confirmed, CheckedIn, CheckedOut, Cancelled}
    public class Reservation
    {
        public Guid ReservationId { get; }
        public DateTime CheckInDate { get; }
        public DateTime CheckOutDate { get; }
        public ReservationStatus reservationStatus { get; private set; }
        public Room Room { get; }

        public Reservation(Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            if (checkOutDate <= checkInDate)
                throw new ArgumentException("Check-out date must be strictly after the check-in date.");

            if (room.IsUnderMaintenance)
                throw new InvalidOperationException("Cannot create a reservation for a room under maintenance.");

            if (!room.IsAvailable(checkInDate, checkOutDate))
                throw new InvalidOperationException("The room is already booked for these dates.");

            ReservationId = Guid.NewGuid();
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Room = room;
            reservationStatus = ReservationStatus.Pending;
            room.RegisterReservation(this);
        }

        public void Confirm()
        {
            if (reservationStatus != ReservationStatus.Pending)
                throw new InvalidOperationException("Only Pending reservations can be Confirmed.");

            reservationStatus = ReservationStatus.Confirmed;
        }

        public void CheckIn()
        {
            if (reservationStatus != ReservationStatus.Confirmed)
                throw new InvalidOperationException("Cannot check-in unless the reservation is Confirmed."); //[cite: 8]

            reservationStatus = ReservationStatus.CheckedIn;
        }

        public void CheckOut()
        {
            if (reservationStatus != ReservationStatus.CheckedIn)
                throw new InvalidOperationException("Cannot check-out unless the guest is CheckedIn.");

            reservationStatus = ReservationStatus.CheckedOut;
        }

        public void Cancel()
        {
            if (reservationStatus != ReservationStatus.Pending && reservationStatus != ReservationStatus.Confirmed)
                throw new InvalidOperationException("Only Pending or Confirmed reservations can be Cancelled.");

            reservationStatus = ReservationStatus.Cancelled;
        }

        public double TotalCost
        {
            get
            {
                int nights = (CheckOutDate - CheckInDate).Days;
                return nights * Room.NightlyRate;
            }
        }
    }
}
