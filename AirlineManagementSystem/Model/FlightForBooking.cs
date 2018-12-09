using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManagerSystem.Model
{
    class FlightForBooking
    {
        public FlightForBooking()
        {
        }

        public List<Schedule> Flights { get; set; }
        public double Price { get; set; }
        public int NumberOfStop { get; set; }
        public string FlightNumbers { get; set; }
        public Schedule FirstFlight { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public static double GetPrice(Schedule item, CabinType cabin)
        {
            double price = (int)item.EconomyPrice;
            double bprice = Math.Floor(price * 1.35);
            double fprice = Math.Floor(bprice * 1.3);

            return cabin.ID == 1 ? price : (cabin.ID == 2 ? bprice : fprice);
        }
        public static double GetPrice(List<Ticket> tickets, int cabinId)
        {
            double price = tickets.Sum(t => (int)t.Schedule.EconomyPrice);

            double bprice = Math.Floor(price * 1.35);
            double fprice = Math.Floor(bprice * 1.3);

            return cabinId == 1 ? price : (cabinId == 2 ? bprice : fprice);
        }

    }
}
