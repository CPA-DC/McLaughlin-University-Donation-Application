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
    public partial class ProfilePledgeTargets : Window
    {
        // member id variable
        public string memberID;
        public DataTable dt = new DataTable();
        public ProfilePledgeTargets(string memberID)
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

        private void NavLogout(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void NavDonorsClick(object sender, RoutedEventArgs e)
        {
            TrackingDonor trackingDonor = new TrackingDonor(memberID);
            this.Close();
            trackingDonor.Show();
        }

        private void NavDonationsClick(object sender, RoutedEventArgs e)
        {
            TrackingDonations trackingDonations = new TrackingDonations(memberID);
            this.Close();
            trackingDonations.Show();
        }

        private void NavPledgesClick(object sender, RoutedEventArgs e)
        {
            TrackingPledges trackingPledges = new TrackingPledges(memberID);
            this.Close();
            trackingPledges.Show();
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
                string SelectQuery = "EXEC Pledges_Tracking @Member_ID = " + memberID;

                //Step 5: Create an SQL command.
                SqlCommand command = new SqlCommand(SelectQuery, conn);
                SqlDataAdapter sqlData = new SqlDataAdapter(command);
                sqlData.Fill(dt);
                PledgesDataTable.ItemsSource = dt.DefaultView;


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
    }
}
