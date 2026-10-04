using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationSystem
{
    public class Guest
    {
        public Guid guestId { get;}
        public string fullName { get;}
        public string phoneNumber { get;}

        public IReadOnlyList<Reservation> Reservations { get;}

        public Guest(string fullname,string phonenumber)
        {

            if (String.IsNullOrWhiteSpace(fullname))
                throw new ArgumentException("Fullname can not be null or empty");   

            if(String.IsNullOrWhiteSpace(phonenumber))
                throw new ArgumentException("Phone number can not be null or empty");

            guestId = Guid.NewGuid();
            fullName = fullname;
            phoneNumber = phonenumber;
            Reservations = new List<Reservation>();
        }
    }
}
