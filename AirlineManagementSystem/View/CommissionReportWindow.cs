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
using excel = Microsoft.Office.Interop.Excel;

namespace AirportManagerSystem.View
{
    public partial class CommissionReportWindow : Form
    {
        public CommissionReportWindow()
        {
            InitializeComponent();
            chartDetail.MouseClick += ChartCommission_MouseClick;
            chartDetail.ChartAreas[0].AxisX.Interval = 1;
        }

        private void ChartCommission_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult rs = chartDetail.HitTest(e.X, e.Y, ChartElementType.DataPoint);

            if (rs.PointIndex >= 0 && rs.Series != null)
            {
                if (rs.Series.Points[rs.PointIndex].YValues[0] == 0)
                {
                    return;
                }

                CommissionDetailWindow f = new CommissionDetailWindow();
                f.User = rs.Series.Points[rs.PointIndex].AxisLabel;
                f.Type = rs.Series.Name;

                this.Hide();
                f.ShowDialog();
                this.Show();
            }
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
            var years = Db.Context.Schedules.Select(t => t.Date.Year).Distinct().OrderBy(t => t).ToList();
            cbYear.DataSource = years;

            LoadData();

            this.Cursor = Cursors.Default;
        }

        private double UpdateCommission(List<Ticket> tickets)
        {
            double revenue = 0;
            foreach (var item in tickets)
            {
                double price = (int)item.Schedule.EconomyPrice;
                double bprice = Math.Floor(price * 1.35);
                double fprice = Math.Floor(bprice * 1.3);

                double amenprice = item.AmenitiesTickets.Sum(t => (int?)t.Price) ?? 0;

                revenue += amenprice;
                revenue += item.CabinTypeID == 1 ? price : (item.CabinTypeID == 2 ? bprice : fprice);
            }

            return revenue * 0.003;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "File Excel|*.xls";
            if (s.ShowDialog() == DialogResult.OK)
            {
                string path = s.FileName;
                reportViewer1.ExportDialog(reportViewer1.LocalReport.ListRenderingExtensions()[0], "", path);

                try
                {
                    excel.Application app = new excel.Application();
                    excel.Workbook book = app.Workbooks.Open(path);
                    excel.Worksheet sheet = book.Sheets[1];

                    excel.ChartObjects charts = (excel.ChartObjects)sheet.ChartObjects(Type.Missing);
                    excel.ChartObject chart = charts.Add(20, 100, 700, 250);

                    chart.Chart.SetSourceData(sheet.UsedRange);
                    chart.Chart.ChartType = excel.XlChartType.xlColumnClustered;

                    book.Save();
                    book.Close(true);
                    app.Quit();

                    MessageBox.Show($"File was created at {path}");
                }
                catch (Exception)
                {
                    MessageBox.Show("Missing library");
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgvCommission.Rows.Clear();
            chartDetail.Series[0].Points.Clear();
            chartDetail.Series[1].Points.Clear();
            chartDetail.Series[2].Points.Clear();

            var users = Db.Context.Users.Where(t=>t.Role.Title == "User").ToList();
            var userReports = new List<UserReport>();
            foreach (var item in users)
            {
                var ur = new UserReport()
                {
                    User = item,
                    Commission = 0
                };

                if (rdbByDate.Checked)
                {
                    var date = dtpDate.Value.Date;

                    ur.Amenities = item.Tickets.Where(k => k.Confirmed && k.Schedule.Date == date).SelectMany(k => k.AmenitiesTickets).ToList();
                    ur.Tickets = item.Tickets.Where(k => k.Confirmed && k.Schedule.Date == date).ToList();
                }
                else if(rdbByMonth.Checked)
                {
                    var month = dtpMonth.Value.Date.Month;
                    var year = dtpMonth.Value.Date.Year;

                    ur.Amenities = item.Tickets.Where(k => k.Confirmed && k.Schedule.Date.Month == month && k.Schedule.Date.Year == year).SelectMany(k => k.AmenitiesTickets).ToList();
                    ur.Tickets = item.Tickets.Where(k => k.Confirmed && k.Schedule.Date.Month == month && k.Schedule.Date.Year == year).ToList();
                }
                else
                {
                    var year = int.Parse(cbYear.Text);

                    ur.Amenities = item.Tickets.Where(k => k.Confirmed && k.Schedule.Date.Year == year).SelectMany(k => k.AmenitiesTickets).ToList();
                    ur.Tickets = item.Tickets.Where(k => k.Confirmed && k.Schedule.Date.Year == year).ToList();
                }

                userReports.Add(ur);
            }

            userReports = userReports.OrderByDescending(t => t.Tickets.Count()).ToList();

            foreach (var item in userReports)
            {
                item.Commission = UpdateCommission(item.Tickets);
            }

            CommissionDataSet.CommisstionReportDataTable dt = new CommissionDataSet.CommisstionReportDataTable();

            foreach (var item in userReports)
            {
                dgvCommission.Rows.Add(item.User.FirstName + " " + item.User.LastName, item.Amenities.Count, item.Tickets.Count, item.Commission.ToString("C2"));

                dt.AddCommisstionReportRow(item.User.FirstName + " " + item.User.LastName, "Amennities Sold", item.Amenities.Count);
                dt.AddCommisstionReportRow(item.User.FirstName + " " + item.User.LastName, "Tickets Sold", item.Tickets.Count);
                dt.AddCommisstionReportRow(item.User.FirstName + " " + item.User.LastName, "Commission Earned", item.Commission);

                chartDetail.Series[0].Points.AddXY(item.User.FirstName + " " + item.User.LastName, item.Amenities.Count);
                chartDetail.Series[1].Points.AddXY(item.User.FirstName + " " + item.User.LastName, item.Tickets.Count);
                chartDetail.Series[2].Points.AddXY(item.User.FirstName + " " + item.User.LastName, item.Commission);
            }

            ReportBindingSource.DataSource = dt;
            reportViewer1.RefreshReport();
        }
    }

    internal class UserReport
    {
        public List<AmenitiesTicket> Amenities { get; set; }
        public double Commission { get; set; }
        public List<Ticket> Tickets { get; set; }
        public User User { get; set; }
    }
}

