using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_RepairRequests.Views;

namespace WPF_RepairRequests
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        DataBaseEntities dbEntities = new DataBaseEntities();
        int Id;
        //public IEnumerable<object> Emp { get; set; }
        public EditWindow(int employeeId)
        {
            InitializeComponent();
            Id = employeeId;
            var selectEmployee = dbEntities.Employee.Where(e => e.Id == Id);

            firstNameTextBox.Text = selectEmployee.ToList().First().FirstName;
            lastNameTextBox.Text = selectEmployee.ToList().First().LastName;
            positionComboBox.SelectedIndex = (int)selectEmployee.ToList().First().Position_Id - 1;
            positionComboBox.ItemsSource = dbEntities.Position.OrderBy(p => p.Id).Select(p => p.Name).ToList();
            dateOfBirthDatePicker.SelectedDate = selectEmployee.ToList().First().DateOfBirth;
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Employee editEmployee = (from emp in dbEntities.Employee
                                     where emp.Id == Id
                                     select emp).Single();

            editEmployee.FirstName = firstNameTextBox.Text;
            editEmployee.LastName = lastNameTextBox.Text;
            editEmployee.Position_Id = positionComboBox.SelectedIndex + 1;
            editEmployee.DateOfBirth = dateOfBirthDatePicker.SelectedDate;

            dbEntities.SaveChanges();
            EmployeesView.dataGrid.ItemsSource = dbEntities.Employee.Where(em => em.Id != 1).ToList();
            this.Close();
        }
    }
}
