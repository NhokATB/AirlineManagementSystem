using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementAPI.Models
{
    public class TicketModel
    {
        public int ID { get; set; }
        public string User { get; set; }
        public string CabinType { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string PassportNumber { get; set; }
        public string Country { get; set; }
        public string BookingReference { get; set; }
        public string Seat { get; set; }

        public ScheduleModel ScheduleModel { get; set; }

        public TicketModel()
        {

        }

        public TicketModel(Ticket ticket)
        {
            this.ScheduleModel = new ScheduleModel(ticket.Schedule);

            this.ID = ticket.ID;
            this.Firstname = ticket.Firstname;
            this.Lastname = ticket.Lastname;
            this.User = ticket.User.FirstName + " " + ticket.User.LastName;
            this.CabinType = ticket.CabinType.Name;
            this.PassportNumber = ticket.PassportNumber;
            this.Country = ticket.Country.Name;
            this.Phone = ticket.Phone;
            this.BookingReference = ticket.BookingReference;
            this.Seat = ticket.Seat ?? "None";
        }
    }
}