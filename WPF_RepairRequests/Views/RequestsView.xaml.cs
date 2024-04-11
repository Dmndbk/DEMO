using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPF_RepairRequests.Views
{
    /// <summary>
    /// Логика взаимодействия для RequestsView.xaml
    /// </summary>
    public partial class RequestsView : UserControl
    {
        DataBaseEntities dbEntities = new DataBaseEntities();
        public static DataGrid dataGrid;
        //public IEnumerable<object> Table { get; set; }
        public RequestsView()
        {
            InitializeComponent();
            //Load();
            dataGrid2.ItemsSource = dbEntities.Request.ToList();
            dataGrid2.SelectedIndex = 0;

            priorityFilterComboBox.SelectedIndex = 0;
        }

        private void Load()
        {
            //Table = from request in dbEntities.Request
            //        join employee in dbEntities.Employee on request.Employee_Id equals employee.Id
            //        join client in dbEntities.Client on request.Client_Id equals client.Id
            //        join status in dbEntities.Status on request.Status_Id equals status.Id
            //        join equipment in dbEntities.Equipment on request.Equipment_Id equals equipment.Id
            //        join defect in dbEntities.TypeOfDefect on request.Defect_Id equals defect.Id
            //        join priority in dbEntities.Priority on request.Proirity_Id equals priority.Id
            //
            //
            //        select new
            //        {
            //            Id = request.Id,
            //            Priority = priority.Name,
            //            Description = request.Description,
            //            Client = client.FirstName,
            //            Status = status.Name,
            //            Employee = employee.FirstName + " " + employee.LastName,
            //            Equipment = equipment.Name,
            //            Defect = defect.Name,
            //            CloseDate = request.CloseDate,
            //            CreationDate = request.CreationDate
            //        };
            //dataGrid2.ItemsSource = Table.ToList();
            dataGrid2.ItemsSource = dbEntities.Request.ToList();

            dataGrid = dataGrid2;
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            int Id = (dataGrid2.SelectedItem as Request).Id;
            EditRequestWindow editRequestWindow = new EditRequestWindow(Id);
            editRequestWindow.ShowDialog();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            int Id = (dataGrid2.SelectedItem as Request).Id;

            var deleteRequest = dbEntities.Request.Where(r => r.Id == Id).Single();
            dbEntities.Request.Remove(deleteRequest);
            try
            {
                dbEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при удалении элемента, возможно он связан с другим элементом другой таблицы: " + ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            dataGrid2.ItemsSource = dbEntities.Request.ToList();
        }

        private void priorityFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (priorityFilterComboBox.SelectedIndex != 0)
            {
                dataGrid2.ItemsSource = dbEntities.Request.Where(r => r.Proirity_Id == priorityFilterComboBox.SelectedIndex).ToList();
            }
            else
            {
                dataGrid2.ItemsSource = dbEntities.Request.ToList();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (searchBox.Text != "")
            {
                if (priorityFilterComboBox.SelectedIndex != 0)
                {
                    dataGrid2.ItemsSource = dbEntities.Request.Where(r => DbFunctions.Like(r.Employee.LastName, searchBox.Text.ToString() + "%") && r.Proirity_Id == priorityFilterComboBox.SelectedIndex).ToList();
                }
                else
                {
                    dataGrid2.ItemsSource = dbEntities.Request.Where(r => DbFunctions.Like(r.Employee.LastName, searchBox.Text.ToString() + "%")).ToList();
                }
            }
            else
            {
                dataGrid2.ItemsSource = dbEntities.Request.ToList();
                MessageBox.Show("Введите ключевое слово.");
            }
        }
    }

}
