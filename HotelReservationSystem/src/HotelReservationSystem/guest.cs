using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationSystem
{
    public class guest
    {
        public Guid guestId { get;}
        public string fullName { get;}
        public string phoneNumber { get;}

        public guest(string fullname,string phonenumber)
        {

            if (String.IsNullOrEmpty(fullname))
                throw new ArgumentException("Fullname can not be null or empty");   

            if(String.IsNullOrEmpty(phonenumber))
                throw new ArgumentException("Phone number can not be null or empty");

            guestId = Guid.NewGuid();
            fullName = fullname;
            phoneNumber = phonenumber;
        }
    }
}
