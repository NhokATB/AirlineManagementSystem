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
using System.Windows.Shapes;
using AirportManagerSystem.Model;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for BookConfirmationWindow.xaml
    /// </summary>
    public partial class BookConfirmationWindow : Window
    {
        public BookConfirmationWindow()
        {
            InitializeComponent();
        }

        public CabinType Cabin { get; internal set; }
        public int Numpass { get; internal set; }
        internal Flight Flight2 { get; set; }
        internal Flight Flight1 { get; set; }
    }
}
