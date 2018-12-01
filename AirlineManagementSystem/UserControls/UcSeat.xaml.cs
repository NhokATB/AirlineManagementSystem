using AirportManagerSystem.HelperClass;
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
    /// Interaction logic for UcSeat.xaml
    /// </summary>
    public partial class UcSeat : UserControl
    {
        public bool IsDual { get; set; }
        public int CabinId { get; internal set; }
        public Schedule Flight { get; internal set; }
        public string NearLeft { get; internal set; }
        public string NearRight { get; internal set; }
        public string Seat { get; internal set; }
        public Ticket Ticket { get; internal set; }
        public UcSeat Previous { get; set; }
        public UcSeat After { get; set; }
        public UcSeat()
        {
            InitializeComponent();
            this.Loaded += UcSeat_Loaded;
        }

        private void UcSeat_Loaded(object sender, RoutedEventArgs e)
        {
            btnSeat.Content = Seat;
            if (Ticket != null)
            {
                ToolTip t = new ToolTip();
                t.Content = $"Id: {Ticket.ID}\nFull name: {Ticket.Firstname} {Ticket.Lastname}\nPassport number: {Ticket.PassportNumber}";
                btnSeat.ToolTip = t;

                btnSeat.Background = new SolidColorBrush(AMONICColor.CheckedIn);
            }
            else if(IsDual == false)
            {
                btnSeat.Background = new SolidColorBrush(AMONICColor.Empty);
            }
            else
            {
                btnSeat.Background = new SolidColorBrush(AMONICColor.Dual);
            }

            if (CabinId == 3)
            {
                this.Background = new SolidColorBrush(AMONICColor.First);
                this.Width += 145;
                btnSeat.Width += 145;
            }
            else if (CabinId == 2)
            {
                this.Background = new SolidColorBrush(AMONICColor.Business);
                this.Width += 25;
                btnSeat.Width += 25;
            }
            else
            {
                this.Width -= 15;
                btnSeat.Width -= 15;
                this.Background = new SolidColorBrush(Colors.LightBlue);
            }
        }
    }
}
