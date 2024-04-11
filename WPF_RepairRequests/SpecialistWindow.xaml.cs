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
using System.Windows.Shapes;

namespace WPF_RepairRequests
{
    /// <summary>
    /// Логика взаимодействия для SpecialistWindow.xaml
    /// </summary>
    public partial class SpecialistWindow : Window
    {
        DataBaseEntities dbEntities = new DataBaseEntities();
        public static DataGrid dataGrid;

        public int GetSpecilistId()
        {
            int userId = (int)Repository.user.Id;
            var specialist = dbEntities.Employee.Where(c => c.User_Id == userId);
            int specId = specialist.First().Id;
            return specId;
        }
        public SpecialistWindow()
        {
            InitializeComponent();
            Load();
            specDataGrid.SelectedIndex = 0;
            int specId = GetSpecilistId();
            var cl = dbEntities.Employee.Where(c => c.Id == specId);
            specialistTextBlock.Text = cl.First().FirstName + " " + cl.First().LastName.ToString() + ", вот запросы, которые вам необходимо выполнить";
        }
        private void Load()
        {
            int specId = GetSpecilistId();
            specDataGrid.ItemsSource = dbEntities.Request.Where(r => r.Employee_Id == specId && r.Status_Id != 3).ToList();
            dataGrid = specDataGrid;
        }

        private void ReportRequest_Click(object sender, RoutedEventArgs e)
        {
            int Id = (specDataGrid.SelectedItem as Request).Id;
            ReportWindow repWindow = new ReportWindow(Id);
            repWindow.ShowDialog();
        }

        private void CloseRequest_Click(object sender, RoutedEventArgs e)
        {
            //int Id = (specDataGrid.SelectedItem as Request).Id;
            //var deleteRequest = dbEntities.Request.Where(r => r.Id == Id).Single();

            //dbEntities.Request.Remove(deleteRequest);
            //try
            //{
            //    dbEntities.SaveChanges();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Возникла ошибка при удалении элемента, возможно он связан с другим элементом другой таблицы: " + ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
            //int specId = GetSpecilistId();
            //specDataGrid.ItemsSource = dbEntities.Request.Where(r => r.Employee_Id == specId && r.Status_Id != 3).ToList();

        }
    }
}
