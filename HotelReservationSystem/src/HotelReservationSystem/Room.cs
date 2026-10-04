using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationSystem
{
    public enum RoomType { Single, Double, Suite }
    public class Room
    {
        public RoomType roomType { get;}
        public int RoomNumber { get; }
        public double NightlyRate {  get; private set;}
        public bool IsUnderMaintenance { get; private set;}

        public Room(int roomnum , RoomType type)
        {
            roomType = type;
            RoomNumber = roomnum;
        }
    }
}
