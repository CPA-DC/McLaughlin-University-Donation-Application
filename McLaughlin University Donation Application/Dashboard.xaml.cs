/* Dashboard.xaml.cs         
 *
 * @author      DBAS6206-05 McLaughlin University
 * @version     1
 * @since       2022.10.01
 */

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
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace McLaughlin_University_Donation_Application
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        // member id variable
        public string memberID;
        public DataTable dt = new DataTable();
        private List<Donor_Type> DonorTypes { get; set; }
        private float percentC, percentF, percentI, countTotal;

        public Dashboard(string memberID)
        {
            InitializeComponent();
            this.memberID = memberID;

            // Pie Chart variables
            float pieWidth = 325, pieHeight = 325, centerX = pieWidth / 2, centerY = pieHeight / 2, radius = pieWidth / 2;
            mainCanvas.Width = pieWidth;
            mainCanvas.Height = pieHeight;

            PieChart(centerX, centerY, radius);
            //MessageBox.Show("" + countIndividuals);
        }

        #region Sidebar Profile

        private void NavJobInfoClick(object sender, RoutedEventArgs e)
        {
            ProfileInformation profileInformation = new ProfileInformation(memberID);
            this.Close();
            profileInformation.Show();
        }

        private void NavTargetsClick(object sender, RoutedEventArgs e)
        {
           ProfilePledgeTargets profilePledgeTargets = new ProfilePledgeTargets(memberID);
            this.Close();
            profilePledgeTargets.Show();
        }

        private void NavLogout(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        #endregion

        #region Sidebar Tracking
        private void TrackingClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void NavDonorsClick(object sender, RoutedEventArgs e)
        {
            TrackingDonor trackingDonor = new TrackingDonor(memberID);
            this.Close();
            trackingDonor.Show();
        }

        private void NavPledgesClick(object sender, RoutedEventArgs e)
        {
            TrackingPledges trackingPledges = new TrackingPledges(memberID);
            this.Close();
            trackingPledges.Show();
        }

        private void NavDonationsClick(object sender, RoutedEventArgs e)
        {
            TrackingDonations trackingDonations = new TrackingDonations(memberID);
            this.Close();
            trackingDonations.Show();
        }
        #endregion

        #region Interface

        private void GenerateReportClick(object sender, RoutedEventArgs e)
        {

        }

        private void ImportDataClick(object sender, RoutedEventArgs e)
        {

        }

        private void ContributionClick(object sender, RoutedEventArgs e)
        {
            // Type contribution percentages
            buttonContribution.Foreground = Brushes.DeepSkyBlue;
            buttonVariety.Foreground = labelTitle.Foreground;
            float pieWidth = 325, pieHeight = 325, centerX = pieWidth / 2, centerY = pieHeight / 2, radius = pieWidth / 2;
            mainCanvas.Width = pieWidth;
            mainCanvas.Height = pieHeight;
            PieChart(centerX, centerY, radius);
        }

        private void VarietyClick(object sender, RoutedEventArgs e)
        {
            // Type variety percentages
            buttonVariety.Foreground = Brushes.DeepSkyBlue;
            buttonContribution.Foreground = labelTitle.Foreground;
            float pieWidth = 325, pieHeight = 325, centerX = pieWidth / 2, centerY = pieHeight / 2, radius = pieWidth / 2;
            mainCanvas.Width = pieWidth;
            mainCanvas.Height = pieHeight;
            PieChart(centerX, centerY, radius);
        }
        #endregion

        #region PieMethods
        private void PieChart(float centerX, float centerY, float radius)
        {
            // Pie chart based on donor type contributions
            if (buttonContribution.Foreground == Brushes.DeepSkyBlue)
            {
                TypeContributions();
            }
            else
            {
                TypeVariety();
                //TypeContributions();
            }

            DonorTypes = new List<Donor_Type>()
            {
                #region test
                new Donor_Type
                {
                    Title = "Individual",
                    Percentage = percentI,
                    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4472C4")),
                },

                new Donor_Type
                {
                    Title = "Corporation",
                    Percentage = percentC,
                    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ED7D31")),
                },

                new Donor_Type
                {
                    Title = "Foundation",
                    Percentage = percentF,
                    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC000")),
                },
                #endregion
            };

            detailsItemsControl.ItemsSource = DonorTypes;

            DrawPie(centerX, centerY, radius);

        }

        private void TypeVariety()
        {
            labelTitle.Content = "Funding Overview (count by donor type)";
            int countCorporations, countFoundations, countIndividuals;
            dt.Clear();
            try
            {
                //step 1 : get connection string from settings.
                string connectstring = Properties.Settings.Default.connectionString;

                //step 2: create a connection object.
                SqlConnection conn = new SqlConnection(connectstring);

                //Step 3: open that connection.
                conn.Open();

                //Step 4: I need to create an SQL query.
                string SelectQuery = "EXEC Donor_Type_Variety";

                //Step 5: Create an SQL command.
                SqlCommand command = new SqlCommand(SelectQuery, conn);
                SqlDataAdapter sqlData = new SqlDataAdapter(command);
                sqlData.Fill(dt);

                countCorporations = Convert.ToInt32(dt.Rows[0]["type_count"]);
                countFoundations = Convert.ToInt32(dt.Rows[1]["type_count"]);
                countIndividuals = Convert.ToInt32(dt.Rows[2]["type_count"]);

                labelCorporation2.Content = countCorporations;
                labelFoundation2.Content = countFoundations;
                labelIndividual2.Content = countIndividuals;

                // Close the connection.
                conn.Close();
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            countTotal = countCorporations + countFoundations + countIndividuals;
            percentC = (float)Math.Round(((countCorporations / countTotal) * 100));
            percentF = (float)Math.Round(((countFoundations / countTotal) * 100));
            percentI = (float)Math.Round((100 - (percentC + percentF)));
        }

        private void TypeContributions()
        {
            labelTitle.Content = "Funding Overview (contribution by donor type)";
            double contributionCorporations, contributionFoundations, contributionIndividuals, contributionTotal;
            dt.Clear();
            try
            {
                //step 1 : get connection string from settings.
                string connectstring = Properties.Settings.Default.connectionString;

                //step 2: create a connection object.
                SqlConnection conn = new SqlConnection(connectstring);

                //Step 3: open that connection.
                conn.Open();

                //Step 4: I need to create an SQL query.
                string SelectQuery = "EXEC Donor_Type_Contributions";

                //Step 5: Create an SQL command.
                SqlCommand command = new SqlCommand(SelectQuery, conn);
                SqlDataAdapter sqlData = new SqlDataAdapter(command);
                sqlData.Fill(dt);

                contributionCorporations = Convert.ToDouble(dt.Rows[0]["amount"]);
                contributionFoundations = Convert.ToDouble(dt.Rows[1]["amount"]);
                contributionIndividuals = Convert.ToDouble(dt.Rows[2]["amount"]);

                labelCorporation2.Content = "$" + contributionCorporations;
                labelFoundation2.Content = "$" + contributionFoundations;
                labelIndividual2.Content = "$" + contributionIndividuals;

                // Close the connection.
                conn.Close();
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            contributionTotal = contributionCorporations + contributionFoundations + contributionIndividuals;
            percentC = (float)Math.Round(((contributionCorporations / contributionTotal) * 100));
            percentF = (float)Math.Round(((contributionFoundations / contributionTotal) * 100));
            percentI = (float)Math.Round((100 - (percentC + percentF)));
        }

        public void DrawPie(float centerX, float centerY, float radius)
        {
            // The logic for developing our pie chart was adopted from a GitHub repository:
            // Sulthan, K. (2020, October 5). Charts/2DPieChart at master · Kareemsulthan07/Charts. GitHub.
            // Retrieved November 30, 2022, from https://github.com/kareemsulthan07/Charts/tree/master/2DPieChart 
            float angle = 0, prevAngle = 0;
            foreach (var type in DonorTypes)
            {
                double line1X = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                double line1Y = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                angle = type.Percentage * (float)360 / 100 + prevAngle;
                Debug.WriteLine(angle);

                double arcX = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                double arcY = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                var line1Segment = new LineSegment(new Point(line1X, line1Y), false);
                double arcWidth = radius, arcHeight = radius;
                bool isLargeArc = type.Percentage > 50;
                var arcSegment = new ArcSegment()
                {
                    Size = new Size(arcWidth, arcHeight),
                    Point = new Point(arcX, arcY),
                    SweepDirection = SweepDirection.Clockwise,
                    IsLargeArc = isLargeArc,
                };
                var line2Segment = new LineSegment(new Point(centerX, centerY), false);

                var pathFigure = new PathFigure(
                    new Point(centerX, centerY),
                    new List<PathSegment>()
                    {
                        line1Segment,
                        arcSegment,
                        line2Segment,
                    },
                    true);

                var pathFigures = new List<PathFigure>() { pathFigure, };
                var pathGeometry = new PathGeometry(pathFigures);
                var path = new Path()
                {
                    Fill = type.ColorBrush,
                    Data = pathGeometry,
                };
                mainCanvas.Children.Add(path);

                prevAngle = angle;


                // draw outlines
                var outline1 = new Line()
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = line1Segment.Point.X,
                    Y2 = line1Segment.Point.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 5,
                };
                var outline2 = new Line()
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = arcSegment.Point.X,
                    Y2 = arcSegment.Point.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 5,
                };

                mainCanvas.Children.Add(outline1);
                mainCanvas.Children.Add(outline2);
            }
        }
        #endregion
    }
    public class Donor_Type
    {
        public float Percentage { get; set; }
        public string Title { get; set; }
        public Brush ColorBrush { get; set; }
    }
}