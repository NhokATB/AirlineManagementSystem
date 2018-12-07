using AirportManagerSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AirportManagerSystem.View
{
    public partial class RevenueDetailWindow : Form
    {
        public int Year { get; internal set; }
        public List<Ticket> Tickets { get; set; }
        public string TimeOrMonthOrQuarterOrYear { get; internal set; }
        public int PointIndex { get; internal set; }
        public string ViewMode { get; internal set; }
        private bool isTheFirstFormLoaded = true;

        public RevenueDetailWindow()
        {
            InitializeComponent();
            chartRevenueFromAmenities.ChartAreas[0].AxisX.Interval = 1;
            chartRevenueFromTicket.ChartAreas[0].AxisX.Interval = 1;

            CbTicketViewType.SelectedIndexChanged += CbTicketViewType_SelectedIndexChanged;
            cbCabinType.SelectedIndexChanged += CbCabinType_SelectedIndexChanged;

            cbAmenities.SelectedIndexChanged += CbAmenities_SelectedIndexChanged;
            cbAmenityViewType.SelectedIndexChanged += CbAmenityViewType_SelectedIndexChanged;

            tcRevenueDetail.SelectedIndexChanged += TcRevenueDetail_SelectedIndexChanged;
        }

        private void TcRevenueDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (tcRevenueDetail.SelectedIndex == 1)
            {
                if (isTheFirstFormLoaded)
                {
                    LoadDataForTabFromTickets();

                    isTheFirstFormLoaded = false;
                }
            }

            this.Cursor = Cursors.Default;
        }

        private void RevenueDetailWindow_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            LoadDataForTabFromAmenities();

            if (ViewMode == "rdbByWeek" || ViewMode == "rdbByTime")
            {
                cbAmenities.Enabled = false;
                cbCabinType.Enabled = false;
            }

            this.Cursor = Cursors.Default;
        }

        #region Revenue from Tickets
        private void LoadDataForTabFromTickets()
        {
            var cabins = Db.Context.CabinTypes.ToList();
            cabins.Insert(0, new CabinType() { Name = "All Cabins" });
            cbCabinType.DataSource = cabins;
            cbCabinType.DisplayMember = "Name";

            CbTicketViewType.SelectedIndex = 0;
        }

        private void DeleteSeriesOfChartRevenueFromTickets()
        {
            foreach (var item in chartRevenueFromTicket.Series)
            {
                item.Points.Clear();
            }
        }

        private void CbTicketViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CbCabinType_SelectedIndexChanged(cbCabinType, new EventArgs());
        }

        private void CbCabinType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCabinType.SelectedIndex == 0)
            {
                LoadChartForAllCabinTypes();
            }
            else
            {
                LoadChartForOneCabinType();
            }
        }

        private void LoadChartForOneCabinType()
        {
            DeleteSeriesOfChartRevenueFromTickets();

            chartRevenueFromTicket.Series[0].ChartType = SeriesChartType.Line;
            chartRevenueFromTicket.Series[0].BorderWidth = 2;

            var cabinId = (cbCabinType.SelectedItem as CabinType).ID;
            var tickets = Tickets.Where(t => t.CabinTypeID == cabinId).ToList();

            if (ViewMode == "rdbByMonth")
            {
                chartRevenueFromTicket.Titles[0].Text = $"Revenue detail for cabin type: {cbCabinType.Text} on {TimeOrMonthOrQuarterOrYear} {Year}";

                var month = DateTime.Parse(TimeOrMonthOrQuarterOrYear + " " + Year).Date;

                int pointIndex = 0;
                for (DateTime date = month; date < month.AddMonths(1); date = date.AddDays(1))
                {
                    string value;

                    if (CbTicketViewType.Text == "Revenue")
                    {
                        chartRevenueFromTicket.Series[0].Name = "Revenue";
                        value = Flight.GetPrice(tickets.Where(t => t.Schedule.Date == date).ToList(), cabinId).ToString("C0");
                    }
                    else
                    {
                        chartRevenueFromTicket.Series[0].Name = "Quantity";
                        value = tickets.Count(t => t.Schedule.Date == date).ToString();
                    }

                    chartRevenueFromTicket.Series[0].Points.AddXY(date.ToString("dd/MM/yyyy"), value);
                    chartRevenueFromTicket.Series[0].Points[pointIndex].Label = value;

                    pointIndex++;
                }
            }
            else if (ViewMode == "rdbByQuarter")
            {
                chartRevenueFromTicket.Titles[0].Text = $"Revenue detail for cabin type: {cbAmenities.Text} in {TimeOrMonthOrQuarterOrYear} {Year}";

                var quarter = int.Parse(TimeOrMonthOrQuarterOrYear.Last().ToString());
                var from = (quarter * 2) + (quarter - 2);
                var to = quarter * 3;

                int poinIndex = 0;
                for (int j = from; j <= to; j++)
                {
                    var month = new DateTime(2018, j, 1).ToString("MMMM");
                    string value;

                    if (CbTicketViewType.Text == "Revenue")
                    {
                        chartRevenueFromTicket.Series[0].Name = "Revenue";
                        value = Flight.GetPrice(tickets.Where(t => t.Schedule.Date.Month == j).ToList(), cabinId).ToString("C0");
                    }
                    else
                    {
                        chartRevenueFromTicket.Series[0].Name = "Quantity";
                        value = tickets.Count(t => t.Schedule.Date.Month == j).ToString();
                    }

                    chartRevenueFromTicket.Series[0].Points.AddXY(month, value);
                    chartRevenueFromTicket.Series[0].Points[poinIndex].Label = value;

                    poinIndex++;
                }

            }
            else if (ViewMode == "rdbByYear")
            {
                chartRevenueFromTicket.Titles[0].Text = $"Revenue detail for cabin type: {cbAmenities.Text} in {Year}";

                int pointIndex = 0;
                for (int j = 1; j < 13; j++)
                {
                    var month = new DateTime(2018, j, 1).ToString("MMMM");
                    string value;

                    if (CbTicketViewType.Text == "Revenue")
                        value = Flight.GetPrice(tickets.Where(t => t.Schedule.Date.Month == j).ToList(), cabinId).ToString("C0");
                    else
                        value = tickets.Count(t => t.Schedule.Date.Month == j).ToString();

                    chartRevenueFromTicket.Series[0].Points.AddXY(month, value);
                    chartRevenueFromTicket.Series[0].Points[pointIndex].Label = value;

                    pointIndex++;
                }
            }
        }

        private void LoadChartForAllCabinTypes()
        {
            DeleteSeriesOfChartRevenueFromTickets();

            chartRevenueFromTicket.Series[0].ChartType = SeriesChartType.Column;

            var cabins = Db.Context.CabinTypes.ToList();
            var tickets = Tickets;

            if (ViewMode == "rdbByTime")
            {
                chartRevenueFromTicket.Titles[0].Text = $"Revenue detail for all Cabin types on {TimeOrMonthOrQuarterOrYear}";
            }
            else if (ViewMode == "rdbByWeek")
            {
                chartRevenueFromTicket.Titles[0].Text = $"Revenue detail for all Cabin types on {TimeOrMonthOrQuarterOrYear} this week";
            }
            else if (ViewMode == "rdbByMonth")
            {
                chartRevenueFromTicket.Titles[0].Text = $"Revenue detail for all Cabin types on {TimeOrMonthOrQuarterOrYear} {Year}";
            }
            else if (ViewMode == "rdbByQuarter")
            {
                chartRevenueFromTicket.Titles[0].Text = $"Revenue detail for all Cabin types in {TimeOrMonthOrQuarterOrYear} {Year}";
            }
            else if (ViewMode == "rdbByYear")
            {
                chartRevenueFromTicket.Titles[0].Text = $"Revenue detail for all Cabin types in {Year}";
            }

            int i = 0;
            foreach (var item in cabins)
            {
                string value;

                if (CbTicketViewType.Text == "Revenue")
                {
                    chartRevenueFromTicket.Series[0].Name = "Revenue";
                    value = Flight.GetPrice(tickets.Where(t => t.CabinTypeID == item.ID).ToList(), item.ID).ToString("C0");
                }
                else
                {
                    chartRevenueFromTicket.Series[0].Name = "Quantity";
                    value = tickets.Count(t => t.CabinTypeID == item.ID).ToString();
                }

                chartRevenueFromTicket.Series[0].Points.AddXY(item.Name, value);
                chartRevenueFromTicket.Series[0].Points[i].Label = value;

                i++;
            }
        }

        #endregion

        #region Revenue from Amenities

        private void LoadDataForTabFromAmenities()
        {
            var amenities = Db.Context.Amenities.Where(t => t.Price != 0).ToList();
            amenities.Insert(0, new Amenity() { Service = "All Amenities" });
            cbAmenities.DataSource = amenities;
            cbAmenities.DisplayMember = "Service";

            cbAmenityViewType.SelectedIndex = 0;
        }

        private void DeleteSeriesOfChartRevenueFromAmenities()
        {
            foreach (var item in chartRevenueFromAmenities.Series)
            {
                item.Points.Clear();
            }
        }

        private void CbAmenities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAmenities.SelectedIndex == 0)
            {
                LoadChartForAllAmenities();
            }
            else
            {
                LoadChartForOneAmenity();
            }
        }

        private void LoadChartForOneAmenity()
        {
            DeleteSeriesOfChartRevenueFromAmenities();

            chartRevenueFromAmenities.Series[0].ChartType = SeriesChartType.Line;
            chartRevenueFromAmenities.Series[0].BorderWidth = 2;

            var amenId = (cbAmenities.SelectedItem as Amenity).ID;
            var amenitiesRevenue = Tickets.SelectMany(t => t.AmenitiesTickets).Where(t => t.AmenityID == amenId).ToList();

            if (ViewMode == "rdbByMonth")
            {
                chartRevenueFromAmenities.Titles[0].Text = $"Revenue detail for amenity: {cbAmenities.Text} on {TimeOrMonthOrQuarterOrYear} {Year}";

                var month = DateTime.Parse(TimeOrMonthOrQuarterOrYear + " " + Year).Date;

                int pointIndex = 0;
                for (DateTime date = month; date < month.AddMonths(1); date = date.AddDays(1))
                {
                    string value;

                    if (cbAmenityViewType.Text == "Revenue")
                    {
                        chartRevenueFromAmenities.Series[0].Name = "Revenue";
                        value = amenitiesRevenue.Where(t => t.Ticket.Schedule.Date == date).Sum(t => t.Price).ToString("C0");
                    }
                    else
                    {
                        chartRevenueFromAmenities.Series[0].Name = "Quantity";
                        value = amenitiesRevenue.Count(t => t.Ticket.Schedule.Date == date).ToString();
                    }

                    chartRevenueFromAmenities.Series[0].Points.AddXY(date.ToString("dd/MM/yyyy"), value);
                    chartRevenueFromAmenities.Series[0].Points[pointIndex].Label = value;

                    pointIndex++;
                }
            }
            else if (ViewMode == "rdbByQuarter")
            {
                chartRevenueFromAmenities.Titles[0].Text = $"Revenue detail for amenity: {cbAmenities.Text} in {TimeOrMonthOrQuarterOrYear} {Year}";

                var quarter = int.Parse(TimeOrMonthOrQuarterOrYear.Last().ToString());
                var from = (quarter * 2) + (quarter - 2);
                var to = quarter * 3;

                int poinIndex = 0;
                for (int j = from; j <= to; j++)
                {
                    var month = new DateTime(2018, j, 1).ToString("MMMM");
                    string value;

                    if (cbAmenityViewType.Text == "Revenue")
                    {
                        chartRevenueFromAmenities.Series[0].Name = "Revenue";
                        value = amenitiesRevenue.Where(t => t.Ticket.Schedule.Date.Month == j).Sum(t => t.Price).ToString("C0");
                    }
                    else
                    {
                        chartRevenueFromAmenities.Series[0].Name = "Quantity";
                        value = amenitiesRevenue.Count(t => t.Ticket.Schedule.Date.Month == j).ToString();
                    }

                    chartRevenueFromAmenities.Series[0].Points.AddXY(month, value);
                    chartRevenueFromAmenities.Series[0].Points[poinIndex].Label = value;

                    poinIndex++;
                }

            }
            else if (ViewMode == "rdbByYear")
            {
                chartRevenueFromAmenities.Titles[0].Text = $"Revenue detail for amenity: {cbAmenities.Text} in {Year}";

                int pointIndex = 0;
                for (int j = 1; j < 13; j++)
                {
                    var month = new DateTime(2018, j, 1).ToString("MMMM");
                    string value;

                    if (cbAmenityViewType.Text == "Revenue")
                        value = amenitiesRevenue.Where(t => t.Ticket.Schedule.Date.Month == j).Sum(t => t.Price).ToString("C0");
                    else
                        value = amenitiesRevenue.Count(t => t.Ticket.Schedule.Date.Month == j).ToString();

                    chartRevenueFromAmenities.Series[0].Points.AddXY(month, value);
                    chartRevenueFromAmenities.Series[0].Points[pointIndex].Label = value;

                    pointIndex++;
                }
            }
        }

        private void LoadChartForAllAmenities()
        {
            DeleteSeriesOfChartRevenueFromAmenities();

            chartRevenueFromAmenities.Series[0].ChartType = SeriesChartType.Column;

            var amenities = Db.Context.Amenities.Where(t => t.Price != 0).ToList();
            var amenitiesRevenue = Tickets.SelectMany(t => t.AmenitiesTickets).GroupBy(t => t.Amenity).ToList();

            if (ViewMode == "rdbByTime")
            {
                chartRevenueFromAmenities.Titles[0].Text = $"Revenue detail for all amenities on {TimeOrMonthOrQuarterOrYear}";
            }
            else if (ViewMode == "rdbByWeek")
            {
                chartRevenueFromAmenities.Titles[0].Text = $"Revenue detail for all amenities on {TimeOrMonthOrQuarterOrYear} this week";
            }
            else if (ViewMode == "rdbByMonth")
            {
                chartRevenueFromAmenities.Titles[0].Text = $"Revenue detail for all amenities on {TimeOrMonthOrQuarterOrYear} {Year}";
            }
            else if (ViewMode == "rdbByQuarter")
            {
                chartRevenueFromAmenities.Titles[0].Text = $"Revenue detail for all amenities in {TimeOrMonthOrQuarterOrYear} {Year}";
            }
            else if (ViewMode == "rdbByYear")
            {
                chartRevenueFromAmenities.Titles[0].Text = $"Revenue detail for all amenities in {Year}";
            }

            int i = 0;
            foreach (var item in amenities)
            {
                string value;

                if (cbAmenityViewType.Text == "Revenue")
                {
                    chartRevenueFromAmenities.Series[0].Name = "Revenue";
                    value = amenitiesRevenue.Where(t => t.Key.ID == item.ID).Sum(t => t.Sum(k => k.Price)).ToString("C0");
                }
                else
                {
                    chartRevenueFromAmenities.Series[0].Name = "Quantity";
                    value = amenitiesRevenue.Where(t => t.Key.ID == item.ID).Sum(t => t.Count()).ToString();
                }

                chartRevenueFromAmenities.Series[0].Points.AddXY(item.Service, value);
                chartRevenueFromAmenities.Series[0].Points[i].Label = value;

                i++;
            }
        }

        private void CbAmenityViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CbAmenities_SelectedIndexChanged(cbAmenities, new EventArgs());
        }

        #endregion
    }
}
