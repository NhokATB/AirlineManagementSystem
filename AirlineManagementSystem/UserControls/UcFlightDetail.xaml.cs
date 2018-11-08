using AirportManagerSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirportManagerSystem.UserControls
{
    /// <summary>
    /// Interaction logic for FlightDetail.xaml
    /// </summary>
    public partial class UcFlightDetail : UserControl
    {
        public UcFlightDetail(Schedule flight, CabinType cabin)
        {
            InitializeComponent();

            tblFrom.Text = flight.Route.Airport.IATACode;
            tblTo.Text = flight.Route.Airport1.IATACode;
            tblCabinType.Text = cabin.Name;
            tblDate.Text = flight.Date.ToString("dd/MM/yyyy");
            tblFlightNumber.Text = flight.FlightNumber;
            tblTime.Text = flight.Time.ToString(@"hh\:mm");
        }
    }
}
