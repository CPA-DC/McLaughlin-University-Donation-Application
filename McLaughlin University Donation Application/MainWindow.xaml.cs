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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace McLaughlin_University_Donation_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string memberID;

        public MainWindow()
        {
            InitializeComponent();
            textBoxUsername.Focus();
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            memberID = textBoxUsername.Text.ToString();
            if (int.TryParse(memberID, out int numericValue))
            {
                if (numericValue > 0)
                {

                    // Password
                    if (textBoxPassword.Password.ToString() == "password")
                    {
                        Dashboard dashboard = new Dashboard(memberID);
                        this.Close();
                        dashboard.Show();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Password, Please Try Again.", "Access Attempt Denied", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Please Enter In Member ID Correctly", "Access Attempt Denied", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            else
            {
                MessageBox.Show("Please Enter In Member ID Correctly", "Access Attempt Denied", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
