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
    public partial class RevenueReportWindow : Form
    {
        List<Color> colors = new List<Color>()
        {
            Color.FromArgb(0,160, 187),
            Color.FromArgb(6,75, 102),
            Color.FromArgb(237,214, 136),
            Color.FromArgb(194,145, 46),
            Color.FromArgb(250,200, 38),
            Color.FromArgb(13,79, 76),
            Color.FromArgb(247,148, 32),
        };
        public RevenueReportWindow()
        {
            InitializeComponent();

            RevenueReportByRoute();
        }

        private void RevenueReportWindow_Load(object sender, EventArgs e)
        {
            cbCriteria.SelectedIndex = 0;

            ResetRouteTab();
            AddAnnotationForChartRevenueDetail();

        }

        #region Revenue by Route
        List<DateTime> datesHaveData = new List<DateTime>();
        List<double> revenueDetails = new List<double>();
        HorizontalLineAnnotation an;
        Color previous;
        int selected = -1;
        double nowAnchor;
        ToolTip t = new ToolTip();

        private void AddAnnotationForChartRevenueDetail()
        {
            an = new HorizontalLineAnnotation();
            an.AxisX = chartRevenueRouteDetail.ChartAreas[0].AxisX;
            an.AxisY = chartRevenueRouteDetail.ChartAreas[0].AxisY;
            an.ClipToChartArea = chartRevenueRouteDetail.ChartAreas[0].Name;
            an.LineColor = Color.Red;
            an.LineWidth = 3;
            an.IsInfinitive = true;
            chartRevenueRouteDetail.Annotations.Add(an);
        }

        private void RevenueReportByRoute()
        {
            chartRevenueSummary.MouseMove += chartSummary_MouseMove;
            chartRevenueSummary.MouseClick += chartSummary_MouseClick;
            chartRevenueSummary.MouseLeave += chartSummary_MouseLeave;

            chartRevenueRouteDetail.MouseMove += ChartRevenueDetail_MouseMove;
            chartRevenueRouteDetail.MouseLeave += ChartRevenueDetail_MouseLeave;
            chartRevenueRouteDetail.Click += ChartRevenueDetail_Click;

            chartRevenueRouteDetail.ChartAreas[0].AxisX.Interval = 1;
        }

        private void ChartRevenueDetail_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < revenueDetails.Count; i++)
            {
                if (revenueDetails[i] < an.AnchorY)
                {
                    foreach (var item in chartRevenueRouteDetail.Series)
                    {
                        item.Points[i].Color = Color.FromArgb(125, item.Color);
                    }
                }
                else
                {
                    foreach (var item in chartRevenueRouteDetail.Series)
                    {
                        item.Points[i].Color = item.Color;
                    }
                }
            }

            nowAnchor = an.AnchorY;
            filterBySelectedToolStripMenuItem.Visible = true;
        }

        private void chartSummary_MouseLeave(object sender, EventArgs e)
        {
            chartSummary_MouseMove(sender, new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1));
        }

        private void ChartRevenueDetail_MouseLeave(object sender, EventArgs e)
        {
            ChartRevenueDetail_MouseMove(sender, new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1));
        }

        private void ChartRevenueDetail_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y <= -1)
            {
                an.Visible = false;
                return;
            }
            else
            {
                an.Visible = true;
                t.SetToolTip(chartRevenueRouteDetail, an.AnchorY.ToString("C0"));
            }

            an.BeginPlacement();
            an.AnchorY = chartRevenueRouteDetail.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
            an.EndPlacement();
        }

        private void chartSummary_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult rs = chartRevenueSummary.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            if (rs.PointIndex >= 0 && rs.Series != null)
            {
                if (rs.Series.Points[rs.PointIndex].BorderColor == Color.White)
                {
                    rs.Series.Points[rs.PointIndex].BorderColor = Color.Transparent;
                }
                else
                {
                    rs.Series.Points[rs.PointIndex].BorderColor = Color.White;
                    rs.Series.Points[rs.PointIndex].BorderWidth = 3;
                }

                LoadChartRevenueDetail();
            }
        }

        private List<Ticket> GetTickets(string criteriaName)
        {
            var date = dtpTime.Value.Date;
            var tickets = new List<Ticket>();

            if (cbCriteria.Text == "Route")
            {
                var departure = criteriaName.Split('-')[0];
                var arrival = criteriaName.Split('-')[1];

                tickets = Db.Context.Schedules.Where(t => t.Route.Airport.IATACode == departure && t.Route.Airport1.IATACode == arrival && t.Confirmed && t.Date.Month == date.Month && t.Date.Year == date.Year).SelectMany(t => t.Tickets).Where(t => t.Confirmed).ToList();
            }
            else if (cbCriteria.Text == "Departure Airport")
            {
                tickets = Db.Context.Schedules.Where(t => t.Route.Airport.IATACode == criteriaName && t.Confirmed && t.Date.Month == date.Month && t.Date.Year == date.Year).SelectMany(t => t.Tickets).Where(t => t.Confirmed).ToList();
            }
            else
            {
                tickets = Db.Context.Schedules.Where(t => t.Route.Airport1.IATACode == criteriaName && t.Confirmed && t.Date.Month == date.Month && t.Date.Year == date.Year).SelectMany(t => t.Tickets).Where(t => t.Confirmed).ToList();
            }

            return tickets;
        }

        private void LoadChartRevenueDetail()
        {
            datesHaveData = new List<DateTime>();

            foreach (var item in chartRevenueSummary.Series[0].Points)
            {
                if (item.BorderColor == Color.White)
                {
                    var title = item.AxisLabel;
                    var tickets = GetTickets(title);

                    var dates = tickets.Select(t => t.Schedule.Date).Distinct().ToList();

                    foreach (var d in dates)
                    {
                        if (datesHaveData.Contains(d) == false)
                            datesHaveData.Add(d);
                    }
                }
            }

            datesHaveData = datesHaveData.OrderBy(t => t).ToList();

            chartRevenueRouteDetail.Series.Clear();
            foreach (var item in chartRevenueSummary.Series[0].Points)
            {
                if (item.BorderColor == Color.White)
                {
                    var title = item.AxisLabel;

                    chartRevenueRouteDetail.Series.Add(title);
                    chartRevenueRouteDetail.Series[chartRevenueRouteDetail.Series.Count - 1].Color = colors[chartRevenueRouteDetail.Series.Count - 1];
                    chartRevenueRouteDetail.Series[chartRevenueRouteDetail.Series.Count - 1].ChartType = SeriesChartType.StackedColumn;

                    var tickets = GetTickets(title);

                    for (int i = 0; i < datesHaveData.Count; i++)
                    {
                        var revenue = UpdateRevenue(tickets.Where(t => t.Schedule.Date == datesHaveData[i]).ToList());
                        chartRevenueRouteDetail.Series[chartRevenueRouteDetail.Series.Count - 1].Points.AddXY(datesHaveData[i].ToString("dd/MM/yyyy"), revenue);
                    }
                }
            }

            if (chartRevenueRouteDetail.Series.Count == 0)
                chartRevenueRouteDetail.Visible = false;
            else chartRevenueRouteDetail.Visible = true;

            chartRevenueRouteDetail.Series.Add("Nhok-Add");
            chartRevenueRouteDetail.Series["Nhok-Add"].IsVisibleInLegend = false;
            chartRevenueRouteDetail.Series["Nhok-Add"].Color = Color.Transparent;

            CalculateTotalForEachStackColumnInChartDetail();

            filterBySelectedToolStripMenuItem.Visible = false;
            resetFilterToolStripMenuItem.Visible = false;
            hideLabelToolStripMenuItem.Text = "Show label";
        }

        private void chartSummary_MouseMove(object sender, MouseEventArgs e)
        {
            HitTestResult rs = chartRevenueSummary.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            if (rs.PointIndex >= 0 && rs.Series != null)
            {
                if (selected != rs.PointIndex)
                {
                    if (selected != -1)
                    {
                        rs.Series.Points[selected].Color = previous;
                        rs.Series.Points[selected].BackHatchStyle = ChartHatchStyle.None;
                        rs.Series.Points[selected].BackSecondaryColor = Color.Transparent;
                    }

                    selected = rs.PointIndex;
                    previous = rs.Series.Points[rs.PointIndex].Color;

                    rs.Series.Points[rs.PointIndex].Color = Color.White;
                    rs.Series.Points[rs.PointIndex].BackHatchStyle = ChartHatchStyle.DottedDiamond;
                    rs.Series.Points[rs.PointIndex].BackSecondaryColor = Color.Black;
                }
            }
            else if (rs.Series == null && selected != -1)
            {
                chartRevenueSummary.Series[0].Points[selected].Color = previous;
                chartRevenueSummary.Series[0].Points[selected].BackHatchStyle = ChartHatchStyle.None;
                chartRevenueSummary.Series[0].Points[selected].BackSecondaryColor = Color.Transparent;

                selected = -1;
            }
        }

        private void ResetRouteTab()
        {
            dgvRevenueOfRouteData.Rows.Clear();
            dgvRevenueOfRouteData.Visible = false;
            chartRevenueSummary.Visible = false;
            chartRevenueRouteDetail.Visible = false;

            foreach (var item in chartRevenueSummary.Series)
            {
                item.Points.Clear();
            }
            foreach (var item in chartRevenueRouteDetail.Series)
            {
                item.Points.Clear();
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            ResetRouteTab();

            //Departure airport case
            var report = new List<RevenueOfRoute>();

            if (cbCriteria.Text == "Departure Airport")
            {
                dgvRevenueOfRouteData.Columns[0].HeaderText = "Departure Airport";

                report = Db.Context.Airports.Select(t => new RevenueOfRoute
                {
                    IATACodeOfDeparture = t.IATACode,
                    Tickets = Db.Context.Schedules.Where(k => k.Route.Airport.IATACode == t.IATACode && k.Confirmed && k.Date.Month == dtpTime.Value.Month && k.Date.Year == dtpTime.Value.Year).SelectMany(k => k.Tickets).Where(k => k.Confirmed).ToList(),
                    Revenue = 0.0
                }).ToList();
            }
            else if (cbCriteria.Text == "Route")
            {
                dgvRevenueOfRouteData.Columns[0].HeaderText = "Route";

                var routes = Db.Context.Routes.OrderByDescending(t => t.Schedules.SelectMany(k => k.Tickets).Count()).Take(7).ToList();

                report = routes.Select(t => new RevenueOfRoute
                {
                    IATACodeOfDeparture = t.Airport.IATACode,
                    IATACodeOfArrival = t.Airport1.IATACode,
                    Tickets = Db.Context.Schedules.Where(k => k.Route.Airport.IATACode == t.Airport.IATACode && k.Route.Airport1.IATACode == t.Airport1.IATACode && k.Confirmed && k.Date.Month == dtpTime.Value.Month && k.Date.Year == dtpTime.Value.Year).SelectMany(k => k.Tickets).Where(k => k.Confirmed).ToList(),
                    Revenue = 0.0
                }).ToList();
            }
            else
            {
                dgvRevenueOfRouteData.Columns[0].HeaderText = "Arrival Airport";

                report = Db.Context.Airports.Select(t => new RevenueOfRoute
                {
                    IATACodeOfArrival = t.IATACode,
                    Tickets = Db.Context.Schedules.Where(k => k.Route.Airport1.IATACode == t.IATACode && k.Confirmed && k.Date.Month == dtpTime.Value.Month && k.Date.Year == dtpTime.Value.Year).SelectMany(k => k.Tickets).Where(k => k.Confirmed).ToList(),
                    Revenue = 0.0
                }).ToList();
            }

            foreach (var item in report)
            {
                item.Revenue = UpdateRevenue(item.Tickets);
            }

            foreach (var item in report)
            {
                if (cbCriteria.Text == "Route")
                {
                    dgvRevenueOfRouteData.Rows.Add(item.IATACodeOfDeparture + "-" + item.IATACodeOfArrival, item.Revenue.ToString("C0"));
                    chartRevenueSummary.Series[0].Points.AddXY(item.IATACodeOfDeparture + "-" + item.IATACodeOfArrival, item.Revenue);
                }
                else if (cbCriteria.Text == "Departure Airport")
                {
                    dgvRevenueOfRouteData.Rows.Add(item.IATACodeOfDeparture, item.Revenue.ToString("C0"));
                    chartRevenueSummary.Series[0].Points.AddXY(item.IATACodeOfDeparture, item.Revenue);
                }
                else
                {
                    dgvRevenueOfRouteData.Rows.Add(item.IATACodeOfArrival, item.Revenue.ToString("C0"));
                    chartRevenueSummary.Series[0].Points.AddXY(item.IATACodeOfArrival, item.Revenue);
                }

                chartRevenueSummary.Series[0].Points[chartRevenueSummary.Series[0].Points.Count - 1].Color = colors[chartRevenueSummary.Series[0].Points.Count - 1];
            }

            dgvRevenueOfRouteData.Visible = true;
            chartRevenueSummary.Visible = true;
        }

        private double UpdateRevenue(List<Ticket> tickets)
        {
            double revenue = 0;
            foreach (var item in tickets)
            {
                revenue += Flight.GetPrice(item.Schedule, item.CabinType);
            }

            return revenue;
        }

        private void HideLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hideLabelToolStripMenuItem.Text == "Show label")
            {
                hideLabelToolStripMenuItem.Text = "Hide label";

                for (int i = 0; i < chartRevenueRouteDetail.Series[0].Points.Count; i++)
                {
                    chartRevenueRouteDetail.Series["Nhok-Add"].Points[i].Label = revenueDetails[i].ToString("C0");
                }
            }
            else
            {
                hideLabelToolStripMenuItem.Text = "Show label";
                for (int i = 0; i < chartRevenueRouteDetail.Series[0].Points.Count; i++)
                {
                    chartRevenueRouteDetail.Series["Nhok-Add"].Points[i].Label = "";
                }
            }
        }

        private void resetFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadChartRevenueDetail();
        }

        private void CalculateTotalForEachStackColumnInChartDetail()
        {
            revenueDetails = new List<double>();
            for (int i = 0; i < chartRevenueRouteDetail.Series[0].Points.Count; i++)
            {
                double total = 0;
                foreach (var item in chartRevenueRouteDetail.Series)
                {
                    var date = datesHaveData[i];

                    var tickets = GetTickets(item.Name);

                    if (cbCriteria.Text == "Route")
                    {
                        var departure = item.Name.Split('-')[0];
                        var arrival = item.Name.Split('-')[1];

                        tickets = tickets.Where(t => t.Schedule.Route.Airport.IATACode == departure && t.Schedule.Route.Airport1.IATACode == arrival && t.Schedule.Date == date && t.Confirmed).ToList();
                    }
                    else if (cbCriteria.Text == "Departure Airport")
                    {
                        tickets = tickets.Where(t => t.Schedule.Route.Airport.IATACode == item.Name && t.Schedule.Date == date && t.Confirmed).ToList();
                    }
                    else
                    {
                        tickets = tickets.Where(t => t.Schedule.Route.Airport1.IATACode == item.Name && t.Schedule.Date == date && t.Confirmed).ToList();
                    }

                    total += UpdateRevenue(tickets);
                }

                revenueDetails.Add(total);

                chartRevenueRouteDetail.Series["Nhok-Add"].Points.AddXY("NhokATB", total);
            }
        }

        private void filterBySelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newDates = new List<DateTime>();
            var newTotals = new List<double>();

            for (int i = 0; i < revenueDetails.Count; i++)
            {
                if (revenueDetails[i] > nowAnchor)
                {
                    newDates.Add(datesHaveData[i]);
                    newTotals.Add(revenueDetails[i]);
                }
            }

            revenueDetails = new List<double>(newTotals);
            datesHaveData = new List<DateTime>(newDates);

            chartRevenueRouteDetail.Series.Clear();
            foreach (var item in chartRevenueSummary.Series[0].Points)
            {
                if (item.BorderColor == Color.White)
                {
                    var title = item.AxisLabel;

                    chartRevenueRouteDetail.Series.Add(title);
                    chartRevenueRouteDetail.Series[chartRevenueRouteDetail.Series.Count - 1].Color = colors[chartRevenueRouteDetail.Series.Count - 1];
                    chartRevenueRouteDetail.Series[chartRevenueRouteDetail.Series.Count - 1].ChartType = SeriesChartType.StackedColumn;

                    var tickets = GetTickets(title);

                    for (int i = 0; i < datesHaveData.Count; i++)
                    {
                        var revenue = UpdateRevenue(tickets.Where(t => t.Schedule.Date == datesHaveData[i]).ToList());
                        chartRevenueRouteDetail.Series[chartRevenueRouteDetail.Series.Count - 1].Points.AddXY(datesHaveData[i].ToString("dd/MM/yyyy"), revenue);
                    }
                }
            }

            if (chartRevenueRouteDetail.Series.Count == 0)
                chartRevenueRouteDetail.Visible = false;
            else chartRevenueRouteDetail.Visible = true;

            chartRevenueRouteDetail.Series.Add("Nhok-Add");
            chartRevenueRouteDetail.Series["Nhok-Add"].IsVisibleInLegend = false;
            chartRevenueRouteDetail.Series["Nhok-Add"].Color = Color.Transparent;

            CalculateTotalForEachStackColumnInChartDetail();

            resetFilterToolStripMenuItem.Visible = true;
            filterBySelectedToolStripMenuItem.Visible = false;
            hideLabelToolStripMenuItem.Text = "Show label";
        }

        private void cbCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartRevenueSummary.Titles[0].Text = $"Rate of revenue of {cbCriteria.Text}";
            chartRevenueRouteDetail.Titles[0].Text = $"Revenue detail of {cbCriteria.Text}";

            ResetRouteTab();
        }
        #endregion
    }
}
