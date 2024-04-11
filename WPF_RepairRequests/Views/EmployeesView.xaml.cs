using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPF_RepairRequests.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : UserControl
    {
        DataBaseEntities dbEntities = new DataBaseEntities();
        public static DataGrid dataGrid;
        //public IEnumerable<object> Table { get; set; }
        public EmployeesView()
        {
            InitializeComponent();
            Load();
            dataGrid1.SelectedIndex = 0;
        }
        private void Load()
        {
            //Table = from employee in dbEntities.Employee
            //        join position in dbEntities.Position on employee.Position_Id equals position.Id
            //
            //        select new
            //        {
            //            Id = employee.Id,
            //            FirstName = employee.FirstName,
            //            LastName = employee.LastName,
            //            Position = position.Name,
            //            DateOfBirth = employee.DateOfBirth
            //        };
            //dataGrid1.ItemsSource = Table.ToList();
            dataGrid1.ItemsSource = dbEntities.Employee.Where(e => e.Id != 1).ToList();
            dataGrid = dataGrid1;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            int Id = (dataGrid1.SelectedItem as Employee).Id;
            EditWindow editWindow = new EditWindow(Id);
            editWindow.ShowDialog();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

            int Id = (dataGrid1.SelectedItem as Employee).Id;
            var deleteEmployee = dbEntities.Employee.Where(emp => emp.Id == Id).Single();

            dbEntities.Employee.Remove(deleteEmployee);
            try
            {
                dbEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при удалении элемента, возможно он связан с другим элементом другой таблицы: " + ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            dataGrid1.ItemsSource = dbEntities.Employee.Where(em => em.Id != 1).ToList();
        }
    }
}
