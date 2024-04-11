using System.Linq;
using System.Windows;
using WPF_RepairRequests.Views;

namespace WPF_RepairRequests
{
    /// <summary>
    /// Логика взаимодействия для EditRequestWindow.xaml
    /// </summary>
    public partial class EditRequestWindow : Window
    {
        DataBaseEntities dbEntities = new DataBaseEntities();
        int Id;
        public EditRequestWindow(int requestId)
        {
            InitializeComponent();
            Id = requestId;
            var selectedRequest = dbEntities.Request.Where(r => r.Id == Id);



            descriptionTextBox.Text = selectedRequest.ToList().First().Description;
            clientComboBox.SelectedIndex = (int)selectedRequest.ToList().First().Client_Id - 1;
            statusComboBox.SelectedIndex = (int)selectedRequest.ToList().First().Status_Id - 1;
            employeeComboBox.SelectedIndex = (int)selectedRequest.ToList().First().Employee_Id - 1;
            equipmentComboBox.SelectedIndex = (int)selectedRequest.ToList().First().Equipment_Id - 1;
            defectComboBox.SelectedIndex = (int)selectedRequest.ToList().First().Defect_Id - 1;
            priorityComboBox.SelectedIndex = (int)selectedRequest.ToList().First().Proirity_Id - 1;
            closeDatePicker.SelectedDate = selectedRequest.ToList().First().CloseDate;


            clientComboBox.ItemsSource = dbEntities.Client.OrderBy(c => c.Id).Select(c => c.FirstName + " " + c.LastName).ToList();
            statusComboBox.ItemsSource = dbEntities.Status.OrderBy(s => s.Id).Select(s => s.Name).ToList();
            employeeComboBox.ItemsSource = dbEntities.Employee.OrderBy(e => e.Id).Where(e => e.Position_Id == 2).Select(e => e.FirstName + " " + e.LastName).ToList();
            equipmentComboBox.ItemsSource = dbEntities.Equipment.OrderBy(eq => eq.Id).Select(eq => eq.Name).ToList();
            defectComboBox.ItemsSource = dbEntities.TypeOfDefect.OrderBy(d => d.Id).Select(d => d.Name).ToList();
            priorityComboBox.ItemsSource = dbEntities.Priority.OrderBy(p => p.Id).Select(p => p.Name).ToList();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Request editRequest = (from r in dbEntities.Request
                                   where r.Id == Id
                                   select r).Single();

            editRequest.Description = descriptionTextBox.Text;
            editRequest.Status_Id = statusComboBox.SelectedIndex + 1;
            editRequest.Client_Id = clientComboBox.SelectedIndex + 1;
            editRequest.Equipment_Id = equipmentComboBox.SelectedIndex + 1;
            editRequest.Employee_Id = employeeComboBox.SelectedIndex + 1;
            editRequest.Defect_Id = defectComboBox.SelectedIndex + 1;
            editRequest.Proirity_Id = priorityComboBox.SelectedIndex + 1;
            editRequest.CloseDate = closeDatePicker.SelectedDate;

            dbEntities.SaveChanges();
            RequestsView.dataGrid.ItemsSource = dbEntities.Request.ToList();
            this.Close();
        }
    }
}
