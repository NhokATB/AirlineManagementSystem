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

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for LayoutWindow.xaml
    /// </summary>
    public partial class LayoutWindow : Window
    {
        public bool IsClosed { get; set; }
        public LayoutWindow()
        {
            InitializeComponent();
            this.Closed += LayoutWindow_Closed;
        }

        private void LayoutWindow_Closed(object sender, EventArgs e)
        {
            IsClosed = true;
        }

        public new void ShowDialog()
        {
            this.ShowDialog();
        }
    }
}
