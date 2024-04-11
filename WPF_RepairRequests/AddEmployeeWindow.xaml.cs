using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPF_RepairRequests.Views;

namespace WPF_RepairRequests
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        DataBaseEntities dbEntities = new DataBaseEntities();
        public AddWindow()
        {
            InitializeComponent();
            fill_comboBox();
        }
        void fill_comboBox()
        {
            DataContext = dbEntities.Position.ToList();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Employee newEmployee = new Employee()
            {
                FirstName = firstNameTextBox.Text,
                LastName = lastNameTextBox.Text,
                Position_Id = positionComboBox.SelectedIndex + 1,
                DateOfBirth = dateOfBirthDatePicker.SelectedDate
            };
            if (newEmployee != null)
            {
                dbEntities.Employee.Add(newEmployee);
                
            }
            else
            {
                MessageBox.Show("Возникла ошибка при добавлении данных, заполнены не все поля.", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            try
            {
                dbEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при добавлении данных в БД: " + ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            EmployeesView.dataGrid.ItemsSource = dbEntities.Employee.Where(em => em.Id != 1).ToList();
            this.Close();
        }
    }
}
