using AirportManagerSystem.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ImportChangeWindow.xaml
    /// </summary>
    public partial class ImportChangeWindow : Window
    {
        public FlightManagementWindow ManageWindow { get; internal set; }

        public ImportChangeWindow()
        {
            InitializeComponent();
            this.Loaded += ImportChangeWindow_Loaded;
        }

        private void ImportChangeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tblDuplicate.Text = tblMissing.Text = tblSuccessful.Text = "";
        }

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            tblDuplicate.Text = tblMissing.Text = tblSuccessful.Text = "";
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "File text, csv|*.txt;*.csv";

            if (o.ShowDialog().Value)
            {
                txtFilePath.Text = o.FileName;
            }

            ToolTip t = new ToolTip();
            t.Content = txtFilePath.Text;
            txtFilePath.ToolTip = t;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (txtFilePath.Text == "")
            {
                MessageBox.Show("Please select the text file with the change", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StreamReader sr;
            try
            {
                sr = new StreamReader(txtFilePath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            string line;
            int lineNumber = 1;
            string duplicateIndexsInImportedFile = " (Lines: ";
            string discardedIndexsInImportedFile = "(Lines: ";
            int success = 0, duplicate = 0, missing = 0;

            while ((line = sr.ReadLine()) != null)
            {
                var splited = line.Split(',');

                if (splited.Length != 9)
                {
                    missing++;
                    discardedIndexsInImportedFile += lineNumber + ", ";
                }
                else
                {
                    try
                    {
                        var from = splited[4];
                        var to = splited[5];
                        var routeId = Db.Context.Routes.Where(t => t.Airport.IATACode == from && t.Airport1.IATACode == to).FirstOrDefault().ID;
                        var aircraft = Db.Context.Aircrafts.Find(int.Parse(splited[6]));

                        if (splited[0] == "ADD")
                        {
                            Schedule flight = new Schedule();
                            flight.Date = DateTime.Parse(splited[1]);
                            flight.FlightNumber = splited[3];
                            flight.Time = TimeSpan.Parse(splited[2]);
                            flight.Confirmed = splited[8] == "OK";
                            flight.Aircraft = aircraft;
                            flight.EconomyPrice = decimal.Parse(splited[7]);
                            flight.RouteID = routeId;

                            if (Db.Context.Schedules.Where(t => t.Date == flight.Date && t.FlightNumber == flight.FlightNumber).FirstOrDefault() != null)
                            {
                                duplicate++;
                                duplicateIndexsInImportedFile += lineNumber + ", ";
                            }
                            else
                            {
                                Db.Context.Schedules.Add(flight);
                                Db.Context.SaveChanges();
                                success++;
                            }
                        }
                        else
                        {
                            var Date = DateTime.Parse(splited[1]);
                            var FlightNumber = splited[3];

                            var flight = Db.Context.Schedules.Where(t => t.Date == Date && t.FlightNumber == FlightNumber).FirstOrDefault();

                            flight.Time = TimeSpan.Parse(splited[2]);
                            flight.Confirmed = splited[8] == "OK";
                            flight.Aircraft = aircraft;
                            flight.EconomyPrice = decimal.Parse(splited[7]);
                            flight.RouteID = routeId;

                            Db.Context.SaveChanges();

                            success++;
                        }
                    }
                    catch (Exception)
                    {
                        missing++;
                        discardedIndexsInImportedFile += lineNumber + ", ";
                    }
                }

                lineNumber++;
            }

            sr.Close();

            tblSuccessful.Text = success.ToString();
            tblDuplicate.Text = duplicate.ToString() + (duplicate == 0 ? "" : duplicateIndexsInImportedFile.Remove(duplicateIndexsInImportedFile.Length - 2) + ")");
            tblMissing.Text = missing.ToString() + (missing == 0 ? "" : discardedIndexsInImportedFile.Remove(discardedIndexsInImportedFile.Length - 2) + ")");

            ManageWindow.LoadFlights();

            txtFilePath.Text = "";
            MessageBox.Show("Import completed");
        }
    }
}
