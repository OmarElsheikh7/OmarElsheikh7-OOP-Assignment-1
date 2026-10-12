using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationSystem
{
    public enum RoomType { Single, Double, Suite }
    public class Room
    {
        public RoomType roomType { get; }
        public int RoomNumber { get; }
        public double NightlyRate { get; private set; }
        public bool IsUnderMaintenance { get; private set; }
        private readonly List<Reservation> _roomReservations;

        public Room(int roomnum, RoomType type)
        {
            roomType = type;
            RoomNumber = roomnum;
            _roomReservations = new List<Reservation>();
        }

        public void SetNightlyRate(double rate)
        {
            if (rate <= 0)
                throw new ArgumentException("Nightly rate must be a positive value.");

            NightlyRate = rate;
        }

        public void StartMaintenance()
        {
            IsUnderMaintenance = true;
        }

        public void EndMaintenance()
        {
            IsUnderMaintenance = false;
        }

        public bool IsAvailable(DateTime checkIn, DateTime checkOut)
        {
            if (IsUnderMaintenance) return false;

            foreach (var res in _roomReservations)
            {
                if (res.reservationStatus != ReservationStatus.Cancelled &&
                    res.reservationStatus != ReservationStatus.CheckedOut)
                {
                    if (checkIn < res.CheckOutDate && checkOut > res.CheckInDate)
                        return false;
                }
            }
            return true;
        }

        internal void RegisterReservation(Reservation reservation)
        {
            _roomReservations.Add(reservation);
        }
    }
}
