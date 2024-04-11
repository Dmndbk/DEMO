using System;
using System.Linq;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;

namespace WPF_RepairRequests
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        DataBaseEntities dbEntities = new DataBaseEntities();
        public static DataGrid dataGrid;
        public int GetClientId()
        {
            int userId = (int)Repository.user.Id;
            var client = dbEntities.Client.Where(c => c.User_Id == userId);
            int clientId = client.First().Id;
            return clientId;
        }
        public ClientWindow()
        {
            InitializeComponent();
            Load();
            int clientId = GetClientId();
            var cl = dbEntities.Client.Where(c => c.Id == clientId);
            clientTextBlock.Text = cl.First().FirstName + " " + cl.First().LastName.ToString() + ", это ваш список запросов";
        }
        private void Load()
        {
            int clientId = GetClientId();
            myRequestsDataGrid.ItemsSource = dbEntities.Request.Where(r => r.Client_Id == clientId).ToList();
            dataGrid = myRequestsDataGrid;
        }

        private void createRequest_Click(object sender, RoutedEventArgs e)
        {
            AddRequestWindow addRequestWindow = new AddRequestWindow();
            addRequestWindow.ShowDialog();
        }

        private void DeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            int Id = (myRequestsDataGrid.SelectedItem as Request).Id;


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
            int clientId = GetClientId();
            myRequestsDataGrid.ItemsSource = dbEntities.Request.Where(r => r.Client_Id == clientId).ToList();
        }
    }
}
