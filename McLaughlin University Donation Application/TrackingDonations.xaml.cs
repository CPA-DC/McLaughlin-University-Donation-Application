using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace McLaughlin_University_Donation_Application
{
    /// <summary>
    /// Interaction logic for TrackingDonations.xaml
    /// </summary>
    public partial class TrackingDonations : Window
    {
        // member id variable
        public string memberID;
        public DataTable dt = new DataTable();
        public TrackingDonations(string memberID)
        {
            InitializeComponent();
            this.memberID = memberID;
            GrabFromDatabase();

        }

        #region Sidebar
        private void DashboardClick(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard(memberID);
            this.Close();
            dashboard.Show();
        }

        private void NavJobInfoClick(object sender, RoutedEventArgs e)
        {
            ProfileInformation profileInformation = new ProfileInformation(memberID);
            this.Close();
            profileInformation.Show();
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

        private void NavLogout(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void NavDonationsClick(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Interface

        private void GenerateReportClick(object sender, RoutedEventArgs e)
        {

        }

        private void ImportDataClick(object sender, RoutedEventArgs e)
        {

        }

        private void FundingMonthlyClick(object sender, RoutedEventArgs e)
        {
            // Toggle monthly line
        }

        private void TargetsClick(object sender, RoutedEventArgs e)
        {
            // Toggle targets line
        }
        #endregion

        #region Methods

        public void GrabFromDatabase()
        {
            try
            {
                //step 1 : get connection string from settings.
                string connectstring = Properties.Settings.Default.connectionString;

                //step 2: create a connection object.
                SqlConnection conn = new SqlConnection(connectstring);

                //Step 3: open that connection.
                conn.Open();

                //Step 4: I need to create an SQL query.
                string SelectQuery = "EXEC Donation_Linked_To_Member @Member_ID = " + memberID;

                //Step 5: Create an SQL command.
                SqlCommand command = new SqlCommand(SelectQuery, conn);
                SqlDataAdapter sqlData = new SqlDataAdapter(command);
                sqlData.Fill(dt);
                DonationDataTable.ItemsSource = dt.DefaultView;


                // Close the connection.
                conn.Close();
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        #endregion

        private void NavTargetsClick(object sender, RoutedEventArgs e)
        {
            ProfilePledgeTargets profilePledgeTargets = new ProfilePledgeTargets(memberID);
            this.Close();
            profilePledgeTargets.Show();
        }
    }
}
