using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Data;

namespace McLaughlin_University_Donation_Application
{
    /// <summary>
    /// Interaction logic for JobInfo.xaml
    /// </summary>
    public partial class ProfileInformation : Window
    {
        // member id variable
        public string memberID;
        public DataTable dt = new DataTable();
        public ProfileInformation(string memberID)
        {
            InitializeComponent();
            this.memberID = memberID;
            GrabFromDatabase();
            
        }

        #region Sidebar Profile
        private void DashboardClick(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard(memberID);
            this.Close();
            dashboard.Show();
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
                string SelectQuery = "EXEC Profile_View @Member_ID = " + memberID;

                //Step 5: Create an SQL command.
                SqlCommand command = new SqlCommand(SelectQuery, conn);
                SqlDataAdapter sqlData = new SqlDataAdapter(command);
                sqlData.Fill(dt);

                labelEmployeeIDField.Content = dt.Rows[0]["Member_ID"].ToString();
                labelEmployeeNameField.Content = dt.Rows[0]["Member_Name"].ToString();
                labelEmployeePhoneField.Content = dt.Rows[0]["Member_Phone"].ToString();
                labelEmployeeEmailField.Content = dt.Rows[0]["Member_Email"].ToString();
                labelAssignedTypeField.Content = dt.Rows[0]["Type_Name"].ToString();

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
