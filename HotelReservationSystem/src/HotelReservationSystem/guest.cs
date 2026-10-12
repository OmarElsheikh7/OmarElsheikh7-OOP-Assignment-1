using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationSystem
{
    public class Guest
    {
        public Guid guestId { get; }
        public string fullName { get; }
        public string phoneNumber { get; }

        private readonly List<Reservation> _reservations;

        public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

        public Guest(string fullname, string phonenumber)
        {
            if (String.IsNullOrWhiteSpace(fullname))
                throw new ArgumentException("Fullname can not be null or empty");

            if (String.IsNullOrWhiteSpace(phonenumber))
                throw new ArgumentException("Phone number can not be null or empty");

            guestId = Guid.NewGuid();
            fullName = fullname;
            phoneNumber = phonenumber;
            _reservations = new List<Reservation>();
        }
        public Reservation MakeReservation(Room room, DateTime checkIn, DateTime checkOut)
        {
            var reservation = new Reservation(room, checkIn, checkOut);
            _reservations.Add(reservation);
            return reservation;
        }
    }
}
