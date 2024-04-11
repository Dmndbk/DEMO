using System.Windows;
using WPF_RepairRequests.Views;

namespace WPF_RepairRequests
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void rbtnEmploees_Checked(object sender, RoutedEventArgs e)
        //{
        //    contentCTRL.Content = new EmployeesView();
        //}
        //
        //private void rbtnRequests_Checked(object sender, RoutedEventArgs e)
        //{
        //    contentCTRL.Content = new RequestsView();
        //}

        private void requestsBtn_Click(object sender, RoutedEventArgs e)
        {
            contentCTRL.Content = new RequestsView();
        }

        private void employeesBtn_Click(object sender, RoutedEventArgs e)
        {
            contentCTRL.Content = new EmployeesView();
        }
    }
}
