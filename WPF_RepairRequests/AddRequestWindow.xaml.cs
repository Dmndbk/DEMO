using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_RepairRequests.Views;

namespace WPF_RepairRequests
{
    /// <summary>
    /// Логика взаимодействия для AddRequestWindow.xaml
    /// </summary>
    public partial class AddRequestWindow : Window
    {
        DataBaseEntities dbEntities = new DataBaseEntities();
        int userId = (int)Repository.user.Id;
        public AddRequestWindow()
        {
            InitializeComponent();
            equipmentComboBox.ItemsSource = dbEntities.Equipment.OrderBy(e => e.Id).Select(e => e.Name).ToList();
            defectComboBox.ItemsSource = dbEntities.TypeOfDefect.OrderBy(d => d.Id).Select(d => d.Name).ToList();
        }


        private void CreateRequest_Click(object sender, RoutedEventArgs e)
        {
            var client = dbEntities.Client.Where(c => c.User_Id == userId);
            Request newRequest = new Request()
            {
                Description = descriptionTextBox.Text,
                Equipment_Id = equipmentComboBox.SelectedIndex + 1,
                Defect_Id = defectComboBox.SelectedIndex + 1,
                Client_Id = client.First().Id,
                Status_Id = 2,
                Proirity_Id = 4,
                CreationDate = DateTime.Now,
                Employee_Id = 1,
                Cost = "Не указано"
            };
            if (descriptionTextBox.Text == "" || equipmentComboBox.Text == "" || defectComboBox.Text == "")
            {
                MessageBox.Show("Не все поля заполнены ", "Ошибка отправки запроса", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                dbEntities.Request.Add(newRequest);
                try
                {
                    dbEntities.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            

            int clientId = client.First().Id;
            ClientWindow.dataGrid.ItemsSource = dbEntities.Request.Where(r => r.Client_Id == clientId).ToList();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
